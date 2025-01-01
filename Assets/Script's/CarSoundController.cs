using UnityEngine;
using UnityEngine.UI;

public class CarSoundController : MonoBehaviour
{
    public Text speedText;
    public AudioSource idleSource; // Renamed to follow C# naming conventions
    public Rigidbody carRigidbody;

    private float speed;
    private float minPitch = 0.2f; // Minimum pitch for idle sound
    private float maxPitch = 2.0f; // Maximum pitch for idle sound

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();

        // Start playing the idle sound
        if (idleSource != null)
        {
            idleSource.Play();
        }
    }

    void Update()
    {
        UpdateSpeed();
        UpdateIdleSound();
    }

    // Updates the displayed speed (optional)
    private void UpdateSpeed()
    {
        speed = carRigidbody.velocity.magnitude * 3.6f; // Convert m/s to km/h

        if (speedText != null)
        {
            speedText.text = "Speed: " + Mathf.Round(speed) + " km/h";
        }
    }

    // Updates the idle sound pitch based on speed
    private void UpdateIdleSound()
    {
        if (idleSource != null)
        {
            // Linearly interpolate the pitch based on speed, clamping it between minPitch and maxPitch
            idleSource.pitch = Mathf.Lerp(minPitch, maxPitch, speed / 100f);
        }
    }
}
