using UnityEngine;
using System.Collections;

public class BattleEndDlgHeroState : MonoBehaviour {
	public ExpBar xpBar;
	public UISprite avatar;
	public UILabel labelXp;
	private Hero _hero;
	private HeroData hd;
	public GameObject levelUp;
	[HideInInspector]
	public Hero hero{
		get{ return _hero; }
		set{
			_hero = value;
			gameObject.name = string.Format("HeroState_{0}", _hero.name);
			hd = (HeroData)hero.data;
			xpBar.initBar(hd.exp);
			labelXp.text = hd.exp.ToString();
			avatar.spriteName = hd.type;
			hideLevelUp();
		}
	}
	
	public void addXp(int delt){
		StartCoroutine(_addXp(delt));
	}
	
	private IEnumerator _addXp(int delt){
		float t = Mathf.Lerp(0.002f,0.2f,1f/(float)delt);
		int oldLv = xpBar.level;
		while(delt>0){
			yield return new WaitForSeconds(0.02f);
			int d = 1;
			delt -=d;
			hd.exp += d;
			
			xpBar.initBar(hd.exp);
			labelXp.text = hd.exp.ToString();
			int newLv = xpBar.level;
			if(newLv>oldLv){
				Debug.Log("XP: "+hd.type + " "+oldLv +"->"+newLv);
				oldLv = newLv;	
				showLevelUp();
				hd.lv = Mathf.Max(hd.lv,newLv);
			}
			_hero.reCalctAtkAndDef();
		}
		UserInfo.instance.saveAll();
	}
	
	private void showLevelUp(){
		Debug.LogError("showLevelUp");
		levelUp.SetActive(true);
		levelUp.transform.localScale = Vector3.one;
		//iTween.FadeTo(levelUp,iTween.Hash("alpha",0, "time",0.2f,"delay",1));
		iTween.ScaleTo(levelUp,iTween.Hash("scale",new Vector3(1.3f,1.3f,1), "time",0.5f,"islocal",true,"oncomplete","hideLevelUp","oncompletetarget",this.gameObject,"easetype",iTween.EaseType.easeOutBounce));
		//MusicManager.playEffectMusic("SFX_level_up_1b");
	}
	void hideLevelUp(){
		//Debug.LogError("hideLevelUp");
		levelUp.SetActive(false);
	}
	
}
