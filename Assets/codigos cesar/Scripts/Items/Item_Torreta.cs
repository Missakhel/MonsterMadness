using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Items
{    /*
         EL COLLIDER QUE NECESITA ESTE CODIGO ES EL DE "VISION"
         CUANDO ENTREN A ESTE COLLIDER LES PUEDE DISPARAR
             */
    using Armas.Balas;
    [RequireComponent(typeof(UnityEngine.AI.NavMeshObstacle), typeof(Audio.Au_Manager))]
    public class Item_Torreta : Dano_base {
        /// <summary>
        /// ENEMIGO A DISPARAR
        /// </summary>
        public GameObject v_Enem;
        /// <summary>
        /// DIRECCION DEL ENEMIGO SEGUN YO
        /// </summary>
       public Vector3 v_Dir;
        /// <summary>
        /// PREFAB DE BALA A DISPARAR
        /// </summary>
        public GameObject v_PrefBala;
        /// <summary>
        /// DISPARANDO?
        /// </summary>
        public bool v_dispara = false;
        /// <summary>
        /// DAÑO A LOS ENEMIGOS
        /// </summary>
        public float v_Dano=6;
        /// <summary>
        /// OBJETO A ROTAR 
        /// </summary>
        public Transform v_Canon;
        /// <summary>
        /// LUGAR DE DONDE SALE LA BALA
        /// </summary>
        public Transform v_salebala;
        /// <summary>
        /// BALA ACTUAL EN EL POOL
        /// </summary>
        public int v_idPool = 0;
        /// <summary>
        /// MAXIMO DE BALAS
        /// </summary>
        public int v_MaxCargador = 20;
        /// <summary>
        /// BALAS A DISPARAR, NO DESTRUIR
        /// </summary>
        public List<GameObject> v_pool = new List<GameObject>();
        /// <summary>
        /// GIRO EN EL STAY
        /// </summary>
        Quaternion NuevaRot= Quaternion.identity;
        WaitForSeconds v_await;
        public UnityEngine.UI.Text v_text;
        public Audio.Au_Manager v_audio;
        public GameObject v_part;
        private void OnEnable()
        {
            if(v_text== null)
            {
                Debug.LogError("FALTA EL TEXTO DE VIDA "+gameObject.name);
                Debug.Break();
            }
            v_audio = GetComponent<Audio.Au_Manager>();
            v_audio.Fn_Inicializa();
            v_audio.Fn_SetAudio(0, false, true);
            v_part.SetActive(false);
            v_await = new WaitForSeconds(0.2f);
            v_VidaMax = 100;
            v_Vida = v_VidaMax;
            v_text.text = v_Vida.ToString();
            v_Def = 2;
            v_Dano =9.0f;
            Fn_Pool();
            v_idPool = 0;
            v_Vivo = true;
        }
        /// <summary>
        /// CREAR TODAS LAS BALAS PARA DESPUES USARLAS
        /// </summary>
        public  void Fn_Pool()
        {
            v_idPool = 0;
            GameObject _inst;
            for (int i = 0; i < v_MaxCargador; i++)
            {
                _inst = Instantiate(v_PrefBala, v_salebala.position, Quaternion.identity,gameObject.transform);
                _inst.GetComponent<Bala>().Fn_Iniciar(v_Dano, 15, 4000.0f, gameObject);
                v_pool.Add(_inst);
            }
        }
        /// <summary>
        /// ALGUIEN ENTRA EN MI AREA DE DISPARO
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag=="Enemy" && v_Enem==null && v_Vivo)
            {
                v_Enem = other.gameObject;
                v_Dir = v_Enem.transform.position - v_Canon.transform.position;
            }
        }
        /// <summary>
        /// ALGUIEN SE SALE DEL RANGO
        /// </summary>
        private void OnTriggerExit(Collider other)
        {
            if(other.tag=="Enemy" && v_Enem==other.gameObject)
            {
                v_Enem = null;
                StopCoroutine(Ie_Dispara());
            }
        }
        /// <summary>
        /// MIENTRAS AGUIEN ESTA DENTRO DE MI ZONA
        /// </summary>
        private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.layer ==8 )//LAYER 8 ES QUE ESTA VIVO NO MUERTO
            {
                if(v_Enem != null && v_Vivo)//TENGO A ALGUIEN EN LA MIRA Y ESTA VIVO
                {
                    v_Dir = v_Enem.transform.position - v_Canon.position;//CALCULOS DE ROTACION
                    v_Dir.y = 0;// NO ROTARLO VERTICAL
                    //v_Dir.z = 0;
                    NuevaRot = Quaternion.LookRotation(v_Dir);
                    v_Canon.transform.rotation = Quaternion.Slerp(v_Canon.transform.rotation, NuevaRot, Time.deltaTime * 5.0f);//ROTAR EL CAÑON
                    if (!v_dispara)//NO ESTOY DISPARANDO
                    {
                        v_pool[v_idPool].GetComponent<Bala>().Fn_Disparo(v_salebala.position, v_salebala.forward);//DISPARA BALA ACTUAL
                        v_idPool++;
                        // v_nue.GetComponent<Rigidbody>().AddForce(v_Dir * 1000.0f);
                        StartCoroutine(Ie_Dispara());
                    }
                    if(v_Enem.layer==10)//SI SE MURIO, LO IGNORO
                    {
                        v_Enem = null;
                        StopCoroutine(Ie_Dispara());
                    }
                }
                else if(other.tag == "Enemy" && v_Enem == null && v_Vivo)// SI NO TENGO A NADIE, Y ESTA VIVO
                {
                    StopCoroutine(Ie_Dispara());
                    v_Enem = other.gameObject;
                }
            }
            v_text.text = v_Vida.ToString();
        }
        /// <summary>
        /// ME MORI
        /// </summary>
        public override void  Fn_Muerto()
        {
            v_part.SetActive(true);
            v_audio.Fn_SetAudio(2, false, true);
            GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;//PARA YA NO ESTORBAR
            if(GetComponentInParent<Tienda.Ti_Ventana>())
            {
                GetComponentInParent<Tienda.Ti_Ventana>().Fn_Torre(false);//AVISAR A MI VENTANA QUE ME MORI
            }
            v_Vida =0;
            v_text.text = v_Vida.ToString();
            v_Vivo = false;
            v_Enem = null;
            Debug.LogError("torreta destruida "+ GetComponentInParent<Tienda.Ti_Ventana>().id);
            Destroy(gameObject,1);
        }
        /// <summary>
        /// SET AL DAÑO
        /// </summary>
        public override void Fn_SetDano(float _dano)
        {
            print("dano a torreta");
            base.Fn_SetDano(_dano);
        }
        /// <summary>
        /// COOLDOWN Y RECARGA SI ES NECESARIO
        /// </summary>
        IEnumerator Ie_Dispara()
        {
            v_audio.Fn_SetAudio(1, false, true);
            v_dispara = true;
            yield return v_await;
            if(v_idPool>=v_MaxCargador)
            {
                v_idPool = 0;
            }
            v_dispara = false;
        }
    }
}
