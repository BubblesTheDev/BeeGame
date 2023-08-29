using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{

    float timer;
    public float endTime;
    public string[] scenes;
    int DayNum;

    // Start is called before the first frame update
    void Start()
    {
        DayNum = DAYTExtScript.DayNum;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= endTime)
        {
            Debug.Log("Change Scene");
            SceneManager.LoadScene(scenes[DayNum]);
            //CHANGE SCENE TO NEXT DAY
        }

    }
}
