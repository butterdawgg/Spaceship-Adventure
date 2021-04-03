using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage { get; set; }
    public float Speed { get; set; }
    public bool isFriendly;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (!isFriendly)
        {
            Vector3 dirToTarget = (Player.Position - transform.position).normalized;
            rb.MoveRotation(Quaternion.LookRotation(dirToTarget));
        }
        
        Destroy(gameObject, 10f);
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * Speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Entity>(out Entity entity) == true & isFriendly)
        {
            entity.health -= Damage;
        }
        else if (other.gameObject.TryGetComponent<Player>(out Player player) == true & !isFriendly)
        {
            player.health -= Damage;
        }
    }
}
