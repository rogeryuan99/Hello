using UnityEngine;
using System.Collections;

public class DragBtn : MonoBehaviour {
	public PreBattleDlg preBattleDlg;
	
	void OnDrag (Vector2 delta)
	{
		foreach(AudioSource eftMusic in MusicManager.Instance.effectMusicObjs){
			if(!eftMusic.isPlaying && eftMusic.clip == MusicManager.Instance.getAudioClipByName("SFX_UI_swipe_loop_1a")){
				//MusicManager.playEffectMusic("SFX_UI_swipe_loop_1a");
			}
		}	
		//MusicManager.playEffectMusic("SFX_UI_swipe_loop_1a");
		preBattleDlg.OnSliderDrag(delta);
	}
	
	void OnPress (bool isPressed)
	{
		preBattleDlg.OnSliderPress(isPressed);
	}	
}
