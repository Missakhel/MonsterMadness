using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script avisa cuando la cabeza impacta con una pared

public class VR_VericadorOjos : MonoBehaviour
{

    [System.NonSerialized]
    public int numColisiones = 0; //Va ayudar cuando debemos controlar cabeza por codigo
    public VR_BlackScreen blackScreen;
    public VR_BlackScreen blackScreenEditor;

    int capaCheck;
    Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
        //Calculamos capa
        capaCheck = 1 << k.Layers.PLAYER;
        //capa2 |= (1 << k.Layers.MANO);
        capaCheck = ~capaCheck; //Ignorar
    }

    /*private void Update()
    {
        //Vericamos si ya no quedan por movimiento
        if(numColisiones > 0)
        {
            if(!Physics.CheckSphere(transform.position, RadioCollider, capaCheck, QueryTriggerInteraction.Ignore))
            {
                //Si ya no hay nadaa colisionando, forzamos a 0
                numColisiones = 0;
            }
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == k.Layers.ESCENARIO || other.gameObject.layer == k.Layers.ENEMY)// || other.gameObject.layer == k.Layers.VENTANA)
        {
            //Calculamos distancia de impacto a pared


            //numColisiones++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == k.Layers.ESCENARIO || other.gameObject.layer == k.Layers.ENEMY 
            || other.gameObject.tag== k.Tags.CABEZA)//|| other.gameObject.layer == k.Layers.VENTANA)
        {
            Vector3 dir;
            float dist;
            Physics.ComputePenetration(col, transform.position, Quaternion.identity, other, other.transform.position, other.transform.rotation, out dir, out dist);
            dist *= 5f;
            dist = Mathf.Clamp01(dist);
            blackScreen.ColorActual = new Color(0f, 0f, 0f, dist);
            blackScreen.Activado = dist > 0f;


            blackScreenEditor.ColorActual = new Color(0f, 0f, 0f, dist);
            blackScreenEditor.Activado = dist > 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == k.Layers.ESCENARIO || other.gameObject.layer == k.Layers.ENEMY
            || other.gameObject.tag == k.Tags.CABEZA)//|| other.gameObject.layer == k.Layers.VENTANA)
        {
            //numColisiones--;
            blackScreen.Activado = false;
            blackScreenEditor.Activado = false;
            /* if (numColisiones < 0)
                 numColisiones = 0;*/
        }
    }
}
