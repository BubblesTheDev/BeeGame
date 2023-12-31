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

    private bool secret;

    private void Start()
    {
        BeeSpawn();
        Invoke("BeeSpawn", Random.Range(0f, 2f));
        Invoke("BeeSpawn", Random.Range(0f, 2f));
        Invoke("BeeSpawn", Random.Range(0f, 2f));
        Invoke("BeeSpawn", Random.Range(0f, 2f));
        Invoke("BeeSpawn", 1);

        secret = false;
    }

    private void Update()
    {
        if(Input.GetKey("y") && Input.GetKey("u") && Input.GetKey("i"))
        {
            secret = true;
        }
    }

    public void BeeSpawn()
    {
        GameObject newBee = Instantiate(beePrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, transform.rotation);

        if (secret)
        {
            float randomSize = Random.Range(0.1f, 7f);

            newBee.transform.localScale *= randomSize;
            newBee.transform.GetChild(1).transform.localScale *= randomSize;
        }

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
}
