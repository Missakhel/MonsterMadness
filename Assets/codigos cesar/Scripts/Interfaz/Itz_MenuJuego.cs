using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace INTERFAZVR
{
    using Jugador;
    public class Itz_MenuJuego : MonoBehaviour
    {
        public void Fn_Retirarse(GameObject _val)//val es el prefab de pausa panel
        {
            //Jug_Datos.Instance.Fn_SetMuerte(true);
            //Jug_Datos.Instance.Fn_SetPuntaje("Rendicion");
            _val.SetActive(false);
        }
        public void Fn_Salir()
        {
            Application.Quit();
        }
        public void Fn_Escena(string _nombre)
        {
            Letras.Fn_SetString(Letras.v_escena, _nombre);
            UnityEngine.SceneManagement.SceneManager.LoadScene(Letras.v_escenaElim);
        }
    }
}
