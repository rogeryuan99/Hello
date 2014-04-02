using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenAnimIPad : PieceAnimation {
	public GameObject finger;
	public UISprite marvel;
	public UITexture title;
	public GameObject SkipDlg;
	public UIViewport viewCamera;
	
//	public Animation Button_Back;
//	public Animation Button_Next;
//	
//	public UISprite Button_BackTop;
//	public UISprite Button_BackBottom;
//	public UISprite Button_NextTop;
//	public UISprite Button_NextBottom;
	
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
	
	public bool isZoomIn = false;
	public int curFrame = 0;
	public bool isInOperation = false;
	
	private GameObject starlordGo;
	private float startPosX = 0f;
	private float endPosX = 0f;
	private float resultPosX = 0f;
	private bool isMouseDown = false;
	private bool isZero = false;
	private float cumTime = 0f;
	private bool isBtnEffectFlag = false;
	private bool isBtnFlag = false;
	private float btnEffectTime = 0f;
	private bool isPlayEffect = false;
	private float flashSpeed = 3.0f;
	private bool isSkipBtn = false;
	private float btnShowTime = 5.0f;
	private Vector3 L0StartPos = new Vector3(-2f,0f,-.1f);
	private Vector3 L0EndPos = new Vector3(0f,0f,-.1f);
	private bool isPageFrame = false;
	private bool isOnceMusic = false;
	
	public List<Vector2> PageFrameDef = new List<Vector2>(){
		new Vector2(99,110),
		new Vector2(214,218),
		new Vector2(299,309),
		new Vector2(376,386)
	};
	private List<Vector2> PauseAnimExtentDef = new List<Vector2>(){
		new Vector2(0,6),
		new Vector2(14,21),
		new Vector2(31,34),
		new Vector2(45,48),
//		new Vector2(59,62),
//		new Vector2(73,75),
//		new Vector2(86,88),
		new Vector2(99,110),
		//new Vector2(120,123),
		new Vector2(135,137),
		new Vector2(149,153),
		new Vector2(163,168),
		new Vector2(177,181),
		new Vector2(192,194),
		new Vector2(204,206),
		new Vector2(214,218),
		//new Vector2(229,238),
		new Vector2(248,252),
		new Vector2(261,266),
		new Vector2(275,279),
		new Vector2(287,290),
		new Vector2(299,309),
		//new Vector2(319,324),
		new Vector2(334,339),
		new Vector2(348,353),
		new Vector2(362,366),
		new Vector2(376,386)
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
	
	public void Start(){
		base.Start();	
		
		UIViewport uv = viewCamera.GetComponent<UIViewport>();
		uv.topLeft.localPosition = new Vector3((-.5f)*Utils.getScreenLogicWidth(),.5f*Utils.getScreenLogicHeight(),0);
		uv.bottomRight.localPosition = new Vector3(.5f*Utils.getScreenLogicWidth(),(-.5f)*Utils.getScreenLogicHeight(),0);
		uv.enabled = true;
		SkipDlg.gameObject.SetActive(false);
	}
	
	public void Update(){
		base.Update();	
		PausePanel();
		
		if(Input.mousePosition.x < 0 ||
			(Input.mousePosition.x >= 120 && Input.mousePosition.x <= Screen.width - 120) ||
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
		if(resultPosX < -70f) GotoNextPanel();
		else if(resultPosX > 70f) BackLastPanel();

		animaPlayEndScript(effectEnd);
	}
	
	public void OnBackBtnClick(){
		BackLastPanel();
	}
	
	public void OnNextBtnClick(){
		GotoNextPanel();	
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
	
	private void PausePanel(){
		for(int n = 0;n < PauseAnimExtentDef.Count;n++){
			if(getCurrentFrame() >= PauseAnimExtentDef[n].x && getCurrentFrame() <= PauseAnimExtentDef[n].y){
				pauseAnima();
				jumpToFrame((int)PauseAnimExtentDef[n].y+1);
				Debug.Log("jump to : " + getCurrentFrame());
			}
		}	
	}
	
	private OpenAnimPanel getCurrentOpenAnimPanel(){
		OpenAnimPanel[]oaps= GameObject.FindObjectsOfType(typeof(OpenAnimPanel)) as OpenAnimPanel[];
		foreach(OpenAnimPanel oap in oaps){
			if(oap.isZoomIn) return oap;
		}
		return null;
	}
	
	private void GotoNextPanel(){
		ClearDragPos();
		if(isInOperation) return;
		if(isZoomIn){
			if(getCurrentFrame() < 8){
				MusicManager.playEffectMusic("SFX_UI_button_tap_2a");	
				iTween.MoveTo(L0.gameObject, new Hashtable(){{"position",L0StartPos},{"time",.5f},{ "easetype","linear"}});	
				iTween.ValueTo(gameObject,new Hashtable(){{"from",marvel.alpha},{"to",0f},{"time",.5f},{ "onupdate","SetColorAlpha"},{"oncomplete","restartAnimNextPanelZoomIn"},{ "easetype","linear"}});
			}else{
				OpenAnimPanel oap = getCurrentOpenAnimPanel();
				if(oap != null){ 
					oap.GotoNextPanel();
				}else{
					Debug.LogError("GotoNextPanel error");
				}
			}
		}else{
			if(getCurrentFrame() < 8){
				MusicManager.playEffectMusic("SFX_UI_button_tap_2a");	
				iTween.MoveTo(L0.gameObject, new Hashtable(){{"position",L0StartPos},{"time",.5f},{ "easetype","linear"}});	
				iTween.ValueTo(gameObject,new Hashtable(){{"from",marvel.alpha},{"to",0f},{"time",.5f},{ "onupdate","SetColorAlpha"},{"oncomplete","restartAnimNextPanel"},{ "easetype","linear"}});
			}else{
				for(int n = 0;n < PageFrameDef.Count;n++){
					isPageFrame = false;
					if(getCurrentFrame() >= PageFrameDef[n].x && getCurrentFrame() <= PageFrameDef[n].y+1){
						isPageFrame = true;
						break;
					}
				}
				if(isPageFrame){
					MusicManager.playEffectMusic("SFX_UI_button_tap_2a");		
				}else{
//					MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");	
					MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
				}
				restart();
			}
			if(getCurrentFrame() >= 386) effectEnd(null);
		}
		//ClearDragPos();
	}
	
	private void BackLastPanel(){
		ClearDragPos();
		if(isInOperation) return;
		if(isZoomIn){
			OpenAnimPanel oap = getCurrentOpenAnimPanel();
			if(oap != null){ 
				oap.BackLastPanel();
			}else{
				Debug.LogError("BackLastPanel error");
			}		
		}else{
			for(int n = 0;n < PauseAnimExtentDef.Count;n++){
				if(n > 0 && getCurrentFrame() > PauseAnimExtentDef[n-1].y && getCurrentFrame() <= PauseAnimExtentDef[n].y+1){
					jumpToFrame((int)PauseAnimExtentDef[n-1].x);	
					Debug.Log("jump to : " + getCurrentFrame());
					for(int i = 0;i < PageFrameDef.Count;i++){
						isPageFrame = false;
						if(getCurrentFrame() >= PageFrameDef[i].x && getCurrentFrame() <= PageFrameDef[i].y+1){
							isPageFrame = true;
							break;
						}
					}
					if(isPageFrame){
						MusicManager.playEffectMusic("SFX_UI_button_tap_2a");		
					}else{
//						MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");	
						MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
					}
					restart();
					break;
				}else if(getCurrentFrame() >= 0 && getCurrentFrame() <= PauseAnimExtentDef[1].y+1){
					jumpToFrame((int)PauseAnimExtentDef[0].x);
					restart();
					if(!isOnceMusic) MusicManager.playEffectMusic("SFX_UI_button_tap_2a");		
					iTween.MoveTo(L0.gameObject, new Hashtable(){{"position",L0EndPos},{"time",.5f},{ "easetype","linear"}});
					iTween.ValueTo(gameObject,new Hashtable(){{"from",marvel.alpha},{"to",1f},{"time",.5f},{ "onupdate","SetColorAlpha"},{"oncomplete","restartAnimLastPanel"},{ "easetype","linear"}});
					break;
				}
			}
		}
		//ClearDragPos();
	}
	
	public void effectEnd(string currentAnima){
		Debug.Log("Finished");
		//gotoTutorial();
		GotoProxy.gotoScene (GotoProxy.UIMAIN);
	}
	
	private void ClearDragPos(){
		startPosX = 0f;
		endPosX = 0f;
		resultPosX = 0f;	
	}
	
	private void createStarLord(string effectName){
		TsHeroFactory f = new TsHeroFactory();
		starlordGo = f.Create("STARLORD");
		Debug.Log(starlordGo);
		StarLord star = starlordGo.GetComponent<StarLord>();
		if(effectName == "Stand") starlordGo.transform.localPosition = new Vector3(-120,-20,0);
		else starlordGo.transform.localPosition = new Vector3(100,-20,0);
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
//		
//		GotoProxy.gotoScene(GotoProxy.BATTLESCENE);
//		GameObject go = new GameObject("Tutorial");
//		TsTheater tt = go.AddComponent<TsTheater>();
//		tt.PreparePart("Start");
//		DontDestroyOnLoad(go);		
		GotoProxy.gotoScene(GotoProxy.UIMAIN);
	}
	
	private IEnumerator delayTime(){
		yield return new WaitForSeconds(.01f);	
		OpenAnimPanel oap0 = GameObject.Find("L8").transform.GetChild(0).GetComponent<OpenAnimPanel>();
		if(oap0 != null){
			oap0.isZoomIn = true;
			StartCoroutine(oap0.CalculateOffset());
		}
	}
	
	private void SetColorAlpha(float a){
		isInOperation = true;
		marvel.alpha = a;
		title.alpha = a;
	}
	
	private void restartAnimNextPanel(){
		isInOperation = false;
		isOnceMusic = false;
		jumpToFrame(8);
		restart();
	}
	
	private void restartAnimNextPanelZoomIn(){
		isInOperation = false;
		jumpToFrame(108);
		restart();
		StartCoroutine(delayTime());
	}
	
	private void restartAnimLastPanel(){
		isOnceMusic = true;
		isInOperation = false;	
	}
	
	private IEnumerator delaySkipDlg(){
		yield return new WaitForSeconds(0.03f);
		isSkipBtn = false;
	}
	
	void OnDestroy(){
		HeroMgr.clear();
	}
}
