using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
namespace Armas
{
    using Jugador;
    public class Arma : MonoBehaviour
    {
        #region VARIABLES
        [Header("POOL")]
        public List<GameObject> v_pool = new List<GameObject>();
        public int v_idPool = -1;
        protected int v_MaxPool { get => 12; }
        public int v_PrecioBalas;
        public int v_PrecioDesbloqueo;
        public float v_Rango = 1;
        public float v_Dano = 1;
        public bool v_Melee = false;
        /// <summary>
        /// tiempo a esperar cuando se sobrecalienta
        /// </summary>
        public float v_TimepoRecarga;
        public bool v_Recargando;
        WaitForSeconds v_await;
        public bool v_puededisparar;
        protected Transform v_SaleBala;
        public GameObject v_prefBala;
        #region CALENTAMIENTO
        public int v_pila;
        public int v_MaxPila;
        public float v_MaxCalenta;
        public float v_AumeCalenta;
        public float v_DisminCalenta;
        public float v_Calentamiento;
        /// <summary>
        /// saber si esta disparando
        /// </summary>
        public bool v_disparo;
        /// <summary>
        /// numero de disparos para sobrecalentarse
        /// </summary>
        public int v_contador;
        [Header("SLIDER CALENTAMIENTO")]
        public UnityEngine.UI.Image _slider;
        public UnityEngine.UI.Text v_texto;
        #endregion
        #endregion

