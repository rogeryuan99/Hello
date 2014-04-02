using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;


class RogerCustomUtils : EditorWindow {
	
	public TextAsset ta;
	
	[MenuItem ("Tools/Roger Custom Editor %R")]
    static void ShowWindow () {
        RogerCustomUtils win = (RogerCustomUtils)EditorWindow.GetWindow (typeof(RogerCustomUtils));
    }

	void OnGUI ()
	{
		GUILayout.BeginVertical();
		
		
		
		if (GUILayout.Button ("save1 down")) 
		{
//			TextAsset newFont = AssetDatabase.LoadMainAssetAtPath("Assets/Games/Fonts/Adobe_Heiti.txt") as TextAsset;
//			Material fontMaterial = AssetDatabase.LoadMainAssetAtPath("Assets/Games/Fonts/Adobe_Heiti.mat") as Material;
//				
//			
//			foreach (Object obj in GameObject.FindObjectsOfType(typeof(SpriteText)))
//			{
//				SpriteText region = (SpriteText)obj;
//				
//				Debug.Log(region.name);
////				GameObject g = region.transform.parent.gameObject;
////				while(true)
////				{
////					if(g.transform.parent == null)
////					{
////						Debug.Log(g.name);
////						break;
////					}
////					g = g.transform.parent.gameObject;
////					if(g.transform.parent == null)
////					{
////						Debug.Log(g.name);
////						break;
////					}	
////					
////				}
//				
//				region.SetFont(newFont, fontMaterial);
//				region.font = newFont;
//			}
		}
		if (GUILayout.Button ("PlayerPrefs.DeleteAll")) 
		{
			PlayerPrefs.DeleteAll();
		}
		if (GUILayout.Button ("activeGameObject")) 
		{
			GameObject a = Selection.activeObject as GameObject;
			a.SetActiveRecursively(!a.activeSelf);
		}
		GUILayout.EndVertical();
		
	}
}