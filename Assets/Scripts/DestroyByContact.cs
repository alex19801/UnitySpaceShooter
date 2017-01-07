using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
    public int scoreValue;

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
        if (other.tag == "Boundary")
        {
            return;
        }

		Instantiate (explosion, transform.position, transform.rotation);

		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
		}

        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
