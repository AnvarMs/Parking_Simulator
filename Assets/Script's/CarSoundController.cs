using UnityEngine;
using UnityEngine.UI;

public class CarSoundController : MonoBehaviour
{
    public Text speedText;
    public AudioSource IdleSource;
    public AudioSource low_off;
    public AudioSource low_on;
    public AudioSource mid_off;
    public AudioSource mid_on;
    public AudioSource high_off;
    public AudioSource high_on;
    public AudioSource enginePopSource;

    public Rigidbody carRigidbody;
    private CarControler carController;

    private float speed;
    private int currentGear = 0;
    private float pitchMultiplier = 1.5f;
    private float gearShiftDelay = 1f;
    private float lastGearShiftTime;

    private float[] gearRatios = new float[] { 1.0f, 2.0f, 3.0f, 4.0f }; // Simplified gear ratios
    private float minPitch = 0.2f;
    private float maxPitch = 2.0f;

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        carController = GetComponent<CarControler>();

        // Play idle sound
        IdleSource.Play();
        PauseAllEngineSounds();
    }

    void Update()
    {
        UpdateSpeed();
        UpdateGear();
        UpdateEngineSounds();
    }

    private void UpdateSpeed()
    {
        speed = carRigidbody.velocity.magnitude * 3.6f; // Convert from m/s to km/h
        
        if (speedText != null)
        {
            speedText.text = "Speed: " + Mathf.Round(speed) + " km/h";
        }
    }

    private void UpdateGear()
    {
        float acceleration = carController.InputMotor;

        if (acceleration > 0.1f && Time.time > lastGearShiftTime + gearShiftDelay)
        {
            if (currentGear < gearRatios.Length - 1)
            {
                currentGear++;
                lastGearShiftTime = Time.time;
            }
        }
        else if (acceleration < 0.1f && Time.time > lastGearShiftTime + gearShiftDelay)
        {
            if (currentGear > 0)
            {
                currentGear--;
                lastGearShiftTime = Time.time;
            }
        }
    }

    private void UpdateEngineSounds()
    {
        float pitch = Mathf.Lerp(minPitch, maxPitch, speed / (gearRatios[currentGear] * 100f)) * pitchMultiplier;

        IdleSource.pitch = Mathf.Lerp(maxPitch, minPitch, speed / 40f);

        if (speed < 50f)
        {
            PlayEngineSound(low_on, low_off, pitch);
        }
        else if (speed < 60f)
        {
            PlayEngineSound(mid_on, mid_off, pitch);
        }
        else
        {
            PlayEngineSound(high_on, high_off, pitch);
        }

        // Optional: Engine pop sound for realism
        if (speed > 10f && enginePopSource != null && !enginePopSource.isPlaying)
        {
            enginePopSource.Play();
        }
    }

    private void PlayEngineSound(AudioSource onSource, AudioSource offSource, float pitch)
    {
        onSource.pitch = pitch;
        if (!onSource.isPlaying)
        {
            onSource.Play();
        }

        offSource.Pause();
    }

    private void PauseAllEngineSounds()
    {
        low_off.Pause();
        low_on.Pause();
        mid_off.Pause();
        mid_on.Pause();
        high_off.Pause();
        high_on.Pause();
    }
}
