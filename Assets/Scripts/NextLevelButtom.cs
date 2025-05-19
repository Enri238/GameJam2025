
using UnityEngine;

public class NextLevelButtom : MonoBehaviour
{
    // Start is called before the first frame update
    public void NextLevel() {
        if (SceneController.instance != null)
        {
            SceneController.instance.NextLevel();
        }
        else
        {
            Debug.LogWarning("SceneController.instance no está asignado.");
        }
    }
    
}
