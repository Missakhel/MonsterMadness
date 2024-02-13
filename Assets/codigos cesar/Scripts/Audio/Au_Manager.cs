using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class Au_Manager : MonoBehaviour
    {
        public AudioClip[] v_sonidos;
        AudioSource v_source;
        public bool v_auto=false;
        private void Awake()
        {
            if (v_auto)
                Fn_Inicializa();
        }
        public void Fn_Inicializa()
        {
            v_source = GetComponent<AudioSource>();
            v_source.Stop();
            v_source.playOnAwake = false;
            v_source.loop = false;
        }
        public void Fn_Solo()
        {
            Fn_SetAudio(0,false,true);
        }
        /// <param name="_inici">play?</param>
        public void Fn_SetAudio(int _indice, bool _loop, bool _inici)
        {
            //Debug.Log("Hola Cesar, soy tu, Cesar.");
            v_source.Stop();
            v_source.clip = v_sonidos[_indice];
            v_source.loop = _loop;
            if(_inici)
                v_source.Play();
        }
        /// <summary>
        /// true en pausa,  falso play
        /// </summary>
        public void Fn_Pausa(bool _pausa)
        {
            if (_pausa)
                v_source.Pause();
            else
                v_source.Play();
        }
        public void Fn_Stop()
        {
            v_source.Stop();
        }
        public bool Fn_GetLoop()
        {
            return v_source.loop;
        }
        public bool Fn_GetPlaying()
        {
            return v_source.isPlaying;
        }
    }
}
