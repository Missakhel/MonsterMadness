using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Eliminador : MonoBehaviour
{
    string v_escena;
    private void Awake()
    {
        /*if(Valve.VR.InteractionSystem.Player.instance)
            DestroyImmediate(Valve.VR.InteractionSystem.Player.instance.gameObject);
        v_escena=Letras.Fn_GetValor(Letras.v_escena);
        Fn_Elimina();*/
    }
    void Fn_Elimina()
    {
        GameObject[] _objs= GameObject.FindObjectsOfType<GameObject>();
        for(int i=0; i<_objs.Length; i++)
        {
            if (_objs[i].tag == k.Tags.NO_ELIMINA || _objs[i] != gameObject  )
            {
                DestroyImmediate(_objs[i]);
            }
        }
        if(!string.IsNullOrEmpty( v_escena))
        {
            Debug.LogError("Ir a " +v_escena);
            UnityEngine.SceneManagement.SceneManager.LoadScene(v_escena);
        }
        else
        {
            Debug.LogError("vacio");
        }
    }
}
