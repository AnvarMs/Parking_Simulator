using UnityEngine;
using UnityEngine.UI;  // To interact with the UI Text component
using UnityEngine.SceneManagement;  // For scene loading
using System.Collections;
using System.Collections.Generic;

public class EndSceneTextWriter : MonoBehaviour
{
    public Text uiText; // Reference to the UI Text component where the text will be displayed
    public float typingSpeed = 0.05f; // Speed of the typing effect
    public List<string> lines = new List<string>(); // List of strings representing lines of text
    public string nextSceneName; // Name of the scene to load after all lines are displayed

    private int currentLineIndex = 0; // Keep track of the current line

    // Start is called before the first frame update
    void Start()
    {
        // Start displaying the text when the scene starts
        StartCoroutine(DisplayText());
    }

    // Coroutine to display text one character at a time with a typing effect
    IEnumerator DisplayText()
    {
        // Loop through all lines in the list
        while (currentLineIndex < lines.Count)
        {
            yield return StartCoroutine(TypeLine(lines[currentLineIndex])); // Type the current line
            currentLineIndex++; // Move to the next line
            yield return new WaitForSeconds(1f); // Pause before displaying the next line (optional)
        }

        // After all lines have been displayed, load the next scene
        LoadNextScene();
    }

    // Coroutine to type out each character in the line with a delay
    IEnumerator TypeLine(string line)
    {
        uiText.text = ""; // Clear the UI text
        foreach (char c in line.ToCharArray())
        {
            uiText.text += c; // Add each character to the UI text
            yield return new WaitForSeconds(typingSpeed); // Wait before adding the next character
        }
    }

    // Load the next scene once all text has been displayed
    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName); // Load the specified scene
    }
}
