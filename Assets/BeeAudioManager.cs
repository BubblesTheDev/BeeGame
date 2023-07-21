using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class BeeAudioManager : MonoBehaviour
{
    
    EventInstance movementBuzz;
    EventInstance idleBuzz;
    
    
    // Start is called before the first frame update
    
    private void Awake()
    {
        movementBuzz = RuntimeManager.CreateInstance("event:/MovementBuzz");
        idleBuzz = RuntimeManager.CreateInstance("event:/idleBuzz");
        idleBuzz.start();
        movementBuzz.start();
        SetBeeAudio(0);
    }
    // Update is called once per frame
    void Update()
    {
       


    }
    public void SetBeeAudio(int beeState)
    {
        switch (beeState)
        {
            case 0:
                movementBuzz.setPaused(false);
                idleBuzz.setPaused(true);
                Debug.Log("Idling");
                break;

            case 1:
                movementBuzz.setPaused(false);
                idleBuzz.setPaused(true);
                Debug.Log("Moving");
                break;

            case 2:
                movementBuzz.setPaused(true);
                idleBuzz.setPaused(true);
                Debug.Log("Collecting");
                break;


        }
    }
}
