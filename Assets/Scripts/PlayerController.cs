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
    // public float fireRate;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public Weapon weapon;


    private Rigidbody rb;
    private float nextTime;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextTime)
        {
            nextTime = Time.time + weapon.fireRate; // fireRate;
            GameObject ammo =  Instantiate(weapon.ammo, shotSpawn.position, shotSpawn.rotation);
            Health damage = ammo.GetComponent<Health>();
            if (damage != null) {
                damage.health = weapon.damage;
            }
            //Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    } 

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity = movement * Speed;

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

        rb.rotation = Quaternion.Euler(0,0, rb.velocity.x * -tilt);
    }
}
