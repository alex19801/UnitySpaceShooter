using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public float StartDelay;
	public float waveDelay;
	public float betwinWaveDelay;
    public GUIText scoreText;
    public GUIText gameOverText;
    public GUIText restartText;


    private bool gameOver;
    private bool restart;
    private int Score;
    private bool paused;
    private Inventory _inventory;
    int wave;

    #region PublicMethods
    public void AddScore(int newScore)
    {
        //Debug.Log("ADD score: " + newScore + " - Score: " + Score);
        Score += newScore;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "GAME OVER";
        gameOver = true;
    }

    public void Restart()
    {
        
    }
    #endregion

    #region PrivateMethods
    void Start()
    {
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        restartText.text = "";
        Score = 0;
        wave = 0;
        UpdateScore();
        StartCoroutine(spawnHazard());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SweachWeapon("WeaponCannon");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SweachWeapon("WeaponDefault");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_inventory == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("Inventary");
                _inventory = go.GetComponent<Inventory>();
            }

            if (!_inventory.isDraggable)
            {
                _inventory.visible = !_inventory.visible;
                Pause(_inventory.visible);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Pause();
        }
    }

    private bool Pause(bool? pause = null)
    {
        if (pause == null)
        {
            pause = !pause;
        }

        if ((bool)pause)
        {
            Time.timeScale = 0;
            paused = true;
        }
        else
        {
            Time.timeScale = 1;
            paused = false;
        }

        return paused;
        
    } 

    public void SweachWeapon(string weaponName)
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        PlayerController player = go.GetComponent<PlayerController>();

        var w1 = Resources.FindObjectsOfTypeAll(typeof(Weapon)) as Weapon[];

        foreach (Weapon item in w1)
        {
            if (item.name == weaponName)
            {
                print("GameObjects " + item);
                player.weapon = item;
            }
        }
    }

    IEnumerator spawnHazard()
	{
		yield return new WaitForSeconds (StartDelay);

		while (true) {
            wave++;
			for (int i = 0; i < wave + 5; i++) {
                GameObject hazard = hazards[UnityEngine.Random.Range(0, hazards.Length)];

                Vector3 spawnPosition = new Vector3 (UnityEngine.Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, hazard.transform.rotation);
				yield return new WaitForSeconds (waveDelay);

                
			}

            UpadateWave();

            yield return new WaitForSeconds (betwinWaveDelay);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart.";
                restart = true;
                break;
            }
		}
	}

    private void UpadateWave()
    {
        GameObject bacground = GameObject.FindGameObjectWithTag("BackGround");
        BackgroundScrolling bacjgroundScrolling = bacground.GetComponent<BackgroundScrolling>();
        bacjgroundScrolling.scrollSpeed = bacjgroundScrolling.scrollSpeed * (1f + (wave / 100f)); // +2% background Speed after every wave;
        scoreText.text =  "BacSpeed: " + bacjgroundScrolling.scrollSpeed;
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + Score;
    }
    #endregion


}
