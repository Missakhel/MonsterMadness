using UnityEngine;
namespace Armas
{
    using Jugador;
    using Armas.Balas;
    public class Ar_Sniper : Arma
    {
        public override void Fn_Iniciar()
        {
            v_Rango = 200;
            v_Dano = 150;
            v_TimepoRecarga = 5;
            v_PrecioDesbloqueo = 400;
            v_PrecioBalas = 300;
            Fn_SetInit(100, 60, 5,5);
        }
        public override void Fn_Pool()
        {
            v_idPool = 0;
            GameObject _inst;
            GameObject _padrepool = GameObject.Find("PoolManager");
            for (int i = 0; i < v_MaxPool; i++)
            {
                _inst = Instantiate(v_prefBala, new Vector3(0, 100, 0), Quaternion.identity);
                _inst.transform.SetParent(_padrepool.transform);
                v_pool.Add(_inst);
                _inst.GetComponent<Bala>().Fn_Iniciar(v_Dano, v_Rango, 8000.0f, Jug_Datos.Instance.gameObject);
            }
        }
        public override void Fn_Down()
        {
            //llama la funcion que tiene el calculo de las balas, para saber si recarga o si ya no tienes balas
            if (Fn_PuedeDisparar())
            {
                //if (Fn_Mira())
                //{
                //Valve.VR.InteractionSystem.Player.instance.rightHand.GetComponent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
                Fn_SetAumento();
                Fn_SetDisparo(true);
                v_pool[v_idPool].GetComponent<Bala>().Fn_Disparo(v_SaleBala.position + new Vector3(0, 0.15f, 0), v_SaleBala.forward);
                v_idPool++;
                v_contador++;
                /////////////////////////77
                //aca puedes hacer tu forma de disparo como tu quieras
                //////////////////////////////////
                ///
                Fn_Revisa();
                //}
            }
        }
        /*
       // public GameObject BalaPref;
        //https://docs.unity3d.com/ScriptReference/Camera.Render.html
        //rango alto, dano alto,recarga lenta,preciode balas alto
        void Awake()
        {
            v_PrecioDesbloqueo = 15;
            v_Rango = 200;
            v_Dano = 150;
            v_TimepoRecarga = 5;
            v_PrecioDesbloqueo = 100;


            v_MaxCargador = 7;
            v_MaxBalas = 240;
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
                _inst.GetComponent<Bala>().Fn_Iniciar(v_Dano, v_Rango, 8000.0f,Jug_Datos.Instance.gameObject);
            }
        }
        public override void Fn_Down()
        {
            if (Fn_PuedeDisparar())
            {
                if (Fn_Mira())
                {
                    base.Fn_Down();//llama la funcion que tiene el calculo de las balas, para saber si recarga o si ya no tienes balas
                    /////////////////////////77
                    //aca puedes hacer tu forma de disparo como tu quieras
                    //////////////////////////////////
                    v_pool[v_idPool].GetComponent<Bala>().Fn_Disparo(v_SaleBala.position +new Vector3(0,0.15f,0), v_SaleBala.forward);
                    //GameObject _linea = Instantiate(Resources.Load("LineaSniper") as GameObject, v_pool[v_idPool].transform);
                    //Destroy(_linea, 10.0f);
                    v_idPool++;                    
                    /////////////////////////77
                    //aca puedes hacer tu forma de disparo como tu quieras
                    //////////////////////////////////
                    v_BalasCargador -= 1;
                    //por si me quedo sin balas despues del disparo
                    if (v_BalasCargador == 0 && v_BalasActuales > 0)
                    {
                        //print("sin balas despues del disparo");
                        //print("cargador " + v_BalasCargador + " actuales " + v_BalasActuales);
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