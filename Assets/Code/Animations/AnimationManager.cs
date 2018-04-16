using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
  [HideInInspector]
  public Animator animator;
  [HideInInspector]
  public Actor actor;

  public float MovementDamp = .1f;

  private void Start()
  {
    if (actor == null)
    {
      actor = GetComponent<Actor>();
    }

    if (animator == null)
    {
      animator = actor.GetComponent<Animator>();
    }
  }

  private void Update()
  {

  }

  public void SetAnimatorValues(MonkeyAnimations.BodyRegion region = MonkeyAnimations.BodyRegion.None, MonkeyAnimations.Animation animation = MonkeyAnimations.Animation.None)
  {
    //set integer for corresponding region
    animator.SetInteger(MonkeyAnimations.GetAnimationIntParmeter(region), (int)animation);

    //stop existing lerp routine
    if (activeLayerRoutines.ContainsKey(region))
    {
      StopCoroutine(activeLayerRoutines[region]);
    }

    //for any applied region, start layer weight routine
    if (region != MonkeyAnimations.BodyRegion.None)
    {
      if (animation != MonkeyAnimations.Animation.None)
      {
        activeLayerRoutines.Add(region, StartCoroutine(LayerLerpRoutine(region, 1, .25f)));
      }
      else
      {
        activeLayerRoutines.Add(region, StartCoroutine(LayerLerpRoutine(region, 0, .25f)));
      }
    }
  }

  private Dictionary<MonkeyAnimations.BodyRegion, Coroutine> activeLayerRoutines = new Dictionary<MonkeyAnimations.BodyRegion, Coroutine>();

  public IEnumerator LayerLerpRoutine(MonkeyAnimations.BodyRegion region, float target, float time)
  {
    //get starting weight
    float startWeight = animator.GetLayerWeight((int)region);

    //start current time proportionately to start weight
    float currentTime = time * (1 - Mathf.Abs(target - startWeight));

    //lerp til completion
    while (currentTime < time)
    {
      currentTime += Time.deltaTime;
      animator.SetLayerWeight((int)region, Mathf.Lerp(startWeight, target, currentTime / time));
      yield return new WaitForEndOfFrame();
    }
  }

  public void SetMovementValues(float forward, float strafe)
  {
    animator.SetFloat("Forward", forward, MovementDamp, Time.deltaTime);
    animator.SetFloat("Strafe", strafe, MovementDamp, Time.deltaTime);
  }
}
