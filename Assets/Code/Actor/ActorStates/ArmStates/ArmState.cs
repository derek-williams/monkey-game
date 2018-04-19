using UnityEngine;
using System.Collections;

public class ArmState
{
  public Arm arm;
  public bool isPressed = false;
  public MonkeyAnimations.BodyRegion region;

  public virtual void Initialize()
  {

  }

  public virtual void Update()
  {

  }
  public virtual void Reset()
  {

  }

  public virtual void OnPress()
  {

  }

  public virtual void OnRelease()
  {

  }

  public virtual void PushState()
  {

  }

  public virtual void PopState()
  {

  }
}

public enum ContextTarget
{
  None,
  Swing,
  Grab
}
