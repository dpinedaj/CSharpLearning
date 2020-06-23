using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Personaje : MonoBehaviour
{

    public Rigidbody2D personajeRB2;
    public SpriteRenderer personajeren;
    public Animator personajeAnim;
    public Collider2D personajeColl;
    public LayerMask layerSuelo;
    public LayerMask layerLomita;
    public Transform chequearSuelo;
    public Transform limiteDerecho;
	public Transform limiteIzquierdo;
	public Transform limiteCabeza;
    
 [Range(0f,10f)]
    public float velocidadLoma = 5f;
    public float velMax;
    public float poderSalto;
    private bool voltearPersonaje = true;
    public float radioChequeo = 0.2f;
    


    void Start()
    {
        personajeRB2.GetComponent<Rigidbody2D> ();
        personajeren.GetComponent<SpriteRenderer>();
        personajeAnim.GetComponent<Animator>();
        personajeColl.GetComponent<Collider2D>();
        
        
    }
    void Update()

    {   // Mover en el eje X
        float mover = Input.GetAxis("Horizontal");
        personajeRB2.velocity = new Vector2(mover * velMax, personajeRB2.velocity.y);
        //voltear y correr
        if ((mover > 0 && !voltearPersonaje) ||(mover < 0 && voltearPersonaje))
        {
            voltearPersonaje = !voltearPersonaje;
            personajeren.flipX = !personajeren.flipX;
        }
            personajeAnim.SetFloat("VelocMov",Mathf.Abs(mover));

        //Variables y constantes antes de saltar y agachar
        bool enSuelo = Physics2D.OverlapCircle(chequearSuelo.position,radioChequeo,layerSuelo);
        personajeAnim.SetBool("estaEnSuelo", enSuelo);
        personajeAnim.SetBool("estaEnAire",false);
        personajeColl.transform.localPosition = new Vector2(0f,0f);
        personajeColl.transform.localScale= new Vector2(1f,1f);
        personajeren.transform.localPosition = new Vector2(0f,0f);
        bool enLomita = Physics2D.OverlapCircle(chequearSuelo.position,radioChequeo,layerLomita);
        personajeAnim.SetBool("estaEnLomita", enLomita);
        personajeAnim.SetBool("agachado",false);

        //saltar
        if ((enSuelo || enLomita) && (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.UpArrow) ||Input.GetKeyDown(KeyCode.W)))
        {
            personajeAnim.SetBool("estaEnAire",true);
            personajeRB2.AddForce(new Vector2(0, poderSalto),ForceMode2D.Impulse);
            enSuelo = false;
            enLomita = false;
        }

        //Agachar
        if (Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.S) )
        {
            personajeAnim.SetBool("estaEnLomita",true);
            personajeAnim.SetBool("agachado", true);
            personajeColl.transform.localScale= new Vector2(1f,0.5f);
            personajeColl.transform.localPosition = new Vector2(0f,-0.21f);
            personajeren.transform.localPosition = new Vector2(0f,-0.1f);

            if(enSuelo)
            {
                //personajeRB.velocity = new Vector2(0f,personajeRB.velocity.y);
            }

            if(enLomita)
            {
                float movery = Input.GetAxis("Vertical");
                personajeRB2.velocity = new Vector2 (movery + velocidadLoma ,movery - velocidadLoma );
            }
        }

        if( Input.GetKey(KeyCode.C))
            {
            personajeAnim.SetBool("estaEnLomita",true);
            personajeAnim.SetBool("agachado", true);
            personajeColl.transform.localScale= new Vector2(1f,0.5f);
            personajeColl.transform.localPosition = new Vector2(0f,-0.21f);
            personajeren.transform.localPosition = new Vector2(0f,-0.1f);
            if(!enLomita && !enSuelo)
            {
                personajeRB2.AddForce(new Vector2(0f,-50f),ForceMode2D.Force);
            }
            if(enLomita)
            {
                float movery = Input.GetAxis("Vertical");
                personajeRB2.velocity = new Vector2 (movery + velocidadLoma ,movery - velocidadLoma );
            }
            }

        bool muroDerecha = Physics2D.OverlapCircle(limiteDerecho.position,radioChequeo,layerSuelo);
        bool muroIzquierda = Physics2D.OverlapCircle(limiteIzquierdo.position,radioChequeo,layerSuelo);
        if((muroDerecha && mover > 0) || (muroIzquierda && mover < 0))
        {
            personajeRB2.velocity = new Vector2(0f,personajeRB2.velocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "monedaOro")
        {
            Destroy(other.gameObject);
        }
    }
	private void OnCollisionEnter2D(Collision2D other)
	{
		bool golpeaCabeza = Physics2D.OverlapCircle(limiteCabeza.position,radioChequeo,layerSuelo);
		if(golpeaCabeza && other.gameObject.tag =="cajaConMoneda")
		{
			other.gameObject.SendMessage("abrirCaja");
		}
	}


}
