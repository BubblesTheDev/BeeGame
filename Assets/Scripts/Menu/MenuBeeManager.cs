using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBeeManager : MonoBehaviour
{
    public GameObject beePrefab;

    public List<Transform> spawnPoints;
    public List<Transform> flowerPoints;
    public List<Transform> exitPoints;

    public List<Transform> playButtonPoints;

    public List<GameObject> beeSpawnInScene;

    public List<GameObject> playSwarmBees;

    private void Start()
    {
        BeeSpawn();
        Invoke("BeeSpawn", Random.Range(0f, 2f));
        Invoke("BeeSpawn", 1);
    }

    public void BeeSpawn()
    {
        GameObject newBee = Instantiate(beePrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, transform.rotation);

        BeeHavior _bh = newBee.GetComponent<BeeHavior>();
        _bh.exitPoint = exitPoints[Random.Range(0, exitPoints.Count)];

        while (true)
        {
            Transform newFlowerPoint = flowerPoints[Random.Range(0, flowerPoints.Count)];

            bool useable = true;

            for(int i = 0; i < beeSpawnInScene.Count; i++)
            {
                if(newFlowerPoint == beeSpawnInScene[i].GetComponent<BeeHavior>().flowerPoint)
                {
                    useable = false;
                }
            }

            if (useable)
            {
                _bh.flowerPoint = newFlowerPoint;
                break;
            }
        }

        _bh._mbm = this;

        _bh.index = beeSpawnInScene.Count;
        beeSpawnInScene.Add(newBee);
    }
    /*
    public void PlayButtonSwarm()
    {
        if(playSwarmBees.Count != 0)
        {
            for(int e = 0; e < playSwarmBees.Count; e++)
            {
                playSwarmBees.RemoveAt(e);
            }
        }

        for (int i = 0; i < 5; i++)
        {
                GameObject newBee = Instantiate(beePrefab, spawnPoints[i].transform.position, transform.rotation);
                BeeHavior _bh = newBee.GetComponent<BeeHavior>();

                _bh.playButtonBee = true;
                _bh.flowerPoint = playButtonPoints[i];
                _bh.exitPoint = spawnPoints[i];
                _bh._mbm = this;

                _bh.index = i;
                playSwarmBees.Add(newBee);
            print("bruh");
        }
    }

    public void StopPlayButtonSwarm()
    {
        for (int i = 0; i < 5; i++)
        {
            playSwarmBees[i].GetComponent<BeeHavior>().TurnToExit();
        }
    }
    */
}
