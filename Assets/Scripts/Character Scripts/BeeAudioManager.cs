using FMOD.Studio;
using FMODUnity;
using FMODUnityResonance;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class BeeAudioManager : MonoBehaviour
{
    
    EventInstance movementBuzz;
    EventInstance idleBuzz;

    EventInstance pollenCollectingSFX;

    bool audioResumed = false;
    private bool isCollecting = false;
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
                if(PlaybackState(idleBuzz)  != PLAYBACK_STATE.PLAYING && !isCollecting)
                {
                    idleBuzz.start();
                    //Debug.Log("Idling");
                }
                movementBuzz.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                
                
                break;

            case 1:
                if (PlaybackState(movementBuzz) != PLAYBACK_STATE.PLAYING)
                {
                    movementBuzz.start();
                }
                
                idleBuzz.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                ResumeAudio();
                //Debug.Log("Moving");
                break;

            case 2:

                movementBuzz.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                idleBuzz.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                //Debug.Log("Collecting");
                break;


        }
    }
    public void PlayCollectionSounds()
    {
        if (PlaybackState(pollenCollectingSFX) != PLAYBACK_STATE.PLAYING)
        {
            pollenCollectingSFX.start();
            isCollecting = true;
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
        isCollecting = false;
    }
   

    public void ResumeAudio()
    {
        if (!audioResumed)
        {
            var result = FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
            //Debug.Log(result);
            result = FMODUnity.RuntimeManager.CoreSystem.mixerResume();
            //Debug.Log(result);
            audioResumed = true;
        }
    }
}
