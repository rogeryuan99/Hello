using UnityEngine;
using System.Collections;

public class EnemyPreviewDlg : DlgBase {
	private int chapterID;
	private int levelID;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void init(Hashtable typesHash,int chapterID,int levelID){
		this.chapterID = chapterID;
		this.levelID = levelID;
	}
	
	public void OnNextBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		TeamChangeDlg dlg = DlgManager.instance.ShowTeamChangeDlg();
		this.gameObject.SetActive(false);
		dlg.onClose = delegate {
			//this.gameObject.SetActive(true);
		};
		//Destroy(gameObject);
	}
}
