using UnityEngine;
using System.Collections;

public class MusicTest : MonoBehaviour {
	void OnGUI(){
		if(GUILayout.Button("playBg")){
			//MusicManager.playBgMusic("Guardians_Menu_Temp_1a");	
		}
		if(GUILayout.Button("FX")){
			MusicManager.playEffectMusic("SFX_Tap_Here_2a",1.5f);
		}
		if(GUILayout.Button("FX2")){
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b",05f);
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a",05f);
		}
		if(GUILayout.Button("FX3")){
			MusicManager.playEffectMusic("SFX_Skill_Training_done_1a",1.3f);
		}
	}
}
