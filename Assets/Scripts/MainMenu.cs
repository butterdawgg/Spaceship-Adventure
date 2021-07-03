using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;
    public Button optionsButton;

    void Awake()
    {
        playButton.onClick.AddListener(OnClickPlayButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        optionsButton.onClick.AddListener(OnClickOptionsButton);
    }

    public void OnClickPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }

    public void OnClickOptionsButton()
    {

    }
}
