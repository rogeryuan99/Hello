using UnityEngine;
using System.Collections;

public class BoneSTARLORD15B_BlastGenerator : PieceAnimation {

  public GameObject Jar_a;
  public GameObject Jar_b;
  public GameObject Jar_c;
  public GameObject Light2_1a;
  public GameObject shield_generator_02w;
			
	
	public override void Awake ()
	{ 		
		base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	protected override void initPartData ()
	{
		partList = new Hashtable();
		partList ["Jar_a"] = Jar_a;
		partList ["Jar_b"] = Jar_b;
		partList ["Jar_c"] = Jar_c;
		partList ["Light2_1a"] = Light2_1a;
		partList ["shield_generator_02w"] = shield_generator_02w;
	}
	
	public void destroySelf(string s)
	{
		Destroy(gameObject);
	}
}
