using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemigos
{
    using Ventanas;
    using Jugador;
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class Hit_Mano : MonoBehaviour
    {
        /// <summary>
        /// DAÑOS QUE HACE
        /// </summary>
        public float v_Dano;
        /// <summary>
        /// PARA SABER QUIEN ESTA MANDANDO DAÑOS A CIERTOS OBJETOS
        /// </summary>
        public GameObject v_padre;
        /// <summary>
        /// ESTADO, SOLO HACER DAÑO CUANDO ES TRUE
        /// </summary>
        public bool v_golpeando = false;
        /// <summary>
        /// RIGIDBODY SOLO DE LA MANO
        /// </summary>
        Rigidbody v_rig;
        private void Awake()
        {
            v_rig = GetComponent<Rigidbody>();
            v_rig.useGravity = false;
            v_rig.isKinematic = true;
        }
        public void FnSetDano(float _dano, GameObject _pa)
        {
            v_Dano = _dano;
            v_padre = _pa;
        }
        //para direccionar el daño al objeto padre
        public void Dano(float _dano)
        {
            if (v_padre != null && v_padre.gameObject.activeInHierarchy)
            {

                v_padre.SendMessage("Dano", _dano, SendMessageOptions.DontRequireReceiver);
            }
        }
        public void Dano(GameObject _aseguir)
        {
            if (v_padre != null && v_padre.gameObject.activeInHierarchy)
            {
                v_padre.SendMessage("Dano", _aseguir, SendMessageOptions.DontRequireReceiver);
            }
        }
        /// <summary>
        /// ACTIVAR/DESACTIVAR 
        /// </summary>
        public void Fn_SetGolpe(bool _gol)
        {
            v_golpeando = _gol;
            if (_gol)
            {
                v_rig.useGravity = true;
                v_rig.isKinematic = false;
            }
            else
            {
                v_rig.useGravity = false;
                v_rig.isKinematic = true;
            }
        }
        public GameObject Fn_GetPadre()
        {
            return v_padre;
        }
        private void OnCollisionEnter(Collision collision)
        {
            //Debug.LogError(collision.transform.name);
            if (collision.transform.tag == "Hit" && v_golpeando)
            {
                collision.transform.SendMessage("Fn_SetDano", v_Dano, SendMessageOptions.DontRequireReceiver);
                collision.transform.SendMessageUpwards("Fn_SetDano", v_Dano, SendMessageOptions.DontRequireReceiver);
                //v_padre.GetComponent<Audio.Au_Manager>().Fn_SetAudio(2, true, true);
                v_golpeando = false;
            }
            else if (collision.transform.tag == "Ventana" && v_golpeando)
            {
                v_golpeando = false;
                collision.transform.GetComponent<Ventana>().Fn_RecibeDano(v_Dano, v_padre);
                if (collision.transform.GetComponent<Ventana>().Fn_GRota())
                {
                    //gameObject.SendMessageUpwards("Fn_Rompio");
                    //v_padre.SendMessage("Fn_Rompio");
                    v_golpeando = false;
                }
            }
            else if (collision.transform.tag == "Player" && v_golpeando)
            {
                v_golpeando = false;
                // Jug_Datos.Instance.fn(v_almas);
                Jug_Datos.Instance.Fn_Dano(v_Dano);
            }
            /* else if(collision.transform.tag=="Torreta" && v_golpeando)
             {
                 print("dano a toreta");
                 if (collision.transform.GetComponent<Item_Torreta>())
                 {
                     collision.transform.GetComponent<Item_Torreta>().Fn_SetDano(v_Dano);
                 }
                 else if (collision.transform.GetComponentInParent<Item_Torreta>())
                 {
                     collision.transform.GetComponentInParent<Item_Torreta>().Fn_SetDano(v_Dano);
                 }
                 if (collision.transform.GetComponent<Item_Obstaculo>())
                 {
                     print("dano en  obst");
                     collision.transform.GetComponent<Item_Obstaculo>().Fn_SetDano(v_Dano);
                 }
                 else if (collision.transform.GetComponentInParent<Item_Obstaculo>())
                 {
                     print("dano en padre obst");
                     collision.transform.GetComponentInParent<Item_Obstaculo>().Fn_SetDano(v_Dano);
                 }
             }*/
        }
    }
}