using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour
{
  //Animations
  public AnimationManager animationManager;

  //controls
  public ActorPlayerController playerController;

  public Stack<ActorState> States;
  public ActorState CurrentState;

  //idklol
  public Transform shoulder;

  public ActorLog actorLog;

  private void Awake()
  {
    CheckForMissingDependencies();

    PushState(new GroundState(), "Pushing to Grounded: Actor Awake");
  }

  public void Update()
  {
    CurrentState.Update(this);
  }

  public void ProcessInput(float forward, float strafe)
  {
    if (CurrentState != null)
    {
      CurrentState.ProcessInput(forward, strafe, this);
    }
  }

  public void PushState(ActorState state, string reason = "No Reason Given")
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
    CurrentState.Reset(this);

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
