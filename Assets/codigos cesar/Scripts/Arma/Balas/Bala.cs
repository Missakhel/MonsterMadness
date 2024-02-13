using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Armas.Balas
{ 
    [RequireComponent(typeof(Rigidbody))]
    public class Bala : MonoBehaviour {
        public float v_dano =0;
        public GameObject v_Decal;
        public float v_rango =1000;
        Vector3 v_PosIn;
        public bool v_Iniciado=false;
        public float v_velocidad=0;
        private Rigidbody v_rig;
        public GameObject v_quien;
        private GameObject v_Part;
        static scr_DecalPool ddecals = null;
        static bool decalsIniciado = false; //Bug raro, ddeclas == null estaba fallando?, asi qeu mejor un bool
        private void Start()
        {
            //Iniciamos decals
            if (decalsIniciado == false)
            {
                v_Part = transform.GetChild(0).gameObject;
                ddecals = new scr_DecalPool();
                ddecals.Init(v_Decal);
                decalsIniciado = true;
            }
        }
        public void Fn_Iniciar(float _dano, float _rango, float _velo, GameObject _quien)
        {
            v_dano = _dano;
            v_rango = _rango;
            v_velocidad = _velo;
            v_rig = GetComponent<Rigidbody>();
            v_Iniciado = true;
            v_quien = _quien;
            gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            if (v_Iniciado)
            {
                v_PosIn = transform.position;
                if(v_Part!= null)
                {
                    v_Part.SetActive(true);
                }
            }
        }
        private void OnDisable()
        {
            v_rig.velocity= Vector3.zero;
            transform.rotation = Quaternion.identity;
            if (v_Part != null)
            {
                v_Part.SetActive(false);
            }
        }
        public void Fn_Disparo(Vector3 _Pos, Vector3 _forward)
        {
            transform.position = _Pos;
            gameObject.SetActive(true);
            v_PosIn = _Pos;
            v_rig.AddForce(_forward * v_velocidad);
        }
        private void FixedUpdate()
        {
            if (!v_Iniciado)
                return;
        
            if (Vector3.Distance(v_PosIn, transform.position) > v_rango)
            {
                v_rig.velocity = Vector3.zero;
                gameObject.SetActive(false);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            //https://forum.unity.com/threads/quaternion-rotation-along-normal.22727/
            float _dist = Vector3.Distance(collision.contacts[0].point, v_PosIn);
            if(collision.transform.tag == k.Tags.PLAYER)
            {
                gameObject.SetActive(false);
            }
            if ( (collision.transform.tag == k.Tags.ENEMY || collision.transform.tag == k.Tags.CABEZA  || collision.transform.tag== k.Tags.MANO) && _dist <= v_rango)
            {
                if (collision.transform.tag == k.Tags.CABEZA)//la cabeza doble daño
                {
                    collision.transform.gameObject.SendMessage("Dano", v_dano*2.0f, SendMessageOptions.DontRequireReceiver);                
                }
                else {
                    collision.transform.gameObject.SendMessage("Dano", v_dano,SendMessageOptions.DontRequireReceiver);//cuerpo daño normal
                }
                collision.transform.gameObject.SendMessage("Dano", v_quien,SendMessageOptions.DontRequireReceiver);
                gameObject.SetActive(false);
            }
            else if ((collision.transform.tag != k.Tags.ENEMY && collision.transform.tag != k.Tags.VENTANA && collision.transform.tag!=k.Tags.HIT && collision.transform.tag != k.Tags.OBJ_COLISION) 
                && _dist <= v_rango)
            {
                Quaternion _rot = Quaternion.identity;
                _rot.x = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal).x;// Quaternion.LookRotation(v_hit.point);
                _rot.z = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal).z;
                ///*GameObject _dec =*/ Instantiate(v_Decal, collision.contacts[0].point, _rot);
                ddecals.Spawn(collision.contacts[0].point, _rot, collision.contacts[0].otherCollider.gameObject);
            }
            v_rig.velocity = Vector3.zero;
            transform.position = v_PosIn;
            gameObject.SetActive(false); 
        }
}
}
