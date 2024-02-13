using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using Valve.VR.InteractionSystem;
namespace Tienda
{
    using Jugador;
    using Armas;
    public class Ti_Arma : Ti_Base {
        [Header("Elementos para Arma")]
        Arma _Arma; 
        public int v_valorArma;
        public int v_costoArma;
        public int v_costoBalas;
        public bool v_comprado=false;
        public UnityEvent v_funcion;
        private void Awake()
        {
            _Arma = GetComponent<Arma>();
            Vector2 _vec = _Arma.Fn_GetCosto();
            v_costoArma = (int)_vec.x;
            v_costoBalas = (int)_vec.y;
            v_comprado = false;
            v_costo = v_costoArma;
            Fn_Config(v_costo);
        }
        /*public override void HandHoverUpdate(Hand hand)
        {
            if (hand != Player.instance.leftHand)
            {
                return;
            }
            if(Player.instance.rightHand)
            {
                v_hand = hand;
                hand.BroadcastMessage("Fn_SetTexto", true, SendMessageOptions.DontRequireReceiver);//mostrar el panel con la cantidad de almas que se tiene
                ControllerButtonHints.ShowButtonHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
                if (!Player.instance.rightHand.GetComponent<Jug_Arma>().Fn_RevisaArma(_Arma.GetType()))
                {
                    v_comprado = false;
                    v_costo = v_costoArma;
                }
                else
                {
                    v_comprado = true;
                    v_valorArma= Player.instance.rightHand.GetComponent<Jug_Arma>().Fn_GetCostoPorcen(_Arma.GetType());
                    if (v_valorArma== -1)//lleno
                    {
                        v_costo = 0;//cambiar a lleno
                    }
                    else
                    {
                        v_costo = v_valorArma;
                    }
                }
                text_costo.text = v_costo.ToString();
                text_costo.gameObject.SetActive(true);//muestra los textos
                text_Creditos.gameObject.SetActive(true);//muestra los textos
                if (!Jug_Datos.Instance.Fn_GetComprando() && Jug_Datos.Instance.Fn_PuedeComprar(v_costo) == true)
                {
                    if (v_valorArma>-1)
                    {
                        v_puede = true;
                        Fn_Materiales(true);
                        text_costo.color = Color.white;
                    }
                    else
                    {
                        text_costo.text = "LLeno";
                        v_puede = false;
                        Fn_Materiales(false);
                        text_costo.color = v_color;
                    }
                    if (hand.GetStandardInteractionButtonDown() && v_puede)
                    {
                        GetComponentInParent<Audio.Au_Manager>().Fn_SetAudio(0, false, true);
                        Fn_Accion();
                    }
                    else if (hand.GetStandardInteractionButtonDown() && !v_puede)//no puede comprarlo
                    {
                        GetComponentInParent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
                    }
                }
                else if (!Jug_Datos.Instance.Fn_GetComprando() && Jug_Datos.Instance.Fn_PuedeComprar(v_costo) == false)
                {
                    Debug.LogError("costo no alcanza");
                    Fn_Materiales(false);
                    text_costo.color = v_color;
                }
            }
        }
        public override void Fn_Accion()
        {
            if (!v_comprado)//compra el arma
            {
                v_funcion.Invoke();
                Jug_Datos.Instance.Fn_Comprar(v_costo, _Arma.GetType(), false);
                if (Player.instance)
                    Player.instance.rightHand.GetComponent<Jug_Arma>().Fn_ActualizaManager();
            }
            else//comprar balas
            {
                Jug_Datos.Instance.Fn_Comprar(v_costo, _Arma.GetType(), true);
            }
            Fn_Set();
            if(v_hand)
                OnHandHoverEnd(v_hand);

        }/*
        /*
        public override void HandHoverUpdate(Hand hand)
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
                //v_valorArma = Jug_Datos.Instance.Fn_RevisaArma(_Arma.GetType(), _Arma.Fn_GetMaxCargador());                
                //if( v_valorArma==0  || v_valorArma== 2)
                //{
                //    v_puede = true;
                //    Fn_Materiales(true);
                //    text_costo.color = Color.white;
                //}
                //else
                //{
                //    v_puede = false;
                //    Fn_Materiales(false);
                //    text_costo.color = v_color;
                //}
                //if (hand.GetStandardInteractionButtonDown() && v_puede)
                //{
                //    Fn_Accion();
                //    Jug_Datos.Instance.Fn_Comprar(v_costo);
                //    text_costo.color = Color.white;
                //}
            }
            else if (!Jug_Datos.Instance.Fn_GetComprando() && Jug_Datos.Instance.Fn_PuedeComprar(v_costo) == false)
            {
                Fn_Materiales(false);
                text_costo.color = v_color;
            }
        }
        public override void Fn_Accion()
        {
            //if (v_valorArma == 0)
            //    Jug_Datos.Instance.Fn_Comprar(v_costo, _Arma.GetType(), _Arma.Fn_GetMaxCargador(), false);
            //else if(v_valorArma==2)
            //    Jug_Datos.Instance.Fn_Comprar(v_costo, _Arma.GetType(), _Arma.Fn_GetMaxCargador(), true);
            //else { return; }

            Fn_Set();
        }
        */
    }
}
