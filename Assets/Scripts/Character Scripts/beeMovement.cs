using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class beeMovement : MonoBehaviour
{
    [Header("Addressables")]
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private NavMeshAgent agent;

    [Header("Statistics")]
    [SerializeField] private float cameraMoveDeadzoneRange = 1f;
    [SerializeField] private LayerMask layerToHit;

    private void Awake()
    {
        cameraHolder = this.gameObject;
        mainCam = Camera.main;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) findPath();

    }

    private void findPath()
    {
        //Creates a ray from the camera through the mouse position on the screen to the ground
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //This detects if anything is hit at all
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerToHit.value))
        {
            //This ensures that when you put the mouse over the bee, they arent permanently moving unless you are EXACTLY on top of them
            if (Vector3.Distance(hit.point, playerObj.transform.position) > cameraMoveDeadzoneRange)
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    //Debug drawing
    private void OnDrawGizmosSelected()
    {

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerToHit.value))
        {
            //This changes the color of the wire sphere bellow based on if the player is aiming within or outside of the deadzone set
            if (Vector3.Distance(hit.point, playerObj.transform.position) > cameraMoveDeadzoneRange) Gizmos.color = Color.green; 
            else Gizmos.color = Color.red;
        }

        //This will draw a wire sphere in the scene view around the players feet that shows if the players mouse is in the proper place 
        Gizmos.DrawWireSphere(playerObj.transform.position - Vector3.up, cameraMoveDeadzoneRange);
    }
}
