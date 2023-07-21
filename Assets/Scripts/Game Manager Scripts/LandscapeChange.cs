using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeChange : MonoBehaviour
{
    public int currentDay;
    
    public List<GameObject> changingLandscapeObjects;

    // Start is called before the first frame update
    void Start()
    {
        currentDay = 1;
        Transform env = this.transform;

        for (int i = 0; i < env.childCount; i++)
        {
            if (env.GetChild(i).TryGetComponent<ChangableLandscapeObject>(out ChangableLandscapeObject tryfind))
            {
                changingLandscapeObjects.Add(tryfind.gameObject);

                if (tryfind.minDayActive != 1)
                {
                    tryfind.gameObject.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            ChangeLandscape();
        }
    }

    public void ChangeLandscape()
    {
        currentDay++;
        
        for(int i = 0; i < changingLandscapeObjects.Count; i++)
        {
            ChangableLandscapeObject CLO = changingLandscapeObjects[i].GetComponent<ChangableLandscapeObject>();

            if (CLO.minDayActive <= currentDay && CLO.maxDayActive >= currentDay)
            {
                changingLandscapeObjects[i].SetActive(true);
            }
            else
            {
                changingLandscapeObjects[i].SetActive(false);
            }
        }
    }
}
