using UnityEngine;
using System.Collections;

public class PreBattleDlg : MonoBehaviour {
	private Vector3 originalPos;
	public GameObject targetObject;
	public UISprite light_right;
	public UISprite lightFly;
	private float cumulate = -2;
	private bool fly = false;
	public GameObject[] showInReady;
	public GameObject[] showInNotReady;
	public UILabel labelReady;
	public UILabel labelEmptyTeam;
	
	
	public DragBtn dragBtn;
	public UILabel labelChapterName;
	public UILabel labelLevel;
	
	public void OnEnable(){
		LevelMgr.Instance.pauseButton.SetActive(false);	
	}
	
	void OnBackBtnClick(){
		//DlgManager.instance.ShowChapterListDlg();
		MusicManager.playEffectMusic("SFX_UI_exit_tap_2a");
		HomePageDlg.reservedDlg = "LevelSelect";
		GotoProxy.gotoScene (GotoProxy.UIMAIN);	
	}
	
	
	void Awake(){
		light_right.alpha = 0;
		originalPos = dragBtn.transform.position;
		setReady(true);
		showEmptyTeam(false);
	}
	public void showEmptyTeam(bool b){
		labelEmptyTeam.gameObject.SetActive(b);
	}
	public void setReady(string[] parms){
		setReady(parms[0].ToLower().Equals("true"));
	}
	public void setReady(bool b){
		labelChapterName.text = Localization.instance.Get("UI_ChapterName_" + MapMgr.Instance.getCurrentChapter().id) + " " + string.Format(Localization.instance.Get("UI_BattleBeginDlg_Level"),MapMgr.Instance.getCurrentLevel().id);
		if( b ){
			foreach(GameObject go in showInReady){
				go.SetActive(true);
			}
			foreach(GameObject go in showInNotReady){
				go.SetActive(false);
			}
			this.dragBtn.transform.position = originalPos;			
			StartFly();	
		}else{
			foreach(GameObject go in showInReady){
				go.SetActive(false);
			}
			foreach(GameObject go in showInNotReady){
				go.SetActive(true);
			}
			
			StopFly();	
		}
	}
		
	private void StopFly(){
		fly = false;
		lightFly.alpha = 0;
		
		labelReady.gameObject.SetActive(false);
		//MusicManager.Instance.stopEffectMusicForLoop("SFX_UI_swipe_loop_1a");
	}
	private void StartFly(){
		fly = true;
		cumulate = -0.1f;
		labelReady.gameObject.SetActive(true);
		light_right.alpha = 0;
		if(GameObject.Find("TeamDlg(Clone)") == null && LevelMgr.Instance.cutscenes.isEnd){
//			MusicManager.playEffectMusic("SFX_UI_swipe_loop_1a");
		}
//		MusicManager.playEffectMusic("SFX_UI_swipe_loop_1a");
	}
	void Update(){
		if(!fly) return;
		cumulate += Time.deltaTime;
		if(cumulate >3){
			cumulate = 0;
			if(GameObject.Find("TeamDlg(Clone)") == null && LevelMgr.Instance.cutscenes.isEnd){
//				MusicManager.playEffectMusic("SFX_UI_swipe_loop_1a");
			}
			//MusicManager.playEffectMusic("SFX_UI_swipe_loop_1a");	
		}
		if(0<= cumulate && cumulate<=1){
			Vector3 p = Vector3.Lerp(originalPos,targetObject.transform.position,cumulate);
			lightFly.transform.position = new Vector3(p.x,lightFly.transform.position.y,lightFly.transform.position.z);
			float a =  Mathf.Sin(cumulate*Mathf.PI) - 0.1f;
			lightFly.alpha = a;//Mathf.Lerp(1,0,cumulate);
		}
	}
	
	public void OnSliderDrag (Vector2 delta)
	{
		Vector3 vc3 = Input.mousePosition;
		Camera ca = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
		Vector3 worldVc3 = ca.ScreenToWorldPoint(vc3);
		worldVc3.y = originalPos.y;
		worldVc3.x = Mathf.Max(originalPos.x, Mathf.Min(targetObject.transform.position.x,worldVc3.x));
		worldVc3.z = originalPos.z;
		this.dragBtn.transform.position = worldVc3;
		//this.dragBtn.transform.localPosition += new Vector3(delta.x,0,0);
		light_right.alpha = lightAlpha();
		//light_right.SetActive(lightAlpha());	
	}
	private bool isReach(){
		Vector3 dist = this.dragBtn.transform.localPosition - targetObject.transform.localPosition;
		return Mathf.Abs(dist.x)<200;	
	}
	private float lightAlpha(){
		Vector3 dist = this.dragBtn.transform.localPosition - targetObject.transform.localPosition;
		if( Mathf.Abs(dist.x)> 400) return 0;
		float f =  1 - (Mathf.Abs(dist.x)-100)/300f;
		return f;
	}
	public void SliderDrop ()
	{
		if(isReach()){
			this.dragBtn.transform.localPosition = targetObject.transform.localPosition;
			StopFly();
			Debug.Log("Begin!!!!");	
			LevelMgr.Instance.OnBattleBtnClick();
		}else{
			//back
			this.dragBtn.transform.position = originalPos;
			light_right.alpha = 0;
			StartFly();
		}
	}
	
	
	public void OnSliderPress (bool isPressed)
	{
		StopFly();
		Debug.Log("onPress "+isPressed);
//		if (enabled)
//		{
//			Collider col = collider;
//			if (col != null) col.enabled = !isPressed;
			if (!isPressed) this.SliderDrop();
//		}
	}		
}
