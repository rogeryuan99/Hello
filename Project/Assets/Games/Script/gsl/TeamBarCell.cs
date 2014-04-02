using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamBarCell : DetailCell {
	public UILabel Index;
	public UISprite light;
	public UISprite flag;
	public HeroData heroData;
	private int index;

	void Start () {
		flag.gameObject.SetActive(false);
	}
	
	void Update () {
		EquipData ed = SlotCursor.instance.getEquipData();
		if(ed == null){
			flag.gameObject.SetActive(false);	
			return;
		}
		if(!SlotCursor.instance.getEnabledOfSlotCursor()){
			flag.gameObject.SetActive(false);	
			return;
		}
		int count = ed.equipDef.specialType.Count;
		if(count == 0){
			flag.gameObject.SetActive(true);
			return;
		}
		for(int n = 0;n < count;n++){
			string type = ed.equipDef.specialType[n] as string;
			if(type == heroData.nickName){
				flag.gameObject.SetActive(true);
			}
		}
	}
	
	public override void OnIn(object data){
		heroData = (HeroData)data; 
		index = this.getDynamicCell().index;
		Debug.Log(index + " : " + heroData.nickName);
		Index.text = "" + index;
		if(isSelected){
			light.gameObject.SetActive(true);
		}else{
			light.gameObject.SetActive(false);
		}
	}
	
	public override void OnOut(){
		
	}
	
	public void OnTeamCellClick(){
		Debug.Log("on click : cell_" + index);
		if(isSelected)
			this.tryToUnselect();
		else
			this.tryToSelect();
	}
	
	public override void OnSelected(){
		if(GameObject.FindObjectsOfType(typeof(TeamBar)).Length!=1)	{
			Debug.LogError("only one TeamBar should exist");
		}
		TeamBar teambar = GameObject.FindObjectOfType(typeof(TeamBar)) as TeamBar;
		if(teambar!=null && index != -1){
			teambar.__OnTeamBarCellClicked(index);
		}
		light.gameObject.SetActive(true);
	}
	
	public override void OnUnselected(){
		Debug.LogError(this.gameObject.name);
		light.gameObject.SetActive(false);
	}
	
	public override void OnKicked(){
		light.gameObject.SetActive(false);
	}
	
	public void SetFlag(string name){
		Debug.LogError("heroData.nickName : " + heroData.nickName + "   name : " + name);
		if(heroData.nickName == name){
			flag.gameObject.SetActive(true);
		}
	}
}
