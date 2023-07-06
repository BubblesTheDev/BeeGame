using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHavior : MonoBehaviour
{
    public MenuBeeManager _mbm;
    public Transform flowerPoint;
    public Transform exitPoint;
    public Rigidbody _rb;

    public float speed;
    public int index;

    private bool moving;
    private bool leaving;
    private float distanceFromFlower;
    private float distanceFromExit;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
        
        speed = Random.Range(0.3f, 2f);

        transform.LookAt(flowerPoint);

        moving = true;
        leaving = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromFlower = Vector3.Distance(transform.position, flowerPoint.position);
        distanceFromExit = Vector3.Distance(transform.position, exitPoint.position);

        if (distanceFromFlower <= 0.05f && distanceFromFlower >= -0.05f && !leaving)
        {
            moving = false;
            Invoke("TurnToExit", 1);
        }

        if (leaving && distanceFromExit <= 0.01f)
        {
            for (int i = 0; i < _mbm.beeSpawnInScene.Count; i++)
            {
                if (_mbm.beeSpawnInScene[i] == this.gameObject)
                {
                    _mbm.beeSpawnInScene.RemoveAt(i);
                }
            }

            _mbm.BeeSpawn();
            Destroy(this.gameObject);
        }

        if (moving)
        {
            _rb.velocity = transform.forward * speed;
        }
        else
        {
            _rb.velocity = new Vector3(0, 0, 0);
        }
    }

    public void TurnToExit()
    {
        transform.LookAt(exitPoint);
        leaving = true;
        moving = true;
    }
}
