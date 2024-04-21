using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class Item : ScriptableObject
{
    public new string name;
    [TextArea]
    public string description;
    public GameObject prefab;
    public IntType idType;
    public int id;
    public float unlockPrice;
    public bool isLocked;
}
