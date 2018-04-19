using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EasyEditor;
using System;

[System.Serializable]
public class Arm
{
  public MonkeyAnimations.BodyRegion region;
  public ArmState currentState;
  public Stack<ArmState> states;
  public Actor actor;
  public InControl.PlayerAction inputToWatch;

  public Arm(Actor pActor, MonkeyAnimations.BodyRegion pRegion, InControl.PlayerAction targetInput)
  {
    actor = pActor;
    region = pRegion;
    inputToWatch = targetInput;

    PushArmState(new ArmNormalState(), "Initialization of " + Enum.GetName(typeof(MonkeyAnimations.BodyRegion), region) + "arm");
  }

  public void PushArmState(ArmState state, string reason = "No Reason Given")
  {
    if (states == null)
    {
      states = new Stack<ArmState>();
    }

    if (state == null)
    {
      return;
    }
    var startingState = currentState;

    //dont push the same state
    if (states.Count > 0 && state.GetType() == states.Peek().GetType())
    {
      return;
    }

    states.Push(state);

    currentState = state;
    currentState.arm = this;
    currentState.Reset();

    if (startingState != null && currentState != null)
    {
      string message = string.Format("Pushed ArmState on {3}: Went from {0} to {1}. Reason: {2}", startingState, currentState, reason, Enum.GetName(typeof(MonkeyAnimations.BodyRegion), region));

      actor.actorLog.AddToLog(ActorLogType.ActorState, message);
    }
  }

  public void ProcessInput()
  {
    if (inputToWatch.IsPressed && currentState.isPressed == false)
    {
      currentState.OnPress();
    }
    else if (!inputToWatch.IsPressed && currentState.isPressed == true)
    {
      currentState.OnRelease();
    }
  }
}
