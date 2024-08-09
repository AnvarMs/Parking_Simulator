using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkingCheck : MonoBehaviour
{
    public Collider carCollider;
    public Collider parkCollider;
    public Slider gear;
    public Material material;
    [Header("Game states")]
    public GameObject FinishPanel, LoosPanel, Controler;

    [Header("Game Ui Animations")]
    public Animator animator;
    private bool IsParked;
    float Ctime = 0;
    // Start is called before the first frame update
    void Start()
    {

        FinishPanel = GameObject.Find("Finish_Panel");
        LoosPanel = GameObject.Find("Loos_Panel");
        Controler = GameObject.Find("Controle_Right");
        animator = FinishPanel.GetComponent<Animator>();
        GameObject pl = GameObject.FindGameObjectWithTag("Player");
        carCollider = pl.GetComponent<Collider>();

        

        FinishPanel.SetActive(false);
        LoosPanel.SetActive(false);
        Controler.SetActive(true);
       
        
    }

    // Update is called once per frame
    void Update()
    {
        IsParked = CheckIsParked(carCollider, parkCollider);


        if (IsParked)
        {
            material.color = Color.green;
            if (gear.value == 3)
            {
               Controler.SetActive(false);
                FinishPanel.SetActive(true);
                animator.SetBool("finish", true);
            }
        }
        else
        {
            BlingTheParkingMark();
        }
        
          
        
    }

    public bool CheckIsParked(Collider carCollider, Collider parkCollider)
    {
        Bounds carBounds = carCollider.bounds;
        Bounds parkBounds = parkCollider.bounds;

        // Check if all corners of the car bounds are within the park bounds
        Vector3[] corners = new Vector3[8];

        corners[0] = carBounds.min; // Bottom-left-front
        corners[1] = new Vector3(carBounds.max.x, carBounds.min.y, carBounds.min.z); // Bottom-right-front
        corners[2] = new Vector3(carBounds.min.x, carBounds.max.y, carBounds.min.z); // Top-left-front
        corners[3] = new Vector3(carBounds.max.x, carBounds.max.y, carBounds.min.z); // Top-right-front
        corners[4] = new Vector3(carBounds.min.x, carBounds.min.y, carBounds.max.z); // Bottom-left-back
        corners[5] = new Vector3(carBounds.max.x, carBounds.min.y, carBounds.max.z); // Bottom-right-back
        corners[6] = new Vector3(carBounds.min.x, carBounds.max.y, carBounds.max.z); // Top-left-back
        corners[7] = carBounds.max; // Top-right-back

        foreach (Vector3 corner in corners)
        {
            if (!parkBounds.Contains(corner))
            {
                return false;
            }
        }

        return true;
    }


    void BlingTheParkingMark()
    {
        

        Ctime = Ctime+ Time.deltaTime;
       
        if (Ctime > 1)
        {
                material.color = material.color == Color.yellow ? Color.white : Color.yellow;
                Ctime = 0;
        }     
       
    }
}
