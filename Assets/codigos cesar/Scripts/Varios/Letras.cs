using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ENCARGADO DE GUARDAR COSAS CON SUS MISMAS VARIABLES
/// </summary>
public class Letras
{
    public static string v_idioma { get => "idioma"; }
    public static string v_puntaje { get => "puntaje"; }
    public static string v_escena { get => "Escena"; }
    public static string v_escenaElim { get => "Scene_Eliminador"; }
    public static string v_primera { get => "Primera"; }
    public static string v_Noidioma { get => "Noidioma"; }
    /// <summary>
    /// regresa el valor en string
    /// </summary>
    public static string Fn_GetValor(string _key)
    {
        string _val =PlayerPrefs.GetString(_key, string.Empty);
        if (string.IsNullOrEmpty(_val))
            return "";
        else
            return _val;
    }
    /// <summary>
    /// regresa un vaor en int 
    /// </summary>
    public static int Fn_GetValor(string _key, int _nada)
    {
        int _val = PlayerPrefs.GetInt(_key, -1);
        return _val;
    }
    public static  void Fn_SetInt(string _key, int _value)
    {
        PlayerPrefs.SetInt(_key, _value);
        PlayerPrefs.Save();
    }
    public static void Fn_SetString(string _key, string _val)
    {
        PlayerPrefs.SetString(_key, _val);
        PlayerPrefs.Save();
    }
}
