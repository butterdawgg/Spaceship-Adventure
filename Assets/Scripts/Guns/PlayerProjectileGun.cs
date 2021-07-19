using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileGun : Gun
{
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject projectilePrototype;

    [SerializeField] GunType type;

    private KeyCode fireKey;

    void Awake()
    {
        if (type == GunType.Main)
            fireKey = SerializeManager.Instance.GetControls(ControlsType.ShootPrimary);
        else if (type == GunType.Side)
            fireKey = SerializeManager.Instance.GetControls(ControlsType.ShootSecondary);

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
        if (Input.GetKey(fireKey) & Player.Energy > 0 & !UI.IsPaused)
        {
            Fire();
            yield return new WaitForSeconds(cooldown);    
        }
        yield return null;
        StartCoroutine(FireCoroutine());
    }
}
