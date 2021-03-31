using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //Public variables:
    public float maxSpeed;
    public float rotationSpeed;
    public float stopRadius;
    public float shootRadius;
    public float goRadius;

    //Private variables:
    private Rigidbody rb;
    Vector3 dirToTarget;
    bool canFire;

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
            canFire = false;
            return;
        }

        dirToTarget = (Player.Position - transform.position).normalized;
        float distance = (Player.Position - transform.position).magnitude;

        if (distance > shootRadius | healthBar == null)
        {
            healthBar.SetActive(false);
        }
        if (distance < shootRadius & healthBar != null)
        {
            healthBar.SetActive(true);
            healthBar.transform.rotation = Quaternion.LookRotation(dirToTarget, transform.up);
            healthBar.transform.localScale = new Vector3(healthBarScale * health, 0.15f, 1f);
        }

        if (distance < goRadius & distance > shootRadius & distance > stopRadius)
        {
            Rotate();
            Move();
            canFire = false;
        }
        else if (distance < goRadius & distance < shootRadius & distance > stopRadius)
        {
            Rotate();
            Move();
            canFire = true;
        }
        else if (distance < goRadius & distance < shootRadius & distance < stopRadius)
        {
            Rotate();
            canFire = true;
        }
        else
        {
            canFire = false;
            return;
        }
    }

    void Move()
    {
        rb.AddForce(transform.forward * maxSpeed);
    }

    void Rotate()
    {
        Quaternion orientation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dirToTarget), rotationSpeed);
        rb.MoveRotation(orientation);
    }

    private IEnumerator FireCoroutine()
    {
        if (canFire & Player.Health > 0)
        {
            yield return new WaitForSeconds(cooldown);
            Fire();
        }
        yield return null;
        StartCoroutine(FireCoroutine());
    }

    void Fire()
    {
        Quaternion orientation = Quaternion.LookRotation(transform.forward);
        GameObject projectile = Instantiate(projectileProtoype, muzzlePoint.transform.position, orientation);
        projectile.GetComponent<Projectile>().Damage = damage;
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;
    }

    IEnumerator DieCoroutine()
    {
        if (health <= 0)
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
