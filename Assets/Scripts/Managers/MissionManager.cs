using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public Mission activeMission;



    void Awake()
    {
        activeMission = new Mission();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
