using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Tutorial
{
    public class Scr_IndiceEnem : MonoBehaviour
    {
        [Header("NORMAL/ARMA")]
        public Text v_nombre;
        public Text v_tipo;
        [Tooltip("el unico que tiene el item")]
        public Text v_tipoataque;
        //public Text v_infoNormal;
        [Header("JEFE")]
        //public GameObject v_panelJefe;
        public Text v_efecto;
        public void Fn_Set(Tutorial.C_Indice  _indice, bool _Armas)
        {
            if(!_Armas)
            {
                v_nombre.text = _indice.v_nombre;
                v_tipo.text = _indice.v_tipo;
                //v_infoNormal.text = _indice.v_infonormal;
                v_tipoataque.text = _indice.v_tipoataque;
                if (_indice.v_jefe)
                {
                    //v_panelJefe.SetActive(true);
                    v_efecto.text = _indice.v_infoEfecto;
                }
            }
            else
            {
                v_nombre.text = Idioma.Scr_ManagerIdioma.instance.Fn_GetTexto(_indice.v_nombre);
                v_tipo.text = _indice.v_InfoExtra;
                v_tipoataque.text = _indice.v_infoEfecto ;//el unico que tiene el item
            }
            //else
            //{
            //    v_panelJefe.SetActive(false);
            //}
        }
    }
}
