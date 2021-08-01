using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileGun : Gun
{
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject projectilePrototype;

    private KeyCode fireKey;

    void Awake()
    {
        fireKey = SerializeManager.Instance.GetControls(ControlsType.Shoot);

        StartCoroutine(FireCoroutine());
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrototype, muzzlePoint.transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().Launch(damage, projectileSpeed, true);
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
