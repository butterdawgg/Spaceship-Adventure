using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Gun
{
    public int fireButtonIndex;

    void Awake()
    {
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        if (Input.GetMouseButton(fireButtonIndex))
        {
            yield return new WaitForSeconds(cooldown);
            Fire();
        }
        yield return null;
        StartCoroutine(FireCoroutine());
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrototype, muzzlePoint.transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().Damage = damage;
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;
    }
}
