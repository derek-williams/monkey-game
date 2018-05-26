/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this Code Monkey project
    I hope you find it useful in your own projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class EditorCompileTimer : MonoBehaviour
{

  private double compileStartTime;
  private bool isCompiling = false;

  #if UNITY_EDITOR
    private void Update()
    {
      if (isCompiling)
      {
        if (!EditorApplication.isCompiling)
        {
          isCompiling = false;
          CompileFinished();
        }
      }
      else
      {
        if (EditorApplication.isCompiling)
        {
          isCompiling = true;
          CompileStarted();
        }
      }
    }

    private void CompileStarted()
    {
      Debug.Log("Compile started...");
      compileStartTime = EditorApplication.timeSinceStartup;
    }

    private void CompileFinished()
    {
      double compileTime = EditorApplication.timeSinceStartup - compileStartTime;
      Debug.Log("Compile Finished: " + compileTime.ToString("F2") + "s");
    }
  #endif
}
