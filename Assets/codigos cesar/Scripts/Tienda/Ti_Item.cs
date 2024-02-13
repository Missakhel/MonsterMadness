using Jugador;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
namespace Tienda
{
    //[RequireComponent(typeof(Interactable), typeof(BoxCollider))]
    public class Ti_Item : Ti_Base {
        [Header("Elementos del item")]
        public Transform v_posicion;//posicion donde instanciar
        public GameObject v_flecha;
        public GameObject v_pref;//prefab del dummy
        public GameObject pref_base;//prefab de la base
        /// <summary>
        /// true, osbtaculo     false, mina
        /// </summary>
        [Tooltip("0, osbtaculo     1, mina    2 hechizo") ][Range(0,2)] 
        public int v_Obtaculo;
        public int v_cont = 0;
        public UnityEngine.Events.UnityEvent v_click;
        void Awake()
        {
            if(v_pref == null || v_posicion == null)// || v_costo<1)
            {
                Debug.LogError(" prefab o transform vacio,   costo en 0, en objeto "+gameObject.name);
                Debug.Break();
            }
            Fn_SetFlecha(false);
            Fn_Config(v_costo);
        }
        public override void Fn_Accion()
        {
            if(v_puede)
            {
                v_puede = false;
                if (v_Obtaculo == 0)
                {
                    Fn_SetFlecha(true);
                    GameObject _a = Instantiate(pref_base, v_posicion.position - new Vector3(0, 0.5f, 0), Quaternion.Euler(90, 0, 0));//Quaternion.identity);//la base donde se coloca el monito para lanzar
                    GameObject _obj = Instantiate(v_pref, v_posicion.position, Quaternion.Euler(270, 0, 0)); //Quaternion.identity);//+ new Vector3(0,1.0f,0), Quaternion.identity); el cuadrito hacerlo mas arriba y en pos original pero se cambio por el modelo y su giro         
                    _obj.GetComponent<Items.Item_Obstaculo>().Fn_Set(this, _a, v_posicion.position - new Vector3(0, 0.5f, 0));
                    _obj.name = "Obstaculo";
                    Jug_Datos.Instance.Fn_SetComprando(false);
                }
                else if (v_Obtaculo == 1)
                {
                    Fn_SetFlecha(true);
                    GameObject _a = Instantiate(pref_base, v_posicion.position - new Vector3(0, 0.5f, 0), Quaternion.Euler(90, 0, 0));//la base donde se coloca el monito para lanzar
                    GameObject _obj = Instantiate(v_pref, v_posicion.position, Quaternion.identity);//+ new Vector3(0,1.0f,0), Quaternion.identity); el cuadrito hacerlo mas arriba y en pos original pero se cambio por el modelo y su giro         
                    _obj.GetComponent<Items.Item_Mina>().Fn_Set(this, _a);
                    //Debug.Break();
                    _obj.name = "Mina" + v_cont;
                    v_cont++;
                    Jug_Datos.Instance.Fn_SetComprando(false);
                    v_click.Invoke();
                }
                else
                    Debug.LogError("error en v_obstaculo");
            }
        }
        public void Fn_SetFlecha(bool _val)
        {
            v_flecha.SetActive(_val);
        }
        public override void Fn_Set()
        {
            v_puede = true;
            if (v_Obtaculo == 1 && Tutorial.Scr_Instru.Instance)
            {
                Tutorial.Scr_Instru.Instance.Fn_Siguiente(1);                
            }
        }
    }
}
