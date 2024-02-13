using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("MiniMap/Map canvas controller")]
[RequireComponent(typeof(RectTransform))]
public class MapCanvasController : MonoBehaviour
{
    #region Singleton
    public static MapCanvasController Instance
    {
        get
        {          
            return _instance;
        }
    }

    private static MapCanvasController _instance;
    #endregion

    #region Public

    /* Transform of the player that will be shown in the center of the map
     */ 
    public Transform playerTransform;

    [Tooltip("Distancia desde la cual ya se muestran en el radar sin poner dentro del radar")]
    public float radarDistance = 10;

    /* If true - objects out of range (radarDistance) will be hidden
     * If false - objects out of range (radarDistance) will stick to the border of the map
     * */
    public bool hideOutOfRadius = true;

    [Tooltip("Distancia maxima que son dectetables y se muestran en la esquina del radar")]
    public float maxRadarDistance = 10;

    /* Sets the scale of the radaDistance and maxRadarDistance
     */
    public float scale = 1.0f;

    /*Minimal opacity for the markers that are farther than radar distance
     */
    public float minimalOpacity = 0.3f;


    public InnerMap InnerMapComponent
    {
        get
        {
            return innerMap;
        }
    }

    public MarkerGroup MarkerGroup
    {
        get
        {
            return markerGroup;
        }
    }
    #endregion


    #region Private
    private RectTransform mapRect;
    private InnerMap innerMap;
    private MapArrow mapArrow;
    private MarkerGroup markerGroup;
    private float innerMapRadius;
    #endregion

    #region Unity Methods

    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (!playerTransform)
        {
            Debug.LogError("You must specify the player transform");
            this.enabled = false;
        }

        mapRect = GetComponent<RectTransform>();

        innerMap = GetComponentInChildren<InnerMap>();
        if (!innerMap)
        {
            Debug.LogError("InnerMap component is missing from children");
        }

        mapArrow = GetComponentInChildren<MapArrow>();
        if (!mapArrow)
        {
            Debug.LogError("MapArrow component is missing from children");
        }

        markerGroup = GetComponentInChildren<MarkerGroup>();
        if (!markerGroup)
        {
            Debug.LogError("MerkerGroup component is missing. It must be a child of InnerMap");
        }

        innerMapRadius = innerMap.getMapRadius();

    }

	void Update () 
    {
        //Rotamoso mapa
        mapRect.localRotation = Quaternion.Euler(new Vector3(0, 0, playerTransform.eulerAngles.y));
        //Rotamos flecha de manera relativa
        mapArrow.rotateVR(Quaternion.Euler(new Vector3(0, 0, -playerTransform.eulerAngles.y)));
     
    }

    #endregion

    #region Custom methods

    public void checkIn(MapMarker marker)
    {
        if (!playerTransform)
        {
            //error was already fired in Awake()
            return;
        }

        float scaledRadarDistance = radarDistance * scale;
        float scaledMaxRadarDistance = maxRadarDistance * scale;

        if (marker.isActive)
        {
            float distance = distanceToPlayer(marker.getPosition());
            float opacity = 1.0f;

            if (distance > scaledRadarDistance)
            {
                if (hideOutOfRadius)
                {
                    if (marker.isVisible())
                    {
                        marker.hide();
                    }
                    return;
                }
                else
                {
                    if (distance > scaledMaxRadarDistance)
                    {
                        if (marker.isVisible())
                        {
                            marker.hide();
                        }
                        return;
                    }
                    else
                    {
                        //Opacidad por distancia

                        distance = scaledRadarDistance;
                    }
                }
            }
            else
            {
                //Opacidad por altura
                if (marker.isVisible())
                {
                   
                    //Obtenemos diferencia en altura
                    float difAltura = distanceAltura(marker.getPosition());

                    if (difAltura >= 2.0f)
                    {
                        opacity = minimalOpacity;
                    }
                }

            }

            if (!marker.isVisible())
            {
                marker.show();
            }

            //Calculamos posicion relativa a jugador
            Vector3 posDif = marker.getPosition() - playerTransform.position;
            Vector3 newPos = new Vector3(posDif.x, posDif.z, 0);
            newPos.Normalize();

            //Calculamois escala en radar
            float markerRadius = (marker.markerSize / 2);
            float newLen = (distance / scaledRadarDistance) * (innerMapRadius - markerRadius);

            //Asignamos
            newPos *= newLen;
            marker.setLocalPos(newPos);
            marker.setOpacity(opacity);
        }
        else
        {
            if (marker.isVisible())
            {
                marker.hide();
            }
        }
    }

    private float distanceToPlayer(Vector3 other)
    {
        
        return Vector2.Distance(new Vector2(playerTransform.position.x, playerTransform.position.z), new Vector2(other.x, other.z));
    }

    /// <summary>
    /// REgresa la altura entre jugadaor y el marker
    /// </summary>
    /// <param name="_other">Diferencia de altura </param>
    /// <returns></returns>
    float distanceAltura(Vector3 _other)
    {
        return Mathf.Abs(playerTransform.position.y - _other.y);
    }
    

    #endregion
}
