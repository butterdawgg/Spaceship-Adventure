using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeybindManager
{
    public static KeybindManager Instance { get; }

    static KeybindManager()
    {
        Instance = new KeybindManager();
    }

    private KeybindManager() { InitializeDictionary(); }

    private Dictionary<ControlsType, KeyCode> keybinds;

    private void InitializeDictionary()
    {
        keybinds = new Dictionary<ControlsType, KeyCode>();

        foreach (ControlsType type in System.Enum.GetValues(typeof(ControlsType)))
        {
            keybinds.Add(type, SerializeManager.Instance.GetControls(type));
        }
    }

    public void BindKey(ControlsType type, KeyCode key)
    {
        if (!keybinds.ContainsValue(key))
        {
            keybinds[type] = key;
            SerializeManager.Instance.SetControls(type, key);
        }
        else
        {
            ControlsType type1 = FindFirstKeyByValue(keybinds, key);

            keybinds[type1] = KeyCode.None;
            SerializeManager.Instance.SetControls(type1, KeyCode.None);

            keybinds[type] = key;
            SerializeManager.Instance.SetControls(type, key);
        }
    }

    public static K FindFirstKeyByValue<K, V>(Dictionary<K, V> dict, V value)
    {
        foreach (KeyValuePair<K, V> pair in dict)
        {
            if (EqualityComparer<V>.Default.Equals(pair.Value, value))
            {
                return pair.Key;
            }
        }
        return default;
    }
}