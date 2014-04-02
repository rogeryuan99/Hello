using UnityEngine;
using System.Collections;

public class EnergyPole : EnergyBase
{
	public override EnergyPoleDef EnergyPoleDef
	{
		set
		{
			this.energyPoleDef = value;
			transform.localPosition = energyPoleDef.localPosition;
		}
		get
		{
			return energyPoleDef;
		}
	}
}
