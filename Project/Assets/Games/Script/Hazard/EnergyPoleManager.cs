using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnergyPoleManager : Hazard
{
	protected EnergyPoleManagerDef energyPoleManagerDef;
		
	public GameObject energyPolePrb;
	public GameObject energyPrb;
	
	public List<EnergyPole> energyPoleList = new List<EnergyPole>();
	public List<Energy> energyList = new List<Energy>();
	
	protected bool isAttack = false;
	
	public void Update()
	{
		if(StaticData.isBattleEnd || TsTheater.InTutorial || !isEnabled)
		{
			stopAttack();
			if(IsInvoking("stopAttack"))
			{
				CancelInvoke("stopAttack");
			}
			return;
		}
		
		if(!isAttack)
		{
			isAttack = true;
			startAttack();
			startStopAttack();
		}
	}
		
	
	public void startAttack()
	{
		if(!IsInvoking("attack"))
		{			
			InvokeRepeating("attack", 3, 7);
		}
	}
	public void startStopAttack()
	{
		if(!IsInvoking("stopAttack"))
		{
			Invoke("stopAttack", 4);
		}
	}
	
	public void attack()
	{
		MusicManager.Instance.playEffectMusicForLoop("SFX_Kyln_Energy_Pole_Loop_1b");
		int attackCount = Random.Range(1, energyPoleList.Count + 1);
		for(int i = 0; i < attackCount; i++)
		{
			int index1 = Random.Range(0, energyPoleList.Count);
			int index2 = index1 + 1;
			if(index2 >= this.energyPoleList.Count)
			{
				index2 = 0;
			}
			energyPoleList[index1].energyHide(false);
			energyPoleList[index2].energyHide(false);
			
			energyList[index1].energyHide(false);
		}
	}
	
	public void stopAttack()
	{
		MusicManager.Instance.stopEffectMusicForLoop("SFX_Kyln_Energy_Pole_Loop_1b");
		foreach(EnergyPole ep in this.energyPoleList)
		{
			ep.energyHide(true);
		}
		
		foreach(Energy e in this.energyList)
		{
			e.energyHide(true);
		}
		isAttack = false;
		cancelAttack();
	}
	
	public void cancelAttack()
	{
		if (IsInvoking ("attack"))
		{
			CancelInvoke ("attack");
		}
	}
	 
	public override HazardDef HazardDef
	{
		get
		{
			return energyPoleManagerDef;
		}
		set
		{
			hazardDef = value;
			transform.localScale = Vector3.one;
			energyPoleManagerDef = hazardDef as EnergyPoleManagerDef;
			
			if(energyPolePrb == null)
			{
				energyPolePrb = Resources.Load("gsl_Cell/EnergyPole") as GameObject;
			}
			
			foreach(EnergyPoleDef epd in energyPoleManagerDef.energyPoleList)
			{
				GameObject energyPoleObj = Instantiate(energyPolePrb) as GameObject;
				EnergyPole ep = energyPoleObj.GetComponent<EnergyPole>();
				ep.transform.parent = transform;
				ep.EnergyPoleDef = epd;
				UISprite s = energyPoleObj.GetComponent<UISprite>();
				s.MakePixelPerfect();
				s.depth = 3;
				this.energyPoleList.Add(ep);
			}
			
			if(energyPrb == null)
			{
				energyPrb = Resources.Load("gsl_Cell/Energy") as GameObject;
			}
			
			for(int i = 0; i < this.energyPoleList.Count; i++)
			{
				GameObject energyObj = Instantiate(energyPrb) as GameObject;
				Energy e = energyObj.GetComponent<Energy>();
				e.EnergyPoleDef = energyPoleManagerDef.energyPoleList[0];
				e.transform.parent = transform.parent;
				
				this.energyList.Add(e);
				int index1 = i;
				int index2 = i + 1;
				if(index2 >= this.energyPoleList.Count)
				{
					index2 = 0;
				}
				
				EnergyPole ep1 = this.energyPoleList[index1];
				EnergyPole ep2 = this.energyPoleList[index2];
				
							
				Vector3 v = ep1.transform.localPosition - ep2.transform.localPosition;
				Vector3 v1 = ep1.transform.localPosition - new Vector3(ep1.transform.localPosition.x, ep2.transform.localPosition.y, 0);
						
				float angle = Vector3.Angle(v1, v);
				
				float dis = Vector3.Distance(ep1.transform.localPosition, ep2.transform.localPosition);
																
				Vector3 eulerAngler = Vector3.zero;
				
				if(ep1.transform.localPosition.x < ep2.transform.localPosition.x)
				{
					if(ep1.transform.localPosition.y < ep2.transform.localPosition.y)
					{
						angle = 90 - angle;
						eulerAngler = new Vector3(0, 0, angle);
					}
					else
					{
						angle = - (90 - angle);
						eulerAngler = new Vector3(0, 0, angle);
					}
				}
				else if(ep1.transform.localPosition.x > ep2.transform.localPosition.x)
				{
					if(ep1.transform.localPosition.y < ep2.transform.localPosition.y)
					{
						angle = - (90 - angle);
						eulerAngler = new Vector3(0, 0, angle);
					}
					else
					{
						angle = 90 - angle;
						eulerAngler = new Vector3(0, 0, angle);
					}
				
				}
				
				
				PackedSprite ps = e.GetComponent<PackedSprite>();
				
				float x = (ep1.transform.localPosition.x + ep2.transform.localPosition.x) / 2;
				float y = (ep1.transform.localPosition.y + ep2.transform.localPosition.y) / 2;
				
				e.transform.localPosition = new Vector3(x, y, 0);
				e.transform.localEulerAngles = eulerAngler;
				
				e.transform.localScale = new Vector3(dis / ps.width, 0.8f, 1);
				
			}
		}
	}
}
