using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //Public fields:
    public float maxSpeed;
    public float rotationSpeed;
    public float stopRadius;
    public float shootRadius;
    public float goRadius;


    //Private fields:
    private Rigidbody rb;
    Vector3 dirToTarget;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DieCoroutine());
        ps.Stop();

        for (int i = 0; i < hardpoints.Length; i++)
        {
            Instantiate(guns[Random.Range(0, guns.Length)], hardpoints[i].transform);
        }
    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            SetGunsFiring(false);
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
            healthBar.transform.localScale = new Vector3(healthBarScale * health, healthBar.transform.localScale.y, 1f);
        }

        if (distance < goRadius & distance > shootRadius & distance > stopRadius)
        {
            Rotate();
            Move();
            SetGunsFiring(false);
        }
        else if (distance < goRadius & distance < shootRadius & distance > stopRadius)
        {
            Rotate();
            Move();
            SetGunsFiring(true);
        }
        else if (distance < goRadius & distance < shootRadius & distance < stopRadius)
        {
            Rotate();
            SetGunsFiring(true);
        }
        else
        {
            SetGunsFiring(false);
            return;
        }
    }

    private void Move()
    {
        rb.AddForce(transform.forward * maxSpeed);
    }

    private void Rotate()
    {
        Quaternion orientation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dirToTarget), rotationSpeed);
        rb.MoveRotation(orientation);
    }

    IEnumerator DieCoroutine()
    {
        if (health <= 0)
        {
            ps.Play();
            Player.Score += scoreGetAmount;
            Destroy(healthBar);
            Destroy(body);
            for (int i = 0; i < hardpoints.Length; i++)
                Destroy(hardpoints[i]);
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
        yield return null;
        StartCoroutine(DieCoroutine());
    }
}