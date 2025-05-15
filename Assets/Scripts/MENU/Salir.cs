using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Salir : MonoBehaviour
{


    public void Salir_(){
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }
    
}
