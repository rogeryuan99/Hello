using UnityEngine;
using System.Collections;

public class HPBar : MonoBehaviour {
public GameObject[] hpObj;

public bool  isHeadIconUsed=false;

private int maxHp;
private int hp;

private float maxhps;
private bool  isChange=false;
private bool isLowHealthOnce = false;

private const float MIN = 0.001f;	
	
public void Update (){
	if(isChange)
	{
		updateHpBar();
	}
}
	
	public int getHPBarRealWidth()
	{
		PackedSprite hpPS = hpObj[0].GetComponent<PackedSprite>();
		
		
		
		return (int)(hpPS.width * hpPS.transform.localScale.x);
	}
	
	public int getHPBarTextureWidth()
	{
		PackedSprite hpPS = hpObj[0].GetComponent<PackedSprite>();
		
		
		
		return (int)(hpPS.width);
	}

private void updateHpBar ()
	{
	maxhps = Mathf.Lerp(maxhps, hp, 0.5f);
	hpObj[1].transform.localScale = new Vector3(Mathf.Max(MIN, maxhps/maxHp), hpObj[1].transform.localScale.y, hpObj[1].transform.localScale.z);
//	hpObj[1].transform.localScale.x = maxhps/maxHp;
	if(Mathf.Abs(maxhps- hp) <= MIN)
	{
			
		isChange = false;
	}
	if(this.transform.parent.tag == "Player" && !isLowHealthOnce && hp <= 0.2*maxHp){
		MusicManager.playEffectMusic("SFX_hero_low_health_2a");		
		isLowHealthOnce = true;
	}
}

public void resetView (){
	hpObj[0].transform.localScale = new Vector3(1, hpObj[0].transform.localScale.y, hpObj[0].transform.localScale.z);
	hpObj[1].transform.localScale = new Vector3(1, hpObj[1].transform.localScale.y, hpObj[1].transform.localScale.z);
//	hpObj[0].transform.localScale.x = 1;
//	hpObj[1].transform.localScale.x = 1;
}
public void initBar ( int maxhp  ){
	this.maxHp = maxhp;
	maxhps = maxHp;
}
	
	public void showHpBar ()
	{
		setHideHpBar(false);
	}

	public void hideHpBar ()
	{
		setHideHpBar(true);
	}
	
	protected void setHideHpBar(bool b)
	{
		for(int i = 0; i < hpObj.Length; i++)
		{
			hpObj[i].GetComponent<PackedSprite>().Hide(b);
		}
	}
	
	public bool isHidenHpBar()
	{
		return hpObj[0].GetComponent<PackedSprite>().IsHidden();
	}

// get this gameObject hp
public IEnumerator ChangeHp ( int hp  ){
	this.hp = Mathf.Max(0,hp);
	float scaleValue = Mathf.InverseLerp(MIN, maxHp, hp);
//	print("--->"+scaleValue);
	hpObj[0].transform.localScale = new Vector3(scaleValue, hpObj[0].transform.localScale.y, hpObj[0].transform.localScale.z);
//	hpObj[0].transform.localScale.x = scaleValue;
	if(isHeadIconUsed || this.hp <= 0)
	{
		hpObj[1].transform.localScale = new Vector3(scaleValue, hpObj[1].transform.localScale.y, hpObj[1].transform.localScale.z);
//		hpObj[1].transform.localScale.x = scaleValue;
	}else{
		yield return new WaitForSeconds(0);
		isChange  = true;
	}
}
	
	// get this gameObject hp
	public void ChangeHpTo ( int hp  )
	{
		this.hp = Mathf.Max(0,hp);
		float scaleValue = Mathf.InverseLerp(MIN, maxHp, hp);
		hpObj[0].transform.localScale = new Vector3(scaleValue, hpObj[0].transform.localScale.y, hpObj[0].transform.localScale.z);
		
		
		maxhps = Mathf.Lerp(maxhps, hp, 0.05f);
		hpObj[1].transform.localScale = new Vector3(scaleValue, hpObj[1].transform.localScale.y, hpObj[1].transform.localScale.z);
		isChange = false;
	}

}