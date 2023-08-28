using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    public bool mouseOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseOver)
        {
            //TurnPlayButtonOn();
            print("burh");
        }
        else
        {
           // TurnPlayButtonOff();
        }
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseOver()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
