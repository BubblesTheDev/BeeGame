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

    private void Awake()
    {
        MainMenuMusic = RuntimeManager.CreateInstance("event:/MainMenuMusic");
        MainMenuMusic.start();
    }
    public void PlayButtonStart()
    {
        StartCoroutine(PlayStartSound());
        
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
