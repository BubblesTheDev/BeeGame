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


    private Color camTargetColor;
    public Color camSunsetColor;
    public Color camNightColor;
    private Color camCurrentColor;

    private Color sunTargetColor;
    public Color sunSunsetColor;
    public Color sunNightColor;
    private Color sunCurrentColor;

    private void Awake()
    {
        MainMenuMusic = RuntimeManager.CreateInstance("event:/MainMenuMusic");
        MainMenuMusic.start();

        play = false;
        ogMat = _mr.materials[0].color;
    }

    private void Update()
    {
        
    }

    public void PlayButtonStart()
    {
        play = true;
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
        yield return new WaitForSeconds(1.6f);
        Debug.Log("changinScenes");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        //MainMenuMusic.setPaused(false);
    }
}
