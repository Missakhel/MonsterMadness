using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
using UnityEngine.AI;
namespace Items
{
    [RequireComponent(typeof(NavMeshObstacle), typeof(Audio.Au_Manager))]
    public class Item_Obstaculo : Dano_base
    {
        //Hand v_hand;
        public Tienda.Ti_Item v_item;
        public GameObject v_Base;
        public bool v_enPos=false;
        public GameObject v_hijo;
        public Vector3 v_point;
        public Collider[] _objs;
        private void OnEnable()
        {
            v_Vivo =true;
            v_VidaMax = 2;
            v_Vida = v_VidaMax;
            v_Def = 2;
           // transform.GetChild(0).GetChild(0).transform.tag = k.Tags.HIT;
           v_hijo.transform.tag = k.Tags.HIT;
        }
        public override void Fn_SetDano(float _dano)
        {
            print("me hace daño a nuez");
            base.Fn_SetDano(_dano);
        }
        public void Fn_SetDefensa(float _Def)
        {
            v_Def = _Def;
        }
        public override void Fn_Muerto()
        {
            GetComponent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
            v_Vida = 0;
            v_Vivo = false;
            v_hijo.GetComponent<Animator>().SetTrigger("muerto");
            //v_hijo.SetActive(false);
            GetComponentInChildren<NavMeshObstacle>().enabled = false;
            Destroy(gameObject,3);
        }
        public void Fn_Set(Tienda.Ti_Item _item, GameObject _base, Vector3 _pos)
        {
            v_item = _item;
            v_Base = _base;
            v_point = _pos;
            GetComponent<Audio.Au_Manager>().Fn_Inicializa();
        }
        private void Fn_MostrarAviso()
        {
            //if(Player.instance.leftHand)
            //    Player.instance.leftHand.GetComponent<Jugador.UI_Datos>().Fn_MuestraAviso(0,5);

        }
        private void OnTriggerEnter(Collider other)
        {//nombre grande    tag Hit   layer   default
            if (other.name.Contains("plano abajo pega todo"))
            {
                Debug.LogError("plano abajo");
                v_item.Fn_SetFlecha(false);
                v_item.Fn_Set();
            }
            else if(other.name == "grande"  )
            {
                //Player.instance.leftHand.DetachObject(gameObject);
                //SendMessage("OnDetachedFromHand", Player.instance.leftHand);
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.position = v_point;
                transform.rotation = Quaternion.Euler(270, 0,0);
                Fn_MostrarAviso();
            }
            //else if (other.gameObject != v_Base && ! other.gameObject.name.Contains("Plataforma moverse"))
            //{
            //    Debug.LogError("Layer " + other.gameObject.layer + "  TAG COLLISION " + other.gameObject.tag + "  " + other.gameObject.name);
            //    if (!v_enPos && (other.gameObject.tag == k.Tags.PISO))//&& collision.gameObject.name == "MSH_Piso1_Suelo")  )
            //    {
            //        //Debug.LogError("Nombre collision " + other.gameObject.name + " other collider " + collision.contacts[0].otherCollider.name, collision.contacts[0].otherCollider.gameObject);
            //        //Debug.LogError(collision.contacts[0].thisCollider.name, collision.contacts[0].thisCollider.gameObject);
            //        Debug.LogError("Nombre collision " + other.gameObject.name , other.gameObject);
            //        FN_Efecto(other.GetComponent<Collider>().ClosestPoint(transform.position));
            //    }
            //}
        }
        public void Fn_SetToca()
        {
            v_toca = false;
        }
        public bool v_toca=false;
        private void OnCollisionEnter(Collision collision)
        {
            if (!v_toca)
            {
                v_toca = true;
                if (collision.gameObject.name == "grande")
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    transform.position = v_point;
                    transform.rotation = Quaternion.Euler(270, 0, 0);
                    Fn_MostrarAviso();
                    v_toca = false;
                }
                else if (collision.gameObject != v_Base && !collision.transform.name.Contains( "Plataforma moverse"))
                {
                    //Debug.LogError("Layer " + collision.gameObject.layer + "  TAG COLLISION " + collision.gameObject.tag + "  " + collision.gameObject.name);
                    if (!v_enPos && (collision.gameObject.tag == k.Tags.PISO))//&& collision.gameObject.name == "MSH_Piso1_Suelo")  )
                    {
                        //Debug.LogError("Nombre collision " + collision.gameObject.name + " other collider " + collision.contacts[0].otherCollider.name, collision.contacts[0].otherCollider.gameObject);
                        //Debug.LogError(collision.contacts[0].thisCollider.name, collision.contacts[0].thisCollider.gameObject);
                        _objs = Physics.OverlapSphere(collision.contacts[0].point, 0.1f);
                        bool _pos = false;
                        for(int i=0; i<_objs.Length; i++)
                        {
                            if(!_pos)
                            {
                               if(_objs[i].gameObject == v_Base //base
                                  ||  _objs[i].transform.name.Contains( "Plataforma moverse")  //punto donde puedes moverte
                                  ||  (_objs[i].gameObject.tag == k.Tags.OBJ_COLISION &&  _objs[i].transform.name.Contains("Piso Tienda"))    )
                                {
                                    Debug.LogError("Error "+ _objs[i].gameObject.name, _objs[i].gameObject);
                                    _pos = true;
                                }
                            }
                        }
                        if(!_pos)
                        {
                            FN_Efecto(collision.contacts[0]);
                            v_toca = true;
                        }
                        else
                        {
                            v_toca = false;
                        }
                    }
                    else
                    {
                        v_toca = false;
                    }
                }
            }
        }//collision
        private void FN_Efecto(Vector3 _conta)
        {
            GetComponent<Audio.Au_Manager>().Fn_SetAudio(0, false, true);
            gameObject.layer = k.Layers.DEFAULT;
            gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            //transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            v_hijo.SetActive(true);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = new Vector3(_conta.x,
            _conta.y, //+ transform.GetChild(0).GetComponent<BoxCollider>().bounds.extents.y   esto se hacia por el cubo de prueba coon el pivote esta en medio
            _conta.z);
            transform.rotation = Quaternion.identity;//    Euler(270, 0, 0);//para que siempre este en su posicion correcta
            v_item.Fn_SetFlecha(false);
            v_item.Fn_Set();//se le dice al opbjeto en la tienda que ya pueden comprarlos de nuevo
            GetComponent<NavMeshObstacle>().enabled = false;
            Destroy(GetComponent<BoxCollider>());
            //Destroy(GetComponent<Throwable>());
            //Destroy(GetComponent<VelocityEstimator>());
            //Destroy(GetComponent<InteractableHoverEvents>());
            //Destroy(GetComponent<Interactable>());
            Destroy(GetComponent<MeshRenderer>());
            Destroy(v_Base);
            v_Base = null;
            v_item = null;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //gameObject.AddComponent<FixedJoint>();
            //GetComponent<FixedJoint>().connectedBody = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Rigidbody>();
            v_enPos = true;
        }
        private void FN_Efecto(ContactPoint _conta)
        {
            GetComponent<Audio.Au_Manager>().Fn_SetAudio(0, false, true);
            gameObject.layer = k.Layers.DEFAULT;
            gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            //transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            v_hijo.SetActive(true);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = new Vector3(_conta.point.x,
            _conta.point.y, //+ transform.GetChild(0).GetComponent<BoxCollider>().bounds.extents.y   esto se hacia por el cubo de prueba coon el pivote esta en medio
            _conta.point.z);
            transform.rotation = Quaternion.identity;//    Euler(270, 0, 0);//para que siempre este en su posicion correcta
            v_item.Fn_Set();//se le dice al opbjeto en la tienda que ya pueden comprarlos de nuevo
            GetComponent<NavMeshObstacle>().enabled = false;
            Destroy(GetComponent<BoxCollider>());
            //Destroy(GetComponent<Throwable>());
            //Destroy(GetComponent<VelocityEstimator>());
            //Destroy(GetComponent<InteractableHoverEvents>());
            //Destroy(GetComponent<Interactable>());
            Destroy(GetComponent<MeshRenderer>());
            Destroy(v_Base);
            v_Base = null;
            v_item = null;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //gameObject.AddComponent<FixedJoint>();
            //GetComponent<FixedJoint>().connectedBody = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Rigidbody>();
            v_enPos = true;
        }
    }
}
