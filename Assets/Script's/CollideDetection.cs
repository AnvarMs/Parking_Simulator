using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollideDetection : MonoBehaviour
{
    
    
    public GameObject controler, finishPanel, LoosPanel;
    public Text text;

    // Start is called before the first frame update

    private void Awake()
    {
        controler = GameObject.Find("Controle_Right");
        finishPanel = GameObject.Find("Finish_Panel");
        LoosPanel = GameObject.Find("Loos_Panel");
    }




    // Update is called once per frame
    void Update()
    {
        //text.text=count.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision != null)
        {
            if (collision.gameObject.tag == "cube")
            {
                

              
            }
            else
            {
                SetPanel();
                
            }
        }
    }
    void SetPanel()
    {
        LoosPanel.SetActive(true);
    }
}
