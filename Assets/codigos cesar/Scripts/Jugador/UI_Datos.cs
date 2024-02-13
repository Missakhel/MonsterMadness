using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Jugador
{
    public class UI_Datos : MonoBehaviour
    {
        public GameObject[] v_avisos;
        public Text v_texto;
        public Text v_ObjPorcentaje;
        WaitForSeconds v_await;
        public Image v_img;
        public Color v_azul;
        public Color v_rojo;
        private void Awake()
        {
            v_await = new WaitForSeconds(0.3f);
            ColorUtility.TryParseHtmlString("#d45353", out v_rojo);
            ColorUtility.TryParseHtmlString("#10f9ff", out v_azul);
            v_img.color = v_azul;
        }
        void OnEnable()
        {
            for (int i = 0; i < v_avisos.Length; i++)
            {
                v_avisos[i].SetActive(false);
            }
            v_texto.text = "";
            v_texto.text = Jug_Datos.Instance.v_Porcen;
        }
        /// <summary>
        /// 0 dummy  1 moverse   2 disparo   3 sin balas   4 arma destruida melee   5 recargandp  6 terminado  7 oleada iniciada 8 oleada terminada
        /// </summary>
        public void Fn_MuestraAviso(int _index, float _tiempo)
        {
            StartCoroutine(Ie_Muestra(_index, _tiempo));
        }
        public void Fn_SetPorcentaje(float _val)
        {
            v_ObjPorcentaje.text = _val.ToString("F0") + " %";
        }
        /// <summary>
        /// 0 dummy  1 moverse   2 disparo   3 sin balas     4 arma destruida melee 5 recargandp  6 terminado
        /// </summary>
        public void Fn_MuestraAviso(bool _valo, int _index)
        {
            if (v_avisos[_index].activeInHierarchy != _valo)
                v_avisos[_index].SetActive(_valo);
        }
        IEnumerator Ie_Muestra(int _index, float _tiempo)
        {
            v_avisos[_index].SetActive(true);
            yield return new WaitForSeconds(_tiempo);
            v_avisos[_index].SetActive(false);
        }
        /// <summary>
        /// ACTUALIZAR INFO DE VENTANAS, ALMAS, VIDA, PUERTAS YCUALES ESTAN ROTAS
        /// </summary>
        public void Fn_Actualizar()
        {
            Vector2 _temp = Manager_Ventanas.Instance.Fn_GetInfo();
            v_texto.text = "";
            v_texto.text = "Vida: " + Jug_Datos.Instance.v_Porcen;
            //+"\n Puertas: " + _temp.y +
            //"\n Rota:" + _temp.x +
            //"\n Muertos:" + Manager_Horda.Instance.v_muertes + " / " + Manager_Horda.Instance.v_maxEnem;
        }
        public void Fn_SetDanoMagico(bool _val)
        {
            StartCoroutine(Ie_Tiempocolor(_val));
        }
        IEnumerator Ie_Tiempocolor(bool _val)
        {
            if (_val)
            {
                v_img.color = v_rojo;
            }
            else
            {
                v_img.color = v_rojo;
                yield return v_await;
                v_img.color = v_azul;
            }
        }
    }
}