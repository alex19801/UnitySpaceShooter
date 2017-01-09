using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

    public GameObject spawnPoint;
    public GameObject ammo;
    public float delay;
    public float repeatTime;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Fire", delay, repeatTime);
	}
	
	// Update is called once per frame
	void Fire () {
        Instantiate(ammo, spawnPoint.transform.position, spawnPoint.transform.rotation);
	}
}
