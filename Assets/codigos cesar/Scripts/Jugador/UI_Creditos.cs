using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Valve.VR.InteractionSystem;
namespace Jugador
{
    public class UI_Creditos : MonoBehaviour
    {
        public GameObject v_panel;
        public Text v_texto;

        private void Awake()
        {
            v_panel.SetActive(false);
        }
        public void Fn_SetTexto(bool _val)
        {
            v_panel.SetActive(_val);
            //v_texto.text=Player.instance.GetComponent<Jug_Datos>().Fn_GetDatos().z.ToString("F0");
        }
    }
}