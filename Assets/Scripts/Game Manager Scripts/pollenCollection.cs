using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using FMODUnity;
using UnityEngine.SceneManagement;


public class pollenCollection : MonoBehaviour
{
    public static pollenCollection pollenCollectionRef;

    [Header("Assignables")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Image collectionTimer;
    [SerializeField] private List<GameObject> flowers;

    [Header("Statistics")]
    [SerializeField] private float timeToCollectPollen;
    [SerializeField] private float rangeToDetectFlower;
    public int pollenCollected;
    [SerializeField] public bool isCollecting;
    private float timer;

    //for lighting
    public DynamicLighting DL;
    //to finish day
    public GameObject hive;
    //set pollen quota for the day
    public int pollenQuota;
    //to switch animation for the pointer
    public Animator pointer;
    public PollenUI pUI;


    BeeAudioManager audioManager;
    

    private void Awake()
    {
        flowers = GameObject.FindGameObjectsWithTag("Flower").ToList();
        agent = GetComponent<NavMeshAgent>();
        audioManager = GetComponent<BeeAudioManager>();
        GetComponent<ParticleSystem>().Pause();

    }

    private void Update()
    {
        foreach (GameObject flower in flowers)
        {
            //detects if a flower is close enough to the player to collect pollen from it, if so do it
            if (Vector3.Distance(flower.transform.position, transform.position) <= rangeToDetectFlower && !isCollecting) StartCoroutine(collectPollen(flower));
        }

        

        //Detects if player is close to hive after collecting the pollen quota then resets the scene 
        if (Vector3.Distance(hive.transform.position, transform.position) <= rangeToDetectFlower * 4 && pollenCollected >= pollenQuota)
        {

            if (SceneManager.GetActiveScene().name != "Day7")
            {
                audioManager.SetBeeAudio(2);

                SceneManager.LoadScene("TransitionScene");
            } else
            {
                SceneManager.LoadScene("Credits");
            }
            

        }

        if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.K))
        {
            if (SceneManager.GetActiveScene().name != "Day7")
            {
                audioManager.SetBeeAudio(2);

                SceneManager.LoadScene("TransitionScene");
            }
            else
            {
                SceneManager.LoadScene("Credits");
            }
        }
        
    }

    public IEnumerator collectPollen(GameObject flower)
    {

        //Set the initial statistics to stop the player from moving until collecting is complete, 
        //and causes the isCollecting bool to be set to true so only one instance of this coroutine can be playing
        isCollecting = true;
        agent.isStopped = true;


        //Play animation of bee collecting pollen
        //Play particle effect of polen being collected

        //stop bee movementsounds
        audioManager.SetBeeAudio(2);
        //start collection sound
        audioManager.PlayCollectionSounds();

        GetComponent<ParticleSystem>().Play();

        //This just is a while loop to return null in order to increase the timer once per frame, for the ammount of time to collect pollen,
        //This also updates the collection timer graphic fill percentage
        while (timer < timeToCollectPollen)
        {
            timer += Time.deltaTime;

            if(collectionTimer != null) collectionTimer.fillAmount = timer / timeToCollectPollen;
            yield return null;
        }

        
        //Change time of day here


        
        //Stop Animation Of Bee collecting Pollen
        
        
        
        //Increase the count of pollen, sets the flower to be deactivated to remove from scene (optional),
        //and removes the flower from the list of available places to collect pollen 
        pollenCollected++;
        flowers.Remove(flower);

        //trigger UI
        pUI.FlowerCollected();

        //Stop particle effect of pollen being collected
        flower.GetComponent<ParticleSystem>().Stop();

        //Play sound for collecting pollen here
        RuntimeManager.PlayOneShot("event:/pollencollect");

        //Just some final statistic changes to set everything back to normal
        timer = 0;
        collectionTimer.fillAmount = 0;
        isCollecting = false;
        agent.isStopped = false;
        audioManager.StopCollectionSounds();
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Pause();

        //Starts the lighting cycle
        DL.cdBool = true;


    }
}
