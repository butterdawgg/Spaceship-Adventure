using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] float scale = 1;
    [SerializeField] Image healthImg;
    [SerializeField] Text healthTxt;
    [SerializeField] Image energyImg;
    [SerializeField] Text energyTxt;
    [SerializeField] Text scoreTxt;
    [SerializeField] Text highScoreTxt;
    [SerializeField] GameObject pauseMenu;

    [SerializeField] Button exitToMenuButton;

    public static bool IsPaused { get; private set; }

    private static bool pause = false;

    void Start()
    {
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
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
        Cursor.visible = true;
    }

    public void ExitToMenu()
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        Resume();
        Cursor.visible = true;
        if (Player.HighScore < Player.Score)
            Player.HighScore = Player.Score;
        SceneManager.LoadScene(0);
    }
}
