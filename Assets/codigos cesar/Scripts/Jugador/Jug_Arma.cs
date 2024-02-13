using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
using Manager;
namespace Jugador
{
    [RequireComponent(typeof(Ar_Manager), typeof(Armas.Ar_Menu))]
    public class Jug_Arma : MonoBehaviour
    {
        Ar_Manager v_man_armas;
        //public Hand v_R_hand;
        [Tooltip("el prefab de la mira laser del arma")]
        /// <summary>
        /// el prefab de la mira "laser" del arma
        /// </summary>
        public GameObject v_miraPref;
        /// <summary>
        /// el objeto que se va a manejar en la escena
        /// </summary>
        protected GameObject v_Mira;
        public GameObject v_panelArmas;
        /// <summary>
        /// puede abrir el menu de cambio de arma
        /// </summary>
        public bool v_cambio=true;
        void Awake()
        {
            v_man_armas = GetComponent<Ar_Manager>();
            v_panelArmas.SetActive(false);
        }
        public void Fn_ActualizaManager()
        {
            if (v_panelArmas.activeInHierarchy)
                v_panelArmas.GetComponent<Armas.Ar_Menu>().Fn_Actualiza(v_man_armas);
        }
        public void Fn_SetCambio(bool _Cambio)
        {
            v_cambio = _Cambio;
        }
        void Update()
        {
            /*if (v_R_hand == null)//SI NO HE AGREGADO LA MANO
            {
                v_R_hand = Player.instance.rightHand;//TOMO LA MANO DEL PREFAB PLAYER
                if (v_Mira == null && v_R_hand != null)//ACOMODA LA MIRA SEGUN LA POSICION DE LA MANO
                {
                    v_R_hand = Player.instance.rightHand;
                    v_Mira = Instantiate(v_miraPref, Player.instance.rightHand.transform);
                    //v_Mira.SetActive(true);
                    v_Mira.SetActive(false);
                    v_Mira.transform.localRotation = Quaternion.identity;
                    v_Mira.transform.localPosition = new Vector3(0, 0, 1.0f);
                }
            }
            else//YA TENGO MANO, PUEDO DISPARAR
            {//SOLO SE PUEDE DISPARAR SI NO ESTA COMPRANDO
                if (!Jug_Datos.Instance.Fn_GetVivo())
                    return;

                if (SteamVR.instance != null && v_R_hand.controller != null)//SI ESTA EL VIVE
                {
                    if (v_R_hand.GetStandardInteractionButtonDown() && !Jug_Datos.Instance.Fn_GetComprando())
                    {
                        v_man_armas.Fn_Down();
                    }
                    if (v_R_hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Axis0)  && !v_panelArmas.activeInHierarchy && v_cambio)
                    {
                        v_panelArmas.SetActive(true);
                    }
                    //if (v_R_hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))// EL BOTON DE LOS LADOS
                    //{
                    //    //v_man_armas.Fn_Anterior();
                    //    if(Manager_Horda.Instance)
                    //        Manager_Horda.Instance.Fn_MataTodo();
                    //}
                    //else if (v_R_hand.controller.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))//PRESIONA EL TOUCH DE ENMEDIO
                    //{
                    //    v_man_armas.Fn_Siguiente();
                    //}
                }
                else//SIN EL VIVE
                {
                    if (v_R_hand.GetStandardInteractionButtonDown() && !Jug_Datos.Instance.Fn_GetComprando())
                    {
                        v_man_armas.Fn_Down();
                    }
                    //if (Input.GetKeyDown(KeyCode.I))
                    //{
                    //    v_panelArmas.SetActive(!v_panelArmas.activeInHierarchy);
                    //}
                    if (Input.GetKeyDown(KeyCode.Y)) //|| v_R_hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))//izquierda
                    {
                        v_man_armas.Fn_Anterior();
                    }
                    else if (Input.GetKeyDown(KeyCode.U))// ||  v_R_hand.controller.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        v_man_armas.Fn_Siguiente();
                    }
                }
            }*/
        }
        /// <summary>
        /// CON LA MANO IZQUIERDA SE HACE EL COBRO Y ACA SE AGREGA LA NUEVA ARMA O RECARGA BALAS
        /// </summary>
        public void Fn_Comprar(System.Type _comprada)
        {
            v_man_armas.Fn_Comprar(_comprada);
        }
        public int Fn_GetCostoPorcen(System.Type _arma)
        {
            return v_man_armas.Fn_GetCostoPorcentaje(_arma);
        }
        public void Fn_Recarga(System.Type _comprada)
        {
            v_man_armas.Fn_ComprarRecarga(_comprada);
        }
        /// <summary>0 no la tiene, la debe comprar
        /// 1 ya tiene al maximo
        /// 2 le faltan balas comprarlas
        /// </summary>
        public bool Fn_RevisaArma(System.Type _arma)
        {
            return v_man_armas.Fn_Existe(_arma);
        }
        public Ar_Manager Fn_GetManager()
        {
            return v_man_armas;
        }
    }
}