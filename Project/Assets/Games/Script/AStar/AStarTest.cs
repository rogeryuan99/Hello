using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class AStarTest : MonoBehaviour {

	/*public BarrierMapData data;
	
	private Point S = new Point();
	private Point E = new Point();
	
	string starPos = "x,y";
	string endPos = "x,y";
	string trnsPos = "0,0";
	
	public void Start(){
		data = gameObject.AddComponent<BarrierMapData>();
	}
	
	public void OnGUI(){
		GUILayout.BeginArea(new Rect(200,300,150,150));
		
		UiStartAndEndInput();
		UiLoadMapInfo();
		UiBeginTest();
		UiCalculateGrid();
		UiTranslatePosInput();
		UiTranslatePosToIndex();
		
		GUILayout.EndArea();
	}
	
	private void UiStartAndEndInput(){
		GUILayout.BeginHorizontal();
			GUILayout.Label("starPos");
			starPos = GUILayout.TextField(starPos);
			GUILayout.Label("endPos");
			endPos = GUILayout.TextField(endPos);
		GUILayout.EndHorizontal();
	}
	
	private void UiLoadMapInfo(){
		if (GUILayout.Button("Load_Kyln_1")){
			LoadMapBarrierInfo("Kyln_1");
			starPos = "2,0";
			endPos = "2,8";
		}
		if (GUILayout.Button("Load_Kyln_2")){
			LoadMapBarrierInfo("Kyln_2");
			starPos = "0,0";
			endPos = "8,8";
		}
	}
	
	private void UiBeginTest(){
		if (GUILayout.Button("Test")){
			data.GetPath(Point.Parse(starPos), Point.Parse(endPos));
		}
	}
	
	private void UiCalculateGrid(){
		if (GUILayout.Button("CalculateGrid")){
			data.CalculateGrid();
			data.ShowGrid();
		}
	}
	
	
	private void UiTranslatePosInput(){
		GUILayout.BeginHorizontal();
			GUILayout.Label("trnsPos");
			trnsPos = GUILayout.TextField(trnsPos);
		GUILayout.EndHorizontal();
	}
	
	private void UiTranslatePosToIndex(){
		if (GUILayout.Button("Tanslate")){
			string[] strs = trnsPos.Split(',');
			Vector2 pos = new Vector2(float.Parse(strs[0]), float.Parse(strs[1]));
			
			trnsPos = data.TranslatePosToIndex(pos).ToString();
		}
	}
	
	private void LoadMapBarrierInfo(string fileName){
		data.Load(fileName);
		data.PrintMap();
	}*/
}
