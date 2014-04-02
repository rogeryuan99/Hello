using UnityEngine;
using System;
using System.Xml;
using System.Collections;

public class StaticData : MonoBehaviour {
public static int objLayer = -50;
public static int lineLayer = -1;
public static int circleLayer = -2;

public static bool  isPhone;
public static bool  isTouch4;

public static bool  isiPhone5;

public static bool  paused ;
private static XmlDocument xmlDataRoot;
private static XmlNode xmlData;
private static XmlDocument eftXmlDataRoot;
private static XmlNode eftXmlData;
//xingyh
public static bool  isBattleEnd = true;

public static int difLevel=1;

public static int staminaRegenerateCostSec;
public static float staminaRechargeCostGold;
	
	public static bool isPVP;
	
public static void setXmlData ( string txtAst  ){
	if(xmlDataRoot == null)
	{
		xmlDataRoot = new XmlDocument();
		xmlDataRoot.LoadXml(txtAst);
			xmlData = getXmlNode(xmlDataRoot, "root");
			
		XmlNode staminaNode = getXmlNode(xmlData, "stamina");
		staminaRegenerateCostSec = int.Parse(staminaNode.Attributes["regenerateCostSec"].Value);
		staminaRechargeCostGold = float.Parse(staminaNode.Attributes["rechargeCostGold"].Value);	
	}
}

public static void initOthersWithJson(ICollection al){
	foreach(Hashtable h in al){
		switch ( h["uid"] as string){
		case "stamina_rechargeCostGold":
			staminaRechargeCostGold = float.Parse(h["v"] as string);		
			break;
		case "stamina_regenerateCostSec":
			staminaRegenerateCostSec = int.Parse(h["v"] as string);
			break;
		}
	}
}	
public static XmlNode getXmlNode(XmlNode newsData, string name)
{
	for (int i = 0; i < newsData.ChildNodes.Count; i++)
	{
		XmlNode xmlNode = newsData.ChildNodes [i];
		if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.Name == name)
		{
			return (XmlElement)xmlNode;
		}
	}
	return null;
}
	
	
public static int findIt(int num, int n)
{
    int power = 1;
	if(n > numLength (num))
	{
		return 0;
	}
    for (int i = 0; i < n; i++)
    {
        power *= 10;
    }
		
    return (num - num / power * power) * 10 / power;
}
	
	public static int numLength(int num)
	{
		int length=0;
		while(num != 0)
		{
			num/=10;
			length++;
		}
		return length;
	}
	
	public static int setBitTo0ByIndex(int v, int index)
	{
		return v ^= 1 << index;
	}
	
	public static int setBitTo1ByIndex(int v, int index)
	{
		return v |= 1 << index;
	}
	
	public static UInt32 setBitTo0ByIndex(UInt32 v, int index)
	{
		return v ^= (UInt32)1 << index;
	}
	
	public static UInt32 setBitTo1ByIndex(UInt32 v, int index)
	{
		return v |= (UInt32)1 << index;
	}
	
	public static string replaceStringByIndex(string s, char a,int index)
	{
		char[] ch = s.ToCharArray();
		ch[index] = a;
		return new string(ch);
	}

public static string FBMessageString (){
	return "";
}

//20121107 for bug fix start----->
public static string dailyRewardString (){
	XmlNode dataNode = getXmlNode(xmlData, "dailyRewardString");//xmlData.DocumentElement.Item["dailyRewardString"];
	if (dataNode != null) {
		string rewardString = dataNode.Attributes["string"].Value;
		return rewardString;
	}else {
		return "You just earned 1 free Credit! Come back every day for more rewards!";
	}
}

public static int dailyRewardAmount (){
	XmlNode dataNode = getXmlNode(xmlData, "dailyRewardAmount");//xmlData.DocumentElement.Item["dailyRewardAmount"];
	if (dataNode != null) {
		string rewardAmountString = dataNode.Attributes["num"].Value;
		int amount = int.Parse(rewardAmountString);
		return amount;
	}else {
		return 1;
	}
}

