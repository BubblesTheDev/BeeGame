using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{   
    private Material playMaterial;
    private Color originalColor;

    public string sceneName;
    private bool mouseOver;

    // Start is called before the first frame update
    void Start()
    {
        playMaterial = GetComponent<MeshRenderer>().materials[1];
        originalColor = playMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseOver)
        {
            TurnPlayButtonOn();
        }
        else
        {
            TurnPlayButtonOff();
        }
    }

    public void TurnPlayButtonOn()
    {
        playMaterial.color = Color.green;

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    public void TurnPlayButtonOff()
    {
        playMaterial.color = originalColor;
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
