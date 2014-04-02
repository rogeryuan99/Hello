using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class RogerDragBodyPart : ScriptableWizard {
 
    [UnityEditor.MenuItem("Tools/RogerDragBodyPart")]
    static void UpdatePrefabs()
    {
        ScriptableWizard.DisplayWizard<RogerDragBodyPart >("RogerDragBodyPart", "No Select,Crash!! GO");
    }
    
    private Dictionary<string,GameObject> prefabItems;
    private Dictionary<string,GameObject> sceneItems;
    private int foundToChange;
    private int changeSuccess;
    private int changeFailed;
    
    void OnWizardCreate () 
    {
        Debug.Log("Replace all selected scene prefabs to library prefabs.");
        foundToChange = changeSuccess = changeFailed = 0;
        bool doContinue = loadLibraryAssets();
        if( doContinue ) doContinue = loadSceneAsset();
		
//        if( doContinue )
//        {
//            foreach (KeyValuePair<string, GameObject> pair in sceneItems)
//            {
//                GameObject sceneItem = pair.Value;
//                GameObject libraryPrefab;
//                if( prefabItems.ContainsKey( pair.Key ) )
//                {
//                    changeSuccess++;
//                    libraryPrefab = prefabItems[ pair.Key ];
//                    PrefabUtility.ReplacePrefab( sceneItem, libraryPrefab );
//                }
//                else
//                {
//                    changeFailed++;
//                    Debug.LogWarning("Prefab Warning: Scene item '"+pair.Key+"' has no prefab");
//                }
//            }
//        }
//        
//        EditorUtility.DisplayDialog ("Completed", "Replaced '"+changeSuccess+"' prefabs from selected scene items.", "Close");
//        Debug.Log("Looked thru '"+foundToChange+"', replaced '"+changeSuccess+"', errors '"+changeFailed+"'");
    }
    
    private bool loadLibraryAssets()
    {
		try{
        prefabItems = new Dictionary<string, GameObject>();
        //string[] list = AssetDatabase.GetAllAssetPaths();
        foreach(GameObject item in Selection.gameObjects)
        {
            
                string name = item.name;
                if( prefabItems.ContainsKey(name) )
                {
                    Debug.LogWarning("Asset already exist, duplicate?: "+ name );
                    if( EditorUtility.DisplayDialog ("Warning", "Prefab '"+name+"' is already saved, must be unique names!", "Continue", "Abort") )
                    {
                        return false;
                    }
                }
				
			Debug.Log(" prefab:"+name);
			
                prefabItems[ name ] = item;

        }
        return true;
		}catch{
			Debug.LogError("selection error");
			return false;
		}
    }
    
    private bool loadSceneAsset()
    {
		Debug.Log("loadSceneAsset");
        sceneItems = new Dictionary<string, GameObject>();
        PieceAnimation body =  GameObject.FindObjectOfType(typeof(PieceAnimation)) as PieceAnimation; 
		Debug.Log("body:"+body);
		System.Type type = body.GetType();
		Debug.Log("type:"+type);
		System.Reflection.FieldInfo[] fis = type.GetFields();
        foreach (System.Reflection.FieldInfo fi in fis)
        {
			if(fi.FieldType == typeof(GameObject)){
				Debug.Log("  - filed"+fi.Name);
				if(prefabItems.ContainsKey(fi.Name)){
					Debug.Log("===== got"+fi.Name);
					fi.SetValue(body,prefabItems[fi.Name]);	
				}
			}
		}
		
		
		
		return false;
//		
//		
//        if( list.Length == 0 ) {
//            EditorUtility.DisplayDialog("Warning", "You must select Prefab connected GameObjects in Hierachy window!", "Abort" );
//            return false;
//        }
//        
//        foreach( GameObject obj in list )
//        {
//            // make sure we have root
//            GameObject item = PrefabUtility.FindPrefabRoot(obj);
//            string name = item.name;
//            if( !sceneItems.ContainsKey(name) )
//            {
//                foundToChange++;
//                sceneItems[ name ] = item;
//				Debug.Log("sceneItem:"+name);
//            }
//        }
//        return true;
    }
    
}