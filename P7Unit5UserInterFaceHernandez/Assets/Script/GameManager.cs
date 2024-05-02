using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float spawnRate = 1.0f;
    public List<GameObject> targets;
    private int score;
    public bool isGameActive;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;

    //music

    public AudioClip easy;
    public AudioClip medium;
    public AudioClip hard;
    private AudioSource source;

    public GameObject titleBg;
    public GameObject ezBg;
    public GameObject mdBg;
    public GameObject hdBg;
    public TextMeshProUGUI livesText;
    private int lives;
    public GameObject pauseScreen;
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }
    private void ChangedPaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangedPaused();
        }
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score:" + score;
    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }
    public void RestartGme()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(38);


        //I wonder who wrote this little bit of code

        if (difficulty == 1)
        {
            Destroy(titleBg);
            ezBg.gameObject.SetActive(true);
            source.Pause();
            source.clip = easy;
            source.Play();
        }
        else if(difficulty == 2)
        {
            Destroy(titleBg);
            mdBg.gameObject.SetActive(true);
            source.Pause();
            source.clip = medium;
            source.Play();
        }
        else
        {
            Destroy(titleBg);
            hdBg.gameObject.SetActive(true);
            source.Pause();
            source.clip = hard;
            source.Play();
        }

        titleScreen.gameObject.SetActive(false);
    }
    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }
}
