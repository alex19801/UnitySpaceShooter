using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float Speed;
    
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * Speed;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
