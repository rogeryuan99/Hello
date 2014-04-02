using UnityEngine;
using System.Xml;
using System.Collections;

public class AnimaFileMgr{

	public static Hashtable heroesActHash = new Hashtable();
	public static Hashtable heroesBoneHash = new Hashtable();
	
	public static Hashtable actHash = new Hashtable();
	public static Hashtable boneHash = new Hashtable();	
	
	public static void initEftActData (){
		/* get <eftAct> node ChildNode list  (<Trainer_skillA name="Trainer_skillA">, <Marine_skillB_foot name="Marine_skillB_foot">) //*/
		XmlNodeList eftActXmlList = StaticData.getEftActXML();
		for( int i=0; i<eftActXmlList.Count; i++)
		{
			Hashtable actMgr = new Hashtable();
			ActData[] actDatas;
			Hashtable actList  = new Hashtable();
			
			// get <Trainer_skillA name="Trainer_skillA"> or <Marine_skillB_foot name="Marine_skillB_foot"> ..
			XmlNode dataNode = eftActXmlList[i];
			
			string eftName = dataNode.Attributes.GetNamedItem("name").Value;
			
			// get <act name="Trainer_skillA" totalFrame="26"> or <act name="j" totalFrame="31"> ..
			XmlNodeList actXMLList = dataNode.ChildNodes;
//			Debug.Log("actXMLList.Count " + actXMLList.Count );
			int length = actXMLList.Count;
			actDatas = new ActData[length];
			for( int y=0; y< actXMLList.Count; y++)
			{
				XmlNode node = actXMLList[y];
				string actName = node.Attributes.GetNamedItem("name").Value;
				string total   = node.Attributes.GetNamedItem("totalFrame").Value;
				
				// Parser <act name="Trainer_skillA" totalFrame="26"> node's childnodes
				Hashtable dataHash = PieceAnimation.xmllistToHash(node.ChildNodes);
				ActData actD   = new ActData(actName, dataHash, int.Parse(total));
				
				actD.loopCycles = -1;
				actD.fps = 24;
				actDatas[y]= actD;
				// actName is <act name="Trainer_skillA" totalFrame="26"> node Attributes "name" is Trainer_skillA
				actList[actName] = actD;
			}
			/**
			 * actDatas's length is 1 or n, the data format is <act name="Trainer_skillA" totalFrame="26"> node child <xuanzhuan21>, <xuanzhuan22>..
			 * actList key is <act name="j" totalFrame="31"> node Attributes "name" is j ,data same as actDatas
			 */ 
			actMgr.Add("actDatas",actDatas); 
			actMgr.Add("actList",actList);
			// eftName is <act name="Trainer_skillA" totalFrame="26"> node Attributes "name" is Trainer_skillA
			actHash[eftName] = actMgr;
		}
	}
	
	public static void initEftBoneData (){
		/* get <eftBone> node ChildNode list  (<Trainer_skillA name="Trainer_skillA">, <Marine_skillB_foot name="Marine_skillB_foot">) //*/
		XmlNodeList eftBoneXmlList = StaticData.getEftBoneXML();
		for(int y=0; y<eftBoneXmlList.Count; y++)
		{
			Hashtable boneMgr = new Hashtable();
			ArrayList partNameArray = new ArrayList();
			ArrayList pos1Array = new ArrayList();
			ArrayList cpos1Array = new ArrayList();
			ArrayList rotationArray = new ArrayList();
				
			XmlNode dataNode = eftBoneXmlList[y]; 
			string eftName = dataNode.Attributes.GetNamedItem("name").Value;
			XmlNodeList boneXMLList = dataNode.ChildNodes;	
			for( int i=0; i< boneXMLList.Count; i++)
			{
				XmlNode node = boneXMLList[i];
				string partName = node.Attributes.GetNamedItem("name").Value;
				partNameArray.Add( partName);
				
				string posStr = node.Attributes.GetNamedItem("pos").Value;
				Vector2 pos1 = new Vector2( int.Parse( posStr.Split("|"[0])[0] ), int.Parse( posStr.Split("|"[0])[1] ) );
				pos1Array.Add(  pos1);
				
				string cPosStr = node.Attributes.GetNamedItem("epos").Value;
				Vector2 cPos1 = new Vector2( int.Parse( cPosStr.Split("|"[0])[0] ), int.Parse( cPosStr.Split("|"[0])[1] ) );
				cpos1Array.Add(  cPos1);
				
				string rotation = node.Attributes.GetNamedItem("roration").Value;
				 rotationArray.Add(  rotation);
			}
			 boneMgr["partName"] = partNameArray;
			 boneMgr["pos1"] = pos1Array;
			 boneMgr["cpos1"] = cpos1Array;
			 boneMgr["rotation"] = rotationArray; 
			 // this hashtable key is <Trainer_skillA name="Trainer_skillA"> node Attributes "name" is Trainer_skillA
			 boneHash[eftName] = boneMgr;
		}
	}
	
