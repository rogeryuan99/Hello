using UnityEngine;
using System.Collections;

public class LevelSelectDlg : DlgBase
{
	public GameObject graphRoot;
	public UILabel displayEnemies;
	private int hSpan = 200;
	private int yCenter = 0;
	private int yHigh = 100;
	private int yLow = -100;
	public GameObject cellPrefab;
	public GameObject linePrefab;
	public UILabel chapterNameText;
	public UICheckbox checkBoxCheat;
	public Level curLv;
	
	private Hashtable id2LvCell;
	private Chapter chapter;
	private Hashtable typesHash;
	void Start ()
	{
		showByMoveFrom(Direction.RIGHT);
		init (MapMgr.Instance.getCurrentChapter());	
	}

	public void init (Chapter chapter)
	{
		this.chapter = chapter;
		Debug.Log("chapter = "+chapter.id);
		//MusicManager.playBgMusic("AMB_CH"+chapter.id+"_1b");
		//MusicManager.playBgMusic("AMB_CH1_1b");
		chapterNameText.text = chapter.name;
		id2LvCell = new Hashtable ();
		int n = -1;
		int isUnlockedID = 0;
		foreach (Level lv in chapter.levels) {
			n++;
			if(lv.id>=100){
				continue;	
			}
			if(!lv.isUnlocked() && isUnlockedID == 0){
				isUnlockedID = lv.id;
			}
			GameObject go = Instantiate (cellPrefab) as GameObject;
			LevelSelectDetailCell levelCell = go.GetComponent<LevelSelectDetailCell> ();
			levelCell.init (lv);
			go.name = "Level_" + lv.id;
			go.transform.parent = this.graphRoot.transform;
			go.transform.localScale = Vector3.one;
			id2LvCell.Add (lv.id, levelCell);
			GameObject preGo = null;
			if(lv.preLvIds == null || lv.preLvIds.Count == 0){
				preGo = null;
			}else{
				preGo = (id2LvCell[lv.preLvIds[0]] as LevelSelectDetailCell).gameObject;	
			}
			if(preGo == null){
				// absolutly at the center line ( start )
				Debug.Log(lv.id + " start");
				go.transform.localPosition = new Vector3 (0, yCenter, 0);
			}else if (lv.preLvIds.Count != 1) {
				// absolutly at the center line ( merge )
				Debug.Log(lv.id + " merge");
				go.transform.localPosition = new Vector3 (preGo.transform.localPosition.x+hSpan, yCenter, 0);
			} else {
				Level preLv = chapter.getLevelByID (lv.preLvIds [0]);
				if (preLv.postLvIds.Count > 1) { // fork
					if (preLv.postLvIds [0] == lv.id) {
						Debug.Log(lv.id + " fork up");
						go.transform.localPosition = new Vector3 (preGo.transform.localPosition.x+hSpan,yHigh,0);
					} else {
						Debug.Log(lv.id + " fork down");
						go.transform.localPosition = new Vector3 (preGo.transform.localPosition.x+hSpan,yLow,0);
					}
				} else { // follow the line of pre one
					Debug.Log(lv.id + " follow");
					go.transform.localPosition = new Vector3 (preGo.transform.localPosition.x+hSpan,preGo.transform.localPosition.y,0);
				}
			}
			
			//draw lines
			if(lv.preLvIds!=null){
				foreach(int preid in lv.preLvIds){
					preGo = (id2LvCell[preid] as LevelSelectDetailCell).gameObject;	
					GameObject line = Instantiate(linePrefab) as GameObject;
					line.transform.parent = this.graphRoot.transform;
					line.transform.localScale = Vector3.one;
					line.transform.localPosition = preGo.transform.localPosition;
					
					if(Mathf.Approximately(preGo.transform.localPosition.y,go.transform.localPosition.y)){
						//no rotation	
					}else if(preGo.transform.localPosition.y > go.transform.localPosition.y){
						line.transform.localRotation = Quaternion.Euler(new Vector3(0,0,-30));
					}else{
						line.transform.localRotation = Quaternion.Euler(new Vector3(0,0,+30));
					}
				}
			}
			//if(lv.id == 1) levelCell.BgSprite.color = new Color(0.78f,0.27f,1f,1f);
		}
		
		playChapterMusic(chapter.id,isUnlockedID);
		
		foreach (LevelSelectDetailCell cell in id2LvCell.Values) {
			if (cell.lv.preLvIds == null)
				continue;
			LevelSelectDetailCell preCell = id2LvCell [cell.lv.preLvIds [0]] as LevelSelectDetailCell;
//			Debug.Log (preCell.lv.id + "->" + cell.lv.id);
			if (cell.lv.preLvIds.Count >= 2) {
				preCell = id2LvCell [cell.lv.preLvIds [1]] as LevelSelectDetailCell;
//				Debug.Log (preCell.lv.id + "->" + cell.lv.id);
			}
		}
//		for(int i = 0;i < this.chapter.levels.Count;i++){
//			Level lv = this.chapter.levels[i];
//			if(!lv.isUnlocked()){
//				OnCellClicked(this.chapter.levels[i-1]);
//				break;
//			}
//		}
	}
	
