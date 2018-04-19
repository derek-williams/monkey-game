using UnityEngine;
using System.Collections;

public class ArmNormalState : ArmState
{
  public override void Initialize()
  {
  }

  public override void OnPress()
  {
    isPressed = true;
    arm.actor.animationManager.SetAnimatorValues(arm.region, MonkeyAnimations.Animation.Swing);
  }

  public override void OnRelease()
  {
    isPressed = false;
    arm.actor.animationManager.SetAnimatorValues(arm.region, MonkeyAnimations.Animation.None);
  }

  public override void PopState()
  {
  }

  public override void PushState()
  {
  }

  public override void Reset()
  {
  }

  public override void Update()
  {

  }
}
