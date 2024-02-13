using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace INTERFAZVR
{
    public class Ifz_MenuInicio : MonoBehaviour
    {
        public GameObject v_confirma;
        public GameObject v_PanelIdioma;
        public GameObject v_Puntaje;
        private void Awake()
        {
            //Debug.LogError(Letras.Fn_GetValor(Letras.v_Noidioma, 1));
            if(Letras.Fn_GetValor(Letras.v_Noidioma,1) ==-1)
            {
                v_PanelIdioma.SetActive(true);
                v_Puntaje.SetActive(false);
                GetComponent<Ifz_Confirma>().Fn_Collider(false);
            }
            else
            {
                v_Puntaje.SetActive(true);
                v_PanelIdioma.SetActive(false);
            }

            //PlayerPrefs.DeleteKey(Letras.v_primera);
        }
        public void Fn_Confirma()
        {
            if (Letras.Fn_GetValor(Letras.v_primera, 8) != -1)
            {//ya tiene algo guardado
                v_confirma.SetActive(false);
                GetComponent<Itz_MenuJuego>().Fn_Escena("Scene_StageTest 1");
            }
            else//la primera vez que entra
            {
                GetComponent<Ifz_Confirma>().Fn_Collider(false);
                v_confirma.SetActive(true);
            }
        }
        public void Fn_Sonido(int _ind)
        {
            //Valve.VR.InteractionSystem.Player.instance.GetComponent<Audio.Au_Manager>().Fn_SetAudio(_ind, false, true);
        }
    }
}
