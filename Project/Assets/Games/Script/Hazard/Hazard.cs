using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour 
{
	public bool isEnabled = false;
	
	protected HazardDef hazardDef;
	
	public virtual void calculateAttackRect()
	{
	}
	
	public virtual HazardDef HazardDef
	{
		get
		{
			return hazardDef;
		}
		set
		{
			hazardDef = value;
		}
	}
}
