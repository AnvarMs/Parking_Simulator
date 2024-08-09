using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // Array to hold car prefabs
    private GameObject selectedCar;
    public Transform spawnPoint; // Corrected spelling for consistency
   
    private void Awake()
    {
        int selectedCarIndex = PlayerPrefs.GetInt("SelectedCarIndex", 0); // Default to the first car if not set
        selectedCar = Instantiate(carPrefabs[selectedCarIndex], spawnPoint.position, spawnPoint.rotation);

        // Check if CarManager exists, if not create it
       
    }

    private void Start()
    {
        if (selectedCar != null)
        {
            CarManager.Instance.SetActiveCar(selectedCar);
        }
    }
}
