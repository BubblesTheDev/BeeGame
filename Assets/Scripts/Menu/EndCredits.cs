using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMOD.Studio;
using FMODUnity;

public class EndCredits : MonoBehaviour
{
    [SerializeField] private Image skipCredsRef;
    [SerializeField] private Text holdText;
    [SerializeField] private Image FadeToBlack;

    [SerializeField] float holdTimer;
    [SerializeField] float timeToHold = 1.5f;

    [SerializeField] float fadeToBlackTimer;
    [SerializeField] float timeToSwitch = .8f;

    [SerializeField] float endTimer;
    [SerializeField] float endTime = 100f;

    

    bool isSkipping;
    bool hasSkipped;
    // Start is called before the first frame update
    void Start()
    {

        holdText = holdText.GetComponent<Text>();
        

    }

    // Update is called once per frame
    void Update()
    {
        endTimer += Time.deltaTime;

        if (endTimer > endTime)
        {
            
            StartCoroutine(SceneChange());
        }

        SkipCredits();
        if (Input.GetMouseButton(0)) 
        {
            isSkipping = true;
            
        }
        if(Input.GetMouseButtonUp(0))
        {
            isSkipping = false;
        }

        
    }
    void SkipCredits() 
    {
        if(isSkipping == true)
        {
            holdTimer += Time.deltaTime;
            holdText.color = new Color(1, 1 ,1 , holdTimer / (timeToHold - 1));
            skipCredsRef.fillAmount = holdTimer / timeToHold;
            if (holdTimer > timeToHold)
            {
                Debug.Log("SkippedCredits");
                hasSkipped = true;
            }
        }
        else
        {
            if (skipCredsRef.fillAmount >= 0)
            {
                holdTimer -= Time.deltaTime;
                skipCredsRef.fillAmount = holdTimer / timeToHold;
                holdText.color = new Color(1, 1, 1, holdTimer / (timeToHold - 1));
            }


        }
        if (hasSkipped == true)
        {
            StartCoroutine(SceneChange());
        }

    }
    void Fade()
    {
        fadeToBlackTimer += Time.deltaTime; 
        FadeToBlack.color = new Color(0, 0, 0, fadeToBlackTimer / timeToSwitch);
    }
    public IEnumerator SceneChange()
    {
        Fade();
        yield return new WaitForSeconds(.8f);
        SceneManager.LoadScene("MainMenu");
    }
}
