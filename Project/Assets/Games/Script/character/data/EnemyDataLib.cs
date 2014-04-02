using UnityEngine;
using System.Xml;
using System.Collections;

public class EnemyDataLib : Hashtable {
	
	public const string Ch1_Korath ="Ch1_Korath";
	
	public const string Ch1_Guard1 ="Ch1_Guard1";
	public const string Ch1_Guard2 ="Ch1_Guard2";
	
	public const string Ch1_Female_Prisoner1 ="Ch1_Female_Prisoner1";
	public const string Ch1_Female_Prisoner2 ="Ch1_Female_Prisoner2";	

	public const string Ch1_Male_Prisoner1 ="Ch1_Male_Prisoner1";
	public const string Ch1_Male_Prisoner2 ="Ch1_Male_Prisoner2";
	public const string Ch1_Male_Prisoner3 ="Ch1_Male_Prisoner3";
	public const string Ch1_Male_Prisoner4 ="Ch1_Male_Prisoner4";
	public const string Ch2_Levan = "Ch2_Levan";
	public const string Ch2_Nebula = "Ch2_Nebula";
	public const string Ch2_Skunge = "Ch2_Skunge";
	public const string Ch3_Boss_RedKing = "Ch3_Boss_RedKing";
	public const string Ch3_Caiera = "Ch3_Caiera";
	public const string Ch3_Miek = "Ch3_Miek";
	public const string Ch3_PitMonster1 = "Ch3_PitMonster1";
	public const string Ch3_PitMonster2 = "Ch3_PitMonster2";
	public const string Ch4_TinyMinion1 = "Ch4_TinyMinion1";
	public const string Ch4_SmallMinion2 = "Ch4_SmallMinion2";
	
	private static EnemyDataLib _instance;
	public static EnemyDataLib instance{
		get{
			if(_instance == null) _instance = new EnemyDataLib();
			return _instance;
		}
	}
	
	public void initWithJson(ICollection al){
		foreach(Hashtable h in al){
			string type = h["uid"]as string;
			Vector6 atkStr = Vector6.createWithHashtable(h, "atk");
			Vector6 defStr = Vector6.createWithHashtable(h, "def");
			
			string hpStr  = h["hp"]as string;
			string mspdStr  = h["mspd"]as string;
			string aspdStr  = h["aspd"]as string;

			string rewardSilverStr = h["rewardSilver"]as string;
			string rewardExpStr = h["rewardExp"]as string;
			
			this[type]=new Hashtable()
			{
				{"type",type},
				{"hp",int.Parse(hpStr)},
				{"mspd",int.Parse(mspdStr)},
				{"aspd",float.Parse(aspdStr)},
				{"atk",atkStr},
				{"def",defStr},
				{"rewardSilver",int.Parse(rewardSilverStr)},
				{ "rewardExp",int.Parse(rewardExpStr)}
			};
		}
		
		Debug.Log("===enemy lib inited==== "+this.Count);
		Debug.Log(Utils.dumpHashTable(this));
	}
	private EnemyDataLib (){
//		XmlNodeList enemyXmlList = StaticData.getEnemiesXML();
//		for( int i=0; i<enemyXmlList.Count; i++)
//		{
//			XmlNode dataNode = enemyXmlList[i];
//			string type = dataNode.Attributes["type"].Value;
//			Vector6 atkStr = Vector6.createWithStaticDefXml(dataNode, "atk");
//			Vector6 defStr = Vector6.createWithStaticDefXml(dataNode, "def");
//			
//			string hpStr  = dataNode.Attributes["hp"].Value;
//			string mspdStr  = dataNode.Attributes["mspd"].Value;
//			string aspdStr  = dataNode.Attributes["aspd"].Value;
//
//			string rewardSilverStr = dataNode.Attributes["rewardSilver"].Value;
//			string rewardExpStr = dataNode.Attributes["rewardExp"].Value;
//			
//			this[type]=new Hashtable()
//			{
//				{"type",type},
//				{"hp",int.Parse(hpStr)},
//				{"mspd",int.Parse(mspdStr)},
//				{"aspd",float.Parse(aspdStr)},
//				{"atk",atkStr},
//				{"def",defStr},
//				{"rewardSilver",int.Parse(rewardSilverStr)},
//				{ "rewardExp",int.Parse(rewardExpStr)}
//			};
//		}
	}
}
