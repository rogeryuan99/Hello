using UnityEngine;
using System.Collections;

public class enemyDruid : enemyWizard {
	public GameObject bearModel;
	public GameObject originalModel;
	public override void Awake (){
		model = originalModel;
		base.Awake();
		atkAnimKeyFrame = 14;
		bearModel.SetActiveRecursively(false);
//		Invoke("transmutation",5);
//		Invoke("toHuman",10);
	}
	//add by gwp at 20130219
//	public void setAbnormalState ( ABNORMAL_NUM abnormal  ){}
	
//	public function transmutation()
//	{
//		model = bearModel;
//		hpBar.gameObject.transform.position=new Vector3(0,200,0);
//		hpBar.gameObject.renderer.enabled = true;
//		originalModel.SetActiveRecursively(false);
//		bearModel.SetActiveRecursively(true);
//		setPieceAnima();
//		pieceAnima.addFrameScript("Attack",atkAnimKeyFrame,atkAnimaScript);
//	}
//	
//	public function toHuman()
//	{
//		model = originalModel;
//		originalModel.SetActiveRecursively(true);
//		bearModel.SetActiveRecursively(false);
//		setPieceAnima();
//		pieceAnima.addFrameScript("Attack",atkAnimKeyFrame,atkAnimaScript);
//	}
//	
//	protected function initSkill()
//	{
////		SkillData skD = SkillData.create("DRUID1","MonstersfromtheId",30);
////		SkillData skD2 = SkillData.create("DRUID6", "SuperEgoRestorative", 45);
////		print("##################" + skD.name);
//		heroData = (data as HeroData);
//		
//		heroData.skillList = ["Monsters from the Id","Super-Ego Restorative"];
//		heroData.passiveList = [];
//		
//		if(!isPuppet)
//		{
//			//(data as HeroData).skillListBattle = [skD,skD2];
//			heroData.buildActiveSkill();
//			heroData.buildPassiveSkill();
//		}
//		EquipCalculator.calculate(heroData);
//		
////		initData(SceneInit.heroDataList[3]);
////		(data as HeroData).isSelect = true;
//	}
//		protected function shootBullet( Vector3 creatVc3 ,   Vector3 endVc3  )
//	{
//		float dis_y = endVc3.y - creatVc3.y;
//		float dis_x = endVc3.x - creatVc3.x;
//		float angle = Mathf.Atan2(dis_y, dis_x);
//		//dirVc3 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
//		
//		bltObj = Instantiate(bulletPrb,creatVc3, transform.rotation);
//		
//		float deg = (angle*360)/(2*Mathf.PI);
//		bltObj.transform.rotation.eulerAngles = new Vector3(0,0, deg);
//		iTween.MoveTo(bltObj,{"x":endVc3.x, "y":endVc3.y,"z":endVc3.z-10, "speed":1500, "easetype":"linear", 
//								"oncomplete":"removeBullet", "oncompletetarget":gameObject});
//	}
//		protected function removeBullet()
//	{
//		PackedSprite bltObjInfo = bltObj.GetComponent<PackedSprite>();
//		bltObjInfo.PlayAnim("Explosion"); 
////		if(targetObj != null)
////		{
////			Enemy enemy = targetObj.GetComponent<Enemy>();
////			int dmg = enemy.defenseAtk(realAtk, this.gameObject);
////			trinketEfts(dmg);
////		}else{
////			if(enemy.getIsDead()){
////				standby();
////			}
////		}
//	}
}
