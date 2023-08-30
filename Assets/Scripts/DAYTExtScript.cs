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
    public DayData DD;

    //FIRST DAY IS DAY 1, NOT DAY 0 
    public int DayNum;
    
    void Start()
    {

        DayNum = DD.DayNum;
        anim.SetInteger("DayNum", DayNum);
        DayNum++;
        DD.DayNum = DayNum;
        
    }

    void Update()
    {
     
        if (tl.time >= 3f)
        {
            //anim.SetInteger("DayNum", DayNum);
            anim.SetInteger("DayNum", DayNum);

        }



    }
    
}
