using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ChapterListDlg : DlgBase {
	public List<ChapterDetailCell> chapterCells;
	
	// Use this for initialization
	void Start () {
		MusicManager.playBgMusic("MUS_UI_Menus");
		//delete chapter 0, it's for tutorial
		
		for(int n = 0;n<chapterCells.Count;n++){
			chapterCells[n].init(MapMgr.Instance.getChapterByID(n+1));	
		}
		
		TsFtueManager.Instance.CheckEvent(string.Format("{0}_{1}",
			TsFtueManager.DIALOG_OPENED, gameObject.name));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void OnBtnBackClicked() {
		MusicManager.playEffectMusic("SFX_UI_exit_tap_2a");
		DlgManager.instance.clearStack();
		DlgManager.instance.ShowHomePageDlg();
		Destroy (gameObject);
	}
	
	public void OnChapterCellClicked(Chapter chapter){
		MapMgr.Instance.currentChapterIndex = chapter.id;
		DlgManager.instance.ShowLevelSelectDlg();
		Destroy (gameObject);
	}
	
}
