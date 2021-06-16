using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private static bool pause = false;

    void Start()
    {
        Pause();
    }

    void Update()
    {
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
}
