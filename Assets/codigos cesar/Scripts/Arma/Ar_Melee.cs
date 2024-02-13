using UnityEngine;
using System.Collections.Generic;
using System.Collections;
//using Valve.VR.InteractionSystem;
namespace Armas
{
    using Jugador;
    using Manager;
    [RequireComponent(typeof(BoxCollider))]
    public class Ar_Melee : Arma
    {
        [Header("MELEE")]
        public MeshRenderer v_mesh;
        public GameObject v_prefNormal;
        public GameObject v_prefRota;
        public Material v_activo;
        public Material v_noactivo;
        bool v_pega = false;
        GameObject v_quien;
        public ParticleSystem v_Part;
        //rango supercorto, dano alto
        public override void Fn_Iniciar()
        {
            v_Part.gameObject.SetActive(false);
            v_quien = Jug_Datos.Instance.gameObject;
            v_Melee = true;
            //v_Rango = 12;
            v_Dano = 26;
            v_PrecioDesbloqueo = 10;
            v_idPool = 0;
            v_pega = false;
            if (v_mesh != null)
                v_mesh.material = v_activo;
            Fn_SetInit(100, 100, 20,2);
            v_prefNormal.SetActive(true);
            v_prefRota.SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            //Debug.LogError("arma le pega a " + other.name);
            if (Fn_Melee() && !v_pega)
            {
                if ((other.tag == k.Tags.ENEMY || other.tag == k.Tags.CABEZA))
                {
                    //Debug.LogError("pega aaaaaa " + other.name);
                    //v_Part.transform.SetParent(other.transform);
                    //v_Part.transform.position = other.ClosestPoint(gameObject.transform.position);
                    //Hacemos daño al jugadoor
                        //Valve.VR.InteractionSystem.Player.instance.rightHand.GetComponent<Audio.Au_Manager>().Fn_SetAudio(5, false, true);
                    if (other.tag == k.Tags.CABEZA)
                    {
                        other.gameObject.SendMessage("Dano", v_Dano * 2.0f, SendMessageOptions.DontRequireReceiver);
                    }
                    else //El tag es enemigo   pega en cuerpo
                    {
                        other.gameObject.SendMessage("Dano", v_Dano, SendMessageOptions.DontRequireReceiver);
                    }
                    //Avisamos a enemigo que ahora ataque al jugador
                    other.gameObject.SendMessage("Dano", v_quien, SendMessageOptions.DontRequireReceiver);
                    // v_idPool++;
                    Fn_SetDisparo(false);
                    if (v_pila<=0)
                    {
                        //Valve.VR.InteractionSystem.Player.instance.rightHand.GetComponent<Audio.Au_Manager>().Fn_SetAudio(8, false, true);
                        v_prefNormal.SetActive(false);
                        v_prefRota.SetActive(true);

                        v_puededisparar = false;
                        v_mesh.material = v_noactivo;
                        /*if (Player.instance.leftHand)
                            Player.instance.leftHand.GetComponent<UI_Datos>().Fn_MuestraAviso(4, 2);*/

                        //GetComponentInParent<Ar_Manager>().Fn_Eliminar(GetComponent<Arma>().GetType());
                        //GetComponentInParent<Jug_Arma>().Fn_ActualizaManager();
                    }
                    if (gameObject.activeSelf)
                        StartCoroutine(Ie_Cooldown());
                }
            }
        }
        WaitForSeconds v_await= new WaitForSeconds(1.5f);
        IEnumerator Ie_Cooldown()
        {
            if (gameObject.activeInHierarchy)
            {
                v_Part.gameObject.SetActive(true);
                v_Part.Play();
                v_mesh.material = v_noactivo;
                v_pega = true;
                yield return v_await;
                if(gameObject.activeInHierarchy)
                {
                    v_pega = false;
                    v_mesh.material = v_activo;
                    v_Part.Stop();
                    v_Part.gameObject.SetActive(false);
                }
            }
        }
        public override void Fn_RecogeMunicion()
        {
            v_prefNormal.SetActive(true);
            v_prefRota.SetActive(false);
            v_puededisparar = true;
            base.Fn_RecogeMunicion();
        }
        public override void Fn_Pool()
        {
            v_idPool = 0;
        }

