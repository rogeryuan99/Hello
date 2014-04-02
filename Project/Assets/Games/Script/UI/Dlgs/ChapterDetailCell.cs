using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChapterDetailCell : MonoBehaviour {
	// you can access the cell object by protected var cell
	
	public GameObject chapterBgSprite;
	
	public UILabel textName;
	public UILabel textStars;
	public GameObject lockedGroup;	
	public GameObject unlockedGroup;	
	public Chapter chapter;
	public void init(object data){
		chapter = (Chapter)data;
		if(chapter.isUnlocked()){
			textName.text = Localization.instance.Get("UI_ChapterName_"+chapter.id);
			textStars.text = string.Format("{0}/{1}",chapter.winStars,chapter.passStars);
			lockedGroup.SetActive(false);
			unlockedGroup.SetActive(true);
		}else{
			lockedGroup.SetActive(true);
			unlockedGroup.SetActive(false);
		}
	}
	
	public void cellClicked(){
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		if(chapter.isUnlocked()){
			NGUITools.FindInParents<ChapterListDlg>(this.gameObject).OnChapterCellClicked(this.chapter);
		}else{
			Debug.Log("locked");	
		}
	}
	
}