        #region FUNCIONES GENERALES
        public virtual void Fn_Iniciar()
        {

        }
        protected void Fn_SetInit(float _maxca, float _aumen, float _dism, int _pila)
        {
            v_MaxCalenta = _maxca;
            v_AumeCalenta = _aumen;
            v_DisminCalenta = _dism;
            v_Calentamiento = 0;
            v_MaxPila = _pila;
            v_pila = v_MaxPila;
            Fn_SetCalenta(-1);
            if(v_texto!= null)
                v_texto.text =v_pila +" / "+v_MaxPila;
        }
        public bool Fn_Melee()
        {
            if (v_Recargando)
            {
                /*if (Player.instance.leftHand)
                    Player.instance.leftHand.GetComponent<UI_Datos>().Fn_MuestraAviso(5, 2);*/

                return false;
            }
            else
            {
                if (v_pila > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool Fn_PuedeDisparar()
        {
            /*if (v_Recargando)
            {
                if (Player.instance.leftHand)
                    Player.instance.leftHand.GetComponent<UI_Datos>().Fn_MuestraAviso(5, 2);

                Player.instance.GetComponent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
                return false;
            }
            else
            {
                if (v_Calentamiento>=v_MaxCalenta)//no balas 
                {
                    if (Player.instance.leftHand)
                        Player.instance.leftHand.GetComponent<UI_Datos>().Fn_MuestraAviso(3, 2);
                
                    Player.instance.GetComponent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
                    v_puededisparar = false;
                }
                else
                {
                    if (v_pila < 1)// no tengo balas que disparar
                    {
                        Player.instance.GetComponent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
                        v_puededisparar = false;
                    }
                    else
                    {
                        //print("disparo");
                        v_puededisparar = true;
                    }
                }
            }*/
            return v_puededisparar;
        }
        protected void Fn_SetDisparo(bool _dis, int _val )
        {
            v_disparo = _dis;
            if (v_disparo)
                StopAllCoroutines();

            v_pila -= _val;
            v_texto.text = v_pila + " / "+ v_MaxPila;
        }
        /// <param name="_val"> cuanto baja la pila</param>
        protected void Fn_SetDisparo(bool _dis )
        {
            v_disparo = _dis;
            if (v_disparo)
                StopAllCoroutines();

            v_pila -= 1;
            v_texto.text = v_pila+" / "+v_MaxPila;
        }
        /// <summary>
        /// revisa el calentamiento y si es que recarga o maximo balas
        /// </summary>
        protected void Fn_Revisa()
        {
            if (v_Calentamiento >= v_MaxCalenta )
            {
                Fn_Enfriar();
            }
            else
            {
                Fn_EnfriarAuto();
            }
            if (v_idPool >= v_MaxPool)
                v_idPool = 0;
        }
        /// <summary>
        /// -1 porcentaje de calentamiento
        /// </summary>
        protected void Fn_SetCalenta(int _val)
        {
            if(_val == -1)
            {
                if (_slider != null)
                    _slider.fillAmount = (v_Calentamiento / v_MaxCalenta);
            }
            else
            {
                if (_slider != null)
                    _slider.fillAmount = _val;
            }
        }
        protected void Fn_EnfriarAuto()
        {
            StartCoroutine(Ie_Auto());
        }
        private IEnumerator Ie_Auto()
        {
            v_await = new WaitForSeconds((v_TimepoRecarga / v_MaxPool));
            v_disparo = false;
            while (!v_disparo )
            {
                if(v_Calentamiento>1)
                {
                    v_Calentamiento -= (v_DisminCalenta) ;
                    Fn_SetCalenta(-1);
                    yield return v_await;   
                }
                else
                {
                    v_Calentamiento = 0;
                    Fn_SetCalenta(-1);
                    v_disparo = true;
                }
            }
            StopAllCoroutines();
        }
        public void Fn_RecogeMunicion(int _val)
        {
            v_pila += _val;
            v_pila = Mathf.Clamp(v_pila, 0, v_MaxPila);
            v_texto.text = v_pila + " / " + v_MaxPila;
            if (v_Calentamiento >= v_MaxCalenta)
            {
                Fn_Enfriar();
            }
        }
        /// <summary>
        /// automatico recarga todo
        /// </summary>
        public virtual void Fn_RecogeMunicion()
        {
            v_pila = v_MaxPila;
            v_texto.text = v_pila + " / "+v_MaxPila;
            if (v_Calentamiento >= v_MaxCalenta )
            {
                Fn_Enfriar();
            }
        }
        protected void Fn_Enfriar()
        {
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(En_Enfriar());
            }
            //if (v_pila>0)   //(v_BalasCargador < v_MaxCargador && v_BalasActuales > 0)//  && !v_Recargando)
            //{
            //    if (gameObject.activeInHierarchy)
            //    {
            //        StartCoroutine(En_Enfriar());
            //    }
            //}
            //else
            //{
            //    Debug.LogError("no pila "+ v_pila);
            //}
        }
        public virtual void Fn_Desactivar()
        {
            v_puededisparar = false;
            StopCoroutine(En_Enfriar());
            gameObject.SetActive(false);
        }
        public void Fn_SetAumento()
        {
            v_Calentamiento += v_AumeCalenta;
            Fn_SetCalenta(-1);
        }
        public void Fn_Activar()
        {
            Fn_SetCalenta(-1);
            if(v_texto!= null)
                v_texto.text = v_pila + " / " + v_MaxPila;
            gameObject.SetActive(true);
            Fn_GetPool();
            v_puededisparar = false;
            if (v_Recargando == true)
            {
                Fn_Enfriar();
            }
        }
        public bool Fn_Recargando()
        {
            return v_Recargando;
        }
        #endregion

        #region Funciones Get
        public Vector3 Fn_GetSaleBala()
        {
            return v_SaleBala.position;
        }
        public int Fn_GetCostoPorcentaje()
        {
            if(v_pila<v_MaxPila)
            {
                return Mathf.FloorToInt(((v_MaxPila - v_pila) * v_PrecioBalas) / v_MaxPila);
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// v_PrecioDesbloqueo, v_PrecioBalas
        /// </summary>
        public Vector2 Fn_GetCosto()
        {
            return new Vector2(v_PrecioDesbloqueo, v_PrecioBalas);
        }
        /// <summary>
        /// pila   maxpila
        /// </summary>
        public Vector2 Fn_GetPila()
        {
            return new Vector2(v_pila, v_MaxPila);
        }
        public void Fn_GetPool()
        {
            if (v_pool.Count < 1)
            {
                Fn_Pool();
            }
        }
        /// <summary>
        /// rango dano  
        /// </summary>
        public Vector2 Fn_getDatos()
        {
            return new Vector3(v_Rango, v_Dano);
        }
        #endregion
        #region Corutinas
        IEnumerator En_Enfriar()
        {
            v_puededisparar = false;
            v_Recargando = true;
            //reiniciar pool
            v_idPool = 0;
            if (v_pool.Count > 0)
            {
                for (int i = 0; i <  v_MaxPool; i++)  ///v_MaxCargador; i++)
                {
                    v_pool[i].transform.position = new Vector3(0, 100, 0);
                }
            }
            v_await = new WaitForSeconds(v_TimepoRecarga);/// v_MaxPool); 
                yield return v_await;

            v_Calentamiento = 0.0f;
            _slider.fillAmount = 0.0f;
            v_puededisparar = true;
            v_Recargando = false;
            StopCoroutine(En_Enfriar());
        }
        #endregion
        #region Sobreescribir
        public virtual void Fn_Press() { }
        public virtual void Fn_Down() { }
        public virtual void Fn_Up() { }
        public virtual void Fn_Pool() { }
        #endregion
        #region Funciones Set de la bala y mira
        /*public void Fn_SetSale()
        {
            v_SaleBala = Valve.VR.InteractionSystem.Player.instance.rightHand.transform;
        }
        protected bool Fn_Mira()
        {
            //Evitamos que jugador pueda travesar paredes al cruzar brazo y teleptransportar asi
            RaycastHit hit2;
            //Validamos que sea punto alcanzable 
            //Lanzamos un rayo de la capsula al control si impacta en pared, no es valido
            Vector3 rayCast2Dir = transform.position - Player.instance.transform.position;
            float rayCast2Dist = rayCast2Dir.magnitude; //Almacenamos distancia
            rayCast2Dir.Normalize(); //Normalizamos para ser dir
                                     //Ignoramos Jugador
            int capa2 = 1 << k.Layers.PLAYER;
            capa2 |= (1 << k.Layers.MANO);
            capa2 = ~capa2; //Ignorar

            if (Physics.Raycast(Player.instance.transform.position, rayCast2Dir, out hit2, rayCast2Dist, capa2))
            {
                //Si impacta en algo, no es valido
                Debug.LogError("NO DISPARA PORQUE PEGA CON ALGO " + hit2.collider.gameObject.name, hit2.collider.gameObject);
                Fn_MostrarAviso(true);
                return false;
            }
            else
            {
                return true;
            }
        }
        private void Fn_MostrarAviso(bool _Valor)
        {
            if(Player.instance.leftHand!= null){Player.instance.leftHand.GetComponent<UI_Datos>().Fn_MuestraAviso( 2,3);}
        }*/
        #endregion
        /*
        public float v_Rango=1;
        public float v_Dano=1;
        public float v_TimepoRecarga;
        public int v_PrecioBalas;
        public int v_PrecioDesbloqueo;
        public bool v_puededisparar;
        public int v_MaxCargador=10;
        public int v_MaxBalas = 20;
        /// <summary>
        /// balas totales que tengo disponibles
        /// </summary>
        public int v_BalasActuales= 100;
        /// <summary>
        /// balas que tengo en el cargador ahora para disparar
        /// </summary>
        public int v_BalasCargador=20;
        public bool v_Recargando;
        public bool v_Melee=false;
        protected Transform v_SaleBala;
        protected List<GameObject> v_pool= new List<GameObject>();
        public GameObject v_prefBala;
        protected int v_idPool=-1;
        WaitForSeconds v_await;
        #region Funciones Generales
        public bool Fn_PuedeDisparar()
        {
            if (v_Recargando)
            {
                if(Player.instance.leftHand)
                    Player.instance.leftHand.GetComponent<UI_Datos>().Fn_MuestraAviso(5, 2);

                return false;
            }
            else
            {
                if (v_BalasActuales == 0 && v_BalasCargador == 0)
                {
                    // print("arma vacia");d45353
                    if(Player.instance.leftHand)
                        Player.instance.leftHand.GetComponent<UI_Datos>().Fn_MuestraAviso(3, 2);

                    v_puededisparar = false;
                }
                else
                {
                    if (v_BalasCargador < 1)// no tengo balas que disparar
                    {
                     //  print("no tengo balas en cargador");
                        v_puededisparar = false;
                       // Fn_Recargar();
                    }
                    else
                    {
                        //print("disparo");
                        v_puededisparar = true;
                   
                        //v_BalasCargador -= 1;
                        ////por si me quedo sin balas despues del disparo
                        //if (v_BalasCargador == 0 && v_BalasActuales > 0)
                        //{
                        //    print("sin balas despues del disparo");
                        //    print("cargador " + v_BalasCargador + " actuales " + v_BalasActuales);
                        //    v_puededisparar = false;
                        //    Fn_Recargar();
                        //}
                        //else
                        //{
                        //    print("con  balas despues del disparo no recarga");
                        //}
                    }
                }
            }
            return v_puededisparar;
        }
        public void Fn_RecogeMunicion(int cantidad)
        {
            Debug.LogError("actuales "+ v_BalasActuales);
            v_BalasActuales += cantidad;
            v_BalasActuales = Mathf.Clamp(v_BalasActuales, 1, v_MaxBalas);
            Debug.LogError("NUEVAS "+ v_BalasActuales);
            Fn_Recargar();
        }
        public void Fn_Recargar()
        {
            if (v_BalasCargador< v_MaxCargador && v_BalasActuales>0 )//  && !v_Recargando)
            {
                if(gameObject.activeInHierarchy)
                {
                    StartCoroutine(En_Recargar());
                }
            }
        }
        public void Fn_Desactivar()
        {
            v_puededisparar = false;
            StopCoroutine(En_Recargar());
            gameObject.SetActive(false);
        }
        public void Fn_Activar()
        {
            gameObject.SetActive(true);
            v_await = new WaitForSeconds(v_TimepoRecarga);
            Fn_GetPool();
            v_puededisparar = false;
            if(v_Recargando==true)
            {
                Fn_Recargar();
            }
        }
        public bool Fn_Recargando()
        {
            return v_Recargando;
        }
        #endregion
        #region Funciones Set
        public void Fn_SetSale()
        {
            v_SaleBala = Valve.VR.InteractionSystem.Player.instance.rightHand.transform;
        }
        #endregion
        #region Corutinas
        IEnumerator En_Recargar()
        {
            v_puededisparar = false;
            v_Recargando = true;
            //reiniciar pool
            v_idPool = 0;
            if(v_pool.Count>0)
            {
                for (int i = 0; i < v_MaxCargador; i++)
                {
                    v_pool[i].transform.position = new Vector3(0, 100, 0);
                }
            }
            yield return v_await;
            int necesito = v_MaxCargador-v_BalasCargador ;//el numero que le voy a quitar y le voy a sumar a los actuales
            //print("necesito "+ necesito);
            if (v_BalasActuales- necesito >= 0 )// me sobran balas extras o me quedo en 0
            {
                v_BalasActuales -= necesito;
                v_BalasCargador += necesito; 
              //  print("me sobran balas actuales"+ v_BalasActuales+"  cargador"+v_BalasCargador);
            }
            else
            {
             //   print("tengo " + v_BalasActuales + "  necesito " + necesito);
                v_BalasCargador += v_BalasActuales;
                v_BalasActuales = 0;

               // print("me quedo en cargador " + v_BalasCargador + " actuales " + v_BalasActuales);
            }
            v_puededisparar = true;
            v_Recargando = false;
            StopCoroutine(En_Recargar());
        }    
        #endregion
        #region Funciones Get
        public Vector3 Fn_GetSaleBala()
	    {
		    return v_SaleBala.position;
	    }
        public int Fn_GetMaxBalas()
        {
            return v_MaxBalas;
        }
        public int Fn_GetMaxCargador()
        { return v_MaxCargador; }
        public Vector2 Fn_GetCosto()
        {
            return new Vector2( v_PrecioDesbloqueo, v_PrecioBalas);
        }
        public void Fn_GetPool()
        {
            if(v_pool.Count<1)
            {
                Fn_Pool();
            }
        }
        /// <summary>
        /// rango dano  
        /// </summary>
        public Vector2 Fn_getDatos()
        {
            return new Vector3(v_Rango, v_Dano);
        }
        /// <summary>
        /// v_BalasCargador   v_BalasActuales  Maxcargador
        /// </summary>
        public Vector3 Fn_getBalas()
        {
            return new Vector3(v_BalasCargador,v_BalasActuales, v_MaxCargador);
        }
    #endregion
        #region Sobreescribir
        public virtual void Fn_Press(){ }
        public virtual void Fn_Down(){}
        public virtual void Fn_Up(){ }
        public virtual void Fn_Pool() { }
        #endregion
        //private void OnDrawGizmosSelected()
        //{
        //    Gizmos.DrawLine(Player.instance.rightHand.transform.position, Player.instance.rightHand.transform.forward);
        //}
        protected bool Fn_Mira()
        {
            //Evitamos que jugador pueda travesar paredes al cruzar brazo y teleptransportar asi
            RaycastHit hit2;
            //Validamos que sea punto alcanzable 
            //Lanzamos un rayo de la capsula al control si impacta en pared, no es valido
            Vector3 rayCast2Dir = transform.position - Player.instance.transform.position;
            float rayCast2Dist = rayCast2Dir.magnitude; //Almacenamos distancia
            rayCast2Dir.Normalize(); //Normalizamos para ser dir
                                     //Ignoramos Jugador
            int capa2 = 1 << k.Layers.PLAYER;
            capa2 |= (1 << k.Layers.MANO);
            capa2 = ~capa2; //Ignorar

            if (Physics.Raycast(Player.instance.transform.position, rayCast2Dir, out hit2, rayCast2Dist, capa2))
            {
                //Si impacta en algo, no es valido
                Debug.LogError("NO DISPARA PORQUE PEGA CON ALGO "+ hit2.collider.gameObject.name, hit2.collider.gameObject);
                Fn_MostrarAviso(true);
                return false;
            }
            else
            {
                return true;
            }
        }*/    
    }
}