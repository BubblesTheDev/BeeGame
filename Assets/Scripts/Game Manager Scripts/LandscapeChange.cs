using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeChange : MonoBehaviour
{
    private int currentDay;
    private bool devToolActive;
    
    [System.Serializable]
    public class ChangingLandscapeObjects
    {
        public GameObject grouping;
        public int dateOfAppearance;
        public int dateOfDestruction;
    }

    public List<ChangingLandscapeObjects> changingLandscapeObjects;

    //public List<GameObject> changingLandscapeObjects;

    // Start is called before the first frame update
    void Start()
    {
        currentDay = 1;

        for (int i = 0; i < changingLandscapeObjects.Count; i++)
        {
                if (changingLandscapeObjects[i].dateOfAppearance != 1)
                {
                    changingLandscapeObjects[i].grouping.SetActive(false);
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("r") && Input.GetKey("t") && Input.GetKey("y"))
        {
            devToolActive = true;
        }
        
        
        
        if (Input.GetKeyDown("p") && devToolActive)
        {
            ChangeLandscape(1);
        }
        else if (Input.GetKeyDown("o") && devToolActive)
        {
            ChangeLandscape(-1);
        }
    }

    public void ChangeLandscape(int dayFactor)
    {
        currentDay = currentDay + dayFactor;
        
        for(int i = 0; i < changingLandscapeObjects.Count; i++)
        {
            if (changingLandscapeObjects[i].dateOfAppearance <= currentDay && changingLandscapeObjects[i].dateOfDestruction > currentDay)
            {
                changingLandscapeObjects[i].grouping.SetActive(true);
            }
            else
            {
                changingLandscapeObjects[i].grouping.SetActive(false);
            }
        }
        
    }
}
