using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorState
{
  public Actor actor;

  public virtual void Initialize()
  {

  }

  public virtual void Reset()
  {

  }

  public virtual void Update()
  {
    actor.LeftArm.currentState.Update();
    actor.RightArm.currentState.Update();
  }

  public virtual void GetHit()
  {

  }

  public virtual void PushState()
  {

  }

  public virtual void PopState()
  {

  }

  public virtual void ProcessInput(float forward, float strafe)
  {
    actor.animationManager.SetMovementValues(forward, strafe);
    actor.LeftArm.ProcessInput();
    actor.RightArm.ProcessInput();
  }

  public virtual void SetActionInt(int actionInt)
  {

  }
}

