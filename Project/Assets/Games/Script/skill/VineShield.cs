using UnityEngine;
using System.Collections;

public class VineShield : MonoBehaviour 
{
	
	public HPBar hpBar;
	
	public int currentHP;
	
	public BoneGROOT5A_VineShield_Front vineShieldFront;
	public BoneGROOT5A_VineShield_Behind vineShieldBehind;
	
	public bool isVineShieldFrontAnimaPlayEnd = false;
	public bool isVineShieldBehindAnimaPlayEnd = false;
	
	public Hero targetHero;
	
	public void init(int maxHP, Hero targetHero)
	{
		this.targetHero = targetHero;
		this.targetHero.hurtBeforeState = Character.HurtBeforeState.NOTHURT;
		this.targetHero.isVineShield = true;
		this.targetHero.vineShield = this;
		this.currentHP = maxHP;
		this.isVineShieldFrontAnimaPlayEnd = false;
		this.isVineShieldBehindAnimaPlayEnd = false;
		initHPBar();
	}
	
	public void initHPBar()
	{		
		this.hpBar.transform.parent = targetHero.hpBar.transform;
		
		int damageRealWidth = targetHero.hpBar.getHPBarTextureWidth() - targetHero.hpBar.getHPBarRealWidth();
		if(damageRealWidth <  this.hpBar.getHPBarTextureWidth())
		{
			this.hpBar.transform.localRotation = new Quaternion(0,0,-180,0);
			this.hpBar.transform.localPosition = new Vector3(Mathf.Abs(targetHero.hpBar.hpObj[0].transform.localPosition.x), 2, 1);
		}
		else
		{
			this.hpBar.transform.localPosition = new Vector3(
				targetHero.hpBar.getHPBarTextureWidth() - damageRealWidth + targetHero.hpBar.hpObj[0].transform.localPosition.x, 
				0,
				1);
		}
		
		
		this.hpBar.transform.localScale = Vector3.one;
				
		this.hpBar.initBar(this.currentHP);
		if(this.targetHero.hpBar.isHidenHpBar())
		{
			this.hpBar.hideHpBar();
		}
		else
		{
			this.hpBar.showHpBar();
		}
	}
	
	public int realDamage(int damage)
	{
		int remainHP = this.currentHP - damage;
		this.currentHP = remainHP;
		this.hpBar.ChangeHpTo(this.currentHP);
		
		
		
		if(remainHP <= 0)
		{
			this.targetHero.realDamage(remainHP);
			this.battleEnd();
		}
		return remainHP;
	}
	
	public void destroySelf()
	{
		if(this.isVineShieldFrontAnimaPlayEnd && this.isVineShieldBehindAnimaPlayEnd)
		{
			Destroy(hpBar.gameObject);
			Destroy(gameObject);
			
		}
	}
	
	public void battleEnd()
	{
		this.targetHero.vineShield = null;
		this.targetHero.hurtBeforeState = Character.HurtBeforeState.HURT;
		this.targetHero.isVineShield = false;
		vineShieldFront.restart();
		vineShieldBehind.restart();
	}
}
