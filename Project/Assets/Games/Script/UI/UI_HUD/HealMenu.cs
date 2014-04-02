using UnityEngine;
using System.Collections;

public class HealMenu : MonoBehaviour {
public PackedSprite[] icons;
public GameObject[] sectors;
public Healer healer;
public GameObject headIcon;
public GameObject glow;
public ArrayList posAry;
public ArrayList rotationArray;
public ArrayList selectPicture;
public ArrayList icoPosition;
private GameObject selectObj;
private int selectI;
public TargetIndicator targetIndtr;
public bool  selectIco = false; 
//private GameObject container;

public void initIcons (){
	Debug.Log("<---------initIcons----->");
	if(healer == null) return;
		this.gameObject.transform.rotation = new Quaternion(
			this.gameObject.transform.rotation.x,
			this.gameObject.transform.rotation.y, 
			0,
			this.gameObject.transform.rotation.w 
			);
//	this.gameObject.transform.rotation.z = 0 ;
	Debug.Log("<---------icons.length-----:"+icons.Length);
	GameObject sector = null;
	for(int j = 0;j< icons.Length ; j++){
		sector = icons[j].gameObject;
		sector.gameObject.transform.rotation = new Quaternion(
				sector.gameObject.transform.rotation.x,
				sector.gameObject.transform.rotation.y, 
				0,
				sector.gameObject.transform.rotation.w 
				);
//		sector.gameObject.transform.rotation.z = 0;
	}
	for(int z = 0;z< sectors.Length ; z++){
		sector = sectors[z].gameObject;
		sector.gameObject.transform.rotation = new Quaternion(sector.gameObject.transform.rotation.x,sector.gameObject.transform.rotation.y, 0,sector.gameObject.transform.rotation.w );
//		sector.gameObject.transform.rotation.z = 0;
	}
	glow.renderer.enabled = false;
	Hashtable heroes = HeroMgr.heroHash; 
	for(int i=0; i<icons.Length ;i++)
	{
		PackedSprite icon = icons[i];
		sector = sectors[i];
		icon.gameObject.SetActiveRecursively(false);
		if( i< heroes.Count-1)
		{
			print("----->"+heroes.Count);  
			int num = heroes.Count-2;
			if(heroes.Count == 4 && healer.model.transform.localScale.x < 0){
				 num = num + 1;
			}
			Debug.Log("posAry[num]:"+posAry.Count);
			ArrayList pos = posAry[num] as ArrayList;
			ArrayList rotArray = rotationArray[heroes.Count-2] as ArrayList;
			Vector2 vc2 = (Vector2)pos[i];
			sector.transform.localPosition = new Vector3(vc2.x, vc2.y, 404);
//			sector.transform.localPosition.x = vc2.x;
//			sector.transform.localPosition.y = vc2.y;
//			sector.transform.localPosition.z = 404;
			sector.transform.Rotate(new Vector3(0,0,float.Parse(rotArray[i].ToString())));
			
			PackedSprite spiSpt; 
			spiSpt = sector.GetComponent<PackedSprite>();
			spiSpt.DoAnim("picture");
			
//			Texture2D txr2D = Resources.Load("healerIco/"+pic[i]);
//			sector.renderer.material.mainTexture = txr2D;
			
			ArrayList icoPost = icoPosition[num] as ArrayList;
			Vector2 icop = (Vector2)icoPost[i];
			icon.gameObject.transform.localPosition = new Vector3(icop.x, icop.y, icon.gameObject.transform.localPosition.z);
//			icon.gameObject.transform.localPosition.x = icop.x;
//			icon.gameObject.transform.localPosition.y = icop.y;
			sector.renderer.enabled = true;
			icon.gameObject.renderer.enabled = true;

		}
		else{
			icon.gameObject.renderer.enabled = false;
			sector.renderer.enabled = false;
		}
	}
	int index=0;
	foreach( string key in heroes.Keys)
	{
		Hero hero = heroes[key] as Hero;
		HeroData heroD = hero.data as HeroData;
		if(heroD.type != HeroData.HEALER){
			GameObject headIconObj = (icons[index]).gameObject;
			headIconObj.SetActiveRecursively(true);
			HeadIcon headIconDoc   =  headIconObj.GetComponent<HeadIcon>();
			headIconDoc.heroType = heroD.type;
			headIconDoc.UpdataHpBar(hero);
			Debug.Log(heroD.type+"--------"+hero.getHp());
			PackedSprite spt = icons[index];
			spt.DoAnim(heroD.type);
			index++;
		}else{
		
		}
	}
//	rotation();
}

public void rotation (){
//		if(container == null){
//			container = new GameObject("healMenu");
//			container.transform.parent = this.gameObject.transform.parent;
//			this.gameObject.transform.parent = container.transform;
//			container.transform.position = this.gameObject.transform.position; 
//			this.gameObject.transform.localPosition = new Vector2(0,0);
//		}
//		container.gameObject.transform.rotation.eulerAngles = Vector3(0,0,90);
//		container.transform.position.y = container.transform.position.y-100;  
	if(healer.gameObject.transform.position.x > 304 || (healer.gameObject.transform.position.x > 0 && healer.gameObject.transform.position.y > 50)){  
		if(healer.gameObject.transform.position.y < -266){
			this.gameObject.transform.Rotate(new Vector3(0,0,45));
			this.gameObject.transform.localPosition = new Vector3(
					this.gameObject.transform.localPosition.x - 78,
					this.gameObject.transform.localPosition.y - 32,
					this.gameObject.transform.localPosition.z
					);
//			this.gameObject.transform.localPosition.x =  this.gameObject.transform.localPosition.x - 78;
//			this.gameObject.transform.localPosition.y =  this.gameObject.transform.localPosition.y - 32;
		}else if(healer.gameObject.transform.position.y < 89){
			this.gameObject.transform.Rotate(new Vector3(0,0,90)); 
			this.gameObject.transform.localPosition = new Vector3(
					this.gameObject.transform.localPosition.x - 113,
					this.gameObject.transform.localPosition.y - 114,
					this.gameObject.transform.localPosition.z
					);
//			this.gameObject.transform.localPosition.x =  this.gameObject.transform.localPosition.x - 113;
//			this.gameObject.transform.localPosition.y =  this.gameObject.transform.localPosition.y - 114;
		}else{
			this.gameObject.transform.Rotate(new Vector3(0,0,135));
			this.gameObject.transform.localPosition = new Vector3(
					this.gameObject.transform.localPosition.x - 76,
					this.gameObject.transform.localPosition.y - 206,
					this.gameObject.transform.localPosition.z
					);
//			this.gameObject.transform.localPosition.x =  this.gameObject.transform.localPosition.x -76;
//			this.gameObject.transform.localPosition.y =  this.gameObject.transform.localPosition.y - 206;
		}
			
		for(int i = 0;i< icons.Length ; i++){
			GameObject sector = icons[i].gameObject;
			if(healer.gameObject.transform.position.y < -266){
				sector.gameObject.transform.Rotate(new Vector3(0,0,-45));
			}else if(healer.gameObject.transform.position.y < 89){
				sector.gameObject.transform.Rotate(new Vector3(0,0,-90));
			}else{
				sector.gameObject.transform.Rotate(new Vector3(0,0,-135));
			}
		}
			
	}else if(healer.gameObject.transform.position.x < (-304) || (healer.gameObject.transform.position.x < 0 && healer.gameObject.transform.position.y > 50)){ 
			GameObject sector = null;
		if(healer.gameObject.transform.position.y < -266){
			this.gameObject.transform.Rotate(new Vector3(0,0,-45));
			this.gameObject.transform.localPosition = new Vector3(
					this.gameObject.transform.localPosition.x + 73,
					this.gameObject.transform.localPosition.y - 50,
					this.gameObject.transform.localPosition.z
					);
//			this.gameObject.transform.localPosition.x =  this.gameObject.transform.localPosition.x + 73;
//			this.gameObject.transform.localPosition.y =  this.gameObject.transform.localPosition.y - 50;
		}
		else if(healer.gameObject.transform.position.y < 89){
			this.gameObject.transform.Rotate(new Vector3(0,0,-90));
			if(HeroMgr.heroHash.Count < 4){ 
				this.gameObject.transform.localPosition = new Vector3(
					this.gameObject.transform.localPosition.x + 110,
					this.gameObject.transform.localPosition.y,
					this.gameObject.transform.localPosition.z
					);
//				this.gameObject.transform.localPosition.x =  this.gameObject.transform.localPosition.x + 110;
			}else{
				this.gameObject.transform.localPosition = new Vector3(
					this.gameObject.transform.localPosition.x + 133,
					this.gameObject.transform.localPosition.y,
					this.gameObject.transform.localPosition.z
					);
//				this.gameObject.transform.localPosition.x =  this.gameObject.transform.localPosition.x + 133;
			}
			this.gameObject.transform.localPosition = new Vector3(
					this.gameObject.transform.localPosition.x,
					this.gameObject.transform.localPosition.y - 137,
					this.gameObject.transform.localPosition.z
					);
//			this.gameObject.transform.localPosition.y =  this.gameObject.transform.localPosition.y - 137;
		}else{
			this.gameObject.transform.Rotate(new Vector3(0,0,-135));
			this.gameObject.transform.localPosition = new Vector3(
				this.gameObject.transform.localPosition.x + 82,
				this.gameObject.transform.localPosition.y - 195,
				this.gameObject.transform.localPosition.z
				);
//			this.gameObject.transform.localPosition.x =  this.gameObject.transform.localPosition.x + 82;
//			this.gameObject.transform.localPosition.y =  this.gameObject.transform.localPosition.y - 195;
		}
		for(int j = 0;j< icons.Length ; j++){
			sector = icons[j].gameObject;
			if(healer.gameObject.transform.position.y < -266){
				sector.gameObject.transform.Rotate(new Vector3(0,0,45));
			}else if(healer.gameObject.transform.position.y < 89){	
				sector.gameObject.transform.Rotate(new Vector3(0,0,90));
			}else{
				sector.gameObject.transform.Rotate(new Vector3(0,0,135));	
			}
		}
	}
}

public void fingerDownHandler ( Vector2 pos  ){
	rotation();
	HeadIcon targetIcon = selectIcon(pos);
	if(targetIcon)
	{
		glow.renderer.enabled = true;
		glow.transform.position = targetIcon.transform.position+ new Vector3(0,0,1);
	}else{
		glow.renderer.enabled = false;
	}
	
	if(healer.model.transform.localScale.x > 0){
		if(HeroMgr.heroHash.Count == 3){
			this.gameObject.transform.localPosition = new Vector3(
				this.gameObject.transform.localPosition.x - 3,
				this.gameObject.transform.localPosition.y,
				this.gameObject.transform.localPosition.z
				);
//			this.gameObject.transform.localPosition.x -= 3;
		}else{
				this.gameObject.transform.localPosition = new Vector3(
				this.gameObject.transform.localPosition.x - 12,
				this.gameObject.transform.localPosition.y,
				this.gameObject.transform.localPosition.z
				);
//			this.gameObject.transform.localPosition.x -= 12;
		}
	}
}

public void fingerMoveHandler ( Vector2 pos  ){
	clearSelect();
	HeadIcon targetIcon = selectIcon(pos);
	if(targetIcon)
	{
		glow.renderer.enabled = true;
		glow.transform.position = targetIcon.transform.position+new Vector3(0,0,1);
	}else{
		glow.renderer.enabled = false;
	}
}

public void clearSelect (){
	// Hashtable heroes = HeroMgr.heroHash; // never used
	Hero hero=HeroMgr.getHeroByType(HeroData.HEALER);
	if(selectObj != null && hero != null){
//		Array spic = picture[heroes.Count-2];
//		Texture2D txr2D = Resources.Load("healerIco/"+spic[selectI]);
//		selectObj.renderer.material.mainTexture = txr2D;
		PackedSprite spiSpt; 
		spiSpt = selectObj.GetComponent<PackedSprite>();
		spiSpt.DoAnim("picture");
		
		selectObj = null;
		selectI = 0 ;
	}
}

public bool fingerUpHandler ( Vector2 pos  ){  
	 glow.renderer.enabled = false;
	HeadIcon targetIcon = selectIcon(pos);
	bool  isClick = false;
	if(targetIcon)
	{
		Hero targetHero = HeroMgr.getHeroByType(targetIcon.heroType);
		if(targetHero && healer.data.isDead == false){
			healer.startHeal(targetHero);
			isClick = true;
		}
	}	
	targetIndtr.hide();

		this.gameObject.transform.localPosition = new Vector3(
				this.gameObject.transform.localPosition.x,
				this.gameObject.transform.localPosition.y - 1000,
				this.gameObject.transform.localPosition.z
				);
//	this.transform.position.y = -1000;
	return isClick;
}

public void glowIconByType ( string type  ){
	clearSelect();
	for(int i=0; i<icons.Length ;i++)
	{
		PackedSprite icon = icons[i];
		HeadIcon headIconDoc = icon.gameObject.GetComponent<HeadIcon>();
		if(headIconDoc.heroType == type)
		{
			Hero hero = HeroMgr.getHeroByType(HeroData.HEALER);
			if(hero != null){		
				//xingyihua
			    GameObject sector = sectors[i]; 
				Hashtable heroes = HeroMgr.heroHash;
				int num = heroes.Count-2;
				if(heroes.Count == 4 && healer.model.transform.localScale.x < 0){
					 num = num + 1;
					 Debug.Log("num----->"+num);
				}
				if(num < 0 || i > num){
					return;
				}
				ArrayList pos = posAry[num] as ArrayList;
//				Array spic = selectPicture[heroes.Count-2];
				Vector2 vc2 = (Vector2)pos[i];
//				Texture2D txr2D = Resources.Load("healerIco/"+spic[i]);
//				spi.renderer.material.mainTexture = txr2D;
				sector.transform.localPosition = new Vector3(vc2.x, vc2.y, sector.transform.localPosition.z);
//				sector.transform.localPosition.x = vc2.x;
//				sector.transform.localPosition.y = vc2.y;
				
				PackedSprite spiSpt; 
				spiSpt = sector.GetComponent<PackedSprite>();
				spiSpt.DoAnim("selectPicture");
				
				selectObj = sector;
				selectI = i;
				//end
			
				glow.renderer.enabled = true;
				glow.transform.position = icon.transform.position+new Vector3(0,0,1);
				break;
			}
		}
	}
}
public void heroDown ( Message msg  ){
	if(this.transform.position.y < -500){
		return;
	}
	Character character = msg.data as Character;
	GameObject charObj = character.gameObject;
	if(charObj.gameObject.transform.tag == "Player"){
		glow.renderer.enabled = false;
//		for(int i = 0;i < icons.length;i++){
//			PackedSprite icon = icons[i];
//			GameObject iconObj = icon.gameObject;
//			icon.gameObject.transform.position.x = 1000;
////			icon.gameObject.renderer.enabled = false;
//		}
//		for(int y = 0;y < sectors.length;y++){
//			GameObject sector = sectors[y];
//			sector.transform.position.x = 1000;
////			sector.renderer.enabled = false;
//		}
		Hero heroHealer = HeroMgr.getHeroByType(HeroData.HEALER);
		if(heroHealer != null && heroHealer.data.isDead == false){
			selectObj = null;
			selectI = 0 ;
			Invoke("startInitIcons",0.5f);
		}else{
				this.gameObject.transform.localPosition = new Vector3(
				this.gameObject.transform.localPosition.x,
				this.gameObject.transform.localPosition.y - 1000,
				this.gameObject.transform.localPosition.z
				);
//			this.transform.position.y = -1000;
		}
	}
}
public void startInitIcons (){
	initIcons();
	this.transform.position = healer.transform.position+new Vector3(0,200,-400);	
	rotation();
}
public HeadIcon selectIcon ( Vector2 pos  ){
	for(int i=0; i<icons.Length; i++)
	{
		PackedSprite spt = icons[i];
		if(!spt.gameObject.renderer.enabled)continue;
	
		GameObject gObj = spt.gameObject;
		HeadIcon headIconDoc = gObj.GetComponent<HeadIcon>();
		Rect rect = StaticData.get2DRect(gObj);
		rect.width *= 1.2f;
		rect.x -= rect.width * 0.1f;
		rect.height *= 1.2f;
		rect.y -= rect.height * 0.1f;
		/*
		rect.x -= rect.width * 0.1f;
		rect.y -= rect.height * 0.1f;
		*/  
		
		//xingyihua
		GameObject sec = sectors[i];  
		Rect sRect = StaticData.get2DRect(sec);
		sRect.width *= 1f;
		sRect.x -= sRect.width * 0.1f;
		sRect.height *= 1f;
		sRect.y -= sRect.height * 0.1f;
		
		Hero hero=HeroMgr.getHeroByType(HeroData.HEALER);
		Hero character = null;
		if(hero != null){
			if(rect.Contains(pos) || sRect.Contains(pos))
			{
				clearSelect();
				//xingyihua
			    GameObject sector = sectors[i]; 
				Hashtable heroes = HeroMgr.heroHash;
				int num = heroes.Count-2;
				if(heroes.Count == 4 && healer.model.transform.localScale.x < 0){
					 num = num + 1;
					 Debug.Log("num----->"+num);
				}
				if(num < 0 || i > num){
					return null;
				}
				ArrayList posA = posAry[num] as ArrayList;
//				Array spic = selectPicture[heroes.Count-2];
				Vector2 vc2 = (Vector2)posA[i];
//				Texture2D txr2D = Resources.Load("healerIco/"+spic[i]);
//				spi.renderer.material.mainTexture = txr2D;
					sector.transform.localPosition = new Vector3(vc2.x, vc2.y, sector.transform.localPosition.z);
//				sector.transform.localPosition.x = vc2.x;
//				sector.transform.localPosition.y = vc2.y;
				PackedSprite spiSpt; 
				spiSpt = sector.GetComponent<PackedSprite>();
				spiSpt.DoAnim("selectPicture");
				
				selectObj = sector;
				selectI = i;
				int index = 0;
				foreach( string key in heroes.Keys)
				{
					Hero heroObj = heroes[key] as Hero;
					HeroData heroD = heroObj.data as HeroData;
					if(heroD.type != HeroData.HEALER){
						if(index == i){
							character = heroObj;
							break;
						}
						index++;
					}
				}				
				selectIco = true;
				Debug.Log(headIconDoc.heroType+"----->");
				targetIndtr.show(hero.data.type, character.data.type);
			//end
				return headIconDoc;
			}
		}
	}
	selectIco = false;
	return null;
}
public bool isAddHp ( GameObject obj  ){
	 bool  b = false;
	Vector2 pos = new Vector2(obj.transform.position.x,obj.transform.position.y); 
	for(int i=0; i<icons.Length; i++)
	{
		PackedSprite spt = icons[i];
		if(!spt.gameObject.renderer.enabled)continue;
	
		GameObject gObj = spt.gameObject;
		// HeadIcon headIconDoc = gObj.GetComponent<HeadIcon>(); // never used
		Rect rect = StaticData.get2DRect(gObj);
		rect.width *= 1.0f;
		rect.x -= rect.width * 0.1f;
		rect.height *= 1.0f;
		rect.y -= rect.height * 0.4f;
		
		GameObject sec = sectors[i];  
		Rect sRect = StaticData.get2DRect(sec);
		sRect.width *= 1.2f;
		sRect.x -= sRect.width * 0.1f;
		sRect.height *= 1.0f;
		sRect.y -= sRect.height * 0.4f;
		
		Hero hero=HeroMgr.getHeroByType(HeroData.HEALER);
		if(hero != null){
			if(rect.Contains(pos) || sRect.Contains(pos))
			{
				b = true;
			}
		}
	}
	return b;
}
public void Awake (){
	MsgCenter.instance.addListener(MsgCenter.FALL_DOWN, heroDown);
	MsgCenter.instance.addListener(MsgCenter.HERO_RELIVE, heroDown);
	icoPosition = 
		new ArrayList()
		{ 
			new ArrayList()
			{
				new Vector2(5,13.4231f)
			},
			new ArrayList()
			{
				new Vector2(-62.34f,-18.47f), 
				new Vector2(49.53484f,-18.47f)
			},
			new ArrayList()
			{
				new Vector2(-85.61f,-63.19f), 
				new Vector2(6.22f,-8.3f), 
				new Vector2(103.06f,-56.08f)
			},
			new ArrayList(){
				new Vector2(-85.61f,-63.19f), 
				new Vector2(6.22f,-8.3f), 
				new Vector2(103.06f,-56.08f)}
		};
	posAry= 
		new ArrayList()
		{
			new ArrayList(){
				new Vector2(-9.905991f,-32.36466f)
			},
			new ArrayList(){
				new Vector2(-52.40f,-63.5f),
				new Vector2(19.21f,-48.29f)
			}, 
			new ArrayList()
			{
				new Vector2(-60.35f,-94.33f),
				new Vector2(-5.74f,-44.85f),
				new Vector2(60.29f,-65.69f)
			},
			new ArrayList()
			{
				new Vector2(-60.35f,-94.33f),
				new Vector2(-5.74f,-44.85f),
				new Vector2(60.29f,-65.69f)}
		};
	rotationArray= 
		new ArrayList()
		{
			new ArrayList(){60},
			new ArrayList(){90,30},
			new ArrayList(){120,60,0}};
//	selectPicture = [["1select"],["2leftSelect","2rightSelect"],["3leftSelect","3InSelect","3rightSelect"]];		
}

public void Start (){
	Healer hb = HeroMgr.getHeroByType(HeroData.HEALER) as Healer;
	healer = hb;
}
public void Update (){
//	if( Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Ended)
//	{
//		Vector2 touchPtUp = Input.GetTouch(0).position;
//		fingerUpHandler(touchPtUp);
//	}
}
public void OnDestroy (){
	MsgCenter.instance.removeListener(MsgCenter.FALL_DOWN, heroDown);
	MsgCenter.instance.removeListener(MsgCenter.HERO_RELIVE, heroDown);

}
}