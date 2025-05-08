using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    public string nombre = "Merlin";
	public int ataque = 5;
    public bool tieneBarba;
    private float experiencia;


    void Start()
    {
        Debug.Log(nombre);
        Debug.Log(ataque);
        Debug.Log(tieneBarba);
        Debug.Log(experiencia);
    }
}

