using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipData
{
	public static int cumulateUid = 0;
	public string uid = "";
	
	
	public EquipDef equipDef;
	
	
//	public ArrayList equipRarityValue = new ArrayList(){1, 3, 5, 10};
	
	protected int lv=0;
	public int count=0;
	
	public List<Effect> equipEftList = new List<Effect>();
	
	//gwp
//	public int maxDurability;
//	public int currentDurability;
//	public int repairSilver;
//	public long repairTotalTime;
//	public long repairStartTime;
	//end
	
//	private ArrayList levelValuesTotal;
//	private ArrayList levelValues;
//	private ArrayList moneyValues = new ArrayList(){1,3,6,9,12,24,29,35,42,60,70};
//	
//	
//	private ArrayList uplevelGoldArray = new ArrayList(){1,1,1,1,2,3,4,5,6,8,12};
	
	public enum Type{
		WEAPON,
		ARMOR,
		ISO,
		TRINKET,
		Gear,
		Uniforms,
		NONE
	};
	
	public enum Slots{
		WEAPON,
		ARMOR,
		TRINKET1,
		TRINKET2,
		ISO1,
		ISO2,
		ISO3,
	}
	
	//wuqi
//	public static string ARMOR     = "Armor";  //junxieku
//	public static string TRINKET1 = "TRINKET1";
//	public static string TRINKET2 = "TRINKET2";
	
//	public static string HERO = "";
//	public static string HAT = "HAT";
//	public static string TRINKET = "Trinket";
//	public static string POTIONS = "Potions";
//	public static string PET     = "Pets";
//	public static string DROPSHIPFUELS     = "DropshipFuels";
//	public static string CHESTBOX     = "ChestBox";
//	public static string CHESTBOXKEY     = "ChestBoxKey";
//	public static string CURRENCY    = "Currency";
//	//slot type

//	//storage	
//	public static string STORAGE = "STORAGE";
	
//	public static ArrayList typesAry = new ArrayList(){WEAPON,ARMOR, POTIONS,PET,DROPSHIPFUELS,TRINKET1,TRINKET2};//,AMULET1,ARMOR];//1,AMULET2,ARMOR]; TRINKET,
	
	
//	public int getEquipRarityValue (){
//		return int.Parse(this.equipRarityValue[(int)(this.equipDef.equipRarity)].ToString());
//	}
	
	public int getCurrentLv()
	{
		return lv + 1;
	}
	
	public int getNextLv()
	{
		return getCurrentLv() + 1;
	}
	
	public int getMaxLv()
	{
		return equipDef.maxLv;
	}
	
//	void initLevelValuesTotal (){
//		levelValuesTotal = new ArrayList();
//		levelValuesTotal.Add(new ArrayList(){1, 1.1f, 1.25f, 1.4f, 1.55f, 2, 2.6f, 3.2f, 3.9f, 4.7f, 5.5f});
//		levelValuesTotal.Add(new ArrayList(){1.5f, 1.6f, 1.75f, 1.9f, 2.05f, 2.5f, 3.1f, 3.7f, 4.4f, 5.2f, 6});
//		levelValuesTotal.Add(new ArrayList(){2, 2.1f, 2.25f, 2.4f, 2.55f, 3, 3.6f, 4.2f, 4.9f, 5.7f, 6.5f});
//		levelValuesTotal.Add(new ArrayList(){2.5f, 2.6f, 2.75f, 2.9f, 3.05f, 3.5f, 4.1f, 4.7f, 5.4f, 6.2f, 7});
//	}
	
	//for 20121107 bug fix   start---->
	//json = {'graphicsID':'weapon03', 'price':100, 'maxDurability':200, 'type':WEAPON, 'QName':'darkSword', 'isLvUp':true, 'specialType':[BIO, SHIELD]}
//	public EquipData ( EquipEft eft ,   Hashtable JsonData  ){
//		this.eft = eft;
//		baseValue = eft.num;
//		jsonD    = JsonData;
//		id         = int.Parse(JsonData["id"].ToString());
//		display    = int.Parse(JsonData["display"].ToString());
//		dif        = int.Parse(JsonData["dif"].ToString());
//		graphicsID = JsonData["graphicsID"].ToString();
//		price      = int.Parse(JsonData["price"].ToString());
//		//gwp
//		//durability = JsonData['durability'];
//		maxDurability = int.Parse(JsonData["maxDurability"].ToString());
//		repairCoins = int.Parse(JsonData["repairCoins"].ToString());
//		repairTotalTime = long.Parse(JsonData["repairTotalTime"].ToString());
//		//end     
//		type       = JsonData["type"].ToString();
//		isLvUp     = bool.Parse(JsonData["isLvUp"].ToString());
//		specialType= (ArrayList)JsonData["specialType"];
//		QName      = JsonData["QName"].ToString();
//		credits    = int.Parse(JsonData["credits"].ToString());
//		iconID     = JsonData["iconID"].ToString();
//		// weapon level up ++  if every weapon different  factory.init(). lib[0] = new EquipData  
//		//add a element levelvalue difinition different percentage
//		//levelValues = [1, 1.1f, 1.25f, 1.4f]; //
//		
//		initLevelValuesTotal();
//		
//		
//		moneyValues = new ArrayList(){1,3,6,9,12,24,29,35,42,60,70};
//		
//		equipRarity = EquipDef.EQUIPRARITY_ENUM.NORMAL;
//		
//		levelValues = levelValuesTotal[0] as ArrayList;
//	}
	
	//for merchandiseItem
	public EquipData (EquipDef equipDef)
	{
		this.equipDef = equipDef;
		
		foreach(Effect eft in equipDef.equipEftList)
		{
			this.equipEftList.Add(eft.clone());
		}
		
//		initLevelValuesTotal();
//		
//		levelValues = levelValuesTotal[0] as ArrayList;
	}
	//<--------------------  end

	//
	public void initLv(int lvNum)
	{
		this.lv = lvNum;
		if(getCurrentLv() >= 2)
		{
			foreach(Effect eft in this.equipEftList)
			{
				eft.num = getNextLvData ((int)eft.num);
			}
		}
	}
	
	
//	public int getPerfectUplevelGold (){
//		//modified by xiaoyong 20130624 for array index out of range.
//		return 0;//(lv + 1 < this.equipDef.maxLv)? (int) uplevelGoldArray[(lv + 1)] : (int)uplevelGoldArray[lv];
//	}
	
//	public int getUplevelSuccesRate (){
//		return 100 - lv * 10;
//	}
	
//	public bool levelUpIsSuccess (){
//		int successRate = Random.Range(1, 101);
//		
//		if(successRate <= lv * 10) 
//		{
//			return false;
//		}
//		
//		return true;
//	}
	
//	public void levelUpFail (){
//		if(this.equipDef.isLvUp && ( lv + 1) < this.equipDef.maxLv)
//		{   
//			float lvValue = (float)levelValues[lv];
//			int needMoney = (int)moneyValues[lv+1];
////			moneyLevel = moneyLevel*needMoney; //reset next level need money
//			UserInfo.instance.consumeSilver(this.equipDef.baseValue * needMoney);
//		}
//	}
	
//	public bool isLevelDown (){
//		 int levelDownRate = Random.Range(1, 101);
//		
//		Debug.LogError(lv);
//		if(lv <= 5)
//		{
//			return false;
//		}
//		
//		if(levelDownRate <= 50) 
//		{
//			return false;
//		}
//		
//		return true;
//	}
	
//	public void leveDown (){
//		if(lv == 1)
//		{
//			return;
//		}
//		lv--;
//		float lvValue = (float)levelValues[lv];
//		this.equipDef.equipEft.num = Mathf.RoundToInt(this.equipDef.baseValue*lvValue); 
//	}
	
	public void levelUp ()
	{
		
		lv++;
		
//		this.eftNum = getProperty(getCurrentLv(), getNextLv() + 1);
		
		foreach(Effect eft in this.equipEftList)
		{
			eft.num = getProperty((int)eft.num, getCurrentLv(), getNextLv() + 1);
		}
	}
	
	public int getLvUpMoney (){
//		float lvValue = (float)levelValues[lv];
//		int needMoney;
//		
//		if((lv + 1) < this.equipDef.maxLv)
//		{
//			needMoney = (int)moneyValues[lv+1];
//		}else{
//			needMoney = (int)moneyValues[lv];
//		}
//		
//		int levelUpMoney = this.equipDef.baseValue*needMoney; //reset next level need money
		
		int levelUpMoney = 0;
		if(this.equipDef.silver != 0)
		{
			levelUpMoney = getLvUpMoney(this.equipDef.silver);
		}
		else if(this.equipDef.gold != 0)
		{
			levelUpMoney = getLvUpMoney(this.equipDef.gold);//Formulas.getUpgradeGearCostSilver(getCurrentLv(), getNextLv(), this.equipDef.gold, 20);
		}
		else if(this.equipDef.commandPoints != 0)
		{
			levelUpMoney = getLvUpMoney(this.equipDef.commandPoints);//Formulas.getUpgradeGearCostSilver(getCurrentLv(), getNextLv(), this.equipDef.commandPoints, 20);
		}
	
		return levelUpMoney;
	}
	
	protected int getLvUpMoney (int money)
	{
		return Formulas.getUpgradeGearCostMoney(getCurrentLv(), getNextLv(), money, 20);
	}
	
	public bool canUpgrade (){
		return getCurrentLv() < this.equipDef.maxLv;
	}
	
	public bool hasNextLv (){
		return this.equipDef.isLvUp && canUpgrade();
	}
	
	public int getNextLvData (int num)
	{
//		if(lv >= 3)
//		{
//			return this.equipDef.equipEft.num;
//		}else{
//			float lvNum = (float)levelValues[lv+1];
//			return Mathf.RoundToInt(this.equipDef.baseValue*lvNum);
//		} 
		
		return getProperty(num, getNextLv(), getNextLv() + 1);
	}
	
	protected int getProperty(int num, int currentLv, int nextLv)
	{
		return Formulas.getUpgradeGearProperty(currentLv , nextLv, num, 1);
	}
	
	public EquipData clone (){	
		
		EquipData equipData = new EquipData(this.equipDef.clone());
		return equipData;
	}
	
//	public string description ()
//	{
//		if(this.equipDef.equipEft!=null)
//		{
//			this.equipDef.des = this.equipDef.equipEft.description();
//			return this.equipDef.des;
//		}else{
//			return this.equipDef.des;
//		}
//		
//	}
	
	public void initUidTemp(){ // should be allocated by server
		Debug.LogError("initUidTemp");
		int u = cumulateUid	++;
		string s = string.Format("{0:d3}",u);
		this.uid = s;
	}
	
	public object dumpDynamicData(bool inPackage=true){
		Hashtable h = new Hashtable();
		switch(equipDef.type){
		case Type.ISO:
			if(inPackage){
				if(this.count == 0){
					return null;	
				}
				h.Add("id",this.equipDef.id);
				h.Add("count",this.count);
			}else{
				h.Add("id",this.equipDef.id);
			}
			break;
		default:
			h.Add("uid",this.uid);
			h.Add("id",this.equipDef.id);
			h.Add("lv",this.lv);
			break;
		}
		return h;
	}
	
	public void loadDynamicData(Hashtable h)
	{
		switch(equipDef.type)
		{
		case Type.ISO:
			if(h.Contains("count"))
			{
				this.count = (int)(double)h["count"];
			}
			else
			{
				this.count = 0;	
			}
			break;
		default:
			this.initLv( (int)(double)h["lv"]);
			this.uid = h["uid"] as string;
			break;
		}
	}
	public string getDescString(){
		string s = "";
		switch(this.equipDef.type){
		case Type.ISO:
			foreach(Effect eft in equipEftList)
			{
				if(eft.num <=0) continue;
				s += " "+ eft.eName + ": [00FF00]" + eft.num;
			}
			break;
		default:
			s = "LV:"+ this.getCurrentLv() +" ";
			foreach(Effect eft in equipEftList)
			{
				if(eft.num <=0) continue;
				s += " "+ eft.eName + ": " + eft.num;
			}
			break;
		}
		return s;
	}
}
