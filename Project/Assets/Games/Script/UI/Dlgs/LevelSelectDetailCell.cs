using UnityEngine;
using System.Collections;

public class LevelSelectDetailCell : MonoBehaviour {
	public UISprite BgSprite;
	public UILabel textName;
	public UISprite spriteLock;
	public UISprite star1;
	public UISprite star2;
	public UISprite star3;
	public UISprite star4;
	public Level lv;
	public void init(Level lv){
		this.lv = lv;
		refresh();
	}
	public void refresh(){
		if(lv.isUnlocked()){
			textName.text = lv.id.ToString();
			spriteLock.enabled = false;
			textName.enabled = true;
			star1.enabled = lv.winStars>=1;
			star2.enabled = lv.winStars>=2;
			star3.enabled = lv.winStars>=3;
			star4.enabled = lv.winStars>=4;
		}else{
			spriteLock.enabled = true;
			textName.enabled = false;
			star1.enabled = false;
			star2.enabled = false;
			star3.enabled = false;
			star4.enabled = false;
		}
	}
	void cellClicked(){
		if(lv.isUnlocked()){
			Debug.Log("lv clicked:"+lv.id);	
			NGUITools.FindInParents<LevelSelectDlg>(this.gameObject).OnCellClicked(this.lv);
		}else{
			Debug.Log("locked");	
		}
	}
//	void OnDoubleClick(){
//		if(lv.isUnlocked()){
//			Debug.Log("lv clicked:"+lv.id);	
//			NGUITools.FindInParents<LevelSelectDlg>(this.gameObject).OnDoubleClicked(this.lv);
//		}else{
//			Debug.Log("locked");	
//		}
//	}
}
