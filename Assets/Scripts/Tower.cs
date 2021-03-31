using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Entity
{
    //Public variables:
    public float radius;
    public float smoothness;
    public GameObject towerBase;
    public Vector3 defaultPos;
    
    //Private variables
    private Rigidbody rb;
    private bool isOnTarget;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(FireCoroutine());
        StartCoroutine(DieCoroutine());
        ps.Stop();
    }

    void FixedUpdate()
    { 
        if (health <= 0)
        {
            isOnTarget = false;
            return;
        }

        transform.localPosition = defaultPos;
        Vector3 dirToTarget = (Player.Position - transform.position).normalized;

        float distance = (Player.Position - transform.position).magnitude;

        if (distance > radius | healthBar == null)
        {
            healthBar.SetActive(false);
        }
        if (distance < radius & healthBar != null)
        {
            healthBar.SetActive(true);
            healthBar.transform.rotation = Quaternion.LookRotation(dirToTarget, towerBase.transform.up);
            healthBar.transform.localScale = new Vector3(healthBarScale * health, 0.2f, 1f);
        }
         
        if (distance > radius)
        {
            isOnTarget = false;
            return;
        }
        isOnTarget = true;


        Quaternion orientation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-dirToTarget), smoothness);
        rb.MoveRotation(orientation);
    }

    void Fire()
    {
        Quaternion orientation = Quaternion.LookRotation(-transform.forward);
        GameObject projectile = Instantiate(projectileProtoype, muzzlePoint.transform.position, orientation);
        projectile.GetComponent<Projectile>().Damage = damage;
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;
    }
    
    private IEnumerator FireCoroutine()
    {
        if (isOnTarget & Player.Health > 0)
        {
            yield return new WaitForSeconds(cooldown);
            Fire();
        }
        yield return null;
        StartCoroutine(FireCoroutine());
    }

    IEnumerator DieCoroutine()
    {
        if(health <= 0)
        {
            ps.Play();
            Player.Score += scoreGetAmount;
            Destroy(healthBar);
            Destroy(body);
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
        yield return null;
        StartCoroutine(DieCoroutine());
    }
}
