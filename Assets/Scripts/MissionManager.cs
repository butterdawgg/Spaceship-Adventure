using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public Mission activeMission;



    // Start is called before the first frame update
    void Start()
    {
        activeMission = new Mission();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
