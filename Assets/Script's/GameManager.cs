using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadCorentScene()
    {

        // Get the active scene index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the current scene by index
        SceneManager.LoadScene(sceneIndex);

    }
}
