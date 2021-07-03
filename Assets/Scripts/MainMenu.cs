using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Transform mainCamera;

    public GameObject mainMenu;
    
    public Button playButton;
    public Button exitButton;
    public Button optionsButton;


    public GameObject options;

    public Button optionsBackButton;
    public Slider masterVolumeSlider;
    public Toggle mouseInversionToggle;
    public Toggle rollAndRightLeftMovementSwapToggle;

    void Awake()
    {
        playButton.onClick.AddListener(OnClickPlayButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        optionsButton.onClick.AddListener(OnClickOptionsButton);
        optionsBackButton.onClick.AddListener(OnClickOptionsBackButton);

        masterVolumeSlider.onValueChanged.AddListener(delegate { OnChangeValueMasterVolumeSlider(); });
        mouseInversionToggle.onValueChanged.AddListener(delegate { OnChangeValueMouseInversionToggle(); });
        rollAndRightLeftMovementSwapToggle.onValueChanged.AddListener(delegate { OnChangeValueRollAndRightLeftMovementSwapToggle(); });

        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
    }

    void FixedUpdate()
    {
        mainCamera.rotation = Quaternion.Euler(0f, mainCamera.eulerAngles.y + 0.33f, 0f);
    }

    public void OnClickPlayButton()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        SceneManager.LoadScene(1);
    }

    public void OnClickExitButton()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        Application.Quit();
    }

    public void OnClickOptionsButton()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        mainMenu.SetActive(false);
        options.SetActive(true);

        if (PlayerPrefs.GetInt("IsMouseInverted") == 0)
            mouseInversionToggle.isOn = false;
        else
            mouseInversionToggle.isOn = true;

        if (PlayerPrefs.GetInt("IsRollAndRightLeftMovementSwapped") == 0)
            rollAndRightLeftMovementSwapToggle.isOn = false;
        else
            rollAndRightLeftMovementSwapToggle.isOn = true;
    }

    public void OnClickOptionsBackButton()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        mainMenu.SetActive(true);
        options.SetActive(false);
    }

    public void OnChangeValueMasterVolumeSlider()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
    }

    public void OnChangeValueMouseInversionToggle()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        if (mouseInversionToggle.isOn)
            PlayerPrefs.SetInt("IsMouseInverted", 1);
        else
            PlayerPrefs.SetInt("IsMouseInverted", 0);
    }

    public void OnChangeValueRollAndRightLeftMovementSwapToggle()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        if (rollAndRightLeftMovementSwapToggle.isOn)
            PlayerPrefs.SetInt("IsRollAndRightLeftMovementSwapped", 1);
        else
            PlayerPrefs.SetInt("IsRollAndRightLeftMovementSwapped", 0);
    }
}
