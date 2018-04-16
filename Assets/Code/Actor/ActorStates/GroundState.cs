using UnityEngine;
using System.Collections;

public class GroundState : ActorState
{
  public override void GetHit(Actor actor)
  {
    throw new System.NotImplementedException();
  }

  public override void Initialize(Actor actor)
  {
    throw new System.NotImplementedException();
  }

  public override void PopState(Actor actor)
  {
    throw new System.NotImplementedException();
  }

  public override void ProcessInput(float forward, float strafe, Actor actor)
  {
    actor.animationManager.SetMovementValues(forward, strafe);
  }

  public override void PushState(Actor actor)
  {
    throw new System.NotImplementedException();
  }

  public override void Reset(Actor actor)
  {
    throw new System.NotImplementedException();
  }

  public override void SetActionInt(int actionInt, Actor actor)
  {
    throw new System.NotImplementedException();
  }

  public override void Update(Actor actor)
  {
    throw new System.NotImplementedException();
  }
}
