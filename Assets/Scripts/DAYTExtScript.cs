using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DAYTExtScript : MonoBehaviour
{

    public PlayableDirector tl;
    public Animator anim;

    //FIRST DAY IS DAY 1, NOT DAY 0 
    public int DayNum;
    EventInstance daystart;
    void Start()
    {
        daystart = RuntimeManager.CreateInstance("event:/Mus_startofday");
    }

    void Update()
    {
     
        if (tl.time >= 3f)
        {
            anim.SetInteger("DayNum", DayNum);
            if (PlaybackState(daystart) != PLAYBACK_STATE.PLAYING)
            {
                daystart.start();
            }

        }



    }
    FMOD.Studio.PLAYBACK_STATE PlaybackState(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE pS;
        instance.getPlaybackState(out pS);
        return pS;
    }
}
