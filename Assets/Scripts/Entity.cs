using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float health = 200;
    public GameObject healthBar;
    public float healthBarScale;

    public GameObject muzzlePoint;
    public GameObject projectileProtoype;
    public float cooldown;
    public float projectileSpeed;
    public float damage;

    public GameObject body;
    public ParticleSystem ps;
    public float scoreGetAmount;
}
