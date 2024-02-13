using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
  public InputActionProperty m_pinchAnimationAction;
  public InputActionProperty m_gripAnimationAction;
  public Animator m_handAnimator;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    float triggerValue = m_pinchAnimationAction.action.ReadValue<float>();
    m_handAnimator.SetFloat("Trigger", triggerValue);
    float gripValue = m_pinchAnimationAction.action.ReadValue<float>();
    m_handAnimator.SetFloat("Grip", gripValue);
  }
}
