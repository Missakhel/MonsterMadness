using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
//using Valve.VR;
using UnityEngine.Events;

namespace Tutorial
{
    public class Scr_Detector : MonoBehaviour
    {
        public bool v_handIzq = true;
        bool v_pos=false;
        public bool v_auto=true;
        public UnityEvent v_event;
        bool v_activo = false;
        public GameObject v_panelInfo;
        private void OnEnable()
        {
            v_pos = false;
            Fn_Panel(true);
            StartCoroutine(Ie_Delay());
        }
        private void Update()
        {
            /*if (v_pos)
            {
                if (ControllerButtonHints.IsButtonHintActive(Scr_Instru.Instance.Fn_GetHand(v_handIzq), EVRButtonId.k_EButton_SteamVR_Touchpad))
                {
                    ControllerButtonHints.HideButtonHint(Scr_Instru.Instance.Fn_GetHand(v_handIzq), EVRButtonId.k_EButton_SteamVR_Touchpad);
                }
                if(v_auto)
                {
                    Scr_Instru.Instance.Fn_Siguiente(1);
                }
            }
            else
            {
                if (SteamVR.instance != null && Scr_Instru.Instance.Fn_GetHand(v_handIzq).controller != null)
                {
                    if (!ControllerButtonHints.IsButtonHintActive(Scr_Instru.Instance.Fn_GetHand(v_handIzq), EVRButtonId.k_EButton_SteamVR_Touchpad))
                    {
                        ControllerButtonHints.HideAllButtonHints(Scr_Instru.Instance.Fn_GetHand(v_handIzq));
                        ControllerButtonHints.ShowButtonHint(Scr_Instru.Instance.Fn_GetHand(v_handIzq), EVRButtonId.k_EButton_SteamVR_Touchpad);
                    }
                    else
                    {
                        if(Scr_Instru.Instance.Fn_GetIndex(v_handIzq) != -1)
                        {
                            SteamVR_Controller.Input(Scr_Instru.Instance.Fn_GetIndex(v_handIzq)).TriggerHapticPulse(20000);
                        }
                    }
                }
            }*/
        }
        public void Fn_Sig()
        {
            Scr_Instru.Instance.Fn_Siguiente(1);
        }
        IEnumerator Ie_Delay()
        {
            v_activo = false;
            yield return new WaitForSeconds(2.0f);
            v_activo = true;
            StopCoroutine(Ie_Delay());
        }
        private void Fn_Panel(bool _val)
        {
            if (v_panelInfo != null)
                v_panelInfo.SetActive(_val);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == k.Tags.PLAYER)
            {
                v_pos = true;
                v_event.Invoke();
            }
        }
    }
}
