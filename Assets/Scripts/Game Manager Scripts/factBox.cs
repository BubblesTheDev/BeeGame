using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class factBox : MonoBehaviour
{
    [Header("Fact Information")]
    public int factToAccessId;
    public List<int> prerequisiteFacts = new List<int>();

    [Header("Dialogue Box Information")]
    [SerializeField] private GameObject factBoxPrefab;
    [SerializeField] private float typingSpeed;
    [SerializeField] private bool hasSpawned;
    private GameObject tempObj;
    private dialogueDatabase factDatabase;

    private void Awake()
    {
        factDatabase = GameObject.Find("DialogueDatabase").GetComponent<dialogueDatabase>();
    }

    private void Update()
    {
        if (hasSpawned)
        {
            tempObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    public void spawnBox()
    {
        hasSpawned = true;

        tempObj = Instantiate(factBoxPrefab, GameObject.Find("Bee Character").transform.position, Quaternion.identity, GameObject.Find("Bee Character").transform);
        
        factDatabase.collectFact(factToAccessId);

        //Play spawn dialogue box sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx_dialoguepopup");

        //Start dialogue box animation

        tempObj.transform.Find("DialogueText").gameObject.GetComponent<Text>().text += factDatabase.facts.Find(x => x.factID == factToAccessId).factDialogue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSpawned)
        {
            foreach (int factToCheck in prerequisiteFacts)
            {
                if(factDatabase.facts.Where(x => x.factID == factToCheck).FirstOrDefault().factCollected == false)
                {
                    return;
                }

            }

            spawnBox();
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && hasSpawned)
        {
            StopAllCoroutines();
            Destroy(tempObj);
            hasSpawned = false;
        }
    }
}
