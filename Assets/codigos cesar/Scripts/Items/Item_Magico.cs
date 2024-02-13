using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
using UnityEngine.AI;
namespace Items
{
    using Jugador;
    [RequireComponent(typeof(NavMeshObstacle),typeof(SphereCollider))]
    public class Item_Magico : Dano_base {
        [Header("Cosas del cristal")]
        public Audio.Au_Manager v_audio;
        public GameObject v_roto;
        public GameObject v_normal;
        /// <summary>
        /// index de la mano izquierda, para vibrar
        /// </summary>
        int v_IndexIzq = -1;
        /// <summary>
        /// index de la mano derecha, para vibrar
        /// </summary>
        int v_IndexDer = -1;
        private void Awake()
        {
            v_roto.SetActive(false);
            v_normal.SetActive(true);
            //if(Player.instance.leftHand)
            //    Player.instance.leftHand.GetComponent<UI_Datos>().Fn_SetPorcentaje((v_Vida / v_VidaMax) * 100);
            v_audio = GetComponent<Audio.Au_Manager>();
            v_audio.Fn_Inicializa();
            base.Fn_SetVida(v_VidaMax, 5);
        }
        /// <summary>
        /// mostrarle al jugador la vida del objeto
        /// </summary>
        public void Fn_Init()
        {
            //if (Player.instance.leftHand)
            //    Player.instance.leftHand.GetComponent<UI_Datos>().Fn_SetPorcentaje((v_Vida / v_VidaMax) * 100);
        }
        public override void Fn_Muerto()
        {
            v_audio.Fn_SetAudio(1, false, true);
            v_roto.SetActive(true);
            v_normal.SetActive(false);
            v_Vivo = false;
            for (int i = 0;i< GetComponents<SphereCollider>().Length; i++)
            {
                GetComponents<SphereCollider>()[i].enabled = false;
            }
            GetComponent<NavMeshObstacle>().enabled = false;
            //Jug_Datos.Instance.Fn_SetMuerte();
            //Jug_Datos.Instance.Fn_SetPuntaje(Idioma.Scr_ManagerIdioma.instance.Fn_GetTexto("perdido_1") ) ;//"Objeto magico destruido"); ; ;
        }
        public override void Fn_SetDano(float _dano)
        {
            if (!v_Vivo)
                return;

            float resta = _dano - v_Def;
            if (resta <= 0)
            {
                return;
            }
            else
            {
                v_Vida -= resta;
                v_Vida = Mathf.Clamp(v_Vida, 0, v_VidaMax);
                v_audio.Fn_SetAudio(0, false, true);
                /*if (Player.instance.leftHand != null)//ACTUALIZA DATOS EN EL UI DE LA MANO
                {
                    if (SteamVR.instance != null)
                    {
                        if (v_IndexIzq == -1)
                            v_IndexIzq = (int)Player.instance.leftHand.GetComponentInChildren<SteamVR_RenderModel>().index;
                        //steamvr render model    index
                        SteamVR_Controller.Input(v_IndexIzq).TriggerHapticPulse(20000);
                    }
                    Player.instance.leftHand.GetComponent<UI_Datos>().Fn_SetPorcentaje((v_Vida/v_VidaMax)*100);
                    Player.instance.leftHand.GetComponent<UI_Datos>().Fn_SetDanoMagico(false);
                }
                if (Player.instance.rightHand && SteamVR.instance != null)
                {
                    if (v_IndexDer == -1)
                        v_IndexDer = (int)Player.instance.rightHand.GetComponentInChildren<SteamVR_RenderModel>().index;

                    SteamVR_Controller.Input(v_IndexDer).TriggerHapticPulse(20000);
                }

                if (v_Vida <= 0)
                {
                    if(Player.instance.leftHand)
                        Player.instance.leftHand.GetComponent<UI_Datos>().Fn_SetDanoMagico(true);

                    Fn_Muerto();
                }*/
            }
        }
    }
}
