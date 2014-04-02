#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class BuildScript_lavaspark : MonoBehaviour
{
	static string[] scenes = { "Assets/Games/Scenes/initGameResource.unity",
		"Assets/Games/Scenes/UIMain.unity",
		"Assets/Games/Scenes/NormalLevel.unity",
		"Assets/Games/Scenes/OpenAnim.unity" };
	static string[] scenes_download = { "Assets/Scenes/xxx.unity","Assets/Scenes/xxx.unity" };
	public const string ANDROID_BUNDLE	= "com.lavaspark.gog";
	public const string IOS_BUNDLE_LAVASPARK	= "com.lavaspark.gog";
	public const string IOS_BUNDLE_DISNEY	= "com.marvel.guardiansofthegalaxy";
	public const string IOS_BUNDLE_EXEC3	= "com.marvel2.guardiansofthegalaxy2";
	public const string IOS_BUNDLE_PAQA	= "com.marvel3.guardiansofthegalaxy3";

    [MenuItem("Build/Build android/lavaspark")]
    static void PerformAndroidBuid()
    {
		PerformAndroidBuild("GotG",ANDROID_BUNDLE,BuildOptions.None);
    }

	[MenuItem("Build/Build iOS/Lavaspark")]
    static void PerformIOS_Lavaspark()
    {
        PerformIOSBuild("GotG_lavaspark",IOS_BUNDLE_LAVASPARK,GetModeOptions());
    }
    [MenuItem("Build/Build iOS/Disney")]
    static void PerformIOS_Disney()
    {
        PerformIOSBuild("GotG_disney",IOS_BUNDLE_DISNEY,GetModeOptions());
    }
    
    [MenuItem("Build/Build iOS/Exec3")]
    static void PerformIOS_Exec3()
    {
        PerformIOSBuild("GotG_exec3",IOS_BUNDLE_EXEC3,GetModeOptions());
    }
    
    [MenuItem("Build/Build iOS/PAQA")]
    static void PerformIOS_PAQA()
    {
		//if(!CopyAndGenerateSettingsFiles("staging2"))	{	return;	}
        PerformIOSBuild("GotG_paqa",IOS_BUNDLE_PAQA,GetModeOptions());
    }
	
	
    static void PerformIOSBuild(string buildPath, string bundleID=null, BuildOptions options=BuildOptions.None)
    {
		string cacheBundleID		= PlayerSettings.bundleIdentifier;
		if(!string.IsNullOrEmpty(bundleID))
		{	PlayerSettings.bundleIdentifier	= bundleID;	}
		
		if(EditorUserBuildSettings.activeBuildTarget != BuildTarget.iPhone)
		{
			UnityEngine.Debug.LogError("Platform error, stop build");
		}
        if (!Directory.Exists(buildPath)) {
            Directory.CreateDirectory(buildPath);
        }
        BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.iPhone, options);
		
		PlayerSettings.bundleIdentifier = cacheBundleID;
    }

	static bool CopyAndGenerateSettingsFiles(string serverBank)
    {
		return true;
    }

	static bool CheckProcceed(BuildTarget expectedTarget)
	{
		if(UnityEditorInternal.InternalEditorUtility.inBatchMode)
		{
			if(EditorUserBuildSettings.activeBuildTarget != expectedTarget)
			{
				UnityEngine.Debug.LogError("BuildScript: Error: Unexpected Build Target In batch Mode: "
					+ EditorUserBuildSettings.activeBuildTarget + " Expected: " + expectedTarget );
			}
			return true;
		}

		if(EditorUserBuildSettings.activeBuildTarget != expectedTarget)
		{
//			if(EditorUtility.DisplayDialogComplex("Platform Warning", "Warning Wrong Platform!\nCurrent: " 
//				+ EditorUserBuildSettings.activeBuildTarget.ToString()
//				+ "\nNew Target: " + expectedTarget.ToString(), "Switch Platform", "Cancel", null) == 0)
//			{
//				EditorUserBuildSettings.SwitchActiveBuildTarget(expectedTarget);
//				AssetDatabase.Refresh();
//				return true;
//			}
			UnityEngine.Debug.LogError("Error platform,stop build");
			return false;
		}
		return true;
	}


	private static UnityEditor.BuildOptions GetModeOptions()
	{
		return BuildOptions.None;
	}
	
	static void PerformAndroidBuild(string buildPath, string bundleID = null,BuildOptions options=BuildOptions.None)
    {
        //string buildPath = "android_Staging";
		if(EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
		{
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
			AssetDatabase.Refresh();
		}
		
		try
		{	
			string path	= Application.dataPath + "/../"+buildPath+"/";
			if(Directory.Exists(path))
			{	Directory.Delete(path);	}
			
		}catch{	}
		
		try
		{	
			string path	= Application.dataPath + "/../"+buildPath+".apk";
			if(File.Exists(path))
			{	File.Delete(path);	}
		}catch{	}

		bool useAPKFIles	= PlayerSettings.Android.useAPKExpansionFiles;
		PlayerSettings.Android.useAPKExpansionFiles	= false;

		string cacheBundleID		= PlayerSettings.bundleIdentifier;
		if(!string.IsNullOrEmpty(bundleID))
		{	PlayerSettings.bundleIdentifier	= bundleID;	}

		//options |= BuildOptions.AutoRunPlayer;
        BuildPipeline.BuildPlayer(scenes, buildPath+".apk", BuildTarget.Android, options);

		PlayerSettings.bundleIdentifier	= cacheBundleID;
		PlayerSettings.Android.useAPKExpansionFiles	= useAPKFIles;
    }
	
	static void PerformAndroidDownloadBuild(BuildOptions options=BuildOptions.None, string bundleID = null)
    {
		if(EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
		{
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
			AssetDatabase.Refresh();
		}
		
		string buildPath = "android_Staging.apk";
		try
		{	
			string path	= Application.dataPath + "/../android_Staging/";
			if(Directory.Exists(path))
			{	Directory.Delete(path);	}
			
		}catch{	}
		
		try
		{	
			string path	= Application.dataPath + "/../android_Staging.apk";
			if(File.Exists(path))
			{	File.Delete(path);	}
		}catch{	}

		bool useAPKFIles	= PlayerSettings.Android.useAPKExpansionFiles;
		PlayerSettings.Android.useAPKExpansionFiles	= true;
		string cacheBundleID		= PlayerSettings.bundleIdentifier;
		if(!string.IsNullOrEmpty(bundleID))
		{	PlayerSettings.bundleIdentifier	= bundleID;	}

		//options |= BuildOptions.AutoRunPlayer;
        BuildPipeline.BuildPlayer(scenes_download, buildPath, BuildTarget.Android, options);

		PlayerSettings.bundleIdentifier	= cacheBundleID;
		PlayerSettings.Android.useAPKExpansionFiles	= useAPKFIles;
    }

	static bool CopyAndroidFiles(string folderName)
    {
		if(!CheckProcceed(BuildTarget.Android))
		{	return false;	}

//		JSONBlobber.Initialize(true, null);
		
		string path	= "Scripts/conf/" + folderName + Path.DirectorySeparatorChar + "ProjectSettings.asset";
		UnityEngine.Debug.Log("path "+"Assets/"+path);
		UnityEngine.Debug.Log("pathto "+"ProjectSettings" + Path.DirectorySeparatorChar + "ProjectSettings.asset");
//		File.Copy("Scripts/conf/" + folderName + Path.DirectorySeparatorChar + "ProjectSettings.asset",
//			"ProjectSettings" + Path.DirectorySeparatorChar + "ProjectSettings.asset", true);
		
//		if(File.Exists(path))
//		{	File.Copy(path, "ProjectSettings" + Path.DirectorySeparatorChar + "ProjectSettings.asset", true);	}ue);

		string file_path	= "Assets" + Path.DirectorySeparatorChar + "StreamingAssets" + Path.DirectorySeparatorChar + "global_settings.json";
//		if(File.Exists(file_path))
//		{	File.Copy("Scripts/conf/" + folderName + Path.DirectorySeparatorChar + "global_settings.json", file_path, true);	}

		file_path	= "Assets" + Path.DirectorySeparatorChar + "StreamingAssets" + Path.DirectorySeparatorChar + "server_settings.json";
		if(File.Exists(file_path))
		{	File.Copy("Scripts/conf/" + folderName + Path.DirectorySeparatorChar + "server_settings.json", file_path, true);	}

//		string jsonVersionPath	= "Assets" + Path.DirectorySeparatorChar + "StreamingAssets" 
//		                  + Path.DirectorySeparatorChar + "android_version.json";
		/*if(File.Exists(jsonVersionPath))
		{	File.Delete(jsonVersionPath);	}
		string version_data	= "{\n\"bundle_identifier\": \"" + PlayerSettings.bundleIdentifier + "\",\n"
		                      + "\"bundle_version\": \"" + PlayerSettings.bundleVersion + "\",\n"
		                      + "\"bundle_short_version\": \"" + PlayerSettings.bundleVersion + "\"\n}";
		System.IO.File.WriteAllText(jsonVersionPath, version_data);*/
		
		
//lavaspark add start
		string P31RestKit = "Assets/Plugins/P31RestKit.dll";
		string newP31RestKit = "Scripts/conf/" + folderName + Path.DirectorySeparatorChar + "P31RestKit.dll";
		if(File.Exists(newP31RestKit)){
			File.Copy(newP31RestKit,P31RestKit,true);
		}
		if(!Directory.Exists("Assets/Editor/Prime31")){
			Directory.CreateDirectory("Assets/Editor/Prime31");
		}
		string[] p31files = new string[3]{"P31MenuItem.dll","P31MenuItem.sln","Prime31MenuItem.dll"};
		for (int i=0; i<p31files.Length; i++)
        {
			string p31file = "Scripts/conf/" + folderName +"/Prime31/" + p31files[i];
			if(File.Exists(p31file)){
				File.Copy(p31file, "Assets/Editor/Prime31/" + p31files[i],true);
			}
        }
		
		AssetDatabase.Refresh(ImportAssetOptions.Default);
//lavaspark add end

		string serverBank	= "invalid";
		if(folderName.Contains("staging2"))
		{	serverBank	= "staging2";	}
		else if(folderName.Contains("live"))
		{	serverBank	= "live";	}
		else if(folderName.Contains("local"))
		{	serverBank	= "local";	}
		else if(folderName.Contains("qa"))
		{	serverBank	= "qa";	}
		
		 Process proc = new Process()
        {
			StartInfo = new ProcessStartInfo(@"python", "Scripts"+Path.DirectorySeparatorChar+"build_manifest.py --out Assets/StreamingAssets/manifest.json " + serverBank )
            {
                RedirectStandardOutput = true,
                RedirectStandardError= true,
                UseShellExecute = false,
            }
        };
        proc.Start();
        proc.WaitForExit();
        string line;
        while ((line = proc.StandardError.ReadLine()) != null)
        {
            UnityEngine.Debug.Log(line);
        }
        UnityEngine.Debug.Log("Python exited with code " + proc.ExitCode);
		return true;
    }
}

#endif
