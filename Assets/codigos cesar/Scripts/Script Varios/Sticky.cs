using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class Sticky : MonoBehaviour {

    public bool v_activo=false;
    public float v_tiempo;
    float v_time;
    public List<GameObject> v_enemigos = new List<GameObject>();
    //Animator v_anim;
    //private void Awake()
    //{
    //    v_anim = GetComponent<Animator>();
    //}
    /// <summary>
    /// se activa el efecto
    /// </summary>
    public void Fn_SetIniciar(float _tiempo)
    {

        v_tiempo = _tiempo;
        v_activo = true;
        v_time = Time.time;
    }
    private void Update()
    {
        if(v_activo && Time.time< (v_time+v_tiempo))
        {
            for (int i=0; i< v_enemigos.Count; i++) //BUG a veces la referencia al enemigo ya no existe
            {
                if(v_enemigos!= null  && v_enemigos[i].activeInHierarchy)
                    v_enemigos[i].SendMessage("Fn_Detener", true, SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            if(v_enemigos.Count>0)
            {
                for (int i = 0; i < v_enemigos.Count; i++)
                {
                    if (v_enemigos != null && v_enemigos[i].activeInHierarchy)
                        v_enemigos[i].SendMessage("Fn_Detener", false, SendMessageOptions.DontRequireReceiver);
                }
                v_enemigos.Clear();
            }
            Destroy(gameObject);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(gameObject.activeInHierarchy)
    //    {
    //        v_anim.SetBool("",true);
    //    }
    //}
    void OnTriggerStay(Collider _coll)
    {
        if (gameObject.activeInHierarchy)
        {
            if (v_activo && _coll.gameObject.layer == 8 && _coll.gameObject.tag == "Enemy")
            {
                if (!v_enemigos.Contains(_coll.gameObject))
                {
                    v_enemigos.Add(_coll.gameObject);
                }
            }
        }
    }
}
