using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class HeroData : CharacterData 
{
//	public enum Slot
//	{
//		Weapon = 0,
//		Armor,
//		Amulet1,
//		Amulet2,
//		Count
//	}
	public List<EquipData> equipSlots = new List<EquipData>(){null,null,null,null}; // added by roger
	
//	public ArrayList skillListBattle=new ArrayList();//SkillData  only activeSkill
//	public ArrayList comboListBattle=new ArrayList();//SkillData  only activeSkill
	//public ArrayList passiveList = new ArrayList(); // delete
	
	
	public List<string> activeSkillIDList = new List<string>();// only activeSkill
	public List<string> passiveSkillIDList = new List<string>();// only activeSkillj
	public List<SkillLearnedData> learnedSkillIdList = new List<SkillLearnedData>();
	public Hashtable passiveHash = new Hashtable();//PassiveSKData
	
	
	
	public int exp;
	
	public int lv = 1;
	/*public int eAttack=0;
	public int eMaxHp=0;
	public int eDefense=0;
	public int eAtkSpd=0;
	public int eMoveSpd=0;
	public int eCrtlStk = 0;*/
	
	
	public enum State
	{
		LOCKED,
		UNLOCKED_NOT_RECRUITED,
		RECRUITED_NOT_SELECTED,
		SELECTED
	}
	public State state = State.LOCKED;
	
	public int displayIndex = 0;
	
//	public bool  isSelect=false;//for select hero
//	//gwp
//	public bool  isHired=false;//for hired hero
//	
//	public bool isLock = true;
	
	// delete by why 2014.2.7
//	public int critValue; 
	//end
	
	public int hireSilver;
	public int hireGold;
	public int hireCp;
	
	public string nickName;
	public string description;
	
	
	public int stamina;//
	public int staminaMax;//
	public DateTime staminaTs=DateTime.Now;
	
	//xingyihua
	public  float hpProportion;
	public  float atkProportion;
	public  float defProportion;
	//end
	public Hashtable equipHash = new Hashtable();
	private Hashtable eftHash   = new Hashtable();
	
	public const string HEALER   = "HEALER";
	public const string MARINE   = "MARINE";
	public const string TANK     = "TANK";   //
	public const string TRAINER  = "TRAINER";
	public const string PRIEST   = "PRIEST";
	public const string WIZARD   = "WIZARD";
	public const string COWBOY   = "COWBOY";
	public const string DRUID    = "DRUID";  //
	public const string DOOM    = "DOOM";
	public const string STARLORD  = "STARLORD";
	public const string GROOT = "GROOT";
	public const string DRAX = "DRAX";
	public const string GAMORA = "GAMORA";
	public const string ROCKET = "ROCKET";
	public const string MANTIS = "MANTIS";
	public const string REDKING = "REDKING";
	public const string BUG = "BUG";
	public const string BETARAYBILL = "BETARAYBILL";
	public const string CHARLIE27 = "CHARLIE27";
	public const string HULK = "HULK";
	public const string IRONMAN = "IRONMAN";
	public const string KORATH = "KORATH";
	public const string LEVAN = "LEVAN";
	public const string CAIERA = "CAIERA";
	public const string SKUNGE = "SKUNGE";
	public const string NEBULA = "NEBULA";
	
	public static ArrayList types = new ArrayList()
	{
		HEALER, 
		MARINE, 
		TANK, 
		TRAINER, 
		PRIEST, 
		WIZARD, 
		COWBOY, 
		DRUID, 
		DOOM, 
		STARLORD,
		GROOT, 
		DRAX, 
		GAMORA, 
		ROCKET, 
		MANTIS,
		BUG,
		BETARAYBILL,
		CHARLIE27,
		HULK,
		IRONMAN,
		KORATH,
		LEVAN,
		CAIERA,
		REDKING
	};
	
	public static List<int> expList;
	
	
//	public void getLocalSk ( List<string> activeSkillIDList ,   List<string> passiveSkillIDList  ){ //name is get, WHY set?? TODO, roger
////		Debug.Log("sklist:"+sklist+" plist:"+plist); 
//		this.activeSkillIDList = activeSkillIDList;
//		this.passiveSkillIDList = passiveSkillIDList;
//	} 
	
	
//	public SkillLearnedData learnSkill(SkillDef def){
//		SkillLearnedData result;
//		
//		if (LearnedState.UNLEARNED != getLearnedData(def.id).State){
//			result = getLearnedData(def.id);
//		}
//		else{
//			result = new SkillLearnedData(def);
//			learnedSkillIdList.Add(result);
//		}
//		
//		return result;
//	}
	
	public SkillLearnedData getLearnedData(string id)
	{
		SkillLearnedData result = null;
		
		for (int i=0; i<learnedSkillIdList.Count; i++)
		{
			if (learnedSkillIdList[i].Id.Equals(id))
			{
				result = learnedSkillIdList[i];
				i = learnedSkillIdList.Count;
			}
		}
		
		return result;
	}
	
	
	
	private void checkAllHeroLevels (){
		bool  reached = true;
		
		foreach( DictionaryEntry tempHero in HeroMgr.heroHash)
		{
			Hero hero=tempHero.Value as Hero;
			reached = reached && ((hero.data as HeroData).lv >= 30);
		}
		
		if ((reached == true) && (HeroMgr.heroHash.Count > 0)) {
			AchievementManager.updateAchievement("ALL_HERO_LV30", 1);
		}
	}
	
//	private void levelUp (){
//		Debug.LogError("==levelup, miss update maxHP,attack, defense here");
//		lv += 1;
////		maxHp  = (int)(maxHp + hpProportion);
////		attack.Add(atkProportion);
////		defense.Add(defProportion);
//		
//		AchievementManager.updateAchievement("HERO_LV10", lv);
//		AchievementManager.updateAchievement("HERO_LV20", lv);
//		AchievementManager.updateAchievement("HERO_LV30", lv);
//		
//		checkAllHeroLevels();
//	}
	
	public void addStamina(int n){
		stamina = Math.Min(staminaMax,stamina+n);	
		UserInfo.instance.saveAllheroes();
	}
	public bool consumeStamina(int n){
		staminaRegenerate();
		if( stamina < n) return false;
		stamina -= n;
		staminaTs = DateTime.Now;
		UserInfo.instance.saveAllheroes();
		return true;
	}
	
//	public void addExp ( int xp  ){
//		if(lv> expList.Count)return;
//		exp += Mathf.Abs(xp);
//		for(int i=lv-1; i<expList.Count; i++)
//		{
//			int totalExp = (int)expList[i];
//			if(exp >= totalExp)
//			{
//				exp -= totalExp;
//				levelUp();
//			}else{
//				break;
//			}
//		}
//		UserInfo.instance.saveAllheroes();
//		//print(type+"saved exp");
//	}
	
	public EquipData equipObj ( EquipData.Slots slot ,   EquipData equipD  )
	{
//		Debug.LogError("equipObj "+slot +","+ equipD);
		if(equipD == null) return null;
//		Debug.LogError("equip id= "+equipD.equipDef.id);
		
		//if(equipInfoStr.Length <= 0) equipD.initUidTemp();		//Add By gsl_2013-10-15
		EquipData oldEquipm = null;
		oldEquipm = equipHash[slot] as EquipData;
		equipHash[slot] = equipD;
		
		foreach(Effect eft in equipD.equipDef.equipEftList)
		{
			if( eftHash[eft.eName] != null)
			{
				ArrayList tempAry = eftHash[eft.eName] as ArrayList;
				tempAry.Add(eft);
				eftHash[eft.eName] = tempAry as ArrayList;
			}
			else
			{
				eftHash[eft.eName] = new ArrayList(){ eft };
			}
		}
		
//		Debug.LogError("equipHash "+ Utils.dumpHashTable(equipHash));
		
		return oldEquipm;
	}
	
//	public function getEfts( string eftName  ):Array
//	{
//		Array efts = eftHash[eftName];
//		return efts;
//	}
	public Hashtable getPSkillByID ( string id  )
	{
		return passiveHash[id] as Hashtable;
	}
	
	//gwp add 
//	public SkillData getASkillByID ( string id  ){
//		for(int i = 0; i<skillListBattle.Count;i++){
//			SkillData skD = skillListBattle[i] as SkillData;
//			if(skD.id == id){
//				return skD;
//			}
//		}
//		return null;
//	}
	
	//<== gwp end
//	public void buildActiveSkill (){
//		if (GotoProxy.getSceneName() == "OpenAnim") {
//			return;
//		}
//		
//		for( int i=0; i<skillList.Count; i++)
//		{
//			string skID = skillList[i].ToString();
//			
//			SkillDef sd = SkillLib.instance.allHeroSkillHash[skID] as SkillDef;
//			if(sd.isPassive)
//			{
//				continue;
//			}
//			SkillData skD = SkillData.create(sd.id,sd.funcName,sd.coolDown,sd.number, sd.partners);
//			if(sd.partners == string.Empty)
//			{
//				skillListBattle.Add(skD);
//				skD.cooldownModify(itemDM.rcd);
//				skD.cooldownModify(skillDM.rcd);
//				singleActSkReduceCD( skD );
//			}
//			else
//			{
//				this.comboListBattle.Add(skD);
//			}
//		}
//	}
	
	public void buildPassiveSkill()
	{
		passiveHash.Clear();
		
		for(int i=0; i<passiveSkillIDList.Count; i++)
		{
			string skid = passiveSkillIDList[i].ToString();
			SkillDef sd = SkillLib.instance.allHeroSkillHash[skid] as SkillDef;
			passiveHash[sd.id] = sd.passiveEffectTable;
		}
	}
	
	public List<Effect> getSkEft ()
	{
		List<Effect> eftAry = new List<Effect>();
		
		foreach(string key1 in passiveHash.Keys)
		{
			Hashtable passiveEffectTable = passiveHash[key1] as Hashtable;
//			foreach(Effect eft in passiveEffectTable.Values)
//			{
//				eftAry.Add(eft);
//			}
			foreach(string k in passiveEffectTable.Keys){
				if (passiveEffectTable[k] is Effect){
					eftAry.Add((Effect)passiveEffectTable[k]);
				}
			}
		}
		return eftAry;
	}
	
	public List<Effect> getEquipEft ()
	{
		List<Effect> eftAry = new List<Effect>();
		foreach(EquipData.Slots slot in equipHash.Keys)
		{
			EquipData equipD = equipHash[slot] as EquipData;
			if(equipD != null)
			{
				foreach(Effect eft in equipD.equipEftList)
				{
					eftAry.Add(eft);
				}
			}
		}
		
		return eftAry;
	}
	
//	public int getWeaponValue()
//	{
//		EquipData equipD = equipHash[EquipData.Slots.WEAPON] as EquipData;
//		if(equipD == null)
//		{
//			return 0;
//		}
//		EquipEft atkEft = equipD.equipDef.equipEft;
//		return equipD.eftNum;
//	}
//	
//	public int getArmorValue()
//	{
//		int defNum;
//		if(equipHash.Contains(EquipData.Slots.ARMOR))
//		{
//			EquipData equipD = equipHash[EquipData.Slots.ARMOR] as EquipData;
//			if(equipD == null) 
//			{
//				return 0;
//			}
//			EquipEft defEft = equipD.equipDef.equipEft;
//			defNum = equipD.eftNum;
//		}
//		else
//		{
//			defNum = 0;
//		}
//		
//		return defNum;
//	}
	
	public EquipData getEquipData ( EquipData.Slots slot  )
	{
		return equipHash[slot] as EquipData;
	}
	
	public void unEquipObj ( EquipData.Slots slot  )
	{
		equipHash.Remove(slot);
	}
	
	
	private void equipAutoItem (){
		return;
		int weaponID = 0;
		int armorID = 0;
		int trinket1ID = 0;
		int trinket2ID = 0;
		
		if(type == STARLORD)
		{
				//weaponID = 1000;
				weaponID = 1002;
//				armorID  = 2001;
//				trinket1ID = 3000;
//				trinket2ID = 3001;
		}
		else if(type == GROOT)
		{
				weaponID = 1001;
//				armorID  = 2000;
//				trinket1ID = 3002;
//				trinket2ID = 3003;
		}
		else if(type == DRAX)
		{
				weaponID = 1000;
//				armorID  = 2002;
//				trinket1ID = 3004;
//				trinket2ID = 3005;
		}
		else if(type == GAMORA)
		{
				weaponID = 1001;
//				armorID  = 2000;
//				trinket1ID = 3006;
//				trinket2ID = 3007;
		}
		else if(type == ROCKET)
		{
				weaponID = 1001;
//				armorID  = 2000;
//				trinket1ID = 3002;
//				trinket2ID = 3003;
		}
		else if(type == MANTIS)
		{
				weaponID = 1001;
//				armorID  = 2000;
//				trinket1ID = 3002;
//				trinket2ID = 3003;
		}
		equipObj(EquipData.Slots.WEAPON, EquipFactory.create(weaponID));
//		equipObj(EquipData.ARMOR, EquipFactory.create(armorID));
//		equipObj(EquipData.TRINKET1, EquipFactory.create(trinket1ID));
//		equipObj(EquipData.TRINKET2, EquipFactory.create(trinket2ID));
	}
	
//	private void singleActSkReduceCD ( SkillData skD  ){
//		string skName = skD.skillName;
//		PassiveSKData pSKD = null;
//		if(skD.id == "WIZARD6"){
//			pSKD = passiveHash["WIZARD11"] as PassiveSKData;
//			if(pSKD != null)
//			{
//				skD.cooldownModify(pSKD.eft.num);
//			}
//		}
//	}
		
	public HeroData clone (){
		return null;
	}
	public static HeroData createWithStaticDefJson(Hashtable h){
		HeroData heroD = new HeroData();//CreateInstance("HeroData");
		heroD.type        = h["uid"]as string; //dataNode.Attributes ["type"].Value;      h[""]as string;
		heroD.nickName =  h["nickName"]as string; 
		heroD.hpProportion = float.Parse(h["hpProportion"]as string);  
		heroD.atkProportion =float.Parse(h["atkProportion"]as string); 
		heroD.defProportion =float.Parse(h["defProportion"]as string); 
		heroD.moveSpeed   =float.Parse(h["mspd"]as string); 		
		heroD.attackSpeed =float.Parse(h["aspd"]as string); 			
		heroD.attackRange =float.Parse(h["range"]as string); 			
		heroD.attack      = Vector6.createWithHashtable(h, "atk");
		heroD.defense     = Vector6.createWithHashtable(h, "def");
		heroD.maxHp       = int.Parse(h["hp"]as string); 
		
		// delete by why 2014.2.7
//		heroD.criticalStk = int.Parse(dataNode.Attributes ["cstk"].Value) ;
		heroD.description = "should locallized";
		heroD.hireSilver = int.Parse(h["hireSilver"]as string);
		heroD.hireGold = int.Parse(h["hireGold"]as string); 	
		heroD.hireCp =int.Parse(h["hireCp"]as string);  		
		heroD.stamina = int.Parse(h["stamina"]as string); 		
		heroD.staminaMax = int.Parse(h["staminaMax"]as string); 
		heroD.displayIndex = int.Parse(h["displayIndex"]as string);
		return heroD;
	}
	public static HeroData createWithStaticDefXml(XmlNode dataNode)
	{
		HeroData heroD = new HeroData();//CreateInstance("HeroData");
		heroD.type        = dataNode.Attributes ["type"].Value;
		heroD.nickName = dataNode.Attributes ["nickName"].Value;
		heroD.hpProportion = float.Parse(dataNode.Attributes ["hpProportion"].Value);
		heroD.atkProportion = float.Parse(dataNode.Attributes ["atkProportion"].Value);
		heroD.defProportion = float.Parse(dataNode.Attributes ["defProportion"].Value);
		heroD.moveSpeed   = float.Parse(dataNode.Attributes ["mspd"].Value)*Utils.characterScale;
		heroD.attackSpeed = float.Parse(dataNode.Attributes ["aspd"].Value);
		heroD.attackRange = float.Parse(dataNode.Attributes ["range"].Value);
		heroD.attack      = Vector6.createWithStaticDefXml(dataNode, "atk");
		heroD.defense     = Vector6.createWithStaticDefXml(dataNode, "def");
		heroD.maxHp       = int.Parse(dataNode.Attributes ["hp"].Value);
		
		// delete by why 2014.2.7
//		heroD.criticalStk = int.Parse(dataNode.Attributes ["cstk"].Value) ;
		heroD.description = dataNode.Attributes ["descriptions"].Value;
		heroD.hireSilver = int.Parse(dataNode.Attributes ["hireSilver"].Value);
		heroD.hireGold = int.Parse(dataNode.Attributes ["hireGold"].Value);
		heroD.hireCp = int.Parse(dataNode.Attributes ["hireCp"].Value);
		heroD.stamina = int.Parse(dataNode.Attributes ["stamina"].Value);
		heroD.staminaMax = int.Parse(dataNode.Attributes ["staminaMax"].Value);
		
		if(HeroData.expList == null)
		{
			XmlNodeList expXmlList = StaticData.getExpListXML ();
			List<int> expList = new List<int>();
			for (int j=0; j<expXmlList.Count; j++) {
				XmlNode expNode = expXmlList [j];
				string expString = expNode.Attributes ["num"].Value;
				
				int expMax = int.Parse (expString);
				expList.Add (expMax);
			}  	
			HeroData.expList = expList;
		}
		return heroD;
	}	
	public object dumpDynamicData()
	{
		Hashtable jsonH = new Hashtable();
		jsonH["type"] = this.type;
		jsonH["lv"] = this.lv;
		jsonH["exp"]   = this.exp;
		jsonH["state"] = (int)this.state;
		jsonH["stamina"]   = this.stamina; //--jevin
		jsonH["staminaTs"]     = staminaTs.ToString();
		
		jsonH["activeSkillIDList"]  = new ArrayList(this.activeSkillIDList);
		jsonH["passiveSkillIDList"] = new ArrayList(this.passiveSkillIDList); 
		jsonH["learnedSkillIdList"] = dumpSkillLearnedDynamicData();
		jsonH["equipSlots"]     = dumpEquipsDynamicData();
		
		
		return jsonH;
	}
	
	private Hashtable dumpEquipsDynamicData(){
		
		string equipStr = "";
		int tempIndex = 0;
		Hashtable result = new Hashtable();
		foreach( EquipData.Slots slot in equipHash.Keys)
		{
			EquipData equipD = equipHash[slot] as EquipData;
			if(equipD!=null){
				result.Add(slot,equipD.dumpDynamicData(false));
			}
		}
		return result;
	}
	
	private Hashtable dumpSkillLearnedDynamicData(){
		Hashtable result = new Hashtable();
		foreach (SkillLearnedData data in learnedSkillIdList){
			result.Add(data.Id, data.DynamicData);
		}
		return result;
	}
	
	private void loadEquipsDynamicData(object o){
		Hashtable h = o as Hashtable;
		if(h == null){
			return;
		}
		foreach(string k in h.Keys)
		{
			Hashtable equipHash = h[k] as Hashtable;
			EquipData.Slots slot = (EquipData.Slots)Enum.Parse(typeof(EquipData.Slots),k);
			EquipData equipD = EquipFactory.create((int)(double)equipHash["id"]);
			int lv = 0;
			if(equipHash.Contains("lv"))
			{
				lv = (int)(double)equipHash["lv"];
			}
			equipD.initLv(lv);
			equipD.uid = equipHash["uid"] as string;
			equipObj( slot ,equipD);
		}
		equipAutoItem();
	}	
	
	private void loadSkillLearnedDynamicData(object obj){
		Hashtable table = obj as Hashtable;
		
		if(table != null){
			learnedSkillIdList.Clear();
			foreach(string key in table.Keys){
				Hashtable dataHash = table[key] as Hashtable;
				learnedSkillIdList.Add(new SkillLearnedData(dataHash));
			}
		}
	}
	
	public void loadDynamicData()
	{
		ArrayList localHeros = SaveGameManager.instance().GetObject("heros") as ArrayList;
		Hashtable localInfo = null;
		if(localHeros!=null)
		{
			foreach( Hashtable h in localHeros)
			{
				if(h["type"] as string == this.type)
				{
					localInfo = h;
					break;
				}
			}
		}
		if(localInfo != null)
		{
			this.lv  = int.Parse (localInfo ["lv"].ToString ());
			this.exp   = int.Parse (localInfo ["exp"].ToString ());
			this.state = (HeroData.State)(int.Parse (localInfo ["state"].ToString ()));
			this.stamina = int.Parse(localInfo ["stamina"].ToString ());
			this.staminaTs = DateTime.Now;//DateTime.Parse(""+localInfo["staminaTs"]);
			
//			this.attack += (this.lv*this.atkProportion);
//			this.attack.Add((this.lv -1)*this.atkProportion);
//			this.defense += this.lv*this.defProportion;
//			this.defense.Add((this.lv-1)*this.defProportion);
//			this.maxHp += (int)((this.lv-1)*this.hpProportion);
			
			this.loadEquipsDynamicData (localInfo ["equipSlots"]);
			this.loadSkillLearnedDynamicData (localInfo ["learnedSkillIdList"]);
			activeSkillIDList = new List<string>((string[])(localInfo["activeSkillIDList"] as ArrayList).ToArray(typeof( string )));
			passiveSkillIDList = new List<string>((string[])(localInfo["passiveSkillIDList"] as ArrayList).ToArray(typeof( string )));
		}
		else
		{
			this.lv = 1;
			this.exp = 0;
			
			if(this.type == HeroData.STARLORD)
			{
				this.state = State.SELECTED;
			}
			else if(this.type == HeroData.MANTIS || 
				this.type == HeroData.BUG || 
				this.type == HeroData.BETARAYBILL  || 
				this.type == HeroData.CHARLIE27  || 
				this.type == HeroData.HULK  || 
				this.type == HeroData.IRONMAN)
			{
				this.state = State.UNLOCKED_NOT_RECRUITED;
			}
			else
			{
				this.state = State.LOCKED;
			}
		}
	}
	
	
	public string staminaRegenerate()
	{
		if(stamina<staminaMax){
			TimeSpan diff = DateTime.Now - staminaTs;
			if(diff.TotalSeconds > StaticData.staminaRegenerateCostSec){
				int addN = (int)diff.TotalSeconds/StaticData.staminaRegenerateCostSec;
				int left = (int)diff.TotalSeconds%StaticData.staminaRegenerateCostSec;
				this.addStamina(addN);
				diff = TimeSpan.FromSeconds(left);
				staminaTs = DateTime.Now - diff;
			}
			int leftSec = StaticData.staminaRegenerateCostSec - (int)diff.TotalSeconds;
			return string.Format("{0}m{1:D2}s",leftSec/60, leftSec%60);
		}else{
			return "";	
		}
	}
	public int getStaminaRechargeCostGold(){
		return Mathf.CeilToInt((staminaMax - stamina)*StaticData.staminaRechargeCostGold);	
	}
	
//	public void DestroyListBattle()
//	{
//		foreach(SkillData obj in this.skillListBattle)
//		{
//			GameObject.Destroy(obj.gameObject);
//		}
//		this.skillListBattle.Clear();
//		
//		foreach(SkillData obj in this.comboListBattle)
//		{
//			GameObject.Destroy(obj.gameObject);
//		}
//		this.comboListBattle.Clear();
//	}'
	public override string ToString ()
	{
		return string.Format ("[HeroData] type:{0},hp:{1},mspd:{2},aspd:{3},range:{4},defense:{5},attack:{6}" ,type,maxHp,this.moveSpeed,this.attackSpeed,this.attackRange,defense,attack);
	}
	
	public List<AtkData> getAtkPowers(){
		List<AtkData> sortlist = new List<AtkData>();
		sortlist.Add( new AtkData("PHY",attack.PHY));
		sortlist.Add( new AtkData("IMP",attack.IMP));
		sortlist.Add( new AtkData("PSY",attack.PSY));
		sortlist.Add( new AtkData("EXP",attack.EXP));
		sortlist.Add( new AtkData("ENG",attack.ENG));
		sortlist.Add( new AtkData("MAG",attack.MAG));
		sortlist.Sort(delegate(AtkData x, AtkData y) {
			return (x.v > y.v)?-1:1;
		});
		foreach(AtkData su in sortlist){
			Debug.Log(su.k+" :" +su.v);	
		}
		return sortlist;
	}
	public class AtkData{
		public string k;
		public float v;
		public AtkData(string k,float v){
			this.k = k;
			this.v = v;
		}
	}
}
			
