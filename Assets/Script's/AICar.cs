using UnityEngine;

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class AICar : MonoBehaviour
{
    
    public List<Transform> waypoints;
    public Transform CorrentTransform;
    private Transform spowrnTransform;
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

    public float maxMotorPower = 1500f; // Max power applied to the motor
    public float acceleration = 10f;  // Acceleration factor
    public float deceleration = 8f;  // Deceleration factor
    public float maxSpeed = 50f; // Maximum speed in units like km/h or m/s
    public float brakingForce = 300f; // Braking force applied to the wheels
    public float currentSpeed; // Current speed of the car
    public bool isBraking; // Flag to detect if braking


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

    // Edge Data (to be loaded dynamically or configured via Unity)
    [System.Serializable]
    public class EdgeData
    {
        public int startNode;
        public int endNode;
    }
    public List<EdgeData> edges;

    public Transform ReSpawnPos;
    void Start()
    {
        spowrnTransform = CorrentTransform;
        InputSteering = 0;
        RB.centerOfMass = centerOfMass.localPosition;
        graph = new Graph();
        queue = new Queue<Transform>();
        queue.Enqueue(CorrentTransform);

        foreach (Transform t in waypoints)
        {
            graph.AddVertex(t);
        }
        foreach (EdgeData edge in edges)
        {

            graph.AddEdge(waypoints[edge.startNode], waypoints[edge.endNode]);

        }
        flaspwan = false;
    }

   

    void FixedUpdate()
    {
        
        AplayMotor();
        AplaySteering();
        MeshUpdate();
        AplayBrake();
        AiSencers();
    }

    bool flaspwan;

    void AiSencers()
    {
        RaycastHit hit;
        bool isObstacleDetected = false;

        // Calculate the start positions for the sensors based on the vehicle's orientation
        Vector3 carCenter = transform.position;
        float carHeightOffset = GetComponent<Collider>().bounds.size.y / 2;

        Vector3 forwardSensorPos = carCenter + transform.forward * rayZpose + Vector3.up * carHeightOffset;
        Vector3 forwardRightSensorPos = forwardSensorPos + transform.right * rayXpose;
        Vector3 forwardLeftSensorPos = forwardSensorPos - transform.right * rayXpose;

        // 30-degree angles for additional sensors
        Vector3 right30DegDirection = Quaternion.AngleAxis(30, Vector3.up) * transform.forward;
        Vector3 left30DegDirection = Quaternion.AngleAxis(-30, Vector3.up) * transform.forward;

        // Forward sensor
        if (Physics.Raycast(forwardSensorPos, transform.forward, out hit, raylength, layerMask) )
        {
            isObstacleDetected = true;
            Debug.DrawLine(forwardSensorPos, hit.point, Color.red);
            InputMotor = 0;
            InputBreake = 1;

            if (!flaspwan)
            {
                RespownetheCar();
                flaspwan = true;
               
            }
        }
        else
        {
            Debug.DrawLine(forwardSensorPos, forwardSensorPos + transform.forward * raylength, Color.green);
            InputMotor = 1;
            InputBreake = 0;
        }

        // Forward right sensor
        if (Physics.Raycast(forwardRightSensorPos, transform.forward, out hit, raylength, layerMask))
        {
            isObstacleDetected = true;
            Debug.DrawLine(forwardRightSensorPos, hit.point, Color.red);
            InputSteering = -1;
        }
        else
        {
            Debug.DrawLine(forwardRightSensorPos, forwardRightSensorPos + transform.forward * raylength, Color.green);
        }

        // Forward left sensor
        if (Physics.Raycast(forwardLeftSensorPos, transform.forward, out hit, raylength, layerMask))
        {
            isObstacleDetected = true;
            Debug.DrawLine(forwardLeftSensorPos, hit.point, Color.red);
            InputSteering = 1;
        }
        else
        {
            Debug.DrawLine(forwardLeftSensorPos, forwardLeftSensorPos + transform.forward * raylength, Color.green);
        }

        // Right 30-degree sensor
        if (Physics.Raycast(forwardRightSensorPos, right30DegDirection, out hit, raylength, layerMask))
        {
            isObstacleDetected = true;
            Debug.DrawLine(forwardRightSensorPos, hit.point, Color.red);
            InputSteering = -1;
        }
        else
        {
            Debug.DrawLine(forwardRightSensorPos, forwardSensorPos + right30DegDirection * raylength, Color.green);
        }

        // Left 30-degree sensor
        if (Physics.Raycast(forwardLeftSensorPos, left30DegDirection, out hit, raylength, layerMask))
        {
            isObstacleDetected = true;
            Debug.DrawLine(forwardLeftSensorPos, hit.point, Color.red);
            InputSteering = 1;
        }
        else
        {
            Debug.DrawLine(forwardLeftSensorPos, forwardSensorPos + left30DegDirection * raylength, Color.green);
        }

        // Downward sensor
        Vector3 downSensorPos = carCenter - Vector3.up * carHeightOffset;
        if (Physics.Raycast(downSensorPos, Vector3.down, out hit, raylength, layerMask))
        {
            isObstacleDetected = true;
            Debug.DrawLine(downSensorPos, hit.point, Color.red);
           // InputMotor = 0;
           // InputBreake = 1;
        }
        else
        {
            Debug.DrawLine(downSensorPos, downSensorPos + Vector3.down * raylength, Color.green);
        }

        // Adjust car behavior if an obstacle is detected
        if (!isObstacleDetected)
        {
            GetInput();
        }
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
        // Get the list of neighbors
        List<Transform> neighbors = graph.GetNeighbors(CorrentTransform);

        // Check if there are neighbors to select from
        if (neighbors == null || neighbors.Count == 0)
        {
            Debug.LogError("No neighbors found for the current node. Ensure that the graph has been correctly set up.");
            return;
        }

        // Get a random index within the bounds of the neighbors list
        int randomIndex = Random.Range(0, neighbors.Count);

        // Ensure the selected node hasn't been used recently
        while (queue.Contains(neighbors[randomIndex]))
        {
            randomIndex = Random.Range(0, neighbors.Count);
        }

        // Enqueue the current node and move to the selected neighbor
        queue.Enqueue(CorrentTransform);
        CorrentTransform = neighbors[randomIndex];

        // Limit the queue size to 2
        if (queue.Count > 2)
        {
            queue.Dequeue();
        }

       
    }





   

    void AplayMotor()
    {
        
        // Calculate current speed in km/h or m/s based on your wheel's angular velocity
        currentSpeed = RL_Wheel_Collider.rpm * (RL_Wheel_Collider.radius * 2 * Mathf.PI * 60 / 1000); // Example calculation in km/h

        // If the car is braking
        if (isBraking)
        {
            // Apply negative torque to simulate braking
            RL_Wheel_Collider.motorTorque = -brakingForce;
            RR_Wheel_Collider.motorTorque = -brakingForce;
        }
        else if (InputMotor > 0)  // If accelerating
        {
            // Accelerate the car, but limit torque if speed is reaching the max speed
            if (currentSpeed < maxSpeed)
            {
                RL_Wheel_Collider.motorTorque = Mathf.Clamp(MotorPower * InputMotor, 0, maxMotorPower);
                RR_Wheel_Collider.motorTorque = Mathf.Clamp(MotorPower * InputMotor, 0, maxMotorPower);
            }
            else
            {
                // If max speed is reached, apply no additional torque
                RL_Wheel_Collider.motorTorque = 0;
                RR_Wheel_Collider.motorTorque = 0;
            }
        }
        else if (InputMotor == 0)  // Decelerate naturally when no input
        {
            RL_Wheel_Collider.motorTorque = 0;  // No motor torque
            RR_Wheel_Collider.motorTorque = 0;

            // You can also apply additional logic for automatic deceleration or drag
            currentSpeed -= deceleration * Time.deltaTime;
        }

        // Debugging speed output
      
           
        
    }


    void AplaySteering()
    {
        FL_Wheel_Collider.steerAngle = Mathf.Lerp(FL_Wheel_Collider.steerAngle, InputSteering * SteeringPower, Time.deltaTime * 5);
        FR_Wheel_Collider.steerAngle = Mathf.Lerp(FR_Wheel_Collider.steerAngle, InputSteering * SteeringPower, Time.deltaTime * 5);
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
     IEnumerator CheckDealy()
    {
        yield return new WaitForSeconds(5);
        
        transform.position = ReSpawnPos.position;
        transform.rotation = ReSpawnPos.rotation;
        CorrentTransform = spowrnTransform;
        queue.Clear();
        flaspwan = false;
    }
    private void RespownetheCar()
    {
        StartCoroutine(CheckDealy());
        
    } 

}
