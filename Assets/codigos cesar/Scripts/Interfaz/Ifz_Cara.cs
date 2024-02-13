using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace INTERFAZVR
{
    public class Ifz_Cara : MonoBehaviour
    {
        public Image v_img;
        public float v_alfa=1;
        private void OnEnable()
        {
            v_alfa = 1;
            v_img.color=new Color( 1,1,1,1);
        }
        private void Update()
        {
            v_alfa -= 0.5f * Time.deltaTime;
            v_img.color=new Color( 1,1,1,v_alfa);
            if(v_alfa<0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
