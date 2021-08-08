using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum FloatType
{
    MasterVolume = 0,
    SfxVolume = 1,
    MusicVolume = 2,
    HighScore = 3,
    Money = 4
}

public enum BoolType
{
    MouseInversionXAxis = 0,
    MouseInversionYAxis = 1,
}

public enum IntType
{
    ActiveShipId = 0,
    ActiveGunId = 1,
}

public enum ControlsType
{
    Shoot = 0,
    FlyForward = 1,
    FlyBackward = 2,
    FlyLeft = 3,
    FlyRight = 4,
    FlyUp = 5,
    FlyDown = 6,
    RollLeft = 7,
    RollRight = 8
}

public enum ControlsDefaults
{
    Mouse0 = 0,
    W = 1,
    S = 2,
    Q = 3,
    E = 4,
    Space = 5,
    LeftShift = 6,
    A = 7,
    D = 8
}

public class SerializeManager
{
    public static SerializeManager Instance { get; }

    static SerializeManager()
    {
        Instance = new SerializeManager();
    }

    private SerializeManager() { }

    public void SetFloat(FloatType type, float value) 
    { 
        PlayerPrefs.SetFloat(type.ToString(), value);
    }

    public float GetFloat(FloatType type) 
    { 
        if (PlayerPrefs.HasKey(type.ToString())) 
            return PlayerPrefs.GetFloat(type.ToString()); 
        else
            return 0f; 
    }

    public void SetInt(IntType type, int value) 
    { 
        PlayerPrefs.SetInt(type.ToString(), value);
    }

    public int GetInt(IntType type)
    {
        if (PlayerPrefs.HasKey(type.ToString()))
            return PlayerPrefs.GetInt(type.ToString());
        else
            return 0;
    }

    public void SetBool(BoolType type, bool value) 
    {
        PlayerPrefs.SetInt(type.ToString(), Convert.ToInt32(value)); 
    }

    public bool GetBool(BoolType type) 
    { 
        if (PlayerPrefs.HasKey(type.ToString()))
            return Convert.ToBoolean(PlayerPrefs.GetInt(type.ToString()));
        else 
            return false; 
    }

    public void SetControls(ControlsType type, KeyCode value) 
    { 
        PlayerPrefs.SetString(type.ToString(), value.ToString()); 
    }

    public KeyCode GetControls(ControlsType type) 
    {
        string value = PlayerPrefs.GetString(type.ToString());

        if (!string.IsNullOrEmpty(value))
            return (KeyCode)Enum.Parse(typeof(KeyCode), value.ToString());
        else
            return (KeyCode)Enum.Parse(typeof(KeyCode), ((ControlsDefaults)type).ToString());
    }

    public void SetItemLockedState(Item item, bool isLocked)
    {
        PlayerPrefs.SetInt(item.name + "_" + item.id, Convert.ToInt32(isLocked));
    }

    public bool GetItemLockedState(Item item)
    {
        if (PlayerPrefs.HasKey(item.name + "_" + item.id))
            return Convert.ToBoolean(PlayerPrefs.GetInt(item.name + "_" + item.id));
        else
            return true;
    }
}
