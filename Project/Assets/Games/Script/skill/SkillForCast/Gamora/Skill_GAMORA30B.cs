using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_GAMORA30B : SkillBase {
	
	public ArrayList objs = null;
	public GameObject Effectt_Prb;
	private int damage = 0;
	private int time = 0;
	protected float per = 0;

	public override IEnumerator Cast (ArrayList objs)
	{
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		MusicManager.playEffectMusic("SFX_Gamora_Deadlist_Woman_1a");
		this.objs = objs;
		
		Gamora gamora = caller.GetComponent<Gamora>();
		gamora.slowdownCallback = SlowDownEffect;
		gamora.damageCallback = DamageEnemy;
		gamora.castSkill("Skill30B");
		
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GAMORA30B");
		Hashtable tempNumber = skillDef.activeEffectTable;
		
		per = ((Effect)tempNumber["atk_PHY"]).num;
		
		time = skillDef.skillDurationTime;
		
		yield return new WaitForSeconds(0f);
	}
	
	private List<GameObject> effects = new List<GameObject>();
	private Hashtable targets = new Hashtable();
	
	public void DamageEnemy()
	{
		GameObject caller = this.objs[1] as GameObject;
		Gamora gamora = caller.GetComponent<Gamora>();
		
		ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
		
		foreach(Enemy enemy in enemyList)
		{
			ShowEffect(enemy);
//			enemy.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
//			enemy.fireWithSeconds();
			
			State s = new State(time, DestroySkillEft);
					enemy.addAbnormalState(s, Character.ABNORMAL_NUM.FIRE);
			// Debug.LogError(string.Format("damage {0}, hp {1}", damage, enemy.realHp));
			
			damage = enemy.getSkillDamageValue(gamora.realAtk, per);
			enemy.realDamage(damage);
		}
	}
	
	public void ShowEffect(Enemy enemy)
	{
		if(Effectt_Prb == null)
		{
			Effectt_Prb = Resources.Load("eft/Gamora/SkillEft_GAMORA30AB") as GameObject;
		}  
		
		GameObject front = Instantiate(Effectt_Prb) as GameObject;
		front.transform.parent = enemy.transform;
		front.transform.localPosition = new Vector3(0,500,-15);
		
		GameObject behind = Instantiate(Effectt_Prb) as GameObject; 
		behind.transform.parent = enemy.transform;
		behind.transform.localPosition = new Vector3(0,500,100);
		
		effects.Add(front);
		effects.Add(behind);
		if (targets.ContainsKey(enemy.getID())){
			targets[enemy.getID()] = effects.Count-2;
		}
		else{
			targets.Add(enemy.getID(), effects.Count-2);
		}
		
		PackedSprite ps = front.GetComponent<PackedSprite>();
		ps.Color = new Color(ps.Color.r, ps.Color.g,ps.Color.b,0.5f);
	}
	
	
	
	public void DestroySkillEft(State state, Character charater)
	{
//		charater.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
		
		int index = (int)targets[charater.getID()];
		for (int i=index; i<index+2; i++){
			GameObject obj = effects[i];
			Destroy(obj);
			effects[i] = null;
		}
		targets.Remove(charater.getID());
		
		if (0 == targets.Count){
			effects.Clear();
		}
	}
	
	public void SlowDownEffect(){
		StartCoroutine(SkillManager.Instance.slowMotion(0.0f, 0.15f));
		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.15f));
	}
	
	
	
	
	
