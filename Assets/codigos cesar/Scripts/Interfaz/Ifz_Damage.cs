using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace INTERFAZVR
{
    public class Ifz_Damage : MonoBehaviour
    {
        public Image v_FondoSlid;
        public Image v_Slid;
        public Text v_num;
        public Sprite v_vivo;
        public Sprite v_muerto;
        public bool v_apaga = false;

        public void Fn_Init(float _val)
        {
            v_Slid.type = Image.Type.Filled;
            v_Slid.fillMethod = Image.FillMethod.Horizontal;
            v_Slid.fillOrigin = 0;
            v_Slid.fillAmount = _val;
            if(!v_apaga)
                v_FondoSlid.sprite = v_vivo;
            else

            if(v_num!= null)
                v_num.text = (v_Slid.fillAmount * 100.0f).ToString("F0") + "%";
        }
        public void Fn_SetFill (float _val, bool _vivo)
        {
            v_Slid.fillAmount = _val;
            if (v_num != null)
                v_num.text = (v_Slid.fillAmount * 100.0f).ToString("F0") + "%";

            if(_vivo)
            {
                if(!v_apaga)
                    v_FondoSlid.sprite = v_vivo;                
            }
            else
            {
                if (v_apaga)
                    gameObject.SetActive(false);
                else
                    v_FondoSlid.sprite =v_muerto;
            }
        }
    }
}
