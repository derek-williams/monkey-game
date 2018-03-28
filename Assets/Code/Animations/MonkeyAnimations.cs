public class MonkeyAnimations
{
  /// <summary>
  /// This corresponds to the region of the body you wish to isolate
  /// </summary>
  public enum BodyRegion
  {
    None,       
    LeftArm,    
    RightArm,    
  }

  public enum Animation
  {
    None,     //0
    Idle,     //1
    Jump,     //2
    Swing,    //3
    Slide,    //4
    Grab,     //5
    Hold,     //6
    Throw,    //7
  }

  /// <summary>
  /// This returns the correct parameter name for the animator based on an isolated region.
  /// </summary>
  /// <param name="region"></param>
  /// <returns></returns>
  public static string GetAnimationIntParmeter(BodyRegion region)
  {
    switch (region)
    {
      case BodyRegion.None:
        return "BaseAnimationInt";
      case BodyRegion.LeftArm:
        return "LeftArmAnimationInt";
      case BodyRegion.RightArm:
        return "RightArmAnimationInt";
      default:
        return "BaseAnimationInt";
    }
  }
}
