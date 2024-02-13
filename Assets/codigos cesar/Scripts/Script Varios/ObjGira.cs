using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
public class ObjGira : MonoBehaviour
{
    public GameObject v_Panel;

    void Update()
    {
        /*Vector3 v_Dir = Player.instance.transform.position - v_Panel.transform.position;//CALCULOS DE ROTACION
        v_Dir.y = 0;// NO ROTARLO VERTICAL
                    //v_Dir.z = 0;
        Quaternion NuevaRot = Quaternion.LookRotation(v_Dir);
        v_Panel.transform.rotation = Quaternion.Slerp(v_Panel.transform.rotation, NuevaRot, Time.deltaTime * 5.0f);//ROTAR EL CAÑON*/
    }
}
