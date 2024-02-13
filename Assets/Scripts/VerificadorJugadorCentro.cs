using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VR = UnityEngine;

public class VerificadorJugadorCentro : MonoBehaviour
{
    float LimiteX = 1.5f;
    Vector3 centerPos;

    public GameObject go_RegresarWarning;

    public Transform MainCameraTransform;

    void Start()
    {
        centerPos = MainCameraTransform.position;
        //VR.InputTracking.Recenter();
    }

    void LateUpdate()
    {
        //Si se presiona C, reseteamos centro del mundo
        if(Input.GetKeyDown(KeyCode.C))
        {
            Vector3 tmp = centerPos;
            tmp.y = MainCameraTransform.localPosition.y;
            tmp.x = 0.0f;
            tmp.z = 0.0f;
            MainCameraTransform.localPosition = tmp;
            return;
        }


        float dif = Mathf.Abs(MainCameraTransform.localPosition.x);
        if (dif > LimiteX)
        {
            //print("Entra en X");
            go_RegresarWarning.SetActive(true);
            print("Fuera de area X " + dif);
            return;
        }
        //print("Dentro area " + dif);
        go_RegresarWarning.SetActive(false);
    }

    //Version 1,, implica acomodar los sensores y la silla por usuario
    /*void Update ()
    {
        if (!OVRManager.tracker.isPresent || Time.time < waitTime)
        {
            return;
        }

        // TODO - probably don't have to do this every frame!
        ComputePlanes();

        OVRPose trackerPose = OVRManager.tracker.GetPose();
        Matrix4x4 trackerMat = Matrix4x4.TRS(trackerPose.position, trackerPose.orientation, Vector3.one);

        // Transform point into volume space
        OVRPose headPose;
        headPose.position = VR.InputTracking.GetLocalPosition(VR.VRNode.Head);

        Vector3 localPos = trackerMat.inverse.MultiplyPoint(headPose.position);

        go_RegresarWarning.SetActive(Mathf.Abs(localPos.x) > LimiteX || Mathf.Abs(localPos.y) > LimiteY);        
    }

    void ComputePlanes()
    {
        OVRTracker.Frustum frustum = OVRManager.tracker.GetFrustum();
        float nearZ = frustum.nearZ;
        float farZ = frustum.farZ;
        float hFOV = Mathf.Deg2Rad * frustum.fov.x * 0.5f;
        float vFOV = Mathf.Deg2Rad * frustum.fov.y * 0.5f;
        float sx = Mathf.Sin(hFOV);
        float sy = Mathf.Sin(vFOV);

        plane[0] = new Plane(Vector3.zero, farZ * new Vector3(sx, sy, 1f), farZ * new Vector3(sx, -sy, 1f));    // right
        plane[1] = new Plane(Vector3.zero, farZ * new Vector3(-sx, -sy, 1f), farZ * new Vector3(-sx, sy, 1f));  // left
        plane[2] = new Plane(Vector3.zero, farZ * new Vector3(-sx, sy, 1f), farZ * new Vector3(sx, sy, 1f));    // top
        plane[3] = new Plane(Vector3.zero, farZ * new Vector3(sx, -sy, 1f), farZ * new Vector3(-sx, -sy, 1f));  // bottom
        plane[4] = new Plane(farZ * new Vector3(sx, sy, 1f), farZ * new Vector3(-sx, sy, 1f), farZ * new Vector3(-sx, -sy, 1f));        // far
        plane[5] = new Plane(nearZ * new Vector3(-sx, -sy, 1f), nearZ * new Vector3(-sx, sy, 1f), nearZ * new Vector3(sx, sy, 1f));	// near
    }*/
}
