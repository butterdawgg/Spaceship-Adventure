using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDetector : MonoBehaviour
{

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.yellow);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
 
            if(hit.collider.TryGetComponent(out Selectable selectable))
            {
                selectable.Select();
            }
            else
            {

            }

        }

    }
}
