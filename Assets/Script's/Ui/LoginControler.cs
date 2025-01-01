
using UnityEngine;
using UnityEngine.UI;

public class LoginControler : MonoBehaviour
{
  [SerializeField] InputField playerNmae;
  [SerializeField] Canvas mainCanvas;
  [SerializeField] Canvas loginCanvas;
  [SerializeField] PlayerDataScriptObj playerData;
  [SerializeField] Button button;

  [SerializeField] Text PlayerNametext, Corrency;

   
    private void Start()
    {
        if (playerData.isLogged)
        {
            mainCanvas.gameObject.SetActive(true);
            loginCanvas.gameObject.SetActive(false);
            DisplayPlayerStatus();
        }
        else
        {
            mainCanvas.gameObject.SetActive(false);
            loginCanvas.gameObject.SetActive(true);
        }
        
    }
    
    public void ValidateLogin()
    {
        playerData.PlayerName = playerNmae.text;
        playerData.isLogged = true;
        loginCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
        DisplayPlayerStatus();
        playerData.SaveData();
    }

    void DisplayPlayerStatus()
    {
        PlayerNametext.text = playerData.PlayerName;
        Corrency.text = playerData.Currency.ToString();
    }


}
