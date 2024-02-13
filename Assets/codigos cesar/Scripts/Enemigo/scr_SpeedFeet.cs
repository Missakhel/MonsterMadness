using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
namespace Enemigos
{
    //Este script calcula la velocidad a la que camina el personaje
    public class scr_SpeedFeet : MonoBehaviour
    {

        [Header("Referencias")]
        [Required]
        public Transform PieDerecho;
        [Required]
        public Transform PieIzquierdo;

        Vector2 PieDerechoLastPosition;
        Vector2 PieIzquierdoLastPosition;

        [System.NonSerialized]
        float speed;

        public float Speed
        {
            get { return speed; }
        }

        private void Start()
        {
            PieDerechoLastPosition = getPositionXZ(transform.InverseTransformPoint(PieDerecho.position));
            PieIzquierdoLastPosition = getPositionXZ(transform.InverseTransformPoint(PieIzquierdo.position));
        }

        void LateUpdate()
        {
            //Calculamos distancia de derecho
            Vector2 pieDerechoPos = getPositionXZ(transform.InverseTransformPoint(PieDerecho.position));
            Vector2 pieIzquierdoos = getPositionXZ(transform.InverseTransformPoint(PieIzquierdo.position));
            float dist = Mathf.Abs(Vector2.Distance(pieDerechoPos, PieDerechoLastPosition));
            speed = dist;
            //Ahora con izquierdo
            dist = Mathf.Abs(Vector2.Distance(pieIzquierdoos, PieIzquierdoLastPosition));
            speed += dist;
            //Actualkizamos posicon anterior
            PieDerechoLastPosition = pieDerechoPos;
            PieIzquierdoLastPosition = pieIzquierdoos;
        }

        Vector2 getPositionXZ(Vector3 _pos)
        {
            return new Vector2(_pos.x, _pos.y);
        }
    }
}