public static void setEftXmlData ( string txtAst  ){
	if(eftXmlDataRoot == null)
	{
		eftXmlDataRoot = new XmlDocument();
		eftXmlDataRoot.LoadXml(txtAst);
			eftXmlData = getXmlNode(eftXmlDataRoot, "root");
	}
}


public static XmlNodeList  getHeroXML (){
	XmlNodeList list = getXmlNode(xmlData, "heroData").ChildNodes;//xmlData.DocumentElement.Item["heroData"].ChildNodes;
//	for( int i = 0; i<list.Count; i++)
//	{
//		XmlNode dataNode = list[i];
//		Debug.Log(dataNode.Attributes["type"].Value);
//	} 
	return list;
//	Debug.Log();
//	return xmlData.Item["heroData"]
}
public static XmlNode  getDefaultScalarXML (){
	return getXmlNode(xmlData, "defaultScalars");
}

public static XmlNodeList  getExpListXML (){
	XmlNodeList list = getXmlNode(xmlData, "expList").ChildNodes;//xmlData.DocumentElement.Item["expList"].ChildNodes; 
	return list;
}

public static XmlNodeList  getEftActXML (){
	XmlNodeList list = getXmlNode(eftXmlData, "eftAct").ChildNodes;//eftXmlData.DocumentElement.Item["eftAct"].ChildNodes;
//	for( int i = 0; i<list.Count; i++)
//	{
//		XmlNode dataNode = list[i];
//		Debug.Log(dataNode.Attributes["type"].Value);
//	} 
	return list;
//	Debug.Log();
//	return xmlData.Item["heroData"]
}public static XmlNodeList  getEftBoneXML (){
	XmlNodeList list = getXmlNode(eftXmlData, "eftBone").ChildNodes;//eftXmlData.DocumentElement.Item["eftBone"].ChildNodes;
//	for( int i = 0; i<list.Count; i++)
//	{
//		XmlNode dataNode = list[i];
//		Debug.Log(dataNode.Attributes["type"].Value);
//	} 
	return list;
//	Debug.Log();
//	return xmlData.Item["heroData"]
}
//public static XmlNodeList  getEnemiesXML (){
//	XmlNodeList list = getXmlNode(xmlData, "enemies").ChildNodes;//xmlData.DocumentElement.Item["enemies"].ChildNodes;
//	return list;
//}

public static XmlNodeList  getEquipXML (){
	XmlNodeList list = getXmlNode(xmlData, "items").ChildNodes;//xmlData.DocumentElement.Item["items"].ChildNodes;
	return list;
}

public static Rect get2DRect ( GameObject obj  ){
	//print("---->"+hero.renderer.bounds.min+"---"+hero.renderer.bounds.max);
	Vector3 minVc3 = obj.renderer.bounds.min;
	Vector3 maxVc3 = obj.renderer.bounds.max;
	Rect rect = new Rect(minVc3.x, minVc3.y, maxVc3.x-minVc3.x, maxVc3.y-minVc3.y);
	return rect;
}

public static bool isInOval ( float xRadius ,   float yRadius ,   Vector2 vc2  ){
	 //oval equation   (x*x)/100*100 + (y*y)/50*50 = 1 ->  y1 = sqrt( (1-(x*x)/10000)*2500); y2 = -sqrt( (1-(x*x)/10000)*2500);
	if(Mathf.Abs(vc2.x)>xRadius){
		return false;
	}
	// float tt = ( 1- (vc2.x*vc2.x)/(xRadius*xRadius) )*(yRadius*yRadius); // never used
	float onOvalValue = Mathf.Sqrt( ( 1- (vc2.x*vc2.x)/(xRadius*xRadius) )*(yRadius*yRadius) ); 
	if( Mathf.Abs(vc2.y)<= Mathf.Abs(onOvalValue) )
	{
		return true;
	}else{
		return false;
	}
	/*
	//gwp 
	if(Mathf.Abs(vc2.x)<=xRadius && Mathf.Abs(vc2.y)<=yRadius && ((vc2.x*vc2.x)/(xRadius*xRadius)+(vc2.x*vc2.x)/(yRadius*yRadius)) <=1){
		return true;
	}else{
		return false;
	}
	//end
	*/
}

