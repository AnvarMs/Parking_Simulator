using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
 
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
    public void LoadCorentScene()
    {

        // Get the active scene index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the current scene by index
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }


    public void setFalseOrTrue(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
    

    
}
