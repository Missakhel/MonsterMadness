using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
namespace Jugador
{
    [RequireComponent(typeof(CapsuleCollider), typeof(UnityEngine.AI.NavMeshObstacle))]
    public class Jug_Cuerpo : MonoBehaviour
    {
        public GameObject v_camara;
        public GameObject Fn_GetObj()
        { return gameObject; }
        void OnEnable()
        {
            /*if (SteamVR.instance != null)
            {
                v_camara = Player.instance.rigSteamVR.GetComponentInChildren<VR_BlackScreen>().gameObject;
            }
            else
            {
                v_camara = Player.instance.rig2DFallback.GetComponentInChildren<VR_BlackScreen>().gameObject;
            }*/
        }
        protected void Update()
        {
            if (v_camara != null)
            {
                transform.position = new Vector3(v_camara.transform.position.x, transform.position.y, v_camara.transform.position.z);
            }
        }
    }
}