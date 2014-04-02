using UnityEngine;
using System.Collections;

public class DlgTester : MonoBehaviour {
	void Awake(){
		DontDestroyOnLoad(this.gameObject);	
	}
	void OnGUI(){
		if(GUILayout.Button("ShowLevelSelectDlg")){
			DlgManager.instance.ShowLevelSelectDlg();
		}
		if(GUILayout.Button("ShowChapterListDlg")){
			DlgManager.instance.ShowChapterListDlg();
		}
	}
}
