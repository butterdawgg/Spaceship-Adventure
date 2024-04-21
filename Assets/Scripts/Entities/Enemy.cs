using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] float maxSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float stopRadius;
    [SerializeField] float shootRadius;
    [SerializeField] float goRadius;
    [SerializeField] ParticleSystem ps1;

    private Rigidbody rb;
    private Vector3 dirToTarget;

    void Awake()
    {
        Health = maxHealth;

        rb = GetComponent<Rigidbody>();
        StartCoroutine(DieCoroutine());
        ps.Stop();

        for (int i = 0; i < hardpoints.Length; i++)
        {
            Instantiate(guns[Random.Range(0, guns.Length)], hardpoints[i].transform);
        }

        healthBar.SetupK(Health);
    }

    void FixedUpdate()
    {
        if (Health <= 0)
        {
            SetGunsFiring(false);
            return;
        }

        dirToTarget = (Player.Position - transform.position).normalized;
        float distance = (Player.Position - transform.position).magnitude;

        healthBar.Do(Health, distance, shootRadius, dirToTarget, transform.up);

        if (Player.Health > 0)
        {
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
        if (Health <= 0)
        {
            ps.Play();
            Player.Score += scoreGetAmount;
            Destroy(body);
            Destroy(ps1.gameObject);
            healthBar.Destroy();
            AudioManager.Instance.PlaySound("Explosion");
            for (int i = 0; i < hardpoints.Length; i++)
                Destroy(hardpoints[i]);
            yield return new WaitForSeconds(0.5f);
            DropLoot();
            Destroy(gameObject);
        }
        yield return null;
        StartCoroutine(DieCoroutine());
    }
}