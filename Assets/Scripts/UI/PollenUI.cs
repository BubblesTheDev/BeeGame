using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenUI : MonoBehaviour
{
    public GameObject flowerGrouping;
    public RuntimeAnimatorController animControl;
    public List<GameObject> pollenImgs;
    private GameObject pollenImgGrouping;
    private Animator _an;

    public float pollenCollectionAnimationTime;
    private int initialFlowerCount;
    private int prevImageIndex;
    private int prevFlowerCount;

    // Start is called before the first frame update
    void Start()
    {
        prevImageIndex = 100000;
        prevFlowerCount = flowerGrouping.transform.childCount;

        pollenImgGrouping = this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).gameObject;

        for(int i = 0; i < pollenImgGrouping.transform.childCount; i++)
        {
            pollenImgs.Add(pollenImgGrouping.transform.GetChild(i).gameObject);
        }

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

        float flowerPercentLeft = ((float)currentFlowerCount / (float)initialFlowerCount) * 100;

        float currentPollenUIIndex = Mathf.Round((flowerPercentLeft * (float)pollenImgs.Count) / 100);

        if(currentPollenUIIndex != prevImageIndex)
        {
            ChangeFlowerUIImg((int)currentPollenUIIndex);
        }

        if (currentFlowerCount != prevFlowerCount)
        {
            prevFlowerCount = currentFlowerCount;
            StartCoroutine(PollenIconAnimationStart(pollenCollectionAnimationTime));
        }
    }

    public void ChangeFlowerUIImg(int index)
    {
        prevImageIndex = index;

        for(int i = 0; i < pollenImgs.Count; i++)
        {
            if (pollenImgs[i].activeSelf)
            {
                pollenImgs[i].SetActive(false);
            }
        }

        pollenImgs[index - 1].SetActive(true);

        Destroy(_an);
        _an = pollenImgs[index - 1].AddComponent<Animator>();
        _an.runtimeAnimatorController = animControl;
    }

    public IEnumerator PollenIconAnimationStart(float time)
    {
        yield return new WaitForSeconds(time);

        _an.SetBool("Bumping", true);

        yield return new WaitForSeconds(0.3f);

        _an.SetBool("Bumping", false);
    }
    

}
