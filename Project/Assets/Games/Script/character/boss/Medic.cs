using UnityEngine;
using System.Collections;

public class Medic : EnemyRemote {
	public GameObject healPrb;
	public GameObject targetEnemy; 
	private int defenseAtkNum=0;
	private bool  isStart = false;
	public override void Awake (){
		base.Awake();
		atkAnimKeyFrame = 19;
	}
	
	public override void Start (){
		base.Start();
		pieceAnima.addFrameScript("Move",20,startMoveOut); 
		//pieceAnima.addFrameScript("Move",26,specialAtkExplosion);
	}
	
	public override void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
	
	public void startHeal (){ 
		InvokeRepeating("healTarget", realAspd, realAspd);
	} 
//	public function relive()
//	{
//		gameObject.transform.position = birthPt;
//		hpBar.resetView();
//		setDepth();	
//		isDead = false;
//		data.isDead = false;
//		initData(data);
//		this.gameObject.collider.enabled = true;
//		toggleEnable(); 
//		
//	}
	public override void toggleEnable (){
		id = EnemyMgr.getID();
		EnemyMgr.enemyHash[id] = this;
		
		Invoke("startHeal",1);
//		Enemy enemy =EnemyMgr.getMinHpEnemy(); 
//		if(enemy)
//		{
//			targetEnemy = enemy.gameObject;
//			startHeal();
//		}
	}
	//override
	public override void move ( Vector3 vc3  ){
		targetPt = vc3;
		state = MOVE_STATE;
		//playAnim("Move");
		setDirection(vc3);
		toward(vc3);
	}
	
	//override
	public override void setTarget ( GameObject gObj  ){
		
	}
	
	public void stopMoving (){
		if(state != ATK_STATE)
		{
			standby();
		}
	}
	// weapon change test
	protected void drawAvatar (){
//		EquipData eD = EquipFactory.lib[4];
//		HeroData heroD = data;
//		heroD.equipObj(EquipData.WEAPON, eD);
		//super.drawAvatar();
	}
	public override void moveToTarget ( GameObject obj  ){
		targetObj = obj;
		//playAnim("Move");
		state = MOVE_TARGET_STATE;
	}
	
//	protected void drawAvatar (){}
	protected override void moveTargetInAtkUpdate (){}
	
	protected override void atkAnimaScript (string s){ 
		if(targetEnemy != null){
			Debug.Log("realHp------->"+realHp);
			MusicManager.playEffectMusic("atk_healer");
			Enemy enemy = targetEnemy.GetComponent<Enemy>();
			if(! enemy.getIsDead()){
//				enemy.addHp(realAtk);
				Vector3 pt = enemy.transform.position + new Vector3(0,10,0);
				GameObject healingObj = Instantiate(healPrb, pt, this.transform.rotation) as GameObject;
				Debug.Log("atkAnimaScript enemy hp----"+realAtk);
			}
		}
	} 
	public void startMoveOut (string s){
		if(isDead){
			return;
		}
		standby();
		if(! isStart){
			isStart = true;
		}else{
			if(IsInvoking("healTarget"))
			{
				CancelInvoke("healTarget");
			}
//			print("start move  ----------->");
			this.gameObject.collider.enabled = true;
		 	this.gameObject.transform.position = new Vector3(-1000, 0, 0);
			Invoke("changePosition", 0.5f);
			defenseAtkNum = 0; 		
		}
	}
	private void changePosition (){ 
		if(isDead){
			return;
		}
		Vector2 vc2 = BattleBg.getPointInScreen();
		Vector3 ve = new Vector3(vc2.x, vc2.y,transform.position.y/10 + StaticData.objLayer);
		this.gameObject.transform.position=ve;
		setDepth();
		isStart = false;
		playAnim("Move"); 
		InvokeRepeating("healTarget",1, realAspd);
	}
	
	private void healTarget (){ 
		Debug.Log("healTarget----->");
		if(isDead){
			cancelAtk();
			return;
		}
		state = ATK_STATE; 
		Enemy enemy =EnemyMgr.getMinHpEnemy(); 
		if(enemy != null){		
				targetEnemy = enemy.gameObject;	
				if(!enemy.getIsDead())
				{
					toward(targetEnemy.transform.position);
					isPlayAtkAnim = true;
					playAnim("Attack");
				}		
		}
	} 
	
	public override void cancelAtk ()
	{
		if(IsInvoking("healTarget"))
		{
			CancelInvoke("healTarget");
		} 
	}
	
	public override void dead (string s=null){
		base.dead();
	}
		
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  ){ 
		if(isDead)return 0;
		int dam = base.getDamageValue(damage);
		Debug.Log(dam+"    realHp---->"+realHp);
		realHp -= dam;
		Debug.Log("realHp---->"+realHp);
		showHpBar();
		defenseAtkNum ++;
		if(realHp<= 0)
		{
			dead();
		}else{
			if(defenseAtkNum > 1){
				if(! isDead){ 
					 //this.gameObject.collider.enabled = false;
					 Hero hero = atkerObj.GetComponent<Hero>();
					 hero.setTarget(null);
					if(IsInvoking("healTarget"))
					{
						CancelInvoke("healTarget");
					}
					 playAnim("Move"); 			 			 
				}
			} 
			else {
					if (!isPlayAtkAnim 
							&& state != CAST_STATE 
							&& state != MOVE_TARGET_STATE 
							&& state != MOVE_TARGET_DIRECTLY_STATE
							&& state != MOVE_STATE){
						playAnim("Damage");
					}
			}
		}
		checkAtkerDefense(atkerObj);
		return dam;
	}
	protected override void AnimaPlayEnd ( string animaName  ){
//		Debug.Log(">>>>>>animaName:"+animaName);
		switch(animaName)
		{  
			case "Attack":
				playAnim("Stand"); 
				break;
			case "Damage": 
				playAnim("Stand"); 
				break;
			case "Death":
				pieceAnima.pauseAnima(); 
				defenseAtkNum = 0;
				Invoke("destroyThis",3); 
				CancelInvoke("healTarget");
//				print("death-------->");
				break;
			default:
				break;
		}
	}

}
