using UnityEngine;
namespace Armas
{
    using Armas.Balas;
    using Jugador;
    public class Ar_Escopeta : Arma
    {
        public B_Esco v_parti;
        //rango corto, dano alto,recarga rapida,preciode balas bajo
        public override void Fn_Iniciar()
        {
            v_Rango = 2.0f;
            v_Dano = 40;
            v_TimepoRecarga = 5;
            v_PrecioBalas = 3;
            v_idPool = 0;
            if (v_parti == null)
            {
                v_parti = GetComponentInChildren<B_Esco>();
                if (v_parti != null)
                {
                    v_parti.Fn_Iniciar(v_Dano, v_Rango, Jug_Datos.Instance.gameObject);
                }
            }
            Fn_SetInit(100, 12, 4, 40);
        }
        public override void Fn_Down()
        {
            if(v_parti!= null)
            {
                if (!v_parti.Fn_GetDisparo()&& Fn_PuedeDisparar())
                {
                    //if (Fn_Mira())
                    //{
                    //Valve.VR.InteractionSystem.Player.instance.rightHand.GetComponent<Audio.Au_Manager>().Fn_SetAudio(2, false, true);
                    Fn_SetAumento();
                    v_parti.Fn_Disparo();
                    Fn_SetDisparo(true);
                    v_idPool += 2;
                    v_contador++;
                    Fn_Revisa();
                    //}
                }
            }
            else
            {
                v_parti = GetComponentInChildren<B_Esco>();
                if (v_parti != null)
                {
                    v_parti.Fn_Iniciar(v_Dano, v_Rango, Jug_Datos.Instance.gameObject);
                }
            }
        }
        public override void Fn_Desactivar()
        {
            v_parti.StopAllCoroutines();
            base.Fn_Desactivar();
        }
        /*
        public B_Esco v_parti;
        //rango corto, dano alto,recarga rapida,preciode balas bajo
        void Awake()
        {
            v_Rango = 15;
            v_Dano = 70;
            v_TimepoRecarga = 5;
            v_PrecioBalas = 3;

            v_MaxCargador = 15;
            v_MaxBalas = 45;

            v_PrecioDesbloqueo = 40;
            v_BalasCargador = v_MaxCargador;
            v_BalasActuales = v_MaxBalas - v_BalasCargador;
            v_idPool = 0;
            if (v_parti == null)
            {
                v_parti = GetComponentInChildren<B_Esco>();
                if(v_parti!=null)
                {
                   v_parti.Fn_Iniciar(v_Dano, v_Rango, Jug_Datos.Instance.gameObject);
                }
            }
        }
        public override void Fn_Down()
        {
            if (Fn_PuedeDisparar())
            {
                //base.Fn_Down();//llama la funcion que tiene el calculo de las balas, para saber si recarga o si ya no tienes balas
                if (Fn_Mira())
                {
                    v_parti.Fn_Disparo();
                    v_BalasCargador -= 5;
                    v_idPool += 5;
                    //por si me quedo sin balas despues del disparo
                    if (v_BalasCargador == 0 && v_BalasActuales > 0)
                    {
                        v_puededisparar = false;
                        Fn_Recargar();
                    }
                }
            }
        }
        */
    }
    
}