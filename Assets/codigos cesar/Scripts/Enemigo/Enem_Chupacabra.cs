using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Enemigos
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enem_Chupacabra : Enemigo_base
    {

        [Header("BOSS")]
        public GameObject v_acompanante;
        [Header("Acompanante")]
        public GameObject v_padre;
        public float v_ejex;
        /// <summary>
        /// es acompañante?
        /// </summary>
        public bool v_acom = false;

        public float _rango;

        private void Awake()
        {
            Inicializar();
        }
        protected override void Inicializar()
        {
            IsTerrestre = false;
            //Ajustes especiales si es jefe    
            //este jefe USA 2 MANOS PARA PEGAR
            if (IsBoss == true) //NOTA: Se puede omitir
            {
                Almas = 30;
                VidaMaxima = 300;
                Vida = VidaMaxima;
                Velocidad = 0.25f;
                Danio = 10;
                Defensa = 3;
                Fn_SetTiempo(-1, 2);
                // Fn_Crea();
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
            if (!v_acom)
            {
                base.Inicializar();
            }
            else
            {
                IsVivo = true;
                v_NavAgent = GetComponent<NavMeshAgent>();
                v_anim = GetComponentInChildren<Animator>();
                v_rig = GetComponent<Rigidbody>();
                GetComponent<Audio.Au_Manager>().Fn_Inicializa();
                GetComponent<Audio.Au_Manager>().Fn_SetAudio(0, true, true);
                v_efec = GetComponent<Enem_Efectos>();
                v_efec.Fn_Inicializa();
                Fn_SetVida();
            }
        }
        IEnumerator E_LoopAcompa()
        {
            var wait_1Segundo = new WaitForSeconds(1.0f);
            while (IsVivo)
            {
                if (v_padre != null)
                {
                    if (!v_padre.GetComponent<Enem_Chupacabra>().IsVivo)
                    {
                        Dano(100000);
                    }
                    else
                    {
                        v_anim.SetBool("v_Vivo", IsVivo);
                        v_anim.SetBool("v_mov", true);
                        if (v_padre.GetComponent<Enem_Chupacabra>().Fn_GetDist(4.0f))
                        {//irme hacia atras
                         // v_NavAgent.SetDestination( ( -1  *v_padre.transform.forward )  );
                            v_NavAgent.SetDestination((v_padre.transform.position + (-v_padre.transform.forward * 1.4f) + (v_padre.transform.right * v_ejex)));// + (v_padre.transform.right * v_ejex))  ;
                        }
                        else
                        {
                            v_NavAgent.SetDestination((v_padre.transform.position + (v_padre.transform.forward * 1.4f) + (v_padre.transform.right * v_ejex)));// + (v_padre.transform.right * v_ejex))  ;
                                                                                                                                                              // v_NavAgent.SetDestination(  new Vector3( v_padre.transform.position.x + v_ejex, transform.position.y, v_padre.transform.position.z ));
                        }
                    }
                }
                // transform.rotation =Quaternion.Euler( Vector3.zero );
                yield return wait_1Segundo;
            }
        }
        //void OnDrawGizmosSelected()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawLine(transform.position, v_NavAgent.destination);
        //}
        public override void Dano(GameObject _aseguir)
        {
            if (!v_acom)
            {
                base.Dano(_aseguir);
            }
        }
        public void Fn_SetPadre(float _eje)
        {
            v_ejex = _eje;
        }
        public void Fn_SetPadre(GameObject _padre)
        {
            v_padre = _padre;
            _rango = v_padre.GetComponent<Enem_Chupacabra>()._rango;
            Velocidad = 0.8f;
            StartCoroutine(E_LoopAcompa());
        }
        public void Fn_Crea(Vector3 _posi)
        {
            Transform _padre = GameObject.Find("enemigos").transform;


            GameObject _obj = Instantiate(v_acompanante, transform.position, Quaternion.identity);
            Manager_Horda.Instance.Fn_AgregaEnemigos(_obj);
            _obj.transform.SetParent(_padre);
            // _obj.transform.position = transform.position + (transform.forward * 1.4f);
            _obj.SendMessage("Fn_SetPadre", gameObject);
            _obj.SendMessage("Fn_SetPadre", -1.0f);
            _obj = null;


            _obj = Instantiate(v_acompanante, transform.position, Quaternion.identity);
            Manager_Horda.Instance.Fn_AgregaEnemigos(_obj);
            _obj.transform.SetParent(_padre);
            // _obj.transform.position = transform.position + (transform.forward * 1.4f);
            _obj.SendMessage("Fn_SetPadre", gameObject);
            _obj.SendMessage("Fn_SetPadre", 0.01f);
            _obj = null;



            _obj = Instantiate(v_acompanante, transform.position, Quaternion.identity);
            Manager_Horda.Instance.Fn_AgregaEnemigos(_obj);
            _obj.transform.SetParent(_padre);
            //_obj.transform.position = transform.position + (transform.forward * 1.4f);
            _obj.SendMessage("Fn_SetPadre", gameObject);
            _obj.SendMessage("Fn_SetPadre", 1.0f);
        }
        public override void Fn_Saltar(bool _valor) { }
        public override void Fn_Atacar(bool _jugador)
        {
            base.Fn_Atacar(_jugador);
            v_anim.SetBool("v_golpe", true);
            v_anim.SetBool("v_mov", false);
        }
        public override void Fn_Efecto() { }
    }
}