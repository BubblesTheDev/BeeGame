using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem _particleSystem;
    
    private GameObject Target;
    private Transform targetTransform;
    
   

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        Target = GameObject.FindGameObjectWithTag("Player");
        targetTransform = Target.transform;
    }


    void LateUpdate()
    {
        if (_particleSystem.isPlaying)
        {
            
            var emission = _particleSystem.emission;
            var main = _particleSystem.main;
            

            if (Vector3.Distance(targetTransform.position, transform.position) <= 10)
            {
                
                emission.rateOverTime = 7.5f;
                //main.startSizeMultiplier = 1.4f;
            }
            else 
            {

                emission.rateOverTime = 6;
                
            }
            
        }
    }
}
