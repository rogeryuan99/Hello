using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoneGROOT30B_Tree : PieceAnimation
{
	
public GameObject Spore_01;
	public GameObject Spore_011      ;
public GameObject Spore_0110			;
public GameObject Spore_0111     ;
public GameObject Spore_0112     ;
public GameObject Spore_0113			;
public GameObject Spore_0114     ;
public GameObject Spore_0115     ;
public GameObject Spore_012      ;
public GameObject Spore_013      ;
public GameObject Spore_014      ;
public GameObject Spore_015      ;
public GameObject Spore_016      ;
public GameObject Spore_017      ;
public GameObject Spore_018      ;
public GameObject Spore_019      ;

public GameObject branch_41;
public GameObject branch_42;
public GameObject branch_43;
public GameObject branch_44;
public GameObject branch_45;
public GameObject branch_46;
public GameObject branch_47;
public GameObject branch_48;
public GameObject drop_shadow;
	
	List<PackedSprite> fruitPackedSpriteList = new List<PackedSprite>(); 
	
	public delegate void ParmsDelegate(BoneGROOT30B_Tree tree);
	public event ParmsDelegate SkillKeyFrameEvent;
	
	public event ParmsDelegate ShowEnemyEft;

	public override void Awake ()
	{ 		
		base.Awake();
	  	PackedSprite[] ps = transform.GetComponentsInChildren<PackedSprite>();
		
		for(int i = 0; i < ps.Length; ++i)
		{
			if(ps[i].name.Contains("SkillEft_GROOT30B_FruitExplode"))
			{
				fruitPackedSpriteList.Add(ps[i]);
			}
		}
		addFrameScript("SkillA",14, fruitExplode);
		
//		addFrameScript("SkillA",20, skillKeyFrameEvent);
		
		animaPlayEndScript(destroySelf);
	}
	
	public void fruitExplode(string s)
	{
		pauseAnima();
		StartCoroutine(fruitExplode());
		
	}
	
	public IEnumerator fruitExplode()
	{
		fruitExplodeFinishCount = 0;
		foreach(PackedSprite fruitPackedSprite in fruitPackedSpriteList)
		{
			yield return new WaitForSeconds(0.1f);
			fruitPackedSprite.SetAnimCompleteDelegate(fruitExplodeFinish);
			fruitPackedSprite.PlayAnim(0);
		}
	}
	int fruitExplodeFinishCount = 0;
	
	public void fruitExplodeFinish(SpriteBase sprite)
	{
		fruitExplodeFinishCount++;
		if(fruitExplodeFinishCount == 2)
		{
			if(ShowEnemyEft != null)
			{
				ShowEnemyEft(this);
			}
		}
		else if(fruitExplodeFinishCount == fruitPackedSpriteList.Count)
		{
			restart();
		}
	}
	
	protected override void initPartData ()
	{
		partList = new Hashtable();
	  partList ["Spore_01"] =    Spore_01		;													
	  partList ["Spore_011"] =   Spore_011;
	  partList ["Spore_0110"] =  Spore_0110			;									
	  partList ["Spore_0111"] =  Spore_0111;
	  partList ["Spore_0112"] =  Spore_0112;
	  partList ["Spore_0113"] =  Spore_0113	;														
	  partList ["Spore_0114"] =  Spore_0114;
	  partList ["Spore_0115"] =  Spore_0115;
	  partList ["Spore_012"] =   Spore_012;
	  partList ["Spore_013"] =   Spore_013;
	  partList ["Spore_014"] =   Spore_014;
	  partList ["Spore_015"] =   Spore_015;
	  partList ["Spore_016"] =   Spore_016;
	  partList ["Spore_017"] =   Spore_017;;
	  partList ["Spore_018"] =   Spore_018;
	  partList ["Spore_019"] =   Spore_019;
	  partList ["branch_41"] =   branch_41;
	  partList ["branch_42"] =   branch_42;
	  partList ["branch_43"] =   branch_43;
	  partList ["branch_44"] =   branch_44;
	  partList ["branch_45"] =   branch_45;
	  partList ["branch_46"] =   branch_46;
	  partList ["branch_47"] =   branch_47;
	  partList ["branch_48"] =   branch_48;
	  partList ["drop_shadow"] = drop_shadow;
	}
	
	public override void OnDestroy()
	{
		base.OnDestroy();
	}
	
	public void destroySelf(string s)
	{
		if(SkillKeyFrameEvent != null)
		{
			SkillKeyFrameEvent(this);
		}
		Destroy(gameObject);
		
	}
}
