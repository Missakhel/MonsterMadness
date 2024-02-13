using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C_PuntajeVisual : MonoBehaviour
{
    public Text v_numOleada;
    public Text v_muerte;
    public Text v_fecha;

    public void Fn_Set(string _oleada, string _muerte, string _fecha)
    {
        v_numOleada.text = _oleada;
        v_muerte.text = _muerte;
        v_fecha.text = _fecha;
    }
}
