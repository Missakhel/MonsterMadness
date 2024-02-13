using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_DecalPool : MonoBehaviour
{
    int sizeLimit = 30;
    //const int sizeLimit = 30;
    int indexActual = -1; //Al llamar por primeva vez, avanza a 0
    List<GameObject> decals;
    GameObject prefabDecal;

    public void Init(GameObject _prefabDecal)//,int _size)
    {
        prefabDecal = _prefabDecal;
        //sizeLimit = _size;
        //Creamos los iniciales
        GameObject go;
        decals= new List<GameObject>();
        GameObject _padrepool = GameObject.Find("PoolManager");
        for (int i=0; i<sizeLimit; i++)
        {
            go = Instantiate(prefabDecal);
            go.transform.SetParent( _padrepool.transform);
            go.SetActive(false);
            decals.Add(go);
        }
    }

    public void Spawn(Vector3 _pos, Quaternion _rot, GameObject _padre)
    {
        GameObject go = GetGo();
        Vector3 scale = go.transform.localScale;
        go.transform.SetParent(_padre.transform);
        go.transform.localScale = scale;
        go.transform.position = _pos;
        go.transform.rotation = _rot;
    }

    //Internas----------------------------------------
    GameObject GetGo()
    {
        //Avanzamos en arreglo
        indexActual++;
        if (indexActual == sizeLimit)
            indexActual = 0;
        //Activmaos y regresamos
        decals[indexActual].SetActive(true);
        return decals[indexActual];
    }

	
}
