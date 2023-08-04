using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class UIButtonFunctionality : MonoBehaviour
{
    private Button pauseButton;
    private Button muteButton;
    private GameObject pauseScreen;

    public beeMovement beeMove;



    // Start is called before the first frame update
    void Start()
    {
        Transform thing = this.transform.GetChild(0).transform.GetChild(1).transform;
        pauseScreen = this.transform.GetChild(0).transform.GetChild(2).gameObject;

        pauseButton = thing.GetChild(0).gameObject.GetComponent<Button>();
        muteButton = thing.GetChild(1).gameObject.GetComponent<Button>();

        pauseScreen.SetActive(false);
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
        pauseScreen.SetActive(false);
        beeMove.enabled = true;
        NavMeshAgent nma = beeMove.gameObject.GetComponent<NavMeshAgent>();
        nma.enabled = true;

        pauseButton.gameObject.SetActive(true);
    }

    public void PressedPause()
    {
        pauseScreen.SetActive(true);
        beeMove.enabled = false;
        NavMeshAgent nma = beeMove.gameObject.GetComponent<NavMeshAgent>();
        nma.enabled = false;
        pauseButton.gameObject.SetActive(false);
    }

    public void PressedMute()
    {
        print("bruh");
    }
}