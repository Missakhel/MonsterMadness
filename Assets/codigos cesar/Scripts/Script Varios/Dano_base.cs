using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Dano_base : MonoBehaviour {

    public bool v_Vivo = true;
    [ReadOnly]
    public float v_Vida = 100;
    public float v_VidaMax = 100;
    public float v_Def = 2;
    public GameObject v_padre;

    public void Fn_SetVida(float _max, float _def)
    {
        v_VidaMax = _max;
        v_Vida = v_VidaMax;
        v_Vivo = true;
        v_Def=_def;
    }
    public bool Fn_GetVivo()
    {
        return v_Vivo;
    }
    public void Fn_SetVivo(bool _val)
    {
        v_Vivo = _val;
    }
    public virtual void Fn_SetDano(float _dano)
    {
        if (!v_Vivo)
            return;
        
        float resta = _dano - v_Def;
        if (resta <= 0)
        {
            return;
        }
        else
        {
            v_Vida -= resta;
            v_Vida = Mathf.Clamp(v_Vida, 0, v_VidaMax);
            if (v_Vida <= 0)
            {
                Fn_Muerto();
            }
        }
    }
    public virtual void  Fn_Muerto(){}
    public void Fn_SetPadre(GameObject _areenviar)
    {
        _areenviar.SendMessage("Fn_SetAtacar", v_padre, SendMessageOptions.DontRequireReceiver);
    }
}
