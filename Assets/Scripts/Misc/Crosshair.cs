using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    
    void Update()
    {
        Vector2 mouseDistance;
        mouseDistance.x = ((Input.mousePosition.x - Screen.width * 0.5f) / Screen.width * 0.5f) * Screen.width;
        mouseDistance.y = ((Input.mousePosition.y - Screen.height * 0.5f) / Screen.height * 0.5f) * Screen.height;

        Vector3 lookDir = Vector3.zero;

        if (SerializeManager.Instance.GetBool(BoolType.MouseInversionXAxis) & SerializeManager.Instance.GetBool(BoolType.MouseInversionYAxis))
            lookDir = new Vector3(-mouseDistance.x, -mouseDistance.y);
        else if (!SerializeManager.Instance.GetBool(BoolType.MouseInversionXAxis) & SerializeManager.Instance.GetBool(BoolType.MouseInversionYAxis))
            lookDir = new Vector3(mouseDistance.x, -mouseDistance.y);
        else if (SerializeManager.Instance.GetBool(BoolType.MouseInversionXAxis) & !SerializeManager.Instance.GetBool(BoolType.MouseInversionYAxis))
            lookDir = new Vector3(-mouseDistance.x, mouseDistance.y);
        else if (!SerializeManager.Instance.GetBool(BoolType.MouseInversionXAxis) & !SerializeManager.Instance.GetBool(BoolType.MouseInversionYAxis))
            lookDir = new Vector3(mouseDistance.x, mouseDistance.y);

        arrow.transform.localRotation = Quaternion.LookRotation(lookDir.normalized, -transform.forward);

        float distance = (Input.mousePosition - transform.position).magnitude;
        Image arrowImg = arrow.transform.GetChild(0).gameObject.GetComponent<Image>();
        arrowImg.color = new Color(255, 255, 255, distance * 0.002f);
    }
}
