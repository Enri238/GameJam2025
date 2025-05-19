using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CongelarMovimiento : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Jugador;
    public CegaroManager cegaroManager;
    public GameObject cegaroPanel;

    void Start()
    {
        if (!cegaroManager)
        {
			Jugador.GetComponent<HeroKnightv2>().enabled = false; // Deshabilitar movimiento del jugador
																  //Jugador.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Detener movimiento del jugador
			Invoke("ActivaMovimiento", 2.5f); // Llamar a la función ActivaMovimiento después de 2 segundos
		}
    }

    void Update()
    {
        if (cegaroManager && cegaroManager.GetRemainingCegaros() <= 0) // Chapucilla para la cinemática de muerte
		{
            Jugador.GetComponent<HeroKnightv2>().StopAllCoroutines(); // Se podría haber hecho con Invoke
            Jugador.GetComponent<HeroKnightv2>().enabled = false;
			if (cegaroPanel)
            {
				cegaroPanel.SetActive(false);
			}
		}
    }

	// Update is called once per frame
	void ActivaMovimiento()
    {
        Jugador.GetComponent<HeroKnightv2>().enabled = true; // Habilitar movimiento del jugador
    }
}
