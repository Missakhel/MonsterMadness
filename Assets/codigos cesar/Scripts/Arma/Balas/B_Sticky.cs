using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Armas.Balas
{
    /*
     EN LA ANIMACION DE CUANDO YA PEGA EN EL SUELO PONER EL EVENTO DE ANIMACION
         */
    [RequireComponent(typeof(Audio.Au_Manager))]
    public class B_Sticky : MonoBehaviour
    {
        //public GameObject v_prefab;
        //public float v_rango = 1000;
        //public List<GameObject> v_enemigos = new List<GameObject>();
        //public GameObject v_quien;
        public bool v_golpe = false;
        public bool v_primera = false;

        Vector3 v_PosIn;
        float v_velocidad = 0;
        private Rigidbody v_rig;
        float v_tiempo;
        Animator v_anim;
        /// <summary>
        /// ya pego en el piso?
        /// </summary>
        public bool v_choca = false;
        /// <summary>
        /// tiempo cuando pega en el piso
        /// </summary>
        float v_time;
        /// <summary>
        /// hace daño ya que hizo la animacion
        /// </summary>
        public bool v_dano=false;
        private void OnTriggerEnter(Collider other)
        {
           // Debug.LogError("enter " + other.name, gameObject);
            if (!v_choca)
                return;
            //Debug.LogError("enter choca true " + v_choca, gameObject);

            //if (!v_dano)
            //    return;

            //Debug.LogError("enter choca true  dano " + v_dano, gameObject);
            if (other.gameObject.layer == 8 && other.gameObject.tag == "Enemy")
            {
              //  Debug.LogError("enter layer enemigo tag", gameObject);
                if (!v_primera)
                {
                    v_primera = true;
                    //Debug.LogError("enter golpe primera", gameObject);
                    GetComponent<Audio.Au_Manager>().Fn_SetAudio(1, true, true);
                    StartCoroutine(Ie_Loop());
                }
            }
        }
        IEnumerator Ie_Loop()
        {
            WaitForSeconds v_await = new WaitForSeconds(1.0f);
            WaitForSeconds _dano = new WaitForSeconds(0.1f);
            while (true)
            {
                v_golpe = false;
                yield return v_await;                
                v_golpe = true;
                yield return _dano;
            }
        }
        public void Fn_SetDano()
        {
            v_dano = true;
            //Debug.LogError("setdano "+ v_dano, gameObject);
        }
        private void OnTriggerStay(Collider other)
        {
            //Debug.LogError("stay "+ other.name  ,gameObject);
            if (!v_choca )
                return;
            //Debug.LogError("stay choca true "+v_choca  ,gameObject);

            if (!v_dano)
            {
                return;
            }
            //Debug.LogError("stay choca true  dano "+v_dano  ,gameObject);
            if (gameObject.activeInHierarchy)
            {
              //  Debug.LogError("active hierarchy"   ,gameObject);
                if ( other.gameObject.layer == 8 && other.gameObject.tag == "Enemy")
                {
                //Debug.LogError("stay layer enemigo tag"   ,gameObject);
                    if(v_golpe && v_primera)
                    {
                        //Debug.LogError("stay golpe primera"   ,gameObject);
                        other.SendMessage("Dano", 5, SendMessageOptions.DontRequireReceiver);
                    }

                    //if (!v_enemigos.Contains(other.gameObject))
                    //{
                    //    v_enemigos.Add(other.gameObject);
                    //}
                    v_anim.SetBool("v_durante", true);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            v_anim.SetBool("v_durante",false);
        }
        private void Update()
        {
            if(v_choca)
            {
                if (Time.time > (v_time + v_tiempo))
                    Destroy(gameObject);
                
                //    if (Time.time < (v_time + v_tiempo))
                //{
                //    for (int i = 0; i < v_enemigos.Count; i++) //BUG a veces la referencia al enemigo ya no existe
                //    {
                //        if (v_enemigos[i] != null && v_enemigos[i].activeInHierarchy)
                //            v_enemigos[i].SendMessage("Fn_Detener", true, SendMessageOptions.DontRequireReceiver);
                //    }
                //}
                //else//ya se debe quitar
                //{
                //    if (v_enemigos.Count > 0)
                //    {
                //        for (int i = 0; i < v_enemigos.Count; i++)
                //        {
                //            if (v_enemigos != null && v_enemigos[i].activeInHierarchy)
                //                v_enemigos[i].SendMessage("Fn_Detener", false, SendMessageOptions.DontRequireReceiver);
                //        }
                //        v_enemigos.Clear();
                //    }
                //    Destroy(gameObject);
                //}
            }
        }
        //public void Fn_Iniciar(float _rango, float _velo, GameObject _quien, float _tiempo)
        /// <param name="_velo">velocidad para addforce</param>
        /// <param name="_tiempo">tiempo que estaq activo</param>
        public void Fn_Iniciar( float _velo, float _tiempo)
        {
           // v_rango = _rango;
            v_velocidad = _velo;
            v_rig = GetComponent<Rigidbody>();
            v_rig.isKinematic = false;
            v_rig.useGravity = true;
            GetComponent<SphereCollider>().enabled = true;
            GetComponent<BoxCollider>().enabled = false;
            //v_Iniciado = true;
           // v_quien = _quien;
            v_tiempo = _tiempo;
            //gameObject.SetActive(false);
            v_anim = GetComponent<Animator>();
            v_anim.enabled = false;
            GetComponent<Audio.Au_Manager>().Fn_Inicializa();
            //v_anim.SetBool("v_choca", false);
        }
        //private void OnEnable()
        //{
        //    if (v_Iniciado)
        //    {
        //        v_PosIn = transform.position;
        //    }
        //}
        //private void OnDisable()
        //{
        //    v_rig.velocity = Vector3.zero;
        //    transform.rotation = Quaternion.identity;
        //}
        public void Fn_Disparo(Vector3 _Pos, Vector3 _forward)
        {
            //gameObject.SetActive(true);
            v_rig.useGravity = true;
            v_rig.isKinematic = false;
            //transform.position = _Pos;
            v_PosIn = _Pos;
            v_rig.AddForce(_forward * v_velocidad);
            //Debug.Break();
        }
        private void FixedUpdate()
        {
            if (Vector3.Distance(v_PosIn, transform.position) > 50)
            {
                Debug.LogError("Destruido "+ gameObject.name);
                Destroy(gameObject);
            }
        }
        private void OnCollisionEnter(Collision _coll)
        {
            //https://forum.unity.com/threads/quaternion-rotation-along-normal.22727/ 
            if (!v_choca&&  _coll.transform.tag == "Piso")
            {
                GetComponent<Audio.Au_Manager>().Fn_SetAudio(0, false, true);
                v_choca = true;
                //Debug.LogError("collision " + v_choca, gameObject);
                v_rig.velocity = Vector3.zero;
                v_rig.useGravity = false;
                v_rig.isKinematic = true;
                transform.position = _coll.contacts[0].point;
                float _rand = Random.Range(0, 359);
                transform.rotation = Quaternion.Euler(270, _rand, 0);
                v_anim.enabled = true;
                v_anim.SetBool("v_choca",v_choca);
                v_time = Time.time;
                GetComponent<SphereCollider>().enabled = false;
                GetComponent<BoxCollider>().enabled = true;
                //Debug.Break();
                //GameObject _dec = Instantiate(v_prefab, _coll.contacts[0].point, Quaternion.identity);
                //_dec.GetComponent<Sticky>().Fn_SetIniciar(v_tiempo);
                //gameObject.SetActive(false);
            }
        }
    }
}