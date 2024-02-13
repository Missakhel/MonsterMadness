using Jugador;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
namespace Tienda
{
    [RequireComponent( typeof(BoxCollider))]
    public class Ti_Hechizo :Ti_Base
    {
        [Header("Elementos del item")]
        public Transform v_posicion;//posicion donde instanciar
        public GameObject v_flecha;
        public GameObject v_pref;//prefab del dummy
        public GameObject pref_base;//prefab de la base
        public int v_cont = 0;
        void Awake()
        {
            if (v_pref == null || v_posicion == null)// || v_costo<1)
            {
                Debug.LogError(" prefab o transform vacio,   costo en 0, en objeto " + gameObject.name);
                Debug.Break();
            }
            Fn_SetFlecha(false);
            Fn_Config(100);
        }
        /*public override void HandHoverUpdate(Hand hand)
        {
            if (hand != Player.instance.leftHand)
            {
                return;
            }
            v_hand = hand;
            text_costo.gameObject.SetActive(true);
            text_Creditos.gameObject.SetActive(true);//se puede modificar
            if (!Jug_Datos.Instance.Fn_GetComprando() && Jug_Datos.Instance.Fn_PuedeComprar(v_costo) && Manager_Horda.Instance.FN_GetJuego())
            {
                if (v_puede)
                {
                    Fn_Materiales(true);
                    text_costo.color = Color.white;
                }
                else
                {
                    Fn_Materiales(false);
                    text_costo.color = v_color;
                }
                if (hand.GetStandardInteractionButtonDown() && v_puede)
                {
                    Fn_Accion();
                    text_costo.color = Color.white;
                    Jug_Datos.Instance.Fn_Comprar(v_costo);
                    GetComponentInParent<Audio.Au_Manager>().Fn_SetAudio(0, false, true);
                }
                else if (hand.GetStandardInteractionButtonDown() && !v_puede)//no puede comprarlo
                {
                    GetComponentInParent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
                }
            }
            else if (!Jug_Datos.Instance.Fn_GetComprando() && (!Jug_Datos.Instance.Fn_PuedeComprar(v_costo) || !Manager_Horda.Instance.FN_GetJuego()  ))
            {
                Fn_Materiales(false);
                text_costo.color = v_color;
            }
        }*/
        public void Fn_SetFlecha(bool _val)
        {
            v_flecha.SetActive(_val);
        }
        public override void Fn_Accion()
        {
            if (v_puede)
            {
                v_puede = false;
                Fn_SetFlecha(true);
                GameObject _a = Instantiate(pref_base, v_posicion.position - new Vector3(0, 0.5f, 0), Quaternion.Euler(90, 0, 0));//la base donde se coloca el monito para lanzar
                GameObject _obj = Instantiate(v_pref, v_posicion.position, Quaternion.identity);//+ new Vector3(0,1.0f,0), Quaternion.identity); el cuadrito hacerlo mas arriba y en pos original pero se cambio por el modelo y su giro         
                _obj.GetComponent<Items.Item_Hechizo>().Fn_Set(this, _a);
                //Debug.Break();
                _obj.name = "Hechizo" + v_cont;
                v_cont++;
                Jug_Datos.Instance.Fn_SetComprando(false);
            }
            //Jug_Datos.Instance.Fn_Comprar(v_costo);
            //Manager_Horda.Instance.Fn_MataTodo();
            //Fn_Set();
        }
    }
}
