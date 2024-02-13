using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Enemigos
{
    public class Enem_Chaneque : Enemigo_base
    {
        private void Awake()
        {//melee, vida bajaa, alta, bajo, terrestre
            IsMelee = true;
            Vida = 10.0f;
            Velocidad = 20.0f;
            Danio = 1.0f;
            IsTerrestre = true;
            base.Inicializar();
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
