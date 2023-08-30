using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostP : MonoBehaviour
{

    float fadeIntensity;
    public DayData DD;
    public Volume volume;

    // Update is called once per frame
    void Start()
    {

        volume.enabled = true;
        fadeIntensity = DD.DayNum;
        VolumeProfile volumeProfile = volume.profile;



        if (volumeProfile.TryGet<ColorAdjustments>(out var coladjustments))
        {    
            if (SceneManager.GetActiveScene().name != "Day7")
            {
                coladjustments.active = true;
                coladjustments.saturation.Override(fadeIntensity * -3);
            } else
            {
                coladjustments.active = true;
                coladjustments.saturation.Override(0);
            }
            
  
        }

        if (volumeProfile.TryGet<Vignette>(out var Vignette))
        {
            if (SceneManager.GetActiveScene().name != "Day7")
            {
                Vignette.active = true;
                Vignette.smoothness.Override(fadeIntensity/ 10f);
            }
            else
            {
                Vignette.active = true;
                Vignette.smoothness.Override(0.1f);
            }


        }
    }
}
