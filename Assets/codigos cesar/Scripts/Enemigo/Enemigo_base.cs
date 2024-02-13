using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;
using Items;
namespace Enemigos
{
    using Ventanas;
    using Manager;
    using Jugador;
    [SelectionBase]//[RequireComponent(typeof(scr_SpeedFeet),typeof(MapMarker))]/*v_mov   v_golpe   v_Vivo  v_sal   v_especial*/
    public class Enemigo_base : MonoBehaviour
    {
        #region VARIABLES
        #region  CONFIGURACION DE DAÑOS Y VIDA
        [Header("Tipo enemigo")]
        /// <summary>
        /// true es terrestre, false es aereo
        /// </summary>
        [Tooltip("true es terrestre, false es aereo")]
        public bool IsTerrestre = true;
        /// <summary>
        /// true es ataque melee (cercania), false es de rango
        /// </summary>
        [Tooltip("true es ataque melee (cercania), false es de rango")]
        public bool IsMelee = true;
        /// <summary>
        /// RANGO DE LA CLASE DE HERENCIA
        /// </summary>
        [Tooltip("los de melee es de 1")] //En codigoo es 0.2...
        [EnableIf("IsMelee_I")]
        public float Rango = 10;
        public bool IsMelee_I()
        {
            return !IsMelee;
        } //Solo ayuda a permitir editar Rangoo
        [Header("Propiedades")]
        public float VidaMaxima = 100;
        public float Vida = 100;
        public float Defensa = 2.0f;
        public float Danio = 10.0f;
        public bool IsVivo = true;
        /// <summary>
        /// la cantidad de balas que le das al jugador cuando mueres
        /// </summary>
        public int Almas;
        [Header("NavMesh")]
        public float Velocidad = 10.0f;
        public NavMeshPath v_navpath;
        public Vector3 v_Destino = new Vector3(-999, -999, 999);
        public int v_IdPos = 0;
        public Vector3[] path;
        //[ReadOnly]
        public GameObject goAtacar = null;
        /// <summary>
        /// cuando ya se rompe la ventana que tengo como mira, se vuelve true y busca al jugador
        /// </summary>
        public bool AttackJugador = false;
        scr_SpeedFeet velocidadPies;
       
