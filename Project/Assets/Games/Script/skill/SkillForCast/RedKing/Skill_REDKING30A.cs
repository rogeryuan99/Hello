using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_REDKING30A : SkillBase {
	protected GameObject shieldEft;
	protected GameObject haloEft;
	protected List<GameObject> PopEftList = new List<GameObject>();

	public override IEnumerator Cast(ArrayList objs){
		GameObject scene = objs[0] as GameObject;
		GameObject caller = objs[1] as GameObject;
		
		Character redKing = caller.GetComponent<Character>();
		redKing.castSkill("Skill30A");
		if (redKing is Hero){
			(redKing as RedKing).showSkill30EftCallback += showEft;
		}
		else{
			(redKing as Ch3_RedKing).showSkill30EftCallback += showEft;
		}
		
		yield return new WaitForSeconds(0f);
	}
	
	protected void showEft(Character c){
		Character redKing = c.GetComponent<Character>();
		if (redKing is Hero){
			(redKing as RedKing).showSkill30EftCallback -= showEft;
		}
		else{
			(redKing as Ch3_RedKing).showSkill30EftCallback -= showEft;
		}
		GameObject shieldEftPrefab = Resources.Load("eft/RedKing/SkillEft_RedKing30_ShieldRotationEft") as GameObject;
		shieldEft = Instantiate(shieldEftPrefab) as GameObject;
		shieldEft.transform.parent = c.transform;
		shieldEft.transform.localPosition = new Vector3(0,160,-30);
		shieldEft.transform.localScale = new Vector3(3f,3f,1f);
		SkillEft_RedKing30_ShieldRotationEft s = shieldEft.GetComponent<SkillEft_RedKing30_ShieldRotationEft>();
		if(s != null) StartCoroutine(delayShieldRotation(s));
		
		GameObject haloEftPrefab = Resources.Load("eft/RedKing/SkillEft_RedKing30_HaloEft") as GameObject;
		haloEft = Instantiate(haloEftPrefab) as GameObject;
		haloEft.transform.parent = c.transform;
		haloEft.transform.localPosition = new Vector3(0,0,1);
		haloEft.transform.localScale = new Vector3(3,3,1);
		
		GameObject bangEftPrefab = Resources.Load("eft/RedKing/SkillEft_RedKing30_BangEft") as GameObject;
		GameObject bangEft = Instantiate(bangEftPrefab) as GameObject;
		bangEft.transform.parent = c.transform;
		bangEft.transform.localPosition = new Vector3(100,1400,-20);
		bangEft.transform.localScale = new Vector3(8,8,1);
		
		restoreSelfHp(c);
		
		StartCoroutine(delayShowPopEft(c));
	}
	
	protected IEnumerator delayShieldRotation(SkillEft_RedKing30_ShieldRotationEft s){
		yield return new WaitForSeconds(0.3f);
		
		s.setToggle(true);
	}
	
	protected IEnumerator delayShowPopEft(Character c){
		GameObject popEftPrefab = Resources.Load("eft/RedKing/SkillEft_RedKing30_PopEft") as GameObject;
		GameObject popEft1 = Instantiate(popEftPrefab) as GameObject;
		popEft1.transform.parent = c.transform;
		popEft1.transform.localPosition = new Vector3(0,600,0);
		popEft1.transform.localScale = new Vector3(5,5,1);
		
		yield return new WaitForSeconds(0.2f);
		
		GameObject popEft2 = Instantiate(popEftPrefab) as GameObject;
		popEft2.transform.parent = c.transform;
		popEft2.transform.localPosition = new Vector3(-200,500,0);
		popEft2.transform.localScale = new Vector3(4,4,1);
		
		GameObject popEft3 = Instantiate(popEftPrefab) as GameObject;
		popEft3.transform.parent = c.transform;
		popEft3.transform.localPosition = new Vector3(200,500,0);
		popEft3.transform.localScale = new Vector3(4,4,1);	
		
		yield return new WaitForSeconds(0.2f);
		
		GameObject popEft4 = Instantiate(popEftPrefab) as GameObject;
		popEft4.transform.parent = c.transform;
		popEft4.transform.localPosition = new Vector3(-300,500,0);
		popEft4.transform.localScale = new Vector3(4,4,1);
		
		GameObject popEft5 = Instantiate(popEftPrefab) as GameObject;
		popEft5.transform.parent = c.transform;
		popEft5.transform.localPosition = new Vector3(300,500,0);
		popEft5.transform.localScale = new Vector3(4,4,1);	
		
		PopEftList.Add(popEft1);
		PopEftList.Add(popEft2);
		PopEftList.Add(popEft3);
		PopEftList.Add(popEft4);
		PopEftList.Add(popEft5);
	}
	
	protected void restoreSelfHp(Character c){
		Character redKing = c.GetComponent<Character>();
		redKing.addHp(redKing.realMaxHp);
		
		SkillDef skillDef = SkillLib.instance.getSkillDefBySkillID("REDKING30A");
		int durTime = skillDef.buffDurationTime;
		float per = ((Effect)skillDef.buffEffectTable["hp"]).num;
		int hp = (int)(redKing.realMaxHp * (per / 100.0f));
		redKing.addBuff("Skill_REDKING30A", durTime, hp/durTime, BuffTypes.HP, buffFinish);
	}
	
	protected void buffFinish(Character c, Buff self){
		Destroy(shieldEft);
		Destroy(haloEft);
		for(int n = PopEftList.Count-1;n >= 0;n--){
			Destroy(PopEftList[n].gameObject);
		}
		PopEftList.Clear();
	}
}
