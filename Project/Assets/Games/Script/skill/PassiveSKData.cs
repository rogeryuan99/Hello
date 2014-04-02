using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassiveSKData
{
	public string id;
	public List<Effect> effectList;
	
	public PassiveSKData ( string id, List<Effect> effectList )
	{
		this.id   = id;
		this.effectList  = effectList;
	}
	
	public override string ToString ()
	{
		return "";//Utils.dumpObject(this,2,5,false);
	}
}