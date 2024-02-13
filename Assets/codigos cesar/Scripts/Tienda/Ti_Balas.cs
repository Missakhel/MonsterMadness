using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
namespace Tienda
{
    using Armas;
    using Jugador;
    public class Ti_Balas : Ti_Base
    {
        [Header("Elementos para Arma")]
        Arma _Arma;
        public int v_valorArma;
        public int v_costoBalas;
        public bool v_comprando = false;
        /// <summary>
        /// ya esta la corutina
        /// </summary>
        public bool v_rec = false;
        public Arma v_armaMan;
        public Vector2 v_PilaDatos;
        private void Awake()
        {
            _Arma = GetComponent<Arma>();
            v_costoBalas =Mathf.RoundToInt( _Arma.Fn_GetCosto().y);
            v_costo =Mathf.RoundToInt(v_costoBalas/10);
            Fn_Config(v_costo);
        }
        /*public override void OnHandHoverEnd(Hand hand)
        {
            v_rec = false;
            v_comprando = false;
            StopAllCoroutines();
            base.OnHandHoverEnd(hand);
        }
        public override void HandHoverUpdate(Hand hand)
        {
            if (v_armaMan == null)
                v_armaMan = Player.instance.rightHand.GetComponent<Jug_Arma>().Fn_GetManager().Fn_GetTipo(_Arma.GetType());

            if (v_armaMan != null)
            {
                //if (hand != Player.instance.leftHand)
                //{
                //    return;
                //}
                v_hand = hand;
                hand.BroadcastMessage("Fn_SetTexto", true, SendMessageOptions.DontRequireReceiver);//mostrar el panel con la cantidad de almas que se tiene
                text_costo.text = v_costo.ToString();
                text_costo.gameObject.SetActive(true);//muestra los textos
                text_Creditos.gameObject.SetActive(true);//muestra los textos
                v_PilaDatos = v_armaMan.Fn_GetPila();
                if (v_PilaDatos.x < v_PilaDatos.y)//ya gastoa algo de pila
                {
                    if (!Jug_Datos.Instance.Fn_GetComprando() && Jug_Datos.Instance.Fn_PuedeComprar(v_costo) && !v_comprando && !v_rec)
                    {
                        Fn_Materiales(true);
                        text_costo.color = Color.white;
                        StartCoroutine(Ie_Recarga());
                        GetComponentInParent<Audio.Au_Manager>().Fn_SetAudio(0, false, true);
                    }
                    //else if (hand.GetStandardInteractionButtonDown() && !v_puede)//no puede comprarlo
                    //{
                    //    GetComponentInParent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
                    //}
                }
                else
                {
                    Fn_Materiales(false);
                    text_costo.color = v_color;
                }
            }
            else { Debug.LogError("Arma null"); }
        }*/
        public override void Fn_Accion(){}
        IEnumerator Ie_Recarga()
        {
            //if(v_armaMan== null)
            //    v_armaMan= Player.instance.rightHand.GetComponent<Jug_Arma>().Fn_GetManager().Fn_GetTipo(_Arma.GetType());

            if (v_armaMan != null)
            {
                int _val = 0;
                WaitForSeconds _wait = new WaitForSeconds((_Arma.v_TimepoRecarga / 10));
                WaitForSeconds _delay = new WaitForSeconds( .04f);
                v_PilaDatos = v_armaMan.Fn_GetPila();
                while ((v_PilaDatos.x < v_PilaDatos.y) )
                {

                    v_rec = true;
                    v_armaMan.Fn_RecogeMunicion(_Arma.v_MaxPila / 10);
                    //Jug_Datos.Instance.Fn_Comprar(v_costo);
                    _val++;
                    v_comprando = false;
                    yield return _wait;
                    Fn_Materiales(true);
                    text_costo.color = Color.white;
                    v_comprando = true;
                    yield return _delay;
                }
            }
            else{ Debug.LogError("Arma no puede recargar"); }
            v_rec = false;
            Fn_Materiales(false);
            text_costo.color = v_color;
            StopCoroutine(Ie_Recarga());
        }
    }
}
