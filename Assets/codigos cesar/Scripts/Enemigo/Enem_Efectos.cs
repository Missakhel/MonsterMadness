using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
namespace Enemigos
{
    [RequireComponent(typeof( Au_Manager))]
    public class Enem_Efectos : MonoBehaviour
    {
        public Au_Manager v_audio;
        public SkinnedMeshRenderer v_mesh;
        WaitForSeconds v_await;
        /// <summary>
        /// color de daño
        /// </summary>
        //public Color v_color;

        public List<C_Efecto> v_lista= new List<C_Efecto>();

        C_Efecto v_dano= new C_Efecto() { v_tiempo = 0.3f };
        C_Efecto v_congela = new C_Efecto() { v_color = Color.cyan, v_tiempo = 5.0f };
        public  void Fn_Inicializa()
        {
            v_audio = GetComponent<Au_Manager>();
            v_dano = new C_Efecto() { v_tiempo = 0.3f };
            v_congela = new C_Efecto() { v_color = Color.cyan, v_tiempo = 5.0f };
            if (!ColorUtility.TryParseHtmlString("#d45353", out v_dano.v_color ))//v_color))
                v_dano.v_color = Color.green;

            v_lista.Add(new C_Efecto() { v_color = Color.white, v_indice = 0, v_congela=false });
        }
        public void Fn_Dano()
        {
            v_dano.v_tiempofin = (v_dano.v_tiempo+ Time.time);
            v_dano.v_congela = false;
            v_lista.Add(v_dano);
            Fn_ColorEfecto(v_dano);//    JsonUtility.ToJson(new C_Efecto() { v_color = v_color, v_tiempo = 0.3f, v_nombre="Dano", v_indice=1 })  );
        }
        public void Fn_Congela()
        {
            v_congela.v_color = Color.cyan;
            v_congela.v_tiempofin = (v_congela.v_tiempo + Time.time);
            v_congela.v_congela = true;
            v_lista.Add(v_congela);
            Fn_ColorEfecto(v_congela);//  JsonUtility.ToJson(new C_Efecto() { v_color = Color.cyan, v_tiempo =5.0f,v_nombre="Congela" , v_indice=2 }));
        }
        void Fn_Color(C_Efecto _val)
        {
            if(gameObject.activeInHierarchy)
            {
                bool _enc = false;
                int _ind = 0;
                for (int i = 0; i < v_lista.Count; i++)
                {
                    if (v_lista[i].v_tiempofin == _val.v_tiempofin && !_enc)
                    {
                        _enc = true;
                        _ind = i;
                    }
                }

                if (_enc)
                {
                    v_lista.RemoveAt(_ind);
                    if (v_lista.Count > 1)
                    {
                        v_mesh.material.SetColor("_Color", v_lista[1].v_color);
                        v_mesh.material.SetColor("_EmissionColor", v_lista[1].v_color);
                        GetComponent<Enemigo_base>().Fn_CongelaDetiene( v_lista[1].v_congela);
                    }
                    else
                    {
                        if (!v_audio.Fn_GetPlaying())
                            v_audio.Fn_SetAudio(0, true, true);


                        v_mesh.material.SetColor("_Color", v_lista[0].v_color);
                        v_mesh.material.SetColor("_EmissionColor", v_lista[0].v_color);
                        GetComponent<Enemigo_base>().Fn_CongelaDetiene(v_lista[0].v_congela);
                    }
                }
                else
                    Debug.LogError("NO ENCONTRADO");
            
            }
            //if(v_lista.Count>1)
            //{
            //    v_lista.Remove(_val);
            //    v_lista.RemoveAt(1);
            //    if (v_lista.Count > 1)
            //    {
            //        v_mesh.material.SetColor("_Color", v_lista[1].v_color);
            //        v_mesh.material.SetColor("_EmissionColor", v_lista[1].v_color);
            //    }
            //    else
            //    {
            //        v_mesh.material.SetColor("_Color", v_lista[0].v_color);
            //        v_mesh.material.SetColor("_EmissionColor", v_lista[0].v_color);
            //    }
            //}
            //else
            //{
            //    v_mesh.material.SetColor("_Color", v_lista[1].v_color);
            //    v_mesh.material.SetColor("_EmissionColor", v_lista[1].v_color);
            //}
        }
        IEnumerator Ie_Tiempocolor(   C_Efecto _val) ///string _va)//  Color _color)
        {
            ///tiene que parar su audio que esta en loop

            //C_Efecto _efe = JsonUtility.FromJson<C_Efecto>(_val);
            v_await = new WaitForSeconds(_val.v_tiempo);

            /// .SetColor(_Color
            if (_val.v_color == Color.cyan)
                v_audio.Fn_Stop();

            v_mesh.material.color = _val.v_color;  //_color; //v_color;
            v_mesh.material.SetColor("_EmissionColor", _val.v_color);// _color);
            yield return v_await;
            Fn_Color(_val);
            //v_mesh.material.color = Color.white;
            //v_mesh.material.Se



            //C_Efecto _efe = JsonUtility.FromJson<C_Efecto>(_va);
            //v_await = new WaitForSeconds(_efe.v_tiempo);

            /// .SetColor(_Color

            //v_mesh.material.color = _efe.v_color;  //_color; //v_color;
            //v_mesh.material.SetColor("_EmissionColor", _efe.v_color);// _color);
            //Debug.LogError("inicia " + _efe.v_nombre);
            //yield return v_await;
            //Debug.LogError("Termina "+ _efe.v_nombre);
            //v_mesh.material.color = Color.white;
            //v_mesh.material.SetColor("_EmissionColor", Color.white);
        }
        public void Fn_ColorEfecto(C_Efecto _val)   //string _info)// Color _col)
        {
            StartCoroutine(Ie_Tiempocolor(_val));     ///_info));//   _col)) ;
        }
    }
    [System.Serializable]
    public class C_Efecto 
    {
        public Color v_color;
        public float v_tiempo;
        public float v_tiempofin;
        /// <summary>
        /// que tipo de efecto, 0 dano 1 conge
        /// </summary>
        public int v_indice;
        public bool v_congela;
        public C_Efecto()
        {
            v_color = Color.white;
            v_tiempo = 0.3f;
            v_indice = 0;
            v_tiempofin = 0;
            v_congela = false;
        }
    }
}
