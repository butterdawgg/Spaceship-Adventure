using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementInMainMenu : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddTorque(transform.up * 10f);
    }
}
