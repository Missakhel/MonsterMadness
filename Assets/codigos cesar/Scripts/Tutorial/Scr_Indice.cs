using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Tutorial
{
    public class Scr_Indice : MonoBehaviour
    {
        public bool v_arma=false;
        public TextAsset[] v_assetexto;
        public List<C_Indice> v_indice;
        public List<GameObject> v_enemigos;
        C_IndiceCollection v_colect;

       // public GameObject v_Panel;
        public GameObject v_BtnSig;
        public GameObject v_BtnAnt;

        //[Header("EXTRA")]
        //public GameObject v_panelExtra;
        //public Text v_extra;
        //public string _sda;
        public int v_indiceActual = -1;
        public Scr_Indice[] v_btns;
        private void Awake()
        {
            v_colect = JsonUtility.FromJson<C_IndiceCollection>(v_assetexto[Idioma.Scr_ManagerIdioma.instance.Fn_GetIdioma()].text);
            v_indice = new List<C_Indice>(v_colect.info);
            v_enemigos = new List<GameObject>();
            for (int i = 0; i < v_indice.Count; i++)
            {
                GameObject _val = Instantiate(Resources.Load("Indice/" + v_indice[i].v_prefab, typeof(GameObject)), transform) as GameObject;
                if (_val != null)
                {
                    _val.transform.position = Vector3.zero;
                    _val.SetActive(false);
                    v_enemigos.Add(_val);
                }
            }
            v_indiceActual = 0;
            //v_Panel.SetActive(false);
            v_BtnSig.SetActive(false);
            v_BtnAnt.SetActive(false);
        }
        public void Fn_Muestra()
        {
            //v_Panel.SetActive(!v_Panel.activeInHierarchy);
            v_BtnSig.SetActive(!v_BtnSig.activeInHierarchy);
            v_BtnAnt.SetActive(!v_BtnAnt.activeInHierarchy);

            if(v_BtnSig.activeInHierarchy)
            {
                for (int i = 0; i < v_btns.Length; i++)
                {
                    v_btns[i].Fn_Apaga();
                }
                v_indiceActual = 0;
                Fn_Set(v_indiceActual);
            }
            else
            {
                for (int i = 0; i < v_enemigos.Count; i++)
                {
                    v_enemigos[i].SetActive(false);
                }
            }
        }
        public void Fn_Menos()
        {
            v_indiceActual--;
            if (v_indiceActual <0)
            {
                v_indiceActual = v_indice.Count - 1;
            }
            Fn_Set(v_indiceActual);
        }
        public void Fn_Mas()
        {
            v_indiceActual++;
            if (v_indiceActual >= v_indice.Count)
            {
                v_indiceActual = 0;
            }
            Fn_Set(v_indiceActual);
        }
        public void Fn_Apaga()
        {
            v_BtnSig.SetActive(false);
            v_BtnAnt.SetActive(false);

            for (int i = 0; i < v_enemigos.Count; i++)
            {
                v_enemigos[i].SetActive(false);
            }
        }
        public void Fn_Set(int _ind)
        {
            for (int i = 0; i < v_enemigos.Count; i++)
            {
                v_enemigos[i].SetActive(false);
            }
            v_enemigos[_ind].SetActive(true);
            v_enemigos[_ind].transform.position = Vector3.zero;
            v_enemigos[_ind].transform.localPosition = Vector3.zero;
            v_enemigos[_ind].GetComponent<Scr_IndiceEnem>().Fn_Set(v_indice[_ind],v_arma);
            

            //v_nombre.text = v_indice[_ind].v_nombre;
            //v_tipo.text = v_indice[_ind].v_tipo;
            //v_infoNormal.text = v_indice[_ind].v_infonormal;
            //if (v_indice[_ind].v_jefe)
            //{
            //    v_panelJefe.SetActive(true);
            //    v_efecto.text = v_indice[_ind].v_infoEfecto;
            //}
            //else
            //{
            //    v_panelJefe.SetActive(false);
            //}







            //if (v_indice[_ind].v_extra)
            //{
            //    v_panelExtra.SetActive(true);
            //    v_extra.text = v_indice[_ind].v_InfoExtra;
            //}
            //else
            //{
            //    v_panelExtra.SetActive(false);
            //}
        }
    }
    [System.Serializable]
    public class C_Indice
    {
        [Header("NORMAL")]
        public string v_nombre;
        public string v_prefab;
        public string v_tipo;
        // public string v_infonormal;
        public string v_tipoataque;
        [Header("JEFE")]
        public bool v_jefe;
        public string v_infoEfecto;
        [Header("EXTRA")]
        public bool v_extra;
        public string v_InfoExtra;
        public C_Indice ()
        {
            v_prefab = "";
            v_nombre="";
            v_tipo="";
            //    v_infonormal="";
            v_tipoataque = "";
            v_jefe=false;
            v_infoEfecto="";
            v_extra=false;
            v_InfoExtra = "";
        }
    }
    [System.Serializable]
    public class C_IndiceCollection
    {
        public C_Indice[] info;
    }
}