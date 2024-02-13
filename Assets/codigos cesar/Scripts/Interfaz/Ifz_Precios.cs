using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using Valve.VR.InteractionSystem;
using UnityEngine.UI;
namespace INTERFAZVR
{
    using Jugador;
    using Manager;
    using Ventanas;
    public class Ifz_Precios : MonoBehaviour
    {
        public UnityEvent v_Funcion;
        //public Tienda.Ti_Ventana v_tiventana;
        /// <summary>
        /// PARA QUE TE DEJE COMPRAR
        /// </summary>
        public Tienda.Ti_ventanaTi v_tiventana;
        /// <summary>
        /// PARA PODER TENER LOS DATOS DE VENTANA ROTA
        /// </summary>
        public Ventana v_Ventana;
        /// <summary>
        /// 0  repara todo  1 repara ventana  2 barrera    3 torreta
        /// </summary>
        [Range(0, 4)]
        public int v_Indice = 0;
        /// <summary>
        /// -1 default, 0 desactivado,  1  no tiene dinero,  2 listo
        /// </summary>
        public int v_puede;
        /// <summary>
        /// desactivado
        /// </summary>
        public Sprite v_ImgDesa;
        /// <summary>
        /// falta de dinero
        /// </summary>
        public Sprite v_Imgdinero;
        /// <summary>
        /// se puede
        /// </summary>
        public Sprite v_ImgActivo;
        public Image v_img;
        public Color v_colorRojo;
        private Color v_color;
        private void Awake()
        {
            if (!ColorUtility.TryParseHtmlString("#d45353", out v_colorRojo))
                v_colorRojo = Color.green;

            v_Ventana = GetComponentInParent<Ventana>();
            v_tiventana = v_Ventana.Fn_GetBoton();
            //v_tiventana = GetComponentInParent<Tienda.Ti_ventanaTi>();
            v_img = GetComponent<Image>();
            v_img.sprite = v_ImgDesa;
            /*if (v_Indice == 0)
            {
                v_Funcion.AddListener(v_tiventana.Fn_ReparaTodo);
            }
            else if (v_Indice == 1)
            {
                v_Funcion.AddListener(v_tiventana.Fn_Repara);
            }
            else if (v_Indice == 2)
            {
                v_Funcion.AddListener(v_tiventana.Fn_ComprarBarrera);
            }
            else if (v_Indice == 3)
            {
                v_Funcion.AddListener(v_tiventana.Fn_ComprarTorre);
            }*/
        }
        private void OnEnable()
        {
            v_img.sprite = v_ImgDesa;
        }
        private void OnDisable()
        {
            v_puede = -1;
            v_img.sprite = v_ImgDesa;
            v_color = v_colorRojo;
            v_tiventana.Fn_SetPrecio(-1, v_color);
            /*if (Player.instance)
                if (Player.instance.leftHand)
                    Player.instance.leftHand.BroadcastMessage("Fn_SetTexto", false, SendMessageOptions.DontRequireReceiver);*/
        }
        /*private void OnHandHoverEnd(Hand hand)
        {
            v_puede = -1;
            v_color = v_colorRojo;
            v_tiventana.Fn_SetPrecio(-1, v_color);
            v_img.sprite = v_ImgDesa;
            hand.BroadcastMessage("Fn_SetTexto", false, SendMessageOptions.DontRequireReceiver);
        }
        private void OnHandHoverBegin(Hand _hand)
        {
            if (_hand.GuessCurrentHandType() == Hand.HandType.Left)
            {
                v_Ventana.GetComponent<Audio.Au_Manager>().Fn_SetAudio(4, false, true);
            }
        }
        private void HandHoverUpdate(Hand hand)
        {
            if (hand != Player.instance.leftHand)
            { return; }
            ControllerButtonHints.ShowButtonHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
            hand.BroadcastMessage("Fn_SetTexto", true, SendMessageOptions.DontRequireReceiver);
            
            v_puede = Fn_Puede();

            if(v_puede==2)
            {
                v_img.sprite = v_ImgActivo;
                v_color = Color.white;
            }
            else
            {
                v_color = v_colorRojo;
                v_img.sprite = v_Imgdinero;
            }

            //if (v_puede == 1)
            //{
            //    v_color = v_colorRojo;
            //    v_img.sprite = v_Imgdinero;
            //}
            //else if (v_puede == 2)
            //{
            //    v_img.sprite = v_ImgActivo;
            //    v_color = Color.white;
            //}
            //else//el desactivado 0cero o hubo un error y llega -1
            //{
            //    v_color = Color.gray;  //v_colorRojo;
            //    v_img.sprite = v_ImgDesa;
            //}
            v_tiventana.Fn_SetPrecio(v_Indice, v_color);//mostrar el precio

            if (v_puede == 2 && hand.GetStandardInteractionButtonDown())
            {
                v_Funcion.Invoke();//la que compra
                v_puede = 0;
                v_Ventana.GetComponent<Audio.Au_Manager>().Fn_SetAudio(0, false, true);
            }
            else if(v_puede!= 2 && hand.GetStandardInteractionButtonDown()){
                v_Ventana.GetComponent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
            }
        }*/
        /// <summary>
        /// -1 default, 0 desactivado,  1  no tiene dinero,  2 listo
        /// </summary>
        public int Fn_Puede()
        {
            if (v_Indice == 0)
            {
                #region PUEDO REPARAR TODAS LAS VENTANAS?
                if (!Manager_Ventanas.Instance.Fn_GetRotas())//no hay ninguna rota
                {
                    return 0;
                }
                else
                {
                    if (Jug_Datos.Instance.Fn_PuedeComprar(v_tiventana.Fn_CosReparaTodo()))
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
                #endregion
               
            }
            else if (v_Indice == 1)
            {
                #region ESTA VENTANA EN LA QUE ESTOY, ESTA ROTA?
                if (!v_Ventana.Fn_GRota())
                {
                    return 0;
                }
                else
                {
                    if (Jug_Datos.Instance.Fn_PuedeComprar(v_tiventana.Fn_CosRepara()))
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
                #endregion
            }
            else if (v_Indice == 2)
            {
                #region PUEDO COMPRAR UNA BARRERA?
                if (v_tiventana.Fn_GBarrera())
                {
                    return 0;
                }
                else
                {
                    if (Jug_Datos.Instance.Fn_PuedeComprar(v_tiventana.Fn_CosBarrera()) && !v_Ventana.Fn_GRota())
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
                #endregion
            }
            else if (v_Indice == 3)
            {
                #region PUEDO COMPRAR TORRETA? 
                if (v_tiventana.Fn_Gtorre())
                {
                    return 0;
                }
                else
                {
                    if (Jug_Datos.Instance.Fn_PuedeComprar(v_tiventana.Fn_CosTorre()))
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
                #endregion
            }
            else
            {
                return -1;
            }
        }
    }
}