using UnityEngine;
using UnityEngine.EventSystems;

public class SteeringWheelUIController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform steeringWheelRect; // Reference to the RectTransform of the steering wheel UI element
    public float maxSteeringAngle = 200f; // Maximum steering angle for the steering wheel
    public float returnSpeed = 5f; // Speed at which the steering wheel returns to center

    private float currentAngle = 0f;
    private float lastAngle = 0f;
    private bool isDragging = false;

    void FixedUpdate()
    {
        if (!isDragging)
        {
            // Return the steering wheel to the center when not being dragged
            currentAngle = Mathf.Lerp(currentAngle, 0f, Time.deltaTime * returnSpeed);
            UpdateSteeringWheelRotation();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        lastAngle = Vector2.Angle(transform.up,   eventData.position- (Vector2)steeringWheelRect.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 steeringWheelCenter = steeringWheelRect.position;
        Vector2 dragPosition = eventData.position;

        float newAngle = Vector2.Angle(Vector2.up, dragPosition - steeringWheelCenter);

        if (dragPosition.x < steeringWheelCenter.x)
        {
            newAngle = 360f - newAngle;
        }

        float angleDelta = Mathf.DeltaAngle(-lastAngle, -newAngle);
        lastAngle = newAngle;

        currentAngle = Mathf.Clamp(currentAngle + angleDelta, -maxSteeringAngle, maxSteeringAngle);
        UpdateSteeringWheelRotation();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    private void UpdateSteeringWheelRotation()
    {
        steeringWheelRect.localEulerAngles = new Vector3(0f, 0f, currentAngle);
    }

    public float GetSteeringInput()
    {
        // Normalize the steering input between -1 and 1
        return -currentAngle / maxSteeringAngle;
    }
}
