using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenUI : MonoBehaviour
{

    public GameObject flowerGrouping;
    private GameObject pEmpty; //these are all placeholders and are subject to change
    private GameObject pHalf;
    private GameObject pFull;

    public float pollenCollectionAnimationTime;
    private int initialFlowerCount;
    private int previousAmount;

    private enum PollenUIState
    {
        empty,
        half,
        full
    }

    private PollenUIState state;

    // Start is called before the first frame update
    void Start()
    {
        Transform thing = this.transform.GetChild(0).transform.GetChild(1).transform;
        state = PollenUIState.empty;

        pEmpty = thing.GetChild(2).gameObject;
        pHalf = thing.GetChild(3).gameObject;
        pFull = thing.GetChild(4).gameObject;

        pHalf.SetActive(false);
        pFull.SetActive(false);

        initialFlowerCount = flowerGrouping.transform.childCount;
        previousAmount = initialFlowerCount;
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
    }

    public IEnumerator PollenIconAnimationStart(float time)
    {
        yield return new WaitForSeconds(time);
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
        
        bruh.SetBool("Bumping", false);
    }

}
