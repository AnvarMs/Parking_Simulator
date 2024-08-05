using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHolder : MonoBehaviour
{
    public GameObject FL_Wheel, FR_Wheel, RL_Wheel, RR_Wheel;
    
    public WheelCollider FL_Collider, FR_Collider, RL_Collider, RR_Collider;
    public float ActionTime;

    private void Start()
    {

        StartCoroutine(StartMotion());
    }
    // Update is called once per frame
    void Update()
    {
        UpdateMesh(FL_Collider, FL_Wheel);
        UpdateMesh(FR_Collider, FR_Wheel);
        UpdateMesh(RL_Collider, RL_Wheel);
        UpdateMesh(RR_Collider, RR_Wheel);

        
    }

    IEnumerator StartMotion()
    {
        FL_Collider.motorTorque = 100;
        FR_Collider.motorTorque = 100;
        RL_Collider.motorTorque = 100;
        RR_Collider.motorTorque = 100;

        yield return new WaitForSeconds(ActionTime);

        FL_Collider.brakeTorque = 1000;
        FR_Collider.brakeTorque = 1000;
        RL_Collider.brakeTorque = 1000;
        RR_Collider.brakeTorque = 1000;
    }

    private void UpdateMesh(WheelCollider coldr, GameObject mesh)
    {
        Quaternion rotate;
        Vector3 pos;
        coldr.GetWorldPose(out pos, out rotate);
        mesh.transform.position = pos;

        mesh.transform.rotation = rotate;
    }
}
