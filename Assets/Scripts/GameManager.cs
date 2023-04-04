using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // Public
    public GameObject hazardPrefab;
    public int maxHazardToSpawn = 3;
    public float maxDrag = 2f;
    public TMPro.TextMeshProUGUI scoreText;
    public Image backgroundMenu;

    public GameObject player;

    public GameObject mainVCam;
    public GameObject zoomVCam;

    public GameObject gameOverMenu;

    public int HighScore => highScore;

    // Private

    private int highScore;
    private int score;
    private float timer;
    private bool gameOver;

    

    private Coroutine hazardsCoroutine;
    private static GameManager instance;
    private const string HighScorePreferenceKey = "HighScore";

    public static GameManager Instance => instance;



    void Start()
    {

        instance = this;
        
        highScore = PlayerPrefs.GetInt(HighScorePreferenceKey);

    }

    private void OnEnable() {

        player.SetActive(true);

        mainVCam.SetActive(true);
        zoomVCam.SetActive(false);

        gameOver = false;
        scoreText.text = "0";
        score = 0;
        timer = 0;

        hazardsCoroutine = StartCoroutine(SpawnHazards());
    }


    private void Update() {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                StartCoroutine(ScaleTime(0, 1, 0.5f));
                backgroundMenu.gameObject.SetActive(false);
            }

            if (Time.timeScale == 1) 
            {
                StartCoroutine(ScaleTime(1, 0,  0.5f));
                backgroundMenu.gameObject.SetActive(true);
            }
        }




        if (gameOver)
            return;


        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            score ++;
            scoreText.text = score.ToString();

            timer = 0;
        }
    }

    IEnumerator ScaleTime(float start, float end, float duration)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer  = 0.0f;

        while (timer < duration)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / duration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }

        Time.timeScale = end;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }


    private IEnumerator SpawnHazards()
    {

        var hazardToSpawn = Random.Range(1, maxHazardToSpawn);

        for (int i = 0; i < hazardToSpawn; i++)
        {
            var X = Random.Range(-7,7);
            var drag = Random.Range(0f, maxDrag);

            var hazard = Instantiate(hazardPrefab, new Vector3 (X, 11, 0), Quaternion.identity);
            hazard.GetComponent<Rigidbody>().drag = drag;

        }

        
        yield return new WaitForSeconds(1f);

        yield return SpawnHazards();
    }

    public void GameOver()
    {
        StopCoroutine(hazardsCoroutine);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt( HighScorePreferenceKey, highScore);
        }

        gameOver = true;

        mainVCam.SetActive(false);
        zoomVCam.SetActive(true);

        gameObject.SetActive(false);
        gameOverMenu.SetActive(true);

        
    }

    public void Enable() {
        gameObject.SetActive(true);
        

    
    }

}
