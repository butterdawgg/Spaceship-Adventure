using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickableType
{
    Life,
    Energy
}

public class Pickable : MonoBehaviour
{
    public PickableType pickableType;
    public float amount;
    public float pickupDistance;
    public float pickupVelocity;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float distance = (Player.Position - transform.position).magnitude;
        Vector3 direction = (Player.Position - transform.position).normalized;

        if (distance < pickupDistance)
        {
            rb.velocity = direction * pickupVelocity;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (pickableType == PickableType.Life)
            Player.Health += amount;
        else if (pickableType == PickableType.Energy)
            Player.Energy += amount;

        FindObjectOfType<AudioManager>().PlaySound("Pickup");

        Destroy(gameObject);
    }
}