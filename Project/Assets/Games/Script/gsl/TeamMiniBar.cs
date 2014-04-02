using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamMiniBar : MonoBehaviour {
	public List<TeamMiniCell> teamCellList = new List<TeamMiniCell>();
	public bool teamMiniBarIsfull = false;
	public HeroData readyHeroData = null;
	public TeamChangeDlg teamChangeDlg;
	
	public void initTeamList(){
		List<HeroData> heroDataList = new List<HeroData>();
		foreach(HeroData hd in UserInfo.heroDataList){
			if(hd.state == HeroData.State.SELECTED) heroDataList.Add(hd);
		}
		if(heroDataList.Count <= 0) Debug.LogError("there are no Heros in Team~");
		for(int n = 0;n < teamCellList.Count;n++){
			TeamMiniCell tc = teamCellList[n];
			tc.SetHeroData((n < heroDataList.Count)? heroDataList[n]: null);
			tc.teamMiniBar = this;
		}
	}	
	
	public void highlightHeroData(HeroData hd){
		for(int n = 0;n < teamCellList.Count;n++){
			TeamMiniCell tc = teamCellList[n];
			if(tc.heroData == hd  && hd != null){
				tc.highLight(true);
			}else{
				tc.highLight(false);	
			}
		}
	}
	public HeroData highLightFirstHero(){
		for(int n = 0;n < teamCellList.Count;n++){
			TeamMiniCell tc = teamCellList[n];
			if(tc.heroData != null){
				highlightHeroData(tc.heroData);
				return tc.heroData;
			}
		}
		return null;
	}
	public bool addHeroData(HeroData hd){
		for(int n = 0;n < teamCellList.Count;n++){
			TeamMiniCell tc = teamCellList[n];
			if(tc.heroData == null){
				tc.SetHeroData(hd);
				hd.state = HeroData.State.SELECTED;
				updateTeamChangeCellView(hd);
				UserInfo.instance.saveAllheroes();
				return true;
			}
		}
		return false;
	}

	public bool removeHeroData(HeroData hd){
		int teamHeros = 0;
		foreach(HeroData h in UserInfo.heroDataList){
			if(h.state == HeroData.State.SELECTED) teamHeros++;
		}
		if(teamHeros <= 1) return false;
		for(int n = 0;n < teamCellList.Count;n++){
			TeamMiniCell tc = teamCellList[n];
			if(tc.heroData == hd){
				tc.SetHeroData(null);
				hd.state = HeroData.State.RECRUITED_NOT_SELECTED;
				updateTeamChangeCellView(hd);
				UserInfo.instance.saveAllheroes();
				return true;
			}
		}
		return false;
	}
	
	private void updateTeamChangeCellView(HeroData hd){
		if(teamChangeDlg == null) return;
		for(int i = 0;i < teamChangeDlg.grid.list.Count;i++){
			TeamChangeCell cell = teamChangeDlg.grid.list[i].GetComponentInChildren<TeamChangeCell>();
			if(hd == cell.heroData)	cell.updateView();
		}	
	}
	
	public delegate void OnHeroClicked(HeroData hd);
	public OnHeroClicked onHeroClicked;
	
	public int getCurTeamHerosCount(){
		int count = 0;
		foreach(HeroData hd in UserInfo.heroDataList){
			if(hd.state == HeroData.State.SELECTED) count++;
		}	
		return count;
	}
}
