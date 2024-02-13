using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public Transform[] v_Posiciones;

    void Awake()
    {
        v_Posiciones = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            v_Posiciones[i] = transform.GetChild(i);
        }
        GetComponent<BoxCollider>().isTrigger = true;
    }
    void OnTriggerEnter(Collider _other)
    {
        if(_other.tag=="Enemy")
        {
            print("mover enemy");
            Fn_Mover(_other.gameObject);
        }else if(_other.tag=="Mano")
        {
            print("mover mano");
            Fn_Mover(_other.GetComponent<Enemigos.Hit_Mano>().Fn_GetPadre());
        }
    }
    void Fn_Mover(GameObject _enem)
    {
        int _pos = Random.Range(0, v_Posiciones.Length);
        _enem.SendMessage("Fn_Detener", SendMessageOptions.DontRequireReceiver);
        _enem.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        _enem.transform.position = v_Posiciones[_pos].position;
        _enem.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
    }
    private void OnDrawGizmosSelected()
    {
        for(int i=0; i<v_Posiciones.Length; i++)
        {
            Gizmos.color = new Color(0, 1, 1, 1.0F);
            Gizmos.DrawSphere(v_Posiciones[i].position, 0.05f);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        print("collision " + other.name);
    }

    private void OnParticleTrigger()
    {
        print("trigger");
    }
}
