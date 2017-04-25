using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour {

    public float ScrollSpeed;
    public int Wave;

    private Vector3 startPosition;
	
	void Start () {
        startPosition = transform.position;
	}
	
	void Update () {

        float newPosition = Mathf.Repeat(Time.time * ScrollSpeed, transform.localScale.y);

        transform.position = startPosition + Vector3.forward * newPosition;
	}
}
