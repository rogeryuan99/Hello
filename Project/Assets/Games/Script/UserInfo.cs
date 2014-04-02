using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class UserInfo {

	public static List<HeroData> heroDataList=new List<HeroData>();
	
	private string playerID;
	private string userName; 
	
	private int silver = 0;
	private int gold=0;
	private int commandPoints = 0;
	private int iso8Slots = 0;
	
	public static UserInfo instance = new UserInfo();
	
	
	
	private  UserInfo (){
	}
	

	public void clearData (){
		SaveGameManager.instance().deleteSavedData();
	}
	public void initDefaultScalars(){
	Debug.Log("initDefaultScalars");
			XmlNode node = StaticData.getDefaultScalarXML();
		Debug.Log(node.OuterXml);
		this.silver = int.Parse(node.Attributes["silver"].Value);
		this.gold = int.Parse(node.Attributes["gold"].Value);
		this.iso8Slots = int.Parse(node.Attributes["isoSlots"].Value);
		this.commandPoints = int.Parse(node.Attributes["cp"].Value);
		iso8Slots = 1;
	}
	public void initOthersWithJson(ICollection al){
		foreach(Hashtable h in al){
			switch ( h["uid"] as string){
			case "default_cp":
				commandPoints = int.Parse(h["v"] as string);
				break;
			case "default_gold":
				gold = int.Parse(h["v"] as string);
				break;
			case "default_isoSlots":
				iso8Slots = int.Parse(h["v"] as string);
				break;
			case "default_silver":
				silver = int.Parse(h["v"] as string);
				break;
			}
		}
	}	
	
//		{
//          "v": "10",
//          "uid": ""
//        },
//        {
//          "v": "15",
//          "uid": ""
//        },
//        {
//          "v": "1",
//          "uid": ""
//        },
//        {
//          "v": "100",
//          "uid": ""
//        },
//        {
//          "v": "180",
//          "uid": ""
//        },
//        {
//          "v": "0.5",
//          "uid": ""
//        }	
	
	public void loadDynamicScalars(){
	Debug.Log("initDefaultScalars");
		Hashtable h = SaveGameManager.instance().GetObject("scalars") as Hashtable;		
		if(h!=null){
			Debug.Log("dynamic scalars:"+Utils.dumpHashTable(h));
			this.silver = (int)(double)h["slv"];
			this.gold = (int)(double)h["gld"];
			this.iso8Slots = (int)(double)h["is"];
			this.commandPoints = (int)(double)h["cp"];
		}
	}
	public Hashtable dumpDynamicScalars(){
		Hashtable h = new Hashtable();
		h["slv"] = this.silver;
		h["gld"] = this.gold;
		h["is"] = this.iso8Slots;
		h["cp"] = this.commandPoints;
		return h;
	}
    
	public int getSilver (){
        return this.silver;
	}
    
	public bool consumeSilver ( int num  ){
         if( ( this.silver - Mathf.Abs(num) ) < 0)
        {
            return false;
        }else{
			MusicManager.playEffectMusic("SFX_UI_silver_1a");
        	this.silver -= Mathf.Abs(num);
			this.saveScalars();
            return true;
        }
	}
    
	public void addSilver ( int num  ){
        this.silver += num;
        this.saveScalars();
       // MsgCenter.instance.dispatch(new Message(MoneyTip.MONEY_CHANGE,this));
	}
	
	
	public void addGold ( int num  ){
		
        this.gold += num;
        this.saveScalars();
        //MsgCenter.instance.dispatch(new Message(MoneyTip.MONEY_CHANGE,this));
	}
	
	public int getGold (){
        return this.gold;
	}
    
	public bool consumeGold ( int num  ){
        if( ( this.gold - Mathf.Abs(num) ) < 0)
        {
            return false;
        }else{
        	this.gold -= Mathf.Abs(num);
        	this.saveScalars();
            return true;
        }
	}
	
	
	public void addCommandPoints ( int num  ){
		
        this.commandPoints += num;
        this.saveScalars();
		
	}
	
	public int getCommandPoints (){
        return this.commandPoints;
	}
    
	public bool consumeCommandPoints ( int num  ){
        if( ( this.commandPoints - Mathf.Abs(num) ) < 0)
        {
            return false;
        }else{
			MusicManager.playEffectMusic("SFX_UI_commandpoints_2a");
        	this.commandPoints -= Mathf.Abs(num);
        	this.saveScalars();
            return true;
        }
	}
	
	public void addEquipmentBarCount ( int num  ){
        iso8Slots += num;
        SaveGameManager.instance().SetInt("equipmentBarCount", iso8Slots);
	}
	public int getEquipmentBarCount (){
		return SaveGameManager.instance().GetInt("equipmentBarCount", 2);
	}
    //end
	
	public void saveAllheroes (){
		ArrayList heros = new ArrayList();
		for(int i=0; i<heroDataList.Count; i++)
		{
			HeroData heroD = heroDataList[i] as HeroData;
			heros.Add(heroD.dumpDynamicData());
		}
		SaveGameManager.instance().SetObject("heros",heros);
		SaveGameManager.instance().saveToFile();
		
	}

	public void savePackage (){
		ArrayList a = EquipManager.Instance.dumpDynamicData();
		SaveGameManager.instance().SetObject("bpack", a);
	}
	public object getPackage (){ 
		return SaveGameManager.instance().GetObject("bpack");		
	}
	
	public void saveMapInfo ()
	{
		ArrayList a = MapMgr.Instance.dumpDynamicData();	
		SaveGameManager.instance().SetObject("map", a);
	}
	
	public object getMapInfo (){
		return SaveGameManager.instance().GetObject("map");
	}
	public object getFtueInfo (){
		return SaveGameManager.instance().GetObject("ftue");
	}
	
	public void saveIsUnLockCombo(bool isUnLockCombo)
	{
		SaveGameManager.instance().SetString("Is_UnLock_Combo",isUnLockCombo.ToString());
	}
	
	public bool getIsUnLockCombo()
	{
		string jsonString = SaveGameManager.instance().GetString("Is_UnLock_Combo");
		
		if(jsonString == "")
		{
			return false;
		}
		else
		{
			return bool.Parse(jsonString);
		}
		return false;
	}
	public void saveScalars(){
		Hashtable h = this.dumpDynamicScalars();
		SaveGameManager.instance().SetObject("scalars",h);
	}
	public void saveFtue(){
		ArrayList a = TsDynamicData.instance.dumpDynamicData();
		Debug.Log("save ftue: "+Utils.dumpList(a));
		SaveGameManager.instance().SetObject("ftue", a);	
	}
	public void saveAllToServer(){
		Debug.LogError("Server_SAVE ");
		new Player_UpdateAllCommand(CommandTest.playerId, CommandTest.authToken,
			delegate(Hashtable data){
				string s = Utils.dumpHashTable(data);
				Debug.Log("complete "+s);	
			},
			delegate(string err_code,string err_msg,Hashtable data){
				Debug.Log("error");	
			}
			).excute();;			
	}
	public void saveAll(){
		Debug.LogError("LOCAL_SAVE ");
		saveScalars();
		saveAllheroes();
		savePackage();
		saveMapInfo();
		saveFtue();
	}
}
