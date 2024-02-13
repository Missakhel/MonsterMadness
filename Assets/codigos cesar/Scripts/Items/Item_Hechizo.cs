using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Items
{
    using Manager;
    //using Valve.VR.InteractionSystem;
    [RequireComponent(typeof(BoxCollider), typeof(Rigidbody), typeof(Audio.Au_Manager))]
    public class Item_Hechizo : MonoBehaviour
    {
        [Header("Unico de hechizo")]
        public GameObject v_Base;
        public Tienda.Ti_Hechizo v_item;
        bool v_choca = false;
        public GameObject v_obj;
        public GameObject v_prefab;
        void Awake()
        {
            v_obj.SetActive(false);
            v_prefab.SetActive(true);
            // ColorUtility.TryParseHtmlString("D45353", out v_color);
            //Fn_Config(200);
        }
        public void Fn_Set(Tienda.Ti_Hechizo _item, GameObject _base)
        {
            v_item = _item;
            v_Base = _base;
            GetComponent<Audio.Au_Manager>().Fn_Inicializa();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if(v_Base!= null && v_item!= null)
            {
                //if (!v_enPos && !v_explota && collision.transform.name != "Plataforma moverse")//&& collision.gameObject.tag != k.Tags.OBJCOLISION) 
                  if ((collision.gameObject != v_Base  && collision.gameObject != v_item.gameObject ) && !v_choca && collision.gameObject.tag != k.Tags.OBJ_COLISION)
                {
                    //Debug.LogError("Pega "+ collision.gameObject.name, collision.gameObject);
                    transform.rotation = Quaternion.identity;
                    v_obj.SetActive(true);
                    v_prefab.SetActive(false);
                    v_choca = true;
                    Destroy(v_Base);
                    v_Base = null;
                    v_item.Fn_SetFlecha(false);
                    v_item.Fn_Set();//se le dice al opbjeto en la tienda que ya pueden comprarlos de nuevo
                    v_item = null;
                    //Destroy(GetComponent<Throwable>());
                    //Destroy(GetComponent<VelocityEstimator>());
                    //Destroy(GetComponent<InteractableHoverEvents>());
                    //Destroy(GetComponent<Interactable>());
                    //transform.GetChild(1).gameObject.SetActive(false);//apagar el modelo grande
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    GetComponent<Audio.Au_Manager>().Fn_SetAudio(0,false,true);
                    Manager_Horda.Instance.Fn_MataTodo();                   
                    StartCoroutine(Ie_Delay());
                }
            }
        }//on collision
        IEnumerator Ie_Delay()
        {
            v_choca = true;
            yield return new WaitForSeconds(2.0f);
            Destroy(gameObject);
        }
    }
}
