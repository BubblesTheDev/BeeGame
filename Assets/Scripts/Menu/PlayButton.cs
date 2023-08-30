using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class PlayButton : MonoBehaviour
{   
    public string sceneName;
    EventInstance MainMenuMusic;

    public MeshRenderer _mr;
    private Color ogMat;
    public Light sunlight;

    private bool play;
    private bool play2;
    private float startPlayTime;

    private Color sunTargetColor;
    public Color sunSunsetColor;
    public Color sunNightColor;
    public Color sunCurrentColor;

    private void Awake()
    {
        MainMenuMusic = RuntimeManager.CreateInstance("event:/MainMenuMusic");
        MainMenuMusic.start();

        sunCurrentColor = sunlight.color;

        play = false;
        play2 = false;
        ogMat = _mr.materials[0].color;
    }

    private void Update()
    {
        if (play && play2 == false)
        {
            float t = (Time.time - startPlayTime) / 1f;
            Color sunColor1 = Color.Lerp(sunCurrentColor, sunSunsetColor, t);
            sunlight.color = sunColor1;

            print(t);

            if(t >= 1)
            {
                print("bruh");
                play2 = true;
                sunCurrentColor = sunColor1;
                startPlayTime = Time.time;
            }
        }

        if (play2)
        {
            float t = (Time.time - startPlayTime) / 1f;
            Color sunColor1 = Color.Lerp(sunCurrentColor, sunNightColor, t);
            sunlight.color = sunColor1;

            print(t);
        }
    }

    public void PlayButtonStart()
    {
        play = true;
        startPlayTime = Time.time;
        StartCoroutine(PlayStartSound());
        
    }

    public void PlayButtonHover()
    {
        _mr.materials[0].color = Color.green;
    }

    public void PlayButtonExit()
    {
        _mr.materials[0].color = ogMat;
    }

    IEnumerator PlayStartSound()
    {
        //MainMenuMusic.setPaused(true);
        MainMenuMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        RuntimeManager.PlayOneShot("event:/GameStart");
        yield return new WaitForSeconds(2f);
        Debug.Log("changinScenes");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        //MainMenuMusic.setPaused(false);
    }
}
