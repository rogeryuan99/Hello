using UnityEngine;
using System.Collections;

public class Hulk : Hero
{
	public GameObject attackEft;

	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 10;
	}
	
	public override void Start()
	{
		base.Start();
	}
	
	protected override void atkAnimaScript (string s)
	{
		
		if(skContainer.Count >= 1)
		{
			StartCoroutine(delayedCastSkill());
			return;
		} 
		
//		MusicManager.playEffectMusic("SFX_Gamora_Basic_1a");
		Vector3 eft;
		if(model.transform.localScale.x > 0)
		{
			eft = transform.position + new Vector3(200,80,-50);
		}else{
			eft = transform.position + new Vector3(-200,80,-50);
		}
		GameObject eftObj= Instantiate(attackEft,eft, transform.rotation) as GameObject;
		if(model.transform.localScale.x <= 0)
		{
			eftObj.transform.localScale = new Vector3(-eftObj.transform.localScale.x, eftObj.transform.localScale.y, eftObj.transform.localScale.z);
		}
		
		base.atkAnimaScript("");
	}
	
	public IEnumerator delayedCastSkill()
	{
		yield return new WaitForSeconds(0.01f); 
		SkillIconManager.Instance.CastSkill(this);
	}
}
