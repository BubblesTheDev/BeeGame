using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class PollenUI : MonoBehaviour
{

    public GameObject flowerGrouping;
    private GameObject pEmpty;
    private GameObject pHalf;
    private GameObject pFull;

    private int initialFlowerCount;

    // Start is called before the first frame update
    void Start()
    {
        Transform thing = this.transform.GetChild(0).transform.GetChild(1).transform;

        pEmpty = thing.GetChild(2).gameObject;
        pHalf = thing.GetChild(3).gameObject;
        pFull = thing.GetChild(4).gameObject;

        pHalf.SetActive(false);
        pFull.SetActive(false);

        initialFlowerCount = flowerGrouping.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        CheckFlowerCount();
    }

    public void CheckFlowerCount()
    {
        int currentFlowerCount = 0;
        
        for (int i = 0; i < flowerGrouping.transform.childCount; i++)
        {
            if (flowerGrouping.transform.GetChild(i).gameObject.activeSelf)
            {
                currentFlowerCount++;
            }
        }

        if(currentFlowerCount <= 0)
        {
            pHalf.SetActive(false);
            pFull.SetActive(true);
        }
        else if (currentFlowerCount <= initialFlowerCount / 2)
        {
            pHalf.SetActive(true);
            pEmpty.SetActive(false);
        }
    }

}
