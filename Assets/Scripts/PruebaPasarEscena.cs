using UnityEngine;

public class NextLevelOnP : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
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
}
