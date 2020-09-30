namespace Tests
{
    public sealed class WordCloud
    internal WordCloud(ICommentsDao dao, // Tools to make queries into db -> extract mispellings, commonwords and more
            SingleObjectCache<CommonText> commonTextCache, // There will be stored the commontext and refeeded 
            ICommentsService commentsService, // Integrates the dao and every resource needed to get survey questions, comments, words, and internally uses DAO to make needed queries
            IBenchmarkService benchmarkService, // Get Benchmark values from topworkplaces, also can create benchmark thresholds, take correlations and comparations
            ISurveyCategoryService surveyCategoryService, // Internally get the data of surveys categories, surveys info, using different event ids and specific benchmarking info
            IScoreService scoreService, // Get scrores from benchmarking, topworkplaces, depending in which demographic is, survey, user scope and more
            SurveyEvent surveyEvent, // Instanciate every feature of a survey and his characteristics
            Company company,// sector, features and respective access to it 
            Sector sector,// sector tree information
            Benchmark benchmark, // results, percentile range, new results and respective group
            UserScope userScope, // Define threshold score, percentiles using the CorrelationSet, internally validates scores
            Responders responders,// TODO Check what does
            SurveyCategories surveyCategories,// Survey Categories related to benchmark 
            SurveyCategory surveyCategory,// Category including the survey themes
            SurveyTheme surveyTheme, // Survey Theme including the questions
            SurveyQuestion surveyQuestion, // Specific Survey question referred to a survey Category and survey theme with Ids
            CommentQuestion question, // Check what kind of question is and returns the question Id
            Nodes nodes,// Contains all the information of departments, hierarchy, parameters #TODO CHECK IT
            Demographics demographics, // Contains the demographic types and demographic information
            IEnumerable<CommentsForQuestion> comments, //CommentsForQuestion: Return the comments if the question have by his category, theme and question
            bool areLeaderCommentsVisible, 
            bool isGeneratingBenchmarkData,
            IApplicationLog log //Logs generator to persist them
            )
        {
            IDictionary<string, string> spellingCorrections = commentsService.GetSpellingCorrections(); //Makes a query from CommonMisspellings
            CommonText commonText = dao.SelectCommonText(commonTextCache); //Makes a query from CommentCommonText
            DateTime overallStart = DateTime.Now;
            DateTime start = DateTime.Now;
            Dictionary<string, string> departmentNames = GetAllDepartmentNames(nodes); // Add all the departaments to a dict by looping the nodes
            IEnumerable<CommentsForQuestion> commentsForQuestions = comments.ToList(); //
            PartialDroplets partialDroplets =
                GetPartialDroplets(spellingCorrections, commonText, sector, commentsForQuestions, company,
                    departmentNames, log); /*Defines the level (the color) of the comment to treat, the words of a comment, extracting from
                them the common text, spellingCorrections and returning a list */
            log.LogPerformance("WordCloud.getDropletCounts()", start);
            if (partialDroplets.Count > 0)
            {
                if (isGeneratingBenchmarkData)
                {
                    start = DateTime.Now;
                    GenerateBenchmarkData(dao, surveyEvent.Id, partialDroplets);
                    log.LogPerformance("WordCloud.generateBenchmarkData()", start);
                }
                else
                {
                    start = DateTime.Now;
                    partialDroplets.CompareToBenchmark(dao, surveyEvent.Id, demographics,
                        surveyCategory?.Id, surveyTheme?.Id, surveyQuestion?.Id, question?.Id, nodes.Department.Id,
                        areLeaderCommentsVisible); /*Initially check the demographic characteristics after it, takes the benchmark variables from a query using the commentdao,
                         after it, filter it using some weighting */
                    log.LogPerformance("PartialDroplets.CompareToBenchmark()", start);
                    if (partialDroplets.Count > 0)
                    {
                        start = DateTime.Now;
                        IList<CommentWordGrouping> wordGroupings = commentsService.GetCommentWordGroupings(); // Using the commentdao extract the commentword group ids, name and words
                        partialDroplets.FilterAndGroupRedundantDroplets(wordGroupings);/* Using these wordGroupings, extract it from the 
                        partialDroplets letting, taking the word count , using the max comments and so on. and as a result there are new droplets with grouped data*/
                        log.LogPerformance("PartialDroplet.FilterAndGroupRedundentDroplets()", start);
                        start = DateTime.Now;
                        SetScoresAndRatios(benchmarkService, surveyCategoryService, scoreService, userScope, benchmark,
                            responders, surveyCategories, surveyCategory, surveyTheme, surveyQuestion, nodes,
                            demographics, partialDroplets);
                        log.LogPerformance("WordCloud.setScoreAndRatio()", start);
                        start = DateTime.Now;
                        partialDroplets.SetEmphasis();
                        log.LogPerformance("PartialDroplets.SetEmphasis()", start);
                        IDictionary<CommentId, Comment> commentsById = commentsForQuestions
                            .SelectMany(q => q.Select(comment => new {comment.CommentId, Comment = comment}))
                            .ToDictionary(c => c.CommentId, c => c.Comment);

                        foreach (PartialDroplet partialDroplet in partialDroplets)
                        {
                            var droplet = new WordDroplet(partialDroplet.Text,
                                partialDroplet.Weight, partialDroplet.Occurrences,
                                partialDroplet.IsEmphasizedPositive,
                                partialDroplet.IsEmphasizedNegative,
                                partialDroplet.PositiveOccurrenceRatio,
                                partialDroplet.Score,
                                partialDroplet.OccurrenceRatio,
                                partialDroplet.PositiveFeedbackOccurences,
                                partialDroplet.ImprovementSuggestionOccurences,
                                partialDroplet.NeutralOccurences,
                                partialDroplet.GroupId,
                                partialDroplet.PositiveCommentIds.Select(id => commentsById[id]),
                                partialDroplet.ImprovementCommentIds.Select(id => commentsById[id]));
                            droplets.Add(droplet);
                            AddDropletToCommentDictionary(DropletsByCommentId, droplet, partialDroplet);
                            if (partialDroplet.Weight > maxWeight)
                            {
                                maxWeight = partialDroplet.Weight;
                            }
                            else if (partialDroplet.Weight < minWeight)
                            {
                                minWeight = partialDroplet.Weight;
                            }
                        }

                        droplets.Sort(WordDropleComparator.Alphabetical);
                    }
                }
            }

            log.LogPerformance("WordCloud construction", overallStart);
        }
    
}