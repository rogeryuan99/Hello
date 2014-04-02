using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

class CreatAssart
{
	static string targetDir = "_AssetBunldes";//AssetBunldes
	static void ExecCreateAssetBunldes()
	{
		string extensionName = ".scifiHero";//打包文件后缀名
		
		Object[] SelectedAsset = Selection.GetFiltered(typeof (Object), SelectionMode.DeepAssets);
		
		if(!Directory.Exists(targetDir)) Directory.CreateDirectory(targetDir);
		
		foreach(Object obj in SelectedAsset)
		{
			string targetPath = targetDir + Path.DirectorySeparatorChar + obj.name + extensionName;//存储文件路径
			
			if(File.Exists(targetPath)) File.Delete(targetPath);
			
			if(!(obj is GameObject) && !(obj is Texture2D) && !(obj is Material)) continue;
			
			if(obj is GameObject)
			{
				extensionName = ".prbSH";
			}else if(obj is Texture2D)
			{
				extensionName = ".imageSH";
			}else if(obj is Material)
			{
				extensionName = ".mtlSH";
			}else{
				extensionName = ".sceneSH";
			}
			
			targetPath =  targetDir + Path.DirectorySeparatorChar + obj.name + extensionName;//存储文件路径
			
			//建立 AssetBundle
			if(BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iPhone)){
			
			Debug.Log(obj.name + " Mission completed!!!!");
			
			}else{
			
			Debug.Log(obj.name + " Mission fail!!!!");
			}
		}
	}
	
	static void ExecCreateAssetBunldes_Android()
	{
		string extensionName = ".scifiHero";//打包文件后缀名
		
		Object[] SelectedAsset = Selection.GetFiltered(typeof (Object), SelectionMode.DeepAssets);
		
		if(!Directory.Exists(targetDir)) Directory.CreateDirectory(targetDir);
		
		foreach(Object obj in SelectedAsset)
		{
			string targetPath = targetDir + Path.DirectorySeparatorChar + obj.name + extensionName;//存储文件路径
			
			if(File.Exists(targetPath)) File.Delete(targetPath);
			
			if(!(obj is GameObject) && !(obj is Texture2D) && !(obj is Material)) continue;
			
			if(obj is GameObject)
			{
				extensionName = ".prbSH";
			}else if(obj is Texture2D)
			{
				extensionName = ".imageSH";
			}else if(obj is Material)
			{
				extensionName = ".mtlSH";
			}else{
				extensionName = ".sceneSH";
			}
			
			targetPath =  targetDir + Path.DirectorySeparatorChar + obj.name + extensionName;//存储文件路径
			
			//建立 AssetBundle
			if(BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android)){
			
			Debug.Log(obj.name + " Mission completed!!!!");
			
			}else{
			
			Debug.Log(obj.name + " Mission fail!!!!");
			}
		}
	}
	
	[MenuItem("Build Assets/Create AssetBunldes")]
	static void buildAssets()
	{
		targetDir = "_AssetBunldes";
		ExecCreateAssetBunldes();
	}
	
	[MenuItem("Build Assets/Create Android Asset Bundles")]
	static void buildAssets_Android()
	{
		targetDir = "_AssetBundles_Android";
		ExecCreateAssetBunldes_Android();
	}
	
	[MenuItem("Build Assets/Create itouch4 AssetBunldes")]
	static void buildTouch4Assets()
	{
		targetDir = "itouch4_AssetBunldes";//AssetBunldes目录
		ExecCreateAssetBunldes();
	}
}