using System;
using UnityEngine;
using TMPro;  // Make sure to include this namespace for TextMeshPro support
using UnityEngine.Events;
using UnityEngine.UI;  // For Unity Events

public class CountdownTimer : MonoBehaviour
{
    public Text timerText;  // Reference to the TextMeshPro UI component
    public int startMinutes = 1;  // Set the starting minutes for the countdown
    public UnityEvent onTimerEnd;  // Event to trigger when timer ends

    private float timeRemaining;
    private bool isCountingDown = false;

    void Start()
    {
        // Calculate total seconds from minutes
        timeRemaining = startMinutes * 60;
        StartCountdown();
    }

    void StartCountdown()
    {
        isCountingDown = true;
        UpdateTimerText();
    }

    void Update()
    {
        if (isCountingDown)
        {
            if (timeRemaining > 0)
            {
                // Reduce timeRemaining by the time passed since last frame
                timeRemaining -= Time.deltaTime;

                // Clamp the value to 0 so it doesn't go negative
                timeRemaining = Mathf.Clamp(timeRemaining, 0, Mathf.Infinity);

                // Update the UI text
                UpdateTimerText();
            }
            else
            {
                // Timer has reached zero, trigger the event
                isCountingDown = false;
                onTimerEnd.Invoke();
            }
        }
    }

    void UpdateTimerText()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Update the TextMeshPro text with formatted time
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