	private void playChapterMusic(int ChapterID,int LvID){
		string musicName = "";
		switch(ChapterID){
		case 1:
			musicName = "AMB_CH1_A";
			break;
		case 2:
			musicName = (LvID > 4)? "AMB_CH2_B" : "AMB_CH2_A";
			break;
		case 3:
			musicName = (LvID > 4)? "AMB_Ch3_B" : "AMB_Ch3_A";
			break;
		case 4:
			musicName = "AMB_CH4_A";
			break;
		default:
			musicName = "AMB_CH1_A";
			break;
		}
		MusicManager.playBgMusic(musicName);
	}
	
	public void OnCellClicked (Level lv)
	{
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		curLv = lv;
//		string s = "Enemies :";
//		typesHash = getMaxTypes (lv.wave);
//		foreach (string key in typesHash.Keys){
//			s += "  " + key;
//		}
//		displayEnemies.text = s;
//		
//		for(int n = 0;n < this.chapter.levels.Count;n++){
//			LevelSelectDetailCell cell = graphRoot.transform.GetChild(n).GetComponent<LevelSelectDetailCell>();
//			if(cell.name == "Level_" + curLv.id) cell.BgSprite.color = new Color(0.78f,0.27f,1f,1f);
//			else cell.BgSprite.color = new Color(0.9f,0.65f,1f,1f);
//		}
		
		Debug.Log ("OnCellClicked:" + lv.id);
		if(this.checkBoxCheat.isChecked){
			lv.winStars = Mathf.Min(lv.winStars+1,4);
			refresh();
		}else{
			MapMgr.Instance.selectChapterAndLevel(chapter.id,lv.id);
			GotoProxy.gotoScene(GotoProxy.BATTLESCENE);
			
			Destroy (gameObject);
		}
	}
	void onArenaClicked(){
		MapMgr.Instance.selectChapterAndLevel(chapter.id,100);
		GotoProxy.gotoScene(GotoProxy.BATTLESCENE);
		Destroy (gameObject);
	}
	private void refresh(){
		foreach(LevelSelectDetailCell cell in id2LvCell.Values){
			cell.refresh();	
		}
	}
	
	public override void OnBtnBackClicked() {
		MusicManager.playEffectMusic("SFX_UI_exit_tap_2a");
		//MusicManager.playBgMusic("");
		DlgManager.instance.clearStack();
		DlgManager.instance.ShowChapterListDlg();
		Destroy (gameObject);
	}
	
	public void OnBattleBtnClick() {
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		Debug.Log ("OnCellClicked:" + curLv.id);
		if(this.checkBoxCheat.isChecked){
			curLv.winStars = Mathf.Min(curLv.winStars+1,4);
			refresh();
		}else{
			MapMgr.Instance.selectChapterAndLevel(chapter.id,curLv.id);
			GotoProxy.gotoScene(GotoProxy.BATTLESCENE);
			Destroy (gameObject);
		}		
	}
	
//	public void OnDoubleClicked(Level lv){
//		OnCellClicked(lv);	
//		OnBattleBtnClick();
//	}
	
	public void OnChangeTeamBtnClick() {
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		DlgManager.instance.ShowEnemyPreviewDlg(typesHash,chapter.id,curLv.id);
		Destroy(gameObject);
	}
	
	private Hashtable getMaxTypes (ArrayList ary)
	{
		Hashtable hashReturn = new Hashtable ();
		for (int i=0; i<ary.Count; i++) {
			Hashtable tempHash = new Hashtable ();
			ArrayList array = ary [i] as ArrayList;
			for (int j=0; j<array.Count; j++) {
				string type = array [j].ToString ();
				if (tempHash.ContainsKey (type)) {
					int tempValue = int.Parse (tempHash [type].ToString ());
					int count = tempValue + 1;
					tempHash [type] = count;
				} else {
					tempHash [type] = 1;
				}
			}
			foreach (string k in tempHash.Keys) {
				if (hashReturn.ContainsKey (k)) {
					int hashValue = int.Parse (hashReturn [k].ToString ());
					int val = int.Parse (tempHash [k].ToString ());
					if (hashValue < val) {
						hashReturn [k] = tempHash [k];
					}
				} else {
					hashReturn [k] = tempHash [k];
				}
			}	
		}
		return hashReturn;
	}
//	void OnGUI(){
//		GUILayout.Space(200);
//		if(GUILayout.Button("reposition")){
//			while(graphRoot.transform.childCount>0){
//				Destroy(graphRoot.transform.GetChild(0).gameObject);
//			}
//			init (NewLevelMgr.Instance.chapters[0]);
//		}
//	}
	
}
