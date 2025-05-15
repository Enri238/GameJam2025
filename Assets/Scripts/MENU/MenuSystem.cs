using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSystem : MonoBehaviour
{
    private Animator transitionAnimator;
    // [SerializeField] private float transitionTime = 1f;

    

    public void Jugar()
    {
        //podemos escribir directamente el numero de la escena, el numero 0
        // es la escena de menu.
        int nextSceneIncex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIncex);
        // StartCoroutine(SceneLoad(nextSceneIncex));
    }

    public void Salir(){
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }
    
    // public IEnumerator SceneLoad(int sceneIndex){
    //     transitionAnimator.SetTrigger("StartTransition");
    //     yield return new WaitForSeconds(transitionTime);
    //     SceneManager.LoadScene(sceneIndex);
    // }
}
