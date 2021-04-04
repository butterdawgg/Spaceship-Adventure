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

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DieCoroutine());
        ps.Stop();
    }

    void FixedUpdate()
    { 
        if (health <= 0)
        {
            SetGunFiring(false);
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
            healthBar.transform.localScale = new Vector3(healthBarScale * health, healthBar.transform.localScale.y, 1f);
        }
         
        if (distance > radius)
        {
            SetGunFiring(false);
            return;
        }
        SetGunFiring(true);

        Quaternion orientation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-dirToTarget), smoothness);
        rb.MoveRotation(orientation);
    }

    private void SetGunFiring(bool fire)
    {
        for (int i = 0; i < hardpoints.Length; i++)
            hardpoints[i].transform.GetChild(0).gameObject.GetComponent<EntityGun>().CanFire = fire;
    }

    IEnumerator DieCoroutine()
    {
        if(health <= 0)
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
