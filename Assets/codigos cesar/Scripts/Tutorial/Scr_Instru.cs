using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
//using Valve.VR;

namespace Tutorial
{
    public class Scr_Instru : MonoBehaviour
    {
        public GameObject v_enem;
        public int v_indiceAc;
        public GameObject[] v_avisos;
        //Hand v_handizq;
        //Hand v_handder;
        public int v_indiceprueba;
        public GameObject v_damage;
        public GameObject v_ObjRender;
        public Transform v_camaraPos;
        //public ReflectionProbe v_reflec;
        private static Scr_Instru instance;
        public static Scr_Instru Instance
        {
            get
            {
                return instance;
            }
        }
        private void Awake()
        {
            if (instance)
            {
                DestroyImmediate(gameObject);
            }
            if (!instance)
            {
                v_ObjRender.SetActive(false);
                v_damage.SetActive(false);
                instance = this;
                //DontDestroyOnLoad(this);
                //v_indiceAc = 0;
                for (int i = 0; i < v_avisos.Length; i++)
                {
                    v_avisos[i].SetActive(false);
                }
                v_avisos[v_indiceAc].SetActive(true);
                //if (v_indiceAc == v_indiceprueba)// && v_indiceprueba<0)
                //{
                //    v_enem.SetActive(true);
                //    v_enem.SendMessage("Fn_DemoInit", SendMessageOptions.DontRequireReceiver);
                //}
                Manager.Manager_Ventanas.Instance.Fn_SetMax(1);
            }
        }
        private void Update()
        {
            /*if (v_handizq == null)
            {
                v_handizq = Player.instance.leftHand;
                if (v_handizq != null)
                {
                    v_handizq = Player.instance.leftHand;
                   // v_IndexIzq = (int)Player.instance.leftHand.GetComponentInChildren<SteamVR_RenderModel>().index;
                }
            }
            if (v_handder == null)
            {
                v_handder = Player.instance.rightHand;
                if (v_handder != null)
                {
                    v_handder = Player.instance.rightHand;
                   // v_IndexDer = (int)Player.instance.rightHand.GetComponentInChildren<SteamVR_RenderModel>().index;
                    v_handder.GetComponent<Jugador.Jug_Arma>().Fn_SetCambio(false);
                }
            }*/
            return;
        }
        /*public Hand Fn_GetHand(bool _izq)
        {
            if (_izq)
                return v_handizq;
            else
                return v_handder;
        }
        public int Fn_GetIndex(bool _izq)
        {
            if (_izq)
                return (int)Player.instance.leftHand.GetComponentInChildren<SteamVR_RenderModel>().index;

            // return v_IndexIzq;
            else
                return (int)Player.instance.rightHand.GetComponentInChildren<SteamVR_RenderModel>().index;
                //return v_IndexDer;
        }
        public void Fn_SetPos()
        {
            Player.instance.gameObject.transform.position =new Vector3( v_camaraPos.position.x,Player.instance.gameObject.transform.position.y, v_camaraPos.position.z);
            Player.instance.gameObject.transform.rotation = Quaternion.identity;
        }*/
        public void Fn_Elim()
        {
            StartCoroutine(Ie_Delay());
        }
        public void Fn_DanoJug()
        {
            StartCoroutine(Ie_Dano(Color.red));
        }
        public void Fn_Hints(bool _izq)
        {
            /*if(_izq)
            {
                ControllerButtonHints.HideAllButtonHints(Fn_GetHand(true));
            }
            else
            {
                ControllerButtonHints.HideAllButtonHints(Fn_GetHand(false));
            }*/
        }
        IEnumerator Ie_Dano(Color _col)
        {
            yield return new WaitForSeconds(2.0f);
            // Player.instance.GetComponent<Enemigos.Enem_Efectos>().Fn_Luz(_col);
            v_damage.SetActive(true);
            //Player.instance.GetComponent<Jugador.Jug_Datos>().Fn_Dano(70);
        }
        IEnumerator Ie_Delay()
        {
            yield return new WaitForSeconds(1.0f);
            DestroyImmediate(v_enem);
            StopCoroutine(Ie_Delay());

        }
        public void Fn_IniciaJuego()
        {
            Letras.Fn_SetString(Letras.v_escena, "Scene_StageTest 1");
            UnityEngine.SceneManagement.SceneManager.LoadScene(Letras.v_escenaElim);
        }
        public void Fn_SetVisible(bool _val)
        {
            /*if (v_handder)
                v_handder.gameObject.SetActive(_val);
            if(v_handizq)
                v_handizq.gameObject.SetActive(_val);

            v_ObjRender.SetActive(!_val);*/
        }
        public void Fn_Siguiente(int _val)
        {
            v_avisos[v_indiceAc].SetActive(false);
            v_indiceAc += _val;
            if (v_indiceAc < v_avisos.Length)
            { 
                v_avisos[v_indiceAc].SetActive(true);
                if (v_indiceAc >= v_avisos.Length-1)//terminado
                {
                    Letras.Fn_SetInt(Letras.v_primera, 1);
                    //v_reflec.RenderProbe();
                    Fn_Hints(false);
                    Fn_Hints(true);
                    //Player.instance.rightHand.GetComponent<Manager.Ar_Manager>().Fn_GetTipo(typeof(Armas.Ar_Pistola)).GetComponent<Armas.Ar_Pistola>().Fn_SetPuedes(true);
                }
                if(v_indiceAc== v_indiceprueba )//&& v_indiceprueba > 0)
                {
                    v_enem.SetActive(true);
                    v_enem.SendMessage("Fn_DemoInit", SendMessageOptions.DontRequireReceiver) ;
                }
            }
            else
            {
                Fn_Hints(false);
                Fn_Hints(true);
            }
        }
    }
}