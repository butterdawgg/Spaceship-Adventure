using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;

    [SerializeField] Button playButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button shopButton;
    [SerializeField] Button shopBackButton;
    [SerializeField] Text highScoreText;


    [SerializeField] GameObject options;
    [SerializeField] GameObject shop;

    [SerializeField] Button optionsBackButton;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Toggle mouseInversionYAxisToggle;
    [SerializeField] Toggle mouseInversionXAxisToggle;

    void Awake()
    {
        playButton.onClick.AddListener(OnClickPlayButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        optionsButton.onClick.AddListener(OnClickOptionsButton);
        optionsBackButton.onClick.AddListener(OnClickOptionsBackButton);
        shopButton.onClick.AddListener(OnClickShopButton);
        shopBackButton.onClick.AddListener(OnClickShopBackButton);

        masterVolumeSlider.onValueChanged.AddListener(OnChangeValueMasterVolumeSlider);
        SFXVolumeSlider.onValueChanged.AddListener(OnChangeValueSFXVolumeSlider);
        musicVolumeSlider.onValueChanged.AddListener(OnChangeValueMusicVolumeSlider);
        mouseInversionYAxisToggle.onValueChanged.AddListener(OnChangeValueMouseInversionYAxisToggle);
        mouseInversionXAxisToggle.onValueChanged.AddListener(OnChangeValueMouseInversionXAxisToggle);
    }

    void Update()
    {
        highScoreText.text = "HIGH SCORE: " + SerializeManager.Instance.GetFloat(FloatType.HighScore);
    }

    public void OnClickPlayButton()
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        SceneManager.LoadScene(1);
    }

    public void OnClickExitButton()
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        Application.Quit();
    }

    public void OnClickShopButton()
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        mainMenu.SetActive(false);
        shop.SetActive(true);
    }

    public void OnClickShopBackButton()
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        mainMenu.SetActive(true);
        shop.SetActive(false);
    }

    public void OnClickOptionsButton()
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        mainMenu.SetActive(false);
        options.SetActive(true);

        mouseInversionYAxisToggle.isOn = SerializeManager.Instance.GetBool(BoolType.MouseInversionYAxis);
        mouseInversionXAxisToggle.isOn = SerializeManager.Instance.GetBool(BoolType.MouseInversionXAxis);

        masterVolumeSlider.value = SerializeManager.Instance.GetFloat(FloatType.MasterVolume);
        SFXVolumeSlider.value = SerializeManager.Instance.GetFloat(FloatType.SfxVolume);
        musicVolumeSlider.value = SerializeManager.Instance.GetFloat(FloatType.MusicVolume);
    }

    public void OnClickOptionsBackButton()
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        mainMenu.SetActive(true);
        options.SetActive(false);
    }

    public void OnChangeValueMasterVolumeSlider(float value)
    {
        SerializeManager.Instance.SetFloat(FloatType.MasterVolume, value);
    }

    public void OnChangeValueSFXVolumeSlider(float value)
    {
        SerializeManager.Instance.SetFloat(FloatType.SfxVolume, value);
    }

    public void OnChangeValueMusicVolumeSlider(float value)
    {
        SerializeManager.Instance.SetFloat(FloatType.MusicVolume, value);
    }

    public void OnChangeValueMouseInversionYAxisToggle(bool value)
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        SerializeManager.Instance.SetBool(BoolType.MouseInversionYAxis, value);
    }

    public void OnChangeValueMouseInversionXAxisToggle(bool value)
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        SerializeManager.Instance.SetBool(BoolType.MouseInversionXAxis, value);
    }
}
