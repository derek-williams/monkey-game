using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;


public static class LogHelper
{
  public static string GenerateTransformMessage(Transform t)
  {
    string message = "Transform Information: \n";

    message += string.Format("Position: {0}\n", t.position.ToString());
    message += string.Format("Rotation: {0}\n", t.rotation.ToString());

    return message;
  }

  public static void GenerateActorLogFile(Actor actor, string additionalFileNameInfo = null)
  {
    string dateTime = DateTime.Now.ToString("MM-dd-yyyy HH.mm");
    string outputPath = System.IO.Directory.GetCurrentDirectory() + "/LogFiles";


    if (!Directory.Exists(outputPath))
    {
      Directory.CreateDirectory(outputPath);
    }

    string message = "";//TODO

    if (additionalFileNameInfo != null)
    {
      System.IO.File.WriteAllText(outputPath + "/" + additionalFileNameInfo + " " + actor.name + " @ " + dateTime + ".txt", message);
    }
    else
    {
      System.IO.File.WriteAllText(outputPath + "/" + actor.name + " @ " + dateTime + ".txt", message);
    }
  }

  public static void GenerateActorLogFiles(List<Actor> actors, string additionalFolderNameInfo = null, string additionalFileNameInfo = null)
  {
    string dateTime = DateTime.Now.ToString("MM-dd-yyyy HH.mm");
    string outputPath = System.IO.Directory.GetCurrentDirectory() + "/LogFiles";
    if (!Directory.Exists(outputPath))
    {
      Directory.CreateDirectory(outputPath);
    }

    if (additionalFolderNameInfo != null)
    {
      outputPath += "/" + additionalFolderNameInfo + " " + dateTime;
    }
    else
    {
      outputPath += "/" + dateTime;
    }

    if (!Directory.Exists(outputPath))
    {
      Directory.CreateDirectory(outputPath);
    }

    foreach (Actor actor in actors)
    {
      string message = "";//TODO

      if (additionalFileNameInfo != null)
      {
        System.IO.File.WriteAllText(outputPath + "/" + additionalFileNameInfo + " " + actor.name + " @ " + dateTime + ".txt", message);
      }
      else
      {
        System.IO.File.WriteAllText(outputPath + "/" + actor.name + " @ " + dateTime + ".txt", message);
      }
    }
  }
}

