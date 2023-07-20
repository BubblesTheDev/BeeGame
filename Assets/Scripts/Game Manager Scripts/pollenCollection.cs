using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    [SerializeField] private bool isCollecting;
    private float timer;

    private void Awake()
    {
        flowers = GameObject.FindGameObjectsWithTag("Flower").ToList();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        foreach  (GameObject flower in flowers)
        {
            //detects if a flower is close enough to the player to collect pollen from it, if so do it
            if (Vector3.Distance(flower.transform.position, transform.position) <= rangeToDetectFlower && !isCollecting) StartCoroutine(collectPollen(flower));
        }
    }

    public IEnumerator collectPollen(GameObject flower)
    {

        //Set the initial statistics to stop the player from moving until collecting is complete, 
        //and causes the isCollecting bool to be set to true so only one instance of this coroutine can be playing
        isCollecting = true;
        agent.speed = 0;


        //Play animation of bee collecting pollen
        //Play particle effect of polen being collected


        //This just is a while loop to return null in order to increase the timer once per frame, for the ammount of time to collect pollen,
        //This also updates the collection timer graphic fill percentage
        while(timer < timeToCollectPollen)
        {
            timer += Time.deltaTime;

            if(collectionTimer != null) collectionTimer.fillAmount = timer / timeToCollectPollen;
            yield return null;
        }

        //Play sound for collecting pollen here
        //Change time of day here


        //Stop particle effect of pollen being collected
        //Stop Animation Of Bee collecting Pollen
        
        
        
        //Increase the count of pollen, sets the flower to be deactivated to remove from scene (optional),
        //and removes the flower from the list of available places to collect pollen 
        pollenCollected++;
        flower.SetActive(false);
        flowers.Remove(flower);

        //Just some final statistic changes to set everything back to normal
        timer = 0;
        collectionTimer.fillAmount = 0;
        isCollecting = false;
        agent.speed = 10;
    }
}
