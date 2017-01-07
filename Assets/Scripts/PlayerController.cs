using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float Speed;
    public float tilt;
    public float fireRate;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;

    private Rigidbody rb;
    private float nextTime;
    private AudioSource audioShot;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioShot = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextTime)
        {
            nextTime = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioShot.Play();
        }

    } 

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        // rigidbody.velocity = 
        // GetComponent("")


        rb.velocity = movement * Speed;

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

        rb.rotation = Quaternion.Euler(0,0, rb.velocity.x * -tilt);
    }
}
