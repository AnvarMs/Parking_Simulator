using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MobileCameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
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

    private void Update()
    {
        if (IsPointerOverUIObject())
        {
            return; // Skip camera rotation logic if touching UI
        }

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            HandleCameraRotation();
        }
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

    private void HandleCameraRotation()
    {
        touchDelta = touchDeltaAction.ReadValue<Vector2>();
        freeLookCamera.m_XAxis.Value += touchDelta.x * Time.deltaTime * 1.5f; // Adjust sensitivity as needed
        freeLookCamera.m_YAxis.Value -= touchDelta.y * Time.deltaTime * 0.05f;
    }

    private bool IsPointerOverUIObject()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = touch.position
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }

        return EventSystem.current.IsPointerOverGameObject();
    }
}
