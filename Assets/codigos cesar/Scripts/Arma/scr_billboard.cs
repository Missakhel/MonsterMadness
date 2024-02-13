using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_billboard : MonoBehaviour {

    Transform camTransform;

	// Use this for initialization
	void Start () {
        camTransform = Camera.main.transform;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.LookAt(camTransform.position);
        transform.Rotate(Vector3.up * 180f);
	}
}
