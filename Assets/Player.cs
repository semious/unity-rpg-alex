using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float xInput;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(Input.GetAxisRaw("Horizontal"));  
        xInput = Input.GetAxisRaw("Horizontal");
    }
}
