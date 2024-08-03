using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
public class AICar : MonoBehaviour
{
    
    public List<Transform> waypoints;
    public Transform CorrentTransform;
    public Graph graph;
    Queue<Transform> queue;
    public float reachDistence = 3f;
    public float reverseDistence = 5;
    
    public Transform position;
    public LayerMask layerMask;
    public GameObject FL_Wheel, FR_Wheel, RL_Wheel, RR_Wheel;
    public WheelCollider FL_Wheel_Collider, FR_Wheel_Collider, RL_Wheel_Collider, RR_Wheel_Collider;

    public Rigidbody RB;
    public Transform centerOfMass;
    
    public float MotorPower;
    public float SteeringPower;
    public float BreakePower;

     float InputMotor;
     float InputSteering;
     float InputBreake;
     float distence;

    public bool Inbreak = false;
    // float Timer = 0;
    [Header("Senserce")]
    public float rayZpose = 0.5f;
    public float raylength = 3f;
    public float rayXpose = 1f;




    void Start()
    {
        InputSteering = 0;
        RB.centerOfMass = centerOfMass.localPosition;
        graph = new Graph();
        queue = new Queue<Transform>();
        queue.Enqueue(CorrentTransform);

        foreach (Transform t in waypoints)
        {
            graph.AddVertex(t);
        }

        graph.AddEdge(waypoints[0], waypoints[1]);

        graph.AddEdge(waypoints[1], waypoints[2]);
        graph.AddEdge(waypoints[1], waypoints[3]);
     
        graph.AddEdge(waypoints[2], waypoints[4]);
        
        graph.AddEdge(waypoints[4], waypoints[5]);
        graph.AddEdge(waypoints[4], waypoints[3]);
      
        graph.AddEdge(waypoints[5], waypoints[6]);
       
        graph.AddEdge(waypoints[6], waypoints[7]);
        graph.AddEdge(waypoints[6], waypoints[3]);
        
        graph.AddEdge(waypoints[7], waypoints[8]);

        graph.AddEdge(waypoints[8], waypoints[0]);
        graph.AddEdge(waypoints[8], waypoints[3]);




    }

    void FixedUpdate()
    {
        
        AplayMotor();
        AplaySteering(InputSteering);
        MeshUpdate();
        AplayBrake();
        AiSencers();
    }

    void AiSencers()
    {
        RaycastHit hit;

        Vector3 starPos= transform.position;

        //friend sensore
        starPos.z += rayZpose;
        if(Physics.Raycast(starPos,transform.forward ,out hit,raylength))
        {
            
        }
        Debug.DrawLine(starPos,hit.point);

        //friend right sensor;
        starPos.x += rayXpose;
        if (Physics.Raycast(starPos, transform.forward, out hit, raylength))
        {

        }
        Debug.DrawLine(starPos, hit.point);

        // fiend left sensor;
        starPos.x -= 2*rayXpose;
        if (Physics.Raycast(starPos, transform.forward, out hit, raylength))
        {

        }
        Debug.DrawLine(starPos, hit.point);


    }


    void GetInput()
    {
        

         distence = Vector3.Distance(transform.position,CorrentTransform.position);
       
        Vector3 DirPosition = (CorrentTransform.position - transform.position).normalized;

        

        if (distence > reachDistence )
        {
            InputBreake = 0;
           

            float dot = Vector3.Dot(transform.forward, DirPosition);
            if (dot > 0)
            {
                InputMotor = 1;
            }
            else
            {
                if (distence < reverseDistence)
                {
                    InputMotor = -1;
                }
                else
                {
                    InputMotor = 1;
                }
            }

            float angle = Vector3.SignedAngle(transform.forward, DirPosition, Vector3.up);

            if (angle > 10)
            {
                InputSteering = 1;
            }
            else if (angle < -10)
            {
                InputSteering = -1;
            }
            else
            {
                InputSteering = 0;
            }

        }
        else
        {
            //the Car is reach the Corrent posishion
            
            
            if(distence < reverseDistence)
            {

                SelectTheNode();
            }
            
                
            
                InputMotor = 0;
                InputSteering = 0;
                InputBreake = 1;
            
            
        }
                     
    }

    void SelectTheNode()
    {

        
        List<Transform> ls = graph.GetNeighbors(CorrentTransform);
        
        // Get a random index
         int randomIndex = Random.Range(0, ls.Count);
        while (queue.Contains(ls[randomIndex]))
        {
            randomIndex = Random.Range(0, ls.Count);
        }
        // Retrieve the random value from the list
        queue.Enqueue(CorrentTransform);
        CorrentTransform = ls[randomIndex];

        if(queue.Count > 2)
        {
            queue.Dequeue();
        }


    }




    void AplayMotor()
    {
        RL_Wheel_Collider.motorTorque = MotorPower * InputMotor;
        RR_Wheel_Collider.motorTorque = MotorPower * InputMotor;
    }

    void AplaySteering(float steerAngle)
    {
        FL_Wheel_Collider.steerAngle = Mathf.Lerp(FL_Wheel_Collider.steerAngle, steerAngle * SteeringPower, Time.deltaTime * 5);
        FR_Wheel_Collider.steerAngle = Mathf.Lerp(FR_Wheel_Collider.steerAngle, steerAngle * SteeringPower, Time.deltaTime * 5);
    }

    void MeshUpdate()
    {
        UpdateMesh(FL_Wheel_Collider, FL_Wheel);
        UpdateMesh(FR_Wheel_Collider, FR_Wheel);
        UpdateMesh(RL_Wheel_Collider, RL_Wheel);
        UpdateMesh(RR_Wheel_Collider, RR_Wheel);

    }

    private void UpdateMesh(WheelCollider coldr, GameObject mesh)
    {
        Quaternion rotate;
        Vector3 pos;
        coldr.GetWorldPose(out pos, out rotate);
        mesh.transform.position = pos;

        mesh.transform.rotation = rotate;
    }

    private void AplayBrake()
    {
        FL_Wheel_Collider.brakeTorque = InputBreake * BreakePower * .7f;
        FR_Wheel_Collider.brakeTorque = InputBreake * BreakePower * .7f;

        RL_Wheel_Collider.brakeTorque = InputBreake * BreakePower * .3f;
        RR_Wheel_Collider.brakeTorque = InputBreake * BreakePower * .3f;
    }

    

}
