using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TransitionManager : MonoBehaviour
{

    float timer;
    public float endTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= endTime)
        {
            Debug.Log("Change Scene");
            //CHANGE SCENE TO NEXT DAY
        }

    }
}
