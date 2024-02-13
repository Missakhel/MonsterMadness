using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jugador;
namespace Armas
{
    using Manager;
    public class Ar_Cura : Arma
    {
        Vector3 v_datos;
        public Animator v_anim;
        [Header("tutorial")]
        public bool v_puede=false;
        public GameObject v_objManager;
        public override void Fn_Iniciar()
        {
            v_puede = false;
            v_Rango = 12;
            v_Dano = 10;
            v_TimepoRecarga = 5;
            v_PrecioDesbloqueo = 200;
            v_PrecioBalas = 200;
            Fn_SetInit(100, 100,0, 1);
            v_anim = GetComponentInChildren<Animator>();
        }
        public void Fn_SetPuede()
        {
            v_puede = true;
        }
        public override void Fn_Down()
        {
            //v_datos = Jug_Datos.Instance.Fn_GetDatos();
            
            if(v_datos.y< v_datos.x  && v_pila > 0  && v_puede)
            {
                v_puede = false;
                if (v_objManager != null)
                    v_objManager.GetComponent<Tutorial.Scr_Instru>().Fn_Siguiente(1);
                //Valve.VR.InteractionSystem.Player.instance.rightHand.GetComponent<Audio.Au_Manager>().Fn_SetAudio(6, false, true);
                v_anim.SetTrigger("play");
                Jug_Datos.Instance.Fn_Curar(20.0f);
                Fn_SetAumento();
                Fn_SetDisparo(false);
                StartCoroutine(Ie_Delay());
            }
            else
            {
                //Valve.VR.InteractionSystem.Player.instance.GetComponent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
            }
        }
        IEnumerator Ie_Delay()
        {
            yield return new WaitForSeconds(1.5f);
            GetComponentInParent<Ar_Manager>().Fn_Eliminar(GetComponent<Arma>().GetType());
            GetComponentInParent<Jug_Arma>().Fn_ActualizaManager();
        }
    }
}