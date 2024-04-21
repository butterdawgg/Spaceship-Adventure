using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Entity
{
    [SerializeField] float radius;
    [SerializeField] float smoothness;
    [SerializeField] GameObject towerPivot;
    [SerializeField] GameObject towerBase;
    [SerializeField] Vector3 defaultPosition;

    void Awake()
    {
        Health = maxHealth;

        StartCoroutine(DieCoroutine());
        ps.Stop();

        transform.localPosition = defaultPosition;

        for (int i = 0; i < hardpoints.Length; i++)
        {
            GameObject gun = Instantiate(guns[Random.Range(0, guns.Length)], hardpoints[i].transform);
            Physics.IgnoreCollision(towerPivot.transform.GetChild(0).GetComponent<Collider>(), 
                                    gun.transform.GetChild(0).GetComponent<Collider>());
        }

        Physics.IgnoreCollision(transform.GetChild(0).GetComponent<Collider>(), 
                                towerPivot.transform.GetChild(0).GetComponent<Collider>());

        healthBar.SetupK(Health);
    }

    void FixedUpdate()
    {
        transform.localPosition = defaultPosition;
        towerPivot.transform.localEulerAngles = new Vector3(towerPivot.transform.localEulerAngles.x, 
                                                            transform.localEulerAngles.y, 
                                                            towerPivot.transform.localEulerAngles.z);

        if (Health <= 0)
        {
            SetGunsFiring(false);
            return;
        }

        float distance = (Player.Position - transform.position).magnitude;
        Vector3 dirToTarget = (Player.Position - transform.position).normalized;

        healthBar.Do(Health, distance, radius, dirToTarget, transform.up);

        if (Player.Health > 0)
        {
            if (distance > radius)
                SetGunsFiring(false);
            else
            {
                SetGunsFiring(true);

                transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                                              Quaternion.LookRotation(dirToTarget, towerBase.transform.up),
                                                              smoothness);

                Vector3 rot = transform.localEulerAngles;
                if (rot.x > 30f & rot.x < 50f)
                    transform.localRotation = Quaternion.Euler(30f, rot.y, rot.z);
            }
        }
        else
        {
            SetGunsFiring(false);
            return;
        }
    }

    IEnumerator DieCoroutine()
    {
        if(Health <= 0)
        {
            ps.Play();
            Player.Score += scoreGetAmount;
            Destroy(body);
            healthBar.Destroy();
            for (int i = 0; i < hardpoints.Length; i++)
                Destroy(hardpoints[i]);
            AudioManager.Instance.PlaySound("Explosion");
            yield return new WaitForSeconds(0.5f);
            DropLoot();
            Destroy(gameObject);
        }
        yield return null;
        StartCoroutine(DieCoroutine());
    }
}
