using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Enemigos
{
    public class Enem_alebrije : Enemigo_base {
        private void Awake()
        {//vida alta
            Vida = 10.0f;
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
