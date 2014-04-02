using UnityEngine;
using System.Collections;

public class DebugDlg : DlgBase
{
	public UILabel GoldLabel;
	public UILabel SilverLabel;
	public UILabel CpLabel;
	public UIGrid heroGrid;
	public GameObject HeroCellPrefab;
	
	private GameObject homePageDlg;
	void Start(){
		homePageDlg =  GameObject.Find("HomePageDlg");
		if(homePageDlg == null){
			homePageDlg =  GameObject.Find("HomePageDlg(Clone)");
		}
		homePageDlg.SetActive(false);
		Debug.Log("HeroMgr.heroHash "+HeroMgr.heroHash.Count);
		foreach(HeroData hd in UserInfo.heroDataList){
			Debug.Log("hero "+hd.type);
			GameObject cell = Instantiate(HeroCellPrefab) as GameObject;
			cell.transform.parent = heroGrid.transform;
			cell.transform.localScale = Vector3.one;
			DebugDlgHeroCell hc = cell.GetComponent<DebugDlgHeroCell>();
			hc.hd = hd;
		}
		heroGrid.repositionNow = true;
	}
	
	void Update(){
		GoldLabel.text = "Gold:"+UserInfo.instance.getGold();	
		SilverLabel.text = "Silver:"+UserInfo.instance.getSilver();	
		CpLabel.text = "Cp:"+UserInfo.instance.getCommandPoints();
	}
	
	void OnSilverAdd(){
		UserInfo.instance.addSilver(5);
	}
	void OnSilverSub(){
		UserInfo.instance.consumeSilver(1);
	}
	void OnGoldAdd(){
		UserInfo.instance.addGold(5);
	}
	void OnGoldSub(){
		UserInfo.instance.consumeGold(5);
	}
	void OnCpAdd(){
		UserInfo.instance.addCommandPoints(5);
	}
	void OnCpSub(){
		UserInfo.instance.consumeCommandPoints(5);
	}
	
	void OnClearFTUE(){
		TsDynamicData.reset();
	}
	void OnClearData(){
		UserInfo.instance.clearData();
	}

	public void OnBackBtnClick() {
		Destroy (gameObject);
		homePageDlg.SetActive(true);
	}
}
