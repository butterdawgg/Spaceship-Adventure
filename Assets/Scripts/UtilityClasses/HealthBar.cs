using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthBar
{
    public GameObject bar;
    public float scale;

    private float k;

    public void SetupK(float value)
    {
        k = scale / value;
    }

    public void Do(float value, float distance, float radius, Vector3 direction, Vector3 up)
    {
        if (distance < radius)
            bar.SetActive(true);
        else
            bar.SetActive(false);

        bar.transform.localScale = new Vector3(value * k, bar.transform.localScale.y, 0f);
        bar.transform.rotation = Quaternion.LookRotation(direction, up);
    }

    public void Destroy()
    {
        GameObject.Destroy(bar);
    }
}
