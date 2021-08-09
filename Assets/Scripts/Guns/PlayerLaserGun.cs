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
        muzzlePoint.localPosition = Vector3.zero;
        muzzlePoint.localEulerAngles = Vector3.zero;

        Ray ray = new Ray(Player.playerCamera.transform.position, Player.playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            Ray ray1 = new Ray(muzzlePoint.position, (ray.GetPoint(hit.distance) - muzzlePoint.position).normalized);
            if (Physics.Raycast(ray1, out RaycastHit hit1, layerMask))
            {
                //rayEndPoint.position = ray1.GetPoint(hit1.distance);
                lr.SetPosition(1, ray1.GetPoint(hit1.distance));
                hitCollider = hit1.collider;
            }
            else
            {
                //rayEndPoint.position = ray.GetPoint(hit.distance);
                lr.SetPosition(1, ray.GetPoint(hit.distance));
            }
        }
        else
        {
            //rayEndPoint.position = ray.GetPoint(100f);
            lr.SetPosition(1, ray.GetPoint(100f));
        }

        lr.SetPosition(0, muzzlePoint.position);

        if (cooldown <= 0 || rayDuration <= 0)
            lr.SetWidth((Mathf.Sin(Time.time * 10f) * 0.03f) + 0.08f, (Mathf.Sin(Time.time * 10f) * 0.03f) + 0.08f);
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
                    AudioManager.Instance.PlaySound("EnemyHit");
                }
            }

            lr.forceRenderingOff = false;

            Player.Energy -= energyDraw;

            AudioManager.Instance.PlaySound("PlayerFireLaser");

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

            AudioManager.Instance.StopSound("PlayerFireBeamLaser");
            AudioManager.Instance.PlaySound("PlayerFireBeamLaser");

            yield return new WaitForSeconds(0.1f);
        }
        else
            lr.forceRenderingOff = true;

        yield return null;
        StartCoroutine(BeamFireCoroutine());
    }
}
