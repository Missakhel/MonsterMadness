using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActiveGrabRay : MonoBehaviour
{
    public GameObject leftGrabRay;
    public GameObject rightGrabRay;

    public XRDirectInteractor leftDirectGrab;
    public XRDirectInteractor rightDirectGrab;

    void Start()
    {

        
    }

    void Update()
    {
        leftGrabRay.SetActive(leftDirectGrab.interactablesSelected.Count == 0);
        rightGrabRay.SetActive(rightDirectGrab.interactablesSelected.Count == 0);


    }

}
