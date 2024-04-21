using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    [SerializeField] Item[] items;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public GameObject GetActiveItemPrefab(IntType itemIdType)
    {
        foreach(Item i in items)
        {
            if (i.idType == itemIdType & i.id == SerializeManager.Instance.GetInt(itemIdType))
                return i.prefab;
        }

        throw new System.ArgumentException("There is no active item id of type: " + itemIdType.ToString());
    }
}