        public UnityEngine.UI.Image v_vidaImg;
        #endregion
        #region COSAS DEL MODO BOSS
        [Header("Boss")]
        public bool IsBoss = false;
        /// <summary>
        /// tiempo para el cooldown del ataque de jefe
        /// </summary>
        public float BossColdown = 10;
        private bool BossIsSpecial = false; //NOTA: parece que es  IsBoss == BossIsSpecial ?
        #endregion
        #region Referencias
        protected Rigidbody v_rig; //
        protected NavMeshAgent v_NavAgent;//
        protected Animator v_anim;//
        protected Enem_Efectos v_efec;
        WaitForSeconds v_await = new WaitForSeconds(0.3f);
        #endregion
        #region AnimParametrosID
        static readonly int AID_v_Sal = Animator.StringToHash("v_sal");
        #endregion
        #endregion
        protected virtual void Inicializar()
        {//Obtenemos refernecis
            v_await = new WaitForSeconds(0.3f);
            v_rig = GetComponent<Rigidbody>();
            v_NavAgent = GetComponent<NavMeshAgent>();
            v_anim = GetComponentInChildren<Animator>();
            if (GetComponent<scr_SpeedFeet>())
                velocidadPies = GetComponent<scr_SpeedFeet>();


            GetComponent<Audio.Au_Manager>().Fn_Inicializa();
            GetComponent<Audio.Au_Manager>().Fn_SetAudio(0, true, true);
            v_efec = GetComponent<Enem_Efectos>();
            v_efec.Fn_Inicializa();

            //Iniciar de variables-
            if (IsMelee)
            {
                Rango = 1.0f;
            }//Prepramos mesh de navegación
            v_NavAgent.ResetPath();
            v_NavAgent.path.ClearCorners();
            v_Destino = new Vector3(-999, -999, 999);
            v_NavAgent.speed = Velocidad;
            v_NavAgent.stoppingDistance = 0.36f;
            v_NavAgent.updateRotation = false;
            v_NavAgent.acceleration = 10f;
            Fn_SetPos(); // ?
                         //Preparamoso lo demas
            int _tam = GetComponentsInChildren<Hit_Mano>().Length;
            for (int i = 0; i < _tam; i++)
            {
                GetComponentsInChildren<Hit_Mano>()[i].FnSetDano((Danio / _tam), gameObject);
            }
            //Preparamos cotontinas
            if (IsBoss == false)
            {
                StartCoroutine(E_Loop());
            }
            else //jefe
            {
                StartCoroutine(E_Cooldown());
                StartCoroutine(E_Loop());
            }

         
            Fn_SetVida();
        }
        protected void LateUpdate() //Rotamos manualmente
        {
            if (IsVivo && goAtacar != null)
            {
                //Rootamos hacia gameobject ah atacaqr
                Vector3 v_Dir = v_NavAgent.steeringTarget - transform.position;
                v_Dir.y = 0f;
                if (v_Dir != Vector3.zero)
                {
                    Quaternion _qua = Quaternion.LookRotation(v_Dir);
                    _qua.x = 0f;
                    _qua.z = 0f;
                    //transform.rotation = Quaternion.Slerp(transform.rotation, _qua, 1.0f * Time.deltaTime);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, _qua, 25.0f * Time.deltaTime);
                }
                float angle = Mathf.Abs(Vector3.SignedAngle(v_Dir, transform.forward, Vector3.up));
                //Solo permitimoso avanzar si estamos entre 30° a dirrecion
                if (angle > 30.0f)
                {
                    //Evitamos que púeda avanzar
                    v_NavAgent.speed = 0.0f;
                    return;
                }
            }
            if (velocidadPies != null)///esto es por el fantasma que no tiene pies
            {
                if (v_anim.GetBool(AID_v_Sal) == false) //Solo actualizamos si NO estamos saltando
                    v_NavAgent.speed = velocidadPies.Speed * Velocidad;
            }
            else//le damos la velocidad original
            {
                v_NavAgent.speed = Velocidad;
            }
        }
        #region IA
        IEnumerator E_Loop()
        {
            var wait_1Segundo = new WaitForSeconds(1.0f);
            while (IsVivo)
            {
                v_anim.SetBool("v_Vivo", IsVivo);
                if (goAtacar == null)//No tenemos que atacar?
                {//Buscamos nuevo objectivo
                    ActualizarDestino(-2.0f);
                    Fn_SetDestination();
                }
                else //Si tenemos algo que atacar
                {
                    if (AttackJugador) //Actualizamos posicion de jugador, ya que puede que se mueva
                    {
                        v_NavAgent.SetDestination(v_Destino);
                        path = v_NavAgent.path.corners; //Debugeo
                    }
                    if (!BossIsSpecial) //No es boss especial
                    {
                        ////Estamos rango de algo?
                        if (Fn_GetDist(1.0f))
                        {
                            //De torreta?
                            if (goAtacar.GetComponent<Item_Torreta>())
                            {
                                // print("Cercas de torreta");
                                if (!goAtacar.GetComponent<Item_Torreta>().Fn_GetVivo()) //Ya no esta vivo?
                                {
                                    //Buscamoso nuevo objectivo para caminar a el
                                    ActualizarDestino(-2.0f);
                                    Fn_SetDestination();
                                }
                                else //Si esta vivoo y cercas, entonces dejamos de caminar y atacamos
                                {
                                    Fn_Atacar(true);
                                }
                            }
                            //Es una ventana?
                            else if (goAtacar.GetComponent<Ventana>())
                            {
                                if (goAtacar.GetComponent<Ventana>().Fn_GRota()) //Si ya esta rota, buscamos jugador
                                {
                                    //Debug.LogError("Atacar objeto desde ventana");
                                    //AtacarJug();
                                    //Fn_SetDestination();
                                    AtacarObjMagico();
                                    //Fn_SetDestination();
                                }
                                else //No esta rota, atacamos
                                {
                                    // Debug.LogError("cerca ventana");
                                    Fn_Atacar(true);
                                }
                            }
                            //es el jugador
                            else if (AttackJugador)
                            {
                                Fn_Atacar(true);
                            }
                            //Es un obstaculo
                            else if (goAtacar.GetComponent<Item_Obstaculo>())
                            {
                                // print("Cercas de Item");
                                if (goAtacar.GetComponent<Item_Obstaculo>().Fn_GetVivo()) //Sigue vivo?
                                {
                                    Fn_Atacar(true);
                                }
                                else //No lo esta, buscamos nuevo objectivo
                                {
                                    ActualizarDestino(-2.0f);
                                    Fn_SetDestination();
                                }
                            }
                            //Es cosa magica?
                            else if (goAtacar.GetComponent<Item_Magico>())
                            {
                                if (goAtacar.GetComponent<Item_Magico>().Fn_GetVivo()) //Aun esta vivo?
                                {
                                    Fn_Detener(true);
                                    Fn_Atacar(true); //atacamoso
                                }
                                else //Busca,os nuevo onjectivo
                                {
                                    ActualizarDestino(-2.0f);
                                    Fn_SetDestination();
                                }
                            }
                            else//NO HAY NADA CERCA SEGUIR MOVIENDOTE
                            {
                                // print("Seguimos avanzando");
                                v_Destino = goAtacar.transform.position;
                                Fn_Detener(false);
                                Fn_SetDestination();
                            }
                        }//if rango
                        else
                        {
                            //Vector2 _vec = new Vector2(transform.position.x, transform.position.z);
                            //Vector2 _vec2 = new Vector2(goAtacar.transform.position.x, goAtacar.transform.position.z);
                            //float DistanciaObjectivo = Vector2.Distance(_vec, _vec2);
                            v_anim.SetBool("v_golpe", false);
                            if (goAtacar.GetComponent<Ventana>())
                            {
                                if (goAtacar.GetComponent<Ventana>().Fn_GRota())
                                {
                                    AtacarObjMagico();
                                }
                                else
                                {
                                   // v_anim.SetBool("v_mov", true);
                                    v_Destino = goAtacar.GetComponent<Ventana>().Fn_GetPosRand(v_IdPos);//ir a una de las posiciones que tiene cada entana
                                }
                            }
                            else
                            {
                                //v_anim.SetBool("v_mov", true);
                                v_IdPos = 0;
                                v_Destino = goAtacar.transform.position;
                            }
                        }
                    }// if (!BossIsSpecial) 
                }
                yield return wait_1Segundo;
            }
            print("Muerto");
            Fn_Muerto();
        }
        #endregion
        //Llammada por ventan.cs
        /// <summary>
        /// CUANDO SE ROMPE LA VENTANA ACOMODAR LA ANIMACION DE ENTRAR
        /// </summary>
        public virtual void Fn_Saltar(bool _valor)
        {
            //Fn_Detener();
            /*
             * if (_valor == true)
            {
                if (IsVivo)
                {
                    //v_Velocidad *= 0.5f;
                    // print("salto valor true");
                    v_NavAgent.speed = 1.0f;
                    v_anim.SetBool("v_mov", false);
                    v_anim.SetBool(AID_v_Sal, true);
                    v_Destino = transform.position + transform.forward * 1.65f;
                    v_NavAgent.SetDestination(v_Destino);
                    path = v_NavAgent.path.corners; //Debugeo
                }
            }
            else
            {
                //v_Velocidad *= 2.0f;
                //print("llegue");
                v_NavAgent.speed = Velocidad;
                v_anim.SetBool("v_mov", true);
                v_anim.SetBool(AID_v_Sal, false);
                //v_NavAgent.autoTraverseOffMeshLink = true;
            }
            */
        }
        //si se mueres aca hace las animaciones
        public virtual void Dano(float _dano)
        {
            if (IsVivo == false) //Si ya esta muerto, noo hacemos nada
                return;
            float resta = _dano - Defensa; //cuanto daño sufrira

            //if (v_efec.Fn_GetMat())
                v_efec.Fn_Dano();
                
            if (resta <= 0)
            {
                return; //Nda,, no hacemos nada
            }
            else
            {
                Vida -= resta;
                Vida = Mathf.Clamp(Vida, 0f, VidaMaxima);
                Vida = (float)System.Math.Round(Vida, 2);
                Fn_SetVida();
                if (Vida <= 0f)
                {
                    Fn_SetVida();
                    Fn_Muerto();  //Ya murio
                }
            }
        }
        public virtual void Dano(GameObject _aseguir)
        {
            if (IsVivo && gameObject.activeInHierarchy)
            {
                //Revisamos si es el jugador?
                if (_aseguir == Jug_Datos.Instance.gameObject)
                {
                    if(Manager_Ventanas.Instance.Fn_GetRotas() )
                    {
                        AttackJugador = true;
                        AtacarJug();
                    }
                    //else { Debug.LogError("NO HAY ROTAS IGNORA AL JUGADOR"); }
                }
                else//if (_aseguir != Jug_Datos.Instance.gameObject)
                {
                    goAtacar = _aseguir; //Actualizamos a quien tiene que atacar
                    v_Destino = _aseguir.transform.position; //Actualizamos posicion a ir
                    Fn_SetDestination();
                }
            }
        }
        protected virtual void Fn_Muerto()
        {
            /*if (Jug_Datos.Instance != null) //Añadimos almas
                Jug_Datos.Instance.Fn_AddAlmas(Almas);*/

            GetComponent<Audio.Au_Manager>().Fn_SetAudio(1, false, true);
            Vida = 0f;
            //Apagamos golpes
            Hit_Mano[] manos = GetComponentsInChildren<Hit_Mano>();
            for (int i = 0; i < manos.Length; i++)
            {
                manos[i].Fn_SetGolpe(false);
            }
            //Ajustes a animacion
            IsVivo = false;
            v_anim.SetBool("v_Vivo", IsVivo);
            v_anim.SetBool("v_mov", false);
            v_anim.SetBool("v_golpe", false);
            //Detenemos cosas
            StopAllCoroutines();
            if (gameObject.activeInHierarchy)
            {
                v_rig.useGravity = false;
                v_NavAgent.enabled = false;
                v_anim.applyRootMotion = true;//para que la animacion vaya al piso y no flotando
                gameObject.layer = k.Layers.MUERTO;
            }
        }
        /// <summary>
        /// Calcula cual es el nuevo objectivo a caminar hacia
        /// </summary>
        protected void ActualizarDestino(float _nuevadist)
        {//Preparamos variables
            GameObject[] _ventanas = Manager_Ventanas.Instance.Fn_GetVentanas(); //Obtenemos ventanas
            //float distancia = float.MaxValue;
            //float temp = 0;
            //int indice = -1;
            //Vector2 _vec2 = Vector2.zero;
            //Vector2 _vec = new Vector2(transform.position.x, transform.position.z);
            //if (_ventanas.Length > 0) //Hay ventanas?                                                                   
            if (Manager_Ventanas.Instance.Fn_GetVentanas().Length > 0) //Hay ventanas?
            {
                //Vector3 posCercana = Vector3.zero;
                //Busamos la ventana más cercas
                //for (int i = 0; i < _ventanas.Length; i++)
                //{
                //    //para que no cuenta como distancia lo del eje de hacia aarriba segun su pivote,solo cuenta xz
                //    _vec2.Set(_ventanas[i].transform.position.x, _ventanas[i].transform.position.z);
                //    //Obtenemoso el más cercano
                //    temp = Vector2.Distance(_vec, _vec2);
                //    if (temp > 0 && temp < distancia)
                //    {
                //        posCercana = _ventanas[i].transform.position;
                //        distancia = temp;
                //        goAtacar = _ventanas[i].gameObject;
                //        indice = i;
                //    }s
                //}
                int _indice = Manager_Ventanas.Instance.Fn_GetCercana(transform.position.x, transform.position.z);
                goAtacar = Manager_Ventanas.Instance.Fn_GetVentanas()[_indice];
                //Debug.LogError("pos mueve " + Manager_Ventanas.Instance.Fn_GetPosSpawn(_indice));
                if (!_ventanas[_indice].GetComponent<Ventana>().Fn_GRota())//La ventana no esta rota
                {
                    AttackJugador = false; //Ignoramos jugadoor
                    v_IdPos = _ventanas[_indice].GetComponent<Ventana>().Fn_GetPosRand();
                    v_Destino = _ventanas[_indice].GetComponent<Ventana>().Fn_GetPosRand(v_IdPos);
                    if (_nuevadist > -2.0f)
                    {
                        transform.position = goAtacar.transform.position + (goAtacar.GetComponent<Ventana>().v_frente.forward * _nuevadist);  
                        //transform.position = goAtacar.transform.position + (goAtacar.GetComponent<Ventana>().v_frente.forward * _nuevadist);
                        Fn_SetDestination();// 69FFFF
                    }
                    return;  //Vamos a ventana
                }
                else //La ventana ya esta rota?
                {
                    AtacarJug();
                    return;
                }
            }
            else //No hay ventanas,, directo a jugadoor
            {
                AtacarJug();
                return;
            }
        }
        /// <summary>
        /// true es detenido false se mueve
        /// </summary>
        public void Fn_Detener(bool _val)
        {
            if (v_NavAgent.enabled)
            {
                //Debug.LogError("detiene "+ _val);
                v_anim.SetBool("v_mov", !_val);
                v_NavAgent.isStopped = _val;
            }
        }
        public void Fn_CongelaDetiene(bool _val)
        {
            if (v_NavAgent.enabled)
            {
                //Debug.LogError("detiene "+ _val);
                v_anim.SetBool("v_conge", _val);
                //v_anim.SetBool("v_mov", !_val);
                v_NavAgent.isStopped = _val;
            }
        }
        /// <summary>
        /// limpia el path
        /// </summary>
        public void Fn_Detener()
        {
            if (v_NavAgent.enabled )
            {
                v_NavAgent.isStopped = true;
                v_NavAgent.ResetPath();
                v_NavAgent.path.ClearCorners();
                v_Destino = new Vector3(-999, -999, -999);
            }
            v_anim.SetBool("v_mov", false);
        }      
        /// <summary>
        /// dejas de golpear
        /// </summary>
        void DejarGolpear()
        {//Apagamos goolpes de manos
            Hit_Mano[] manos = GetComponentsInChildren<Hit_Mano>();
            for (int i = 0; i < manos.Length; i++)
            {
                manos[i].Fn_SetGolpe(false); //manos[i].Fn_SetGolpe(true);
            }
            //Actualizamos animaciones
            v_anim.SetBool("v_mov", true);
            v_anim.SetBool("v_golpe", false);
        }
        void AtacarJug()
        {
            AttackJugador = true;
            //goAtacar = Jug_Datos.Instance.Fn_GetPosicion();
            v_Destino = goAtacar.transform.position;
        }
        protected virtual void AtacarObjMagico()
        {
            goAtacar = Manager_Ventanas.Instance.Go_ObjetivoEnemigos;
            v_Destino = goAtacar.transform.position;
            Fn_SetDestination();
        }
        /// <summary>
        /// Actualiza el mesh de navegacion y animacion a mover
        /// </summary>
        protected void Fn_SetDestination()
        {
            if (IsVivo)
            {
                v_NavAgent.ResetPath();
                v_NavAgent.path.ClearCorners();
                v_NavAgent.SetDestination(v_Destino);
                path = v_NavAgent.path.corners; //Debugeo
                v_anim.SetBool("v_mov", true);
                v_anim.SetBool("v_golpe", false);
            }
        }
        /// <summary>
        /// al instanciarlo, lo mueves arriba y con el rayo checas la altura, se regresa a su "0"  
        /// Y ya no da error de cercania el navmesh
        /// </summary>
        void Fn_SetPos()
        {
            Vector3 _nuevapos = Manager_Ventanas.Instance.Fn_GetPosRandom();
            RaycastHit _hit;
            Ray _rayo = new Ray();
            _rayo.origin = _nuevapos;
            _rayo.direction = -transform.up;
            if (Physics.Raycast(_rayo, out _hit))
            {
                transform.position = _hit.point;
            }
            else
            {
                transform.position = _nuevapos;
            }
        }
        /// <summary>
        /// ASigna a quien atacar
        /// </summary>
        public void Fn_SetAtacar(GameObject _enem)
        {
            goAtacar = _enem;
            v_Destino = _enem.transform.position;
            Fn_SetDestination();
        }
        protected void Fn_SetVida()
        {
            if (IsBoss)
            {
                v_vidaImg.fillAmount = Vida / VidaMaxima;
            }
        }
        /// <summary>
        /// DETECCION DE OBJETOS
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == k.Tags.HIT)
            {
                Fn_Detener();
                //print("Revisar!!!!");
                other.SendMessage("Fn_SetPadre", gameObject, SendMessageOptions.DontRequireReceiver);
                other.SendMessageUpwards("Fn_SetPadre", gameObject, SendMessageOptions.DontRequireReceiver);
            }
            else if (other.tag == k.Tags.PLAYER)
            {
                print("Atacar jugadoor desde ontrigger");
                Fn_Detener();
                goAtacar = other.gameObject;
                v_Destino = other.transform.position;
                Fn_SetDestination();
                AttackJugador = true;
            }
        }
        public bool Fn_GetDist(float _dis)
        {
            if (goAtacar != null)
            {
                Vector2 _vec = new Vector2(transform.position.x, transform.position.z);
                Vector2 _vec2 = new Vector2(goAtacar.transform.position.x, goAtacar.transform.position.z);
                float DistanciaObjectivo = Vector2.Distance(_vec, _vec2);
                //Estamos rango de algo?
                if (DistanciaObjectivo < (Rango * _dis))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool Fn_GetVivo() { return IsVivo; }
        public virtual void Fn_Revivir()
        {
            Debug.LogError("Fue llamado reivir");
            //Debug.Break();
            v_anim.enabled = false;
            Manager.Manager_Horda.Instance.Fn_Incremento();
        }
        public virtual void Fn_Atacar(bool _jugador) { }
        public virtual void Fn_Efecto() {  }
      
        #region Boss
        /// <summary>
        /// 0 es false 1 true  del jefe
        /// </summary>
        public void Fn_SetEfecto(int _valor)
        {
            if (_valor == 0)
            {
                BossIsSpecial = false;
                Fn_Detener(false);
                v_anim.SetBool("v_mov", true);
                v_anim.SetBool("v_especial", false);
            }
            else if (_valor == 1)
            {
                BossIsSpecial = true;
                v_anim.SetBool("v_especial", true);
                v_anim.SetBool("v_mov", false);
            }
        }
        /// <summary>
        /// aca tu le mandas numeros para que cada cantida de tiempo pueda hacer su ataque especial
        /// </summary>
        public void Fn_SetTiempo(float _min, float _max)
        {
            if (_min < 0)
                BossColdown = -1;
            else
                BossColdown = Random.Range(_min, _max);
        }
        /// <summary>
        /// COOLDOWN DEL ATAQUE ESPECIAL DEL BOSS
        /// </summary>
        IEnumerator E_Cooldown()
        {
            if (BossColdown > 0)
            {
                while (IsVivo)
                {
                    Fn_SetEfecto(0);
                    //v_Especial = false;
                    yield return new WaitForSeconds(BossColdown);
                    Fn_Detener(true);
                    // v_Especial = true;
                    Fn_SetEfecto(1);
                    Fn_Efecto();
                }
                StopCoroutine(E_Cooldown());
            }
        }
        #endregion //Boss
        /// <summary>
        /// CAMBIAR DE LAYER PARA MOVERSE DE NUEVO
        /// </summary>
        IEnumerator E_Revivir()
        {
            print("Fue llamado reivir de coroutina");
            Debug.Break();
            yield return new WaitForSeconds(4.0f);
            //8 enemigo 9 escenario  10 muerto
            gameObject.layer = k.Layers.ENEMY;
            print("revivo " + transform.name);
            //Jug_Datos.Instance.Fn_AddAlmas(v_almas);
            //v_Vida = 0;
            Vida = VidaMaxima;
            IsVivo = true;
            //StopAllCoroutines();
            //v_NavAgent.enabled = false;
            v_NavAgent.enabled = true;
            v_anim.enabled = true;
            v_rig.isKinematic = false;
            v_rig.useGravity = true;
            Fn_SetPos();
            Inicializar();
        }
       
        #region DEBUG
        private void OnDrawGizmosSelected()
        {
            if (goAtacar != null)
            {
                if (path.Length == 1)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(transform.position, path[0]);
                    Gizmos.DrawSphere(path[0], 0.2f);
                }
                else
                {
                    //Dibujamos camino que trata de tomar el enemigo
                    for (int i = 0; i < path.Length - 1; i++)
                    {
                        Gizmos.color = new Color(1, 0, 0, 0.75F);
                        Gizmos.DrawLine(path[i], path[i + 1]);
                        Gizmos.color = new Color(0, 1, 1, 1.0F);
                        Gizmos.DrawSphere(path[i + 1], 0.2f);
                    }
                }
            }
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(v_Destino, 0.05f);
        }
        #endregion
    }
}