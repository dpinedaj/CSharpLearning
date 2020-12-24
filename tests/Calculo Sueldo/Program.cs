using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CalculoSueldo
{
    class Program
    {
        static void Main(string[] args)
        {
            int horasTrabajadas;
            float costoHora;
            float sueldo;
            string linea;


            //Se ingresan los datos leyendo lo escrito en la consola
            Console.Write("Ingrese horas trabajadas por el operario: ");//Es un print y se convierte en input con el ===> ReadLine
            linea = Console.ReadLine();
            horasTrabajadas = int.Parse(linea);

            Console.Write("Ingrese el pago por hora: ");
            linea = Console.ReadLine();
            costoHora = float.Parse(linea);
            
            sueldo = horasTrabajadas * costoHora;

            Console.Write("El sueldo total del operario es: ");
            Console.Write(sueldo);
            Console.ReadKey();




        }
    }

}