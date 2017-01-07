using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            PlayerController player  =   go.GetComponent<PlayerController>();
            player.fireRate = player.fireRate * 0.7f;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            PlayerController player = go.GetComponent<PlayerController>();
            player.fireRate = player.fireRate * 1.3f;
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
				Instantiate (hazard, spawnPosition, spawnRotation);
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
        bacjgroundScrolling.scrollSpeed = bacjgroundScrolling.scrollSpeed * (1 + (wave / 10)); // +2% background Speed after every wave;
        scoreText.text =  "BacSpeed: " + bacjgroundScrolling.scrollSpeed;
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + Score;
    }
    #endregion


}
