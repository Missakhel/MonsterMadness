using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evemtos : MonoBehaviour {
    
    /// <summary>
    /// Ponerle a la animacion de golpe los 2 eventos desde la ventana de animacion
    /// </summary>
    void Activar(float _valo)
    {
        bool _atacar=false;
        if(_valo==0)
        {
            _atacar = true;
        }
        else if(_valo==1)
        {
            _atacar = false;
        }
        GetComponentInChildren<Enemigos.Hit_Mano>().Fn_SetGolpe(_atacar);
    }
    /// <summary>
    /// en la animacion de muerte cuando inicie, llamar el evento 
    /// </summary>
    void Fn_Layer(int _lay)
    {
        gameObject.layer = k.Layers.MUERTO;// 10;
        BoxCollider[] _colls= GetComponentsInChildren<BoxCollider>(); 
        for (int i=0; i<_colls.Length; i++)
        {
            _colls[i].gameObject.layer = k.Layers.MUERTO;
        }
    }
    public void Fn_SetAnim()//lo usa la bala sticky para no hacer daño hasta que se vea la animacion
    {
        SendMessage("Fn_SetDano");
    }
    /// <summary>
    /// en la animacion de muerte, cuandoo termine llamar el evento 
    /// </summary>
    void Fn_Muerte(int _muere)
    {
        GetComponent<Animator>().enabled = false;
        if(Manager_Horda.Instance)
        {
            Manager_Horda.Instance.Fn_Incremento();
        }
        else if(Tutorial.Scr_Instru.Instance)
        {
            Tutorial.Scr_Instru.Instance.Fn_Elim();
        }
        gameObject.SetActive(false);
    }
}
