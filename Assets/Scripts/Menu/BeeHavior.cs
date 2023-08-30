using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHavior : MonoBehaviour
{
    public MenuBeeManager _mbm;
    public Rigidbody _rb;
    public Animator _an;
    public Transform flowerPoint;
    public Transform exitPoint;
    private GameObject particles;

    public float speed;
    public int index;

    public bool playButtonBee;
    private bool moving;
    private bool leaving;
    private float distanceFromFlower;
    private float distanceFromExit;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
        _an = this.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Animator>();
        particles = transform.GetChild(1).gameObject;
        particles.SetActive(false);

        if (!playButtonBee)
        {
            speed = Random.Range(2f, 6f);
            Invoke("GlitchFix", 50);
            
        }
        else
        {
            speed = 6f;
            this.gameObject.layer = 12;
            this.transform.GetChild(0).transform.GetChild(0).gameObject.layer = 12;
        }

        transform.LookAt(flowerPoint);

        moving = true;
        leaving = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromFlower = Vector3.Distance(transform.position, flowerPoint.position);
        distanceFromExit = Vector3.Distance(transform.position, exitPoint.position);

        if (distanceFromExit > 30 && playButtonBee)
        {
            ExitAndRespawn();
        }
  
        if (distanceFromFlower <= 0.05f && distanceFromFlower >= -0.05f && !leaving && moving)
        {
            moving = false;
            Invoke("RotateToFlower", 0.2f);

            if (!playButtonBee)
            {
                Invoke("TurnToExit", 2);
            }
        }

        if (leaving && distanceFromExit <= 0.05f && distanceFromExit >= -0.05f)
        {
            ExitAndRespawn();
        }

        if (moving)
        {
            _rb.velocity = transform.forward * speed;
        }
        else
        {
            _rb.velocity = new Vector3(0, 0, 0);
        }

        _an.SetBool("moving", moving);
    }

    public void RotateToFlower()
    {
        particles.SetActive(true);

        transform.rotation = flowerPoint.rotation;
    }

    public void TurnToExit()
    {
        transform.LookAt(exitPoint);
        leaving = true;
        moving = true;
        particles.SetActive(false);

        if (!playButtonBee)
        {
            Invoke("GlitchFix", 50);
        }
    }

    public void ExitAndRespawn()
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

    public void GlitchFix()
    {
        ExitAndRespawn();
    }
}
