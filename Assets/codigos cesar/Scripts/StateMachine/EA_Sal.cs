using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EA_Sal : StateMachineBehaviour {

    //NavMeshAgent v_nav;
    //Transform v_padre;
    //AnimatorClipInfo[] m_AnimatorClipInfo;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //v_padre = animator.transform.parent;
        //v_nav = animator.GetComponentInParent<NavMeshAgent>();
        //m_AnimatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
    }
    //https://docs.unity3d.com/ScriptReference/AnimatorClipInfo.html
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {/*
        if (v_nav != null)
        {
            if (v_nav.isOnOffMeshLink)
            {
                v_nav.isStopped = true;
                Debug.Log("moviendo" + m_AnimatorClipInfo[0].clip.length);
                v_padre.position = Vector3.Lerp(v_padre.position, v_nav.currentOffMeshLinkData.endPos, m_AnimatorClipInfo[0].clip.length);
            }
        } */
      //  animator.transform.parent.SendMessage("Fn_Detener");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Debug.Log("sali");
        //animator.transform.parent.SendMessage("Fn_Saltar", false);
        animator.transform.SendMessage("Fn_Saltar", false);

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