        /*
        public MeshRenderer v_mesh;
        public Material v_activo;
        public Material v_noactivo;
        protected bool v_pega=false;
        GameObject v_quien;
        //rango supercorto, dano alto
        void Awake()
        {
            v_quien = Jug_Datos.Instance.gameObject;
            v_Melee = true;
            //v_Rango = 12;
            v_Dano = 50;
            v_TimepoRecarga = 2;
            v_PrecioDesbloqueo = 10;
            v_MaxBalas = 100;
            v_MaxCargador = v_MaxBalas;
            v_BalasCargador = v_MaxCargador;
            v_BalasActuales = v_MaxBalas - v_BalasCargador;
            v_idPool = 0;
            v_pega = false;
            if(v_mesh!= null)
                v_mesh.material = v_activo ;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            //Debug.LogError("arma le pega a " + other.name);
            if (Fn_PuedeDisparar() && !v_pega)
            {
                if ((other.tag == k.Tags.ENEMY || other.tag == k.Tags.CABEZA))
                {
                    //Debug.LogError("pega aaaaaa " + other.name);
                    //Hacemos daño al jugadoor
                    if (other.tag == k.Tags.CABEZA)
                    {
                        other.gameObject.SendMessage("Dano", v_Dano * 2.0f, SendMessageOptions.DontRequireReceiver);
                    }
                    else //El tag es enemigo
                    {
                        other.gameObject.SendMessage("Dano", v_Dano, SendMessageOptions.DontRequireReceiver);
                    }
                    //Avisamos a enemigo que ahora ataque al jugador
                    other.gameObject.SendMessage("Dano", v_quien, SendMessageOptions.DontRequireReceiver);
                    // v_idPool++;
                    v_BalasCargador -= 10;
                    if (v_BalasCargador <= 0)
                    {
                        v_puededisparar = false;
                        v_mesh.material = v_noactivo;
                        if(Player.instance.leftHand)
                            Player.instance.leftHand.GetComponent<UI_Datos>().Fn_MuestraAviso(4, 2);

                        GetComponentInParent<Ar_Manager>().Fn_Eliminar(GetComponent<Arma>().GetType());
                    }
                    if(gameObject.activeSelf)
                        StartCoroutine(Ie_Cooldown());
                }
            }
        }
       
        IEnumerator Ie_Cooldown()
        {
            v_mesh.material =  v_noactivo;
            v_pega = true;
            yield return new WaitForSeconds(2.0f);
            v_pega = false;
            v_mesh.material = v_activo ;
        }
        public override void Fn_Pool()
        {
            v_idPool = 0;
        }
        public override void Fn_Down()
        {
            if (v_puededisparar == true)
            {
                base.Fn_Down();//llama la funcion que tiene el calculo de las balas, para saber si recarga o si ya no tienes balas
                /////////////////////////77
                //aca puedes hacer tu forma de disparo como tu quieras
                //////////////////////////////////
            }
        }*/
    }
}
        /*private void OnCollisionEnter(Collision collision)
        {
                Debug.LogError("arma le pega a " + collision.transform.name);
            if (Fn_PuedeDisparar() && !v_pega)
            {
                if ((collision.contacts[0].thisCollider.transform.tag == k.Tags.ENEMY || collision.contacts[0].thisCollider.transform.tag == k.Tags.CABEZA))
                {//El tag es enemigo
                    if (collision.contacts[0].thisCollider.transform.tag == k.Tags.CABEZA)
                    {
                        collision.contacts[0].thisCollider.gameObject.SendMessage("Dano", v_Dano * 2.0f);
                    }
                    else 
                    {
                        collision.contacts[0].thisCollider.gameObject.SendMessage("Dano", v_Dano);
                    }
                    //Avisamos a enemigo que ahora ataque al jugador
                    collision.contacts[0].thisCollider.gameObject.SendMessage("Dano", v_quien);
                }
               // v_idPool++;
                v_BalasCargador -= 10;
                if (v_BalasCargador <=  0)
                {
                    v_puededisparar = false;
                    GetComponentInParent<Ar_Manager>().Fn_Eliminar(GetComponent<Arma>().GetType());
                }
                StartCoroutine(Ie_Cooldown());
            }
        }*/