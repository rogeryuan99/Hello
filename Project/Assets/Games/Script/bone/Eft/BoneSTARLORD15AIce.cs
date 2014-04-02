using UnityEngine;
using System.Collections;

public class BoneSTARLORD15AIce : PieceAnimation
{

public GameObject ICE_A01;
public GameObject ICE_A02;
public GameObject ICE_A03;
public GameObject ICE_A04;
public GameObject ICE_A05;
public GameObject ICE_A06;
public GameObject ICE_A07;
public GameObject ICE_A08;
public GameObject ICE_B01;
public GameObject drop_shadow;
	
	protected Color oldColor;
	
	public override void Awake ()
	{ 		
		base.Awake();
		animaPlayEndScript(destroySelf);
		
		oldColor = gameObject.renderer.material.color;
		gameObject.renderer.material.color = Color.white;
		
		addFrameScript("SkillA", 15, changeStateColorFinished);
		
	}
	
	public void changeStateColorFinished(string s)
	{
		gameObject.renderer.material.color = oldColor;
	}
	
	protected override void initPartData ()
	{
		partList = new Hashtable();
		  partList ["ICE_A01"] =ICE_A01;
		  partList ["ICE_A02"] =ICE_A02;
		  partList ["ICE_A03"] =ICE_A03;;
		  partList ["ICE_A04"] =ICE_A04;
		  partList ["ICE_A05"] =ICE_A05;
		  partList ["ICE_A06"] =ICE_A06;
		  partList ["ICE_A07"] =ICE_A07;
		  partList ["ICE_A08"] =ICE_A08;
		  partList ["ICE_B01"] =ICE_B01;
		  partList ["ICE_B01__1"] =ICE_B01;
		  partList ["ICE_B01__2"] =ICE_B01;
		  partList ["ICE_B01__3"] =ICE_B01;
		  partList ["ICE_B01__4"] =ICE_B01;
		  partList ["ICE_B01__5"] =ICE_B01;;
		  partList ["ICE_B01__6"] =ICE_B01;
		  partList ["ICE_B01__7"] =ICE_B01;
		  partList ["ICE_B01__8"] =ICE_B01;
		  partList ["ICE_B01__9"] =ICE_B01;
		  partList ["drop_shadow"] =drop_shadow;
				
				
	}
	
	public void OnDestroy()
	{
		removeFrameScript("SkillA", 15);
	}
	
	protected void destroySelf (string s)
	{
		Destroy(this.gameObject);
	}
}
