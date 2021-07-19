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

        Camera.main.gameObject.GetComponent<Rigidbody>().AddTorque(Camera.main.transform.up * 10f);

        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /*
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Camera.main.gameObject.GetComponent<Rigidbody>().AddTorque(Camera.main.transform.up * 10f);
    }
    */
}
