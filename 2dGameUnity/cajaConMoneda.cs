using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cajaConMoneda : MonoBehaviour
{
    public Animator cajaAnim;
    // Start is called before the first frame update
    void Start()
    {
        cajaAnim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void abrirCaja()
    {
        cajaAnim.Play("cajaConMoneda");
    }
}
