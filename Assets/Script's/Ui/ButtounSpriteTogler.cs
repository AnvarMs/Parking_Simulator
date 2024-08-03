using UnityEngine;
using UnityEngine.UI;

public class MusicButtonController : MonoBehaviour
{
    public Sprite musicEnabledSprite;
    public Sprite musicDisabledSprite;
    public Image buttonImage;
    private bool isMusicEnabled = true;

    void Start()
    {
        // Set the initial sprite
        buttonImage.sprite = musicEnabledSprite;
    }

    public void OnButtonPress()
    {
       
        // Toggle the music state
        isMusicEnabled = !isMusicEnabled;

        // Change the sprite based on the new state
        if (isMusicEnabled)
        {
            buttonImage.sprite = musicEnabledSprite;
           
            // Code to enable music
           // AudioListener.volume = 1;
        }
        else
        {
            buttonImage.sprite = musicDisabledSprite;
            
            // Code to disable music
            //AudioListener.volume = 0;
        }
    }
}
