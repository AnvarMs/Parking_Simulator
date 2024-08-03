using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarSoundController : MonoBehaviour
{
    public Text text;
    public AudioSource engineIdleSource;
    public AudioSource engineAccelerationSource;
    public AudioSource engineDecelerationSource;
    public AudioSource brakeSource;


    public Rigidbody carRG;
    private CarControler carControler;
    private float speed;

    private float maxPitch = 2.0f;
    private float minPitch = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        carRG = GetComponent<Rigidbody>();
        carControler = GetComponent<CarControler>();

        engineIdleSource.Play();
        engineAccelerationSource.Play();
        engineDecelerationSource.Play();
        brakeSource.Play();

        engineIdleSource.loop = true;
        engineAccelerationSource.loop = true;
        engineDecelerationSource.loop = true;
        brakeSource.loop = true;

        engineIdleSource.volume = 1.0f;
        engineAccelerationSource.volume = 0.0f;
        engineDecelerationSource.volume = 0.0f;
        brakeSource.volume = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        AudioControler();
    }


    private void AudioControler()
    {
        speed = carRG.velocity.magnitude;
        float enginePitch = Mathf.Lerp(minPitch, maxPitch, speed / 100.0f);
        engineIdleSource.pitch = enginePitch;
        engineAccelerationSource.pitch = enginePitch;
        engineDecelerationSource.pitch = enginePitch;

        float acceleration = carControler.InputMotor;
        if (acceleration > 0)
        {
            engineIdleSource.volume = 0.0f;
            engineAccelerationSource.volume = Mathf.Lerp(engineAccelerationSource.volume, 1.0f, Time.deltaTime * 2);
            engineDecelerationSource.volume = 0.0f;
        }
        else if (acceleration < 0)
        {
            engineIdleSource.volume = 0.0f;
            engineAccelerationSource.volume = 0.0f;
            engineDecelerationSource.volume = Mathf.Lerp(engineDecelerationSource.volume, 1.0f, Time.deltaTime * 2);
        }
        else
        {
            engineIdleSource.volume = Mathf.Lerp(engineIdleSource.volume, 1.0f, Time.deltaTime * 2);
            engineAccelerationSource.volume = 0.0f;
            engineDecelerationSource.volume = 0.0f;
        }

        /*float MotorDir = Vector3.Dot(transform.forward, RB.velocity);
        if (MotorDir < -.5f && carControler.InputMotor > .5f)
        {
            InputBreake = Mathf.Abs(InputMotor);
        }
        else if (MotorDir > .5f && carControler.InputMotor < 0)
        {
            InputBreake = Mathf.Abs(InputMotor);
        }
        else
        {
            InputBreake = carControler.Inbreak ? 1 : 0;
        }*/
    }
}
