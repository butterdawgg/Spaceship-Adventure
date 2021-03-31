using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    //Basic settings:
    public float range;
    public float amount;
    public bool isAllowedScaling;

    //Object that would spawn:
    public GameObject prototype;

    void Start()
    {
        if (isAllowedScaling)
        {
            for (int i = 0; i < amount; i++)
            {
                //Randomizing the position, rotation and scale of the spawning object:
                Vector3 cords = new Vector3(transform.position.x + Random.Range(-range, range), transform.position.y + Random.Range(-range, range), transform.position.z + Random.Range(-range, range));
                Quaternion rot = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
                prototype.transform.localScale = new Vector3(Random.Range(5f, 10f), Random.Range(5f, 10f), Random.Range(5f, 10f));

                //Actual spawning:
                GameObject.Instantiate(prototype, cords, rot);
            }
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                //Randomizing the position and rotation of the spawning object:
                Vector3 cords = new Vector3(transform.position.x + Random.Range(-range, range), transform.position.y + Random.Range(-range, range), transform.position.z + Random.Range(-range, range));
                Quaternion rot = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

                //Actual spawning:
                GameObject.Instantiate(prototype, cords, rot);
            }
        }
    }
}
