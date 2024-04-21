using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGun : Gun
{
    [SerializeField] GameObject projectilePrototype;
    [SerializeField] float projectileSpeed;
    public bool CanFire { get; set; }

    void Awake()
    {
        StartCoroutine(FireCoroutine());
    }

    private void Fire() 
    {
        GameObject projectile = Instantiate(projectilePrototype, muzzlePoint.transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().LaunchFromEnemy(damage, projectileSpeed);
        AudioManager.Instance.PlaySound("EnemyFire");
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
