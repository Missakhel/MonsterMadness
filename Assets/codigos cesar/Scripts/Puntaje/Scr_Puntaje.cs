using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Puntaje
{
    [RequireComponent(typeof(Audio.Au_Manager))]
    public class Scr_Puntaje : MonoBehaviour
    {
        public List<C_Puntaje> v_lista = new List<C_Puntaje>();
        public C_PuntajeVisual[] v_Visual;
        C_PuntajeCollection v_colect = new C_PuntajeCollection();
        C_Puntaje v_puntaje = new C_Puntaje();
        Audio.Au_Manager v_audio;
        //[Header("TITULOS")]
        //public Text v_oleada;
        //public Text v_almas;
        //public Text v_muerte;
        /// <summary>
        /// usa para reset, el texto que dice que no hay puntos
        /// </summary>
        public GameObject v_Obj;
        void Awake()
        {
            v_audio = GetComponent<Audio.Au_Manager>();
            if (v_audio)
                v_audio.Fn_Inicializa();

            if (v_Obj)
                v_Obj.SetActive(false);
            if (Letras.Fn_GetValor(Letras.v_puntaje) == "")
            {
                v_lista = new List<C_Puntaje>();
                v_colect.puntajes = v_lista.ToArray();
                string _json = JsonUtility.ToJson(v_colect);
                Letras.Fn_SetString(Letras.v_puntaje, _json);
            }
            v_colect = JsonUtility.FromJson<C_PuntajeCollection>(Letras.Fn_GetValor(Letras.v_puntaje));
            v_lista = new List<C_Puntaje>(v_colect.puntajes);
            //Fn_Set(Random.Range(1, 10000), Random.Range(1, 100), "autmatico");
            //Fn_Set(Random.Range(1, 10000), Random.Range(1, 100), "autmatico");
            //Fn_Set(Random.Range(1, 10000), Random.Range(1, 100), "autmatico");
            //Fn_Set(Random.Range(1, 10000), Random.Range(1, 100), "autmatico");
            //Fn_Set(Random.Range(1, 10000), Random.Range(1, 100), "autmatico");
            //Fn_Set(Random.Range(1, 10000), Random.Range(1, 100), "autmatico");
            Fn_Muestra();
        }
        void Fn_Muestra()
        {
            //if (v_oleada != null & v_almas != null && v_muerte != null)
            //{
                //v_oleada.text = "<color=red>" + Idioma.Scr_ManagerIdioma.instance.Fn_GetTexto("puntaje_2") + "</color>";
                //v_almas.text = "<color=teal>" + Idioma.Scr_ManagerIdioma.instance.Fn_GetTexto("puntaje_3") + "</color>";
                //v_muerte.text = "<color=lightblue>" + Idioma.Scr_ManagerIdioma.instance.Fn_GetTexto("puntaje_7") + "</color>";
            if(v_Visual!= null  && v_Visual.Length>0)
            {
                for (int i = 0; i < v_lista.Count; i++)
                {
                    //v_oleada.text += "\n " + v_lista[i].v_numOleada;
                    //v_almas.text += "\n " + v_lista[i].v_fecha;
                    //v_muerte.text += "\n " + v_lista[i].v_muerte;
                    v_Visual[i].Fn_Set( v_lista[i].v_numOleada.ToString(), v_lista[i].v_muerte, v_lista[i].v_fecha);
                    //v_oleada.text += "\n " + v_lista[i].v_numOleada;
                    //v_almas.text += "\n " + v_lista[i].v_fecha;
                    //v_muerte.text += "\n " + v_lista[i].v_muerte;
                    //sdsfds
                }
            }
            //}
            //else
            //{ Debug.LogError("Falta un texto"); }
        }
        public void Fn_Condicion(GameObject _obj)
        {
            if (v_Obj)
                v_Obj.SetActive(false);
            if (v_lista.Count > 0)
            {
                v_audio.Fn_SetAudio(0,false,true);
                _obj.SetActive(true);
            }
            else
            {
                if (v_Obj)
                    v_Obj.SetActive(true);
                Debug.LogError("No hay puntajes");
                v_audio.Fn_SetAudio(1,false,true);
            }
        }
        public void Fn_Reset()
        {
            if (v_Obj)
                v_Obj.SetActive(false);
            for (int i = 0; i < v_lista.Count; i++)
            {
                //v_oleada.text += "\n " + v_lista[i].v_numOleada;
                //v_almas.text += "\n " + v_lista[i].v_fecha;
                //v_muerte.text += "\n " + v_lista[i].v_muerte;
                v_Visual[i].Fn_Set("","","");
                //v_oleada.text += "\n " + v_lista[i].v_numOleada;
                //v_almas.text += "\n " + v_lista[i].v_fecha;
                //v_muerte.text += "\n " + v_lista[i].v_muerte;
                //sdsfds
            }
            v_lista.Clear();
            v_colect.puntajes = v_lista.ToArray();
            string _json = JsonUtility.ToJson(v_colect);
            Letras.Fn_SetString(Letras.v_puntaje, _json);
            Fn_Muestra();
        }
        public string Fn_Set(int _almas, int _oleada, string _str)
        {
            v_puntaje = new C_Puntaje() { v_almas = _almas, v_numOleada = _oleada, v_muerte = _str,
                v_fecha = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") };
            string _punt = JsonUtility.ToJson(v_puntaje);
            if (v_lista.Count > 0)
            {
                if(v_lista.Count>=2)//9)
                {
                    //if (v_lista.Last().v_almas < v_puntaje.v_almas)
                    if (v_lista.Last().v_numOleada < v_puntaje.v_numOleada)
                    {
                        v_lista.Add(v_puntaje);
                        //IEnumerable<C_Puntaje> _as = v_lista.OrderByDescending(x => x.v_almas);
                        IEnumerable<C_Puntaje> _as = v_lista.OrderByDescending(x => x.v_numOleada);
                        v_lista = new List<C_Puntaje>(_as);
                        if (v_lista.Count >= 2)//9)//borrar los que se pasaen solo quedan 10
                        {
                            //v_lista.RemoveRange(10, (v_lista.Count -10));
                            v_lista.RemoveRange(3, (v_lista.Count -3));
                            v_colect.puntajes = v_lista.ToArray();
                            string _json = JsonUtility.ToJson(v_colect);
                            Letras.Fn_SetString(Letras.v_puntaje, _json);
                        }
                        else
                        {
                            v_colect.puntajes = v_lista.ToArray();
                            string _json = JsonUtility.ToJson(v_colect);
                            Letras.Fn_SetString(Letras.v_puntaje, _json);
                        }
                    }
                    else//no alcanza al ultimo lugar
                    {
                    }
                }
                else//todavia no hay 10
                {
                    v_lista.Add(v_puntaje);
                    //IEnumerable<C_Puntaje> _as = v_lista.OrderByDescending(x => x.v_almas);
                    IEnumerable<C_Puntaje> _as = v_lista.OrderByDescending(x => x.v_numOleada);
                    v_lista = new List<C_Puntaje>(_as);
                    v_colect.puntajes = v_lista.ToArray();
                    string _json = JsonUtility.ToJson(v_colect);
                    Letras.Fn_SetString(Letras.v_puntaje, _json);
                }
            }
            else//no hay ningun registro
            {
                v_lista.Add(v_puntaje);
                v_colect.puntajes = v_lista.ToArray();
                string _json = JsonUtility.ToJson(v_colect);
                Letras.Fn_SetString(Letras.v_puntaje, _json);
            }
            return "aaa";
        }
    }
    [System.Serializable]
    public class C_Puntaje
    {
        public int v_almas;
        public int v_numOleada;
        public string v_muerte;
        public string v_fecha;
        public override string ToString()
        {
            return "almas " + v_almas + "  " + v_numOleada + " " + v_muerte;
        }
    }
    [System.Serializable]
    public class C_PuntajeCollection
    {
        public C_Puntaje[] puntajes;
    }
}
