using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Manager
{
    using Ventanas;
    public class Manager_Ventanas : MonoBehaviour
    {
        #region VARIABLES GENERALES
        /// <summary>
        /// los originales
        /// </summary>
        public Transform[] v_SpawnTotal;
        /// <summary>
        /// temporales por cada ronda
        /// </summary>
        public List<Transform> v_posiciones = new List<Transform>();
        /// <summary>
        /// los originales
        /// </summary>
        public Ventana[] v_Ventanas;
        /// <summary>
        /// temporales por cada ronda
        /// </summary>
        public List<GameObject> v_desb = new List<GameObject>();
        /// <summary>
        /// NUMERO MAXIMO DE VENTANAS
        /// </summary>
        int v_maxVen;
        public GameObject Go_ObjetivoEnemigos;
        [Header("DISTANCIA DE LA VENTANA")][Range(1,10)]
        public float v_DistSpawn=0.0f;
        private static Manager_Ventanas instance;
        public static Manager_Ventanas Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion
        private void Awake()
        {
            if (instance)
            {
                DestroyImmediate(gameObject);
            }
            if (!instance)
            {
                instance = this;
                //DontDestroyOnLoad(this);
                Fn_SetPosiciones();
            }
        }
        private void Fn_SetPosiciones()
        {
            for (int i = 0; i < v_SpawnTotal.Length; i++)
            {
                v_SpawnTotal[i].position = v_Ventanas[i].transform.position+( v_Ventanas[i].GetComponent<Ventana>().v_frente.forward * v_DistSpawn);
            }
        }
        /// <summary>
        /// REGRESA VENTANAS ACTIVAS, ARREGLO GAMEOBJECT
        /// </summary>
        public GameObject[] Fn_GetVentanas()
        {
            return v_desb.ToArray();
        }
        /// <summary>
        /// CUANDO COMPRAS UNA VENTANA Y SU POSICION, DESBLOQUEARLA PARA SU USO
        /// </summary>
        public void AddVentana(int _comprada)
        {
            v_desb.Add(v_Ventanas[_comprada].gameObject);
            v_posiciones.Add(v_SpawnTotal[_comprada]);
        }
        /// <summary>
        /// REGRESA UNA POSCION DE LAS QUE A ESTAN ACTIVADAS
        /// </summary>
        public Vector3 Fn_GetPosRandom()
        {
            int _random = Random.Range(0, v_posiciones.Count);
            float x = Random.Range(v_posiciones[_random].position.x - 1.5f , v_posiciones[_random].position.x + 1.5f);
            float z = Random.Range(v_posiciones[_random].position.z - 1.5f, v_posiciones[_random].position.z+ 1.5f);
            Vector3 _pos = new Vector3(x, v_posiciones[_random].position.y + 1.0f, z);
            return _pos;
        }
        /// <summary>
        /// NUMERO DE VENTANAS ACTIVAS
        /// </summary>
        public int Fn_GetActivas()
        {
            return v_desb.Count;
        }
        /// <summary>
        ///X NUMERO DE ROTAS,   Y NUMERO DE ACTIVAS
        /// </summary>
        public Vector2 Fn_GetInfo()
        {
            float _rota = 0;
            for (int i = 0; i < v_desb.Count; i++)
            {
                if (v_desb[i].GetComponent<Ventana>().Fn_GRota())
                    _rota++;
            }
            return new Vector2(_rota, Fn_GetActivas());
        }
        /// <summary>
        /// PARA ACTIVAR EL BOTON EN CADA VENTANA DE REPARAR TODAS LAS VENTANAS, SI HAY MAS DE UNA SI SE PUEDE REPARAR
        /// </summary>
        public bool Fn_GetRotas()
        {
            int _rota = 0;
            for (int i = 0; i < v_desb.Count; i++)
            {
                if (v_desb[i].GetComponent<Ventana>().Fn_GRota())
                {
                    _rota++;
                }
            }
            if (_rota > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public C_Ventana Fn_GetNoRotas(bool _val)
        {
            C_Ventana _ven = new C_Ventana();
            for (int i = 0; i < v_desb.Count; i++)
            {
                if (!v_desb[i].GetComponent<Ventana>().Fn_GRota())
                {
                    _ven.v_ven.Add(v_desb[i]);
                    _ven.v_pos.Add(v_posiciones[i]);
                }
            }
            if(_ven.v_ven.Count>1)
            {
                int _rand = Random.Range(0, _ven.v_ven.Count);
                return new C_Ventana() { v_pos = new List<Transform>() { _ven.v_pos[_rand] }, v_ven = new List<GameObject>() { _ven.v_ven[_rand] } };
            }
            else
            {
                return _ven;
            }
        }
        /// <summary>
        /// El fantasma la usa para moverse a una nueva NO ROTA
        /// </summary>
        public bool Fn_GetNoRotas()
        {
            int _rota = 0;
            for (int i = 0; i < v_desb.Count; i++)
            {
                if (!v_desb[i].GetComponent<Ventana>().Fn_GRota())
                {
                    _rota++;
                }
            }
            if (_rota > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// REPARAR TODAS LAS VENTANAS
        /// </summary>
        public void Fn_ReparaToda()
        {
            for (int i = 0; i < v_desb.Count; i++)
            {
                if (v_desb[i].GetComponent<Ventana>().Fn_GRota())
                {
                    v_desb[i].GetComponent<Ventana>().Fn_Repara();
                }
            }
        }
        /// <summary>
        /// lo llama el managerhorda, se hace la nueva lista de ventanas y posiciones activas
        /// </summary>
        public void Fn_SetMax(int _max)
        {
            v_desb.Clear();
            v_posiciones.Clear();
            //v_maxVen = _max;
            //for (int i = 0; i < v_Ventanas.Length; i++)//AGREGAR LAS VENTANAS Y SUS POSICIONES, TOOOOODDDAAAAASSS
            //{
            //    v_desb.Add(v_Ventanas[i].gameObject);
            //    v_posiciones.Add(v_SpawnTotal[i]);
            //}
            //int _temp = -1;
            //int _long = (v_Ventanas.Length - v_maxVen);//TOTALES MENOS LA QUE PIDE LOS DATOS DE LA HORDA
            //for (int i = 0; i < _long; i++)
            //{
            //    _temp = Random.Range(0, v_desb.Count);//DE TODAS UNA RANDOM
            //    v_desb.Remove(v_desb[_temp]);//QUITAR LA VENTANA
            //    v_posiciones.Remove(v_posiciones[_temp]);//QUITAR SU POSICION
            //}
            //if(Manager_Horda.Instance)
            //    Manager_Horda.Instance.Fn_Instanciar();

            v_desb = new List<GameObject>();
            v_posiciones = new List<Transform>() ;
            C_Ventana _rotas = new C_Ventana();
            for(int i=0; i< v_Ventanas.Length; i++)
            {
                if(v_Ventanas[i].Fn_GRota())
                {
                    _rotas.v_pos.Add(v_SpawnTotal[i]);
                    _rotas.v_ven.Add(v_Ventanas[i].gameObject);
                }
            }
            //Debug.LogError("hay " + _rotas.v_ven.Count+" rotas");
            if(_rotas.v_ven. Count> _max)//tengop mas de lo que deberia
            {
                int _maxBusca = (_rotas.v_ven.Count - _max);///numero de ventanas que tengo que quitar del final               
               // Debug.LogError("se deben quitar  " + _maxBusca);
                for(int i=0; i< _maxBusca; i++)
                {
                    _rotas.v_ven.Remove(_rotas.v_ven[Random.Range(0, _rotas.v_ven.Count )]);
                    _rotas.v_pos.Remove(_rotas.v_pos[Random.Range(0, _rotas.v_pos.Count )]);
                }
            }
            else
            {
                int _maxBusca = (_max- _rotas.v_ven.Count );///numero de ventanas que tengo que agregar              
              //  Debug.LogError("se deben agregar  " + _maxBusca);
                int _valWhile = 0;
                int _valRandom = 0;
                while(_valWhile< _maxBusca)
                {
                    _valRandom = Random.Range(0, v_Ventanas.Length);
                    if(!_rotas.v_ven.Contains( v_Ventanas[_valRandom].gameObject))
                    {
                       // Debug.LogError("agregando  " +_valRandom+" con nombre"+ v_Ventanas[_valRandom].name);
                        _rotas.v_ven.Add( v_Ventanas[_valRandom].gameObject );
                        _rotas.v_pos.Add( v_SpawnTotal[_valRandom] );
                        _valWhile++;
                    }
                }
            }
            v_desb = _rotas.v_ven;
            v_posiciones = _rotas.v_pos;
            if (Manager_Horda.Instance)
                Manager_Horda.Instance.Fn_Instanciar();
        }
        /// <summary>
        /// indice de la ventana mas cercana
        /// </summary>
        public int Fn_GetCercana(float _x, float _z)
        {
            float distancia = float.MaxValue;
            float temp = 0;
            int indice = -1;
            Vector2 _vec2 = Vector2.zero;
            Vector2 _vec = new Vector2(_x, _z);
            Vector3 posCercana = Vector3.zero;
            //Busamos la ventana más cercas
            for (int i = 0; i < v_desb.Count; i++)
            {//para que no cuenta como distancia lo del eje de hacia aarriba segun su pivote,solo cuenta xz
                _vec2 = new Vector2(v_desb[i].transform.position.x, v_desb[i].transform.position.z);
                //_vec2.Set(v_desb[i].transform.position.x, v_desb[i].transform.position.z);
                //Obtenemoso el más cercano
                temp = Vector2.Distance(_vec, _vec2);
                if (temp > 0 && temp < distancia)
                {
                    posCercana = v_desb[i].transform.position;
                    distancia = temp;
                    indice = i;
                }
            }
            //return v_desb[i]
            return indice;
        }
        public Vector3 Fn_GetPosSpawn(int _ind)
        {
            if (_ind < 0 || _ind > v_SpawnTotal.Length)
                return Vector3.zero;
            else
                return v_SpawnTotal[_ind].position;
        }
    }
}