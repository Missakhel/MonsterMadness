using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Armas;
namespace Jugador
{
    public class UI_Arma : MonoBehaviour
    {
        public Text text_arma;
        Color v_rojo;
        Color v_azul;
        [Header("DATOS DE HORDA")]
        [Tooltip("DATOS DE LA HORDA ")]
        public Text text_Horda;
        public GameObject v_PanelTiempo;
        public Text text_Tiempo;
        private void Awake()
        {
            v_PanelTiempo.SetActive(false);
            text_arma.text = "";
            if (text_Horda != null)
                text_Horda.text = "";
            if (text_Tiempo != null)
                text_Tiempo.text = "";
            ColorUtility.TryParseHtmlString("#d45353", out v_rojo);
            ColorUtility.TryParseHtmlString("#10f9ff", out v_azul);
        }
        /// <summary>
        /// MOSTRAR LA INFO DEL ARMA ACTUAL
        /// </summary>
        public void Fn_Set(Arma _arma)
        {
            //Vector3 _vec = _arma.Fn_getBalas();
            //if ( (_vec.y-1) < _vec.z)
            //{
            //    text_arma.color = v_rojo;
            //}
            //else
            //{
            //    text_arma.color = v_azul;
            //}
            //text_arma.text ="(" +_vec.x + " / " + _vec.y + ")/ <color=green>"+_vec.z+"</color>";
        }
        /// <summary>
        /// TIEMPO RESTANTE ANTES DE QUE ACABE EL DESCANSO
        /// </summary>
        public void FN_SetHorda(float _tiempo)
        {
            if(_tiempo<1)
            {
                //text_Tiempo.gameObject.SetActive(false);
                v_PanelTiempo.SetActive(false);
            }
            else
            {
                v_PanelTiempo.SetActive(true);
                //text_Tiempo.gameObject.SetActive(true);
                if(_tiempo<6)
                {
                    text_Tiempo.color =Color.red;
                }
                else
                {
                    text_Tiempo.color = v_azul ;
                }
                text_Tiempo.text = _tiempo.ToString("F0");
            }
        }
        /// <summary>
        /// DURANTE LA PARTIDA, MUERTES QUE LLEVAS, MAXIMO DE ENEMIGOS Y OLEADA ACTUAL
        /// </summary>
        public void FN_SetHorda(int _muertes, int _maxenem, int _numOle)
        {
            text_Horda.text = _numOle.ToString();
        }
    }
}