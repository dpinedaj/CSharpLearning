using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSigue : MonoBehaviour
{
    public Transform objetivo;
    public float suavizado = 5f;
    Vector3 desfase;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        Vector3 posicionObjetivo = new Vector3(objetivo.position.x,objetivo.position.y+1f,-10f);
        transform.position = Vector3.Lerp ( transform.position, posicionObjetivo, suavizado*Time.deltaTime );
         
    }
}
