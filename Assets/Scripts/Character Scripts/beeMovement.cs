using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;
using FMODUnity;
using FMOD.Studio;

public class beeMovement : MonoBehaviour
{
    [Header("Addressables")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject movementCursor;
    [SerializeField] private GameObject placedCursor;

    [Header("Statistics")]
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private float distanceToMoveCursor;
    private RaycastHit hit;
    private Ray mouseAimRay;

    EventInstance movementBuzz;
    EventInstance idleBuzz;
    bool isIdling = true;
    bool soundPlayed = false;

    private void Awake()
    {
        mainCam = Camera.main;
        agent = GetComponent<NavMeshAgent>();

        //set fmod event instances and start idle sound
        movementBuzz = RuntimeManager.CreateInstance("event:/MovementBuzz");
        idleBuzz = RuntimeManager.CreateInstance("event:/idleBuzz");
        idleBuzz.start();
        movementBuzz.start();
    }

    private void Update()
    {
        //Creates a ray from the camera through the mouse position on the screen to the ground
        mouseAimRay = mainCam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(mouseAimRay, out hit, Mathf.Infinity, layerToHit.value);

        //Adding a coding failsafe
        if (movementCursor == null)
        {
            Debug.LogWarning("There is no object assigned in the: Movement Cursor variable within the inspector. Please assign it and try again");
            return;
        }
        else
        {
            //This just moves the movement cursor to where the player is aiming
            if (hit.point != null) movementCursor.transform.position = hit.point;
        }



        if (Input.GetMouseButtonDown(0))
        {
            findPath();
            placeMovementCursor();

            //Start Movement Sound
            isIdling = false;

        }

        if (Vector3.Distance(gameObject.transform.position, agent.destination) < distanceToMoveCursor)
        {
            Debug.Log("Reached Destination");
            removePlacedCursor();

            //Start idle sound
            isIdling = true;
            
            
        }

        setBuzzAudio();


    }

    private void findPath()
    {

        //This detects if anything is hit at all
        if (hit.point != null)
        {
            agent.SetDestination(hit.point);
        }
    }



    private void placeMovementCursor()
    {
        //Adding a coding failsafe
        if (placedCursor == null)
        {
            Debug.LogWarning("There is no object assigned in the: Placed Cursor variable OR the placed particles array is empty\nwithin the inspector. Please assign it and try again");
            return;
        }
        else
        {
            placedCursor.transform.position = hit.point;
        }
    }


    //This will be called to remove the placed cursor once hte player is on top of it,
    //Not removing it from the scene, but just moving it to an object pool to save on loading
    private void removePlacedCursor()
    {
        //Adding a coding failsafe
        if (placedCursor == null)
        {
            Debug.LogWarning("There is no object assigned in the: Placed Cursor variable OR the placed particles array is empty\nwithin the inspector. Please assign it and try again");
            return;
        }
        else
        {
            placedCursor.transform.position = Vector3.one * 100;
        }
    }
    private void setBuzzAudio()
    {
        if (!soundPlayed)
        {
            
            if (!isIdling)
            {
                movementBuzz.setPaused(false);
                idleBuzz.setPaused(true);
                
            }
            else
            {

                movementBuzz.setPaused(true);
                idleBuzz.setPaused(false);

            }
        }
    }
}
