using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.AI;
using UnityEngine.AI;
using UnityEngine.UI;

public class dialogueDatabase : MonoBehaviour
{
    [Header("Bee Fact Information")]
    public List<fact> facts = new List<fact>();
    public float timeForEachText = 1.5f;
    public GameObject factBoxPrefab;


    private GameObject player;
    private NavMeshAgent agent;
    private pollenCollection pollenScript;
    private bool factIsShown;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        player = GameObject.Find("Bee Character");
        agent = player.GetComponent<NavMeshAgent>();
        pollenScript = player.GetComponent<pollenCollection>();
    }

    private void Update()
    {
        if (factIsShown && agent.isStopped == false) agent.isStopped = true;
    }

    public void collectFact(int factToSetCollected)
    {
        facts.Find(x => x.factID == factToSetCollected).factCollected = true;
    }

    public IEnumerator daytimeFact(int[] daytimeFactIDs)
    {
        agent.isStopped = true;
        factIsShown = true;

        if(player.transform.Find("DialogueBox(1)(Clone)")) player.transform.Find("DialogueBox(1)(Clone)").gameObject.SetActive(false);

        GameObject tempObj = Instantiate(factBoxPrefab, GameObject.Find("Bee Character").transform.position, Quaternion.identity, GameObject.Find("Bee Character").transform);
        tempObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        for (int i = 0; i < daytimeFactIDs.Length; i++)
        {
            tempObj.transform.Find("DialogueText").gameObject.GetComponent<Text>().text = facts.Find(x => x.factID == daytimeFactIDs[i]).factDialogue;

            collectFact(daytimeFactIDs[i]);

            //Play spawn dialogue box sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/sfx_dialoguepopup");
            tempObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            if (i != daytimeFactIDs.Length) yield return new WaitForSeconds(timeForEachText);
        }
        yield return new WaitForSeconds(timeForEachText);


        if (player.transform.Find("DialogueBox(1)(Clone)")) player.transform.Find("DialogueBox(1)(Clone)").gameObject.SetActive(true);
        Destroy(tempObj);
        if(!pollenScript.isCollecting) agent.isStopped = false;
        factIsShown = false;

    }
}

[System.Serializable]
public class fact
{
    public int factID;
    public bool factCollected;
    [TextArea(3, 6)]
    public string factDialogue;
}
