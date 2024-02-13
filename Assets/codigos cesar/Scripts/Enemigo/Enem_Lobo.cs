using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Enemigos
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enem_Lobo : Enemigo_base
    {
        private void Awake()
        {//melee, vida menor, alta, media, terrestre
            IsMelee = true;
            Vida = 20.0f;
            Velocidad = 20.0f;
            Danio = 2.0f;
            IsTerrestre = true;
            base.Inicializar();
        }
        public override void Fn_Atacar(bool _jugador)
        {
            base.Fn_Atacar(_jugador);
           
        }
    }
}