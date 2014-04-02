using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cutscenes : MonoBehaviour {
	public Camera viewCamera;
	public UITexture uiTexture;
	public GameObject uiBtn;
	public GameObject SkipDlg;
	
	protected int curPage = 0;
	protected int curPanel = 0;
	protected int chapterID = 0;
	protected int levelID = 0;
	protected int pageCount = 0;
	protected Vector3 viewCameraPos = Vector3.zero;
	protected bool isSkipBtn = false;
	protected float startPosX = 0f;
	protected float endPosX = 0f;
	protected float resultPosX = 0f;
	protected bool isMouseDown = false;
	protected bool isBtnFlag = false;
	public bool isEnd = true;
	
	const float ArtW = 2030f;
	const float ArtH = 1150f;
	
	protected List<Vector3> viewCameraInQuadrant = new List<Vector3>(){
		new Vector3(-0.25f,0.2f,0f),
		new Vector3(0.25f,0.2f,0f),
		new Vector3(-0.25f,-0.2f,0f),
		new Vector3(0.25f,-0.2f,0f)
	};
	
	protected List<Vector3> cutscenesDef = new List<Vector3>(){
		new Vector3(1,1,1),			//1--chapterID,2--levelID,3--pageCount
		new Vector3(1,4,1),
		new Vector3(1,8,1),
		new Vector3(1,12,2),
		new Vector3(2,1,1),
		new Vector3(2,4,1),
		new Vector3(2,8,1),
		new Vector3(2,12,2),
		new Vector3(3,1,1),
		new Vector3(3,4,1),
		new Vector3(3,8,1),
		new Vector3(3,12,2),
		new Vector3(4,1,1),
		new Vector3(4,4,1),
		new Vector3(4,8,1),
		new Vector3(4,11,1),
		new Vector3(4,12,2),
		new Vector3(5,1,1),
		new Vector3(5,4,1),
		new Vector3(5,8,1),
		new Vector3(5,11,1),
		new Vector3(5,12,2),
		new Vector3(6,1,1),
		new Vector3(6,4,1),
		new Vector3(6,6,2),
		new Vector3(6,12,3)
	};
	
	void Start(){
		uiTexture.transform.localScale = new Vector3(Utils.getScreenLogicWidth(),Utils.getScreenLogicHeight(),1);
		SetFullSize(0.5f);
		//CalculateOffset(curPanel%4);
		//beginCutscenes();
		
		UIViewport uv = viewCamera.GetComponent<UIViewport>();
		uv.topLeft.localPosition = new Vector3((-.5f)*Utils.getScreenLogicWidth(),.5f*Utils.getScreenLogicHeight(),0);
		uv.bottomRight.localPosition = new Vector3(.5f*Utils.getScreenLogicWidth(),-.5f*Utils.getScreenLogicHeight(),0);	
	}
	
	void Update(){
		if(isEnd) return;
		
		if(Input.mousePosition.x < 0 ||
			(Input.mousePosition.x >= 60 && Input.mousePosition.x <= Screen.width - 60) ||
			Input.mousePosition.x > Screen.width){
			isBtnFlag = true;
		}else{
			isBtnFlag = false;
		}
			
		if(Input.GetMouseButtonDown(0) && !isMouseDown && isBtnFlag && !isSkipBtn){
			startPosX = Input.mousePosition.x;
			isMouseDown = true;
		}
		if(Input.GetMouseButtonUp(0) && isMouseDown && isBtnFlag && !isSkipBtn){
			endPosX = Input.mousePosition.x;
			isMouseDown = false;
			resultPosX = endPosX-startPosX;
		}
		if(resultPosX < -70f) OnNextBtnClick();
		else if(resultPosX > 70f) OnBackBtnClick();	
	}
	
	public void initCutscenes(Level lv){
		setCutscenes(lv,true);
	}
	
	public void GotoLevel(Level lv){
		setCutscenes(lv,false);
	}
	
	protected void setCutscenes(Level lv,bool isBefore){
		int chapterID = lv.chapter.id;
		int levelID = lv.id;
		
		foreach(Vector3 v3 in cutscenesDef){
			if(chapterID == v3.x && levelID == v3.y){
				if(isBefore){
					if(levelID == 1) 
						playCutscenes((int)v3.x,(int)v3.y,(int)v3.z,false);
					else 
						return;
				}else{
					if(levelID == 1) 
						return;
					else if(levelID == 12) 
						playCutscenes((int)v3.x,(int)v3.y,(int)v3.z,true);
					else 
						playCutscenes((int)v3.x,(int)v3.y,(int)v3.z,false);
				}
			}
		}
	}
	
	protected void playCutscenes(int chapterID,int levelID,int pageCount,bool isBoss){
		string s = isBoss? ("CutscenesSources/CBOSS" + chapterID + "_" + levelID)
								: ("CutscenesSources/C" + chapterID + "_" + levelID);
		if(pageCount > 1)	s += "_0";	
		uiTexture.mainTexture = Resources.Load(s) as Texture;
		if(uiTexture.mainTexture == null){
			endCutscenes();
			return;
		}
		this.chapterID = chapterID;
		this.levelID = levelID;
		this.pageCount = pageCount;
		curPanel = 0;
		beginCutscenes();
	}
	
	public void OnNextBtnClick(){
		ClearDragPos();
		GotoNextPanel();
	}
	
	public void OnBackBtnClick(){
		ClearDragPos();
		BackLastPanel();
	}
	
	public void OnSkipBtnClick(){
		MusicManager.playEffectMusic("SFX_UI_exit_tap_2a");
//		SkipDlg.gameObject.SetActive(true);
//		isSkipBtn = true;
		endCutscenes();
	}
	
	public void OnYesBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		SkipDlg.gameObject.SetActive(false);
		
		endCutscenes();
	}
	
	public void OnNoBtnClick(){
//		MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		SkipDlg.gameObject.SetActive(false);
		StartCoroutine(delaySkipDlg());
		
		SkipDlg.SetActive(false);
	}
	
	protected void GotoNextPanel(){
		curPanel++;
		int pageIndex = curPanel/4;
		if(pageIndex >= pageCount){
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			endCutscenes();	
		}else if(curPanel%4 == 0){
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			uiTexture.mainTexture = Resources.Load("CutscenesSources/C" + chapterID + "_" + levelID + "_" + pageIndex) as Texture;	
		}else{
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");		
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		}
		CalculateOffset(curPanel%4);
	}
	
	protected void BackLastPanel(){
		curPanel--;
		int pageIndex = curPanel/4;
		if(curPanel < 0){
			curPanel = 0;
			return;	
		}else{
			if((curPanel+1)%4 == 0){
				MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
				uiTexture.mainTexture = Resources.Load("CutscenesSources/C" + chapterID + "_" + levelID + "_" + pageIndex) as Texture;		
			}else{
//				MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");	
				MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			}
		}
		CalculateOffset(curPanel%4);
	}
	
	protected void CalculateOffset(int p){
		CalculateQuadrantPoint(p);
		Vector3 pScale = this.transform.parent.localScale;
		iTween.MoveTo(viewCamera.gameObject, new Hashtable(){{"position",viewCameraPos*pScale.x},{"time",.5f},{ "easetype","linear"}});
	}
	
	protected void CalculateQuadrantPoint(int p){
		Vector3 v3 = viewCameraInQuadrant[p];
		viewCameraPos = new Vector3(v3.x*Utils.getScreenLogicWidth(),v3.y*Utils.getScreenLogicHeight(),0f);
	}
	
	protected void SetFullSize(float size){
		viewCamera.GetComponent<UIViewport>().fullSize = size;
	}
	
	protected IEnumerator delaySkipDlg(){
		yield return new WaitForSeconds(0.03f);
		isSkipBtn = false;
	}
	
	protected void ClearDragPos(){
		startPosX = 0f;
		endPosX = 0f;
		resultPosX = 0f;	
	}
	
	protected void beginCutscenes(){
		isEnd = false;
		viewCamera.gameObject.SetActive(true);
		uiTexture.gameObject.SetActive(true);
		uiBtn.gameObject.SetActive(true);
		CalculateOffset(curPanel%4);
		Debug.Log("begin cutscenes");
//		MusicManager.playBgMusic("MUS_UI_Menus");
	}
	
	protected void endCutscenes(){
		isEnd = true;
		viewCamera.gameObject.SetActive(false);
		uiTexture.gameObject.SetActive(false);
		uiTexture.mainTexture = null;
		uiBtn.gameObject.SetActive(false);
		SkipDlg.SetActive(false);
		Debug.Log("end cutscenes");
		LevelMgr.Instance.PlayBgMusic();
	}
	
}
