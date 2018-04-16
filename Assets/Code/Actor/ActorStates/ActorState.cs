using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorState
{
  public abstract void Initialize(Actor actor);

  public abstract void Reset(Actor actor);

  public abstract void Update(Actor actor);

  public abstract void GetHit(Actor actor);

  public abstract void PushState(Actor actor);

  public abstract void PopState(Actor actor);

  public abstract void ProcessInput(float forward, float strafe, Actor actor);

  public abstract void SetActionInt(int actionInt, Actor actor);
}

