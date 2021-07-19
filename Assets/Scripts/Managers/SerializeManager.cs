using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum FloatType
{
    MasterVolume,
    SfxVolume,
    MusicVolume,
    HighScore
}

public enum BoolType
{
    MouseInversionXAxis,
    MouseInversionYAxis,
}

public enum ControlsType
{
    ShootPrimary = 0,
    ShootSecondary = 1,
    FlyForward = 2,
    FlyBackward = 3,
    FlyLeft = 4,
    FlyRight = 5,
    FlyUp = 6,
    FlyDown = 7,
    RollLeft = 8,
    RollRight = 9
}

public enum ControlsDefault
{
    Mouse0,
    Mouse1,
    W,
    S,
    A,
    D,
    Space,
    LeftShift,
    Q,
    E
}

public class SerializeManager
{
    public static SerializeManager Instance { get; }

    static SerializeManager()
    {
        Instance = new SerializeManager();
    }

    private SerializeManager() { }

    public void SetFloat(FloatType type, float value) { PlayerPrefs.SetFloat(type.ToString(), value); }

    public float GetFloat(FloatType type) 
    { 
        if (PlayerPrefs.HasKey(type.ToString())) 
            return PlayerPrefs.GetFloat(type.ToString()); 
        else
            return 0f; 
    }

    public void SetBool(BoolType type, bool value) { PlayerPrefs.SetInt(type.ToString(), Convert.ToInt32(value)); }

    public bool GetBool(BoolType type) 
    { 
        if (PlayerPrefs.HasKey(type.ToString()))
            return Convert.ToBoolean(PlayerPrefs.GetInt(type.ToString()));
        else 
            return false; 
    }

    public void SetControls(ControlsType type, KeyCode value) { PlayerPrefs.SetString(type.ToString(), value.ToString()); }

    public KeyCode GetControls(ControlsType type) 
    {
        string value = PlayerPrefs.GetString(type.ToString());

        if (!string.IsNullOrEmpty(value))
            return (KeyCode)Enum.Parse(typeof(KeyCode), value.ToString());
        else
            return (KeyCode)Enum.Parse(typeof(KeyCode), ((ControlsDefault)type).ToString());
    }
}
