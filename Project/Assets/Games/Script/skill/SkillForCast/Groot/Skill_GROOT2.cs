using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_GROOT2 : SkillBase
{
	protected ArrayList parms;
	
	private Object comboEftPrefab;
	private Character groot;
	private Character enemy;
	private float damage;
	private bool isTowardRight;
	
	
	public override IEnumerator Cast(ArrayList objs){
		parms = objs;
		LoadResources();
		
		groot.toward(enemy.transform.position);
		groot.castSkill("GroupAttackA");
		MusicManager.playEffectMusic("SFX_Combo3_Melee_Range_1b");
		
		yield return new WaitForSeconds(.5f);
		ErgodicAttack();
		yield return new WaitForSeconds(.2f);
		DamageEnemy();
	}
	
	private void LoadResources(){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		groot = caller.GetComponent<Character>();
		enemy = target.GetComponent<Character>();
		
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("GROOT2");
		damage = ((Effect)def.activeEffectTable["atk_PHY"]).num;
		
		isTowardRight = groot.model.transform.localScale.x > 0;
		
		if (null == comboEftPrefab){
			comboEftPrefab = Resources.Load("eft/Drax/Combo_DRAX1") as GameObject;
		}
	}
	
	private void DamageEnemy(){
		enemy.realDamage(200);
	}
	
	private void ErgodicAttack(){
		groot.toward(enemy.transform.position);
		float xPos = (groot.model.transform.position.x > enemy.transform.position.x)? -groot.data.attackRange: groot.data.attackRange;
		Vector3 moveToPos = BattleBg.CorrectingEndPointToActionBounds (enemy.transform.position + new Vector3 (xPos, 0, 0));
		
		groot.castSkill("GroupAttackB");
		iTween.MoveTo(enemy.gameObject, iTween.Hash("position", moveToPos, "time", 0.2, "easetype", "easeOutExpo" ));
	}
	
	public void showComboGamora1Eft(){
		float eftX = groot.transform.position.x + (isTowardRight? -100: 100);
		Transform eftParent = groot.model.transform.FindChild("MEDIUM_Weapon_01");
		
		if(eftParent == null){
			Debug.LogError("MEDIUM_Weapon_01 not fond");
		}
		else{
			GameObject eft = Instantiate(comboEftPrefab) as GameObject;
			eft.transform.position = new Vector3(eftX, groot.transform.position.y + 30, -100);
			eft.transform.parent = eftParent;
			eft.transform.localScale = new Vector3(7, 7, 1);
		}
	}
	
	
	/*protected ArrayList gameObjects;
	
	protected Vector3 comboGamora1OldHeroPos = Vector3.zero;
	protected GameObject comboGamora1EftPre = null;
	protected GameObject comboGamora1Eft = null;
	private List<Enemy> enemies = new List<Enemy>();
	
	
	public override IEnumerator Cast(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		gameObjects = objs;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		yield return new WaitForSeconds(0.5f);
		
		MusicManager.playEffectMusic("SFX_Combo3_Melee_Range_1b");
		
		heroDoc.castSkill("GroupAttackA");
		StartCoroutine(StartComboGamora1());
	}
	
	public IEnumerator StartComboGamora1()
	{
		yield return new WaitForSeconds(0.5f);
		
		GameObject scene = gameObjects[0] as GameObject;
		GameObject caller = gameObjects[1] as GameObject;
		GameObject target = gameObjects[2] as GameObject;
		
		comboGamora1OldHeroPos = caller.transform.position;
		
		CollectEnemies();
		ErgodicAttack();
	}
	
	private void CollectEnemies(){
		enemies.Clear();
		foreach (Enemy enemy in EnemyMgr.enemyHash.Values){
			enemies.Add(enemy);
		}
//		Debug.LogError(string.Format("Enemies.Count is {0}", enemies.Count));
	}
	
	private void ErgodicAttack(){
		
		for (int i=0; i<enemies.Count; ){
			if (null == enemies[i] || enemies[i].isDead){
				enemies.RemoveAt(0);
			}
			else{
				i = enemies.Count;
			}
		}
		
		if (enemies.Count > 0){
			GameObject caller = gameObjects[1] as GameObject;
			GameObject target = enemies[0].gameObject;
			
			Hero heroDoc = caller.GetComponent<Hero>();
			
			heroDoc.toward(enemies[0].transform.position);
			float xPos = (heroDoc.model.transform.position.x > target.transform.position.x)? -heroDoc.data.attackRange: heroDoc.data.attackRange;
			// Vector3 moveToPos = enemies[0].transform.position + new Vector3 (xPos, 0, 0);
			Vector3 moveToPos = BattleBg.CorrectingEndPointToActionBounds (enemies[0].transform.position + new Vector3 (xPos, 0, 0));
			
			heroDoc.castSkill("GroupAttackB");
//			showComboGamora1Eft();
//			enemies[0].realDamage((int)(((float)heroDoc.realAtk * 2.2f)));
			enemies.RemoveAt(0);
			ComboGamora1MoveByTween(caller, moveToPos, "ErgodicAttack");
		}
		else{
			EndAttack();
		}
	}
	
	private void EndAttack(){
		GameObject caller = gameObjects[1] as GameObject;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		heroDoc.pieceAnima.restart();
		heroDoc.standby();
//		Debug.LogError("EndAttack");
	}
	
	/*public void ComboGamora1MoveToPositionFinished()
	{			
		GameObject caller = gameObjects[1] as GameObject;
		Hero heroDoc = caller.GetComponent<Hero>();
		
		heroDoc.model.transform.localScale = new Vector3 (-heroDoc.model.transform.localScale.x, heroDoc.scaleSize.y, 1);
	
		showComboGamora1Eft();
		
		ComboGamora1MoveByTween(caller, comboGamora1OldHeroPos, "ComboGamora1MoveToOldPositionFinished");
	}

	public void ComboGamora1MoveToOldPositionFinished()
	{
		
		GameObject caller = gameObjects[1] as GameObject;
		GameObject target = gameObjects[2] as GameObject;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		
		heroDoc.pieceAnima.restart();
		heroDoc.moveToTarget(target);
		
	}*/
	
	/*public void ComboGamora1MoveByTween(GameObject target, Vector3 moveToPos, string completeFunStr)
	{
		iTween.MoveTo
		(
			target,
			iTween.Hash
				(
					"position", moveToPos,
					"time", 0.2,
					"easetype", "easeOutExpo",
					"oncomplete", completeFunStr,
					"onCompleteTarget", this.gameObject
				)
		);
	}
	
	
	public void showComboGamora1Eft()
	{
		GameObject scene = gameObjects[0] as GameObject;
		GameObject caller = gameObjects[1] as GameObject;
		// GameObject target = gameObjects[2] as GameObject;
		
		Vector3 comboGamora1EftlocalScale = Vector3.zero;
		float comboGamora1EftPositionX = 0.0f;
		comboGamora1EftlocalScale = new Vector3(7, 7, 1);
		
		Character heroDoc = caller.GetComponent<Character>();	
		
		if(heroDoc.model.transform.localScale.x > 0)
		{
			comboGamora1EftPositionX = heroDoc.transform.position.x - 100;
		}
		else
		{
			comboGamora1EftPositionX = heroDoc.transform.position.x + 100;
		}
		
		if(comboGamora1EftPre == null)
		{
			comboGamora1EftPre = Resources.Load("eft/Drax/Combo_DRAX1") as GameObject;
		}
			
		Transform eftParent = heroDoc.model.transform.FindChild("MEDIUM_Weapon_01");
		
		if(eftParent == null)
		{
			Debug.LogError("MEDIUM_Weapon_01 not fond");
		}
		else
		{
			comboGamora1Eft = (GameObject)Instantiate(
				comboGamora1EftPre,
				new Vector3
				(
				comboGamora1EftPositionX,
				heroDoc.transform.position.y + 30,
				-100
				),
				heroDoc.transform.rotation
			);
			comboGamora1Eft.transform.parent = eftParent;
		
			comboGamora1Eft.transform.localScale = comboGamora1EftlocalScale;
		}
	}*/
}