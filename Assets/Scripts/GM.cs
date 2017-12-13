using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour {

    public int lives = 3;
    public int score = 0;
    public int bricks = 20;
    public Text livesText;
    public Text scoreText;
    public float resetDelay = 1f;
    public GameObject Paddle;
    public GameObject deathParticles;
    public GameObject EndScreen;
    public GameObject bricksPrefab;
    public static GM instance = null;
    private GameObject clonePaddle;

	void Start ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Setup();
	}
	
    public void Setup()
    {
        clonePaddle = Instantiate(Paddle, transform.position, Quaternion.identity) as GameObject;
        Instantiate(bricksPrefab, transform.position, Quaternion.identity);
    }

    void CheckGameOver()
    {
        if(bricks < 1)
        {
            EndScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        if(lives < 1)
        {
            EndScreen.SetActive(true);
            Time.timeScale =0f;
        }
    }

    public void LoseLife()
    {
        lives--;
        livesText.text = ("Lives: " + lives);
        Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
        Destroy(clonePaddle);
        Invoke("SetupPaddle", resetDelay);
        CheckGameOver();
    }

    void SetupPaddle()
    {
        clonePaddle = Instantiate(Paddle, transform.position, Quaternion.identity) as GameObject;
    }

    public void DestroyBrick()
    {
        bricks--;
        ScoreUp(100);
        CheckGameOver();
    }

    void ScoreUp(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }
}
