using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Armas.Balas
{
    [RequireComponent(typeof(MeshCollider))]
    public class B_Esco : MonoBehaviour
    {
        [Header("CONO")]
        public bool v_disparando = false;
        WaitForSeconds v_await = new WaitForSeconds(0.4f);

        //public GameObject v_Decal;
        [Header("Bala")]
        public float v_dano;
        public float v_rango;
        public GameObject v_quien;
        public ParticleSystem v_particula;
        public MeshCollider v_meshColl;
        public MeshRenderer v_mesh;
        public Vector3 v_PosIn;
        public void Fn_Iniciar(float _dano, float _rango, GameObject _quien)
        {
            v_meshColl = GetComponent<MeshCollider>();
            v_mesh = GetComponent<MeshRenderer>();
            v_dano = _dano;
            v_rango = _rango;
            v_quien = _quien;
        }
        public void Fn_Disparo()
        {
            if (v_particula.isPlaying)
            {
                v_particula.Stop();
            }
            v_particula.Play();
            v_PosIn = transform.position;
            StartCoroutine(Ie_Delay());
            //play a la particula
            //v_PosIn = transform.position;
        }
        IEnumerator Ie_Delay()
        {
            v_meshColl.enabled = true;
            v_mesh.enabled = true;
            v_disparando = true;
            yield return v_await;
            v_disparando = false;
            v_meshColl.enabled = false;
            v_mesh.enabled = false;
            StopCoroutine(Ie_Delay());
        }
        public bool Fn_GetDisparo() { return v_disparando; }
        private void OnTriggerEnter(Collider _other)
        {
            if (v_disparando)
            {
                if (_other.transform.tag == k.Tags.PLAYER)
                {
                    return;
                }

                float _dist = Vector3.Distance(_other.gameObject.transform.position, v_PosIn);
                if ((_other.transform.tag == k.Tags.ENEMY || _other.transform.tag == k.Tags.CABEZA || _other.transform.tag == k.Tags.MANO) && _dist <= v_rango)
                {
                    /// (1-(distancia/rango)) * dano
                    float _dano = 0;
                    float _porc = (_dist / v_rango);
                    _porc = (1 - _porc) * v_dano;
                    _dano = Mathf.Clamp(_porc, 2, Mathf.Infinity);
                    if (_other.transform.tag == k.Tags.CABEZA)//la cabeza doble daño
                    {
                        //Debug.LogError("cabeza  "+  "dano  " + _dano * 2.0f + " dist "+ _dist);
                        _other.gameObject.SendMessage("Dano", _dano * 2.0f, SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
                        //Debug.LogError("cuerpo  " + "dano  " + _dano + " dist " + _dist);
                        _other.gameObject.SendMessage("Dano", _dano, SendMessageOptions.DontRequireReceiver);//cuerpo daño normal
                    }
                    _other.gameObject.SendMessage("Dano", v_quien, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                }
                //else if ((_other.transform.tag != k.Tags.ENEMY && _other.transform.tag != k.Tags.VENTANA && _other.transform.tag != k.Tags.HIT && collision.transform.tag != k.Tags.OBJCOLISION)
                //    && _dist <= v_rango)
                //{
                //    Quaternion _rot = Quaternion.identity;
                //    _rot.x = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal).x;// Quaternion.LookRotation(v_hit.point);
                //    _rot.z = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal).z;
                //    ///*GameObject _dec =*/ Instantiate(v_Decal, collision.contacts[0].point, _rot);
                //    ddecals.Spawn(collision.contacts[0].point, _rot);
                //}
            }
        }
        /*
        public float v_dano;
        public GameObject v_Decal;
        public float v_rango;
        //Vector3 v_PosIn;
        public GameObject v_quien;
        ParticleSystem v_particula;
        public void Fn_Iniciar(float _dano, float _rango, GameObject _quien)
        {
            v_particula = GetComponent<ParticleSystem>();
            v_dano = (_dano / v_particula.main.maxParticles);
            v_rango = _rango;
            v_quien = _quien;
        }
        public void Fn_Disparo()
        {
            if (v_particula.isPlaying)
            {
                v_particula.Stop();
            }
            v_particula.Play();
            //play a la particula
            //v_PosIn = transform.position;
        }

        */





        /*
        private void OnCollisionEnter(Collision collision)
        {
            //https://forum.unity.com/threads/quaternion-rotation-along-normal.22727/
            //print("   objeto " + collision.transform.name);

            float _dist = Vector3.Distance(collision.contacts[0].point, v_PosIn);
            if ((collision.transform.tag == "Enemy" || collision.transform.tag == "Cabeza") && _dist <= v_rango)
            {
                float _porc = (_dist / v_rango);
                _porc = Mathf.Clamp(_porc, 0.01f, Mathf.Infinity);
                System.Math.Round(_porc, 2);
                _porc = 1 - _porc;
                _porc *= v_dano;
                if (collision.transform.gameObject.activeInHierarchy)
                {
                    if (collision.transform.tag == "Cabeza")
                    {
                        collision.transform.gameObject.SendMessage("Dano", v_dano * 2.0f);
                    }
                    else
                    {
                        collision.transform.gameObject.SendMessage("Dano", v_dano);
                    }
                    collision.transform.gameObject.SendMessage("Dano", v_quien);
                }
                gameObject.SetActive(false);
            }
            else if (collision.transform.tag != "Enemy" && collision.transform.tag != "Ventana"
                && collision.transform.tag != "Player" && _dist <= v_rango)
            {
                Quaternion _rot = Quaternion.identity;
                _rot.x = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal).x;// Quaternion.LookRotation(v_hit.point);
                _rot.z = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal).z;
                GameObject _dec = Instantiate(v_Decal, collision.contacts[0].point, _rot);
            }
            gameObject.SetActive(false);
        }
        */
        /*public float v_dano;
        public GameObject v_Decal;
        public float v_rango;
        Vector3 v_PosIn;
        public bool v_Iniciado = false;
        public float v_velocidad = 0;
        private Rigidbody v_rig;
        public GameObject v_quien;
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

            }
        }
        private void OnDisable()
        {
            v_rig.velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
        public void Fn_Disparo(Vector3 _Pos, Vector3 _forward)
        {
            gameObject.SetActive(true);
            transform.position = _Pos;
            v_PosIn = _Pos;
            v_rig.AddForce(_forward * v_velocidad);
        }
        private void FixedUpdate()
        {
            if (!v_Iniciado)
                return;
            if (Vector3.Distance(v_PosIn, transform.position) > v_rango)
            {
                gameObject.SetActive(false);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            //https://forum.unity.com/threads/quaternion-rotation-along-normal.22727/
            //print("   objeto " + collision.transform.name);

            float _dist = Vector3.Distance(collision.contacts[0].point, v_PosIn);
            if ((collision.transform.tag == "Enemy" || collision.transform.tag == "Cabeza") && _dist <= v_rango)
            {
                float _porc = (_dist / v_rango);
                _porc= Mathf.Clamp(_porc, 0.01f, Mathf.Infinity);        
                System.Math.Round(_porc, 2);
                _porc = 1 - _porc;
                _porc *= v_dano;
                if(collision.transform.gameObject.activeInHierarchy)
                {
                    if (collision.transform.tag == "Cabeza")
                    {
                        collision.transform.gameObject.SendMessage("Dano", v_dano * 2.0f);
                    }
                    else
                    {
                        collision.transform.gameObject.SendMessage("Dano", v_dano);
                    }
                    collision.transform.gameObject.SendMessage("Dano", v_quien);
                }
                gameObject.SetActive(false);
            }
            else if (collision.transform.tag != "Enemy" && collision.transform.tag != "Ventana"
                && collision.transform.tag != "Player"  && _dist <= v_rango)
            {
                Quaternion _rot = Quaternion.identity;
                _rot.x = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal).x;// Quaternion.LookRotation(v_hit.point);
                _rot.z = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal).z;
                GameObject _dec = Instantiate(v_Decal, collision.contacts[0].point, _rot);
            }
            gameObject.SetActive(false);
        }*/
    }
}