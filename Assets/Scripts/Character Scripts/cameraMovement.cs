using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [Header("Addressables")]
    [SerializeField] private GameObject playerObj;
    [SerializeField] private Camera mainCam;

    [Header("Variables")]
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private float cameraSmoothing;
    [SerializeField] private float cameraRangeLimit;
    private Vector3 currentCamVelocity = Vector3.zero;
    private Vector3 targetPos;


    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        mainCam = Camera.main;
    }


    //Note: We place this function inside the late update so that it happens AFTER the movement calculation and rotation, helps with unessicary jitters
    private void LateUpdate()
    {
        moveCamera();
    }

    void moveCamera()
    {
        //This is creating a ray from the main camera to a point within the world
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, layerToHit.value);

        //This spesific part is setting hte target position of the camera to be the player body, plus a direction toward where the camera is aiming,
        //and then a distance to multiply the direction to give it a spot to put the camera
        targetPos = playerObj.transform.position 

            //This creates the direction from where the mouse is aiming and where the players body currently is at 
            + ((hit.point - playerObj.transform.position).normalized) 

            //This then takes that direction and multiplies it by a magnitude that is clamped by the camera range limit minimum and maxium, 
            //note: the magnitude is also divided by two, as we want the camera to be inbetween where the mouse is and where the player is
            * Mathf.Clamp((hit.point - playerObj.transform.position).magnitude / 2, -cameraRangeLimit, cameraRangeLimit);
        
        //Making sure hte camera always stays at the correct height
        targetPos.y = 0;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentCamVelocity, cameraSmoothing);
    }
    //Debug drawing
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(targetPos, 1f);
    }
}
