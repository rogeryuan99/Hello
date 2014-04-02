using UnityEngine;
using System.Collections;

public class TsHeroFactory: TsIFactory{
	
	public GameObject Create(string type){
		GameObject obj = null;
		
		for(int i=0; i<UserInfo.heroDataList.Count; i++){
			HeroData heroData = UserInfo.heroDataList[i] as HeroData;
			if(heroData.type != type) continue;//selected only
			
			obj = MonoBehaviour.Instantiate(CacheMgr.getHeroPrb(heroData.type)) as GameObject;
			obj.transform.localScale = new Vector3(Utils.characterSize, Utils.characterSize,1);
			Hero hero = obj.GetComponent<Hero>();
			hero.initData(heroData);
		}
		
		return obj;
	}
}
