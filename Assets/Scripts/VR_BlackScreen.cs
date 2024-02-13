using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_BlackScreen : MonoBehaviour
{
    [System.NonSerialized]
    public Color ColorActual = new Color(0, 0, 0, 0);
    [System.NonSerialized]
    public bool Activado = false;
    Material fadeMaterial;
    int SHADERID_fadeMaterialColor;

    void OnEnable()
    {
        //Creamos material
        if (fadeMaterial == null)
        {
            fadeMaterial = new Material(Shader.Find("Custom/SteamVR_Fade"));
            SHADERID_fadeMaterialColor = Shader.PropertyToID("fadeColor");
        }
    }

    void OnPostRender()
    {
        if (Activado)
        {
            /*var overlay = SteamVR_Overlay.instance;
            if (overlay != null)
            {
                overlay.alpha = 1.0f - ColorActual.a;
            }*/

            if (ColorActual.a > 0 && fadeMaterial)
            {
                fadeMaterial.SetColor(SHADERID_fadeMaterialColor, ColorActual);
                fadeMaterial.SetPass(0);
                GL.Begin(GL.QUADS);

                GL.Vertex3(-1, -1, 0);
                GL.Vertex3(1, -1, 0);
                GL.Vertex3(1, 1, 0);
                GL.Vertex3(-1, 1, 0);
                GL.End();
            }
        }
    }
}
