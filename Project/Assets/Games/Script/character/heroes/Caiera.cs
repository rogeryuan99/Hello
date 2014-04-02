using UnityEngine;
using System.Collections;

public class Caiera : Hero
{
	public delegate void ParmsDelegate(Character character);
	
	public ParmsDelegate stretchSkill1ChainCallback;
	

	public ParmsDelegate showSkill5AEftCallback;
	
	public ParmsDelegate stretchSkill15AChainCallback;
	
	public ParmsDelegate skill15AFirstRotateTargetCallback;
	public ParmsDelegate showSkill15ADamageEftCallback;
	public ParmsDelegate skill15ASecondRotateTargetCallback;
	public ParmsDelegate skill15AFinishCallback;
	
	public ParmsDelegate showSkill30BHaloEftCallback;
	
	public ParmsDelegate showSkill30AAttackEftCallback;
	public ParmsDelegate showSkill30ADamageEftCallback;
	
	public override void Awake ()
	{
		base.Awake();
		atkAnimKeyFrame = 18;
	}
	
	public override void Start()
	{
		base.Start();
		pieceAnima.addFrameScript("SkillA", 18, stretchSkill1Chain);
		
		pieceAnima.addFrameScript("Skill5A", 16, showSkill5AEft);
		
		pieceAnima.addFrameScript("Skill15A", 18, stretchSkill15AChain);
		pieceAnima.addFrameScript("Skill15A", 42, skill15AFirstRotateTarget);
		pieceAnima.addFrameScript("Skill15A", 44, showSkill15ADamageEft);
		pieceAnima.addFrameScript("Skill15A", 50, skill15ASecondRotateTarget);
		pieceAnima.addFrameScript("Skill15A", 52, skill15AFinish);
		
		pieceAnima.addFrameScript("Skill30A", 15, showSkill30AAttackEft);
		pieceAnima.addFrameScript("Skill30A", 17, showSkill30ADamageEft);
		pieceAnima.addFrameScript("Skill30B", 13, showSkill30BHaloEft);
		
	}
	
	public void OnDestroy()
	{
		pieceAnima.removeFrameScript("SkillA", 18);
		
		pieceAnima.removeFrameScript("Skill5A", 16);
		
		pieceAnima.removeFrameScript("Skill15A", 18);
		pieceAnima.removeFrameScript("Skill15A", 42);
		pieceAnima.removeFrameScript("Skill15A", 44);
		pieceAnima.removeFrameScript("Skill15A", 50);
		pieceAnima.removeFrameScript("Skill15A", 52);
		
		
		pieceAnima.removeFrameScript("Skill30A", 15);
		pieceAnima.removeFrameScript("Skill30A", 17);
		pieceAnima.removeFrameScript("Skill30B", 13);
	}
	
	public void stretchSkill1Chain(string s)
	{
		if(stretchSkill1ChainCallback != null)
		{
			stretchSkill1ChainCallback(this);
		}
	}
	
	
	public void showSkill5AEft(string s)
	{
		if(showSkill5AEftCallback != null)
		{
			showSkill5AEftCallback(this);
		}
	}
	
	public void stretchSkill15AChain(string s)
	{
		if(stretchSkill15AChainCallback != null)
		{
			stretchSkill15AChainCallback(this);
		}
	}
	
	public void skill15AFirstRotateTarget(string s)
	{
		if(skill15AFirstRotateTargetCallback != null)
		{
			skill15AFirstRotateTargetCallback(this);
		}
	}
	
	public void showSkill15ADamageEft(string s)
	{
		if(showSkill15ADamageEftCallback != null)
		{
			showSkill15ADamageEftCallback(this);
		}
	}
	
	public void skill15ASecondRotateTarget(string s)
	{
		if(skill15ASecondRotateTargetCallback != null)
		{
			skill15ASecondRotateTargetCallback(this);
		}
	}
	
	public void skill15AFinish(string s)
	{
		if(skill15AFinishCallback != null)
		{
			skill15AFinishCallback(this);
		}
	}
	
	public void showSkill30AAttackEft(string s)
	{
		if(showSkill30AAttackEftCallback != null)
		{
			showSkill30AAttackEftCallback(this);
		}
	}
	
	public void showSkill30ADamageEft(string s)
	{
		if(showSkill30ADamageEftCallback != null)
		{
			showSkill30ADamageEftCallback(this);
		}
	}
	
	public void showSkill30BHaloEft(string s)
	{
		if(showSkill30BHaloEftCallback != null)
		{
			showSkill30BHaloEftCallback(this);
		}
	}
	
	protected override void atkAnimaScript (string s)
	{
		MusicManager.playEffectMusic("SFX_Drax_Basic_1a");
		if(skContainer.Count >= 1)
		{
			StartCoroutine(delayedCastSkill());
		}
		else	
		{
			base.atkAnimaScript("");
		}
	}
	
	public IEnumerator delayedCastSkill()
	{
		yield return new WaitForSeconds(0.01f); 
		SkillIconManager.Instance.CastSkill(this);
	}
	
	public override void battleEnd ()
	{
		this.attackAnimaName = "Attack";
		base.battleEnd ();
	}
	
	public override void dead(string s)
	{
		this.attackAnimaName = "Attack";
		base.dead();	
	}
}
