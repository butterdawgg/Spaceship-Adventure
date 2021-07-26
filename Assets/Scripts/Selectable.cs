using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectable : MonoBehaviour
{
    public Item item;
    public Text nameText;
    public Text descriptionText;
    public Text selectedText;

    void Awake()
    {
        nameText.text = item.name;
        descriptionText.text = item.description;
        DeselectAll();
    }

    public void Select()
    {
        SerializeManager.Instance.SetInt(item.idType, item.id);
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        selectedText.gameObject.SetActive(true);
        Debug.Log(item.name + " (id: " + item.id + ") is selected as active");
    }
    public void Deselect()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        selectedText.gameObject.SetActive(false);
    }

    public static void DeselectAll()
    {
        foreach(Selectable s in FindObjectsOfType<Selectable>())
        {
            s.Deselect();
        }
    }
}
