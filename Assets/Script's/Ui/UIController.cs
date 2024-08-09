using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public void OnAcceleratePress()
    {
        CarManager.Instance.Accilerate(1f);
    }

    public void OnAccelerateRelease()
    {
        CarManager.Instance.Accilerate(0f);
    }

    public void OnBrakePress()
    {
        CarManager.Instance.BreakLeverPress(true);
    }

    public void OnBrakeRelease()
    {
        CarManager.Instance.BreakLeverPress(false);
    }
}
