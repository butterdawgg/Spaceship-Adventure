using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float health = 200;
    public GameObject healthBar;
    public float healthBarScale;

    public GameObject body;
    public ParticleSystem ps;
    public float scoreGetAmount;

    public GameObject[] hardpoints;
    public GameObject[] guns;
    public GameObject drop;

    protected void SetGunsFiring(bool fire)
    {
        for (int i = 0; i < hardpoints.Length; i++)
        {
            if (hardpoints[i] != null)
                hardpoints[i].transform.GetChild(0).gameObject.GetComponent<EntityGun>().CanFire = fire;
        }
    }

    protected void DropLoot()
    {
        int random = Random.Range(3, 5);

        for (int i = 0; i < random; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-1f, 1f),
                                      transform.position.y + Random.Range(-1f, 1f),
                                      transform.position.z + Random.Range(-1f, 1f));

            Instantiate(drop, pos, transform.rotation);
        }
    }
}