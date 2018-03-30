using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{
  //Animations
  public AnimationManager animationManager;

  //controls
  public ActorPlayerController playerController;

  //idklol
  public Transform shoulder;


  private void Start()
  {
    CheckForMissingDependencies();
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
