using Jugador;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
public enum Ejes
{
    EjeX,
    EjeY,
    EjeZ
}
namespace Tienda
{
    using Ventanas;
    //[RequireComponent(typeof(Interactable))]
    public class Ti_Ventana : MonoBehaviour {
        #region VARIABLES GENERALES
        public Ejes v_eje;
        /// <summary>
        /// CAMBIAR DE COLOR CUANDO LA MANO LO TOQUE
        /// </summary>
        [Tooltip("Interactua"), Header("datos generales")]
        public Material v_puede;
        /// <summary>
        /// GUARDAR EL MATERIAL QUE TIENE ANTES DE CAMBIARLO
        /// </summary>
        Material v_original;
        public int id;
        /// <summary>
        /// INFO DE LA VENTANA ACTUAL
        /// </summary>
        Ventana v_ventana;
        /// <summary>
        /// MODELO DE LA VENTANA
        /// </summary>
        [Tooltip("modelo de la ventana")]
        GameObject v_modelo;
        /// <summary>
        /// PANEL QUE TIENE LAS OPCIONES DE COMPRAR
        /// </summary>
        public GameObject v_PanelItem;
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
            if (transform.childCount < 3)
            {
                Debug.LogError("HIJO 1 ES PARA MURO, 2 EL PANEL Y 3 ES POSTORRETA desde " + gameObject, gameObject);
                Debug.Break();
            }
            //v_PanelItem = gameObject.transform.GetChild(1).gameObject;
            v_PanelItem.SetActive(false);
            v_modelo = gameObject;
            v_ventana = GetComponent<Ventana>();
            //if(v_modelo.GetComponent<MeshRenderer>() )
            //{
            //    Debug.LogError("siii " + v_modelo.GetComponent<MeshRenderer>().material.name);
            //}
            //else
            //{
            //    Debug.LogError("NOOOOO "+ v_modelo.name);
            //}
            if (v_modelo.GetComponent<MeshRenderer>())
            {
                v_original = v_modelo.GetComponent<MeshRenderer>().material;
            }
            else if (v_modelo.GetComponentInChildren<MeshRenderer>())
            {
                v_original = v_modelo.GetComponentInChildren<MeshRenderer>().material;
            }
            v_Postorre = transform.GetChild(2);
            // transform.GetChild(0).gameObject.SetActive(false);
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
            v_modelo.GetComponent<MeshRenderer>().material = v_original; 
            if (v_modelo.GetComponent<Ventana>().Fn_GRota()  )//&& !v_activa)
                v_modelo.GetComponent<MeshRenderer>().enabled = false;
            //else
            //    v_modelo.GetComponent<MeshRenderer>().enabled = true;
            ControllerButtonHints.HideButtonHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        }
        private void HandHoverUpdate(Hand hand)
        {
            // SOLO DEBE FUNCIONAR CON LA MANO IZQUIERDA
            if (hand != Player.instance.leftHand)
            { return; }
            v_modelo.GetComponent<MeshRenderer>().material = v_puede;
            v_modelo.GetComponent<MeshRenderer>().enabled = true;
            ControllerButtonHints.ShowButtonHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
            if (hand.GetStandardInteractionButtonDown() && !v_activa)
            {
                //Jug_Datos.Instance.Fn_SetComprando(true);//NO PUEDES DISPARAR MIENTRAS COMPRAS
                v_PanelItem.SetActive(true);
                //v_PanelItem.transform.position = Fn_Pos( hand.transform.position);
                //v_PanelItem.transform.localRotation =Quaternion.Euler( (transform.rotation.eulerAngles)-new Vector3(0,180,0));
                GetComponent<Interactable>().enabled = true;
                v_activa = true;
            }
            else if (hand.GetStandardInteractionButtonDown() && v_activa)
            {
                //Jug_Datos.Instance.Fn_SetComprando(false);
                v_PanelItem.SetActive(false);
                v_activa = false;
                //v_modelo.GetComponent<MeshRenderer>().material = v_original;
            }
        }
        /// <summary>
        /// CAMBIAR EL COLOR CUANDO LO COMIENZA A TOCAR
        /// </summary>
        private void HandHoverBegin(Hand hand)
        {
            if (hand == Player.instance.leftHand)
            {
                v_modelo.GetComponent<MeshRenderer>().material = v_puede;
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
                v_PanelItem.SetActive(false);
                v_activa = false;
                Player.instance.leftHand.BroadcastMessage("Fn_SetTexto", false, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                Debug.LogError("false comprar torre");
            }
        }
        public void Fn_ComprarBarrera()
        {
            if (!v_Barrera)
            {
                Jug_Datos.Instance.Fn_Comprar(v_Cosbarrera);
                Fn_Barrera(true);
                GetComponent<Ventana>().Fn_EscudoInit();               
                v_PanelItem.SetActive(false);
                v_activa = false;
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
                print("reparar una");
                Jug_Datos.Instance.Fn_Comprar(v_Cosrepara);
                v_ventana.Fn_Repara();
                v_PanelItem.SetActive(false);
                v_activa = false;
                Player.instance.leftHand.BroadcastMessage("Fn_SetTexto", false, SendMessageOptions.DontRequireReceiver);
            }
        }
        public void Fn_ReparaTodo()
        {
            print("reparar Todo");
            Jug_Datos.Instance.Fn_Comprar(v_CosreparaTodo);
            Manager.Manager_Ventanas.Instance.Fn_ReparaToda();
            v_PanelItem.SetActive(false);
            v_activa = false;
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
        /// <summary>
        /// mover el panel de los botones solo en lo horizontal y vertical(local)
        /// </summary>
        private Vector3 Fn_Pos(Vector3 _pos)
        {
            Vector3 _ret = Vector3.zero;
            if (v_eje == Ejes.EjeX)
            {
                _ret = new Vector3(_pos.x, v_PanelItem.transform.position.y, v_PanelItem.transform.position.z);
            }
            else if (v_eje == Ejes.EjeY)
            {
                _ret = new Vector3( v_PanelItem.transform.position.x, _pos.y, v_PanelItem.transform.position.z);
            }
            else
            {
                _ret = new Vector3( v_PanelItem.transform.position.x, v_PanelItem.transform.position.y, _pos.x);
            }
            return _ret;
            //if(v_eje == Ejes.EjeX)
            //{
            //    _ret = new Vector3(v_PanelItem.transform.position.x, _pos.y, _pos.z);
            //}
            //else if(v_eje == Ejes.EjeY)
            //{
            //    _ret = new Vector3(_pos.x, v_PanelItem.transform.position.y,  _pos.z);
            //}
            //else
            //{
            //    _ret = new Vector3( _pos.x, _pos.y, v_PanelItem.transform.position.z);
            //}
            //return _ret;
        }
    }
}
