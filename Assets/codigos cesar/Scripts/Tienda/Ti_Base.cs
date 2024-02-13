using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Tienda
{
    using Jugador;
    //using Valve.VR.InteractionSystem;
    //[RequireComponent(typeof(Interactable), typeof(BoxCollider))]
    public class Ti_Base : MonoBehaviour {
        [Header("Elementos basicos")]
        public Material _puede;
        public Material _NoPuede;
        public Material[] _normal;
        public MeshRenderer[] v_mesh;
        public int v_costo = 0;
        public bool v_puede = true;
        //protected Hand v_hand= null;
        public Text text_costo;  //Referencia a cuanto cuesta
        public Text text_Creditos;  //Referencia a cuanto cuesta
        public Color v_color;
        /// <summary>
        /// el costo de comprarlo, ya hace los materiales
        /// </summary>
        public virtual void Fn_Config(int _costo)
        {
            v_costo = _costo;
            v_puede = true;
            _normal = new Material[v_mesh.Length];
            for (int i = 0; i<_normal.Length; i++)
            {
                _normal[i] = v_mesh[i].material;
            }
            if(text_costo== null)
            {
                Debug.Log("FALTA EL TEXTO EN " + gameObject.name);
                Debug.Break();
            }

            GetComponentInParent<Audio.Au_Manager>().Fn_Inicializa();
            text_costo.text = v_costo.ToString();
            text_costo.gameObject.SetActive(false);
            text_Creditos.gameObject.SetActive(true);
            if (!ColorUtility.TryParseHtmlString("#d45353", out v_color))
                v_color = Color.green;
        }
        /*public virtual void OnHandHoverEnd(Hand hand)
        {
            for (int i = 0; i < _normal.Length; i++)
            {
                v_mesh[i].material = _normal[i];
            }
            text_costo.gameObject.SetActive(false);
            text_Creditos.gameObject.SetActive(false);
            ControllerButtonHints.HideButtonHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
            hand.BroadcastMessage("Fn_SetTexto", false, SendMessageOptions.DontRequireReceiver);
            //ControllerButtonHints.HideTextHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        }
        public virtual void OnHandHoverBegin(Hand hand){}
        public virtual void HandHoverUpdate(Hand hand)
        {
            if (hand != Player.instance.leftHand)
            {
                return;
            }
            v_hand = hand;
            hand.BroadcastMessage("Fn_SetTexto", true, SendMessageOptions.DontRequireReceiver);//mostrar el panel con la cantidad de almas que se tiene
            ControllerButtonHints.ShowButtonHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
            text_costo.gameObject.SetActive(true);
            text_Creditos.gameObject.SetActive(true);
            if (!Jug_Datos.Instance.Fn_GetComprando() && Jug_Datos.Instance.Fn_PuedeComprar(v_costo) == true)
            {
                if (v_puede)
                {
                    Fn_Materiales(true);
                    text_costo.color = Color.white ; 
                }
                else
                {
                    Fn_Materiales(false);
                    text_costo.color = v_color; 
                }
                if (hand.GetStandardInteractionButtonDown() &&v_puede)
                {
                    Fn_Accion();
                    Jug_Datos.Instance.Fn_Comprar(v_costo);
                    text_costo.color = Color.white;
                    GetComponentInParent<Audio.Au_Manager>().Fn_SetAudio(0,false,true) ;
                }
                else if (hand.GetStandardInteractionButtonDown() && !v_puede)//no puede comprarlo
                {
                    GetComponentInParent<Audio.Au_Manager>().Fn_SetAudio(1,false,true) ;
                }
            }
            else if (!Jug_Datos.Instance.Fn_GetComprando() && Jug_Datos.Instance.Fn_PuedeComprar(v_costo) == false)//no tengo dinero
            {
                Fn_Materiales(false);
                text_costo.color= v_color;
            }
        }*/
        protected void Fn_Materiales(bool _valor)
        {
            for (int i = 0; i < v_mesh.Length; i++)
            {
                if (_valor)
                {
                    v_mesh[i].material = _puede;
                }
                else
                {
                    v_mesh[i].material = _NoPuede;
                }
            }
        }
        public virtual void Fn_Accion(){ }
        /// <summary>
        /// para volver a activar el poder usarlo
        /// </summary>
        public virtual void Fn_Set()
        {v_puede = true;}
    }
}