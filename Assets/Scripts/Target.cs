using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public Image imageTarget;
    public Transform cameraTransform;

    private void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        // Cast a ray from the camera center to check if it hits a specific object
        //Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit))
        {
            // Check if the hit object is the specific object you are looking for
            if (hit.transform.CompareTag("robber"))
            {
                // Change the color of the Image target to the highlight color
                imageTarget.color = Color.red;
            }
            else
            {
                // Reset the color of the Image target to its original color
                imageTarget.color = Color.white; // or any other default color you want
            }
        }
    }
 }
