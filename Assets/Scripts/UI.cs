using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public float scale = 1;
    public Image healthImg;
    public Text healthTxt;
    public Image energyImg;
    public Text energyTxt;
    public Text scoreTxt;
    public Text highScoreTxt;
    public GameObject pauseMenu;

    public Button resetHighScoreButton;
    public Button exitToMenuButton;

    public static bool IsPaused { get; private set; }

    private static bool pause = false;

    void Start()
    {
        resetHighScoreButton.onClick.AddListener(ResetHighScore);
        exitToMenuButton.onClick.AddListener(ExitToMenu);
        Pause();
    }

    void Update()
    {
        IsPaused = pause;

        scoreTxt.text = Player.Score.ToString();
        highScoreTxt.text = Player.HighScore.ToString();

        healthTxt.text = Mathf.Ceil(Player.Health).ToString();
        healthImg.transform.localScale = new Vector3(Player.Health * scale, 1f, 1f);

        energyTxt.text = Mathf.Ceil(Player.Energy).ToString();
        energyImg.transform.localScale = new Vector3(Player.Energy * scale, 1f, 1f);
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
                Resume();
            else
                Pause();
        }
    }

    void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
    }

    public void ExitToMenu()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        SceneManager.LoadScene(0);
    }

    public void ResetHighScore()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        Player.HighScore = 0f;
        PlayerPrefs.SetFloat("HighScore", Player.HighScore);
    }
}
