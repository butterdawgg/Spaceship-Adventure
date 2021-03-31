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
    public int fireButtonIndex;

    // Start is called before the first frame update
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
