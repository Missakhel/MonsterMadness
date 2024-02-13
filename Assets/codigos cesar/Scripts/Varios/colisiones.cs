using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisiones : MonoBehaviour
{
    private void OnParticleTrigger()
    {

        Debug.LogError("triger", gameObject);
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.LogError("aaaa" + other.name, gameObject);
    }
}
