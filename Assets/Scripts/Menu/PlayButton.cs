using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;


public class PlayButton : MonoBehaviour
{   
    public string sceneName;

    public void PlayButtonStart()
    {
        StartCoroutine(PlayStartSound());
    }

    IEnumerator PlayStartSound()
    {
        RuntimeManager.PlayOneShot("event:/GameStart");
        yield return new WaitForSeconds(1.6f);
        Debug.Log("changinScenes");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
