using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_STARLORD30A : SkillBase {

	private GameObject fireBulletPrb;
	private GameObject fireBulletTarget;
	
	private GameObject fireBullet;
	private bool isProgressingOnUpdate = false;
	
	private int fireBlastDamage;
	private int time;
	private const int maxMovingCount = 2;
	private const int invalidCount = -1;
	private int hitAtCount = 0;
	private int curCount = invalidCount;
	private bool isEnemyHigher = false;
	private float maxHightPerMoving = BattleBg.actionBounds.size.y / (float)(maxMovingCount);
	
	private List<GameObject> effects = new List<GameObject>();
	private Hashtable targets = new Hashtable();
	private ArrayList parms;
	
	public override IEnumerator Cast (ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		parms = objs;
		MusicManager.playEffectMusic("SFX_StarLord_Fire_Storm_1a");
		fireBulletTarget = target;
		
		Hero heroDoc = caller.GetComponent<Hero>();
		
		heroDoc.toward(target.transform.position);
		heroDoc.castSkill("Skill30A");
		yield return new WaitForSeconds(1.265f);
					
		SkillDef skillDef =  SkillLib.instance.getSkillDefBySkillID("STARLORD30A");
		
		Hashtable tempNumber = skillDef.activeEffectTable;
		float tempAtkPer = (int)((Effect)tempNumber["atk_PHY"]).num;
		fireBlastDamage = target.GetComponent<Character>().getSkillDamageValue(heroDoc.realAtk, tempAtkPer);
		time = (int)skillDef.skillDurationTime;
		createBullets(objs);
	}
	
	private Vector2 getActionBouncePoint(){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		Vector2 actionPoint = BattleBg.actionBounds.center;
		Vector2 halfSize = BattleBg.actionBounds.size/2f + new Vector3(10f, 0f, 0f);
		
		if (target.transform.position.y > caller.transform.position.y){
			if (target.transform.position.x > caller.transform.position.x){
				// right top
				actionPoint += halfSize;
			}
			else{
				// left top
				actionPoint = new Vector2(actionPoint.x-halfSize.x, actionPoint.y+halfSize.y);
			}
		}
		else{
			if (target.transform.position.x > caller.transform.position.x){
				// right bottom
				actionPoint = new Vector2(actionPoint.x+halfSize.x, actionPoint.y-halfSize.y);
			}
			else{
				// left bottom
				actionPoint -= halfSize;
			}
		}
		
		return actionPoint;
	}
	private int getHitCount(Vector2 acPoint){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		Vector2 actionPoint = acPoint - (Vector2)caller.transform.position;
		
		float k = actionPoint.y / actionPoint.x;
		float absY = Mathf.Abs(target.transform.position.y);
		int hitAt = (Mathf.Abs(target.transform.position.x * k) < absY
						|| Mathf.Abs(caller.transform.position.y - target.transform.position.y) > maxHightPerMoving)
						// ? (absY > Mathf(actionPoint.y/2f)? 2: 1)
						? 1: 0;
		return hitAt;
	}
	
	private void createBullets(ArrayList objs){
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		Vector2 acPoint = getActionBouncePoint();
		hitAtCount = getHitCount(acPoint);
		Vector3 createPt = (caller.GetComponent<Hero>().model.transform.localScale.x > 0)
							? caller.transform.position + new Vector3(60,0,0)
							: caller.transform.position + new Vector3(-60,0,0);
		Vector3 targetPt = new Vector3(acPoint.x, target.transform.position.y, target.transform.position.z);
		// Debug.LogError("****************************");
		curCount = 0;
		isEnemyHigher = target.transform.position.y > caller.transform.position.y;
		if (hitAtCount > 0){
			targetPt = getTargetPoint(acPoint.x, createPt);
		}
		
		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.3f));
		if(fireBulletPrb == null) {
			fireBulletPrb = Resources.Load("eft/StarLord/30A_FireStrom/StarLord30A_FireStorm_Fireball") as GameObject;
		}
		fireBullet = Instantiate(fireBulletPrb,createPt, transform.rotation) as GameObject;
		fireBullet.transform.localScale = Vector3.zero;
		iTween.ScaleTo(fireBullet, Vector3.one, .2f);
		shootFireBullet(targetPt);
	}
	
	private Vector3 getTargetPoint(float edgeX, Vector3 curPoint){
		GameObject caller = parms[1] as GameObject;
		GameObject target = parms[2] as GameObject;
		
		Vector3 adjustPt = target.transform.position - curPoint;
		float adjustY = adjustPt.y/adjustPt.x * (edgeX - curPoint.x);
		// Debug.LogError(string.Format("adjustPt is {0}, edgeX is {1}, adjustY is {2}", adjustPt, edgeX, adjustY));
		if (Mathf.Abs(adjustY) > maxHightPerMoving){
			// Debug.LogError(string.Format("AdjustY is {0}, After is {1}", adjustY, (true == isEnemyHigher? maxHightPerMoving: -maxHightPerMoving)));
			// Debug.LogError(string.Format("- isEnemyHigher is {0}, target.transform.position.y is {1}, curPoint.y is {2}", isEnemyHigher, target.transform.position.y, curPoint.y));
			adjustY = ((true == isEnemyHigher && target.transform.position.y > curPoint.y)? maxHightPerMoving: -maxHightPerMoving);
			if (Mathf.Abs(adjustY) > Mathf.Abs(target.transform.position.y - curPoint.y)){
				adjustY = target.transform.position.y - curPoint.y;
				// Debug.LogError(string.Format("targetPosY is {0}, curPointY is {1}, adjustY is {2}", target.transform.position.y, curPoint.y, adjustY));
			}
			hitAtCount+= (curCount == hitAtCount)? 1: 0;
		}
		if ((true == isEnemyHigher) != (adjustY > 0)){
			adjustY = 0;
		}
		return new Vector3(edgeX, adjustY + curPoint.y, adjustY/10f+StaticData.objLayer);
	}
	
	private void FixedUpdate(){
		if (false == isProgressingOnUpdate 
				&& curCount > 0 
				&& null != fireBullet 
				// && BattleBg.IsOutOfActionBounce(fireBullet.transform.position)){
				&& (fireBullet.transform.position.x > BattleBg.actionBounds.max.x 
					|| fireBullet.transform.position.x < BattleBg.actionBounds.min.x)){
			if (curCount > maxMovingCount){
				Destroy(fireBullet);
				fireBullet = null;
				curCount = invalidCount;
			}
			else {
				isProgressingOnUpdate = true;
				iTween.Stop(fireBullet);
				Vector3 targetPoint = getTargetPoint(BattleBg.actionBounds.center.x - (fireBullet.transform.position.x - BattleBg.actionBounds.center.x),
														fireBullet.transform.position);
				shootFireBullet(targetPoint);
				Invoke("delayOpenUpdateSwitch", .2f);
			}
		}
		if (null != fireBullet){
			ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
			foreach(Enemy enemy in enemyList){
				if (StaticData.isInOval(50f, 50f, (Vector2)(enemy.transform.position - fireBullet.transform.position)) 
					&& false == enemy.buffHash.ContainsKey("STARLORD30A")){
					enemy.addBuff("STARLORD30A",time,100,BuffTypes.DE_HP);
					// enemy.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
					// enemy.fireWithSeconds(time);
					ShowEnemyOnFireEft(enemy);
//					enemy.fireWithSeconds();
					
					State s = new State(time, DestroySkillEft);
					enemy.addAbnormalState(s, Character.ABNORMAL_NUM.FIRE);
				}
			}
		}
	}
	
	private void delayOpenUpdateSwitch(){
		isProgressingOnUpdate = false;
	}
	
	
	private void shootFireBullet (Vector3 endVc3){
		float dis_y = endVc3.y - fireBullet.transform.position.y;
		float dis_x = endVc3.x - fireBullet.transform.position.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		float deg = (angle*360)/(2*Mathf.PI);
		float ry = Mathf.Abs(deg)>90? 180f: 0f;
		// deg = (180f == ry)? (deg > 0? 180-deg: -180-deg): deg;
		
		// fireBullet.transform.rotation = Quaternion.Euler(new Vector3(0,ry,deg));
		fireBullet.transform.rotation = Quaternion.Euler(new Vector3(0,ry,0));
		iTween.MoveTo(fireBullet,new Hashtable(){{"x",endVc3.x},{ "y",endVc3.y},{"z",(endVc3.y/10f+StaticData.objLayer)},{ "speed",1200},{ "easetype","linear"}});// ,{ 
								// "oncomplete",oncompleteFunStr},{ "oncompletetarget",gameObject},{ "oncompleteparams",fireBullet}});
		curCount++;
	}
	private void ShowEnemyOnFireEft (Character enemy){
		
//		enemy.addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
		
		if(SkillEft_STARLOAD1_FireBlast_Prb == null)
		{
			SkillEft_STARLOAD1_FireBlast_Prb = Resources.Load("eft/StarLord/30A_FireStrom/StarLord30A_FireStorm_Fire") as GameObject;
		}  
		
		GameObject front = Instantiate(SkillEft_STARLOAD1_FireBlast_Prb) as GameObject;
		front.transform.parent = enemy.transform;
		front.transform.localPosition = new Vector3(0,600,-15);
		
		GameObject behind = Instantiate(SkillEft_STARLOAD1_FireBlast_Prb) as GameObject; 
		behind.transform.parent = enemy.transform;
		behind.transform.localPosition = new Vector3(0,600,100);
		
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
	
	
	

	protected GameObject SkillEft_STARLOAD1_FireBlast_Prb;
//	protected GameObject fireEftFront;
//	protected GameObject fireEftBehind;
	
	public void DestroySkillEft(State s, Character charater)
	{
//		charater.removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj, DestroySkillEft);
//		Destroy(fireEftFront);
//		Destroy(fireEftBehind);
		
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
}