	//cache hero animation data
	public static void initHeroActData ( string heroType ,   string xmlStr  ){
		Hashtable actMgr = new Hashtable();
		ActData[] actDatas;
		Hashtable actList  = new Hashtable();
		
		XmlDocument loadXML = new XmlDocument();
		loadXML.LoadXml(xmlStr);
		
		XmlNodeList actXMLList = loadXML.DocumentElement.GetElementsByTagName("act");
		int length = actXMLList.Count;
		actDatas = new ActData[length];
		
		for( int y=0; y< actXMLList.Count; y++)
		{
			XmlNode node = actXMLList[y];
			string actName = node.Attributes.GetNamedItem("name").Value;
			string total   = node.Attributes.GetNamedItem("totalFrame").Value;
			
			Hashtable dataHash = PieceAnimation.xmllistToHash(node.ChildNodes);
			ActData actD   = new ActData(actName, dataHash, int.Parse(total));
			
			actD.loopCycles = -1;
			actD.fps = 24;
			actDatas[y]= actD;
			actList[actName] = actD;
		}
		actMgr.Add("actDatas",actDatas); 
		actMgr.Add("actList",actList);
		heroesActHash[heroType] = actMgr;
	}
	
	public static void initHeroBoneData ( string heroType ,   string xmlStr  ){
		Hashtable boneMgr = new Hashtable();
		ArrayList partNameArray = new ArrayList();
		ArrayList pos1Array = new ArrayList();
		ArrayList cpos1Array = new ArrayList();
		ArrayList rotationArray = new ArrayList();
		
		XmlDocument xml = new XmlDocument();
		xml.LoadXml(xmlStr);
		XmlNodeList boneXMLList = xml.DocumentElement.GetElementsByTagName("part");
		
		for( int i=0; i< boneXMLList.Count; i++)
		{
			XmlNode node = boneXMLList[i];
			string partName = node.Attributes.GetNamedItem("name").Value;
			partNameArray.Add( partName);
			
			string posStr = node.Attributes.GetNamedItem("pos").Value;
			Vector2 pos1 = new Vector2( float.Parse( posStr.Split("|"[0])[0] ), float.Parse( posStr.Split("|"[0])[1] ) );
			pos1Array.Add(  pos1);
			
			string cPosStr = node.Attributes.GetNamedItem("epos").Value;
			Vector2 cPos1 = new Vector2( float.Parse( cPosStr.Split("|"[0])[0] ), float.Parse( cPosStr.Split("|"[0])[1] ) );
			cpos1Array.Add(  cPos1);
			
			string rotation = node.Attributes.GetNamedItem("roration").Value;
			 rotationArray.Add(  rotation);
		}
		boneMgr["partName"] = partNameArray;
		boneMgr["pos1"] = pos1Array;
		boneMgr["cpos1"] = cpos1Array;
		boneMgr["rotation"] = rotationArray; 
		heroesBoneHash[heroType] = boneMgr;
	}
}
