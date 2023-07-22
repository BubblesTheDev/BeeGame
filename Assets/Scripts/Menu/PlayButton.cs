using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;


public class PlayButton : MonoBehaviour
{   
    private Material playMaterial;
    private Color originalColor;
    private MenuBeeManager _mbm;

    public string sceneName;
    private bool mouseOver;
    private bool swarmable;

    // Start is called before the first frame update
    void Start()
    {
        _mbm = GameObject.Find("BeeManager!!!!!!").GetComponent<MenuBeeManager>();
        playMaterial = GetComponent<MeshRenderer>().materials[0];
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
            StartCoroutine(PlayStartSound());
           
        }

        if (swarmable)
        {
            swarmable = false;
            _mbm.PlayButtonSwarm();
        }
    }

    public void TurnPlayButtonOff()
    {
        playMaterial.color = originalColor;

        if (!swarmable)
        {
            swarmable = true;
            _mbm.StopPlayButtonSwarm();
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
    IEnumerator PlayStartSound()
    {
        RuntimeManager.PlayOneShot("event:/GameStart");
        yield return new WaitForSeconds(1.6f);
        Debug.Log("changinScenes");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
