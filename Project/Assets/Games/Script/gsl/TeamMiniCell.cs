using UnityEngine;
using System.Collections;

public class TeamMiniCell : MonoBehaviour {
	public UISprite Bg;
	public UISprite BgHighlight;
	public UISprite upperHighlight;
	public UISprite Icon;
	public TeamMiniBar teamMiniBar;
	
	//private bool isDisplay = false;
	public HeroData heroData;
	
	public void SetHeroData(HeroData hd){
		this.heroData = hd;
		updateView();
	}
	
	public void OnCellClick(){
		Debug.Log("OnCellClick");
		if(teamMiniBar.onHeroClicked!=null)
			teamMiniBar.onHeroClicked(this.heroData);
	}
	
	public void updateView(){
		if(heroData == null){
			Bg.enabled = false;
			Icon.enabled = false;
			Debug.Log("clear icons.......");
		}else{
			Bg.enabled = true;
			Icon.enabled = true;	
			Icon.spriteName = "" + this.heroData.type;
			Icon.MakePixelPerfect();
		}
	}
	public void highLight(bool b){
		Bg.gameObject.SetActive(!b);
		BgHighlight.gameObject.SetActive(b);
		upperHighlight.gameObject.SetActive(b);
	}
}
