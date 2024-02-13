using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Valve.VR.InteractionSystem;
//[RequireComponent(typeof(Interactable),typeof(Audio.Au_Manager), typeof(UnityEngine.AI.NavMeshObstacle))]
public class Scr_Consejo : MonoBehaviour
{
    [Header("Consejos")]
    /// <summary>
    /// panel si es que tiene diseño
    /// </summary>
    public GameObject v_panel;
    public GameObject v_fondo;
    [Header("Inicio")]
    public GameObject v_panelInicio;
    public GameObject v_fondoInicio;
    [Header("Lo demas")]
    public TextAsset[] v_asset;
    /// <summary>
    /// texto a cambiar
    /// </summary>
    public Text v_text;
    /// <summary>
    /// resultados de el textasset
    /// </summary>
    //List<C_Info> v_arr;
    /// <summary>
    /// lista random
    /// </summary>
    //public List<C_Info> v_lista;
    //public C_InfoCollection v_collec;
    public Animator v_anim;
    int v_Actual=0;
    public int v_IndexIzq=-1;
    Audio.Au_Manager v_audio;
    private void Awake()
    {
        v_panelInicio.SetActive(true);
        v_fondoInicio.SetActive(true);

        v_audio = GetComponent<Audio.Au_Manager>();
        v_audio.Fn_Inicializa();
        v_IndexIzq = -1;
        v_panel.SetActive(false);
        v_fondo.SetActive(false);
        //Fn_Crea(false);
    }
}
    /*private void HandHoverUpdate(Hand _hand)
    {
        if (_hand.GuessCurrentHandType() != Hand.HandType.Left)
            return;
        
        if(_hand.GetStandardInteractionButtonDown())
        {
            StartCoroutine(Ie_Delay());
        }
    }
    private void OnHandHoverBegin(Hand _hand)
    {
        if (_hand.GuessCurrentHandType() != Hand.HandType.Left)
            return;
            
        if(!v_audio.Fn_GetPlaying())
            v_audio.Fn_SetAudio(0, false, true);

        if (v_IndexIzq == -1  && Player.instance.leftHand.GetComponentInChildren<SteamVR_RenderModel>())
            v_IndexIzq = (int)Player.instance.leftHand.GetComponentInChildren<SteamVR_RenderModel>().index;

        //steamvr render model    index
        if (v_IndexIzq != -1)
        {
            SteamVR_Controller.Input(v_IndexIzq).TriggerHapticPulse(2000);
            SteamVR_Controller.Input(v_IndexIzq).TriggerHapticPulse(2000);
            SteamVR_Controller.Input(v_IndexIzq).TriggerHapticPulse(2000);
        }
    }
    WaitForSeconds v_away = new WaitForSeconds(6.0f);
    IEnumerator Ie_Delay()
    {
        //if(!v_audio.Fn_GetLoop())
            v_audio.Fn_SetAudio(1, true, true);
        
        v_Actual++;
        if(v_Actual>= v_lista.Count)
        {
            Fn_Crea(true);
        }
        else
        {
            v_anim.SetBool("v_con",true);
            int _val = v_Actual;

            v_panelInicio.SetActive(false);
            v_fondoInicio.SetActive(false);

            v_panel.SetActive(true);
            v_fondo.SetActive(true);
            v_text.text = v_lista[v_Actual].info;
            yield return v_away;
            if(_val== v_Actual)
            {
                v_anim.SetBool("v_con",false);
                v_panel.SetActive(false);
                v_fondo.SetActive(false);

                v_panelInicio.SetActive(true);
                v_fondoInicio.SetActive(true);

                //apaga sonido
                v_audio.Fn_Stop();
            }
        }
    }
    void Fn_Crea(bool _inicia)
    {
        v_Actual = -1;
        v_collec = JsonUtility.FromJson<C_InfoCollection>(v_asset[Idioma.Scr_ManagerIdioma.instance.Fn_GetIdioma()].text);
        v_arr = new List<C_Info>(v_collec.info);
        v_lista = new List<C_Info>();
        int _val = v_arr.Count;
        for(int i=0; i<_val; i++)
        {
            int _ran = Random.Range(0, v_arr.Count);
            v_lista.Add ( v_arr [ _ran]);
            v_arr.Remove(v_arr[_ran]);
        }
        if(_inicia)
        {
            StartCoroutine(Ie_Delay());
        }
    }
}
[System.Serializable]
public class C_Info
{
    public string info;
    public C_Info()
    {
        info = "";
    }
}
[System.Serializable]
public class C_InfoCollection
{
    public C_Info[] info;
}*/