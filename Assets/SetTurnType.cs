using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetTurnType : MonoBehaviour
{
  public ActionBasedSnapTurnProvider snapTurn;
  public ActionBasedContinuousTurnProvider continuousTurn;

  public void SetTypeFromIndex(int Index)
  {
    if(Index == 0)
    {
      snapTurn.enabled = false;
      continuousTurn.enabled = true;
    }
    else if (Index == 1)
    {
      snapTurn.enabled = true;
      continuousTurn.enabled = false;
    }

  }
}
