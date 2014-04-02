using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {
	
	// Jugg S
	public UILabel dialogueDesc;
	public UILabel dialogueName;
	// Jugg E
	
	public GameObject dialogueFigure;
	public GameObject skipBtn;
	public static string dialogueInfo;
	
	// Jugg
    private UILabel dialogueDescInfo;
	
//    private SpriteText dialogueNameInfo;
	private ArrayList dialogueIList; 
	private int dialogueNum = 0;
	private string tempHeroIcon;
	private Vector3 tempSkipBtnVc3;
	private Vector3 tempInfoVc3;
	private Vector3 tempNameInfoVc3;
	private float xValue;
//	private Rect rect;
	private Rect btnRect;
	private Ray ray;
	private RaycastHit outHit;
	private string Direction;
	private string LEFT = "LEFT";
	private string RIGHT = "RIGHT";
	public GameObject phoneIcon;
	
	public void Awake (){
//		if(StaticData.isPhone)
//		{
//			gameObject.transform.localScale = new Vector3(0.95f,0.95f,1.0f);
//			xValue = 0.95f;
//		}else{
			gameObject.transform.localScale = new Vector3(0.9f,0.9f,1.0f);
			xValue=0.9f;
//		}
		// Jugg
	  dialogueDescInfo = dialogueDesc.GetComponent<UILabel>(); 
//	  dialogueNameInfo = dialogueName.GetComponent<SpriteText>(); 
	  
	  MsgCenter.instance.addListener(MsgCenter.CREATE_DIALOGUE, initDialogue); 
//	  tempSkipBtnVc3 = skipBtn.transform.position;
//	  tempInfoVc3 = dialogueDescInfo.transform.position;
//	  tempNameInfoVc3 = dialogueName.transform.position;
	  getBtnBoxCollider();
	 #if UNITY_ANDROID
		// Jugg
//		dialogueDescInfo.SetCharacterSize(25);
	 #endif
	}
	
	public void Start (){
	}
	
//	function getBgBoxCollider()
//	{
//		BoxCollider dialogBgCollider = GetComponent<BoxCollider>();	
//		Vector3 minVc3 = dialogBgCollider.bounds.min;
//		Vector3 maxVc3 = dialogBgCollider.bounds.max;
//		rect = new Rect(minVc3.x, minVc3.y, maxVc3.x-minVc3.x, maxVc3.y-minVc3.y);
//	}
	
	public void getBtnBoxCollider (){
		BoxCollider btnCollider = transform.Find("skipBtn").GetComponent<BoxCollider>();
		Vector3 btnMinVc3 = btnCollider.bounds.min;
		Vector3 btnMaxVc3 = btnCollider.bounds.max;
		btnRect = new Rect(btnMinVc3.x, btnMinVc3.y, btnMaxVc3.x-btnMinVc3.x, btnMaxVc3.y-btnMinVc3.y);
	}
	
	private void initDialogue ( Message msg  ){
//		ArrayList allData = MiniJSON.jsonDecode(dialogueInfo) as ArrayList;
//		Debug.Log("allData:"+allData);
//		for( int i=0; i<allData.Count; i++)
//		{
//			Hashtable lvData = allData[i] as Hashtable;
//			if(int.Parse(lvData["level"].ToString()) == GData.currentLevel && lvData["planet"].ToString() == GData.currentPlanet)
//			{
//				int difLevel = GData.difLevel;
//				switch(GData.difLevel){
//					case 1:
//						dialogueIList = lvData["Dialogue"] as ArrayList;
//						break;
//					case 2:
//						dialogueIList = lvData["Dialogue_n"] as ArrayList;
//						break;
//					case 3:
//						dialogueIList = lvData["Dialogue_s"] as ArrayList;
//						break;
//					default:
//						dialogueIList = lvData["Dialogue"] as ArrayList;
//						break;
//				}
//				if(dialogueIList == null){
//					dialogueIList = lvData["Dialogue"] as ArrayList;
//				}
//			}
//		} 
////		getSayData();
//		getDialogueDesc();				
	} 
	
	/*
	private void getSayData (){
		XmlNodeList allList = GData.heroTalkData.DocumentElement.GetElementsByTagName("planet");
		for(int i = 0; i<allList.Count; i++)
		{
			XmlNode node = allList[i];
			XmlNodeList childs = node.ChildNodes;
			string type = node.Attributes["type"].Value;
			string level = node.Attributes["level"].Value;
			Debug.Log("type:"+type+"      level:"+level);
			for(int j = 0; j<childs.Count; j++)
			{
				XmlNode childNode = childs[j];
				int id = int.Parse(childNode.Attributes["id"].Value);
				string heroType = childNode.Attributes["hero"].Value;
				Debug.Log("id:"+id+"      heroType:"+heroType);
				XmlNodeList nodes = childNode.ChildNodes;
				for(int k = 0; k<nodes.Count ; k++)
				{
					XmlNode nd = nodes[k];
					if("description".Equals(nd.Name))
					{
						Debug.Log("description:"+nd.InnerText);
//						currentHash[heroType] = nd.InnerText;
//						tempHash.Add(id,currentHash);
					}
				}
			}
		}
	}*/
	
	 private void getDialogueDesc (){
	        //int num = 0;
			ArrayList array = dialogueIList[dialogueNum] as ArrayList; 
			Texture2D txr2D = Resources.Load("dialogueIco/"+array[0]) as Texture2D;
			foreach( string key in HeroMgr.heroHash.Keys)
			{
				Hero heroD = HeroMgr.heroHash[key] as Hero;
				if(heroD.data.type == array[0])
				{
					break;
				}
			}
			
//			if(array[0] == "DL")
//			{
//				phoneIcon.renderer.enabled = false;
//				dialogueName.Text = "";
//			}
			
			if(dialogueNum == 0)
			{
				tempHeroIcon = array[0].ToString();
				Direction = LEFT;
			}
			if(tempHeroIcon == array[0])
			{
				if(Direction == LEFT)
				{
					left();
				}else{
					right();
				}
			}else{
				if(Direction == LEFT)
				{
					right();
				}else{
					left();
				}
			}
			tempHeroIcon = array[0].ToString();
	        dialogueFigure.renderer.material.mainTexture = txr2D;
//			dialogueName.Text = array[1];
			dialogueDescInfo.text = array[2].ToString();
			/*foreach(string type in array){
				if( num % 3 == 0){
					Debug.Log("1111111111111111111111111111");
				}else if(num % 3 == 1){
					Debug.Log("222222222222222222222222222");
				}else if(num % 3 == 2){
					dialogueDescInfo.Text = type;
				}
				num++;
			}	*/
	 }
	 
 	 private string getNickNameFromType ( string type  ){
		if(type == "WIZARD")
		{
			return "WIZARD";
		}
		else if(type == "COWBOY")
		{
			return "COWBOY";
		}
		else if(type == "HEALER")
		{
			return "HEALER";
		}
		else if(type == "MARINE")
		{
			return "MARINE";
		}
		else if(type == "TANK")
		{
			return "TANK";
		}
		else if(type == "TRAINER")
		{
			return "TRAINER";
		}
		else if(type == "PRIEST")
		{
			return "PRIEST";
		}
		else if(type == "DRUID")
		{
			return "DRUID";
		}
		else if(type == "DL")
		{
			return "???????????";
		}
		return "???????????";
//	 	switch(type)
//	 	{
//	 		case "WIZARD":return InitGameData.txt["WIZARD"];
//	 		case "COWBOY":return InitGameData.txt["COWBOY"];
//	 		case "HEALER":return InitGameData.txt["HEALER"];
//	 		case "MARINE":return InitGameData.txt["MARINE"];
//	 		case "TANK":return InitGameData.txt["TANK"];
//	 		case "TRAINER":return InitGameData.txt["TRAINER"];
//	 		case "PRIEST":return InitGameData.txt["PRIEST"];
//	 		case "DRUID":return InitGameData.txt["Ben Krell"];
//	 		case "DL":return "???????????";
//	 	}
	 }
	 
	 public void right (){
		Direction = "right";
		ExtensionMethods.SetX(transform.localScale, -xValue);
//	 	transform.localScale.x = -xValue;
		ExtensionMethods.SetX(dialogueDesc.transform.localScale, -1);
//		dialogueDesc.transform.localScale.x = -1;
		ExtensionMethods.SetX(skipBtn.transform.localScale, -1);
//		skipBtn.transform.localScale.x = -1;
		skipBtn.transform.position = tempSkipBtnVc3;
		dialogueDescInfo.transform.position = tempInfoVc3;
		dialogueDescInfo.transform.position+= new Vector3(-140,0,0);
		
		// Jugg
//		dialogueDescInfo.Anchor = SpriteText.Anchor_Pos.Upper_Left;
		
		ExtensionMethods.SetX(dialogueName.transform.localScale, -1);
//		dialogueName.transform.localScale.x = -1;
	 }
	 
	 public void left (){
	 	Direction = LEFT;
		ExtensionMethods.SetX(transform.localScale, xValue);
//	 	transform.localScale.x = xValue;
		ExtensionMethods.SetX(dialogueName.transform.localScale, 1);
//	 	dialogueName.transform.localScale.x = 1;
		ExtensionMethods.SetX(dialogueDesc.transform.localScale, 1);
//		dialogueDesc.transform.localScale.x = 1;
//		skipBtn.transform.localScale.x = 1;
		skipBtn.transform.position = tempSkipBtnVc3;
		dialogueDescInfo.transform.position = tempInfoVc3;
		
		// Jugg
//		dialogueDescInfo.Anchor = SpriteText.Anchor_Pos.Upper_Left;
	 }
	 
	 private void skipDialogueDesc (){
	    Message ms = new Message(MsgCenter.CREATE_START_Battle, this);
	    MsgCenter.instance.dispatch(ms);
	 } 
	 
	 private void nextDialogueDesc (){
		dialogueNum++;
	    if(dialogueNum < dialogueIList.Count){
	        getDialogueDesc();
	    }else{
	    	skipDialogueDesc();
	    }
	 }
	  
	public  void OnDestroy (){
		MsgCenter.instance.removeListener(MsgCenter.CREATE_DIALOGUE, initDialogue);
    }
    
    public bool getIsClickBtn ( Vector2 fingerPosition  ){
    	return btnRect.Contains(fingerPosition);
    }
    
}