//	protected ArrayList gameObjects;
//	
//	protected Vector3 comboGamora1OldHeroPos = Vector3.zero;
//	protected GameObject comboGamora1EftPre = null;
//	protected GameObject comboGamora1Eft = null;
//	private List<Enemy> enemies = new List<Enemy>();
//	
//	
//	public override IEnumerator Cast(ArrayList objs)
//	{
//		GameObject scene = objs[0] as GameObject;
//		GameObject caller = objs[1] as GameObject;
//		GameObject target = objs[2] as GameObject;
//		
//		gameObjects = objs;
//		
//		Hero heroDoc = caller.GetComponent<Hero>();
//		yield return new WaitForSeconds(0.5f);
//		
//		heroDoc.castSkill("GroupAttackA");
//		StartCoroutine(StartComboGamora1());
//	}
//	
//	public IEnumerator StartComboGamora1()
//	{
//		yield return new WaitForSeconds(0.5f);
//		
//		GameObject scene = gameObjects[0] as GameObject;
//		GameObject caller = gameObjects[1] as GameObject;
//		GameObject target = gameObjects[2] as GameObject;
//		
//		comboGamora1OldHeroPos = caller.transform.position;
//		
//		CollectEnemies();
//		ErgodicAttack();
//	}
//	
//	private void CollectEnemies(){
//		enemies.Clear();
//		foreach (Enemy enemy in EnemyMgr.enemyHash.Values){
//			enemies.Add(enemy);
//		}
//		Debug.LogError(string.Format("Enemies.Count is {0}", enemies.Count));
//	}
//	
//	private void ErgodicAttack(){
//		
//		for (int i=0; i<enemies.Count; ){
//			if (null == enemies[i] || enemies[i].isDead){
//				enemies.RemoveAt(0);
//			}
//			else{
//				i = enemies.Count;
//			}
//		}
//		
//		if (enemies.Count > 0){
//			GameObject caller = gameObjects[1] as GameObject;
//			GameObject target = enemies[0].gameObject;
//			
//			Hero heroDoc = caller.GetComponent<Hero>();
//			
//			heroDoc.toward(enemies[0].transform.position);
//			float xPos = (heroDoc.model.transform.position.x > target.transform.position.x)? -heroDoc.data.attackRange: heroDoc.data.attackRange;
//			// Vector3 moveToPos = enemies[0].transform.position + new Vector3 (xPos, 0, 0);
//			Vector3 moveToPos = BattleBg.CorrectingEndPointToActionBounds (enemies[0].transform.position + new Vector3 (xPos, 0, 0));
//			
//			heroDoc.castSkill("GroupAttackB");
//			showComboGamora1Eft();
//			enemies[0].realDamage((int)(((float)heroDoc.realAtk * 2.2f)));
//			enemies.RemoveAt(0);
//			ComboGamora1MoveByTween(caller, moveToPos, "ErgodicAttack");
//		}
//		else{
//			EndAttack();
//		}
//	}
//	
//	private void EndAttack(){
//		GameObject caller = gameObjects[1] as GameObject;
//		
//		Hero heroDoc = caller.GetComponent<Hero>();
//		heroDoc.pieceAnima.restart();
//		heroDoc.standby();
//		Debug.LogError("EndAttack");
//	}
//	
//	/*public void ComboGamora1MoveToPositionFinished()
//	{			
//		GameObject caller = gameObjects[1] as GameObject;
//		Hero heroDoc = caller.GetComponent<Hero>();
//		
//		heroDoc.model.transform.localScale = new Vector3 (-heroDoc.model.transform.localScale.x, heroDoc.scaleSize.y, 1);
//	
//		showComboGamora1Eft();
//		
//		ComboGamora1MoveByTween(caller, comboGamora1OldHeroPos, "ComboGamora1MoveToOldPositionFinished");
//	}
//
//	public void ComboGamora1MoveToOldPositionFinished()
//	{
//		
//		GameObject caller = gameObjects[1] as GameObject;
//		GameObject target = gameObjects[2] as GameObject;
//		
//		Hero heroDoc = caller.GetComponent<Hero>();
//		
//		heroDoc.pieceAnima.restart();
//		heroDoc.moveToTarget(target);
//		
//	}*/
//	
//	public void ComboGamora1MoveByTween(GameObject target, Vector3 moveToPos, string completeFunStr)
//	{
//		iTween.MoveTo
//		(
//			target,
//			iTween.Hash
//				(
//					"position", moveToPos,
//					"time", 0.2,
//					"easetype", "easeOutExpo",
//					"oncomplete", completeFunStr,
//					"onCompleteTarget", this.gameObject
//				)
//		);
//	}
//	
//	
//	public void showComboGamora1Eft()
//	{
//		GameObject scene = gameObjects[0] as GameObject;
//		GameObject caller = gameObjects[1] as GameObject;
//		// GameObject target = gameObjects[2] as GameObject;
//		
//		Vector3 comboGamora1EftlocalScale = Vector3.zero;
//		float comboGamora1EftPositionX = 0.0f;
//		comboGamora1EftlocalScale = new Vector3(7, 7, 1);
//		
//		Gamora heroDoc = caller.GetComponent<Gamora>();	
//		
//		if(heroDoc.model.transform.localScale.x > 0)
//		{
//			comboGamora1EftPositionX = heroDoc.transform.position.x - 100;
//		}
//		else
//		{
//			comboGamora1EftPositionX = heroDoc.transform.position.x + 100;
//		}
//		
//		if(comboGamora1EftPre == null)
//		{
//			comboGamora1EftPre = Resources.Load("eft/Drax/Combo_DRAX1") as GameObject;
//		}
//			
//		Transform eftParent = heroDoc.model.transform.FindChild("FEMALE_Weapon_01");
//		
//		if(eftParent == null)
//		{
//			Debug.LogError("FEMALE_Weapon_01 not fond");
//		}
//		else
//		{
//			comboGamora1Eft = (GameObject)Instantiate(
//				comboGamora1EftPre,
//				new Vector3
//				(
//				comboGamora1EftPositionX,
//				heroDoc.transform.position.y + 30,
//				-100
//				),
//				heroDoc.transform.rotation
//			);
//			comboGamora1Eft.transform.parent = eftParent;
//		
//			comboGamora1Eft.transform.localScale = comboGamora1EftlocalScale;
//		}
//	}
}
