using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Items
{
    //using Valve.VR.InteractionSystem;
    [RequireComponent(typeof(BoxCollider),typeof(Rigidbody),typeof(Audio.Au_Manager))]
    public class Item_Mina : MonoBehaviour {
        public Tienda.Ti_Item v_item;
        public GameObject v_Base;
        public bool v_enPos = false;
        public bool v_explota = false;//ya exploto?
        [Range(0.01f,3.0f)]
        public float v_radio=1.2f;//radio de la explosion dano
        public float v_dano=60.0f;
        public Collider[] _danados;
        public ParticleSystem v_particle;
        private void Awake()
        {
            v_particle.Stop();
            v_radio =1.2f;
            v_dano = 60.0f;
        }
        public void Fn_Set(Tienda.Ti_Item _item, GameObject _base)
        {
            v_item = _item;
            v_Base = _base;
            GetComponent<Audio.Au_Manager>().Fn_Inicializa();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject != v_Base   )
            {
                if (!v_enPos && !v_explota  && collision.transform.name != "Plataforma moverse"  && collision.gameObject.tag != k.Tags.OBJ_COLISION) 
                {

                    //NavMeshHit hitNav;
                    //bool Factible = NavMesh.SamplePosition(collision.contacts[0].point, out hitNav, 0.05f, NavMesh.AllAreas);
                    //if (!Factible)
                    //{
                    //    //Camino bloqueado
                    //    //Debug.LogError("no factible");
                    //    CambiarPos = false;
                    //    debugColorLinea = Color.red;
                    //}
                    //else
                    //{
                    //    //choca con un objeto que si puedes hacer el cambio
                    //    //Debug.LogError("navmesh sample position true");
                    //    CambiarPos = true;
                    //    v = hit.transform.gameObject;
                    //    v_escalera = false;
                    //    debugColorLinea = Color.blue;
                    //}


                    Destroy(v_Base);
                        v_Base = null;
                        v_enPos = true;
                        v_item.Fn_SetFlecha(false);
                        v_item.Fn_Set();//se le dice al opbjeto en la tienda que ya pueden comprarlos de nuevo
                        v_item = null;
                        //Destroy(GetComponent<Throwable>());
                        //Destroy(GetComponent<VelocityEstimator>());
                        //Destroy(GetComponent<InteractableHoverEvents>());
                        //Destroy(GetComponent<Interactable>());
                        transform.GetChild(1).gameObject.SetActive(false);//apagar el modelo grande
                        GetComponent<Rigidbody>().velocity = Vector3.zero;
                        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    if ((collision.gameObject.tag == k.Tags.PISO))// && collision.gameObject.name == "MSH_Piso1_Suelo"))//ponerse en el suelo
                    {
                        //Debug.Break();
                        //Debug.LogError("Pisoooo");
                        gameObject.layer = k.Layers.DEFAULT;
                        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);//prender el objeto que trae la animacion
                        transform.rotation = Quaternion.identity;//para que siempre este en su posicion correcta
                        transform.position = new Vector3(transform.position.x, 
                            collision.contacts[0].point.y-transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().bounds.extents.z,transform.position.z);
                        GetComponent<Rigidbody>().isKinematic = true;
                        GetComponent<Rigidbody>().useGravity = false;
                        GetComponent<BoxCollider>().isTrigger = true;// prender uno grande que es el quie detecta
                        GetComponent<BoxCollider>().size = new Vector3(0.35f, 0.35f, 0.35f);// prender el que detecta
                    }
                    else //if(collision.gameObject.tag== k.Tags.ENEMY)//explota cuando le pegue al enemigo
                    {
                       // Debug.Break();
                        //Debug.LogError(collision.contacts[0].otherCollider.gameObject.name) ;
                        Destroy(GetComponent<MeshRenderer>());
                        Fn_Explota();
                    }
                }
            }
        }//on collision
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Contains("Mina") && !v_explota)
            {
                //Debug.Break();
                //Debug.LogError("minaaa trigger");
                if (v_Base!= null)
                {
                    Destroy(v_Base);
                }
                v_Base = null;
                if(v_item != null)
                {
                    v_item.Fn_SetFlecha(false);
                    v_item.Fn_Set();//se le dice al opbjeto en la tienda que ya pueden comprarlos de nuevo
                }
                v_item = null;
                transform.GetChild(0).gameObject.SetActive(false);
                Fn_Explota();
            }
            else if ( other.gameObject.layer == k.Layers.ENEMY  &&!v_explota && v_enPos)//other.tag== k.Tags.ENEMY && 
            {
                //Debug.Break();
                //Debug.LogError("enemigo  trigger");
                if (v_Base != null)
                {
                    Destroy(v_Base);
                }
                v_Base = null;
                if (v_item != null)
                {
                    v_item.Fn_SetFlecha(false);
                    v_item.Fn_Set();//se le dice al opbjeto en la tienda que ya pueden comprarlos de nuevo
                }
                v_item = null;
                transform.GetChild(0).gameObject.SetActive(false);
                Fn_Explota();
                //Ie_Cooldown();
            }
        }
        private void Fn_Explota()
        {
            // print("  EXPLOTAAAA  " +gameObject.name);
            GetComponent<Audio.Au_Manager>().Fn_SetAudio(0, false, true);
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);//apagar el modelo grande
            v_particle.Play();
            v_explota = false;
            int _capaig = 1 << k.Layers.ESCENARIO;
            _capaig |= (1 << k.Layers.DEFAULT);
            _capaig |= (1 << k.Layers.PLAYER);
            _capaig = ~_capaig; //Ignorar
            _danados = Physics.OverlapSphere(transform.position, v_radio,_capaig);
           // Debug.ClearDeveloperConsole();
          // Debug.Break();
            for(int i=0; i<_danados.Length; i++)
            {//print("dano a " + _danados[i].gameObject.name);
                _danados[i].gameObject.SendMessage("Dano", v_dano,SendMessageOptions.DontRequireReceiver);
            }
            GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(Ie_Cooldown());
        }
        IEnumerator Ie_Cooldown()
        {
            yield return new WaitForSeconds(2.0f);
            DestroyImmediate(gameObject);
        }
        /*private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(gameObject.transform.position, v_radio);
        }*/
    }
}
