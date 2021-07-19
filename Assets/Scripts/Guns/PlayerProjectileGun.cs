using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileGun : Gun
{
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject projectilePrototype;
    [SerializeField] int fireButtonIndex;

    void Awake()
    {
        StartCoroutine(FireCoroutine());
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrototype, muzzlePoint.transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().Damage = damage;
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;
        projectile.GetComponent<Projectile>().IsFriendly = true;
        Player.Energy -= energyDraw;
        FindObjectOfType<AudioManager>().PlaySound("PlayerFireProjectile");
    }

    private IEnumerator FireCoroutine()
    {
        if (Input.GetMouseButton(fireButtonIndex) & Player.Energy > 0 & !UI.IsPaused)
        {
            Fire();
            yield return new WaitForSeconds(cooldown);    
        }
        yield return null;
        StartCoroutine(FireCoroutine());
    }
}
