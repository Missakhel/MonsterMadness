using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
using DigitalRuby.FastLineRenderer;
using UnityEngine.AI;
namespace Jugador
{
    public class Moverse : MonoBehaviour
    {

        /// <summary>
        /// el punto de donde va a salir el rayo
        /// </summary>
        public Transform v_SaleRayo;
        public GameObject v;
        /// <summary>
        /// el prefab qie hice
        /// </summary>
        public GameObject laserPrefab;
        /// <summary>
        /// el objeto  que va a etsra en la escena
        /// </summary>
        private GameObject laser;
        private Transform laserTransform;
        /// <summary>
        /// PUNTO DONDE CHOCA EL RAYO
        /// </summary>
        private Vector3 hitPoint;
        private bool v_escalera = false;
        /// <summary>
        /// saber si golpeaste en algun punto donde te 
        /// puedes mover
        /// </summary>
        public bool CambiarPos = false;
        //public Hand _hand;
        public GameObject v_Radar;
        public GameObject v_MenuPausa;

        [Tooltip("Tranform contendor de todo sobre VR, cabeza y manos")]
        public Transform trans_Cuerpo;
        [Tooltip("Tranform ojos de VR")]
        public Transform trans_Ojos;

        //Control de efecto de escalera
        bool EscaleraEnVerde = false;
        GameObject EscaleraGameObject;

        [Header("Debug")]
        public FastLineRenderer debug;
        Color debugColorLinea;

        [Header("Rayos moverse")]

        RaycastHit hit2;
        public Vector3 rayCast2Dir;
        public float rayCast2Dist;

