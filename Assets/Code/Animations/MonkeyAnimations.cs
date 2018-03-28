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
    Idle,     //0
    Jump,     //1
    Swing,    //2
    Slide,    //3
    Grab,     //4
    Hold,     //5
    Throw,    //6
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
