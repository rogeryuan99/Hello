using UnityEngine;
using System.Collections;

public class BoneBetaRayBill5BLighting : PieceAnimation
{          
	
    public GameObject SkillEft_BETARAYBILL5B_LIGHTING1;
	public GameObject SkillEft_BETARAYBILL5B_LIGHTING2;
	public GameObject SkillEft_BETARAYBILL5B_LIGHTING3;
	public GameObject SkillEft_BETARAYBILL5B_LIGHTING4;
	public GameObject SkillEft_BETARAYBILL5B_LIGHTING5;
	public GameObject SkillEft_BETARAYBILL5B_LIGHTING6;

	
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
		
		partList ["Special_effects_10c"] = SkillEft_BETARAYBILL5B_LIGHTING1;     
		partList ["Special_effects_11c"] = SkillEft_BETARAYBILL5B_LIGHTING2; 
		partList ["Special_effects_12c"] = SkillEft_BETARAYBILL5B_LIGHTING3; 
		partList ["Special_effects_13c"] = SkillEft_BETARAYBILL5B_LIGHTING4; 
		partList ["Special_effects_14c"] = SkillEft_BETARAYBILL5B_LIGHTING5; 
		partList ["Special_effects_15c"] = SkillEft_BETARAYBILL5B_LIGHTING6; 
	}
	
	public void DestroyMySelf(string s){
		Destroy(gameObject);
	}
}
