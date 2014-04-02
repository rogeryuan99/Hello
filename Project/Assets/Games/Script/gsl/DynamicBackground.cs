using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicBackground : MonoBehaviour {
	public Hashtable levelMapHash = new Hashtable();
	public Hashtable levelBackMapHash = new Hashtable();
	public Hashtable levelDecorHash = new Hashtable();
	//public Hashtable hazardHash = new Hashtable();
	protected Hashtable hazardHashTable = new Hashtable();
	
//	public 
	
	private Level curLevel;
	private Level nextLevel;
	private Level readyLevel = null;
	private float ScreenLogicWidth = 853.7f;
	private int index;
	private bool isWin = false;
	private int levelIndex;
	private float time = 1.5f;

	void Start () {
		
	}
	
	void Update () {
		
	}
	
	private void scaleBackGround(){
		//Debug.Log("Utils.getScreenLogicWidth()="+Utils.getScreenLogicWidth());
		float xx = Utils.getScreenLogicWidth()/854f;//856=1024/768 * 640
		float yy = Utils.getScreenLogicHeight()/640f;
		
		//this.transform.localScale = new Vector3(Mathf.Max(xx,yy)*0.85f,Mathf.Max(xx,yy),1);
		this.transform.localScale = new Vector3(Mathf.Max(xx,yy),Mathf.Max(xx,yy),1);
	}
	
	public void init(Level curLevel,Level nextLevel){
		scaleBackGround();
		//textureWidth = Camera.mainCamera.orthographicSize *2* ((float)Screen.width/(float)Screen.height) +20;
		this.curLevel = curLevel;
		this.nextLevel = nextLevel;
		
		drawMap(curLevel);
		if(nextLevel != null)
		{	
			drawMap(nextLevel);
		}
		drawDecor(curLevel);
		
		if(nextLevel != null && curLevel.chapter == nextLevel.chapter)
		{
			drawDecor(nextLevel);
		}
		
		drawBackMap(curLevel);
		
		if(nextLevel != null && (nextLevel.id-1)%4 == 0)
		{
			drawBackMap(nextLevel);
		}
		
	}
	
	private void drawMap(Level level){
		Debug.Log("map : MapPrefabs/L" + getMapName(level));
		GameObject prefab = Resources.Load("MapPrefabs/L" + getMapName(level)) as GameObject;
		GameObject go = Instantiate(prefab) as GameObject;
		go.transform.parent = this.transform;
		if(level == this.curLevel) 
		{
			go.transform.localPosition = Vector3.zero;
		}
		
		if(nextLevel != null && level == this.nextLevel) 
		{
			go.transform.localPosition = new Vector3(ScreenLogicWidth,0f,0f);
		}
		if(this.readyLevel != null && level == this.readyLevel) go.transform.localPosition = new Vector3(ScreenLogicWidth*2.0f,0f,0f);
		go.transform.localScale = Vector3.one;
		levelMapHash[getMapName(level)] = go;	
		
		drawHazardDecor(level,go);
	}
	
	private void drawBackMap(Level level){
		Debug.Log("backMap : MapPrefabs/LB" + getBackMapName(level));
		GameObject prefab = Resources.Load("MapPrefabs/LB"+getBackMapName(level)) as GameObject;
		GameObject go = Instantiate(prefab) as GameObject;
		go.transform.parent = this.transform;
//		if(level == this.curLevel) go.transform.localPosition = new Vector3((-ScreenLogicWidth/3.0f)*((level.id-1)%4),0f,5f);
//		if(nextLevel != null && level == this.nextLevel) go.transform.localPosition = new Vector3(ScreenLogicWidth-(ScreenLogicWidth/3.0f)*((level.id-1)%4),0f,5f);
		DynamicBackgroundFarMoveFactor df = go.GetComponent<DynamicBackgroundFarMoveFactor>();
		go.transform.localPosition = new Vector3(-df.factor*((level.id-1)%4),0f,5f);
		go.transform.localScale = Vector3.one;
		go.SendMessage("JumptoLv",this.curLevel.id,SendMessageOptions.DontRequireReceiver);
		levelBackMapHash[getBackMapName(level)] = go;
	}
	
	private void drawDecor(Level level){
		Debug.Log("decor : " + getDecorName(level));
		GameObject prefab = Resources.Load("MapPrefabs/Decor" + getDecorName(level)) as GameObject;
		if(prefab == null) return;
		GameObject go = Instantiate(prefab) as GameObject;
		go.transform.parent = this.transform;
		go.transform.localScale = Vector3.one;
		if(level == this.curLevel) go.transform.localPosition = Vector3.zero;
		if(nextLevel != null && level == this.nextLevel) go.transform.localPosition = new Vector3(ScreenLogicWidth,0f,0f);
		if(this.readyLevel != null && level == this.readyLevel)
		{
			go.transform.localPosition = new Vector3(ScreenLogicWidth*2.0f,0f,0f);
		}
		
		levelDecorHash[getDecorName(level)] = go;//chapter = 1, level = 1, ->1_0_1
//		drawCannonDecor(level,go);
//		drawTrapDoorDecor(level,go);
		
//		drawHazardDecor(level,go);
	}
	
	protected Hazard createHazard(GameObject go, HazardDef hazardDef)
	{
		string resStr = "gsl_Cell/" + hazardDef.Type;
		if(hazardDef.Type == HazardDef.HazardType.EnergyPole || hazardDef.Type == HazardDef.HazardType.PowerUp)
		{
			resStr += "Manager";
		}
			
		GameObject prefab = Resources.Load(resStr) as GameObject;
		GameObject decor = Instantiate(prefab) as GameObject;
		decor.transform.parent = go.transform;
		Hazard hazard = decor.GetComponent<Hazard>();
		
		
		if(hazard != null)
		{
			float xx = Mathf.Lerp(-Utils.getScreenLogicWidth()/2f,Utils.getScreenLogicWidth()/2f,(1+hazardDef.Position.x)/2f);
			float yy = Mathf.Lerp(-Utils.getScreenLogicHeight()/2f,Utils.getScreenLogicHeight()/2f,(1+hazardDef.Position.y)/2f);
			hazard.transform.localPosition = new Vector3(xx, yy, 0);
			
			//hazard.transform.localPosition = new Vector3(    hazardDef.Position.x,hazardDef.Position.y,0);
			
			hazard.HazardDef = hazardDef;
		}
		
		return hazard;
	}
	
	protected void drawHazardDecor(Level level,GameObject go)
	{
		List<Hazard> hazardList = new List<Hazard>();
		
		foreach(HazardDef hazardDef in level.hazardDefs)
		{
			Hazard hazardTemp = createHazard(go, hazardDef);
			
			if(hazardTemp != null)
			{
				hazardList.Add(hazardTemp);
				
				if(level.id == curLevel.id)
				{	
					hazardTemp.isEnabled = true;
					hazardTemp.calculateAttackRect();
				}
				else
				{
					hazardTemp.isEnabled = false;
				}
			}
			
			if(hazardList.Count > 0)
			{
				hazardHashTable[level.chapter.id + "_" + level.id] = hazardList;
			}
		}	
	}
	
//	private void drawCannonDecor(Level level,GameObject go){
//		foreach(HazardDef hd in level.hazardDefs){
//			
//			
//			
//			
//			
//		}
//	}
//	
//	private void drawTrapDoorDecor(Level level,GameObject go){
//		foreach(TrapDoorDef tdd in level.trapDoors){
//			
//		}
//	}
	
	public void GotoLevel(Level nextLevel){
		this.readyLevel = nextLevel;
		if(nextLevel != null && (nextLevel.id-1)%4 != 0) drawDecor(this.readyLevel);
		
		moveBackMap();
		moveMap();
		moveDecor();
	}
	
	private void moveBackMap(){
		GameObject curGo = levelBackMapHash[getBackMapName(this.curLevel)] as GameObject;
		GameObject nextGo = levelBackMapHash[getBackMapName(this.nextLevel)] as GameObject;
		if(this.curLevel.id%4 > 0){
			iTween.MoveTo(curGo,iTween.Hash(
				"x",(-ScreenLogicWidth/3.0f)*(this.curLevel.id%4),
				"time", time,
				"easetype","linear",
				"islocal",true));
		}else{
			iTween.MoveTo(curGo,iTween.Hash(
				"x",(float)(-ScreenLogicWidth*2),
				"time", time,
				"easetype","linear",
				"islocal",true,
				"oncomplete","moveBackMapFinished",
				"oncompletetarget",gameObject));	
			iTween.MoveTo(nextGo,iTween.Hash(
				"x",0,
				"time", time,
				"easetype","linear",
				"islocal",true));
			nextGo.SendMessage("GotoLv",this.nextLevel.id,SendMessageOptions.DontRequireReceiver);
		}
		curGo.SendMessage("GotoLv",this.nextLevel.id,SendMessageOptions.DontRequireReceiver);
	}
	
	private void moveMap(){
		iTween.MoveTo(levelMapHash[getMapName(this.curLevel)] as GameObject,iTween.Hash(
			"x",(float)(-ScreenLogicWidth),
			"time", time,
			"easetype","linear",
			"islocal",true,
			"oncomplete","moveFinished",
			"oncompletetarget",gameObject));
		iTween.MoveTo(levelMapHash[getMapName(this.nextLevel)] as GameObject,iTween.Hash(
			"x",0f,
			"time", time,
			"easetype","linear",
			"islocal",true));	
	}
	
	private void moveDecor(){
		GameObject curGo = levelDecorHash[getDecorName(this.curLevel)] as GameObject;
		if(curGo != null) iTween.MoveTo(curGo,iTween.Hash(
			"x",(float)(-ScreenLogicWidth),
			"time", time,
			"easetype","linear",
			"islocal",true));
		GameObject nextGo = levelDecorHash[getDecorName(this.nextLevel)] as GameObject;
		if(nextGo != null) iTween.MoveTo(nextGo,iTween.Hash(
			"x",0f,
			"time", time,
			"islocal",true,
			"easetype","linear"));
		GameObject readyGo = levelDecorHash[getDecorName(this.readyLevel)] as GameObject;
		if(readyGo != null){
			iTween.MoveTo(readyGo,iTween.Hash(
			"x",(float)(ScreenLogicWidth),
			"time", time,
			"islocal",true,
			"easetype","linear"));
		}
	}
	
	private void moveFinished()
	{
		
		
		GameObject mapGo = levelMapHash[getMapName(this.curLevel)] as GameObject;
		levelMapHash.Remove(getMapName(this.curLevel));
		Destroy(mapGo);
		GameObject decorGo = levelDecorHash[getDecorName(this.curLevel)] as GameObject;
		levelDecorHash.Remove(getDecorName(this.curLevel));
		//hazardHash.Remove(getDecorName(this.curLevel));
		Destroy(decorGo);
		this.curLevel = this.nextLevel;
		this.nextLevel = this.readyLevel;
		this.readyLevel = null;
		
		string hazardKey = curLevel.chapter.id + "_" + curLevel.id;
		List<Hazard> hazardList = hazardHashTable[hazardKey] as List<Hazard>;
		if(hazardList != null && hazardList.Count > 0)
		{
			foreach(Hazard hazard in hazardList)
			{
				hazard.isEnabled = true;
				hazard.calculateAttackRect();
			}
			hazardList.Clear();
			
			hazardHashTable.Remove(hazardKey);
		}
		
		drawMap(this.nextLevel);
		if(nextLevel != null && (this.nextLevel.id-1)%4 == 0) drawBackMap(this.nextLevel);
		
//		Hazard h = hazardHash[getDecorName(this.curLevel)] as Hazard;
//		h.calculateAttackRect();
		
		LevelMgr.Instance.MoveBGTextureFinish();
	}
	
	private void moveBackMapFinished(){
		GameObject go = levelBackMapHash[getBackMapName(this.curLevel)] as GameObject;	
		levelBackMapHash.Remove(getBackMapName(this.curLevel));
		Destroy(go);
	}
	
	public void OnSuccess(bool isWin){
		GameObject go = levelMapHash[getMapName(this.curLevel)] as GameObject;
		go.SendMessage("OnSuccess",isWin,SendMessageOptions.DontRequireReceiver);
	}
	
	private string getMapName(Level lv){
		//return lv.chapter.id+"_"+lv.id;	
		return MapMgr.Instance.currentChapterIndex +"_"+lv.id;	
	}
	
	private string getBackMapName(Level lv){
		int stage = (lv.id-1)/4;
		//return lv.chapter.id+"_"+stage;
		return MapMgr.Instance.currentChapterIndex +"_"+stage;
	}
	
	private string getDecorName(Level lv){
		//return lv.chapter.id+"_"+(lv.id-1)+"_"+lv.id;
		return MapMgr.Instance.currentChapterIndex +"_"+(lv.id-1)+"_"+lv.id;
	}
}
