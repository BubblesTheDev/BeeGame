using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAnimation : MonoBehaviour
{
    private Animator _an;

    
    // Start is called before the first frame update
    void Start()
    {
        _an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RussleFlower()
    {
        _an.SetTrigger("ruffle");
    }
}
