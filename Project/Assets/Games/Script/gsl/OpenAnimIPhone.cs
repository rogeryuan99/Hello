using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenAnimIPhone : PieceAnimation {
	public UISprite marvel;
	public UITexture title;
	public GameObject SkipDlg;
	public Camera viewCamera;
	
	public GameObject L0;
	public GameObject L1;
	public GameObject L2;
	public GameObject L3;
	public GameObject L4;
	public GameObject L5;
	public GameObject L6;
	//public GameObject L7;
	public GameObject L8;
	public GameObject L9;
	public GameObject L10;
	public GameObject L11;
	public GameObject L12;
	public GameObject L13;
	public GameObject L14;
	public GameObject L14_offset;
	public GameObject L15;
	public GameObject L16;
	public GameObject L17;
	public GameObject L18;
	public GameObject L19;
	public GameObject L20;
	public GameObject L21;
	public GameObject L22;
	public GameObject L23;
	public GameObject L24;
	public GameObject L25;
	public GameObject L26;
	public GameObject L27;
	public GameObject L28;
	public GameObject L29;
	public GameObject L30;
	public GameObject L31;
	public GameObject L32;
	public GameObject L33;
	public GameObject L34;
	public GameObject L35;
	public GameObject L36;
	public GameObject L37;
	public GameObject L38;
	public GameObject L39;
	public GameObject L40;
	public GameObject L42;
	public GameObject L43;
	public GameObject L44;
	
	private GameObject starlordGo;
	private Vector3 L0StartPos = new Vector3(-2f,0f,-.1f);
	private Vector3 L0EndPos = new Vector3(0f,0f,-.1f);
	private int curPoint = -1;
	private Vector3 viewCameraPos = Vector3.zero;
	private float viewCameraSize = 0f;
	private float startPosX = 0f;
	private float endPosX = 0f;
	private float resultPosX = 0f;
	private bool isMouseDown = false;
	private bool isBtnFlag = false;
	private bool isSkipBtn = false;
	private bool inAnim = false;
	private bool isPageFrame = false;
	const float ArtW = 2030f;
	const float ArtH = 1150f;
	
	private List<Vector3> viewCameraInQuadrant = new List<Vector3>(){
		new Vector3((-.25f)*ArtW*0.4f,(.25f)*ArtH*0.4f,0f),
		new Vector3((.25f)*ArtW*0.4f,(.25f)*ArtH*0.4f,0f),
		new Vector3((-.25f)*ArtW*0.4f,(-.25f)*ArtH*0.4f,0f),
		new Vector3((.25f)*ArtW*0.4f,(-.25f)*ArtH*0.4f,0f)
	};
	
	private List<Vector2> PageFrameDef = new List<Vector2>(){
		new Vector2(0,6),
		new Vector2(99,110),
		new Vector2(214,218),
		new Vector2(299,309),
		new Vector2(375,385)
	};
	
	protected override void initPartData(){
		partList = new Hashtable ();
		partList ["L1"] = L1;
		partList ["L2"] = L2;
		partList ["L3"] = L3;
		partList ["L4"] = L4;
		partList ["L5"] = L5;
		partList ["L6"] = L6;	
		//partList ["L7"] = L7;
		partList ["L8"] = L8;
		partList ["L9"] = L9;
		partList ["L10"] = L10;
		partList ["L11"] = L11;
		partList ["L12"] = L12;	
		partList ["L13"] = L13;
		partList ["L14"] = L14;
		partList ["L14_offset"] = L14_offset;
		partList ["L15"] = L15;
		partList ["L16"] = L16;
		partList ["L17"] = L17;	
		partList ["L18"] = L18;
		partList ["L19"] = L19;
		partList ["L20"] = L20;
		partList ["L21"] = L21;
		partList ["L22"] = L22;
		partList ["L23"] = L23;	
		partList ["L24"] = L24;
		partList ["L25"] = L25;
		partList ["L26"] = L26;
		partList ["L27"] = L27;
		partList ["L28"] = L28;
		partList ["L29"] = L29;	
		partList ["L30"] = L30;
		partList ["L31"] = L31;
		partList ["L32"] = L32;
		partList ["L33"] = L33;
		partList ["L34"] = L34;
		partList ["L35"] = L35;	
		partList ["L36"] = L36;
		partList ["L37"] = L37;
		partList ["L38"] = L38;
		partList ["L39"] = L39;
		partList ["L40"] = L40;
		partList ["L42"] = L42;
		partList ["L43"] = L43;
		partList ["L44"] = L44;
	}
	
	private void moveViewPort(){
		UIViewport uv = viewCamera.GetComponent<UIViewport>();
//		uv.topLeft.localPosition = new Vector3((-.5f)*Utils.getScreenLogicWidth(),.5f*2030f*0.4f*Utils.getScreenLogicHeight()/Utils.getScreenLogicWidth(),0);
//		uv.bottomRight.localPosition = new Vector3(.5f*Utils.getScreenLogicWidth(),(-.5f)*2030f*0.4f*Utils.getScreenLogicHeight()/Utils.getScreenLogicWidth(),0);
		float w =  .5f*Utils.getScreenLogicWidth()+4;
		float h1 = .5f*Utils.getScreenLogicHeight()+4;
		//float h2 = .5f*2030f*0.4f*Utils.getScreenLogicHeight()/Utils.getScreenLogicWidth() + 2;
		float h2= ArtH/ArtW*w+1;
		float h = Mathf.Min(h1,h2);
		Debug.Log("h1="+h1+" h2="+h2+" h="+h);
		uv.topLeft.localPosition = new Vector3(-w,h,0);
		uv.bottomRight.localPosition = new Vector3(w,-h,0);
	}
	
	public void Start () {
		base.Start();	
		
		UIViewport uv = viewCamera.GetComponent<UIViewport>();
		uv.topLeft.localPosition = new Vector3((-.5f)*Utils.getScreenLogicWidth(),.5f*Utils.getScreenLogicHeight(),0);
		uv.bottomRight.localPosition = new Vector3(.5f*Utils.getScreenLogicWidth(),-.5f*Utils.getScreenLogicHeight(),0);

		Debug.Log("width : " + Utils.getScreenLogicWidth() + "   height : " + Utils.getScreenLogicHeight());
		OpenAnimPanel[] oaps = GameObject.FindObjectsOfType(typeof(OpenAnimPanel)) as OpenAnimPanel[];
		foreach(OpenAnimPanel oap in oaps){
			if(oap != null) Destroy(oap);	
		}
	}
	
	public void Update () {
		base.Update();
		PausePoint();

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
		
		animaPlayEndScript(effectEnd);
	}
	
	public void OnBackBtnClick(){
		//MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
//		for(int n = 0;n < PageFrameDef.Count;n++){
//			isPageFrame = false;
//			if(getCurrentFrame() >= PageFrameDef[n].x && getCurrentFrame() <= PageFrameDef[n].y){
//				isPageFrame = true;
//			}
//		}
//		if(isPageFrame){
//			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");		
//		}else{
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");	
//		}
		ClearDragPos();
		if(inAnim) return;
		if(curPoint < 0) return;
		else if(curPoint == 0){
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");	
			iTween.MoveTo(L0.gameObject, new Hashtable(){{"position",L0EndPos},{"time",.5f},{ "easetype","linear"}});
			iTween.ValueTo(gameObject,new Hashtable(){{"from",marvel.alpha},{"to",1f},{"time",.5f},{ "onupdate","SetColorAlpha"},{"oncomplete","restartAnimLastPoint"},{ "easetype","linear"}});
			jumpToFrame(0);
			restart();
			iTween.MoveTo(viewCamera.gameObject, new Hashtable(){{"position",Vector3.zero},{"time",.5f},{ "easetype","linear"}});
			iTween.ValueTo(gameObject,new Hashtable(){{"from",viewCamera.GetComponent<UIViewport>().fullSize},{"to",1f},{"time",.5f},{ "onupdate","SetFullSize"},{"oncomplete","endAnim"},{ "easetype","linear"}});
			UIViewport uv = viewCamera.GetComponent<UIViewport>();
			uv.topLeft.localPosition = new Vector3((-.5f)*Utils.getScreenLogicWidth(),.5f*Utils.getScreenLogicHeight(),0);
			uv.bottomRight.localPosition = new Vector3(.5f*Utils.getScreenLogicWidth(),(-.5f)*Utils.getScreenLogicHeight(),0);
			curPoint = -1;
			return;	
		}else 
			BackLastPoint();
	}
	
	public void OnNextBtnClick(){
		//MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
//		for(int n = 0;n < PageFrameDef.Count;n++){
//			isPageFrame = false;
//			if(getCurrentFrame() >= PageFrameDef[n].x && getCurrentFrame() <= PageFrameDef[n].y){
//				isPageFrame = true;
//			}
//		}
//		if(isPageFrame){
//			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");		
//		}else{
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");	
//		}
		ClearDragPos();
		if(inAnim) return;
		if(curPoint < 0){
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			iTween.MoveTo(L0.gameObject, new Hashtable(){{"position",L0StartPos},{"time",.5f},{ "easetype","linear"}});	
			iTween.ValueTo(gameObject, new Hashtable(){{"from",marvel.alpha},{"to",0f},{"time",.5f},{ "onupdate","SetColorAlpha"},{"oncomplete","restartAnimNextPoint"},{ "easetype","linear"}});	
		}else{
			GotoNextPoint();	
		}
	}
	
	public void OnSkipBtnClick(){
		MusicManager.playEffectMusic("SFX_UI_exit_tap_2a");
		SkipDlg.gameObject.SetActive(true);
		isSkipBtn = true;
	}
	
	public void OnYesBtnClick(){
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		Debug.Log("skip~");
		effectEnd(null);	
		SkipDlg.gameObject.SetActive(false);
	}
	
	public void OnNoBtnClick(){
		MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		SkipDlg.gameObject.SetActive(false);
		StartCoroutine(delaySkipDlg());
	}
	
	private void PausePoint(){
		for(int n = 0;n < PageFrameDef.Count;n++){
			if(getCurrentFrame() >= PageFrameDef[n].x &&getCurrentFrame() <= PageFrameDef[n].y){
				pauseAnima();
				jumpToFrame((int)PageFrameDef[n].y);
			}
		}	
	}
	
	private void GotoNextPoint(){
		if(getCurrentFrame() == PageFrameDef[PageFrameDef.Count-1].y && curPoint == 15){
			effectEnd(null);	
		}
		curPoint++;
		CalculateOffset(curPoint%4);
		Debug.Log("curPoint : " + curPoint + "   curFrame : " + getCurrentFrame());
		if(curPoint%4 == 0){
			if(curPoint != 0) MusicManager.playEffectMusic("SFX_UI_button_tap_2a");	
			for(int n = 0;n < PageFrameDef.Count;n++){
				if(n < PageFrameDef.Count-1 && getCurrentFrame() <= PageFrameDef[n].y){
					jumpToFrame((int)(PageFrameDef[n+1].x));	
					restart();
					break;
				}
			}	
		}else if(curPoint > 0){
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");	
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		}
	}
	
	private void BackLastPoint(){
		if(curPoint%4 == 0){
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");	
			for(int n = 0;n < PageFrameDef.Count;n++){
				if(n > 0 && getCurrentFrame() <= PageFrameDef[n].y){
					jumpToFrame((int)(PageFrameDef[n-1].x));	
					restart();
					break;
				}
			}
		}else if(curPoint > 0){
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");	
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
		}
		curPoint--;
		CalculateOffset(curPoint%4);
		Debug.Log("curPoint : " + curPoint + "   curFrame : " + getCurrentFrame());
	}
	
	private void SetColorAlpha(float a){
		inAnim = true;
		marvel.alpha = a;
		title.alpha = a;
	}
	
	private void restartAnimNextPoint(){
		inAnim = false;
		GotoNextPoint();
	}
	
	private void restartAnimLastPoint(){
		inAnim = false;
	}

	private void CalculateOffset(int p){
		getCurViewCameraPosAndSize(p);
		Vector3 pScale = this.transform.parent.localScale;
		iTween.MoveTo(viewCamera.gameObject, new Hashtable(){{"position",viewCameraPos*pScale.x},{"time",.5f},{ "easetype","linear"}});
		iTween.ValueTo(gameObject,new Hashtable(){{"from",viewCamera.GetComponent<UIViewport>().fullSize},{"to",viewCameraSize},{"time",.5f},{ "onupdate","SetFullSize"},{"oncomplete","endAnim"},{ "easetype","linear"}});
		this.moveViewPort();
	}
	
	private void SetFullSize(float size){
		inAnim = true;
		viewCamera.GetComponent<UIViewport>().fullSize = size;
	}
	
	private void endAnim(){
		inAnim = false;	
	}
	
	private void ClearDragPos(){
		startPosX = 0f;
		endPosX = 0f;
		resultPosX = 0f;	
	}
	
	public void effectEnd(string currentAnima){
		Debug.Log("Finished");
		//gotoTutorial();
		GotoProxy.gotoScene (GotoProxy.UIMAIN);
	}
	
	private void createStarLord(string effectName){
		TsHeroFactory f = new TsHeroFactory();
		starlordGo = f.Create("STARLORD");
		Debug.Log(starlordGo);
		StarLord star = starlordGo.GetComponent<StarLord>();
		if(effectName == "Stand") starlordGo.transform.localPosition = new Vector3(-120,-20,0);
		else starlordGo.transform.localPosition = new Vector3(100,-20,0);
		//star.playAnim("Celebrate");
		star.playAnim(effectName);
	}
	
	private void gotoTutorial(){
		TsTheater.InTutorial = true;
		StaticData.isBattleEnd = false;
		for(int i=0; i<UserInfo.heroDataList.Count; i++)
		{
			HeroData heroData = UserInfo.heroDataList[i] as HeroData;
			//heroData.isSelect = false; heroData.state ???
		}
		
//		NewLevelMgr.Instance.selectChapterAndLevel(999,1);
//		GotoProxy.gotoScene(GotoProxy.BATTLESCENE);
//		GameObject go = new GameObject("Tutorial");
//		TsTheater tt = go.AddComponent<TsTheater>();
//		tt.PreparePart("Start");
//		DontDestroyOnLoad(go);		
		GotoProxy.gotoScene(GotoProxy.UIMAIN);		
	}
	
	private void getCurViewCameraPosAndSize(int index){
		viewCameraPos = viewCameraInQuadrant[index];
		viewCameraSize = 2030f*0.4f*0.5f/Utils.getScreenLogicWidth();
	}
	
	private IEnumerator delaySkipDlg(){
		yield return new WaitForSeconds(0.03f);
		isSkipBtn = false;
	}
	
	void OnDestroy(){
		HeroMgr.clear();
	}
}
