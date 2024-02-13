using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Armas { 
    public class Ar_Congela : Arma
    {
        [Header("Tiempo de uso"), Range(0, 20)]
        public float v_tiempo;
        public override void Fn_Iniciar()
        {
            v_Rango = 12;
            v_Dano = 0;
            v_TimepoRecarga = 6;
            v_tiempo = 45;
            v_PrecioDesbloqueo = 10;
            Fn_SetInit(100, 14, 1, 100);
        }

        public override void Fn_Down()
        {
            //llama la funcion que tiene el calculo de las balas, para saber si recarga o si ya no tienes balas
            if (Fn_PuedeDisparar())
            {
                //if (Fn_Mira())
                //{
                Fn_SetAumento();
                Fn_SetDisparo(true);
                GameObject _bala = Instantiate(v_prefBala, v_SaleBala.position, Quaternion.identity);
                _bala.name = "Congela_" + v_pila;
                _bala.GetComponent<Balas.B_Congela>().Fn_Iniciar(4000.0f, v_tiempo);
                //si hay animator el addforce no funciona, mejor apagar el animator 
                _bala.GetComponent<Balas.B_Congela>().Fn_Disparo(v_SaleBala.position, v_SaleBala.forward);
                v_contador++;
                Fn_Revisa();
                //Valve.VR.InteractionSystem.Player.instance.rightHand.GetComponent<Audio.Au_Manager>().Fn_SetAudio(4, false, true);
                //}
            }
        }
    }
}
