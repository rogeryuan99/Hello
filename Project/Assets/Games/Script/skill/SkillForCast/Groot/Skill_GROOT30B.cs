using UnityEngine;
using System.Collections;

public class Skill_GROOT30B : SkillBase {

	protected ArrayList grootObj;
	
	protected GameObject rotateEftPrb;
	protected GameObject rotateEft;
	
	protected GameObject treePrb;
	protected GameObject tree;
	
	protected GameObject fruitExplodePrb;
	
	
	protected BoneGROOT30B_Tree boneGROOT30B_Tree;
	
	public override IEnumerator Cast(ArrayList objs)
	{
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		GRoot heroDoc = caller.GetComponent<GRoot>();
		heroDoc.SkillKeyFrameEvent += hidenSelf;
		heroDoc.castSkill("GROOT30B");// Hero.castSkill
		
		
		grootObj = objs;
		
		yield return new WaitForSeconds(0.0f);
		
		
	}
	
	public void hidenSelf(Character character)
	{
		(character as GRoot).SkillKeyFrameEvent -= hidenSelf;
		character.pieceAnima.pauseAnima();
		character.isHealthLocked = true;
		character.transform.localScale = Vector3.zero;
		
		if(rotateEftPrb == null)
		{
			rotateEftPrb = Resources.Load("eft/Groot/SkillEft_GROOT30B_RotateEft") as GameObject;;
		}
		
		rotateEft = Instantiate(rotateEftPrb) as GameObject;
		PackedSprite rotateEftPS = rotateEft.GetComponent<PackedSprite>();
		rotateEft.transform.position = character.transform.position + new Vector3(0, 100, 0);
		
		rotateEftPS.SetAnimCompleteDelegate(rotateFinish);
	}
		
	public void rotateFinish(SpriteBase sprite)
	{
		Destroy(sprite.gameObject);
	
		GameObject caller = grootObj[1] as GameObject;
		
		if(treePrb == null)
		{
			treePrb = Resources.Load("eft/Groot/SkillEft_GROOT30B_Tree") as GameObject;;
		}
		
		GRoot heroDoc = caller.GetComponent<GRoot>();
		
		tree = Instantiate(treePrb) as GameObject;
		tree.transform.position = heroDoc.transform.position + new Vector3(-4, 70, 0);
		
		boneGROOT30B_Tree = tree.GetComponent<BoneGROOT30B_Tree>();
		boneGROOT30B_Tree.ShowEnemyEft += ShowEnemyEft;
		boneGROOT30B_Tree.SkillKeyFrameEvent += treeShowFinish;
	}
	
	public void treeShowFinish(BoneGROOT30B_Tree tree)
	{
		tree.SkillKeyFrameEvent -= treeShowFinish;
		GameObject caller = grootObj[1] as GameObject;
		GRoot heroDoc = caller.GetComponent<GRoot>();
		heroDoc.isHealthLocked = false;
		heroDoc.transform.localScale = new Vector3(0.23f, 0.23f, 1);
		heroDoc.pieceAnima.restart();
	}
	
	public void ShowEnemyEft(BoneGROOT30B_Tree tree)
	{
		tree.ShowEnemyEft -= ShowEnemyEft;
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("GROOT30B");
		
		Hashtable tempNumber = skillDef.buffEffectTable;
		
		int time = skillDef.buffDurationTime;
		float mspdValue = ((Effect)tempNumber["mspd"]).num;
		float aspdValue = ((Effect)tempNumber["aspd"]).num;
		
		if(fruitExplodePrb == null)
		{
			fruitExplodePrb = Resources.Load("eft/Groot/SkillEft_GROOT30B_FruitExplode") as GameObject;
		}
		
		foreach(Enemy enemy in EnemyMgr.enemyHash.Values)
		{
			if(enemy.getIsDead())
			{
				continue;
			}
			
			StartCoroutine(ShowFruitExplodeEft(enemy));
			
			StartCoroutine(addBuffToEnemy(enemy, time, mspdValue, aspdValue));
		}
	}
	
	public IEnumerator ShowFruitExplodeEft(Enemy enemy)
	{
		
		int fruitExplodeCount = Random.Range(3, 5);
		for(int i = 0; i < fruitExplodeCount; ++i)
		{
			Vector3 fruitExplodePosition = getPosInEnemyBody(enemy);
			GameObject fruitExplodeObj = Instantiate(fruitExplodePrb) as GameObject;
			fruitExplodeObj.transform.position = fruitExplodePosition + new Vector3(0, 0, enemy.transform.position.z - 10);
			PackedSprite fruitExplodePackedSprite = fruitExplodeObj.GetComponent<PackedSprite>();
			fruitExplodePackedSprite.animations[0].onAnimEnd = UVAnimation.ANIM_END_ACTION.Destroy;
			float s = Random.Range(1, 3) * 0.1f;
			yield return new WaitForSeconds(s);
			fruitExplodePackedSprite.PlayAnim(0);			
		}
	}
	
	public IEnumerator addBuffToEnemy(Enemy enemy, int time, float mspdValue, float aspdValue)
	{
		yield return new WaitForSeconds(0.3f);
		enemy.addBuff("Skill_GROOT30B_Mspd", time, -(mspdValue / 100.0f), BuffTypes.MSPD);
		enemy.addBuff("Skill_GROOT30B_Aspd", time, -(aspdValue / 100.0f), BuffTypes.ASPD);
	}
	
	public Vector3 getPosInEnemyBody(Enemy enemy)
	{
		BoxCollider bc = enemy.collider as BoxCollider;
		float randomX = UnityEngine.Random.Range(bc.bounds.min.x,bc.bounds.max.x);
		float randomY  = UnityEngine.Random.Range(bc.bounds.min.y,bc.bounds.max.y);
		return new Vector3(randomX, randomY,0);
	}
}
