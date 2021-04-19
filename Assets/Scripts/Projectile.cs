using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage { get; set; }
    public bool isFriendly;

    public LayerMask layerMask;

    void Awake()
    {
        if (!isFriendly)
        {
            Vector3 initialForward = transform.forward;
            transform.LookAt(Player.Position);
            if (Vector3.Angle(initialForward, transform.forward) > 10f)
                transform.forward = initialForward;
        }
        Destroy(gameObject, 10f);
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
