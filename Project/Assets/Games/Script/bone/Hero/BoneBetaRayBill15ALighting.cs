using UnityEngine;
using System.Collections;

public class BoneBetaRayBill15ALighting : PieceAnimation
{          
	
    public GameObject SkillEft_BETARAYBILL15A_LIGHTING1;
	public GameObject SkillEft_BETARAYBILL15A_LIGHTING2;
	public GameObject SkillEft_BETARAYBILL15A_LIGHTING3;
	public GameObject SkillEft_BETARAYBILL15A_LIGHTING4;
	public GameObject SkillEft_BETARAYBILL15A_LIGHTING5;
	public GameObject SkillEft_BETARAYBILL15A_LIGHTING6;

	
	// Use this for initialization
	public override void Awake ()
	{
		animaPlayEndScript(DestroyMySelf);
		base.Awake ();
	}
	
	// Update is called once per frame
	protected override void initPartData ()
	{
		partList = new Hashtable ();
		
		partList ["Special_effects_92c"] = SkillEft_BETARAYBILL15A_LIGHTING1;     
		partList ["Special_effects_93c"] = SkillEft_BETARAYBILL15A_LIGHTING2; 
		partList ["Special_effects_94c"] = SkillEft_BETARAYBILL15A_LIGHTING3; 
		partList ["Special_effects_95c"] = SkillEft_BETARAYBILL15A_LIGHTING4; 
		partList ["Special_effects_96c"] = SkillEft_BETARAYBILL15A_LIGHTING5; 
		partList ["Special_effects_97c"] = SkillEft_BETARAYBILL15A_LIGHTING6; 
	}
	
	public void DestroyMySelf(string s){
		Destroy(gameObject);
	}
}
