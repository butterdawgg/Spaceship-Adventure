using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGun : Gun
{
    public bool CanFire { get; set; }
    public GameObject projectilePrototype;
    public float projectileSpeed;

    void Awake()
    {
        StartCoroutine(FireCoroutine());
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrototype, muzzlePoint.transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().Damage = damage;
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;
        projectile.GetComponent<Projectile>().IsFriendly = false;

        FindObjectOfType<AudioManager>().PlaySound("EnemyFire");
    }

    private IEnumerator FireCoroutine()
    {
        if (CanFire & !UI.IsPaused)
        {
            Fire();
            yield return new WaitForSeconds(cooldown);
        }
        yield return null;
        StartCoroutine(FireCoroutine());
    }
}
