using UnityEngine;

public class CreditsAnimationEvents : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        Debug.Log("Animación terminada!");
        SceneController.instance.NextLevel();
    }
}
