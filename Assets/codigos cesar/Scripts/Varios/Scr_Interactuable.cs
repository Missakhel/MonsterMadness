using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
using UnityEngine.Events;
//[RequireComponent(typeof(Interactable), typeof(BoxCollider))]
public class Scr_Interactuable : MonoBehaviour
{
    public UnityEvent v_event;
    //public Hand.HandType v_handType;
    //private Hand v_hand;
    /*public virtual void OnHandHoverEnd(Hand hand)
    {
    }
    public virtual void OnHandHoverBegin(Hand hand)
    {

    }
    public virtual void HandHoverUpdate(Hand hand)
    {
        if(v_handType != Hand.HandType.Any &&  v_handType != hand.GuessCurrentHandType() )
        {
            Debug.LogError("MANO INCORRECTA");
            return;
        }
        v_hand = hand;
        if (v_hand != null  )
        {            
            if(v_hand.GetStandardInteractionButtonDown())
            {
                v_event.Invoke();
            }
        }
    }*/
}
