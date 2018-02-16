using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using System.Linq;

public class BuildTools
{
	public static string[] GetScenes()
	{
		var scenes = from scene in EditorBuildSettings.scenes
								 where scene.enabled == true
								 select scene.path;

		return scenes.ToArray();

	}

	[MenuItem("Build/Standalone/Windows Player/Client")]
	public static void BuildWindowsStandalonePlayer()
	{
		build(GetScenes(), BuildTarget.StandaloneWindows, "/StandaloneWindows/Client/", ".exe");
	}

	[MenuItem("Build/Standalone/Mac Player/Client")]
	public static void BuildMacStandalonePlayer()
	{
		build(GetScenes(), BuildTarget.StandaloneOSX, "/StandaloneMac/Client/", "");
	}

	[MenuItem("Build/Mobile/Android/Client")]
	public static void BuildMobileAndroidPlayer()
	{
		int p4Number = 1;
		int.TryParse(System.Environment.GetEnvironmentVariable("P4_CHANGELIST"), out p4Number);

		PlayerSettings.Android.bundleVersionCode = p4Number;

		build(GetScenes(), BuildTarget.Android, "/Android/Client/", ".apk");
	}

	[MenuItem("Build/Mobile/iOS/Client")]
	public static void BuildMobileiOSPlayer()
	{
		build(GetScenes(), BuildTarget.iOS, "/iPhone/Client/", "", false);
	}

	private static void build(string[] levels, BuildTarget buildTarget, string deployPath, string ext, bool debug = true)
	{
		var buildOptions = debug ? (BuildOptions.AllowDebugging | BuildOptions.Development) : BuildOptions.None;

		var productName = "kingdom";
		try
		{
			PlayerSettings.bundleVersion = "0." + System.Environment.GetEnvironmentVariable("P4_CHANGELIST");
		}
		catch (System.Exception)
		{
			PlayerSettings.bundleVersion = "0xd3adb33f";
		}
		

		var buildPath = Application.dataPath + "/../../Build" + deployPath + productName.Replace(" ", "") + ext;

		EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);
		string errors = BuildPipeline.BuildPlayer(levels, buildPath, buildTarget, buildOptions);
	}
}

