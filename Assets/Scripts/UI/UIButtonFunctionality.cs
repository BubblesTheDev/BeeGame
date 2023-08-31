using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using FMODUnity;
using FMOD.Studio;

public class UIButtonFunctionality : MonoBehaviour
{
    private Button pauseButton;
    private Button muteButton;
    private GameObject pauseScreen;

    public beeMovement beeMove;

    private Bus masterBus;

    // Start is called before the first frame update
    void Start()
    {
        Transform thing = this.transform.GetChild(0).transform.GetChild(1).transform;
        pauseScreen = this.transform.GetChild(0).transform.GetChild(2).gameObject;

        pauseButton = thing.GetChild(0).gameObject.GetComponent<Button>();
        muteButton = thing.GetChild(1).gameObject.GetComponent<Button>();

        pauseScreen.SetActive(false);

        masterBus = RuntimeManager.GetBus("bus:/");
    }

    public void HoveredPause()
    {
        beeMove.enabled = false;
    }

    public void ExitHoveredPause()
    {
        beeMove.enabled = true;
    }

    public void HoveredMute()
    {
        beeMove.enabled = false;
    }

    public void ExitHoveredMute()
    {
        beeMove.enabled = true;
    }

    public void PressedPlay()
    {
        RuntimeManager.PlayOneShot("event:/sfx_menuselect");
        masterBus.setVolume(1);
        pauseScreen.SetActive(false);
        beeMove.enabled = true;
        NavMeshAgent nma = beeMove.gameObject.GetComponent<NavMeshAgent>();
        nma.enabled = true;

        pauseButton.gameObject.SetActive(true);
        
    }

    public void PressedPause()
    {
        RuntimeManager.PlayOneShot("event:/sfx_menuselect");
        masterBus.setVolume(0);
        pauseScreen.SetActive(true);
        beeMove.enabled = false;
        NavMeshAgent nma = beeMove.gameObject.GetComponent<NavMeshAgent>();
        nma.enabled = false;
        pauseButton.gameObject.SetActive(false);

        
        
        
        
        
        
            
        
    }

    public void PressedMute()
    {
        RuntimeManager.PlayOneShot("event:/sfx_menuselect");

        masterBus.getVolume(out float volume);
        if (volume == 1) {
            masterBus.setVolume(0);
        }
        if (volume == 0)
        {
            masterBus.setVolume(1);
        }
    }
    
}
