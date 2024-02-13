using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;//para acceder directo al player
/*
 FORMATO QUE SE LEE DESDE EL TXT DE HORDA V1
 ( ENEMIGOS MOMIA)-(NUMERO DE VENTANAS ACTIVAS)/(JEFES MOMIA)
    FORMATO QUE SE LEE DESDE EL TXT DE HORDA V2
                                MINIONS            JEFES
 (NUMERO DE VENTANAS ACTIVAS)/(MOMIA-FANTASMA)/   (MOMIA-FANTASMA)
     */
using Jugador;
namespace Manager
{
    public class Manager_Horda : MonoBehaviour
    {
        #region PREFABS
        [Header("PREFAB MINION")]
        public GameObject[] v_Minions;
        [Header("PREFAB JEFES")]
        public GameObject[] v_Jefes;
        #endregion
        #region VARIABLES
        [Header("Tiempos y oleadas"), Tooltip("tiempo que esperas para la siguiente oleada")]
        /// <summary>
        /// tiempo que esperas para la siguiente oleada
        /// </summary>
        public float v_tiempo = 10;
        /// <summary>
        /// numero de la oleada actual
        /// </summary>
        public int v_numOleada = 1;
        public int v_maxOleada = 2;
        /// <summary>
        /// Cuantos vas matando actualmente
        /// </summary>
        public int v_muertes;
        /// <summary>
        /// maximo de enemigos activos en la oleada
        /// </summary>
        public int v_maxEnem = 5;
        /// <summary>
        /// enemigos que estan activos
        /// </summary>
        public List<GameObject> v_enem = new List<GameObject>();
        [Header("DATOS PARA CREAR ENEMIGOS")]
        /// <summary>
        /// el texto que tiene los datos donde se leen las cantidades 
        /// de enemigos ventanas
        /// </summary>
        public TextAsset v_texto;
        //se usa para leer el texto de la informacion
        public string[] v_datOleada;
        //mas datos en la lectura, enemigos y ventana
        public string[] _datos;
        //cantidad de enemigos por oleada
        public string[] _enem;
        //la cantidad de enemigos por oleada
        public string[] _jefes;
        protected bool v_EnJuego = false;
        private static Manager_Horda instance;
        public static Manager_Horda Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion
        /// <summary>
        /// solo es un seguro para que no agregue el numero de oleada muchas veces en un frame
        /// </summary>
        bool _puede = false;
        private void Awake()
        {
            if (v_Minions.Length < 1 || v_Jefes.Length < 1)
            {
                Debug.Break();
                Debug.LogError("Faltan Prefabas de los minions o jefes");
            }
            if (instance)
            {
                DestroyImmediate(gameObject);
            }
            if (!instance)
            {
                /*if (Player.instance.rightHand != null)//POR SI NO ESTA EL VIVE NO DE ERROR
                {
                    Player.instance.rightHand.GetComponent<UI_Arma>().FN_SetHorda(v_tiempo);
                }*/
                v_datOleada = v_texto.text.Split("\n"[0]);
                v_maxOleada = v_datOleada.Length + 1;
                v_numOleada = 1;
                v_muertes = 0;
                // v_tiempo = 10;
                v_maxEnem = -1;
                instance = this;
                //DontDestroyOnLoad(this);
                Fn_Descanso();
            }
        }
        #region FUNCIONES PARA LLAMAR DESDE OTROS LADOS Y QUE TE DEN ACCESO A LAS CORUTINAS
        /// <summary>
        /// LLAMA A LA CORUTINA DE DESCANSO
        /// </summary>
        public void Fn_Descanso()
        {
            //StartCoroutine(Ie_Descanso());
        }
        /// <summary>
        /// LLAMA A LA CORUTINA DE DESACTIVAR
        /// </summary>
        void Fn_Desactivar()
        {
            StartCoroutine(Ie_Desactivar());
        }
        /// <summary>
        /// LLAMA A LA CORUTINA DE ACTIVAR
        /// </summary>
        void Fn_Activar()
        {
            StartCoroutine(Ie_Activar());
        }
        /// <summary>
        /// LLAMA A LA CORUTINA DE INSTANCIAR
        /// </summary>
        public void Fn_Instanciar()
        {
            StartCoroutine(Ie_Instancia());
            //StartCoroutine(Ie_Instancia(v_maxEnem));
        }
        #endregion
        #region FUNCIONES PARA EFECTOS Y COMPORTAMIENTO DEL JUEGO
        /// <summary>
        /// cuando matas a un enemigo
        /// </summary>
        public void Fn_Incremento()
        {
            v_muertes++;//aumentar el numero de enemigos muertos
            /*if (Player.instance.rightHand != null)//actualizar los datos en la mano
            {
                Player.instance.rightHand.GetComponent<Jugador.UI_Arma>().FN_SetHorda(v_muertes, v_maxEnem, v_numOleada);
            }
            if (v_muertes >= v_maxEnem)//ya mataste a todos
            {
                if (!_puede)
                {
                    if (Player.instance.leftHand)
                        Player.instance.leftHand.GetComponent<UI_Datos>().Fn_MuestraAviso(8, 2);

                    v_numOleada++;
                    Fn_Desactivar();
                    v_muertes = 0;
                    _puede = true;
                }
            }**/
        }
        /// <summary>
        /// incremento de enemigos por ronda
        /// </summary>
        void Fn_Enemigos()
        {
            v_EnJuego = false;
            if(string.IsNullOrEmpty( v_datOleada[v_numOleada - 1]) || string.IsNullOrWhiteSpace(v_datOleada[v_numOleada - 1]))
            {
                Debug.LogError("TEMINADA ULTIMA OLEADA");
                //Jug_Datos.Instance.Fn_SetMuerte("");
            }
            else if (v_numOleada >= v_maxOleada)
            {
                Debug.LogError("TEMINADA ULTIMA OLEADA");
                //Jug_Datos.Instance.Fn_SetMuerte("");
            }
            else
            {
                _datos = v_datOleada[v_numOleada - 1].Split("/"[0]);// leer la info para la oleada actual
                int _ventana = int.Parse(_datos[0]);
                if(_ventana<1)
                {
                    _ventana = 1;
                }
                _enem = _datos[1].Split("-"[0]);//ventanas y enemigos momia
                int _tempo = 0;
                v_maxEnem = 0;
                for (int i = 0; i < _enem.Length; i++)
                {
                    _tempo = int.Parse(_enem[i]);
                    v_maxEnem += _tempo;
                }
                _jefes = _datos[2].Split("-"[0]);//numero de jefes     
                for (int i = 0; i < _jefes.Length; i++)
                {
                    _tempo = int.Parse(_jefes[i]);
                    if (i == 1)
                        v_maxEnem += (4 * (_tempo));
                    else
                        v_maxEnem += _tempo;
                }
                Manager_Ventanas.Instance.Fn_SetMax(_ventana);
            }
        }
        /// <summary>
        /// Ya acabaron de crearse todos los enemigos? 
        /// </summary>
        public bool FN_GetJuego()
        {
            return v_EnJuego;
        }
        public int Fn_GetMaxEnem()
        {
            return v_enem.Count;
        }
        /// <summary>
        /// lo usa el item de eliminar a todos
        /// </summary>
        public void Fn_MataTodo()
        {
            //print("mata todos");
            for (int i = 0; i < v_enem.Count; i++)//todos los enemigos 
            {
                v_enem[i].SendMessage("Dano", 10000, SendMessageOptions.DontRequireReceiver);//hacerles mucho daño 
            }
            v_EnJuego = false;
        }
        /// <summary>
        /// para usar en el efecto del jefe chupacabras
        /// </summary>
        public void Fn_AgregaEnemigos(GameObject _go)
        {
            if (!v_enem.Contains(_go))
            {
                v_enem.Add(_go);
            }
        }
        #endregion
        #region  corutinas
        /// <summary>
        /// tiempo libre entre cada horda
        /// </summary>
        /*IEnumerator Ie_Descanso()
        {
            v_EnJuego = false;
            StopCoroutine(Ie_Desactivar());
            /*if(Player.instance.GetComponent<Jug_Datos>().Fn_GetVivo())
            {
                if (v_numOleada >= v_maxOleada)
                {
                    Debug.LogError("TEMINADA ULTIMA OLEADA");
                    Jug_Datos.Instance.Fn_SetMuerte("");
                }
                else
                {
                    float _temp = v_tiempo;//CARGAR EL TIEMPO DE ESPERA
                    if (Player.instance.rightHand != null)//POR SI NO ESTA EL VIVE NO DE ERROR
                    {
                        Player.instance.rightHand.GetComponent<UI_Arma>().FN_SetHorda(_temp);
                    }
                    while (_temp > 0)
                    {
                        yield return new WaitForSeconds(1.0f);
                        _temp -= 1;//CONTADOR HACIA ATRAS
                        if (Player.instance.rightHand != null)
                        {
                            Player.instance.rightHand.GetComponent<UI_Arma>().FN_SetHorda(_temp);//ACTUALIZAR EL CONTADOR
                        }
                    }
                    if (Player.instance.leftHand)
                        Player.instance.leftHand.GetComponent<UI_Datos>().Fn_MuestraAviso(7, 2);

                    Fn_Enemigos();//NUEVA RONDA
                }
            }
            else
            {
                Debug.LogError("jugador muerto no carga nada");
            }
        }*/
        /// <summary>
        /// ELIMINAR TODOS LOS ENEMIGOS DELAA LISTA PARA NUEVAS REFERENCIA, LIMPIAR LA LISTA Y MANDAR AL DESCANSO
        /// </summary>
        IEnumerator Ie_Desactivar()
        {
            WaitForSeconds _wait = new WaitForSeconds(0.3f);
            for (int i = 0; i < v_enem.Count; i++)
            {
                yield return _wait;
                Destroy(v_enem[i]);
            }
            v_enem.Clear();
            yield return _wait;
            Fn_Descanso();
        }
        /// <summary>
        /// REVIVIR A LOS ENEMIGOS
        /// </summary>
        IEnumerator Ie_Activar()
        {
            for (int i = 0; i < v_enem.Count; i++)
            {
                v_enem[i].SetActive(true);
                v_enem[i].SendMessage("Fn_Revivir");
                yield return new WaitForSeconds(0.2f);
            }
        }
        int _num = 0;
        /// <summary>
        /// INSTANCIAR LOS NUEVOS ENEMIGOS Y JEFES
        /// </summary>   
        IEnumerator Ie_Instancia()//(float _largo)
        {
            WaitForSeconds delay = new WaitForSeconds(0.4f);
            GameObject _enemigo;
            Transform _padre = GameObject.Find("enemigos").transform;
            int _tempo = 0;
            for (int i = 0; i < _enem.Length; i++)
            {
                _tempo = int.Parse(_enem[i]);//el numero de enemigos de ese tipo
                for (int k = 0; k < _tempo; k++)
                {
                    GameObject _a = v_Minions[i];
                    _enemigo = Instantiate(v_Minions[i], Manager_Ventanas.Instance.Fn_GetPosRandom(), Quaternion.identity);
                    _enemigo.name = "min " + _num;
                    _num++;
                    _enemigo.SendMessage("ActualizarDestino", -2.0f);
                    _enemigo.transform.SetParent(_padre); v_enem.Add(_enemigo);
                    yield return delay;
                }
            }
            for (int i = 0; i < _jefes.Length; i++)
            {
                _tempo = int.Parse(_jefes[i]);
                for (int k = 0; k < _tempo; k++)
                {
                    Vector3 _posVen = Manager_Ventanas.Instance.Fn_GetPosRandom();
                    _enemigo = Instantiate(v_Jefes[i], _posVen, Quaternion.identity) as GameObject;
                    _enemigo.SendMessage("ActualizarDestino", -2.0f);
                    _enemigo.name = "boss " + _num;
                    _num++;
                    _enemigo.transform.SetParent(_padre);
                    _enemigo.SendMessage("Fn_Crea", _posVen, SendMessageOptions.DontRequireReceiver);
                    v_enem.Add(_enemigo);
                    yield return delay;
                }
            }
            _puede = false;
            yield return new WaitForSeconds(0.5f);
            v_EnJuego = true;
            //ACTUALIZAR LOS DATOS EN EL UI
            /*if (Player.instance.rightHand != null)
            {
                Player.instance.rightHand.GetComponent<UI_Arma>().FN_SetHorda(v_muertes, v_maxEnem, v_numOleada);
            }*/
        }
        #endregion
    }
}