using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManagerDef : HazardDef{
	public PowerUpDef powerupDef;
	
	public override void parserAttributes(Hashtable attributesTable, HazardDef.HazardType hazardType){
		type = hazardType;
		
		int puDelay = int.Parse(attributesTable["delay"] as string);
		int puRepeat = int.Parse(attributesTable["repeat"] as string);
		string puBuffType = attributesTable["bufftype"] as string;
		int puBuffValue = int.Parse(attributesTable["buffvalue"] as string);
		int puBuffDurTime = int.Parse(attributesTable["bufftime"] as string);
		
		ArrayList posArrList = attributesTable["rangepos"] as ArrayList;
		
		List<Vector2> rangePosList = new List<Vector2>();
		
		for(int i = 0;i < posArrList.Count;i++){
			string[] posStr = posArrList[i].ToString().Split(',');
			Vector2 v2 = new Vector2(float.Parse(posStr[0]),float.Parse(posStr[1]));
			rangePosList.Add(v2);
		}
		
		PowerUpDef powerupDef = new PowerUpDef();
		
		powerupDef.puDelay = puDelay;
		powerupDef.puRepeat = puRepeat;
		powerupDef.puBuffType = puBuffType;
		powerupDef.puBuffValue = puBuffValue;
		powerupDef.puBuffDurTime = puBuffDurTime;
		powerupDef.puRangePosList = rangePosList;
		
		this.powerupDef = powerupDef;
	}
}
