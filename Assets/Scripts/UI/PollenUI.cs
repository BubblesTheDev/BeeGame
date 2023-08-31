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
    private HiveWaypoint _hw;

    public int dailyPollenQuota;
    public float pollenCollectionAnimationTime;
    private int flowersCollected;
    private int prevFlowerCount;
    private float currentPollenUIIndex;

    // Start is called before the first frame update
    void Start()
    {
        _hw = GetComponent<HiveWaypoint>();
        
        pollenImgGrouping = this.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).gameObject;

        for (int i = 0; i < pollenImgGrouping.transform.childCount; i++)
        {
            pollenImgs.Add(pollenImgGrouping.transform.GetChild(i).gameObject);
        }

        for(int i = 0; i < pollenImgs.Count; i++)
        {
            if (pollenImgs[i].activeSelf)
            {
                pollenImgs[i].SetActive(false);
            }
        }

        pollenImgs[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void FlowerCollected()
    {
        flowersCollected++;

        float flowerPercentGotten =  Mathf.Round(((float)flowersCollected / (float)dailyPollenQuota) * 100);

        currentPollenUIIndex = Mathf.Round(((float)flowerPercentGotten * (float)pollenImgs.Count) / 100);

        if (currentPollenUIIndex < 10)
        {
            ChangeFlowerUIImg((int)currentPollenUIIndex);
        }
        else if (currentPollenUIIndex == 10)
        {
            for (int i = 0; i < pollenImgs.Count; i++)
            {
                if (pollenImgs[i].activeSelf)
                {
                    pollenImgs[i].SetActive(false);
                }
            }

            pollenImgs[9].SetActive(true);

            if (pollenImgs[9].TryGetComponent<Animator>(out Animator _anCurrent))
            {
                _an = _anCurrent;
                _an.runtimeAnimatorController = animControl;
            }
            else
            {
                Destroy(_an);
                _an = pollenImgs[9].AddComponent<Animator>();
                _an.runtimeAnimatorController = animControl;
            }
        }
        else if(currentPollenUIIndex >= 11)
        {
            for (int i = 0; i < pollenImgs.Count; i++)
            {
                if (pollenImgs[i].activeSelf)
                {
                    pollenImgs[i].SetActive(false);
                }
            }

            pollenImgs[10].SetActive(true);

            if (pollenImgs[10].TryGetComponent<Animator>(out Animator _anCurrent))
            {
                _an = _anCurrent;
                _an.runtimeAnimatorController = animControl;
            }
            else
            {
                Destroy(_an);
                _an = pollenImgs[10].AddComponent<Animator>();
                _an.runtimeAnimatorController = animControl;
            }

            if (_hw.off)
            {
                _hw.TurnOn();
            }
        }

        StartCoroutine(PollenIconAnimationStart(pollenCollectionAnimationTime));
    }

    public void CheckFlowerCount()
    {
        
    }

    public void ChangeFlowerUIImg(int index)
    {
        for(int i = 0; i < pollenImgs.Count; i++)
        {
            if (pollenImgs[i].activeSelf)
            {
                pollenImgs[i].SetActive(false);
            }
        }

        pollenImgs[index].SetActive(true);

        if(pollenImgs[index].TryGetComponent<Animator>(out Animator _anCurrent))
        {
            _an = _anCurrent;
            _an.runtimeAnimatorController = animControl;
        }
        else
        {
            Destroy(_an);
            _an = pollenImgs[index].AddComponent<Animator>();
            _an.runtimeAnimatorController = animControl;
        }
    }

    public IEnumerator PollenIconAnimationStart(float time)
    {
        yield return new WaitForSeconds(time);
        
        print(currentPollenUIIndex);

        if(currentPollenUIIndex >= 11)
        {
            _an.SetBool("Full", true);

            yield return new WaitForSeconds(0.3f);

            _an.SetBool("Full", false);
        }
        else if (currentPollenUIIndex < 11)
        {
            _an.SetBool("Bumping", true);

            yield return new WaitForSeconds(0.3f);

            _an.SetBool("Bumping", false);
        }
    }
    

}
