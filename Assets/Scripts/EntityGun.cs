using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGun : Gun
{
    public bool CanFire { get; set; } 

    void Awake()
    {
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        if (CanFire)
        {
            yield return new WaitForSeconds(cooldown);
            Fire();
        }
        yield return null;
        StartCoroutine(FireCoroutine());
    }
}
