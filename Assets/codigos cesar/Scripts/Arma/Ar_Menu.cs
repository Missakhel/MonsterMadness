using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Valve.VR.InteractionSystem;
namespace Armas
{
    public class Ar_Menu : MonoBehaviour
    {
        public GameObject[] v_Objs;
        //public GameObject v_centro;
        //public Hand v_R_hand;
        public Vector2 v_pos;
        public Vector2 v_temp;
        Manager.Ar_Manager v_manager;
        /// <summary>
        /// posicion del arreglo de botones
        /// </summary>
        public int v_index=-1;
        public Text v_text;
        public int v_IndexObli=-1;
        public UnityEngine.Events.UnityEvent v_extra;
        public Audio.Au_Manager v_audioMAn;
        public bool v_derecha=false;
        private void OnEnable()
        {
            v_audioMAn = GetComponent<Audio.Au_Manager>();
            v_audioMAn.Fn_Inicializa();
            v_audioMAn.Fn_SetAudio(0, false, true);//moverte entre las opciones
            v_pos = Vector2.zero;
            v_temp = Vector2.zero;
            //v_manager = Player.instance.rightHand.GetComponent<Manager.Ar_Manager>();
            
            for (int i = 0; i < v_Objs.Length; i++)
            {
                v_Objs[i].GetComponent<Collider>().enabled=!v_derecha;
            }
        }
        public void Fn_Actualiza(Manager.Ar_Manager _man)
        {
            v_manager= _man;
            for (int i = 0; i < v_Objs.Length; i++)
            {
                v_Objs[i].GetComponent<Ar_MenuSelec>().Fn_Actualiza();
            }
            if (v_IndexObli > -1)
            {
                v_Objs[v_IndexObli].GetComponent<Ar_MenuSelec>().Fn_Loop();
            }
        }
        public void Fn_SetObli(int _val)
        {
            v_IndexObli = _val;
        }
        public Manager.Ar_Manager Fn_GetManager()
        {
            return v_manager;
        }
        void Update()
        {
            /*if (v_R_hand == null)//SI NO HE AGREGADO LA MANO
            {
                v_R_hand = Player.instance.rightHand;//TOMO LA MANO DEL PREFAB PLAYER
            }
            else//YA TENGO MANO, PUEDO DISPARAR
            {//SOLO SE PUEDE DISPARAR SI NO ESTA COMPRANDO
                if (!Jugador.Jug_Datos.Instance.Fn_GetVivo())
                    return;

                if (SteamVR.instance != null && v_R_hand.controller != null)//SI ESTA EL VIVE
                {
                    if (v_IndexObli > -1)
                    {
                        v_Objs[v_IndexObli].GetComponent<Ar_MenuSelec>().Fn_Loop();
                    }
                    if(v_derecha)
                    {
                        v_temp = Fn_SetCoordenada(v_R_hand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0));

                        if (v_temp != v_pos)
                        {
                            if (v_temp != Vector2.zero)
                            {
                                v_audioMAn.Fn_SetAudio(0, false, true);//moverte entre las opciones
                            }
                            v_pos = v_temp;
                        }
                        for (int i = 0; i < v_Objs.Length; i++)
                        {
                            v_Objs[i].GetComponent<Ar_MenuSelec>().Fn_Color(0);
                            v_Objs[i].GetComponent<Ar_MenuSelec>().Fn_Select(v_manager.Fn_GetActual());
                        }
                        if (v_pos != Vector2.zero)// no esta en ceros
                        {
                            Fn_GetObj();
                            v_Objs[v_index].GetComponent<Ar_MenuSelec>().Fn_Color(1);
                            if (v_R_hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Axis0))
                            {
                                if (v_Objs[v_index].GetComponent<Ar_MenuSelec>().Fn_GetIndex() == -1)
                                {
                                    v_audioMAn.Fn_SetAudio(1, false, true); //"NO LA TIENES");
                                }
                                else
                                {
                                    if (v_index == v_IndexObli)
                                        v_extra.Invoke();

                                    Player.instance.rightHand.GetComponent<Audio.Au_Manager>().Fn_SetAudio(7, false, true);//cerrar el menu
                                    v_manager.Fn_Set(v_Objs[v_index].GetComponent<Ar_MenuSelec>().Fn_GetIndex());
                                    v_Objs[v_index].GetComponent<Ar_MenuSelec>().Fn_Color(2);//comprar
                                    gameObject.SetActive(false);
                                }
                            }
                        }
                    }//if para lo del cambio de arma

                    //else
                    //{
                        if (v_R_hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Axis0))
                        {
                            Player.instance.rightHand.GetComponent<Audio.Au_Manager>().Fn_SetAudio(7, false, true);//cerrar el menu
                            gameObject.SetActive(false);
                        }
                            v_text.text = "";
                        v_index = -1;
                        for(int i=0; i<v_Objs.Length; i++)
                        {
                            v_Objs[i].GetComponent<Ar_MenuSelec>().Fn_Color(-1);
                        }
                    //}
                }
                else//SIN EL VIVE
                {
                    
                }
            }*/
        }

        /// <summary>
        /// te dice en que casilla estas
        /// </summary>
        void Fn_GetObj()
        {
            if (v_pos.x == -1 && v_pos.y == 0)
            {
                v_index = 0;
            }
            else if (v_pos.x == -1 && v_pos.y == 1)
            {
                v_index = 1;
            }
            else if (v_pos.x == 0 && v_pos.y == 1)
            {
                v_index = 2;
            }
            else if (v_pos.x == 1 && v_pos.y == 1)
            {
                v_index = 3;
            }
            else if (v_pos.x == 1 && v_pos.y == 0)
            {
                v_index = 4;
            }
            else if (v_pos.x == 1 && v_pos.y == -1)
            {
                v_index = 5;
            }
            else if (v_pos.x == 0 && v_pos.y == -1)
            {
                v_index = 6;
            }
            else if (v_pos.x == -1 && v_pos.y == -1)
            {
                v_index = 7;
            }
            else
            {
                v_index = -1;
            }
        }
        Vector2 Fn_SetCoordenada(Vector2 _vec)
        {
            int _x, _y= 0;
            if (_vec.x < -0.3f)
                _x = -1;
            else if (_vec.x > 0.3f)
                _x = 1;
            else
                _x = 0;


            if (_vec.y < -0.3f)
                _y = -1;
            else if (_vec.y > 0.3f)
                _y = 1;
            else
                _y = 0;

            return new Vector2(_x, _y);
        }
    }
}
