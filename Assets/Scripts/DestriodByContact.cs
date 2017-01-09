using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestriodByContact : MonoBehaviour
{

    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject ammoExplosion;
    public int scoreValue;
    public float health;

    private GameController gameController;


    private void Start()
    {
        GameObject gameContrrollerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameContrrollerObject != null)
        {
            gameController = gameContrrollerObject.GetComponent<GameController>();
        }

        if (gameContrrollerObject == null)
        {
            Debug.Log("Cannot find 'GameControlle' script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.name);
        if (other.tag == "Boundary" || other.tag == "Enemy")
        {
            return;
        }

        Health otherObject = other.gameObject.GetComponent<Health>();
        float otherHealth;
        if (otherObject == null)
        {
            otherHealth = 999;
        }
        else
        {
            otherHealth = otherObject.health;
        }
         
        if (other.tag == "Ammo" || other.CompareTag ("Player"))
        {
            Instantiate(ammoExplosion, transform.position, transform.rotation);
            float ammoHealth = otherHealth;
            otherHealth -= health;
            otherObject.health = otherHealth;
            health -= ammoHealth;
            if (other.tag == "Player")
            {
                GameObject gameContrrollerObject = GameObject.FindGameObjectWithTag("GUI");
                if (gameContrrollerObject != null)
                {
                    GUIText gameController = gameContrrollerObject.GetComponent<GUIText> ();
                }
                else
                {
                    Debug.Log("Cannot find 'GameControlle' script");
                }
            }
        }

        if (other.tag == "Player" && otherHealth <= 0)
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
            Destroy(other.gameObject);
        }

        if (health <= 0)
        {
            gameController.AddScore(scoreValue);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (otherHealth <= 0)
        {
            //Debug.Log(otherHealth);
            Destroy(other.gameObject);
        }
    }
}
