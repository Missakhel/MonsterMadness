using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//para el obstaculo
//using UnityEngine.UI;
using NaughtyAttributes;
using INTERFAZVR;
using Jugador;
namespace Ventanas
{
    // typeof(Tienda.Ti_Ventana),
    [RequireComponent(typeof(NavMeshObstacle), typeof(Audio.Au_Manager))]
    public class Ventana : MonoBehaviour
    {
        
        /// <summary>
        /// maximo de vida por si algun power up
        /// </summary>
        public float v_MaxVida;
        /// <summary>
        /// vida actual
        /// </summary>
        public float v_Vida;
        /// <summary>
        /// defensa para disminuir
        /// </summary>
        public float v_Defensa;
        /// <summary>
        /// si ya se rompio los enemigos la atraviesan
        /// </summary>
        public bool v_Rota;
        /// <summary>
        /// al comprar el area es true
        /// </summary>
        public bool v_activa;
        /// <summary>
        /// particulas de prueba para muerte
        /// </summary>
        public GameObject part;
        //[ReadOnly]
        /// <summary>
        /// si se rompe, mandarle el mensaje a todos los que le han pegado
        /// </summary>   
        public List<GameObject> enemigos;
        /// para usar bien el match target animator
        //public Transform v_medio;

        public Transform[] v_PosArr;
        [Header("VIDAS")]
        #region PARA EL SLIDER 
        //Tienda.Ti_Ventana v_ventana;
        public Ifz_Damage v_Slider;
        public Ifz_Damage v_SliderEscudo;
        //public bool v_escudo=false;
        public float v_vidaEsc;

        #endregion
        //public GameObject v_Muro;
        public Transform v_frente;

        [Header("NUEVA VERSION VENTANA")]
        public MeshRenderer v_mesh;
        NavMeshObstacle v_navmesh;
        public Tienda.Ti_ventanaTi v_boton;



        [Header("DEBUG DISTANCIA")]
        public float v_rango;
        public Vector3 v_rangoVec;

