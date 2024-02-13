using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Enemigos
{
    using Jugador;
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enem_Zombie : Enemigo_base
    {
        protected bool v_Efecto;
        float v_VelOriginal;

        private void Awake()
        {//melee, vida alta, lento, media, terrestre
            IsMelee = true;
            Vida = 50.0f;
            Velocidad = 2.0f;
            Danio = 2.0f;
            IsTerrestre = true;
            v_VelOriginal = Velocidad;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.gray;
            base.Inicializar();
        }
        public override void Fn_Atacar(bool _jugador)
        {
            base.Fn_Atacar(_jugador);

            if (IsBoss == true)
            {
                Fn_Efecto();
            }
            else if (IsBoss == false)
            {
                
            }
        }
        public override void Fn_Efecto()
        {
            Object[] temp = FindObjectsOfType(typeof(Enem_Zombie));
            foreach (Enem_Zombie item in temp)
            {
                if (item.IsVivo && item.IsBoss == false)
                {//haceres aumento de algo, zombie los hace mas rapidos
                    item.Velocidad += 0.5f;
                    item.Velocidad = Mathf.Clamp(item.Danio, item.v_VelOriginal, item.v_VelOriginal + 2.0f);
                }
            }
        }
        public IEnumerator E_Efecto()
        {
            while (IsVivo)
            {
                yield return new WaitForSeconds(0.4f); //TODO: AAAAA!
            }
        }
    }
}