using UnityEngine;
using System.Collections;

public class BoneBetaRayBill30ALighting : PieceAnimation
{          
	
    public GameObject SkillEft_BETARAYBILL30A_LIGHTING1;
	public GameObject SkillEft_BETARAYBILL30A_LIGHTING2;
	public GameObject SkillEft_BETARAYBILL30A_LIGHTING3;
	public GameObject SkillEft_BETARAYBILL30A_LIGHTING4;
	public GameObject SkillEft_BETARAYBILL30A_LIGHTING5;
	public GameObject SkillEft_BETARAYBILL30A_LIGHTING6;
	public GameObject SkillEft_BETARAYBILL30A_LIGHTING7;
	public GameObject SkillEft_BETARAYBILL30A_LIGHTING8;
	public GameObject SkillEft_BETARAYBILL30A_LIGHTING9;

	
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
		
		partList ["Special_effects_99c"] = SkillEft_BETARAYBILL30A_LIGHTING1;     
		partList ["Special_effects_100c"] = SkillEft_BETARAYBILL30A_LIGHTING2; 
		partList ["Special_effects_101c"] = SkillEft_BETARAYBILL30A_LIGHTING3; 
		partList ["Special_effects_102c"] = SkillEft_BETARAYBILL30A_LIGHTING4; 
		partList ["Special_effects_103c"] = SkillEft_BETARAYBILL30A_LIGHTING5; 
		partList ["Special_effects_104c"] = SkillEft_BETARAYBILL30A_LIGHTING6;
		partList ["Special_effects_105c"] = SkillEft_BETARAYBILL30A_LIGHTING7;
		partList ["Special_effects_106c"] = SkillEft_BETARAYBILL30A_LIGHTING8;
		partList ["Special_effects_107c"] = SkillEft_BETARAYBILL30A_LIGHTING9;
	}
	
	public void DestroyMySelf(string s){
		Destroy(gameObject);
	}
}
