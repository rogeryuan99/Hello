using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;


class RogerMapGen : EditorWindow {
	
	public TextAsset ta;
	
	[MenuItem ("Tools/RogerMapGen %R")]
    static void ShowWindow () {
        RogerMapGen win = (RogerMapGen)EditorWindow.GetWindow (typeof(RogerMapGen));
    }
	
	private int DX = 20;
	private int DY = 15;
	
	private List<Vector2> samplePoints = new List<Vector2>(){new Vector2(.2f,.2f),new Vector2(.2f,.8f),new Vector2(.8f,.2f),new Vector2(.8f,.8f),new Vector2(.5f,.5f)};
	void OnGUI ()
	{
		GUILayout.BeginVertical();
		Texture2D map = Selection.activeObject as Texture2D;
		GUILayout.Label(string.Format("Map Grids: {0}x{1}",DX,DY));	
		GUILayout.Label(map==null? "no texture selected":map.name);	
		if (GUILayout.Button ("Read Pixel")) 
		{
			if(map == null) return;
			float stepX = map.width / (float) DX;
			float stepY = map.height / (float) DY;
			string all = "";
			for(int y = DY-1;y>=0;y--){
				//List<Color> line = new List<Color>();
				string s = "";
				for(int x = 0;x<DX;x++){
					Color average = Color.black;
					foreach(Vector2 samplePoint in samplePoints){
						average += map.GetPixel((int)((x+samplePoint.x)*stepX), (int)((y+samplePoint.y)*stepY)) / (float)samplePoints.Count;
					}
					if(average.r>0.5f){
						s+="0";	
					}else if(average.g>0.5f){
						s+="1";	
					}
					//line.Add(average);
				}
				Debug.Log(s);		
				all+= s+"\r\n";
			}
			File.WriteAllText(Application.dataPath+"/MapGen/"+map.name+".txt", all);
        	AssetDatabase.Refresh();

		}
		GUILayout.EndVertical();
		
	}
}