using UnityEngine;
using System.Collections;

public class Skill_DRAX30A : SkillBase {
	
	protected Drax drax;
	
	protected ArrayList objs;
	
	protected GameObject skill30EftPrb;
	
	protected GameObject skill30BodyEft_FrontPrb;
	protected GameObject skill30BodyEft_BehindPrb;
	
	protected GameObject skill30BodyEft_Front;
	protected GameObject skill30BodyEft_Behind;
	
	public override IEnumerator Cast(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		
		this.objs = objs;
		
		Drax drax = caller.GetComponent<Drax>();
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("DRAX30A");
		int skillDurationTime = skillDef.skillDurationTime;
		drax.castSkill("Skill30");
		drax.attackAnimaName = "Skill30A";
		drax.showSkill30EftCallback += showEft;
		drax.showSkill30BodyEftCallback += showBodyEft;
		drax.attackAnimaEvent += Skill_DRAX30AEffect;
		
		drax.addBuff("Skill_DRAX30A", skillDurationTime + 5, 0, BuffTypes.ATK_PHY, cancelSkill_DRAX30AEffect);
		
		this.drax = drax;
		
		yield return new WaitForSeconds(0.5f);	
		MusicManager.playEffectMusic("SFX_Drax_Common_1a");
	}
	
	public void showEft()
	{	
		GameObject caller = objs[1] as GameObject;
		Drax drax = caller.GetComponent<Drax>();
		drax.showSkill30EftCallback -= showEft;
		
		if(skill30EftPrb == null)
		{
			skill30EftPrb = Resources.Load("eft/Drax/SkillEft_DRAX30") as GameObject;
		}
		
		GameObject skill30Eft1 = Instantiate(skill30EftPrb) as GameObject;
		skill30Eft1.transform.position = drax.transform.position + new Vector3(0, 140, -1);
		skill30Eft1.transform.localScale = new Vector3(3.0f, 1.5f, 1);
		skill30Eft1.renderer.material.color = new Color32(255, 0, 0, 35);
		
		PackedSprite skill30Eft1Ps = skill30Eft1.GetComponent<PackedSprite>();
//		skill30Eft1Ps.SetAnimCompleteDelegate(skill30Eft1PackedSpriteAnimComplete);
		skill30Eft1Ps.PlayAnim(0);
		
		GameObject skill30Eft2 = Instantiate(skill30EftPrb) as GameObject;
		skill30Eft2.transform.position = drax.transform.position + new Vector3(0, 140, 0);
		skill30Eft2.transform.localScale = new Vector3(0.2f, 0.1f, 1);
		
		PackedSprite skill30Eft2Ps = skill30Eft2.GetComponent<PackedSprite>();
//		skill30Eft2Ps.SetAnimCompleteDelegate(skill30Eft2PackedSpriteAnimComplete);
		skill30Eft2Ps.PlayAnim(0);
		
		StartCoroutine(changeSkill30EftState1(skill30Eft1Ps, skill30Eft2Ps));
	}
	
	public IEnumerator changeSkill30EftState1(PackedSprite skill30Eft1Ps, PackedSprite skill30Eft2Ps)
	{
		int count = 5;
		int currnetCount = 0;
		while(currnetCount != count)
		{
			skill30Eft1Ps.transform.localScale -= new Vector3(0.2f, 0.1f, 0);
			skill30Eft1Ps.renderer.material.color += new Color32(0, 0, 0, 44);
			skill30Eft2Ps.transform.localScale += new Vector3(0.2f, 0.1f, 0);
			currnetCount++;
			yield return new WaitForSeconds(0.08f);
		}
		skill30Eft1Ps.transform.localScale = new Vector3(1.0f, 0.5f, 0);
		skill30Eft1Ps.renderer.material.color = new Color32(255, 0, 0, 255);
		skill30Eft2Ps.transform.localScale = new Vector3(1.0f, 0.5f, 0);
		
//		skill30Eft1Ps.PlayAnim(0);
		
		StartCoroutine(changeSkill30EftState2(skill30Eft1Ps, skill30Eft2Ps));
	}
	
	public IEnumerator changeSkill30EftState2(PackedSprite skill30Eft1Ps, PackedSprite skill30Eft2Ps)
	{
		int count = 5;
		int currnetCount = 0;
		while(currnetCount != count)
		{
			skill30Eft1Ps.transform.localScale += new Vector3(0.2f, 0.1f, 0);
			skill30Eft1Ps.renderer.material.color -= new Color32(0, 0, 0, 44);
			currnetCount++;
			yield return new WaitForSeconds(0.08f);
		}
		Destroy(skill30Eft1Ps.gameObject);
		
//		skill30Eft2Ps.PlayAnim(0);
		StartCoroutine(changeSkill30EftState3(skill30Eft2Ps));
	}
	
