using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tutorial
{
    public class Scr_IndiceAnim : MonoBehaviour
    {
        Animator v_anim;
        private void OnEnable()
        {
            v_anim = GetComponent<Animator>();
            v_anim.SetBool("v_Vivo", true);
            v_anim.SetBool("v_mov", false);
            v_anim.SetBool("v_golpe", false);
        }
    }
}
