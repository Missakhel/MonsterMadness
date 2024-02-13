using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Enemigos
{
    [RequireComponent(typeof(NavMeshAgent), typeof(scr_SpeedFeet))]
    public class Enem_Momia : Enemigo_base {
        float v_DanoOriginal = 0;
        float v_DefensaOriginal = 0;
        bool IsJumping = false;
        [Header("DEMO TUTORIAL")]
        public bool v_demo = false;
        public GameObject v_PanelRepara;
        public Idioma.Scr_Idioma v_textoRepara;
        public Collider v_Coll;
        public Collider v_CollBtn;
        private void Awake()
        {
            //if(v_demo)
            //{
            //    Fn_DemoInit();
            //}
            //else
            //{
            //    Inicializar();
            //}
                Inicializar();
        }
        protected override void Inicializar()
        {
            //if(!v_demo)
            //{
                if (IsBoss == true) //NOTA: Se puede omitir
                {
                    Velocidad = 90;
                    Almas = 50;
                    VidaMaxima = 150;
                    Vida = VidaMaxima;
                    Danio = 40;
                    Defensa = 5;
                    Fn_SetTiempo(2,12);//Fn_SetTiempo(20, 40);
                }
                else
                {
                    Velocidad = 100;
                    Almas = 15;
                    VidaMaxima = 100;
                    Vida = VidaMaxima;
                    Danio = 10;
                    Defensa = 1;
                }
                v_DanoOriginal = Danio;
                v_DefensaOriginal = Defensa;
                base.Inicializar();
            //}
        }
        public override void Fn_Atacar(bool _jugador)
        {
            base.Fn_Atacar(_jugador);
            v_anim.SetBool("v_golpe", true);
            v_anim.SetBool("v_mov", false);
        }
        public override void Fn_Saltar(bool _valor)
        {
            //Solo debe llamarse una vez al saltar
            if (IsJumping && _valor) return;
            IsJumping = _valor;
            base.Fn_Saltar(_valor);
            StartCoroutine(JumpAnim());
        }
        
        IEnumerator JumpAnim()
        {
            v_NavAgent.speed = 0.0f;
            yield return new WaitForSeconds (0.45f);
            v_NavAgent.speed = 2.9f;
            yield return new WaitForSeconds(0.55f);
            //v_NavAgent.speed = 1.1f;
           // yield return new WaitForSeconds(0.1f);
            v_NavAgent.speed = 0f;
            //agregado por cesar   se le da posicion que deberia tener como destino
            v_Destino = goAtacar.transform.position;
            Fn_SetDestination();
            //agregado por cesar
        }
        protected override void AtacarObjMagico()
        {
            if(v_demo)
            {
                Tutorial.Scr_Instru.Instance.Fn_SetVisible(true);//mostrar las manos y quitar el render
                v_Coll.enabled = false;
                v_PanelRepara.SetActive(true);
                v_anim.SetBool("v_Vivo", true);
                v_anim.SetBool("v_mov", false); 
                v_anim.SetBool("v_golpe", false);
            }
            else
                base.AtacarObjMagico();
        }
        public override void Dano(GameObject _aseguir)
        {
            if (!v_demo)
                base.Dano(_aseguir);
        }
        protected override void Fn_Muerto()
        {
            if(v_demo)
            {
                v_CollBtn.enabled = true;
                IsVivo = false;
                v_anim.SetBool("v_Vivo", IsVivo);
                v_anim.SetBool("v_mov", false);
                v_anim.SetBool("v_golpe", false);
                v_textoRepara.Fn_SetKey("tuto_17");
                //Tutorial.Scr_Instru.Instance.Fn_Siguiente(1);
            }
            base.Fn_Muerto();
        }
        public override void Dano(float _dano)
        {
            if(v_demo)
            {
                if(!Manager.Manager_Ventanas.Instance.Fn_GetRotas())
                {
                    Debug.LogError("no hace dano");
                    v_efec.Fn_Dano();
                }
                else
                    base.Dano(_dano);
            }
            else
            {
                base.Dano(_dano);
            }
        }
        public void Fn_DemoInit()
        {
            Debug.LogError("inicia demo");
            IsVivo = true;
            v_anim = GetComponentInChildren<Animator>();
            v_anim.SetBool("v_Vivo", IsVivo);
            v_anim.SetBool("v_mov", true);
            v_anim.SetBool("v_golpe", false);
            v_rig = GetComponent<Rigidbody>();
            v_NavAgent = GetComponent<NavMeshAgent>();
            v_PanelRepara.SetActive(false);
            Velocidad = 100;
            Almas = 140;
            VidaMaxima = 100;
            Vida = VidaMaxima;
            Danio = 10;
            Defensa = 3;

            //Iniciar de variables
            if (IsMelee)
            {
                Rango = 1.0f;
            }//Prepramos mesh de navegación

            //v_efec.Fn_Inicializa();
            //if (!ColorUtility.TryParseHtmlString("#d45353", out v_color))
            //    v_color = Color.green;
        }
        //https://docs.unity3d.com/ScriptReference/Object.FindObjectsOfType.html
        //https://unity3d.com/es/learn/tutorials/topics/scripting/loops
        //System.Type es diferente a Types
        public override void Fn_Efecto()
        {
            Object[] temp = FindObjectsOfType(typeof(Enem_Momia));
            foreach (Enem_Momia item in temp)
            {
                if (item.IsVivo && item.IsBoss == false)
                {//haceres aumento de algo
                    item.Danio += 0.5f;
                    item.Defensa += 0.5f;
                    item.Danio = Mathf.Clamp(item.Danio, item.v_DanoOriginal, item.v_DanoOriginal + 2.0f);
                    item.Defensa = Mathf.Clamp(item.Defensa, item.v_DefensaOriginal, item.v_DefensaOriginal + 2.0f);
                    //item.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.green;
                }
            }
        }
    }
}