//gwp
public static long getSystemCurrentTime (){
		
	return  System.DateTime.Now.Month*30*24*3600 + 
			System.DateTime.Now.Day*24*3600 + 
				System.DateTime.Now.Hour*3600 + 
				System.DateTime.Now.Minute*60 + 
				System.DateTime.Now.Second;
}
public static long getSystemCurrentTiker (){
	return System.DateTime.Now.Ticks/10000/1000;
}

//static void arrayIsContainString ( string str ,  Array arr  ){
//	bool  isContain = false;
//	foreach(string tempStr in arr){
//		if(str.Equals(tempStr)){
//			isContain = true;
//			break;
//		}
//	}
//	return isContain;
//}
//end
/*
//gwp --add space for skillName
static void skillNameFormat ( string skillName  ){
	string tempStr = "";
	Regex patt = new Regex("[A-Z][a-z0-9]*");
	
	for(System.Text.RegularExpressions.Match match in patt.Matches(skillName)){
		tempStr += match.Value + " ";
	}
	return tempStr;
}
//end
*/

//example: 10% chance get Equipment.
//if(computeChance(10,100)){  get Equipment}
public static bool computeChance ( int chanceValue ,   int per  ){
	 return UnityEngine.Random.value*per < chanceValue;
}

public static bool hasNetwork (){
//	return false;
	#if UNITY_EDITOR
//	print("Network.player.ipAddress:"+Network.player.ipAddress.ToString());
    if (Network.player.ipAddress.ToString() != "127.0f.0.1f")
    {
        return true;       
    }else{
	    return false;    
    }
	#endif
	
	#if (UNITY_IPHONE || UNITY_ANDROID)&& !UNITY_EDITOR
//	NotReachable	                 Network is not reachable
//	ReachableViaCarrierDataNetwork	 Network is reachable via carrier data network
//	ReachableViaWiFiNetwork	         Network is reachable via WiFi network
	if(iPhoneSettings.internetReachability == iPhoneNetworkReachability.NotReachable)
	{
		return false;
	}else{
		return true;
	}
	#endif
}
	
	public static void createObjFromPrb(ref GameObject prb, string prbPath, ref GameObject obj, Transform parent)
	{
		if(prb == null)
		{
			prb = Resources.Load(prbPath) as GameObject;
		}
		obj = Instantiate(prb) as GameObject;
		
		if(parent != null)
		{
			obj.transform.parent = parent;
		}	
	}
	
	public static void createObjFromPrb(ref GameObject prb, string prbPath, ref GameObject obj, Transform parent, Vector3 localPosition)
	{
		createObjFromPrb(ref prb, prbPath, ref obj, parent);
		
		obj.transform.localPosition = localPosition;	
	}
	
	public static void createObjFromPrb(ref GameObject prb, string prbPath, ref GameObject obj, Transform parent, Vector3 localPosition, Vector3 localScale)
	{
		createObjFromPrb(ref prb, prbPath, ref obj, parent, localPosition);
		
		obj.transform.localScale = localScale;	
	}
	
	
	public static void splashDamage(Character centerChar, Character atker, ICollection clt, Vector6 damage, int aoeRadius)
	{
		ArrayList cltClone = new ArrayList(clt);
		foreach(Character character in cltClone)
		{
			if(centerChar != character)
			{
				Vector2 vc2 = centerChar.transform.position - character.transform.position;
				if( isInOval(aoeRadius,aoeRadius, vc2) )
				{
					character.defenseAtk(damage, atker.gameObject);
				}
			}
			
		}
		cltClone.Clear();
	}
	
	public static void splashHeal(Character centerChar, Character healer, ICollection clt, int hp, int aoeRadius)
	{
		ArrayList cltClone = new ArrayList(clt);
		foreach(Character character in cltClone)
		{
			Vector2 vc2 = centerChar.transform.position - character.transform.position;
			if((character == centerChar) || isInOval(aoeRadius,aoeRadius, vc2) )
			{
				character.addHp(hp);
			}
		}
		cltClone.Clear();
	}
}
 
