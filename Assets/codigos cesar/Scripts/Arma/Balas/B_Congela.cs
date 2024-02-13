using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Armas.Balas
{
    [RequireComponent(typeof(Audio.Au_Manager))]
    public class B_Congela : MonoBehaviour
    {
        //public GameObject v_prefab;
        //public float v_rango = 1000;
        public Vector3 v_PosIn;
        //public bool v_Iniciado = false;
        public float v_velocidad = 0;
        private Rigidbody v_rig;
        //public GameObject v_quien;
        /// <summary>
        /// cuanto tiempo va a existir
        /// </summary>
        public float v_tiempo;
        public float v_tiempoEfecto; 
        /// <summary>
        /// objeto animado
        /// </summary>
        public GameObject v_cristal;
        public GameObject v_meshBala;
        public GameObject v_part;
        [Header("Nueva bala animacion")]
        //Animator v_anim;
        /// <summary>
        /// ya choco con el piso
        /// </summary>
        bool v_choca = false;
        /// <summary>
        /// la hora cuando choco con el piso
        /// </summary>
        float v_time;
        ///// <summary>
        ///// ya puede empezar a hacer efecto
        ///// </summary>
        //public bool v_dano = false;

        public Collider[] v_enemigo;

        private void OnTriggerEnter(Collider other)
        {
            if (!v_choca)
                return;

            //if (!v_dano)
            //{
            //    return;
            //}
            if(other.gameObject.layer == 8 && other.gameObject.tag == "Enemy"  && v_enemigo.Length==0)
            {
                int _capaig = 1 << k.Layers.ESCENARIO;
                _capaig |= (1 << k.Layers.DEFAULT);
                _capaig |= (1 << k.Layers.PLAYER);
                _capaig |= (1 << k.Layers.BALA);
                _capaig |= (1 << k.Layers.MANO);
                _capaig = ~_capaig; //Ignorar
                v_enemigo = Physics.OverlapSphere(transform.position, 2,_capaig);
                v_tiempo +=v_tiempoEfecto;
                v_cristal.SetActive(false);
                v_part.SetActive(true);
                GetComponent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
                StartCoroutine(Ie_Delay());
            }
            //v_anim.SetTrigger("v_efecto");
        }
        IEnumerator Ie_Delay()
        {
            for (int i = 0; i < v_enemigo.Length; i++)
            {
                if (v_enemigo[i].gameObject.activeInHierarchy && v_enemigo[i].gameObject.layer == k.Layers.ENEMY 
                    && v_enemigo[i].gameObject.tag== k.Tags.ENEMY)
                {
                    //Debug.LogError("Pega a "+ v_enemigo[i].gameObject.name, v_enemigo[i].gameObject);
                    GetComponent<BoxCollider>().enabled = false;
                    v_enemigo[i].SendMessage("Fn_Congela", SendMessageOptions.DontRequireReceiver);
                    v_enemigo[i].SendMessage("Fn_CongelaDetiene", true, SendMessageOptions.RequireReceiver);
                }
            }
            yield return new WaitForSeconds(v_tiempoEfecto);
            //for (int i = 0; i < v_enemigo.Length; i++)
            //{
            //    if (v_enemigo[i].gameObject.activeInHierarchy)
            //    {
            //        if (v_enemigo[i].gameObject.activeInHierarchy)
            //        {
            //            v_enemigo[i].SendMessage("Fn_CongelaDetiene", false, SendMessageOptions.RequireReceiver);
            //        }
            //    }
            //}
            StopAllCoroutines();
            Destroy(gameObject, 2);
                /*
                if(v_enemigo.activeInHierarchy)
                {
                    GetComponent<BoxCollider>().enabled = false;
                    //Enemigos. C_Efecto _efe = new Enemigos.C_Efecto() { v_color = Color.cyan, v_tiempo = v_tiempoEfecto, v_nombre = "Congela" };
                    //Debug.LogError("manda "+ JsonUtility.ToJson(_efe));
                    //v_enemigo.SendMessage("Fn_ColorEfecto", JsonUtility.ToJson(_efe), SendMessageOptions.DontRequireReceiver);
                    v_enemigo.SendMessage("Fn_Congela",  SendMessageOptions.DontRequireReceiver);
                
                    //v_enemigo.SendMessage("Fn_ColorEfecto", Color.cyan, SendMessageOptions.DontRequireReceiver);
                    v_enemigo.SendMessage("Fn_CongelaDetiene", true, SendMessageOptions.RequireReceiver);
                    yield return new WaitForSeconds(v_tiempoEfecto);
                    if(v_enemigo.activeInHierarchy)
                    {
                       // v_enemigo.SendMessage("Fn_ColorEfecto", JsonUtility.ToJson(_efe), SendMessageOptions.DontRequireReceiver);
                        //v_enemigo.SendMessage("Fn_ColorEfecto", Color.white, SendMessageOptions.DontRequireReceiver);
                        v_enemigo.SendMessage("Fn_CongelaDetiene", false, SendMessageOptions.RequireReceiver);       
                    }
                    StopAllCoroutines();
                    Destroy(gameObject, 2);
                   // DestroyImmediate(gameObject);
                }
            }
                */
        }
        //public void Fn_SetDano()
        //{
        //    v_dano = true;
        //}
        //private void OnTriggerStay(Collider other)
        //{
        //    if (!v_choca)
        //        return;

        //    if (!v_dano)
        //    {
        //        return;
        //    }
        //    if (gameObject.activeInHierarchy)
        //    {
        //        if (other.gameObject.layer == 8 && other.gameObject.tag == "Enemy")
        //        {
        //            if (!v_enemigos.Contains(other.gameObject))
        //            {
        //                v_enemigos.Add(other.gameObject);
        //            }
        //            v_anim.SetBool("v_durante", true);
        //        }
        //    }
        //}
        //private void OnTriggerExit(Collider other)
        //{
        //    for (int i = 0; i < v_enemigos.Count; i++)
        //    {
        //        if (v_enemigos != null && v_enemigos[i].activeInHierarchy)
        //        {
        //            v_enemigos[i].SendMessage("Fn_Detener", false, SendMessageOptions.RequireReceiver);
        //            v_enemigos[i].SendMessage("Fn_ColorEfecto", Color.white, SendMessageOptions.DontRequireReceiver);
        //        }
        //    }
        //    v_enemigos.Clear();
        //    v_anim.SetBool("v_durante", false);
        //}
        private void Update()
        {
           // v_posiciones.Add(transform.position);
            if (v_choca)
            {
                if (Time.time > (v_time + v_tiempo))
                {
                    if (v_enemigo != null)
                    {
                        //C_Efecto _efe = new C_Efecto() { v_color = Color.white, v_tiempo = 0.1f };
                        //v_enemigo.SendMessage("Fn_ColorEfecto", JsonUtility.ToJson(_efe), SendMessageOptions.DontRequireReceiver);
                       // v_enemigo.SendMessage("Fn_ColorEfecto", Color.white, SendMessageOptions.DontRequireReceiver);
                       for(int i=0; i<v_enemigo.Length; i++)
                        {
                            //v_enemigo.SendMessage("Fn_CongelaDetiene", false, SendMessageOptions.RequireReceiver);
                            v_enemigo[i].SendMessage("Fn_CongelaDetiene", false, SendMessageOptions.RequireReceiver);
                        }
                        //v_enemigo.GetComponent<Enemigos.Enemigo_base>();
                      //  Destroy(gameObject, 2);
                    }
                    v_cristal.SetActive(false);
                        GetComponent<BoxCollider>().enabled = false;
                        GetComponent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
                        Destroy(gameObject, 1);
                }
            }
        }
        //public void Fn_Iniciar(float _rango, float _velo, GameObject _quien, float _tiempo)
        /// <param name="_velo">velocidad para addforce</param>
        /// <param name="_tiempo">tiempo que estaq activo</param>
        public void Fn_Iniciar(float _velo, float _tiempo)
        {
            v_part.SetActive(false);
            v_cristal.SetActive(false);
            v_meshBala.SetActive(true);
            v_tiempoEfecto = 5;
            // v_rango = _rango;
            v_velocidad = _velo;
            v_rig = GetComponent<Rigidbody>();
            v_rig.isKinematic = false;
            v_rig.useGravity = true;
            GetComponent<SphereCollider>().enabled = true;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Audio.Au_Manager>().Fn_Inicializa();
            //v_Iniciado = true;
            // v_quien = _quien;
            v_tiempo = _tiempo;
            //gameObject.SetActive(false);
            //v_anim = GetComponent<Animator>();
            //v_anim.enabled = false;

            //v_anim.SetBool("v_choca", false);
        }
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
                Debug.LogError("Destruido " + gameObject.name);
                DestroyImmediate(gameObject);
            }
        }
        private void OnCollisionEnter(Collision _coll)
        {
            //https://forum.unity.com/threads/quaternion-rotation-along-normal.22727/ 
            if (!v_choca && _coll.transform.tag == "Piso" )
            {
                GetComponent<Audio.Au_Manager>().Fn_SetAudio(0, false, true);
                v_rig.velocity = Vector3.zero;
                v_rig.useGravity = false;
                v_choca = true;
                transform.position = _coll.contacts[0].point;
                v_rig.isKinematic = true;
                float _rand = Random.Range(0, 359);
                transform.rotation = Quaternion.Euler(0, _rand, 0);
                //v_anim.enabled = true;
                //v_anim.SetBool("v_choca", v_choca);
                //Debug.LogError(_coll.contacts[0].point+ "  choca" + _coll.contacts[0].otherCollider.name ,gameObject);
                v_time = Time.time;
                GetComponent<SphereCollider>().enabled = false;
                GetComponent<BoxCollider>().enabled = true;
                v_cristal.SetActive(true);
                v_meshBala.SetActive(false);
                //Destroy(gameObject, 2);
                ///z arriba
               // Debug.Break();
                //GameObject _dec = Instantiate(v_prefab, _coll.contacts[0].point, Quaternion.identity);
                //_dec.GetComponent<Sticky>().Fn_SetIniciar(v_tiempo);
                //gameObject.SetActive(false);
            }
        }
    }
}