        private void Awake()
        {
            //v_ventana = GetComponent<Tienda.Ti_Ventana>();
            //v_boton = GetComponent<Tienda.Ti_ventanaTi>();
            v_activa = false;
            v_MaxVida = 100;
            v_vidaEsc = v_MaxVida / 2.0f;
            //v_Vida = v_MaxVida;
            v_Defensa = 5.0f;
            gameObject.tag = k.Tags.VENTANA;
            gameObject.layer = k.Layers.VENTANA;
            //if (transform.childCount < 4)
            //{
            //    Debug.LogError("FALTAN HIJOS " + gameObject.name);
            //}
            if (v_PosArr.Length < 1)
            {
                Debug.LogError("Arreglo de posiciones", gameObject);
                Debug.Break();
            }
            if (v_Slider != null)
                v_Slider.Fn_Init(1);

            if (v_SliderEscudo != null)
                v_SliderEscudo.Fn_Init(0);

            GetComponent<Audio.Au_Manager>().Fn_Inicializa();
            //v_mesh = GetComponent<MeshRenderer>();
            v_navmesh = GetComponent<NavMeshObstacle>();
            //v_Muro.SetActive(false);
            v_Slider.Fn_SetFill((v_Vida / v_MaxVida), true);
        }
        public void Fn_RecibeDano(float _dano, GameObject _elquedana)
        {
            GetComponent<Audio.Au_Manager>().Fn_SetAudio(2,false,true);
            //if (v_ventana.Fn_GBarrera())
            if (v_boton.Fn_GBarrera())
            {
                v_vidaEsc -= _dano;
                Fn_Lista(_elquedana);
                v_vidaEsc = Mathf.Clamp(v_vidaEsc, 0, (v_MaxVida / 2.0f));
                v_SliderEscudo.Fn_SetFill((v_vidaEsc / (v_MaxVida / 2.0f)), true);
                if (v_vidaEsc <= 0)
                {
                    v_SliderEscudo.Fn_SetFill(0, false);
                    //v_boton.Fn_Barrera(false);
                    //v_ventana.Fn_Barrera(false);
                }
            }
            else
            {
                float resta = _dano - v_Defensa;
                if (resta <= 0 || v_Rota == true)
                {
                    return;
                }
                else
                {
                    Fn_Lista(_elquedana);
                    v_Vida -= resta;
                    v_Vida = Mathf.Clamp(v_Vida, 0, v_MaxVida);
                    v_Slider.Fn_SetFill((v_Vida / v_MaxVida), true);
                    if (v_Vida <= 0)
                    {
                        //_elquedana.SendMessage("Fn_Rompio", gameObject.name);

                        /*for (int i = 0; i < enemigos.Count; i++)
                        {
                            if (enemigos[i] != null && enemigos[i].activeInHierarchy)
                            {
                                //Debug.Log("se rompio ventanba " + enemigos[i].transform.name);
                                //enemigos[i].SendMessage("Fn_Rompio");
                                enemigos[i].SendMessage("Fn_Saltar", true);
                                // enemigos[i].SendMessage("Fn_Saltar", v_medio);
                            }
                        }*/
                        Fn_Romper();
                    }
                }
            }

        }
        public void Fn_Repara()//float _recuperar)
        {
            // GetComponent<BoxCollider>().enabled = true;
            v_navmesh.enabled = true;
            v_mesh.enabled = true;
            v_Vida = v_MaxVida;
            v_Slider.Fn_SetFill((v_Vida / v_MaxVida), true);
            v_Rota = false;
            //v_Muro.SetActive(false);
            //Valve.VR.InteractionSystem.Player.instance.leftHand.GetComponent<UI_Datos>().Fn_Actualizar();
            enemigos.Clear();
        }
        public void Fn_Romper()
        {
            v_Vida = 0;
            v_Slider.Fn_SetFill(0, false);
            v_Rota = true;
            GetComponent<Audio.Au_Manager>().Fn_SetAudio(3, false, true);
            GameObject na = Instantiate(part, transform.position, Quaternion.identity);
            na.GetComponent<ParticleSystem>().Play();
            Destroy(na, 1.5f);
            //GetComponent<BoxCollider>().enabled = false;
            v_navmesh.enabled = false;
            v_mesh.enabled = false;
            //v_Muro.SetActive(true);
            enemigos.Clear();
            /*if (Valve.VR.InteractionSystem.Player.instance.leftHand)
                Valve.VR.InteractionSystem.Player.instance.leftHand.GetComponent<UI_Datos>().Fn_Actualizar();*/
        }
        public void Fn_IncrementoVidaMax(float _nuevaMax)
        {
            v_MaxVida = _nuevaMax;
        }
        /// <summary>
        /// para que la ventanaa sepa quien le esta pegando, y asi el enemigo sepa cuando ya se rompio
        /// </summary>
        void Fn_Lista(GameObject agregar)
        {
            if (!enemigos.Contains(agregar))
            {
                enemigos.Add(agregar);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Mano")
            {
                Fn_Lista(other.GetComponentInParent<UnityEngine.AI.NavMeshAgent>().gameObject);
            }
        }
        public Tienda.Ti_ventanaTi Fn_GetBoton()
        { return v_boton; }
        public Vector3 Fn_Pos()
        {
            return transform.position;
        }
        public bool Fn_GRota()
        {
            return v_Rota;
        }
        public bool Fn_GActiva()
        {
            return v_activa;
        }
        public float Fn_GDefensa()
        {
            return v_Defensa;
        }
        public void Fn_Comprar()
        {
            v_activa = true;
            v_Rota = false;
            v_Vida = v_MaxVida;
        }
        public void Fn_EscudoInit()
        {
            v_vidaEsc = v_MaxVida / 2;
            GetComponent<Ventana>().v_SliderEscudo.gameObject.SetActive(true);
            GetComponent<Ventana>().v_SliderEscudo.Fn_SetFill(1, true);
        }
        public Vector3 Fn_GetPosRand(int _index)
        { return v_PosArr[_index].position;}
        public int Fn_GetPosRand()
        {
            if (v_PosArr.Length == 1)
            {
                return 0;
            }
            else
            {
                int _ind = Random.Range(0, v_PosArr.Length);
                return _ind;
            }
        }
        //para ver cual es el enfrente de la ventana
        //void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawLine(transform.position, transform.position + v_frente.forward * v_rango);
        //    v_rangoVec = transform.position + (v_frente.forward * v_rango);
        //}

        /*
            /// <summary>
            /// maximo de vida por si algun power up
            /// </summary>
            public float v_MaxVida;
            /// <summary>
            /// vida actual
            /// </summary>
            public float v_Vida;
            /// <summary>
            /// defensa para disminuir
            /// </summary>
            public float v_Defensa;
            /// <summary>
            /// si ya se rompio los enemigos la atraviesan
            /// </summary>
            public bool v_Rota;
            /// <summary>
            /// al comprar el area es true
            /// </summary>
            public bool v_activa;
            /// <summary>
            /// particulas de prueba para muerte
            /// </summary>
            public GameObject part;
            [ReadOnly]
            /// <summary>
            /// si se rompe, mandarle el mensaje a todos los que le han pegado
            /// </summary>   
            public List<GameObject> enemigos;
            /// para usar bien el match target animator
            //public Transform v_medio;

            public Transform[] v_PosArr;

            #region PARA EL SLIDER 
            public Ifz_Damage v_Slider;
            #endregion
            public GameObject v_Muro;

            public Transform v_frente;

            private void Awake()
            {
                v_activa = false;
                v_MaxVida = 100;
                v_Vida = v_MaxVida;
                v_Defensa = 5.0f;
                gameObject.tag = k.Tags.VENTANA;
                gameObject.layer = k.Layers.VENTANA;
                if (transform.childCount<4)
                {
                    Debug.LogError("FALTAN HIJOS " + gameObject.name);
                }
                if(v_PosArr.Length<1)
                {
                    Debug.LogError("Arreglo de posiciones",gameObject);
                    Debug.Break();
                }
                if(v_Slider!= null)
                    v_Slider.Fn_Init();

                v_Muro.SetActive(false);
            }
            public void Fn_RecibeDano(float _dano, GameObject _elquedana)
            {
                //print("recibo dano de " + _dano+ " yo " + name);        
                float resta = _dano - v_Defensa;
                if (resta <= 0 || v_Rota == true)
                {
                    return;
                }
                else
                {
                    Fn_Lista(_elquedana);
                    v_Vida -= resta;
                    v_Vida = Mathf.Clamp(v_Vida, 0, v_MaxVida);
                    v_Slider.Fn_SetFill((v_Vida / v_MaxVida),true);
                    //v_Slider.fillAmount = v_Vida / v_MaxVida;
                    if (v_Vida <= 0)
                    {
                        //_elquedana.SendMessage("Fn_Rompio", gameObject.name);
                        for (int i = 0; i < enemigos.Count; i++)
                        {
                            if (enemigos[i] != null && enemigos[i].activeInHierarchy)
                            {
                                //Debug.Log("se rompio ventanba " + enemigos[i].transform.name);
                                //enemigos[i].SendMessage("Fn_Rompio");
                                enemigos[i].SendMessage("Fn_Saltar", true);
                               // enemigos[i].SendMessage("Fn_Saltar", v_medio);
                            }
                        }
                        Fn_Romper();
                    }
                }
            }
            public void Fn_Repara()//float _recuperar)
            {
                //for (int i = 0; i < gameObject.GetComponents<BoxCollider>().Length; i++)
                //{
                //    gameObject.GetComponents<BoxCollider>()[i].enabled = true;
                //}   
                // para poner triggers con la ventana y el modelo como hijo
                //GetComponent<MeshCollider>().enabled = true;// este es por si la ventana lo tiene el modelo
                GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<NavMeshObstacle>().enabled = true;
                v_Vida = v_MaxVida;
                //v_Slider.fillAmount = v_Vida / v_MaxVida;
                v_Slider.Fn_SetFill((v_Vida / v_MaxVida), true) ;
                v_Rota = false;
                //transform.GetChild(0).gameObject.SetActive(false);
                v_Muro.SetActive(false);
                Valve.VR.InteractionSystem.Player.instance.leftHand.GetComponent<UI_Datos>().Fn_Actualizar();
                enemigos.Clear();
            }
            public void Fn_Romper()
            {
                v_Vida = 0;
                v_Slider.Fn_SetFill(0, false);
                v_Rota = true;
                GameObject na = Instantiate(part, transform.position, Quaternion.identity);
                na.GetComponent<ParticleSystem>().Play();
                Destroy(na, 1.5f);
              //  GetComponent<MeshCollider>().enabled = false;// este es por si la ventana lo tiene el modelo
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<NavMeshObstacle>().enabled = false;
                //for (int i = 0; i < gameObject.GetComponents<BoxCollider>().Length; i++)
                //{
                //    gameObject.GetComponents<BoxCollider>()[i].enabled = false;
                //}
                // para poner triggers con la ventana y el modelo como hijo

                //tiene que ser un trigger con el codigo    Muro
                //transform.GetChild(0).gameObject.SetActive(true);
                v_Muro.SetActive(true);
                //trues obstaculo
                enemigos.Clear();
                if(Valve.VR.InteractionSystem.Player.instance.leftHand)
                    Valve.VR.InteractionSystem.Player.instance.leftHand.GetComponent<UI_Datos>().Fn_Actualizar();
            }
            public void Fn_IncrementoVidaMax(float _nuevaMax)
            {
                v_MaxVida = _nuevaMax;
            }
            /// <summary>
            /// para que la ventanaa sepa quien le esta pegando, y asi el enemigo sepa cuando ya se rompio
            /// </summary>
            void Fn_Lista(GameObject agregar)
            {
                if (!enemigos.Contains(agregar))
                {
                    enemigos.Add(agregar);
                }
            }
            private void OnTriggerEnter(Collider other)
            {
                if(other.tag=="Mano")
                {
                    Fn_Lista(other.GetComponentInParent<UnityEngine.AI.NavMeshAgent>().gameObject);
                }
                //Fn_Lista(other.gameObject);
            }
            public Vector3 Fn_Pos()
            {
                return transform.position;
            }
            public bool Fn_GRota()
            {
                return v_Rota;
            }
            public bool Fn_GActiva()
            {
                return v_activa;
            } 
            public float Fn_GDefensa()
            {
                return v_Defensa;
            }
            public void Fn_Comprar()
            {
                v_activa = true;
                v_Rota = false;
                v_Vida = v_MaxVida;
            }
            public Vector3 Fn_GetPosRand(int _index)
            {
                return v_PosArr[_index].position;
            }
            public int Fn_GetPosRand()
            {
                if (v_PosArr.Length==1)
                {
                    return 0;
                }
                else
                {
                    int _ind = Random.Range(0, v_PosArr.Length);
                    return _ind;
                }
            }
            void OnDrawGizmos()
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position,transform.position+  v_frente.forward *2.0f);
            }
            */
    }
    public class C_Ventana
    {
        public List< GameObject> v_ven;
        public List<Transform> v_pos;
        public C_Ventana()
        {
            v_ven = new List<GameObject>();
            v_pos = new List<Transform>();
        }
    }
}