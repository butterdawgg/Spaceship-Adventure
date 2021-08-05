using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask layerMask;

    private float Damage;
    private bool IsFriendly;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void Launch(float damage, float speed, bool isFriendly)
    {
        Damage = damage;
        IsFriendly = isFriendly;

        if (!IsFriendly)
        {
            Vector3 initialForward = transform.forward;
            transform.LookAt(Player.Position);
            if (Vector3.Angle(initialForward, transform.forward) > 10f)
                transform.forward = initialForward;
        }
        else
        {
            Ray ray = new Ray(Player.playerCamera.transform.position, Player.playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
                transform.LookAt(hit.point);
            else
                transform.LookAt(ray.GetPoint(100f));
        }

        rb.velocity = transform.forward * speed;

        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Entity entity) & IsFriendly)
        {
            entity.Health -= Damage;
            AudioManager.Instance.PlaySound("EnemyHit");
        }
        else if (other.gameObject.TryGetComponent(out Player player) & !IsFriendly)
        {
            Player.Health -= Damage;
            if (Player.Health > 0)
                AudioManager.Instance.PlaySound("PlayerHit");
        }
    }
}
