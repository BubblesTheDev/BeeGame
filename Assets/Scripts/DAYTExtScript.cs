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
    
    void Start()
    {
        
    }

    void Update()
    {
     
        if (tl.time >= 3f)
        {
            anim.SetInteger("DayNum", DayNum);
            

        }



    }
    
}
