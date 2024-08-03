
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CarControler : MonoBehaviour
{
    public GameObject FL_Wheel, FR_Wheel, RL_Wheel, RR_Wheel;
    public WheelCollider FL_Wheel_Collider, FR_Wheel_Collider, RL_Wheel_Collider, RR_Wheel_Collider;

    public Rigidbody RB;
    public Transform centerOfMass;
    public SteeringWheelUIController SteeringWheel;


    public float MotorPower;
    public float SteeringPower;
    public float BreakePower;

    public float InputMotor;
    float InputSteering;
    public float InputBreake;
    

    public bool Inbreak= false;
    float InAccil=0,GearValue=0;
    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        RB.centerOfMass = centerOfMass.localPosition;
        SteeringWheel = GameObject.Find("Canvas").GetComponentInChildren<SteeringWheelUIController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        AplayMotor();
        AplaySteering();
        MeshUpdate();
        AplayBrake();

    }

    public void Accilerater(float value)
    {
        InAccil=value;
    }
    void GetInput()
    {
        GearValue = slider.value;


        InputMotor =Mathf.Lerp(0, GearValue==0? InAccil:GearValue==2?-InAccil:0,0.5f);
       InputSteering= SteeringWheel.GetSteeringInput();



        float MotorDir = Vector3.Dot(transform.forward, RB.velocity);
        if(MotorDir < -.5f&& InputMotor>.5f)
        {
            InputBreake = Mathf.Abs(InputMotor);
        }else if(MotorDir >.5f && InputMotor < 0)
        {
            InputBreake= Mathf.Abs(InputMotor);
        }
        else
        {
            InputBreake = Inbreak ? 1 : 0;
        }
       


    }
    void AplayMotor()
    {
        RL_Wheel_Collider.motorTorque = MotorPower * InputMotor;
        RR_Wheel_Collider.motorTorque = MotorPower * InputMotor;
    }

    void AplaySteering()
    {
        FL_Wheel_Collider.steerAngle = SteeringPower * InputSteering;
        FR_Wheel_Collider.steerAngle = SteeringPower * InputSteering;
    }

    void MeshUpdate()
    {
        UpdateMesh(FL_Wheel_Collider, FL_Wheel);
        UpdateMesh(FR_Wheel_Collider, FR_Wheel);
        UpdateMesh(RL_Wheel_Collider, RL_Wheel);
        UpdateMesh(RR_Wheel_Collider, RR_Wheel);

    }

    private void UpdateMesh(WheelCollider coldr,GameObject mesh)
    {
        Quaternion rotate;
        Vector3 pos;
        coldr.GetWorldPose(out pos, out rotate);
        mesh.transform.position = pos;

        mesh.transform.rotation=rotate;
    }

    private void AplayBrake()
    {
        FL_Wheel_Collider.brakeTorque = InputBreake * BreakePower * .7f;
        FR_Wheel_Collider.brakeTorque = InputBreake * BreakePower * .7f;

        RL_Wheel_Collider.brakeTorque = InputBreake * BreakePower * .3f;
        RR_Wheel_Collider.brakeTorque = InputBreake * BreakePower * .3f;
    }
    public void BreakLeverPress(bool value)
    {
        Inbreak = value;
    }
    

}
