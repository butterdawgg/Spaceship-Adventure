using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public float scale = 1;
    public Image img;
    public Text healthTxt;
    public Text scoreTxt;
    public Text highScoreTxt;
    public static bool pause = false;
    public GameObject pauseMenu;

    void Start()
    {
        Pause();
    }

    void Update()
    {
        scoreTxt.text = Player.Score.ToString();
        highScoreTxt.text = Player.HighScore.ToString();

        if (Player.Health <= 0)
        {
            healthTxt.text = "0";
            img.transform.localScale = new Vector3(0f, 1f, 1f);
        }
        else
        {
            healthTxt.text = Player.Health.ToString();
            img.transform.localScale = new Vector3(Player.Health * scale, 1f, 1f);
        }

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
}
