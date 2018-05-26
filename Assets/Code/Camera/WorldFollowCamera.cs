using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasyEditor;

public class WorldFollowCamera : MonoBehaviour
{
  [HideInInspector]
  public Camera attachedCamera;

  [Space(10)]
  [Header("Camera Positioning")]
  public float MovementSpeed = 1f;
  public Vector3 TargetRotation;

  public List<CameraTarget> Targets = new List<CameraTarget>();

  [Space(10)]
  [Header("Zoom")]
  public float ZoomSpeed = 1f;
  public float ZoomThreshold = .2f;
  public float ZoomMin = 3f;
  public float ZoomMax = 10f;
  private float CurrentZoom = 1f;


  private static WorldFollowCamera instance;
  public static WorldFollowCamera Instance
  {
    get
    {
      if (instance == null)
      {
        instance = GameObject.FindObjectOfType<WorldFollowCamera>();
      }
      return instance;
    }
  }


  // Use this for initialization
  void Start()
  {
    attachedCamera = GetComponent<Camera>();

    CurrentZoom = ZoomMax;
  }

  // Update is called once per frame
  void LateUpdate()
  {
    FollowTargets();
  }

  private Vector3 AveragePosition;
  private Vector3 TargetPosition;
  public void FollowTargets()
  {
    if (Targets == null || Targets.Count <= 0)
    {
      return;
    }

    //calculate average position
    AveragePosition = Vector3.zero;
    foreach(CameraTarget target in Targets)
    {
      AveragePosition += target.transform.position * target.weight;
    }

    EvaluateZoom();

    TargetPosition = AveragePosition - Quaternion.Euler(TargetRotation) * (Vector3.forward * CurrentZoom);
    transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * MovementSpeed);

    transform.eulerAngles = TargetRotation;

  }

  public void EvaluateZoom()
  {
    //Check ScreenSpace of Targets
    bool zoomOut = false;
    bool zoomIn = false;
    foreach (CameraTarget target in Targets)
    {
      Vector2 viewportPoint = attachedCamera.WorldToViewportPoint(target.transform.position);

      if (Mathf.Abs(viewportPoint.x) > (1 - ZoomThreshold))
      {
        zoomOut = true;
      }

      if (Mathf.Abs(viewportPoint.y) > (1 - ZoomThreshold))
      {
        zoomOut = true;
      }

      if (Mathf.Abs(viewportPoint.x) < (1 - ZoomThreshold))
      {
        zoomIn = true;
      }

      if (Mathf.Abs(viewportPoint.y) < (1 - ZoomThreshold))
      {
        zoomIn = true;
      }
    }

    if (zoomOut == true)
    {
      ZoomOut();
    }
    else if (zoomIn == true)
    {
      ZoomIn();
    }
  }

  public void ZoomOut()
  {
    if(CurrentZoom < ZoomMax)
    {
      CurrentZoom = Mathf.Lerp(CurrentZoom, CurrentZoom + ZoomSpeed, Time.deltaTime);
    }
    else
    {
      CurrentZoom = ZoomMax;
    }
  }

  public void ZoomIn()
  {
    if (CurrentZoom > ZoomMin)
    {
      CurrentZoom = Mathf.Lerp(CurrentZoom, CurrentZoom - ZoomSpeed, Time.deltaTime);
    }
    else
    {
      CurrentZoom = ZoomMin;
    }
  }

  public void AddTarget(Transform target, float weight)
  {
    if(Targets.Where(x => x.transform == target).Count() == 0)
    {
      Targets.Add(new CameraTarget(target, weight));
    }
  }

  public void RemoveTarget(Transform target)
  {
    Targets.RemoveAll(x => x.transform == target);
  }
}

[System.Serializable]
public class CameraTarget
{
  public CameraTarget(Transform pTarget, float pWeight)
  {
    transform = pTarget;
    weight = pWeight;
  }

  public Transform transform;
  public float weight;
}
