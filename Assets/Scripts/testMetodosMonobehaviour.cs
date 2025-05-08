using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMetodosMonobehaviour : MonoBehaviour
{
    public int num;

    private void Awake()
    {
        Debug.Log("Awake...." + num);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start...." + num);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update...." + num);
        Incrementar();
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate...." + num);
    }

    private void LateUpdate()
    {
        Debug.Log("LateUpdate...." + num);
    }

    private void Incrementar()
    {
        num++;
    }
}
