using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLighting : MonoBehaviour
{

    public GameObject Sun;
    public float rotationSpeed = 10;
    float cdTimer;
    public float cdDuration;
    public bool cdBool = false;
    int pollenCount = 0;
    public bool nightTime = false;
    public GameObject nL;

    private Color camTargetColor;
    public Color camSunsetColor;
    public Color camNightColor;
    private Color camCurrentColor;
    float transitionDuration;
    bool stopCol = false;

    private Color sunTargetColor;
    public Color sunSunsetColor;
    public Color sunNightColor;
    private Color sunCurrentColor;

    private Camera mainCamera;
    private Color camInitialColor;
    private float timer;

    private Light directionalLight;
    private Color sunInitialColor;

    public Material nightMat;

    public bool collectDebug;
    public pollenCollection pc;

    private dialogueDatabase database;
    [Space]
    public int[] startOfDayDialogueIDs = new int[0];
    public int[] MiddleOfDialogueIDs = new int[0];
    public int[] EndOfDayDialogueIDs = new int[0];

    private bool MiddleOfDayPlayed;
    private bool EndOfDayPlayed;

    // Start is called before the first frame update
    void Start()
    {

        camTargetColor = camSunsetColor;
        sunTargetColor = sunSunsetColor;

        nL.SetActive (false);

        mainCamera = Camera.main;
        directionalLight = Sun.GetComponent<Light>();

        sunInitialColor = directionalLight.color;
        camInitialColor = mainCamera.backgroundColor;

        camCurrentColor = camInitialColor;
        sunCurrentColor = sunInitialColor;

        timer = 0f;

        database = GameObject.Find("DialogueDatabase").GetComponent<dialogueDatabase>();

        StartCoroutine(timeBeforeFirstFacts());
    }

    // Update is called once per frame
    void Update()
    {

        pollenCount = pc.pollenCollected;

        transitionDuration = 50 / rotationSpeed;

        if (cdBool == true)
        {
            cdTimer += Time.deltaTime;
        }


        if (cdTimer < cdDuration*pollenCount && cdBool == true)
        {

            //Rotate the sunlight
            Sun.transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));


            //color math
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / transitionDuration);



            //Sunlight colour change
            Color sunNewColor = Color.Lerp(sunCurrentColor, sunTargetColor, t);
            directionalLight.color = sunNewColor;


            //Background colour change

            Color camNewColor = Color.Lerp(camCurrentColor, camTargetColor, t);
            mainCamera.backgroundColor = camNewColor;

            if (timer >= transitionDuration && stopCol == false)
            {
                timer = 0f;
                camCurrentColor = camSunsetColor;
                camTargetColor = camNightColor;
                sunCurrentColor = sunSunsetColor;
                sunTargetColor = sunNightColor;
                stopCol = true;


                if(MiddleOfDayPlayed == false)
                {
                    StartCoroutine(database.daytimeFact(MiddleOfDialogueIDs));
                    MiddleOfDayPlayed = true;
                }
            }





        } else
        {
            cdBool = false;
            cdTimer = 0;
            pollenCount = 0;
        }

        if (Sun.transform.rotation.y < 0f)
        {
            nightTime = true;
            nL.SetActive(true);

            if (EndOfDayPlayed == false)
            {
                StartCoroutine(timeBeforeEndFacts());
                EndOfDayPlayed = true;
            }

            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("nightLighting");

            // Change the material of each tagged GameObject
            foreach (GameObject taggedObject in taggedObjects)
            {
                Renderer renderer = taggedObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = nightMat;
                }
            }




        }



        //Debug.Log(Sun.transform.rotation.y);

    }

    public IEnumerator timeBeforeFirstFacts()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        StartCoroutine(database.daytimeFact(startOfDayDialogueIDs));
    }

    public IEnumerator timeBeforeEndFacts()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        StartCoroutine(database.daytimeFact(EndOfDayDialogueIDs));
    }
}
