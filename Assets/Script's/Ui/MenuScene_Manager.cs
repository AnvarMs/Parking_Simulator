using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene_Manager : MonoBehaviour
{
    public GameObject Maine_Panel;
    public GameObject Exit_Panel;
    public GameObject Option_Panel;
    public GameObject Level_Panel;
    // Start is called before the first frame update
    void Start()
    {
        Maine_Panel.SetActive(true);
        Exit_Panel.SetActive(false);
        Option_Panel.SetActive(false);
        Level_Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

    }



   public void Exit()
    {
        Application.Quit();
    }
    public void Play()
    {
        Maine_Panel.SetActive(false);
        Exit_Panel.SetActive(false);
        Option_Panel.SetActive(false);
        Level_Panel.SetActive(true);
    }
    public void exit_Panel()
    {
        Maine_Panel.SetActive(false);
        Option_Panel.SetActive(false);
        Level_Panel.SetActive(false);
        Exit_Panel.SetActive(true);
    }
    public void Options()
    {
        Maine_Panel.SetActive(false);
        Exit_Panel.SetActive(false);
        Level_Panel.SetActive(false);
        Option_Panel.SetActive(true);
    }

    public void BackToMain_Panel()
    {
        Exit_Panel.SetActive(false);
        Option_Panel.SetActive(false);
        Level_Panel.SetActive(false);
        Maine_Panel.SetActive(true);
    }
}
