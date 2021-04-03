using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGun : Gun
{
    public bool CanFire { get; set; } 

    void Awake()
    {
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        if (CanFire)
        {
            yield return new WaitForSeconds(cooldown);
            Fire();
        }
        yield return null;
        StartCoroutine(FireCoroutine());
    }

    public void Fire()
    {
        GameObject projectile = Instantiate(projectilePrototype, muzzlePoint.transform.position, transform.rotation);
        Projectile pr = projectile.GetComponent<Projectile>();
        pr.Damage = damage;
        pr.Speed = projectileSpeed;
    }
}
