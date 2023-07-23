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

    EventInstance pollenCollectingSFX;

    // Start is called before the first frame update

    private void Awake()
    {
        movementBuzz = RuntimeManager.CreateInstance("event:/MovementBuzz");
        idleBuzz = RuntimeManager.CreateInstance("event:/idleBuzz");
        pollenCollectingSFX = RuntimeManager.CreateInstance("event:/pollencollecting");
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
                movementBuzz.setPaused(true);
                idleBuzz.setPaused(false);
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
    public void PlayCollectionSounds()
    {
        if (PlaybackState(pollenCollectingSFX) != PLAYBACK_STATE.PLAYING)
        {
            pollenCollectingSFX.start();
        }
    }
    FMOD.Studio.PLAYBACK_STATE PlaybackState(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE pS;
        instance.getPlaybackState(out pS);
        return pS;
    }
    public void StopCollectionSounds()
    {
        pollenCollectingSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
