using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeUnhider : MonoBehaviour
{
    public GameObject currentHiddenObject;
    
    public Transform beeTransform;
    public Transform camTransform;

    private RaycastHit hit;
    private Vector3 dir;

    private float dist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dist = (camTransform.position - beeTransform.position).magnitude;
        dir = (camTransform.position - beeTransform.position).normalized;

        if(Physics.Raycast(beeTransform.position, dir, out hit, dist))
        {
            if(hit.collider.gameObject != currentHiddenObject)
            {
                ChangeHiddenObject(hit.collider.gameObject);
            }
            
        }
    }

    public void ChangeHiddenObject(GameObject hitObj)
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(beeTransform.position, dir * dist);

    }
}
