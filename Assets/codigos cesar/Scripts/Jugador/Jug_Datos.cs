using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
namespace Jugador
{
    public class Jug_Datos : MonoBehaviour
    {
        /// <summary>
        /// index de la mano izquierda, para vibrar
        /// </summary>
        public int v_IndexIzq = -1;
        /// <summary>
        /// index de la mano derecha, para vibrar
        /// </summary>
        public int v_IndexDer = -1;
        ///// <summary>
        ///// el objeto que van a golpear, el body collider
        ///// </summary>
        //public GameObject v_cuerpo;
        /// <summary>
        /// ALMAS QUE TE SIRVEN PARA COMPRAR
        /// </summary>
        public int v_Almas = 100;
        public int v_AlmasTot = 100;

        /// <summary>
        /// ESTAS VIVO?
        /// </summary>
        public bool v_Vivo = true;
        /// <summary>
        /// VIDA ACTUAL
        /// </summary>
        public float v_Vida = 100;
        /// <summary>
        /// MAXIMO DE VIDA POSIBLE
        /// </summary>
        public float v_MaxVida = 100;
        /// <summary>
        /// DEFENSA PARA DAÑOS
        /// </summary>
        public float v_Defensa = 1;
        /// <summary>
        /// los objetos a los que puede atacar el enemigo
        /// </summary>
        public Items.Item_Magico v_Objetos;
        ///// <summary>
        ///// CUANTOS OBJETOS MAGICOS ESTAN ROTOS
        ///// </summary>
        //public int v_t_Actual = 0;
        /// <summary>
        /// ESTOY COMPRANDO?
        /// </summary>
        public bool v_comprando = false;
        public GameObject v_PanelCreditos;
        public GameObject v_PanelFinal;
        public UnityEngine.UI.Text v_textoFinal;
        public Audio.Au_Manager v_audio;
        public string v_Porcen
        {
            get
            {
                return ((v_Vida / v_MaxVida) * 100).ToString("F1") + " %";
            }
        }
        // Singleton instance 
        private static Jug_Datos instance;
        public static Jug_Datos Instance
        {
            get
            {
                return instance;
            }
        }
       
