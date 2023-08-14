using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeUnhider : MonoBehaviour
{
    private GameObject currentHiddenObject;
    private GameObject previousHiddenObject;
    
    public Transform beeTransform;
    public Transform camTransform;

    private RaycastHit hit;
    private Vector3 dir;

    public LayerMask layerMask;

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

        if(Physics.Raycast(beeTransform.position, dir, out hit, dist, layerMask))
        {
            if(hit.collider.gameObject != currentHiddenObject)
            {
                ChangeHiddenObject(hit.collider.gameObject);
            }
            
        }
        else if(currentHiddenObject != null)
        {
            currentHiddenObject.transform.GetChild(0).gameObject.SetActive(true);
            currentHiddenObject.transform.GetChild(1).gameObject.SetActive(false);
            currentHiddenObject = null;
        }
    }

    public void ChangeHiddenObject(GameObject hitObj)
    {
        currentHiddenObject = hitObj;
        currentHiddenObject.transform.GetChild(1).gameObject.SetActive(true);
        currentHiddenObject.transform.GetChild(0).gameObject.SetActive(false);

        if(previousHiddenObject != null)
        {
            if (previousHiddenObject != currentHiddenObject || currentHiddenObject == null)
            {
                previousHiddenObject.transform.GetChild(0).gameObject.SetActive(true);
                previousHiddenObject.transform.GetChild(1).gameObject.SetActive(false);
                previousHiddenObject = currentHiddenObject;
            }
        }

        previousHiddenObject = currentHiddenObject;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(beeTransform.position, dir * dist);

    }
}
