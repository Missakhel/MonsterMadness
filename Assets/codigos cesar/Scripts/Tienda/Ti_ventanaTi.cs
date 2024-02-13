using Jugador;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
using Ventanas;
using Audio;
namespace Tienda
{
    //[RequireComponent(typeof(Interactable), typeof(InteractableHoverEvents), typeof(Au_Manager))]
    public class Ti_ventanaTi : MonoBehaviour
    {
        #region VARIABLES GENERALES
        /// <summary>
        /// CAMBIAR DE COLOR CUANDO LA MANO LO TOQUE
        /// </summary>
        [Header("datos generales")]
        Animator v_anim;
        public float v_vel = 1;
        Au_Manager v_audio;
        public int id;
        /// <summary>
        /// INFO DE LA VENTANA ACTUAL
        /// </summary>
        public Ventana v_ventana;
        /// <summary>
        /// PANEL QUE TIENE LAS OPCIONES DE COMPRAR
        /// </summary>
        public GameObject v_PanelItem;
        public GameObject v_flecha;
        public GameObject v_barreraItem;
        /// <summary>
        /// YA SE ESTA MOSTRANDO EL PANEL?
        /// </summary>
        public bool v_activa = false;
        #endregion
        #region VARIABLES DE LOS ITEMS A COMPRAR
        /// <summary>
        /// YA COMPRASTE LA TORRETA?
        /// </summary>
        [Header("datos para comprar los items")]
        public bool v_torre = false;
        /// <summary>
        ///PREFAB A INSTANACIAR LA TORRETA
        /// </summary>
        public GameObject v_PrefTorre;
        /// <summary>
        /// DONDE SE VA A PONER LA TORRETA
        /// </summary>
        public Transform v_Postorre;
        /// <summary>
        /// YA COMPRASTE LA BARRERA?
        /// </summary>
        public bool v_Barrera = false;
        /// <summary>
        /// PRECIO POR REPARAR UNA VENTANA
        /// </summary>
        [Header("precios")]
        public int v_Cosrepara = 100;
        /// <summary>
        /// PRECIO POR REPARAR TODO
        /// </summary>
        public int v_CosreparaTodo = 1000;
        /// <summary>
        /// PRECIO DE LA BARRERA
        /// </summary>
        public int v_Cosbarrera = 100;
        /// <summary>
        /// PRECIO  DE LA TORRETA
        /// </summary>
        public int v_Costorreta = 200;
        public UnityEngine.UI.Text v_texto;
        #endregion
        private void Awake()
        {
            v_activa = false;
            v_anim = GetComponentInChildren<Animator>();
            v_audio = GetComponent<Au_Manager>();
            v_audio.Fn_Inicializa();
            v_PanelItem.SetActive(false);
            v_flecha.SetActive(false);
            //v_ventana = GetComponentInParent<Ventana>();
            //v_Postorre = transform.GetChild(2);
            if (v_PrefTorre == null)
            {
                Debug.LogError("OLVIDASTE PORNER EL PREFAB DE TORRE" + gameObject.name);
            }
        }
        /*private void OnHandHoverEnd(Hand hand)
        {
            if (hand != Player.instance.leftHand)
            {
                return;
            }
            ControllerButtonHints.HideButtonHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        }
        private void HandHoverUpdate(Hand hand)
        {
            // SOLO DEBE FUNCIONAR CON LA MANO IZQUIERDA
            if (hand != Player.instance.leftHand)
            { return; }
            ControllerButtonHints.ShowButtonHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
            if (hand.GetStandardInteractionButtonDown())
            {
                v_anim.SetTrigger("push");
                v_audio.Fn_SetAudio(0, false, true);
                v_activa = ! v_activa;
                v_PanelItem.SetActive(v_activa);
                v_flecha.SetActive(v_activa);
            }
        }
        public void Fn_ComprarTorre()
        {
            if (!v_torre)
            {
                v_torre = true;
                Jug_Datos.Instance.Fn_Comprar(v_Costorreta);
                GameObject _obj = Instantiate(v_PrefTorre, v_Postorre.position, Quaternion.identity, v_Postorre);//como se esta tomando la rotacion del modelo, viene girado, entonces con esto se acomoda a las coordenadas del mundo
                _obj.transform.rotation = Quaternion.FromToRotation(Vector3.forward, v_Postorre.forward);
                v_activa = false;
                v_anim.SetTrigger("push");
                v_PanelItem.SetActive(v_activa);
                v_flecha.SetActive(v_activa);
                if(Player.instance.leftHand)
                    Player.instance.leftHand.BroadcastMessage("Fn_SetTexto", false, SendMessageOptions.DontRequireReceiver);
            }
        }
        public void Fn_ComprarBarrera()
        {
            if (!v_Barrera)
            {
                Jug_Datos.Instance.Fn_Comprar(v_Cosbarrera);
                Fn_Barrera(true);
                v_ventana.Fn_EscudoInit();
                v_activa = false;
                v_anim.SetTrigger("push");
                v_PanelItem.SetActive(v_activa);
                v_flecha.SetActive(v_activa);
                if (Player.instance.leftHand)
                    Player.instance.leftHand.BroadcastMessage("Fn_SetTexto", false, SendMessageOptions.DontRequireReceiver);
            }
        }
        public void Fn_Barrera(bool _val)
        {
            v_Barrera = _val;
            v_barreraItem.SetActive(_val);
        }
        public void Fn_Repara()
        {
            if (v_ventana.Fn_GRota())
            {
                Jug_Datos.Instance.Fn_Comprar(v_Cosrepara);
                v_ventana.Fn_Repara();
                v_activa = false;
                v_anim.SetTrigger("push");
                v_PanelItem.SetActive(v_activa);
                v_flecha.SetActive(v_activa);
                if (Player.instance.leftHand)
                    Player.instance.leftHand.BroadcastMessage("Fn_SetTexto", false, SendMessageOptions.DontRequireReceiver);
            }
        }
        public void Fn_ReparaTodo()
        {
            Jug_Datos.Instance.Fn_Comprar(v_CosreparaTodo);
            Manager.Manager_Ventanas.Instance.Fn_ReparaToda();
            v_activa = false;
            v_anim.SetTrigger("push");
            v_PanelItem.SetActive(v_activa);
            v_flecha.SetActive(v_activa);
            if (Player.instance.leftHand)
                Player.instance.leftHand.BroadcastMessage("Fn_SetTexto", false, SendMessageOptions.DontRequireReceiver);
        }*/
        /// <summary>
        /// LO USA EL ITEM DE TORRETA, SET SI HAY TORREA ACTIVA
        /// </summary>
        public void Fn_Torre(bool _valor)
        {
            v_torre = _valor;
        }
        /// <summary>
        /// HAY TORRE ACTIVA?
        /// </summary>
        public bool Fn_Gtorre()
        {
            return v_torre;
        }
        /// <summary>
        /// HAY BARRERA ACTIVA?
        /// </summary>
        public bool Fn_GBarrera()
        {
            return v_Barrera;
        }
        /// <summary>
        /// CUANTO CUESTA
        /// </summary>
        public int Fn_CosRepara()
        {
            return v_Cosrepara;
        }
        /// <summary>
        /// CUANTO CUESTA
        /// </summary>
        public int Fn_CosReparaTodo()
        {
            return v_CosreparaTodo;
        }
        public void Fn_SetPrecio(int _indice, Color _col)
        {
            int _precio = 0;
            if (_indice == 0)
                _precio = v_Cosrepara;
            else if (_indice == 1)
                _precio = v_CosreparaTodo;
            else if (_indice == 2)
                _precio = v_Cosbarrera;
            else if (_indice == 3)
                _precio = v_Costorreta;
            else
            {
                _col = Color.white;
                _precio = 0;
            }

            v_texto.text = _precio.ToString("F0");
            v_texto.color = _col;
        }
        /// <summary>
        /// CUANTO CUESTA
        /// </summary>
        public int Fn_CosTorre()
        {
            return v_Costorreta;
        }
        /// <summary>
        /// CUANTO CUESTA
        /// </summary>
        public int Fn_CosBarrera()
        {
            return v_Cosbarrera;
        }
    }
}
