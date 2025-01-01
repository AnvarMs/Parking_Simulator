using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class CarData
{
    public int carID; // This represents the key
    public bool isPurchased; // This represents the value
}

[System.Serializable]
public class LevelData
{
    public string level;
    public bool isCleared;
}

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerDataScriptObj : ScriptableObject
{
    
    [SerializeField] private string playerName;
    [SerializeField] private int currency;
    [SerializeField] private List<CarData> cars = new List<CarData>(); // List to store car data
    [SerializeField] private List<LevelData> levels = new List<LevelData>(); // List to store car data
    [SerializeField] public bool isLogged;
    [SerializeField] private string savePath;
    public static UnityEvent<string> UpdateLevel;
    // Properties for easy access
    public string PlayerName { get => playerName; set => playerName = value; }
    public int Currency { get => currency; set => currency = value; }
    public List<CarData> Cars { get => cars; set => cars = value; }

    private void OnEnable()
    {
        savePath = Path.Combine(Application.persistentDataPath, "parkingsimulator.json");
        InitializeDefaultCars();
        LoadData();
        UpdateLevel = new UnityEvent<string>();
        if(UpdateLevel != null)
        {
            UpdateLevel.AddListener(LevelCleared);
           UpdateLevel.AddListener(eventCheck); 
        }
    }

    void eventCheck(string message)
    {
        Debug.Log(message);
    }

    private void InitializeDefaultCars()
    {
        // Check if cars are empty, then add default values
        if (cars.Count == 0)
        {
            cars.Add(new CarData { carID = 0, isPurchased = true });
            cars.Add(new CarData { carID = 1, isPurchased = false });
            cars.Add(new CarData { carID = 2, isPurchased = false });
            // Add more default cars as needed
        }
        if (levels.Count == 0) { 
        
                levels.Add(new LevelData { level= "Level 1",isCleared= true});
                levels.Add(new LevelData { level= "Level 2",isCleared= false});
                levels.Add(new LevelData { level= "Level 3",isCleared= false });
                levels.Add(new LevelData { level= "Level 4",isCleared= false });
                levels.Add(new LevelData { level= "Level 5",isCleared= false });
                levels.Add(new LevelData { level= "Level 6",isCleared= false });
                levels.Add(new LevelData { level= "Level 7",isCleared= false });
                levels.Add(new LevelData { level= "Level 8",isCleared= false });
                levels.Add(new LevelData { level= "Level 9",isCleared= false });
                levels.Add(new LevelData { level= "Level 10",isCleared= false });
        
        }
       
    }

    // Call this to save the data
    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(savePath, jsonData);
    }

    // Call this to load the data
    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            JsonUtility.FromJsonOverwrite(jsonData, this);
        }
    }

    // Additional methods to work with the car data
    public void PurchaseCar(int carID)
    {
        CarData carData = cars.Find(c => c.carID == carID);
        if (carData == null)
        {
            cars.Add(new CarData { carID = carID, isPurchased = true });
        }
        else
        {
            carData.isPurchased = true; // Mark as purchased
        }
        SaveData();
        LoadData();
    }

    public bool IsCarPurchased(int carID)
    {
        CarData carData = cars.Find(c => c.carID == carID);
        return carData != null && carData.isPurchased;
    }

    public void AddCar(int carID)
    {
        // Check if the car is already in the list by searching for the carID
        CarData carData = cars.Find(car => car.carID == carID);

        // If no car with the given ID is found, add a new one
        if (carData == null)
        {
            cars.Add(new CarData { carID = carID, isPurchased = false });
        }
        SaveData();
        LoadData();
    }
    public void LevelCleared(string theLevel)
    {
        // Assuming 'levels' is a List<LevelData>
        LevelData levelData = levels.Find(c => c.level.Equals(theLevel));

        // Check if the level was found
        if (levelData == null)
        {
            // If level is not found, add it to the list and mark it as cleared
            levels.Add(new LevelData { level = theLevel, isCleared = true });
        }
        else
        {
            // If level exists, just update the isCleared flag
            levelData.isCleared = true;
        }

      SaveData();
      LoadData();
    }
    public bool isLevelClearde(string theLevel) {

        LevelData levelData = levels.Find(c => c.level == theLevel);
        return levelData != null && levelData.isCleared;

    }


    private void OnApplicationQuit()
    {
        SaveData();
    }

}
