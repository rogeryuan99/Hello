using UnityEngine;
using System.Collections;
using System.Xml;

public class TestTutorial : MonoBehaviour {
	string secretParms = "{0}Title{1}Description{2}headIcon";
	string parmsResult = string.Empty;
	string xmlFirstChildName = string.Empty;
	string xmlReader = string.Empty;
	
	void Start(){
		StartCoroutine(delayedStart());
	}	
	IEnumerator delayedStart(){
		Debug.LogError("aaaaaa4");
		yield return new WaitForSeconds(0.1f);	
		TsTheater.Instance.PlayPart("Start");
		Debug.LogError("aaaaaa5");
	}
	public void OnGUI(){
		GUILayout.BeginArea(new Rect(0,0,500,500));
		GUILayout.BeginVertical();
		
		// TestParmsTranslator();
		// CheckFirstChildName();
		// TestXmlReader();
		TestCreation();
			
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
	public void TestParmsTranslator(){
		GUILayout.BeginHorizontal();
		secretParms = GUILayout.TextField(secretParms);
		if (GUILayout.Button("Translate")){
			string[] parms = TsParmsTranslator.Translate(secretParms);
			parmsResult = string.Empty;
			for (int i=0; i<parms.Length; i++){
				if (!string.IsNullOrEmpty(parmsResult)){
					parmsResult += ", ";
				}
				parmsResult += parms[i];
			}
		}
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal("output");
		GUILayout.Label(parmsResult);
		GUILayout.EndHorizontal();
	}	
	public void CheckFirstChildName(){
		GUILayout.BeginHorizontal();
		GUILayout.Label(xmlFirstChildName);
		if (GUILayout.Button("Check FirstChild.Name")){
			XmlDocument doc = new XmlDocument();
			doc.LoadXml((Resources.Load ("configData/TsChapter_" + "CreateTeam") as TextAsset).text);
			xmlFirstChildName = doc.ChildNodes[1].Name;
		}
		GUILayout.EndHorizontal();
	}
	public void TestXmlReader(){
		GUILayout.BeginHorizontal();
		GUILayout.Label(xmlReader);
		if (GUILayout.Button("xmlReader")){
			xmlReader = TsXmlReader.ReadPart("Start").ToString();
			Debug.ClearDeveloperConsole();
			Debug.Log(xmlReader);
		}
		GUILayout.EndHorizontal();
	}
	public void TestCreation(){
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("create")){
			TsTheater.Instance.PlayPart("Start");
		}
		GUILayout.EndHorizontal();
	}
}
