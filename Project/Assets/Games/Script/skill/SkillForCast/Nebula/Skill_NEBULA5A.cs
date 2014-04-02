using UnityEngine;
using System.Collections;

public class Skill_NEBULA5A : SkillBase {
	
	private ArrayList parms;
	private Character nebula;
	private Object haloPrefab;
	private Object deckGunPrefab;
	private int time;
	private float damage;
	private float attackSpeed;
	private bool isTowardRight;
	
	public override IEnumerator Cast (ArrayList objs)
	{
		parms = objs;
		LoadResources();
		
		nebula.castSkill("Skill5A");
		yield return new WaitForSeconds(.2f);
		CreateHalo();
		yield return new WaitForSeconds(.8f);
		CreateDeckGun();
	}
	
	private void LoadResources(){
		GameObject caller = parms[1] as GameObject;
		nebula = caller.GetComponent<Character>();
		
		if (null == deckGunPrefab){
			deckGunPrefab = Resources.Load("eft/Nebula/SkillEft_NEBULA5A_DeckGun");
		}
		if (null == haloPrefab){
			haloPrefab = Resources.Load("eft/Nebula/SkillEft_NEBULA5A_Halo");
		}
		
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("NEBULA5A");
		// attackSpeed = 1f/((Effect)def.activeEffectTable["aspd"]).num *0.01f * nebula.realAspd;
		attackSpeed = ((Effect)def.activeEffectTable["aspd"]).num;
		damage = ((Effect)def.activeEffectTable["atk_PHY"]).num * .01f;
		time = def.skillDurationTime;
		
		isTowardRight = nebula.model.transform.localScale.x > 0;
	}
	
	private void CreateHalo(){
		GameObject halo = Instantiate(haloPrefab) as GameObject;
		halo.transform.position = nebula.transform.position;
	}
	
	private void CreateDeckGun(){
		GameObject obj = Instantiate(deckGunPrefab, nebula.transform.position, transform.rotation) as GameObject;
		obj.transform.position += new Vector3((isTowardRight? 150f: -150f), 0f, 0f);
		obj.transform.localScale = new Vector3((isTowardRight? 1f: -1f), 1f, 1f);
		DeckGun gun = obj.GetComponent<DeckGun>();
		gun.Init(time, damage, nebula, attackSpeed);
	}
}
