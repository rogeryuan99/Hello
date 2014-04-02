using UnityEngine;
using System.Collections;

public class HeadIcon : MonoBehaviour {
public string heroType;
public HPBar hpBar;

public bool  isFirstCall = true;

public void Awake (){
	MsgCenter.instance.addListener(MsgCenter.HERO_HP_CHANGE, hpChange);
}

public void hpChange ( Message msg  ){
	Hero hero = msg.data as Hero;
	if((! hero.data.isDead) && heroType == hero.data.type){
		if(this.gameObject.active){
			StartCoroutine(hpBar.ChangeHp(hero.getHp()));
		}
	}
}

public void UpdataHpBar ( Hero targetHero  ){
//	Hero targetHero = HeroMgr.getHeroByType(heroType);
	HeroData heroD = targetHero.data as HeroData;
//	if(isFirstCall)
//	{
		//modified by xiaoyong 20120416
		int hp = int.Parse((heroD.maxHp+heroD.maxHp*(heroD.itemMult.maxHp+heroD.skillMult.maxHp)/100.0f).ToString());
		hpBar.initBar(hp);
//		isFirstCall = false;
//	}
	StartCoroutine(hpBar.ChangeHp(targetHero.getHp()));
}
public void OnDestroy (){
	MsgCenter.instance.removeListener(MsgCenter.HERO_HP_CHANGE, hpChange);
}
public void Update (){
}
}