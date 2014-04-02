using UnityEngine;
using System.Collections;

public class WyvernRider : Enemy {
	public GameObject atkEft;
	public override void Awake (){	 
		base.Awake();
		atkAnimKeyFrame = 36; 
	}
	
	public override void Start (){
		base.Start();
		pieceAnima.addFrameScript("Attack",19,attackEft);
	}
	public void attackEft (string s){
		GameObject tempFlame_atkEft;
		PackedSprite tempFlame_atkEftInfo;

		if(model.transform.localScale.x > 0){
				tempFlame_atkEft= Instantiate(atkEft,new Vector3(gameObject.transform.position.x+55,gameObject.transform.position.y+58,gameObject.transform.position.z),transform.rotation) as GameObject;
				tempFlame_atkEftInfo= tempFlame_atkEft.GetComponent<PackedSprite>();
				tempFlame_atkEftInfo.PlayAnim("eft");
		}
		else{
				tempFlame_atkEft = Instantiate(atkEft,new Vector3(gameObject.transform.position.x-53,gameObject.transform.position.y+42,gameObject.transform.position.z),transform.rotation) as GameObject;
				tempFlame_atkEftInfo = tempFlame_atkEft.GetComponent<PackedSprite>();
				tempFlame_atkEftInfo.transform.localScale = new Vector3(-1, tempFlame_atkEftInfo.transform.localScale.y, tempFlame_atkEftInfo.transform.localScale.z);
//				tempFlame_atkEftInfo.transform.localScale.x = -1;
				tempFlame_atkEftInfo.PlayAnim("eft");
		}
	}
	
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("SFX_enemy_melee_attack_1b");
		base.atkAnimaScript("");
	}
	
	public override void dead (string s=null){
		base.dead();
	}
}
