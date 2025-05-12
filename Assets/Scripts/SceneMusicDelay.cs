using UnityEngine;
using System.Collections;

public class SceneMusicActivator : MonoBehaviour
{
    public GameObject sceneMusicObject;
    public float delaySeconds = 3f;
    public bool MusicOn = true;
    void Start()
    {
        if(MusicOn){
            if (sceneMusicObject != null)
            {
                sceneMusicObject.SetActive(false); // Por seguridad, lo apaga al inicio
                StartCoroutine(ActivateAfterDelay());
            }
        }
        
    }

    IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(delaySeconds);
        sceneMusicObject.SetActive(true);
    }
}
