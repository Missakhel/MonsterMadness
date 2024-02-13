using UnityEngine;
using System.Collections;

[AddComponentMenu("MiniMap/Map arrow")]
public class MapArrow : MonoBehaviour {

    private RectTransform _arrowRect;

    public RectTransform ArrowRect
    {
        get
        {
            if (!_arrowRect)
            {
                _arrowRect = GetComponent<RectTransform>();
                if (!_arrowRect)
                {
                    Debug.LogError("RectTransform not found. MapArrow script must by attached to an Image.");
                }
            }

            return _arrowRect;
        }
    }

    public void rotate(Quaternion quat)
    {
        ArrowRect.rotation = quat;
    }


    public void rotateVR(Quaternion quat)
    {
        /*Vector3 rot = quat.eulerAngles;
        rot.x = 0.0f;
        rot.y = 0.0f;
        ArrowRect.localRotation = Quaternion.Euler(rot);*/
        ArrowRect.localRotation = quat;
    }
}
