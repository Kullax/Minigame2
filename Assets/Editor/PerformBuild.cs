﻿// C# example
using UnityEditor;
using System.IO;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

class PerformBuild
{
	static string[] GetBuildScenes()
	{
		List<string> names = new List<string>();
		
		foreach(EditorBuildSettingsScene e in EditorBuildSettings.scenes)
		{
			if(e==null)
				continue;
			
			if(e.enabled)
				names.Add(e.path);
		}
		return names.ToArray();
	}
	
	static string GetBuildPathAndroid()
	{
		return "Builds/Icescape.apk";
	}
	
	[UnityEditor.MenuItem("CUSTOM/Test Command Line Build Step Android")]
	static void CommandLineBuildAndroid ()
	{
		Debug.Log("Command line build android version\n------------------\n------------------");
		
		string[] scenes = GetBuildScenes();
		string path = GetBuildPathAndroid();
		if(scenes == null || scenes.Length==0 || path == null)
			return;
		
		Debug.Log(string.Format("Path: \"{0}\"", path));
		for(int i=0; i<scenes.Length; ++i)
		{
			Debug.Log(string.Format("Scene[{0}]: \"{1}\"", i, scenes[i]));
		}

		PlayerSettings.keystorePass = "pjaskevand";
		PlayerSettings.keyaliasPass = "534231";

		string[] args = System.Environment.GetCommandLineArgs ();
		for (int i = 0; i < args.Length; i++) {
			//Debug.Log("############################# - arg: " + i + " value: " + args[i]);
			if(args[i].Equals("-bversion")){
				PlayerSettings.Android.bundleVersionCode = int.Parse(args[i+1]);
				PlayerSettings.bundleVersion = args[i+1];
			}
		}
		
		Debug.Log("Starting Android Build!");
		BuildPipeline.BuildPlayer(scenes, path, BuildTarget.Android, BuildOptions.None);
	}
}