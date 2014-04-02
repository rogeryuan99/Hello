using UnityEngine;
using System.Collections;

public class GotoTutorialButton : MonoBehaviour {

	public void GotoTutorial(){
		TsTheater.InTutorial = true;
		StaticData.isBattleEnd = false;
		for(int i=0; i<UserInfo.heroDataList.Count; i++)
		{
			HeroData heroData = UserInfo.heroDataList[i] as HeroData;
			//heroData.isSelect = false; heroData.state??
		}
		MapMgr.Instance.selectChapterAndLevel(999,1);
		
		GotoProxy.gotoScene(GotoProxy.BATTLESCENE);
//		GotoProxy.gotoScene(GotoProxy.TUTORIALSCENE);
		GameObject go = new GameObject("Tutorial");
		TsTheater tt = go.AddComponent<TsTheater>();
		tt.PreparePart("Start");
		DontDestroyOnLoad(go);
	}

}
