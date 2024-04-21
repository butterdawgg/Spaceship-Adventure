using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float amount;

    [SerializeField] GameObject prototype;

    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 cords = new Vector3(transform.position.x + Random.Range(-range, range), transform.position.y + Random.Range(-range, range), transform.position.z + Random.Range(-range, range));
            Quaternion rot = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

            Instantiate(prototype, cords, rot);
        }
    }
}
