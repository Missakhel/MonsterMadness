using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Valve.VR.InteractionSystem;
namespace Armas
{
    public class Ar_MenuSelec : MonoBehaviour
    {


        /// <summary>
        /// tipo de arma a comprar
        /// </summary>
        public Arma v_tipo;
        /// <summary>
        /// index en el arreglo actual de manager
        /// </summary>
        public int v_Index;
        public Image v_img;
        public Text v_text;
        public string v_key;
        string v_nombre;
        Vector2 v_info;
        public Image v_imgfondo;
        private void OnEnable()
        {
            v_tipo = GetComponent<Arma>();
            v_img = GetComponent<Image>();
            v_nombre = Idioma.Scr_ManagerIdioma.instance.Fn_GetTexto(v_key);
            //v_nombre = v_tipo.GetType().ToString().Split('_')[1];
            Fn_Actualiza();
            Fn_Color(0);
            Fn_Select(GetComponentInParent<Ar_Menu>().Fn_GetManager().Fn_GetActual());
        }
        private void OnDisable()
        {
            StopCoroutine(Ie_Loop());
        }
        /// <summary>
        /// puede regresar -1 si no tiene esa arma
        /// </summary>
        /// <returns></returns>
        public int Fn_GetIndex()
        {
            return v_Index;
        }

        /// <summary>
        /// 0 normal  
        /// </summary>
        public void Fn_Color(int _val)
        {
            if(_val==0)//normal
            {
                v_img.color = Color.white;
            }
            else if(_val ==1)//selecciona
            {
                v_img.color = Color.blue;
                if (v_Index == -1)//no la tengo
                {
                    v_text.text = "<color=red>"+ Idioma.Scr_ManagerIdioma.instance.Fn_GetTexto("armas_1")+"</color>";
                    // v_img.color = Color.green;
                }
                else
                {
                    v_info= GetComponentInParent<Ar_Menu>().Fn_GetManager().Fn_GetArma(v_Index, "a");
                    v_text.text = "<color=white>"+v_nombre + "</color>";// v_info.x + " / " + v_info.y;
                }
            }
            else if(_val==2)//comprar
            {
                v_img.color = Color.red;
                v_text.text = "COMPRADA";
            }
        }
        /*#region NUEVA FORMA
        public virtual void OnHandHoverEnd(Hand hand)
        {
            if (hand.GuessCurrentHandType() != Hand.HandType.Left)
            {
                return;
            }
            Fn_Color(0);
            Fn_Select(GetComponentInParent<Ar_Menu>().Fn_GetManager().Fn_GetActual());
        }
        public virtual void OnHandHoverBegin(Hand hand)
        {
            if (hand.GuessCurrentHandType() != Hand.HandType.Left)
            {
                return;
            }
            Fn_Select(GetComponentInParent<Ar_Menu>().Fn_GetManager().Fn_GetActual());
            GetComponentInParent<Ar_Menu>().v_audioMAn.Fn_SetAudio(0, false, true);
            Fn_Color(1);
        }
        public virtual void HandHoverUpdate(Hand hand)
        {

            if (hand.GuessCurrentHandType() != Hand.HandType.Left  )
            {
                return;
            }
            if (hand.GetStandardInteractionButtonDown())
            {
                if (Fn_GetIndex() == -1)
                {
                    GetComponentInParent<Ar_Menu>().v_audioMAn.Fn_SetAudio(1, false, true); //"NO LA TIENES");
                }
                else
                {
                    if (Fn_GetIndex() == GetComponentInParent<Ar_Menu>().v_IndexObli)
                    {
                        GetComponentInParent<Ar_Menu>().v_extra.Invoke();
                    }
                    Player.instance.rightHand.GetComponent<Audio.Au_Manager>().Fn_SetAudio(7, false, true);//cerrar el menu
                    GetComponentInParent<Ar_Menu>().Fn_GetManager().Fn_Set(Fn_GetIndex());
                    Fn_Color(2);//comprar
                    GetComponentInParent<Ar_Menu>().gameObject.SetActive(false);
                }
            }
        }

        #endregion*/

        public void Fn_Select(Arma _tipo)
        {
            if(_tipo!= null)
            {
                if(v_tipo.GetType() == _tipo.GetType())
                {
                    v_img.color = Color.green;
                }
            }
        }
        public void Fn_Actualiza()
        {
            v_Index = GetComponentInParent<Ar_Menu>().Fn_GetManager().Fn_GetArma(v_tipo.GetType());
            v_imgfondo.gameObject.SetActive(false);
            StopCoroutine(Ie_Loop());
            Fn_Color(0);
        }
        public void Fn_Loop()
        {
            if(!v_imgfondo.gameObject.activeInHierarchy)
            {
                Debug.LogError("Loop obligatorio");
                StartCoroutine(Ie_Loop());
            }
        }
        IEnumerator Ie_Loop()
        {
            WaitForSeconds v_await =new WaitForSeconds( 0.5f);
            v_imgfondo.gameObject.SetActive(true);

            while(true)
            {
                v_imgfondo.color = Color.white;
                yield return v_await;
                v_imgfondo.color = Color.black;
                yield return v_await;
            }
        }
    }
}
