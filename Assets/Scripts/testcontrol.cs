using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.vrheads.com/valve-knuckles-controllers
//https://www.raywenderlich.com/149239/htc-vive-tutorial-unity  
//https://forums.unrealengine.com/filedata/fetch?id=1111783&d=1460020388
public class testcontrol : MonoBehaviour {



    //private SteamVR_TrackedObject trackobj;
    /*private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackobj.index);
        }

    }*/
    private void Awake()
    {
        //trackobj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update () {
        // 1
        
        /*if (Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }

        // 2
        if (Controller.GetHairTriggerDown())
        {
            Debug.Log(gameObject.name + " Trigger Press");
        }

        // 3
        if (Controller.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + " Trigger Release");
        }

        // 4
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Press");
        }

        // 5
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Release");
        }
        //touchpad ya te da un vector2, pos vec2 = touchpad va de -1 1 en los ejes
        if(Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {

        }*/
    }
}
