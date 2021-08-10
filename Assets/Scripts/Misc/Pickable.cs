using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickableType
{
    Life,
    Energy,
    Money
}

public class Pickable : MonoBehaviour
{
    [SerializeField] PickableType pickableType;
    [SerializeField] float amount;
    [SerializeField] float pickupDistance;
    [SerializeField] float pickupVelocity;

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
        else if (pickableType == PickableType.Money)
            SerializeManager.Instance.SetFloat(FloatType.Money, SerializeManager.Instance.GetFloat(FloatType.Money) + amount);

        AudioManager.Instance.PlaySound("Pickup");

        Destroy(gameObject);
    }
}