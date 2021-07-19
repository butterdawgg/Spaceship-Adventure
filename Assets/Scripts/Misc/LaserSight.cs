using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform endPoint;

    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void LateUpdate()
    {
        lr.SetPosition(0, transform.position);

        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, layerMask))
            endPoint.localPosition = new Vector3(0f, 0f, hit.distance);
        else
            endPoint.localPosition = new Vector3(0f, 0f, 1000f);
        
        lr.SetPosition(1, endPoint.position);
    }
}
