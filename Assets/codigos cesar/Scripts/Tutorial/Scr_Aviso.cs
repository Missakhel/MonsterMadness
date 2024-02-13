using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
//using Valve.VR;
using UnityEngine.Events;
namespace Tutorial
{
    public class Scr_Aviso : MonoBehaviour
    {
        public bool v_handIzq = true;
        //public EVRButtonId v_btn;
        //public EVRButtonId v_btnAccion;
        public GameObject[] v_objs;
        public GameObject v_panel;
        public UnityEvent v_event;
        bool v_activo = false;
        [Tooltip("PARA USAR EL ARMA QUE CURA")]
        public bool v_disparo;
        private void OnEnable()
        {
            if(v_panel!= null)
                v_panel.SetActive(true);
            Fn_Objetos(false);
            StartCoroutine(Ie_Delay());
        }
        public void Fn_Objetos(bool _val)
        {
            for (int i = 0; i < v_objs.Length; i++)
            {
                v_objs[i].SetActive(_val);
            }
        }
        public void Fn_Panel(bool _val)
        {
            if (v_panel != null)
                v_panel.SetActive(_val);
        }
        void Update()
        {
            /*if (v_activo)
            {
                if (SteamVR.instance != null && Scr_Instru.Instance.Fn_GetHand(v_handIzq).controller != null && gameObject.activeInHierarchy)
                {
                    if (!ControllerButtonHints.IsButtonHintActive(Scr_Instru.Instance.Fn_GetHand(v_handIzq), v_btn))
                    {
                        ControllerButtonHints.HideAllButtonHints(Scr_Instru.Instance.Fn_GetHand(v_handIzq));
                        ControllerButtonHints.ShowButtonHint(Scr_Instru.Instance.Fn_GetHand(v_handIzq), v_btn);
                    }
                    else
                    {
                        if (Scr_Instru.Instance.Fn_GetIndex(v_handIzq) != -1)
                        {
                            SteamVR_Controller.Input(Scr_Instru.Instance.Fn_GetIndex(v_handIzq)).TriggerHapticPulse();
                        }
                    }


                    if (
                        Scr_Instru.Instance.Fn_GetHand(v_handIzq).controller.GetPressDown( v_btnAccion)   &&
                        ControllerButtonHints.IsButtonHintActive(Scr_Instru.Instance.Fn_GetHand(v_handIzq), v_btn)
                        )
                    {
                        if(!v_disparo)
                            v_event.Invoke();
                        else
                        {
                            if(Player.instance.rightHand.GetComponent<Manager.Ar_Manager>().Fn_GetActual().GetType()  == typeof(Armas.Ar_Cura) )
                            {
                                v_event.Invoke();
                            }
                            else///no es el arma correcta
                            {
                                Player.instance.GetComponent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
                            }
                        }
                    }
                }
                else
                {
                    if (Scr_Instru.Instance.Fn_GetHand(v_handIzq).GetStandardInteractionButtonDown())
                    {
                        v_event.Invoke();
                    }
                }
            }
            else
            {//quitar todos
                ControllerButtonHints.HideAllButtonHints(Player.instance.leftHand);
                ControllerButtonHints.HideAllButtonHints(Player.instance.rightHand);
            }*/
        }
        IEnumerator Ie_Delay()
        {
            v_activo = false;
            yield return new WaitForSeconds(2.0f);
            v_activo = true;
            StopCoroutine(Ie_Delay());
        }
        public void Fn_Sig()
        {
            Fn_Apaga();
            Scr_Instru.Instance.Fn_Siguiente(1);
        }
        void Fn_Apaga()
        {
            //ControllerButtonHints.HideAllButtonHints(Scr_Instru.Instance.Fn_GetHand(v_handIzq));
            Fn_Objetos(false);
            v_activo = false;
            if (v_panel != null)
                v_panel.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
