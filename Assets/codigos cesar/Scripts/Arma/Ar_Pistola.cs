using UnityEngine;
namespace Armas
{
    using Jugador;
    using Armas.Balas;
    public class Ar_Pistola : Arma
    {
        [Header("tutorial")]
        public bool v_puede;
        public override void Fn_Iniciar()
        {
            v_puede = true;
            v_Rango = 12;
            v_Dano = 15;
            v_TimepoRecarga = 5;
            v_PrecioDesbloqueo =0;
            v_PrecioBalas = 200;
            Fn_SetInit(100, 10, 2,100);
        }
        public void Fn_SetPuedes(bool _val)
        {
            v_puede = _val;
        }
        public void Fn_SetPuede()
        {
            v_puede = false;
        }
        public override void Fn_Pool()
        {
            v_idPool = 0;
            GameObject _inst;
            GameObject _padrepool = GameObject.Find("PoolManager") ;
            for (int i = 0; i <v_MaxPool; i++)
            {
                _inst = Instantiate(v_prefBala, new Vector3(0, 100, 0), Quaternion.identity);
                _inst.transform.SetParent(_padrepool.transform);
                v_pool.Add(_inst);
                _inst.GetComponent<Bala>().Fn_Iniciar(v_Dano, v_Rango, 4000.0f, Jug_Datos.Instance.gameObject);
            }
        }
        public override void Fn_Down()
        {
            //llama la funcion que tiene el calculo de las balas, para saber si recarga o si ya no tienes balas
            if (Fn_PuedeDisparar() )
            {
                if(v_puede)
                {

                //if (Fn_Mira())
                //{
                //Valve.VR.InteractionSystem.Player.instance.rightHand. GetComponent<Audio.Au_Manager>().Fn_SetAudio(0, false, true);
                Fn_SetAumento();
                    Fn_SetDisparo(true);
                    v_pool[v_idPool].GetComponent<Bala>().Fn_Disparo(v_SaleBala.position, v_SaleBala.forward);
                    v_idPool++;
                    v_contador++;
                    Fn_Revisa();
                //}
                }
                else
                {
                    //Valve.VR.InteractionSystem. Player.instance.GetComponent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
                }
            }
        }
        /*
        //public GameObject BalaPref;
        //rango medio, dano bajo,recarga rapida,preciode balas bajo
        void Awake()
        {
            v_Rango = 12;
            v_Dano = 10;
            v_TimepoRecarga = 2;
            v_PrecioDesbloqueo = 10;
            v_MaxCargador = 20;
            v_MaxBalas = 60;
            v_BalasCargador = v_MaxCargador;
            v_BalasActuales = v_MaxBalas - v_BalasCargador;
        }
        public override void Fn_Pool()
        {
            v_idPool = 0;
            GameObject _inst;
            for (int i = 0; i < v_MaxCargador; i++)
            {
                _inst = Instantiate(v_prefBala, new Vector3(0, 100, 0), Quaternion.identity, gameObject.transform);
                v_pool.Add(_inst);
                _inst.GetComponent<Bala>().Fn_Iniciar(v_Dano, v_Rango, 4000.0f,Jug_Datos.Instance.gameObject);
            }
        }
        public override void Fn_Down()
        {
            //llama la funcion que tiene el calculo de las balas, para saber si recarga o si ya no tienes balas
            if (Fn_PuedeDisparar())
            {
                if (Fn_Mira())
                {
                    base.Fn_Down(); // ?
                    v_pool[v_idPool].GetComponent<Bala>().Fn_Disparo(v_SaleBala.position, v_SaleBala.forward);
                    v_idPool++; //El reseteo ocurre al recargar
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
                    else
                    {
                        //print("con  balas despues del disparo no recarga");
                    }
                }
            }
        }
        */
    }
}