	public IEnumerator changeSkill30EftState3(PackedSprite skill30Eft2Ps)
	{
		int count = 5;
		int currnetCount = 0;
		while(currnetCount != count)
		{
			skill30Eft2Ps.transform.localScale += new Vector3(0.1f, 0.1f, 0);
			skill30Eft2Ps.renderer.material.color -= new Color32(0, 0, 0, 44);
			currnetCount++;
			yield return new WaitForSeconds(0.08f);
		}
		Destroy(skill30Eft2Ps.gameObject);
	}
	
	public void showBodyEft()
	{
		GameObject caller = objs[1] as GameObject;
		Drax drax = caller.GetComponent<Drax>();
		drax.showSkill30BodyEftCallback -= showBodyEft;
		
		if(skill30BodyEft_FrontPrb == null)
		{
			skill30BodyEft_FrontPrb = Resources.Load("eft/Drax/SkillEft_DRAX30_BodyEft_Front") as GameObject;
		}
		if(skill30BodyEft_BehindPrb == null)
		{
			skill30BodyEft_BehindPrb = Resources.Load("eft/Drax/SkillEft_DRAX30_BodyEft_Behind") as GameObject;
		}
		
		Destroy(skill30BodyEft_Front);
		Destroy(skill30BodyEft_Behind);
		
		skill30BodyEft_Front = Instantiate(skill30BodyEft_FrontPrb) as GameObject;
		skill30BodyEft_Behind = Instantiate(skill30BodyEft_BehindPrb) as GameObject;
		
		skill30BodyEft_Front.transform.parent = drax.transform;
		skill30BodyEft_Behind.transform.parent = drax.transform;
		
		skill30BodyEft_Front.transform.localScale = new Vector3(7,7,0);
		skill30BodyEft_Front.transform.localPosition = Vector3.zero;
		skill30BodyEft_Front.transform.localPosition += new Vector3(0, 1111, 0);
		
		skill30BodyEft_Behind.transform.localScale = new Vector3(7,7,0);
		skill30BodyEft_Behind.transform.localPosition = Vector3.zero;
		skill30BodyEft_Behind.transform.localPosition += new Vector3(0, 1117, 1);
		
		
		StartCoroutine(moveBodyEft());
	}
	
	public IEnumerator moveBodyEft()
	{
		yield return new WaitForSeconds(0.1f);
		int count = 5;
		int current = 0;
		while(current != count)
		{
			skill30BodyEft_Front.transform.localPosition -= new Vector3(0,160,0);
			skill30BodyEft_Behind.transform.localPosition -= new Vector3(0,161,0);
			current++;
			yield return new WaitForSeconds(0.12f);
		}
		
		skill30BodyEft_Front.transform.localPosition = new Vector3(0,366,0);
		skill30BodyEft_Behind.transform.localPosition = new Vector3(0,401,1);
	}
	
	public void Skill_DRAX30AEffect(Drax drax, Character target)
	{
		MusicManager.playEffectMusic("SFX_Drax_The_Destroyer_1a");
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("DRAX30A");
		int skillDurationTime = skillDef.skillDurationTime;
		int buffDurationTime = skillDef.buffDurationTime;
		Hashtable buffTable = skillDef.buffEffectTable;
		
		float per = ((Effect)buffTable["hp"]).num;
		int hp = (int)(target.realMaxHp * (per / 100.0f));
		
		if(StaticData.computeChance(50, 100))
		{
			target.addBuff("Skill_DRAX30A_DE_HP", buffDurationTime, hp / buffDurationTime, BuffTypes.DE_HP);
//			target.layDownWithSeconds();
			
			State s = new State(skillDurationTime, null);
			target.addAbnormalState(s, Character.ABNORMAL_NUM.LAYDOWN);
		}
		
		target.defenseAtk(drax.realAtk, drax.gameObject);
		
		
	}
	
	protected void cancelSkill_DRAX30AEffect(Character character, Buff self)
	{
		Debug.LogError("cancelSkill_DRAX30AEffect");
		Destroy(skill30BodyEft_Front);
		Destroy(skill30BodyEft_Behind);
		this.drax.attackAnimaName = "Attack";
		this.drax.attackAnimaEvent -= Skill_DRAX30AEffect;
	}
}
