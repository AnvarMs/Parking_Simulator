using Cinemachine;
using UnityEngine;

public class CarSelecter : MonoBehaviour
{
    public GameObject[] carPrefabs; // Array to hold car prefabs
    private GameObject selectedCar;
    public Transform spawnPoint; // Corrected spelling for consistency
    public CinemachineVirtualCamera virtualCamera;
    private int index;

    private void Start()
    {
        index = 0;
        selectedCar = Instantiate(carPrefabs[index], spawnPoint.position, spawnPoint.rotation);
        virtualCamera.LookAt = selectedCar.transform;
        virtualCamera.Follow = selectedCar.transform;
    }

    public void SelectCar(int n)
    {
        index += n;

        // Wrap the index around if it goes out of bounds
        if (index < 0)
        {
            index = carPrefabs.Length - 1;
        }
        else if (index >= carPrefabs.Length)
        {
            index = 0;
        }

        // Destroy the currently selected car if it exists
        if (selectedCar != null)
        {
            Destroy(selectedCar);
        }

        // Instantiate the new selected car at the spawn point with the exact rotation
        selectedCar = Instantiate(carPrefabs[index], spawnPoint.position, spawnPoint.rotation);
        PlayerPrefs.SetInt("SelectedCarIndex", index);
        PlayerPrefs.Save();
        // Update the virtual camera to look at and follow the new car
        virtualCamera.LookAt = selectedCar.transform;
        virtualCamera.Follow = selectedCar.transform;
    }
}
