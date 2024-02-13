using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script que neutraliza el movimiento de la cabeza para eivtar entre a una pared
//SIN USO

public class VR_ControlCabeza : MonoBehaviour {

    //Referencias
    public VR_VericadorOjos ojos;
    public Transform cabezaTransoform;

    //Interno
    Transform transform_;
    Vector3 posAnterior;

    [Header("DEBUG")]
    public GameObject go_feedback;

    private void Start()
    {
        transform_ = transform;
        go_feedback.SetActive(false);
    }

    void LateUpdate ()
    {
        //Si hay colisiones, tenemos que movernos para contrarestar el movimiento de la cabeza
		if(ojos.numColisiones > 0)
        {
            Vector3 newPos = posAnterior - cabezaTransoform.localPosition;
            newPos *= 1.5f; //Exageramos al 10% para soltar de la colision
            newPos.y = 0.0f;
            transform_.localPosition = newPos;
            go_feedback.SetActive(true);
            //posAnterior = newPos;
        }
        else
        {
            //Almacenamos datos de posoicoon
            posAnterior = cabezaTransoform.localPosition;
            go_feedback.SetActive(false);
        }
	}
}
