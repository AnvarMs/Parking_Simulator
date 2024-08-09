using UnityEngine;

public class CarManager : MonoBehaviour
{
    public static CarManager Instance;

    private CarControler activeCarController;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of CarManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetActiveCar(GameObject car)
    {
        activeCarController = car.GetComponent<CarControler>();
    }

    public void Accilerate(float value)
    {
        if (activeCarController != null)
        {
            activeCarController.InAccil = value;
        }
    }

    public void BreakLeverPress(bool value)
    {
        if (activeCarController != null)
        {
            activeCarController.Inbreak = value;
        }
    }
}
