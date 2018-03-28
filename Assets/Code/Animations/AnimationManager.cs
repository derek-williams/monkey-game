using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
  public Animator animator;
  public Actor actor;

  private void Start()
  {
    if (actor == null)
    {
      actor = GetComponent<Actor>();
    }

    if(animator == null)
    {
      animator = actor.GetComponent<Animator>();
    }
  }

  private void Update()
  {

  }

  public void SetAnimatorValues(MonkeyAnimations.BodyRegion region = MonkeyAnimations.BodyRegion.None, MonkeyAnimations.Animation animation = MonkeyAnimations.Animation.Idle)
  {
    animator.SetInteger(MonkeyAnimations.GetAnimationIntParmeter(region), (int)animation);
  }
}