        private void Awake()
        {
            if (instance)
            {
                DestroyImmediate(gameObject);
            }
            if (!instance)
            {
                instance = this;
                if(v_PanelFinal)
                    v_PanelFinal.SetActive(false);
                //DontDestroyOnLoad(this);
                v_MaxVida = 1000;
                v_Vida = v_MaxVida;
                //CUANTOS OBJETOS MAGICOS ESTAN EN EL NIVEL
                v_Objetos = FindObjectOfType<Items.Item_Magico>();
                v_audio = GetComponent<Audio.Au_Manager>();
                v_audio.Fn_Inicializa();
                v_AlmasTot = v_Almas;
                //Items.Item_Magico[] _val = FindObjectOfType<Items.Item_Magico>();
                //v_Objetos = new GameObject[_val.Length];
                //for (int i = 0; i < _val.Length; i++)
                //{
                //    v_Objetos[i] = _val[i].gameObject;
                //}
            }
        }
        /// <summary>
        /// ESTOY COMPRANDO?
        /// </summary>
        public bool Fn_GetComprando()
        {
            return v_comprando;
        }
        public void Fn_SetComprando(bool _comp)
        {
            v_comprando = _comp;
        }
        public void Fn_SetObj()
        {
            if (v_Objetos != null)
                v_Objetos.Fn_Init();
        }
        /// <summary>
        /// CUANDO  ALGUIEN HACE DAÑO ACA SE HACEN LOS CALCULOSS
        /// </summary>
        public void Fn_Dano(float _dano)
        {
            if (!v_Vivo)
                return;

            float resta = _dano - v_Defensa;
            if (resta <= 0 || v_Vivo == false)
            {
                return;
            }
            else
            {
                v_Vida -= resta;
                v_Vida = Mathf.Clamp(v_Vida, 0, v_MaxVida);
                v_audio.Fn_SetAudio(2, false, true);
                /*if (Player.instance.leftHand != null)//ACTUALIZA DATOS EN EL UI DE LA MANO
                {
                    if (SteamVR.instance != null)
                    {
                        if (v_IndexIzq == -1)
                            v_IndexIzq = (int)Player.instance.leftHand.GetComponentInChildren<SteamVR_RenderModel>().index;
                        //steamvr render model    index
                        if(v_IndexIzq!=-1)
                            SteamVR_Controller.Input(v_IndexIzq).TriggerHapticPulse(20000);
                    }
                    Player.instance.leftHand.GetComponent<UI_Datos>().Fn_Actualizar();
                }
                if (Player.instance.rightHand && SteamVR.instance != null)
                {
                    if (v_IndexDer == -1)
                        v_IndexDer = (int)Player.instance.rightHand.GetComponentInChildren<SteamVR_RenderModel>().index;

                    if(v_IndexDer!= -1)
                        SteamVR_Controller.Input(v_IndexDer).TriggerHapticPulse(20000);
                }
                if (v_Vida <= 0)//MUERTO
                {
                    Debug.LogError("me mori " + transform.name);
                    v_Vida = 0;
                    v_Vivo = false;
                    Fn_SetPuntaje(Idioma.Scr_ManagerIdioma.instance.Fn_GetTexto("perdido_3"));//  "Jugador muerto");
                }*/
            }
        }
        /// <summary>
        /// ME ALCANZA SEL DINERO SEGUN EL PRECIO?
        /// </summary>
        public bool Fn_PuedeComprar(int _costo)
        {
            int _resta = v_Almas - _costo;
            if (_resta > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Fn_Curar(float _vida)
        {
            v_Vida += _vida;
            v_Vida = Mathf.Clamp(v_Vida, 0, v_MaxVida);
            //Player.instance.leftHand.GetComponent<UI_Datos>().Fn_Actualizar();
        }

        /// <summary>0 no la tiene la debe comprar,
        /// 1 ya tiene al maximo,
        /// 2 le faltan balas comprarlas
        /// </summary>
        /*public bool Fn_RevisaArma( System.Type _arma)
        {
            if (Player.instance.rightHand)
                return Player.instance.rightHand.GetComponent<Jug_Arma>().Fn_RevisaArma(_arma);
            else
            {
                // Debug.LogError("no mano derecha");
                return true;//-1;
            }
        }
        public int Fn_GetCostoPorcen( System.Type _arma)
        {
            if (Player.instance.rightHand)
                return Player.instance.rightHand.GetComponent<Jug_Arma>().Fn_GetCostoPorcen(_arma);
            else
            {
                // Debug.LogError("no mano derecha");
                return -1;
            }
        }*/

        /// <summary>
        /// COMPRAR/RECARGAR UN ARMA
        /// </summary>
        /*public void Fn_Comprar(int _costo, System.Type _armaComprada, bool _recarga)
        {
            //hace el "cobro" y hace sus calculos a la mano derecha
            v_Almas -= _costo;
            v_Almas = (int)Mathf.Clamp(v_Almas, 0, Mathf.Infinity);
            Fn_SetComprando(false);
            if (Player.instance.rightHand != null)
            {
                if (_recarga)
                    Player.instance.rightHand.GetComponent<Jug_Arma>().Fn_Recarga(_armaComprada);
                else
                    Player.instance.rightHand.GetComponent<Jug_Arma>().Fn_Comprar(_armaComprada);
            }
            if (Player.instance.leftHand != null)
            {
                Player.instance.leftHand.GetComponent<UI_Datos>().Fn_Actualizar();
            }
        }
        /// <summary>
        /// COMPRAR CUALQUIER COSA, SOLO SE HACE EL COBRO 
        /// </summary>
        public void Fn_Comprar(int _costo)
        {
            v_Almas -= _costo;
            v_Almas = Mathf.Clamp(v_Almas, 0, int.MaxValue);
            Fn_SetComprando(false);
            if (Player.instance.leftHand != null)
            {
                Player.instance.leftHand.GetComponent<UI_Datos>().Fn_Actualizar();
            }
        }
        /// <summary>
        /// AGREGAR ALMAS 
        /// </summary>
        public void Fn_AddAlmas(int agregar)
        {

            v_Almas += agregar;
            v_Almas = Mathf.Clamp(v_Almas, 0, int.MaxValue);
            v_AlmasTot += agregar;
            v_AlmasTot = Mathf.Clamp(v_AlmasTot, 0, int.MaxValue);
            if (Player.instance.leftHand != null)
            {
                Player.instance.leftHand.GetComponent<UI_Datos>().Fn_Actualizar();
            }
            //print("agrego almas"+ v_Almas);
        }
        /// <summary>
        /// max vida, vida, almas
        /// </summary>
        public Vector3 Fn_GetDatos()
        {
            return new Vector3(v_MaxVida, v_Vida, v_Almas);
        }
        /// <summary>
        /// REGRESAR EL OBJETO
        /// </summary>
        public GameObject Fn_GetPosicion()
        {
            if (SteamVR.instance != null)
            {
                return Player.instance.rigSteamVR.GetComponentInChildren<Jug_Cuerpo>().Fn_GetObj();
            }
            else
            {
                return Player.instance.rig2DFallback.GetComponentInChildren<Jug_Cuerpo>().Fn_GetObj();
            }
        }
        /// <summary>
        /// matan al jugador o todos los objetos
        /// </summary>
        public bool Fn_GetVivo()
        { return v_Vivo; }
        /// <summary>
        /// Todas las oleadas terminadas
        /// </summary>
        public void Fn_SetMuerte(string _val)
        {
            Fn_SetPuntaje(Idioma.Scr_ManagerIdioma.instance.Fn_GetTexto("perdido_2"));// "Todas las oleadas terminadas");
            if (Player.instance.leftHand != null)
            { Player.instance.leftHand.GetComponent<UI_Datos>().Fn_MuestraAviso(6, 3); }
        }
        /// <summary>
        /// para rendirse, terminar todo
        /// </summary>
        public void Fn_SetMuerte(bool _val)
        {
            v_Vivo = false;
            v_Objetos.Fn_SetVivo(false);
            //for(int i=0; i< v_Objetos.Length; i++)
            //{
            //    v_Objetos[i].GetComponent<Items.Item_Magico>().Fn_SetVivo(false);
            //}
        }
        /// <summary>
        /// DESTRUYEN UN OBJETO MAGICO
        /// </summary>
        public void Fn_SetMuerte()
        {
            Debug.LogError("PIERDE POR LOS MAGICOS");
            v_Vivo = false;

            //v_t_Actual++;
            //if (v_t_Actual >= v_Objetos.Length)
            //{
            //    v_Vivo = false;
            //}
        }
        public void Fn_SetPuntaje(string _str)
        {
            Puntaje.Scr_Puntaje _puntaje = FindObjectOfType<Puntaje.Scr_Puntaje>();
            _puntaje.Fn_Set(v_AlmasTot, Manager_Horda.Instance.v_numOleada, _str);
            //_puntaje.Fn_Set(v_Almas, Manager_Horda.Instance.v_numOleada, _str);
            v_PanelFinal.SetActive(true);
            v_textoFinal.text = _str+".\n"+Idioma.Scr_ManagerIdioma.instance.Fn_GetTexto("confirma_1_2");
            Manager_Horda.Instance.Fn_MataTodo();
        }
        public void Fn_Continuar()
        {
            Letras.Fn_SetString(Letras.v_escena, "Menu_Escenario"); //"Menu");
            UnityEngine.SceneManagement.SceneManager.LoadScene(Letras.v_escenaElim);
        }*/
    }
}
/*
 5/3-0-0/0-0-0
2/4-0-0/0-0-0
2/4-0-0/0-0-0
1/1-0-1/1-0-0
7/3-1-3/1-2-0
6/3-2-3/1-2-0
4/3-3-3/2-1-0
4/3-4-3/2-2-1
5/4-2-3/1-2-2
5/5-5-3/2-1-3
2/2-4-3/2-0-3
2/7-2-3/1-1-3
3/3-3-3/3-1-3
4/5-4-3/1-2-3
5/6-5-3/1-3-3
*/
