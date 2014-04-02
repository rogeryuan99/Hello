using UnityEngine;
using System.Collections;

public class DataModifier
{
	public int maxHp=0;
	public Vector6 attack = new Vector6();
	public Vector6 defense = new Vector6();
	public float atkSpd=0;
	public float moveSpd=0;
	
//	public const float CRIT_VALUE = 0.3f;
//	public const float STK_VALUE  = 0.9f;
//	public const float EVADE_VALUE = 0.1f;
	
//	public int crtlStk = 0;
//	public int strike = 0;
//	public int evade  = 0;
	
//	public int rcd = 0;
//	public int exp = 0;
	
//	public int atkHealPart = 0;
//	public int atkHealSelf = 0;
	
	public string toString ()
	{
		return Utils.dumpObjectSimple(this,false);
	}
}
