using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;

public class MobileCameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float XSensityvity = 7f;
    public float YSensityvity = 0.05f;
    private Transform carTransform;
    private Vector2 touchDelta;

    private InputAction touchPositionAction;
    private InputAction touchDeltaAction;

    private void Awake()
    {
        var inputActions = new PlayerInputActions();
        inputActions.Camera.Enable();

        touchPositionAction = inputActions.Camera.TouchPosition;
        touchDeltaAction = inputActions.Camera.TouchDelta;
    }

    private void Start()
    {
        AssignCarTransform();
    }

    private bool isTouchingUI = false; // Track if the first touch was on the UI

    private void Update()
    {
        // If no touch is detected, reset the flag
        if (Touchscreen.current == null || Touchscreen.current.touches.Count == 0)
        {
            isTouchingUI = false;
            return;
        }

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (!Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                // This is not the initial touch, continue camera rotation if the first touch was not on UI
                if (!isTouchingUI)
                {
                    HandleCameraRotation();
                }
            }
            else
            {
                // First touch detected, check if it's on the UI
                isTouchingUI = IsPointerOverUIObject(Touchscreen.current.primaryTouch);
            }
        }
    }

    private bool IsPointerOverUIObject(TouchControl touch)
    {
        // Get the current touch position
        Vector2 touchPosition = touch.position.ReadValue();

        // Create a new PointerEventData for the touch
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = touchPosition
        };

        // Raycast to check if the touch is over UI elements
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0; // Return true if there's any UI element being touched
    }

    private void AssignCarTransform()
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Player");
        if (cars.Length > 0)
        {
            carTransform = cars[0].transform;
            freeLookCamera.Follow = carTransform;
            freeLookCamera.LookAt = carTransform;
        }
        else
        {
            Debug.LogError("No car with tag 'Player' found in the scene.");
        }
    }
private float xRotationVelocity;
private float yRotationVelocity;
public float smoothTime = 0.01f; 
   
private void HandleCameraRotation()
{
    // Get the current input
    touchDelta = touchDeltaAction.ReadValue<Vector2>();

    // Calculate target rotation values based on input
    float targetXRotation = freeLookCamera.m_XAxis.Value + touchDelta.x * Time.deltaTime * XSensityvity;
    float targetYRotation = freeLookCamera.m_YAxis.Value - touchDelta.y * Time.deltaTime * YSensityvity;

    // Smoothly interpolate between the current rotation and the target rotation
    freeLookCamera.m_XAxis.Value = Mathf.SmoothDamp(
        freeLookCamera.m_XAxis.Value, 
        targetXRotation, 
        ref xRotationVelocity, 
        smoothTime
    );

    freeLookCamera.m_YAxis.Value = Mathf.SmoothDamp(
        freeLookCamera.m_YAxis.Value, 
        targetYRotation, 
        ref yRotationVelocity, 
        smoothTime
    );
}
}