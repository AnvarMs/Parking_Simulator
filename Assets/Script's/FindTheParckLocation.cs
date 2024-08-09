using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTheParkLocation : MonoBehaviour
{
    public GameObject Arrow;
    public GameObject ParkingMark;

    // Update is called once per frame
    private void Awake()
    {
        ParkingMark = GameObject.Find("ParkingMarck");
    }
    void Update()
    {
        // Calculate the direction vector from the arrow to the parking mark
        Vector3 direction = (ParkingMark.transform.position - transform.position).normalized;

        // Calculate the rotation that looks in the direction
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Apply the calculated rotation to the arrow
        Arrow.transform.rotation = targetRotation;
    }
}
