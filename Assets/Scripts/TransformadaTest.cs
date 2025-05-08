using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TransformadaTest : MonoBehaviour
{
    public bool moverse;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position);
        Debug.Log(transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (moverse)
        {
            transform.position = new Vector3(transform.position.x, 
                                                transform.position.y + 0.001f, 
                                                transform.position.z);
        }   
    }
}
