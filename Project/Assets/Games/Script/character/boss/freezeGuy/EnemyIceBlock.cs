using UnityEngine;
using System.Collections;

public class EnemyIceBlock : Enemy {
	private Hero hero;
	private ArrayList defensAtk = new ArrayList(){"FGB1","FGB2","FGB3","FGB4","FGB5"};
	private int defensAtkNum = 0;
	public override void Awake (){
		base.Awake();
//		id = EnemyMgr.getID();
//		EnemyMgr.enemyHash[id] = this;
		atkAnimKeyFrame = 13;
		CharacterData characterD = new CharacterData(new Hashtable(){{"type","freezeGuyEft"},{ "hp",500},{ "mspd",0},{ "aspd",0},{ "atk",0},{ "def",0},{ "rewardCoins",0},{ "rewardExp",0},{ "cstk",0},{ "evd",0},{ "stk",0}});
		initData(characterD);
		this.hpBar.hideHpBar();
//		MsgCenter.instance.addListener(MsgCenter.FREEZE_DEAD, heroDead);
	}
	
	public override void Start (){
		relive();
		pieceAnima.addFrameScript("FGB5",6,startDead); 
		pieceAnima.addFrameScript("Death",13,dead);
	//	pieceAnima.addFrameScript("Move",13,startMoveOut);
	}
	public override void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
	public void startDead (string s){
		playAnim("Death");
	}
	
	protected override void atkAnimaScript (string s){
		MusicManager.playEffectMusic("SFX_enemy_melee_attack_1b");
		base.atkAnimaScript("");
	}
	
	public override void relive (){	
		//setDepth();
		hpBar.resetView();
		isDead = false;
		data.isDead = false;
		initData(data);
		playAnim(defensAtk[defensAtkNum].ToString());
		this.gameObject.collider.enabled = true;
		InvokeRepeating("atkDamage",1,1);
		//toggleEnable();
	}
	private void atkDamage (){
//		hero.defenseAtk(40,this.gameObject);
	}
	public void setHero ( Hero h  ){
		this.hero=h;
	}
	public override void dead (string s=null){
		//super.dead();
		CancelInvoke("atkDamage");
		defensAtkNum = 0;
		if(this.hero != null)
		{
			
			this.hero=null;
			this.gameObject.transform.position = new Vector3(-1000, 0, 0);
			DestroyObject(this.gameObject);
		}
//		Debug.Log("freezeGuy dead------>");
	}
	public override void initData ( CharacterData characterD  ){
		data = characterD;
		realHp  = characterD.maxHp;
//		realDef = (int)characterD.defense;
//		realAtk = (int)characterD.attack;
		realMspd= characterD.moveSpeed;
		realAspd= characterD.attackSpeed;
		
		// delete by why 2014.2.7	
//		realCStk=characterD.criticalStk;
//		realEvade=characterD.evade;
//		realStk=characterD.strike;
		hpBar.initBar(characterD.maxHp);
	}
	public override void highLight (){}
	public override void cancelHighLight (){}
	
	public override void move ( Vector3 vc3  ){}
	
	public override void moveToTarget ( GameObject obj  ){}
	
	
	public override void setTarget ( GameObject gObj  ){
		targetObj = gObj;
	}
	public override string getTargetTagType (){
		return "freezeGuy";
	}
	// public override bool getIsDead (){
		// return isDead;
	// }
	
	public override int defenseAtk ( Vector6 damage ,   GameObject atkerObj  ){
		if(isDead)return 0;
		defensAtkNum++;
		if(defensAtkNum < defensAtk.Count)
		{
			playAnim(defensAtk[defensAtkNum].ToString());	
//			print(defensAtk[defensAtkNum]);	
		}else{}
	
		return 0;
	}
	
//	public function flash( float r ,   float g ,   float b  )
//	{
//		isPlayEft = true;
////		model.renderer.material.SetColor("_Color",Color.red);
//		iTween.ColorTo(model, {"r":r, "g":g,"b":b,"time":0.2f, "looptype":"pingPong", "easetype":"linear"});
//	}
//	public function unFlash()
//	{
//		if(isDead)return;
//		isPlayEft = false;
//		iTween.ColorTo(model, {"r":0.5f,"g":0.5f,"b":0.5f,"time":0.1f,"easetype":"linear"});
//	}
	
	//////////////////////////////////////
	////skill script use these function
	/////////////////////////////////////
//	public override void addAtk ( int atkNum  ){}
	public override void resetAtk (){}
	
//	public override void addDef ( int defNum  ){
//		realDef += defNum;
//	}
	public override void resetDef (){
//		realDef = (int)data.defense;
	}
	
	public void changeMoveSpd ( float per  ){
		realMspd  = data.moveSpeed*per;
	}
	public void changeAtkSpd ( float per  ){
		realAspd = data.attackSpeed*per;
	}
	
	public override void addHp ( int hpNum  ){
		realHp+= hpNum;
		realHp = Mathf.Min(data.maxHp,realHp);
		realHp = Mathf.Max(0,realHp);
		showHpBar();
	}
	
	public override void realDamage ( int dam  ){
		addHp(-dam);
		if(realHp<= 0){
			dead();
		}
		/*else{
			if (!isPlayAtkAnim &&  state != CAST_STATE && state != MOVE_TARGET_STATE && state != MOVE_STATE)
			{
			}
		}*/
	}
	
	public override void characterDeadMsg (){
	}
	//////////////////////////////////////
	
	public override void showHpBar ()
	{
	}
	public override void hideHpBar (){
//		hpBar.gameObject.SetActiveRecursively(false);
//		iTween.ScaleTo(hpBar.gameObject, {"x":0,"y":0,"z":0,"time":0.2f});
	}
	
	public override void toward ( Vector3 vc3  ){
		if(model.transform.position.x>vc3.x)
		{
			model.transform.localScale = new Vector3(-scaleSize.x,scaleSize.y,1);
		}else{
			model.transform.localScale = new Vector3(scaleSize.x,scaleSize.y,1);
		}
	}
	
	public override void setDirection ( Vector3 vc3  ){
		float dis_y = vc3.y - transform.position.y;
		float dis_x = vc3.x - transform.position.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		dirVc3 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
	}
	
	public override void setDepth (){
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y/10 + StaticData.objLayer-12);
//		transform.position.z = transform.position.y/10 + GData.objLayer-12;
	}
	public override void destroyThis (){
		this.gameObject.transform.position =new Vector3(-1000, 0, 0);
//		Destroy (this.gameObject);
	}
	public override void attackTargetInvok (){
	
	}
	protected override void moveStateUpdate (){
		float dis = Vector2.Distance(transform.position,targetPt);
		float spd = Time.deltaTime*realMspd;
		if(Mathf.Abs(dis)>spd){
			if(dirVc3.y != 0){
				setDepth();
			}
			transform.Translate(dirVc3*spd);
		}else{
			standby();
		}
	}
	void heroDead ( Message msg  ){
//		this.startDead();
		playAnim(defensAtk[4].ToString());
	}
	public override void moveTargetStateUpdate (){}
	protected override void moveTargetInAtkUpdate (){}

	void OnDestroy (){
//		MsgCenter.instance.removeListener(MsgCenter.FREEZE_DEAD, heroDead);
	}
}


