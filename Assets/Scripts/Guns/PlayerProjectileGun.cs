using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileGun : Gun
{
    [SerializeField] float projectileSpeed;
    [SerializeField] int projectileCount;
    [SerializeField] float inaccuracyAngle;
    [SerializeField] GameObject projectilePrototype;

    private KeyCode fireKey;

    void Awake()
    {
        fireKey = SerializeManager.Instance.GetControls(ControlsType.Shoot);

        if (projectileCount <= 0)
            projectileCount = 1;

        StartCoroutine(FireCoroutine());
    }

    private void Fire()
    {
        for(int i = 1; i <= projectileCount; i++)
        {
            GameObject projectile = Instantiate(projectilePrototype, muzzlePoint.transform.position, transform.rotation);
            projectile.GetComponent<Projectile>().LaunchFromPlayer(damage, projectileSpeed, inaccuracyAngle);
            Physics.IgnoreCollision(GetComponentInChildren<Collider>(), projectile.GetComponent<Collider>());
        }

        Player.Energy -= energyDraw;
        AudioManager.Instance.PlaySound("PlayerFireProjectile");
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
