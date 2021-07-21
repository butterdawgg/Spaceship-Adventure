using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovementInMainMenu : MonoBehaviour
{
    public static CameraMovementInMainMenu Instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(RotateCoroutine());
    }

    public IEnumerator RotateCoroutine()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * 10f);
        else
            RenderSettings.skybox.SetFloat("_Rotation", 0f);

        yield return new WaitForEndOfFrame();

        StartCoroutine(RotateCoroutine());
    }
}
