using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongelarMovimiento : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Jugador;

    void Start()
    {
        Jugador.GetComponent<HeroKnightv2>().enabled = false; // Deshabilitar movimiento del jugador
        //Jugador.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Detener movimiento del jugador
        Invoke("ActivaMovimiento", 2.5f); // Llamar a la función ActivaMovimiento después de 2 segundos

    }

    // Update is called once per frame
    void ActivaMovimiento()
    {
        Jugador.GetComponent<HeroKnightv2>().enabled = true; // Habilitar movimiento del jugador
    }
}
