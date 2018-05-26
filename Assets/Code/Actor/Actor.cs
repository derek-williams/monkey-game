using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using EasyEditor;

public class Actor : MonoBehaviour
{
  //Animations
  public AnimationManager animationManager;

  //controls
  public ActorPlayerController playerController;

  public ActorState CurrentState;
  public Stack<ActorState> States;

  public Arm LeftArm;
  public Arm RightArm;

  //idklol
  public Transform shoulder;

  public ActorLog actorLog;

  private void Awake()
  {
    CheckForMissingDependencies();

    PushMovementState(new GroundState(), "Pushing to Grounded: Actor Awake");
    LeftArm = new Arm(this, MonkeyAnimations.BodyRegion.LeftArm, playerController.actorBindings.FireOne);
    RightArm = new Arm(this, MonkeyAnimations.BodyRegion.RightArm, playerController.actorBindings.FireTwo);

    WorldFollowCamera.Instance.AddTarget(this.transform, 1);
  }

  public void Update()
  {
    CurrentState.Update();
  }

  public void ProcessInput(float forward, float strafe)
  {
    ProcessMovementInput(forward, strafe);
  }

  public void ProcessMovementInput(float forward, float strafe)
  {
    if (CurrentState == null) { return; }
    
    CurrentState.ProcessInput(forward, strafe);
  }

  public void PushMovementState(ActorState state, string reason = "No Reason Given")
  {
    if (States == null)
    {
      States = new Stack<ActorState>();
    }

    if (state == null)
    {
      return;
    }
    var startingState = CurrentState;

    //dont push the same state
    if (States.Count > 0 && state.GetType() == States.Peek().GetType())
    {
      return;
    }

    States.Push(state);

    CurrentState = state;
    CurrentState.actor = this;
    CurrentState.Reset();

    if (startingState != null && CurrentState != null)
    {
      string message = string.Format("Pushed State: Went from {0} to {1}. Reason: {2}", startingState, CurrentState, reason);

      this.actorLog.AddToLog(ActorLogType.ActorState, message);
    }
  }

  public void CheckForMissingDependencies()
  {
    if (animationManager == null)
    {
      animationManager = this.GetComponent<AnimationManager>();
    }

    if (playerController == null)
    {
      playerController = this.GetComponent<ActorPlayerController>();
    }
  }
}

