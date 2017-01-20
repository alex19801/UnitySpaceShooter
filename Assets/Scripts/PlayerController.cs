using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public Transform primalSpawn;
    public Transform leftSpawn;
    public Transform rightlSpawn;

    public Weapon weapon;
    public Dictionary<ShipSlots, Item> equipment = new Dictionary<ShipSlots, Item>();


    public Dictionary<ShipSlots, Transform> shotSpawnPoints = new Dictionary<ShipSlots, Transform>();
    private Rigidbody rb;
    private Dictionary<ShipSlots, float> slotTimers = new Dictionary<ShipSlots, float>();


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        GameController pc = go.GetComponent<GameController>() as GameController;
        
        pc.SweachWeapon("WeaponLaser");
        //pc.SweachWeapon("WeaponDefault", ShipSlots.leftWing);
        //pc.SweachWeapon("WeaponDefault", ShipSlots.rightWing);
        // equipment[ShipSlots.rightWing].autoShot = true;

        // initialize timers
        foreach (ShipSlots slot in Enum.GetValues(typeof(ShipSlots)).Cast<ShipSlots>())
        {
            slotTimers[slot] = Time.time;
        }
        //print(GetComponent<ShotSpawnPrimal>().transform);
        //shotSpawnPoints[ShipSlots.primalWeapon] = GetComponent<ShotSpawnLeft>().transform;
        //shotSpawnPoints[ShipSlots.leftWing] = GetComponent<ShotSpawnLeft>().transform;
        //shotSpawnPoints[ShipSlots.rightWing] = GetComponent<ShotSpawnRight>().transform;
        shotSpawnPoints[ShipSlots.primalWeapon] = primalSpawn;
        shotSpawnPoints[ShipSlots.leftWing] = leftSpawn;
        shotSpawnPoints[ShipSlots.rightWing] = rightlSpawn;

    }

    private Transform FindTransform(Transform[] parent, String name)
    {
        //if (parent.name == name) return parent;
        //var transforms = parent.GetComponentsInChildren<Transform>();
        Transform[] transforms = parent;
        foreach (Transform t in transforms)
        {
            if (t.name == name) return t;
        }

        return null;
    }

    private void Update()
    {
        foreach (ShipSlots currentSlot in Enum.GetValues(typeof(ShipSlots)).Cast<ShipSlots>())
        {
            if (equipment.ContainsKey(currentSlot) && equipment[currentSlot] != null)
            {
                ShotWeapon(currentSlot);
            }
        }

    } 
    //public static class Extension 
    //{
    //    public static void GetListMethod(this ShipSlots i)
    //    {
    //        Enum ee = Enum.GetValues(typeof(ShipSlots)).Cast<ShipSlots>();
    //        return ee;
    //    }
    //}

    private void ShotWeapon(ShipSlots slot)
    {

        if (equipment.ContainsKey(slot)
            && equipment[slot] != null)
        {
            if (Time.time > slotTimers[slot] && (true || equipment[slot].autoShot || Input.GetButton("Fire1")))
            {
                Weapon weapon = equipment[slot].weapon;
                Quaternion razbros = weapon.transform.rotation;
                razbros.y += UnityEngine.Random.Range(-weapon.dispersion, weapon.dispersion);

                slotTimers[slot] = Time.time + weapon.fireRate; // fireRate;
                Transform spawnPoint = shotSpawnPoints[slot];
                GameObject ammo = Instantiate(weapon.ammo, spawnPoint.position, razbros);
                ammo.GetComponent<Mover>().Speed = weapon.bulletSpead;
                Health damage = ammo.GetComponent<Health>();
                if (damage != null)
                {
                    damage.health = weapon.damage;
                }
            }
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
