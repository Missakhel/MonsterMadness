using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ventanas
{
    [RequireComponent(typeof(BoxCollider))]
    public class Muro : MonoBehaviour {
        //public Transform v_medio;
        private void Awake()
        {
           // gameObject.tag = "Ventana";
            gameObject.layer = 13;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                //print("salto");
                other.SendMessage("Fn_Saltar", true);
               // other.SendMessage("Fn_Saltar", v_medio);
            }
        }
    }
}