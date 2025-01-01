using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgChenger : MonoBehaviour
{
    public GameObject BridgBrocken, BridgSafe;
    public GameObject BridgBrokeParticle;
    public AudioSource BridgBrockAudio;
    // Start is called before the first frame update
    void Start()
    {
        BridgSafe.SetActive(true);
        BridgBrocken.SetActive(false);
    }


    public void ActiveteBridg()
    {
        BridgSafe.SetActive(false);
        BridgBrocken.SetActive(true);
        BridgBrokeParticle.SetActive(true);
        BridgBrockAudio.Play();
    }
   
}
