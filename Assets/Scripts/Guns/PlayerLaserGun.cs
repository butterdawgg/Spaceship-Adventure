using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserGun : Gun
{
    [SerializeField] float rayDuration;
    [SerializeField] Transform rayEndPoint;

    [SerializeField] LayerMask layerMask;

    private KeyCode fireKey;

    private LineRenderer lr;
    private Collider hitCollider;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.forceRenderingOff = true;

        fireKey = SerializeManager.Instance.GetControls(ControlsType.Shoot);

        if (cooldown <= 0 || rayDuration <= 0)
            StartCoroutine(BeamFireCoroutine());
        else
            StartCoroutine(PulseFireCoroutine());
    }

    void LateUpdate()
    {
        rayEndPoint.localPosition = Vector3.zero;
        rayEndPoint.localEulerAngles = Vector3.zero;

        muzzlePoint.localPosition = Vector3.zero;
        muzzlePoint.localEulerAngles = Vector3.zero;

        Ray ray = new Ray(Player.playerCamera.transform.position, Player.playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            muzzlePoint.LookAt(hit.point);
            Ray ray1 = new Ray(muzzlePoint.position, muzzlePoint.forward);
            if(Physics.Raycast(ray1, out RaycastHit hit1, layerMask))
            {
                rayEndPoint.localPosition = new Vector3(0f, 0f, hit1.distance);
                hitCollider = hit1.collider;
            }
        }
        else
        {
            rayEndPoint.position = ray.GetPoint(100f);
            hitCollider = null;
        }

        lr.SetPosition(0, muzzlePoint.position);
        lr.SetPosition(1, rayEndPoint.position);
    }

    private IEnumerator PulseFireCoroutine()
    {
        if (Input.GetKey(fireKey) & Player.Energy > 0 & !UI.IsPaused)
        {
            if(hitCollider != null)
            {
                if (hitCollider.gameObject.TryGetComponent<Entity>(out Entity entity))
                {
                    entity.Health -= damage;
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
        if (Input.GetKey(fireKey) & Player.Energy > 0 & !UI.IsPaused)
        {
            if (hitCollider != null)
            {
                if (hitCollider.gameObject.TryGetComponent<Entity>(out Entity entity))
                    entity.Health -= damage * 0.1f;
            }

            lr.forceRenderingOff = false;
            
            Player.Energy -= energyDraw * 0.1f;

            FindObjectOfType<AudioManager>().StopSound("PlayerFireBeamLaser");
            FindObjectOfType<AudioManager>().PlaySound("PlayerFireBeamLaser");

            yield return new WaitForSeconds(0.1f);
        }
        else
            lr.forceRenderingOff = true;

        yield return null;
        StartCoroutine(BeamFireCoroutine());
    }
}
