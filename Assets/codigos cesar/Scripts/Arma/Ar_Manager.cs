using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Manager
{
    using Armas;
    [RequireComponent(typeof(Audio.Au_Manager))]
    public class Ar_Manager : MonoBehaviour {
        /// <summary>
        /// las armas que tienes en uso
        /// </summary>
        public List<Arma> v_Armas = new List<Arma>();
        public int v_ArmaActual = 0;
        /// <summary>
        /// las armas originales que no se deben eliminar
        /// </summary>
        public List<Arma> v_originales = new List<Arma>();
        public Jugador.UI_Arma v_ui;
        private WaitForSeconds v_await;
        private void Awake()
        {
            if (v_Armas.Count <= 0)
            {

                //Debug.Break();
                //Debug.LogError("EN ARMAS DEBE HABER ALGUNA ARMA QUE EL JUGADOR YA PUEDE UTILIZAR");
                //return;
                v_ArmaActual = -1;
            }
            else
            {
                for (int i = 0; i < v_Armas.Count; i++)
                {
                    v_Armas[i].gameObject.SetActive(true);
                    v_Armas[i].Fn_Iniciar();
                    v_Armas[i].Fn_Desactivar();
                }
                v_Armas[v_ArmaActual].Fn_Activar();
            }
            GetComponent<Audio.Au_Manager>().Fn_Inicializa();
            v_await = new WaitForSeconds(0.3f);
            StartCoroutine(E_Loop());
        }
        IEnumerator E_Loop()
        {
            while (true)
            {
                yield return v_await;
                if(v_ArmaActual>-1)
                    v_ui.Fn_Set(v_Armas[v_ArmaActual]);
            }
        }
        public void Fn_Down()
        {
            if(v_Armas.Count>0)
            {
                //v_Armas[v_ArmaActual].Fn_SetSale();
                v_Armas[v_ArmaActual].Fn_Down();
            }
        }
        public void Fn_Press()
        {
            if (v_Armas.Count > 0)
            {
                v_Armas[v_ArmaActual].Fn_Press();
            }
        }
        public void Fn_Up()
        {
            if (v_Armas.Count > 0)
            {
                v_Armas[v_ArmaActual].Fn_Up();
            }
        }
        public void Fn_Eliminar(System.Type _borrar)
        {
            //Debug.LogError("manager " + _borrar);
            Arma _temp;
            for (int i = 0; i < v_Armas.Count; i++)
            {
                if (v_Armas[i].GetType() == _borrar)
                {
                    _temp = v_Armas[i];
                    v_Armas[i].Fn_Desactivar();
                    v_Armas.Remove(_temp);
                    Fn_GetUltimo(); //Cambiamos a ultima arma posible
                    break; //Salimos de for
                }
            }
        }
        public void Fn_Comprar(System.Type _nueva)
        {
            bool _enc = false;
            if(!Fn_Existe(_nueva))
            {
                for (int i = 0; i < v_originales.Count; i++)
                {
                    if (_enc == false && (v_originales[i].GetType() == _nueva) )
                    {
                        _enc = true;
                        v_Armas.Add(v_originales[i]);
                        Fn_GetUltimo(true).Fn_Iniciar() ;
                        //Debug.LogError("agrega " + v_originales[i].name);
                        Fn_GetUltimo();
                        return;
                    }
                }
            }
            else { Debug.LogError("YA TIENE ESA ARMA"); }
        }
        public void Fn_ComprarRecarga(System.Type _nueva)
        {
            bool _enc = false;
            for (int i = 0; i < v_Armas.Count; i++)
            {
                if (_enc == false && (v_Armas[i].GetType() == _nueva))
                {
                    _enc = true;
                   // Debug.LogError("Municion de " + v_Armas[i].GetType());
                    v_Armas[i].GetComponent<Arma>().Fn_RecogeMunicion();
                    Fn_Set(i);
                }
            }
        }
        public int Fn_GetCostoPorcentaje(System.Type _val)
        {
            bool _enc = false;
            int _precio = -1;
            for (int i = 0; i < v_Armas.Count; i++)
            {
                if (_enc == false && (v_Armas[i].GetType() == _val))
                {
                    _enc = true;
                    _precio= v_Armas[i].GetComponent<Arma>().Fn_GetCostoPorcentaje();
                    _precio = Mathf.FloorToInt(_precio);
                }
            }
            return _precio;
        }
        public Arma Fn_GetTipo(System.Type _arma)
        {
            bool _enc = false;
            Arma _ret = null ;
            for (int i = 0; i < v_Armas.Count; i++)
            {
                if (_enc == false && (v_Armas[i].GetType() == _arma))
                {
                    _enc = true;
                    _ret = v_Armas[i];
                }
            }
            return _ret;
        }
        public Arma Fn_GetActual()
        {
            if (v_Armas.Count > 0)
                return v_Armas[v_ArmaActual];
            else
                return null;
        }
        public int Fn_GetIdActual()
        {
            return v_ArmaActual;
        }
        public void Fn_Siguiente()
        {
            if(v_Armas.Count==1)
            {
                return;
            }
            if(v_ArmaActual == v_Armas.Count-1 && v_Armas.Count >= 2)
            {
                v_Armas[v_ArmaActual].Fn_Desactivar();
                v_ArmaActual=0;
                v_Armas[v_ArmaActual].Fn_Activar();
            }
            else
            {
                v_Armas[v_ArmaActual].Fn_Desactivar();
                v_ArmaActual++;
                v_Armas[v_ArmaActual].Fn_Activar();
            }
        }
        public void Fn_Anterior()
        {
            if (v_Armas.Count == 1)
            {
                return;
            }
            if (v_ArmaActual ==0 && v_Armas.Count>=2)
            {
                v_Armas[v_ArmaActual].Fn_Desactivar();
                v_ArmaActual = v_Armas.Count-1;
                v_Armas[v_ArmaActual].Fn_Activar();
            }else
            {
                v_Armas[v_ArmaActual].Fn_Desactivar();
                v_ArmaActual -=1;
                v_Armas[v_ArmaActual].Fn_Activar();
            }
        }
        //public void Fn_Recargar()
        //{
        //    v_Armas[v_ArmaActual].Fn_Recargar();
        //}
        public void Fn_RecogeMunicion()
        {
            v_Armas[v_ArmaActual].Fn_RecogeMunicion();
        }
	    public Vector3 Fn_GetSaleBala()
	    {
		    return v_Armas [v_ArmaActual].Fn_GetSaleBala();
	    }
        public void Fn_Set(int _val)
        {
            v_Armas[v_ArmaActual].Fn_Desactivar();
            v_ArmaActual = _val;
            v_Armas[v_ArmaActual].Fn_Activar();
        }
        public Arma Fn_GetUltimo(bool _val)
        {
            if (v_Armas.Count > 1)
{
v_ArmaActual=v_Armas.Count - 1;
return v_Armas[v_Armas.Count - 1];
}
                
            else
                return null;
        }
        public void Fn_GetUltimo()
        {
            if(v_Armas.Count>0)
            {
                for(int i= 0; i<v_Armas.Count; i++)
                {
                    v_Armas[i].Fn_Desactivar();

                }
                v_ArmaActual = v_Armas.Count - 1;
                v_Armas[v_ArmaActual].Fn_Activar();
            }
            else
            {
                v_ArmaActual = -1;
                //v_ArmaActual = v_Armas.Count - 1; //0 o -1
                //v_Armas[v_ArmaActual].Fn_Activar();
            }
           /*
            despues de comprar el arma te aparece el enemigo y destruye tu ventana
            de ahi reparas la ventana rota
            se le avisa que se le va a hacer daño para que pueda usar la otra arma(curar)
            obligarlo a usar el menu  de armas para curarse
            que se cure y ya seguimos con lo del final
            */
        }
        /// <summary>0 no la tiene, la debe comprar
        /// 1 ya tiene al maximo
        /// 2 le faltan balas comprarlas
        /// </summary>
        public bool Fn_Existe(System.Type _nueva)
        {
            //int _indice = -1;
            bool _enc = false;
            for (int i = 0; i < v_Armas.Count; i++)
            {
                if (!_enc && (v_Armas[i].GetType() == _nueva))
                {
                    _enc = true;
                }
            }
            return _enc;
            //if(!_enc)
            //    return 0;
            //else
            //{
            //    if(v_Armas[_indice].Fn_getBalas() .y>= (v_Armas[_indice].v_MaxBalas -2) )
            //    {
            //        return 1;
            //    }
            //    else
            //    {
            //        return 2;
            //    }
            //}
            //return 1;
        }
        public int Fn_GetArma(System.Type _arma)
        {
            int _val = -1;
            bool _enc=false;
            for(int i=0; i<v_Armas.Count; i++)
            {
                if(!_enc&& v_Armas[i].GetType() == _arma)
                {
                    _val = i;
                    _enc = true;
                }
            }
            return _val;
        }
        public Vector2 Fn_GetArma(int _index, string a)
        {
            return v_Armas[_index].Fn_GetPila();
        }
    }
}
