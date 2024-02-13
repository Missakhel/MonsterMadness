using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Idioma
{
    public class Scr_Idioma : MonoBehaviour
    {
       public string v_key= "";
        /*
        [Tooltip("CAMBIO DE IDIOMA AUTOMATICO")]
        public bool v_auto=true;
        private void Awake()
        {
            if(v_variante.Length==0)
            {
                Debug.Break();
                Debug.LogError("Faltan variantes de idioma en "+ gameObject.name);
            }
        }*/
        private void OnEnable()
        {
            if (Scr_ManagerIdioma.instance == null)
            {
                Debug.LogError("no esta el manager de idiomas");
                Debug.Break();
                return;
            }
            if (Scr_ManagerIdioma.instance.v_disp)
            {
                Fn_SetIdioma();
            }
        }
        public void Fn_SetIdioma()
        {
            if (Scr_ManagerIdioma.instance.v_disp)
            {
                string _tex = Scr_ManagerIdioma.instance.Fn_GetTexto(v_key);
                if (GetComponent<Text>() && _tex != null)
                    GetComponent<Text>().text = _tex;
            }
        }
        public void Fn_SetKey(string _key)
        {
            v_key = _key;
            Fn_SetIdioma();
        }
        public void Fn_SetIdioma(string _key,string _id)
        {
            if (Scr_ManagerIdioma.instance.v_disp)
            {
                string _tex = Scr_ManagerIdioma.instance.Fn_GetTexto(_key + _id);
                if (GetComponent<Text>() && _tex != null)
                    GetComponent<Text>().text = _tex;
            }
        }
        /*public void Fn_SetIdioma(int _idnuevo)
        {
            for (int i = 0; i < v_variante.Length; i++)
            {
                v_variante[i].SetActive(false);
            }
            if (_idnuevo < 0)
                _idnuevo = 0;

            v_variante[_idnuevo].SetActive(true);
        }*/
    }
}
