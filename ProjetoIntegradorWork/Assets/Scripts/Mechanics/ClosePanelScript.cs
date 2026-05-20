using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePanelScript : MonoBehaviour
{
    public GameObject painel;


    void Start()
    {
        
    }

    
    void Update()
    {
        
        

    }

public void OnClick()
    {
        if (Input.touchCount == 1)
        {
            painel.SetActive(false);
        }
    }
}
