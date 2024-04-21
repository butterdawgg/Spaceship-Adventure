using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlsWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ControlsType type;

    private bool mouseOver;

    void Update()
    {
        foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode))
            {
                if (mouseOver)
                {
                    KeybindManager.Instance.BindKey(type, kcode);
                }
            }
        }

        transform.GetChild(0).GetComponent<Text>().text = SerializeManager.Instance.GetControls(type).ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}