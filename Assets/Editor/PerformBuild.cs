// C# example
using UnityEditor;
using System.IO;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class PerformBuild
{
	
	static string buildFolder = "Builds";
	static string apkName = "Icescape.apk";
	
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
	
	static void CommandLineBuildAndroid ()
	{
		Debug.Log("Command line build android version\n------------------\n------------------");
		
		string[] scenes = GetBuildScenes();
		string path = buildFolder + "/" + apkName;
		if(scenes == null || scenes.Length==0 || path == null)
			return;
		
		Debug.Log(string.Format("Path: \"{0}\"", path));
		for(int i=0; i<scenes.Length; ++i)
		{
			Debug.Log(string.Format("Scene[{0}]: \"{1}\"", i, scenes[i]));
		}

		PlayerSettings.Android.useAPKExpansionFiles = false;
		
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
	
	static Regex sceneNameRgx = new Regex(@"(?<=/)([^/]+)(?=.unity)", RegexOptions.IgnoreCase);

	static Regex removeNonCharactersRgx = new Regex("[^a-zA-Z]");

	[UnityEditor.MenuItem("Build/Build Test Release")]
	static void FinalBuild(){
		System.IO.Directory.CreateDirectory (buildFolder);

		EditorApplication.SaveScene();
		
		string[] scenes = GetBuildScenes();
		string path = buildFolder + "/TestRelease.apk";
		if(scenes == null || scenes.Length==0 || path == null)
			return;
		
		Debug.Log(string.Format("Path: \"{0}\"", path));
		for(int i=0; i<scenes.Length; ++i)
		{
			Debug.Log(string.Format("Scene[{0}]: \"{1}\"", i, scenes[i]));
		}
		
		PlayerSettings.Android.useAPKExpansionFiles = false;

		string bundleIdent = PlayerSettings.bundleIdentifier;
		PlayerSettings.bundleIdentifier = "com.prototype.game2test";
		
		string pName = PlayerSettings.productName;
		PlayerSettings.productName = "Test Minigame 2";
		
		PlayerSettings.keystorePass = "pjaskevand";
		PlayerSettings.keyaliasPass = "534231";

        UnityException exc = null;

        try
        {
			BuildPipeline.BuildPlayer(scenes, path, BuildTarget.Android, BuildOptions.None);
		} catch(UnityException e){
			exc = e;
		}

		PlayerSettings.bundleIdentifier = bundleIdent;
		PlayerSettings.productName = pName;

		EditorApplication.SaveScene();

		if (exc != null)
			throw exc;

		System.Diagnostics.Process.Start (System.IO.Directory.GetCurrentDirectory() + "/" + buildFolder + "/");
	}

	[UnityEditor.MenuItem("Build/Prototype From Open Scene")]
	static void PrototypeBuild ()
	{

		EditorApplication.SaveScene();

		string sceneToBuild = "MainMenu";
		string sceneToBuildPath = "Assets/Scenes/MainMenu.unity";
		
		sceneToBuildPath = EditorApplication.currentScene;
		sceneToBuild = sceneNameRgx.Match (sceneToBuildPath).Value;
		
		System.IO.Directory.CreateDirectory (buildFolder);
		
		PlayerSettings.keystorePass = "pjaskevand";
		PlayerSettings.keyaliasPass = "534231";
		
		bool obbFiles = PlayerSettings.Android.useAPKExpansionFiles;
		PlayerSettings.Android.useAPKExpansionFiles = false;
		
		string bundleIdent = PlayerSettings.bundleIdentifier;
		PlayerSettings.bundleIdentifier = "com.prototype." + removeNonCharactersRgx.Replace(sceneToBuild, "");
		
		string pName = PlayerSettings.productName;
		PlayerSettings.productName = sceneToBuild;

		UnityException exc = null;

		Debug.Log ("Building prototype with scene: " + sceneToBuild);
		try{
			BuildPipeline.BuildPlayer(new string[]{sceneToBuildPath}, buildFolder + "/" + sceneToBuild + ".apk", BuildTarget.Android, BuildOptions.None);
		} catch(UnityException e){
			exc = e;
		}

		PlayerSettings.Android.useAPKExpansionFiles = obbFiles;
		PlayerSettings.bundleIdentifier = bundleIdent;
		PlayerSettings.productName = pName;

		EditorApplication.SaveScene();

		if (exc != null)
			throw exc;

		System.Diagnostics.Process.Start (System.IO.Directory.GetCurrentDirectory() + "/" + buildFolder + "/");
	}
}