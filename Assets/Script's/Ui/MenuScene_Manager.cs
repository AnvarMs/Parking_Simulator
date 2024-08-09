using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene_Manager : MonoBehaviour
{
    public GameObject Maine_Panel;
    public GameObject Exit_Panel;
    public GameObject Option_Panel;
    public GameObject Level_Panel;
    public GameObject CarSelectionBut_Panel;
    public GameObject CarSelection_Panel;
    // Start is called before the first frame update
    void Start()
    {
        CarSelectionBut_Panel.SetActive(true);
        Maine_Panel.SetActive(true);
        Exit_Panel.SetActive(false);
        Option_Panel.SetActive(false);
        Level_Panel.SetActive(false);
        CarSelection_Panel.SetActive(false);
    }

    // Update is called once per frame
   


    public void CarSelection()
    {
        CarSelection_Panel.SetActive(true);
        Maine_Panel.SetActive(false);
        CarSelectionBut_Panel.SetActive(false);
        Exit_Panel.SetActive(false);
        Option_Panel.SetActive(false);
        Level_Panel.SetActive(false);
    }



   public void Exit()
    {
        Application.Quit();
    }
    public void Play()
    {
        CarSelection_Panel.SetActive(false);
        Maine_Panel.SetActive(false);
        Exit_Panel.SetActive(false);
        Option_Panel.SetActive(false);
        Level_Panel.SetActive(true);
    }
    public void exit_Panel()
    {
        CarSelection_Panel.SetActive(false);
        Maine_Panel.SetActive(false);
        Option_Panel.SetActive(false);
        Level_Panel.SetActive(false);
        Exit_Panel.SetActive(true);
    }
    public void Options()
    {
        CarSelection_Panel.SetActive(false);
        Maine_Panel.SetActive(false);
        Exit_Panel.SetActive(false);
        Level_Panel.SetActive(false);
        Option_Panel.SetActive(true);
    }

    public void BackToMain_Panel()
    {
        CarSelectionBut_Panel.SetActive(true);
        CarSelection_Panel.SetActive(false);
        Exit_Panel.SetActive(false);
        Option_Panel.SetActive(false);
        Level_Panel.SetActive(false);
        Maine_Panel.SetActive(true);

    }
}