        void Awake()
        {
            /*v_Radar.SetActive(false);
            _hand = Player.instance.leftHand;
            CambiarPos = false;
            laser = Instantiate(laserPrefab);
            laserTransform = laser.transform;
            v_escalera = false;
            //v_SaleRayo = transform.Find("Sale Rayo");
            if (v_SaleRayo == null)
            {
                Debug.LogError("FAlta el punto de salida en moverse", this);
            }*/
        }
        void Update()
        {
            /*if (_hand == null)
            {
                _hand = Player.instance.leftHand;
                if (_hand != null)
                {
                    _hand = Player.instance.leftHand;
                    GetComponent<UI_Datos>().Fn_Actualizar();
                }
            }
            else
            {
                if (!Jug_Datos.Instance.Fn_GetVivo())
                    return;
                /*
                 Necesita el layer  Escenario
                 */
                /*
                probar a ponerle tag para que toque la nuez tag de piso  
                y el layer de escenario  paara que funcioone el rayo
                
                if (SteamVR.instance != null && Player.instance.leftHand.controller != null)
                {

                    if (Player.instance.leftHand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
                    {
                        v_Radar.SetActive(!v_Radar.activeInHierarchy);
                    }
                    else if (Player.instance.leftHand.controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && v_MenuPausa)
                    {
                        v_MenuPausa.SetActive(!v_MenuPausa.activeInHierarchy);
                    }
                    else
                    {
                        if (!Jug_Datos.Instance.Fn_GetComprando() && (Player.instance.leftHand.controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)))
                        {
                            if (!ControllerButtonHints.IsButtonHintActive(Player.instance.leftHand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
                            {
                                ControllerButtonHints.ShowButtonHint(Player.instance.leftHand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
                            }

                            RaycastHit hit;     //layer escenario esta en 1000000000 de binario a normal da 512
                                                //if (Physics.Raycast(v_SaleRayo.position, transform.forward, out hit, 50, 512))
                            if (Physics.Raycast(_hand.transform.position, transform.forward, out hit, 50, 512))
                            {//_hand.transform.position
                             //Evitamos que jugador pueda travesar paredes al cruzar brazo y teleptransportar asi
                                //RaycastHit hit2;
                                
                                //Validamos que sea punto alcanzable 
                                //Lanzamos un rayo de la capsula al control si impacta en pared, no es valido
                                rayCast2Dir = _hand.transform.position - Player.instance.transform.position;
                                //Vector3 rayCast2Dir = v_SaleRayo.position - Player.instance.transform.position;
                                rayCast2Dist = rayCast2Dir.magnitude; //Almacenamos distancia
                                rayCast2Dir.Normalize(); //Normalizamos para ser dir
                                                         //Ignoramos Jugador
                                int capa2 = 1 << k.Layers.PLAYER;
                                capa2 |= (1 << k.Layers.MUERTO);
                                capa2 |= (1 << k.Layers.MANO);
                                capa2 |= (1 << k.Layers.UI);
                                capa2 = ~capa2; //Ignorar
                                if (Physics.Raycast(Player.instance.transform.position, rayCast2Dir, out hit2, rayCast2Dist, capa2))
                                {

                                    //Si impacta en algo, no es valido
                                    //Debug.LogError("no valido"+ hit2.transform.gameObject.name+ " con tag "+ hit2.transform.gameObject.tag); 
                                    CambiarPos = false;
                                    Fn_MostrarAviso(true);
                                    return;
                                }
                                Fn_MostrarAviso(false);
                                //Debug.DrawRay(_hand.transform.position, transform.forward, Color.red);
                                //Debug.LogError(hit.transform.tag + "  " + (hit.point.y <= Player.instance.transform.position.y + 1));
                                if (hit.transform.tag == k.Tags.PISO && (hit.point.y <= Player.instance.transform.position.y + 1))
                                {
                                    if (hit.transform.name.Contains("Plataforma moverse"))
                                    {
                                        //choca con un objeto que si puedes hacer el cambio
                                        CambiarPos = true;
                                        v = hit.transform.gameObject;
                                        v_escalera = false;
                                        debugColorLinea = Color.blue;
                                    }
                                    else
                                    {
                                        //Verificamos que no este muy pegadoo
                                        NavMeshHit hitNav;
                                        bool Factible = NavMesh.SamplePosition(hit.point, out hitNav, 0.05f, NavMesh.AllAreas);
                                        if (!Factible)
                                        {
                                            //Camino bloqueado
                                            //Debug.LogError("no factible");
                                            CambiarPos = false;
                                            debugColorLinea = Color.red;
                                        }
                                        else
                                        {
                                            //choca con un objeto que si puedes hacer el cambio
                                            //Debug.LogError("navmesh sample position true");
                                            CambiarPos = true;
                                            v = hit.transform.gameObject;
                                            v_escalera = false;
                                            debugColorLinea = Color.blue;
                                        }
                                    }
                                }
                                else if (hit.transform.tag == "Escalera")
                                {
                                    v_escalera = true;
                                    CambiarPos = true;
                                    v = hit.transform.gameObject;
                                    EscaleraGameObject = v; //Respaldo para regresar a coloor normal
                                }
                                else
                                {
                                    debugColorLinea = Color.red;
                                    v_escalera = false;
                                    CambiarPos = false;
                                }
                                hitPoint = hit.point;
                                Fn_Mostrar();
                                //ESta sobre escalera y no esta en verde
                                if (v_escalera)
                                {
                                    if (EscaleraEnVerde == false)
                                    {
                                        EscaleraEnVerde = true;
                                        LeanTween.color(EscaleraGameObject, Color.green, 0.3f);
                                    }
                                }
                                else //Ya no esta en escalera
                                {
                                    //Esta coloreado la escalera?
                                    if (EscaleraEnVerde)
                                    {
                                        EscaleraEnVerde = false;
                                        LeanTween.color(EscaleraGameObject, new Color32(152, 152, 152, 255), 0.3f); //color normal
                                    }
                                }

                                //Dibujamos debug
                                if (debug != null)
                                {
                                    debug.Reset();
                                    FastLineRendererProperties props = new FastLineRendererProperties();
                                    props.Start = _hand.transform.position;
                                    //props.Start = v_SaleRayo.position;
                                    props.End = hit.point;
                                    props.Radius = 0.1f;
                                    props.Color = debugColorLinea;
                                    debug.AddLine(props);
                                    debug.Apply();
                                }
                            }
                            else
                            {
                                //Limpiamos debug
                                if (debug != null)
                                {
                                    debug.Reset();
                                }
                            }
                        }
                        else //Solto el joystick
                        {
                            if ((Player.instance.leftHand.controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)))
                            {
                                ControllerButtonHints.HideButtonHint(Player.instance.leftHand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
                            }
                            //Limpiamos debug
                            if (debug != null)
                            {
                                debug.Reset();
                            }
                            if (CambiarPos == true)
                            {
                                if (v_escalera)
                                {
                                    Vector3 pos = GameObject.Find("subir").transform.position;
                                    Player.instance.gameObject.transform.position = pos + new Vector3(0, 0.5f, 0);
                                    LeanTween.color(EscaleraGameObject, new Color32(152, 152, 152, 255), 0.3f); //color normal
                                    EscaleraGameObject = v = null;
                                    CambiarPos = false;
                                    EscaleraEnVerde = false;
                                }
                                else
                                {
                                    Vector3 pos = transform.parent.position;
                                    Player.instance.transform.position = new Vector3(hitPoint.x, Player.instance.transform.position.y, hitPoint.z);
                                    //Moovemos cuerpo para central personaje
                                    //Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
                                    Vector3 cuerpoNewPos = -trans_Ojos.localPosition;
                                    cuerpoNewPos.y = 0.0f;
                                    trans_Cuerpo.localPosition = cuerpoNewPos;
                                    CambiarPos = false;
                                    // GameObject.Find("tienda").GetComponent<Obj_tienda>().Fn_Moverse();
                                    //PLAN B, solo mover player.trackingOriginTransform
                                }
                            }
                            Fn_MostrarAviso(false);
                            laser.SetActive(false);
                        }
                    }
                }
                else//si no estoy con steamvr
                {
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        v_Radar.SetActive(!v_Radar.activeInHierarchy);
                    }
                    else if (Input.GetKeyDown(KeyCode.L))
                    {
                        v_MenuPausa.SetActive(!v_MenuPausa.activeInHierarchy);
                    }
                    else
                    {
                        if (!Jug_Datos.Instance.Fn_GetComprando() && Input.GetKey(KeyCode.T))
                        {
                            RaycastHit hit;     //layer escenario esta en 1000000000 de binario a normal da 512                  
                            if (Physics.Raycast(_hand.transform.position, transform.forward, out hit, 50, 512))
                            {
                                //Evitamos que jugador pueda travesar paredes al cruzar brazo y teleptransportar asi
                                RaycastHit hit2;
                                //Validamos que sea punto alcanzable 
                                //Lanzamos un rayo de la capsula al control si impacta en pared, no es valido
                                Vector3 rayCast2Dir = _hand.transform.position - Player.instance.transform.position;
                                float rayCast2Dist = rayCast2Dir.magnitude; //Almacenamos distancia
                                rayCast2Dir.Normalize(); //Normalizamos para ser dir
                                int capa2 = 1 << k.Layers.PLAYER;//Ignoramos Jugador
                                capa2 |= (1 << k.Layers.MUERTO);
                                capa2 = ~capa2; //Ignorar
                                if (Physics.Raycast(Player.instance.transform.position, rayCast2Dir, out hit2, rayCast2Dist, capa2))
                                {
                                    //Si impacta en algo, no es valido
                                    Debug.LogError("no valido");
                                    CambiarPos = false;
                                    Fn_MostrarAviso(true);
                                    return;
                                }
                                Fn_MostrarAviso(false);
                                //Debug.DrawRay(_hand.transform.position, transform.forward, Color.red);
                                if (hit.transform.tag == "Piso" && (hit.point.y <= Player.instance.transform.position.y + 1))
                                {
                                    if (hit.transform.name.Contains("Plataforma moverse"))
                                    {
                                        //choca con un objeto que si puedes hacer el cambio
                                        CambiarPos = true;
                                        v = hit.transform.gameObject;
                                        v_escalera = false;
                                        debugColorLinea = Color.blue;
                                    }
                                    else
                                    {
                                        //Verificamos que no este muy pegadoo
                                        NavMeshHit hitNav;
                                        bool Factible = NavMesh.SamplePosition(hit.point, out hitNav, 0.05f, NavMesh.AllAreas);
                                        if (!Factible)
                                        {
                                            //Camino bloqueado
                                            Debug.LogError ("no factible");
                                            CambiarPos = false;
                                            debugColorLinea = Color.red;
                                        }
                                        else
                                        {
                                            Debug.LogError("navmesh sample position true");
                                            //choca con un objeto que si puedes hacer el cambio
                                            CambiarPos = true;
                                            v = hit.transform.gameObject;
                                            v_escalera = false;
                                            debugColorLinea = Color.blue;
                                        }
                                    }
                                }
                                else if (hit.transform.tag == "Escalera")
                                {
                                    v_escalera = true;
                                    CambiarPos = true;
                                    v = hit.transform.gameObject;
                                    EscaleraGameObject = v; //Respaldo para regresar a coloor normal
                                }
                                else
                                {
                                    v_escalera = false;
                                    CambiarPos = false;
                                }
                                hitPoint = hit.point;
                                Fn_Mostrar();

                                //ESta sobre escalera y no esta en verde
                                if (v_escalera)
                                {
                                    if (EscaleraEnVerde == false)
                                    {
                                        EscaleraEnVerde = true;
                                        LeanTween.color(EscaleraGameObject, Color.green, 0.3f);
                                    }
                                }
                                else //Ya no esta en escalera
                                {
                                    //Esta coloreado la escalera?
                                    if (EscaleraEnVerde)
                                    {
                                        EscaleraEnVerde = false;
                                        LeanTween.color(EscaleraGameObject, new Color32(152, 152, 152, 255), 0.3f); //color normal
                                    }
                                }
                                //Dibujamos debug
                                if (debug != null)
                                {
                                    debug.Reset();
                                    FastLineRendererProperties props = new FastLineRendererProperties();
                                    props.Start = _hand.transform.position;
                                    props.End = hit.point;
                                    props.Radius = 0.1f;
                                    props.Color = debugColorLinea;
                                    debug.AddLine(props);
                                    debug.Apply();
                                }

                            }
                            else
                            {
                                //Limpiamos debug
                                if (debug != null)
                                {
                                    debug.Reset();
                                }
                            }
                        }
                        else
                        {
                            if (CambiarPos == true)
                            {
                                if (v_escalera)
                                {
                                    Vector3 pos = GameObject.Find("subir").transform.position;
                                    Player.instance.gameObject.transform.position = pos + new Vector3(0, 0.5f, 0);
                                    v = null;
                                    CambiarPos = false;
                                }
                                else
                                {
                                    Vector3 pos = transform.parent.gameObject.transform.position;
                                    Player.instance.gameObject.transform.position = new Vector3(hitPoint.x, Player.instance.gameObject.transform.position.y, hitPoint.z);
                                    CambiarPos = false;
                                    // GameObject.Find("tienda").GetComponent<Obj_tienda>().Fn_Moverse();
                                }
                            }
                            Fn_MostrarAviso(false);
                            laser.SetActive(false);
                        }
                    }
                }
            }
        }
        private void Fn_Mostrar()
        {
            laser.SetActive(true);
            laserTransform.position = hitPoint;// +new Vector3(0.0f,0.1f,0.0f);
        }
        private void OnDrawGizmos()
        {
            //if (_hand != null && hitPoint != null)
            //{
            //    Gizmos.DrawLine(_hand.transform.position, transform.forward);
            //    Gizmos.DrawSphere(hitPoint, 0.2f);
            //}
            //Gizmos.DrawSphere(transform.position, 0.02f);
        }
        private void Fn_MostrarAviso(bool _Valor)
        {
            if (Player.instance.leftHand)
                Player.instance.leftHand.GetComponent<UI_Datos>().Fn_MuestraAviso(_Valor, 1);*/
        }
    }
}