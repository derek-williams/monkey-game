using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ActorPlayerController : MonoBehaviour
{
  public Actor actor;
  public ActorPlayerControllerBindings actorBindings;

  void Awake()
  {
    actorBindings = ActorPlayerControllerBindings.CreateDefaultKeyboardBindings();
  }

  private void FixedUpdate()
  {
    actor.ProcessInput(actor.playerController.actorBindings.MoveAxes.Y, actor.playerController.actorBindings.MoveAxes.X); 
  }
}

[System.Serializable]
public class ActorPlayerControllerBindings : PlayerActionSet
{
  //fire
  public PlayerAction FireOne;
  public PlayerAction FireTwo;

  //jump
  public PlayerAction Jump;

  //Movement
  public PlayerTwoAxisAction MoveAxes;
  public PlayerAction MoveLeft;
  public PlayerAction MoveRight;
  public PlayerAction MoveForward;
  public PlayerAction MoveBackward;

  //Aiming
  public PlayerTwoAxisAction AimAxes;
  public PlayerAction AimLeft;
  public PlayerAction AimRight;
  public PlayerAction AimUp;
  public PlayerAction AimDown;

  //logging buttons
  public PlayerAction GenerateMyLog;
  public PlayerAction AutoGenerateAllActorLogs;
  public bool autoGenerateAllLogs = false;

  public ActorPlayerControllerBindings()
  {
    //fire buttons
    FireOne = CreatePlayerAction("FireOne");
    FireTwo = CreatePlayerAction("FireTwo");

    //movement
    MoveForward = CreatePlayerAction("MoveForward");
    MoveRight = CreatePlayerAction("MoveRight");
    MoveBackward = CreatePlayerAction("MoveBackward");
    MoveLeft = CreatePlayerAction("MoveLeft");
    MoveAxes = CreateTwoAxisPlayerAction(MoveLeft, MoveRight, MoveBackward, MoveForward);

    //jump
    Jump = CreatePlayerAction("Jump");

    //aiming
    AimUp = CreatePlayerAction("AimUp");
    AimRight = CreatePlayerAction("AimRight");
    AimDown = CreatePlayerAction("AimDown");
    AimLeft = CreatePlayerAction("AimLeft");
    AimAxes = CreateTwoAxisPlayerAction(AimLeft, AimRight, AimDown, AimUp);

    GenerateMyLog = CreatePlayerAction("GenerateMyLog");
    AutoGenerateAllActorLogs = CreatePlayerAction("AutoGenerateAllActorLogs");
  }

  public ActorPlayerControllerBindings CreateDefaultControllerBindings()
  {
    ActorPlayerControllerBindings actorBindings = new ActorPlayerControllerBindings();

    //set up default fire buttons
    actorBindings.FireOne.AddDefaultBinding(InputControlType.LeftTrigger);
    actorBindings.FireOne.AddDefaultBinding(InputControlType.RightTrigger);

    //setup default movement
    actorBindings.MoveForward.AddDefaultBinding(InputControlType.LeftStickUp);
    actorBindings.MoveRight.AddDefaultBinding(InputControlType.LeftStickRight);
    actorBindings.MoveBackward.AddDefaultBinding(InputControlType.LeftStickDown);
    actorBindings.MoveLeft.AddDefaultBinding(InputControlType.LeftStickLeft);

    //set up default aiming
    actorBindings.AimUp.AddDefaultBinding(InputControlType.RightStickUp);
    actorBindings.AimRight.AddDefaultBinding(InputControlType.RightStickRight);
    actorBindings.AimDown.AddDefaultBinding(InputControlType.RightStickDown);
    actorBindings.AimLeft.AddDefaultBinding(InputControlType.RightStickLeft);

    actorBindings.ListenOptions.IncludeUnknownControllers = true;
    actorBindings.ListenOptions.MaxAllowedBindings = 4;

    actorBindings.ListenOptions.OnBindingFound = (action, binding) =>
    {
      if (binding == new KeyBindingSource(Key.Escape))
      {
        action.StopListeningForBinding();
        return false;
      }
      return true;
    };

    actorBindings.ListenOptions.OnBindingAdded += (action, binding) =>
    {
    };

    actorBindings.ListenOptions.OnBindingRejected += (action, binding, reason) =>
    {
    };

    return actorBindings;
  }

  public static ActorPlayerControllerBindings CreateDefaultKeyboardBindings()
  {
    ActorPlayerControllerBindings actorBindings = new ActorPlayerControllerBindings();

    //set up default fire buttons
    actorBindings.FireOne.AddDefaultBinding(Mouse.LeftButton);
    actorBindings.FireTwo.AddDefaultBinding(Mouse.RightButton);

    //setup default movement
    actorBindings.MoveForward.AddDefaultBinding(Key.W);
    actorBindings.MoveRight.AddDefaultBinding(Key.D);
    actorBindings.MoveBackward.AddDefaultBinding(Key.S);
    actorBindings.MoveLeft.AddDefaultBinding(Key.A);

    //setup default aiming
    actorBindings.AimUp.AddDefaultBinding(Mouse.PositiveY);
    actorBindings.AimRight.AddDefaultBinding(Mouse.PositiveX);
    actorBindings.AimDown.AddDefaultBinding(Mouse.NegativeY);
    actorBindings.AimLeft.AddDefaultBinding(Mouse.NegativeX);

    //logging if needed
    actorBindings.GenerateMyLog.AddDefaultBinding(Key.LeftControl, Key.F1);
    actorBindings.AutoGenerateAllActorLogs.AddDefaultBinding(Key.LeftControl, Key.F4);

    return actorBindings;
  }

  public bool AnyKeyWasPressed()
  {
    foreach (PlayerAction action in this.Actions)
    {
      if (action.WasPressed && action != MoveForward && action != MoveLeft && action != MoveRight && action != MoveBackward) return true;
    }
    return false;
  }
}
