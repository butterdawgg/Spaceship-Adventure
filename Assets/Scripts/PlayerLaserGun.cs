using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserGun : Gun
{
    public int fireButtonIndex;
    public float rayDuration;
    public Transform rayEndPoint;
    public LayerMask layerMask;

    private LineRenderer lr;
    private Collider hitCollider;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.forceRenderingOff = true;

        if (cooldown <= 0 || rayDuration <= 0)
            StartCoroutine(BeamFireCoroutine());
        else
            StartCoroutine(PulseFireCoroutine());
    }

    void LateUpdate()
    {
        Ray ray = new Ray(muzzlePoint.position, muzzlePoint.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            rayEndPoint.localPosition = new Vector3(0f, 0f, hit.distance);
            hitCollider = hit.collider;
        }
        else
            rayEndPoint.localPosition = new Vector3(0f, 0f, 1000f);

        lr.SetPosition(0, muzzlePoint.position);
        lr.SetPosition(1, rayEndPoint.position);
    }

    private IEnumerator PulseFireCoroutine()
    {
        if (Input.GetMouseButton(fireButtonIndex) & Player.Energy > 0 & !UI.IsPaused)
        {
            if(hitCollider != null)
            {
                if (hitCollider.gameObject.TryGetComponent<Entity>(out Entity entity))
                {
                    entity.health -= damage;
                    FindObjectOfType<AudioManager>().PlaySound("EnemyHit");
                }
            }

            lr.forceRenderingOff = false;

            Player.Energy -= energyDraw;

            FindObjectOfType<AudioManager>().PlaySound("PlayerFireLaser");

            yield return new WaitForSeconds(rayDuration);

            lr.forceRenderingOff = true;

            yield return new WaitForSeconds(cooldown);
        }

        yield return null;
        StartCoroutine(PulseFireCoroutine());
    }    

    private IEnumerator BeamFireCoroutine()
    {
        if (Input.GetMouseButton(fireButtonIndex) & Player.Energy > 0 & !UI.IsPaused)
        {
            if (hitCollider != null)
            {
                if (hitCollider.gameObject.TryGetComponent<Entity>(out Entity entity))
                    entity.health -= damage * 0.1f;
            }

            lr.forceRenderingOff = false;
            
            Player.Energy -= energyDraw * 0.1f;

            yield return new WaitForSeconds(0.1f);
        }
        else
            lr.forceRenderingOff = true;

        yield return null;
        StartCoroutine(BeamFireCoroutine());
    }
}
