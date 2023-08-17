using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class dialogueDatabase : MonoBehaviour
{
    [Header("Bee Fact Information")]
    public List<fact> facts = new List<fact>();

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1)) logCollectedFacts();
    }

    public void collectFact(int factToSetCollected)
    {
        facts.Find(x => x.factID == factToSetCollected).factCollected = true;
    }

    private void logCollectedFacts()
    {
        string loggedIds = new string("Logged ID's Include: ");

        foreach (fact factToCheck in facts)
        {
            if (factToCheck.factCollected) loggedIds += factToCheck.factID.ToString() + ", ";
        }

        Debug.Log(loggedIds);
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
