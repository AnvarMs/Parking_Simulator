using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CarSelecter : MonoBehaviour
{
    public GameObject[] carPrefabs;// Array to hold car prefabs
    public Sprite[] sprites;
    public int[] carPrises;
    private GameObject selectedCar;
    public Transform spawnPoint; // Corrected spelling for consistency
    public CinemachineVirtualCamera virtualCamera;
    public Image image;
    public PlayerDataScriptObj PlayerData;
    public MenuScene_Manager MenuScene;
    public GameObject EnofMonyPanel;
    public Text PlayOrBuyText, CarPriceText , Corrency;
    private int index;
    private bool IsCarPlayeble;

    

     

    private void Start()
    {
        
        index = PlayerPrefs.GetInt("SelectedCarIndex");
        selectedCar = Instantiate(carPrefabs[index], spawnPoint.position, spawnPoint.rotation);
        virtualCamera.LookAt = selectedCar.transform;
        virtualCamera.Follow = selectedCar.transform;
        updatetheImage();
        BouttoneTextUpdate();
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
        BouttoneTextUpdate();
        // Instantiate the new selected car at the spawn point with the exact rotation
        selectedCar = Instantiate(carPrefabs[index], spawnPoint.position, spawnPoint.rotation);
        PlayerPrefs.SetInt("SelectedCarIndex", index);
        PlayerPrefs.Save();
        // Update the virtual camera to look at and follow the new car
        virtualCamera.LookAt = selectedCar.transform;
        virtualCamera.Follow = selectedCar.transform;
        updatetheImage();
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("SelectedCarIndex") != index)
        {
            PlayerPrefs.SetInt("SelectedCarIndex", index);
            PlayerPrefs.Save();
        }
    }

    public void updatetheImage()
    {
        image.sprite= sprites[index];
    }
    public void PlayGame()
    {
        if (IsCarPlayeble)
        {
            MenuScene.Play();
        }
        else
        {
            if (PlayerData.Currency >= carPrises[index])
            {
                PlayerData.Currency = PlayerData.Currency- carPrises[index];
                
                PlayerData.PurchaseCar(index);
                Corrency.text = PlayerData.Currency.ToString();
                BouttoneTextUpdate();
            }
            else
            {
                EnofMonyPanel.SetActive(true);
            }
        }
    }
    void BouttoneTextUpdate()
    {
        IsCarPlayeble = PlayerData.IsCarPurchased(index);
        if (IsCarPlayeble)
        {
            PlayOrBuyText.text = "PLAY";
            CarPriceText.text = "";
        }
        else
        {
            PlayOrBuyText.text = "BUY";
            CarPriceText.text = "$"+carPrises[index].ToString();
        }
    }
}
