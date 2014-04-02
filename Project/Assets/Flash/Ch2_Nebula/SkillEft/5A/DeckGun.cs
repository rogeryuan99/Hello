using UnityEngine;
using System.Collections;

public class DeckGun : MonoBehaviour {
	
	public BoneDeckGun model;
	private Object bulletPrefab;
	
	private float time;
	private float damage;
	private Vector6 chAtk;
	private float attackSpeed;
	private bool isTowardRight;
	private bool isNeedRotate;
	
	private const float fireDifY = 140f;
	private bool isAnimReady = false;
	private bool isTimeReady = true;
	private bool isEnemy = true;
	private bool IsCanFire{
		get{
			bool result = false;
			if (isAnimReady && isTimeReady){
				if(isEnemy){
					ArrayList heroList = new ArrayList(HeroMgr.heroHash.Values);
					foreach(Character hero in heroList){
						if (Mathf.Abs(hero.transform.position.y - transform.position.y) < fireDifY){
							result = true;
							if (isTowardRight == hero.transform.position.x > transform.position.x){
								isNeedRotate = false;
								break;
							}
							isNeedRotate = true;
						}// end if
					}// end foreach
				}else{
					ArrayList enemyList = new ArrayList(EnemyMgr.enemyHash.Values);
					foreach(Character enemy in enemyList){
						if (Mathf.Abs(enemy.transform.position.y - transform.position.y) < fireDifY){
							result = true;
							if (isTowardRight == enemy.transform.position.x > transform.position.x){
								isNeedRotate = false;
								break;
							}
							isNeedRotate = true;
						}// end if
					}// end foreach
				}
			}// end if
			return result;
		}
	}
	
	public void Init(int time_, float damage_, Character c, float attackSpeed_){
		isEnemy = (c is Ch2_Nebula)? true : false;
		time = time_;
		damage = damage_;
		chAtk = c.realAtk;
		if(c is Ch2_Nebula){
			Ch2_Nebula nebula = c as Ch2_Nebula;
			attackSpeed = nebula.showSkill10APassive(attackSpeed_);
		}else if(c is Nebula){
			Nebula nebula = c as Nebula;
			if(nebula.canShowNebulaPassive("NEBULA10A")){
				attackSpeed = nebula.showSkill10APassive(attackSpeed_);
			}else{
				attackSpeed = attackSpeed_;	
			}
		}
		//attackSpeed = attackSpeed_;
		Invoke("DestroyMySelf", time);
		isTowardRight = model.transform.localScale.x > 0;
		
		if (null == bulletPrefab){
			bulletPrefab = Resources.Load("eft/Nebula/SkillEft_NEBULA5A_Bullet");
		}
	}
	
	public void Start(){
		model.OnMoveEnd = OnMoveEnd;
		model.OnAttackEnd = OnAttackEnd;
	}
	
	public void Update(){
		if (IsCanFire){
			StartCoroutine(Fire());
		}
	}
	
	private void FixedUpdate(){
		if (StaticData.isBattleEnd){
			DestroyMySelf();
		}
//		else if (IsCanFire){
//			
//		}
	}
	
	private void OnMoveEnd(){
		model.pauseAnima();
		isAnimReady = true;
		isTimeReady = true;
	}
	
	private void OnAttackEnd(){
		isAnimReady = true;
		model.pauseAnima();
		Invoke("OnTimeReady", attackSpeed);
	}
	private void OnTimeReady(){
		isTimeReady = true;
	}
	
	
	private IEnumerator Fire(){
		isTimeReady = false;
		isAnimReady = false;
		
		if (isNeedRotate){
			transform.localScale = new Vector3((isTowardRight? -1f: 1f), 1f, 1f);
			isTowardRight = !isTowardRight;
		}
		model.playAct("Attack");
		yield return new WaitForSeconds(.5f);
		
		CreateBullet();
	}
	
	private void CreateBullet(){
		Vector3 createPos = transform.position + new Vector3((isTowardRight? 100f: -100f),80f,-0f);
		Vector3 targetPos = transform.position + new Vector3((isTowardRight? Screen.width: -Screen.width),80f,0f);
		GameObject bulletObj = Instantiate(bulletPrefab, createPos, transform.rotation) as GameObject;
		
		Bullet b = bulletObj.GetComponent<Bullet>();
		//b.targetType = Bullet.TARGETTYPE.ENEMY;
		b.targetType = isEnemy? Bullet.TARGETTYPE.HERO : Bullet.TARGETTYPE.ENEMY;
		b.attack = (int)(chAtk.PHY + damage);
		b.shootBullet(createPos, targetPos);
	}
	
	
	private void DestroyMySelf(){
		CancelInvoke("OnTimeReady");
		CancelInvoke("DestroyMySelf");
		Destroy(gameObject);
	}
}
