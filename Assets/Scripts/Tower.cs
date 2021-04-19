using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Entity
{
    //Public variables:
    public float radius;
    public float smoothness;
    public GameObject towerPivot;
    public GameObject towerBase;
    public Vector3 defaultPosition;
    

    void Awake()
    {
        StartCoroutine(DieCoroutine());
        ps.Stop();

        transform.localPosition = defaultPosition;

        for (int i = 0; i < hardpoints.Length; i++)
        {
            GameObject gun = Instantiate(guns[Random.Range(0, guns.Length)], hardpoints[i].transform);
            Physics.IgnoreCollision(towerPivot.transform.GetChild(0).GetComponent<Collider>(), gun.transform.GetChild(0).GetComponent<Collider>());
        }

        Physics.IgnoreCollision(transform.GetChild(0).GetComponent<Collider>(), towerPivot.transform.GetChild(0).GetComponent<Collider>());
    }

    void FixedUpdate()
    {
        transform.localPosition = defaultPosition;
        towerPivot.transform.localEulerAngles = new Vector3(towerPivot.transform.localEulerAngles.x, transform.localEulerAngles.y, towerPivot.transform.localEulerAngles.z);

        if (health <= 0)
        {
            SetGunFiring(false);
            return;
        }
        
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

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dirToTarget, towerBase.transform.up), smoothness);
    }

    private void SetGunFiring(bool fire)
    {
        for (int i = 0; i < hardpoints.Length; i++)
        {
            if (hardpoints[i] != null)
                hardpoints[i].transform.GetChild(0).gameObject.GetComponent<EntityGun>().CanFire = fire;
        }
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
