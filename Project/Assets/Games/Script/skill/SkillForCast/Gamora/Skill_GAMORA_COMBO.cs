using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_GAMORA_COMBO : SkillBase
{
	protected ArrayList gameObjects;
	
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
			showComboGamora1Eft();
//			enemies[0].realDamage((int)(((float)heroDoc.realAtk * 2.2f)));
			int damage = enemies[0].getSkillDamageValue(heroDoc.realAtk, 220f);
			enemies[0].realDamage(damage);	
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
		heroDoc.setDepth();
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
	
	public void ComboGamora1MoveByTween(GameObject target, Vector3 moveToPos, string completeFunStr)
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
			
		Transform eftParent = heroDoc.model.transform.FindChild("FEMALE_Weapon_01");
		
		if(eftParent == null)
		{
			Debug.LogError("FEMALE_Weapon_01 not fond");
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
	}
}
