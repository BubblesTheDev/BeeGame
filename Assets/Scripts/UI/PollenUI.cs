using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenUI : MonoBehaviour
{

    public GameObject flowerGrouping;
    private GameObject pollenImgGrouping;
    public List<GameObject> pollenImgs;

    public float pollenCollectionAnimationTime;
    private int initialFlowerCount;
    private int initialImgCount;

    // Start is called before the first frame update
    void Start()
    {
        pollenImgGrouping = this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).gameObject;

        for(int i = 0; i < pollenImgGrouping.transform.childCount; i++)
        {
            pollenImgs.Add(pollenImgGrouping.transform.GetChild(i).gameObject);
        }
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

        print(currentFlowerCount);
     

        /*
        int currentFlowerCount = 0;
        
        for (int i = 0; i < flowerGrouping.transform.childCount; i++)
        {
            if (flowerGrouping.transform.GetChild(i).gameObject.activeSelf)
            {
                currentFlowerCount++;
            }
        }

        if(previousAmount > currentFlowerCount)
        {
            previousAmount = currentFlowerCount;
            StartCoroutine(PollenIconAnimationStart(pollenCollectionAnimationTime));
        }

        if(currentFlowerCount <= 0)
        {
            pHalf.SetActive(false);
            pFull.SetActive(true);
            state = PollenUIState.full;
        }
        else if (currentFlowerCount <= initialFlowerCount / 2)
        {
            pHalf.SetActive(true);
            pEmpty.SetActive(false);
            state = PollenUIState.half;
        }
        */
    }

    public IEnumerator PollenIconAnimationStart(float time)
    {
        yield return new WaitForSeconds(time);
        /*
        Animator bruh = null;
        if (state == PollenUIState.empty)
        {
            bruh = pEmpty.GetComponent<Animator>();
        }
        else if (state == PollenUIState.half)
        {
            bruh = pHalf.GetComponent<Animator>();
        }
        else if (state == PollenUIState.full)
        {
            bruh = pFull.GetComponent<Animator>();
        }

        bruh.SetBool("Bumping", true);

        yield return new WaitForSeconds(0.12f);
        
        bruh.SetBool("Bumping", false);*/
    }
    

}
