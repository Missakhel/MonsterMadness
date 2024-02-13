using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Armas
{
    public class Ar_Sticky : Arma
    {
        [Header("Tiempo de uso"), Range(0, 20)]
        public float v_tiempo;
        public override void Fn_Iniciar()
        {
            v_Rango = 12;
            v_Dano = 0;
            v_TimepoRecarga = 6;
            v_PrecioDesbloqueo = 10;
            v_tiempo = 10;
            Fn_SetInit(100, 14, 1, 100);            
        }
        //public override void Fn_Pool()
        //{
        //    v_idPool = 0;
        //    GameObject _inst;
        //    for (int i = 0; i < v_MaxPool; i++)
        //    {
        //        _inst = Instantiate(v_prefBala, new Vector3(0, 100, 0), Quaternion.identity, gameObject.transform);
        //        v_pool.Add(_inst);
        //        //el ultimo valor es el tiempo en segundos que va a estar activo el efecto
        //        //_inst.GetComponent<Balas.B_Sticky>().Fn_Iniciar(v_Rango, 4000.0f,Jugador.Jug_Datos.Instance.gameObject, v_tiempo);
        //        _inst.GetComponent<Balas.B_Sticky>().Fn_Iniciar(4000.0f, v_tiempo);
        //    }
        //}
        public override void Fn_Down()
        {
            //llama la funcion que tiene el calculo de las balas, para saber si recarga o si ya no tienes balas
            if (Fn_PuedeDisparar())
            {
                //if (Fn_Mira())
                //{
                //Valve.VR.InteractionSystem.Player.instance.rightHand.GetComponent<Audio.Au_Manager>().Fn_SetAudio(3, false, true);
                Fn_SetAumento();
                Fn_SetDisparo(true);
                GameObject _bala= Instantiate(v_prefBala, v_SaleBala.position, Quaternion.identity);
                _bala.name = "StickyDa_" + v_pila;
                 //Debug.Break();
                _bala.GetComponent<Balas.B_Sticky>().Fn_Iniciar(4000.0f, v_tiempo);
                //si hay animator el addforce no funciona, mejor apagar el animator 
                _bala.GetComponent<Balas.B_Sticky>().Fn_Disparo(v_SaleBala.position, v_SaleBala.forward);
               // v_idPool++;
                v_contador++;
                Fn_Revisa();
                //}
            }
        }
        /*
        [Header("Tiempo de uso"), Range(0,20)]
        public float v_tiempo;
        private void Awake()
        {
            v_Rango = 12;
            v_Dano = 0;
            v_TimepoRecarga = 2;
            v_PrecioDesbloqueo = 10;
            v_MaxCargador = 20;
            v_MaxBalas = 100;
            v_BalasCargador = v_MaxCargador;
            v_BalasActuales = v_MaxBalas - v_BalasCargador;
            v_idPool = 0;
        }
        public override void Fn_Pool()
        {
            v_idPool = 0;
            GameObject _inst;
            for (int i = 0; i < v_MaxCargador; i++)
            {
                _inst = Instantiate(v_prefBala, new Vector3(0, 100, 0), Quaternion.identity,gameObject.transform);
                v_pool.Add(_inst);
                //el ultimo valor es el tiempo en segundos que va a estar activo el efecto
                _inst.GetComponent<B_Sticky>().Fn_Iniciar( v_Rango, 4000.0f, Jug_Datos.Instance.gameObject,v_tiempo);
            }
        }
        public override void Fn_Down()
        {
            //llama la funcion que tiene el calculo de las balas, para saber si recarga o si ya no tienes balas
            if (Fn_PuedeDisparar())
            {
                if (Fn_Mira())
                {
                    base.Fn_Down();

                    v_pool[v_idPool].GetComponent<B_Sticky>().Fn_Disparo(v_SaleBala.position, v_SaleBala.forward);
                    v_idPool++;

                    /////////////////////////77
                    //aca puedes hacer tu forma de disparo como tu quieras
                    //////////////////////////////////
                    v_BalasCargador -= 1;
                    //por si me quedo sin balas despues del disparo
                    if (v_BalasCargador == 0 && v_BalasActuales > 0)
                    {
                        v_puededisparar = false;
                        Fn_Recargar();
                    }
                }
            }//puede disparar
        }
        */
    }
}
