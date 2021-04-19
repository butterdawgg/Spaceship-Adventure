using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzlePoint;
    public GameObject projectilePrototype;
    public float damage;
    public float cooldown;
    public float projectileSpeed;

    protected void Fire()
    {
        GameObject projectile = Instantiate(projectilePrototype, muzzlePoint.transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().Damage = damage;
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;
    }
}
