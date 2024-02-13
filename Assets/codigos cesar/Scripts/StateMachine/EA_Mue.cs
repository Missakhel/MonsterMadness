using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Mue : StateMachineBehaviour {
    bool _muerto = false;

    private void OnEnable()
    {
        _muerto = false;
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!_muerto)
        {
            animator.transform.gameObject.layer = k.Layers.MUERTO;
            _muerto = true;
        }
        else
        {
            //Manager_Horda.Instance.Fn_Incremento();
            animator.gameObject.SendMessage("Fn_Revivir");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    //  OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        /* Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("ANM_Mummy_Death"));
         Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("golpe"));
         Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("ANM_Mummy_Walk 0"));
         Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("New State"));
         Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));*/
        //animator.gameObject. SendMessage("Fn_Revivir");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
