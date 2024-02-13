using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace INTERFAZVR
{
    using Jugador;
    using Manager;
    using Ventanas;
    public class Ifz_Botones : MonoBehaviour {
        /// <summary>
        /// PARA QUE TE DEJE COMPRAR
        /// </summary>
        private Tienda.Ti_Ventana v_tiventana;
        /// <summary>
        /// PARA PODER TENER LOS DATOS DE VENTANA ROTA
        /// </summary>
        private Ventana v_Ventana;

        private void OnEnable()
        {
            v_Ventana = GetComponentInParent<Ventana>();
            v_tiventana = GetComponentInParent<Tienda.Ti_Ventana>();
            //APAGAR TODOS
            for (int i=0; i<4; i++)
            {
                transform.GetChild(0).GetComponent<Button>().interactable = false;
            }
        }
        /* ACTIVAR DESCATIVAR EL INTERACTABLE DE LOS BOTONES
        */
        private void Update()
        {
            #region ESTA VENTANA EN LA QUE ESTOY, ESTA ROTA?
            if (Jug_Datos.Instance.Fn_PuedeComprar(v_tiventana.Fn_CosRepara()) && v_Ventana.Fn_GRota())
            {
                transform.GetChild(0).GetComponent<Button>().interactable = true;
            }else
            {
                transform.GetChild(0).GetComponent<Button>().interactable = false;
            }
            #endregion

            #region PUEDO REPARAR TODAS LAS VENTANAS?
            if (Jug_Datos.Instance.Fn_PuedeComprar(v_tiventana.Fn_CosReparaTodo()) && Manager_Ventanas.Instance.Fn_GetRotas())
            {
                transform.GetChild(1).GetComponent<Button>().interactable = true;
            }else
            {
                transform.GetChild(1).GetComponent<Button>().interactable = false;
            }
            #endregion

            #region PUEDO COMPRAR TORRETA? 
            if (Jug_Datos.Instance.Fn_PuedeComprar(v_tiventana.Fn_CosTorre()) && !v_tiventana.Fn_Gtorre())
            {
                transform.GetChild(3).GetComponent<Button>().interactable = true;
            }else
            {
                transform.GetChild(3).GetComponent<Button>().interactable = false;
            }
            #endregion
        
            #region PUEDO COMPRAR UNA BARRERA?
            if (Jug_Datos.Instance.Fn_PuedeComprar(v_tiventana.Fn_CosBarrera()) && !v_tiventana.Fn_GBarrera())
            {
                transform.GetChild(2).GetComponent<Button>().interactable = true;

            } else
            {
                transform.GetChild(2).GetComponent<Button>().interactable = false;
            }
            #endregion
        }
    }
}
