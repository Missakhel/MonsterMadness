using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Idioma
{
    public class Scr_ManagerIdioma : MonoBehaviour
    {
        public TextAsset[] v_idiomas;
        public Dictionary<string, string> v_dic;
        public int v_idIdioma=-1;
        public bool v_disp = false;
        public C_Idioma[] _DictIdioma;
        public static Scr_ManagerIdioma instance { get; private set; }
        private void Awake()
        {
            if (instance != null)
            {
                enabled = false;
                DestroyImmediate(this);
                return;
            }
            DontDestroyOnLoad(this);
            instance = this;
            Fn_Carga();
            //foreach (KeyValuePair<string, string> kvp in v_dic)
            //{
            //    Debug.LogError("Key =" + kvp.Key + " Value = " + kvp.Value);
            //}
        }
        public void Fn_CambioIdioma(int _valor)
        {
            Letras.Fn_SetInt(Letras.v_idioma, _valor);
            Letras.Fn_SetInt(Letras.v_Noidioma, _valor);
            Fn_Carga();
        }
        public int Fn_GetIdioma()
        {
            if (v_idIdioma < 0)
                v_idIdioma = 0;
            return v_idIdioma;
        }
        private void Fn_Carga()
        {
            v_disp = false;
            v_dic = new Dictionary<string, string>();
            v_idIdioma = Letras.Fn_GetValor(Letras.v_idioma,1);//  PlayerPrefs.GetInt("idioma");
            if (v_idIdioma < 0)
                v_idIdioma = 0;
            // Debug.LogError(v_idiomas[v_idIdioma].text);

            C_IdiomaCollection _col= JsonUtility.FromJson<C_IdiomaCollection>(v_idiomas[v_idIdioma].text);
            _DictIdioma = _col.info;

            for (int i = 0; i < _DictIdioma.Length; i++)
            {
                v_dic.Add(_DictIdioma[i].v_key, _DictIdioma[i].v_value);
            }


            //string[] _arr = v_idiomas[v_idIdioma].text.Split(']');
            //string[] _text;
            //for (int i = 0; i < _arr.Length - 1; i++)
            //{
            //    _text = _arr[i].Split('&');
            //    if (_text.Length == 2)
            //    {
            //        string _st = _text[0].Replace("\r\n", "");
            //        v_dic.Add(_st, _text[1]);
            //    }
            //}
            v_disp = true;
            Scr_Idioma[] _idio = FindObjectsOfType<Scr_Idioma>();
            for (int i = 0; i < _idio.Length; i++)
            {
                _idio[i].Fn_SetIdioma();
            }
        }
        public string Fn_GetTexto(string _key)
        {
            if (string.IsNullOrEmpty(_key) || string.IsNullOrWhiteSpace(_key))
            {
                return "";
            }
            string _ret = string.Empty;
            if (v_dic.ContainsKey(_key))
            {
                v_dic.TryGetValue(_key, out _ret);
                return _ret;
            }
            else
                return "";
        }
    }
    [System.Serializable]
    public class C_Idioma
    {
        public string v_key;
        public string v_value;
        public C_Idioma ()
        {
            v_key = "";
            v_value = "";
        }
    }
    [System.Serializable]
    public class C_IdiomaCollection
    {
        public C_Idioma[] info;
    }
}