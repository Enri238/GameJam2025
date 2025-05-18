using UnityEngine;

public class NextLevelOnP : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
