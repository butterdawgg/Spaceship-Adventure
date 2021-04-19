using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Camera shipCamera;
    public float smoothness;
    public float rotationSmoothness;
    public float angle;

    void Start()
    {
        Vector3 pos = new Vector3(0f, 3f, -4f);
        shipCamera.transform.localPosition = pos;
    }

    void Update()
    {
        if(Player.Health > 0)
        {
            Quaternion rot0 = Quaternion.Euler(15f, 0f, 0f);
            Quaternion rot1 = Quaternion.Euler(15f, 0f, -angle);
            Quaternion rot2 = Quaternion.Euler(15f, 0f, angle);

            Vector3 pos0 = new Vector3(0f, 3f, -10f);
            Vector3 pos1 = new Vector3(0f, 2f, -10f);
            Vector3 pos2 = new Vector3(0f, 4f, -10f);
            Vector3 pos3 = new Vector3(-1f, 3f, -10f);
            Vector3 pos4 = new Vector3(1f, 3f, -10f);
            Vector3 currentPos = shipCamera.transform.localPosition;



            if (Input.GetKey(KeyCode.W))
                shipCamera.fieldOfView = Mathf.Lerp(shipCamera.fieldOfView, 110f, smoothness * Time.deltaTime);

            else if (Input.GetKey(KeyCode.S))
                shipCamera.fieldOfView = Mathf.Lerp(shipCamera.fieldOfView, 70f, smoothness * Time.deltaTime);

            else
                shipCamera.fieldOfView = Mathf.Lerp(shipCamera.fieldOfView, 90f, smoothness * Time.deltaTime);



            if (Input.GetKey(KeyCode.A))
                shipCamera.transform.localRotation = Quaternion.Slerp(shipCamera.transform.localRotation, rot1, rotationSmoothness * Time.deltaTime);

            else if (Input.GetKey(KeyCode.D))
                shipCamera.transform.localRotation = Quaternion.Slerp(shipCamera.transform.localRotation, rot2, rotationSmoothness * Time.deltaTime);

            else
                shipCamera.transform.localRotation = Quaternion.Slerp(shipCamera.transform.localRotation, rot0, rotationSmoothness * 1.5f * Time.deltaTime);



            if (Input.GetKey(KeyCode.Space))
                currentPos.y = Mathf.Lerp(currentPos.y, pos1.y, smoothness * Time.deltaTime);

            else if (Input.GetKey(KeyCode.LeftShift))
                currentPos.y = Mathf.Lerp(currentPos.y, pos2.y, smoothness * Time.deltaTime);

            else if (Input.GetKey(KeyCode.Q))
                currentPos.x = Mathf.Lerp(currentPos.x, pos4.x, smoothness * Time.deltaTime);

            else if (Input.GetKey(KeyCode.E))
                currentPos.x = Mathf.Lerp(currentPos.x, pos3.x, smoothness * Time.deltaTime);

            else
            {
                currentPos.x = Mathf.Lerp(currentPos.x, pos0.x, smoothness * Time.deltaTime);
                currentPos.y = Mathf.Lerp(currentPos.y, pos0.y, smoothness * Time.deltaTime);
            }

            shipCamera.transform.localPosition = currentPos;
        }
    }
}
