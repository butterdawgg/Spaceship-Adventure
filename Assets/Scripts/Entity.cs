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

    protected void SetGunsFiring(bool fire)
    {
        for (int i = 0; i < hardpoints.Length; i++)
        {
            if (hardpoints[i] != null)
                hardpoints[i].transform.GetChild(0).gameObject.GetComponent<EntityGun>().CanFire = fire;
        }
    }
}
