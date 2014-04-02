using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	public PowerUpDef powerupDef;
	public GameObject heart;
	public GameObject shadow;
	public bool isBorn = false;
	
	private float curTime = 0f;
	
	void Start () {
	
	}

	void Update () {
		if(isBorn){
			calculateDistance();
			curTime += Time.deltaTime;	
			if(curTime >= (float)powerupDef.puBuffDurTime){
				Destroy(this.gameObject);
			}
		}
	}
	
	private void calculateDistance(){
		foreach(Hero hero in HeroMgr.heroHash.Values){
			Vector2 vc2 = hero.transform.position - shadow.transform.position;
			if(StaticData.isInOval(50,50,vc2) && !hero.isDead){
				string buffType = "BUFF_HP";
				switch(powerupDef.puBuffType){
				case "HP":
					buffType = BuffTypes.HP;
					break;
				case "ATK":
					buffType = BuffTypes.ATK_PHY;
					break;
				}
				int hp = (int)(hero.realMaxHp*((float)powerupDef.puBuffValue/100f));
				hero.addHp(hp);
				isBorn = false;
				Destroy(this.gameObject);
			}
		}
	}
	
}
