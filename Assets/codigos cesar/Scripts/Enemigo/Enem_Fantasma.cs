using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Ventanas;

namespace Enemigos
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enem_Fantasma : Enemigo_base {
        private void Awake()
        {//rango, vida media, media, media, aereo
          
            Inicializar();
        }
        protected override void Inicializar()
        {
            //Ajustes especiales si es jefe    
            //este jefe USA 2 MANOS PARA PEGAR
            if (IsBoss == true) //NOTA: Se puede omitir
            {
                Almas = 30;
                VidaMaxima = 300;
                Vida = VidaMaxima;
                Velocidad = 0.25f;
                Danio =20;
                Defensa = 8;
                Fn_SetTiempo(20,25);
            }
            else//este jefe USA 1 MANO PARA PEGAR
            {
                Almas = 30;
                VidaMaxima = 100;
                Vida = VidaMaxima;
                Velocidad = 0.3f;
                Danio = 6;
                Defensa = 3;
            }
            base.Inicializar();
        }
        public override void Fn_Atacar(bool _jugador)
        {
            base.Fn_Atacar(_jugador);
            v_anim.SetBool("v_golpe", true);
            v_anim.SetBool("v_mov", false);
        }
        public override void Fn_Saltar(bool _valor){}
        //https://docs.unity3d.com/ScriptReference/Object.FindObjectsOfType.html
        //https://unity3d.com/es/learn/tutorials/topics/scripting/loops
        //System.Type es diferente a Types
        public override void Fn_Efecto()
        {
            if (!v_anim.GetBool("v_golpe"))
            {
                if (goAtacar.GetComponent<Ventana>())
                {
                    Vector2 _vec = new Vector2(transform.position.x, transform.position.z);
                    Vector2 _vec2 = new Vector2(goAtacar.transform.position.x, goAtacar.transform.position.z);
                    float DistanciaObjectivo = Vector2.Distance(_vec, _vec2);
                    if (Manager.Manager_Ventanas.Instance.Fn_GetNoRotas())//faltan por romper
                    {
                        //Debug.LogError("mueve a otra ventana NOO ROTA");
                        C_Ventana _info = Manager.Manager_Ventanas.Instance.Fn_GetNoRotas(true);
                        //Debug.LogError(gameObject, gameObject);
                        //Debug.LogError(goAtacar,goAtacar);
                        goAtacar = _info.v_ven[0];

                        //Debug.LogError(" Distancia"+ DistanciaObjectivo);

                        //Debug.LogError(transform.position);
                        //Debug.LogError("pos"+ _info.v_ven[0].transform.position + "forward "+(_info.v_ven[0].GetComponent<Ventana>().v_frente.forward * DistanciaObjectivo));


                        v_NavAgent.enabled = false;
                        Vector3 _as = _info.v_ven[0].transform.position + (_info.v_ven[0].GetComponent<Ventana>().v_frente.forward * DistanciaObjectivo);
                        transform.position = new Vector3(_as.x, transform.position.y, _as.z);
                        v_NavAgent.enabled = true;


                        //transform.position= Vector3.MoveTowards(_info.v_ven[0].GetComponent<Ventana>().v_frente.position, _info.v_pos[0].position, DistanciaObjectivo);

                        //transform.position = _info.v_pos[0].position;
                        //Debug.LogError("efecto se mueve a " + transform.position, gameObject);
                        //Debug.Break();
                        Fn_Detener();
                        v_NavAgent.ResetPath();
                        v_NavAgent.path.ClearCorners();
                        v_IdPos = goAtacar.GetComponent<Ventana>().Fn_GetPosRand();
                        v_Destino = goAtacar.GetComponent<Ventana>().Fn_GetPosRand(v_IdPos);
                        Fn_SetDestination();
                        //Debug.Break();
                    }
                    else
                    {
                        Fn_Detener();
                        AttackJugador = true;
                        //goAtacar = Jugador.Jug_Datos.Instance.Fn_GetPosicion();
                        v_NavAgent.ResetPath();
                        v_NavAgent.path.ClearCorners();
                        v_Destino = goAtacar.transform.position;
                        Fn_SetDestination();
                    }
                    //Vector3 _posnueva =Manager.Manager_Ventanas.Instance.Fn_GetPosRandom();//una nueva
                    //transform.position = _posnueva;
                    //Debug.LogError("se mueve fantasma "+ transform.position);
                    //ActualizarDestino(DistanciaObjectivo);///este es el que puede mandarle que siga al jugador
                }
            }
            else { }//Debug.LogError("YA ATACANDO NO MUEVE"); }
        }
        protected override void AtacarObjMagico()
        {
            //base.AtacarObjMagico();
            if (IsBoss)
            {
                if (Manager.Manager_Ventanas.Instance.Fn_GetNoRotas())//faltan por romper
                {
                    // Debug.LogError("mueve a otra ventana NOO ROTA", gameObject);
                    C_Ventana _info = Manager.Manager_Ventanas.Instance.Fn_GetNoRotas(true);
                    goAtacar = _info.v_ven[0];
                    v_NavAgent.enabled = false;
                    transform.position = _info.v_pos[0].position;
                    v_NavAgent.enabled = true;
                    //Debug.LogError("objeto magico se mueve a "+ transform.position);
                    Fn_Detener();
                    v_NavAgent.ResetPath();
                    v_NavAgent.path.ClearCorners();
                    v_IdPos = goAtacar.GetComponent<Ventana>().Fn_GetPosRand();
                    v_Destino = goAtacar.GetComponent<Ventana>().Fn_GetPosRand(v_IdPos);
                    Fn_SetDestination();

                }
                else { base.AtacarObjMagico(); }
            }
            else { base.AtacarObjMagico(); }
        }
    }
}
