using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public GameObject arrow;
    //(Input.mousePosition - arrow.transform.localPosition).normalized
    void Update()
    {
        arrow.transform.LookAt(Input.mousePosition, -transform.forward);
        float distance = (Input.mousePosition - transform.position).magnitude;
        Image arrowImg = arrow.transform.GetChild(0).gameObject.GetComponent<Image>();
        arrowImg.color = new Color(255, 255, 255, distance * 0.002f);
    }
}
