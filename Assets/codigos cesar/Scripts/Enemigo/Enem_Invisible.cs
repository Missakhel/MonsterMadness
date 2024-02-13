using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Enemigos
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enem_Invisible : Enemigo_base
    {

        private void Awake()
        {//melee, vida meda, lento, media, terrestre
            IsMelee = true;
            Vida = 50.0f;
            Velocidad = 5.0f;
            Danio = 2.0f;
            IsTerrestre = true;
            //base.Fn_Iniciar();
            gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        public override void Fn_Atacar(bool _jugador)
        {
            base.Fn_Atacar(_jugador);
            ///si es true le hace daño directo al jugador sino a la ventana
            if (_jugador == true)
            {
            }
            else
            {
            }
        }
    }
}