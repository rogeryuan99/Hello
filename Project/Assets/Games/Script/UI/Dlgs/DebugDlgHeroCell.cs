using UnityEngine;
using System.Collections;

public class DebugDlgHeroCell : MonoBehaviour {
	public HeroData hd;
	public UILabel textStamina;
	public UILabel textName;
	void OnStaminaAdd(){
		hd.addStamina(1);
	}
	void OnStaminaSub(){
		hd.consumeStamina(1);
	}
	void OnFire(){
		hd.state = HeroData.State.UNLOCKED_NOT_RECRUITED;
		UserInfo.instance.saveAllheroes();
	}
	void Update(){
		if(hd!=null){
			textName.text = hd.type;
			string lefttime = hd.staminaRegenerate();
			textStamina.text = "Stamina:"+hd.stamina + "\n"+lefttime + "\nRegenCostGold:"+ hd.getStaminaRechargeCostGold();	
		}
	}
}
