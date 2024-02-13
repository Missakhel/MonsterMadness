using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace INTERFAZVR
{
    public class Ifz_Confirma : MonoBehaviour
    {
        public GameObject v_ObjContinuar;
        public BoxCollider[] v_coll;
       // public GameObject v_PanelBtn;
        public void Fn_ApagaPanel(GameObject _obj)
        {
            _obj.SetActive(false);
        }
        public void Fn_PrendePanel(GameObject _obj)
        {
            _obj.SetActive(true);
        }
        public void Fn_Siguiente(string _funcion)
        {
            v_ObjContinuar.SendMessage(_funcion);
        }
        public void Fn_Collider(bool _val)
        {
            //v_PanelBtn.SetActive(_val);

            if(transform.childCount>0)
            {
                for (int i= 0; i< transform.childCount;i++)
                {
                    transform.GetChild(i).gameObject.SetActive(_val);
                }
                //Debug.LogError("childcount>0 "+ _val);
                //gameObject.SetActive(true);
            }
            else
            {
                //Debug.LogError("no child "+ _val);
                gameObject.SetActive(_val);
            }

            //BoxCollider[] _arr = GetComponents<BoxCollider>();
            //if (_arr.Length == 0)
            //{
            //    _arr = GetComponentsInChildren<BoxCollider>();
            //}
            //v_coll = _arr;
            //if (_arr.Length > 0)
            //{
            //    for (int i = 0; i < _arr.Length; i++)
            //    {
            //        _arr[i].enabled = _val;
            //    }
            //}
        }
    }
}
