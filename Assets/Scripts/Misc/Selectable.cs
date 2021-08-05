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

    public Button unlockButton;

    void Awake()
    {
        //SerializeManager.Instance.SetItemLockedState(item, true);
        //SerializeManager.Instance.SetFloat(FloatType.Money, 0f);
        nameText.text = item.name;
        descriptionText.text = item.description;
        unlockButton.onClick.AddListener(OnClickUnlockButton);
        unlockButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = "UNLOCK: " + item.unlockPrice;
        DeselectAll();
    }

    void Update()
    {
        item.isLocked = SerializeManager.Instance.GetItemLockedState(item);
    }

    public void Select()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        if (item.isLocked)
        {
            unlockButton.gameObject.SetActive(true);
            SerializeManager.Instance.SetInt(item.idType, 0);
        }
        else
        {
            selectedText.gameObject.SetActive(true);
            SerializeManager.Instance.SetInt(item.idType, item.id);
            Debug.Log(item.name + " (id: " + item.id + ") is selected as active");
        }
    }
    public void Deselect()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        selectedText.gameObject.SetActive(false);
        unlockButton.gameObject.SetActive(false);
    }

    public static void DeselectAll()
    {
        foreach(Selectable s in FindObjectsOfType<Selectable>())
        {
            s.Deselect();
        }
    }

    public void OnClickUnlockButton()
    {
        if (item.unlockPrice <= SerializeManager.Instance.GetFloat(FloatType.Money))
        {
            AudioManager.Instance.PlaySound("ButtonClick");
            SerializeManager.Instance.SetItemLockedState(item, false);
            SerializeManager.Instance.SetFloat(FloatType.Money, SerializeManager.Instance.GetFloat(FloatType.Money) - item.unlockPrice);
        }
    }
}
