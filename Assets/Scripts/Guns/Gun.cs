using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Main,
    Side
}

public class Gun : MonoBehaviour
{
    [SerializeField] protected Transform muzzlePoint;
    [SerializeField] protected float damage;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float energyDraw;
}
