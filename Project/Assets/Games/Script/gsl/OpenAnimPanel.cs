using UnityEngine;
using System.Collections;

public class OpenAnimPanel : MonoBehaviour {
	public OpenAnimIPad openAnim;
	public GameObject topLeft;
	public GameObject bottomRight;
	public GameObject lastPanel;
	public GameObject nextPanel;
	public bool isZoomIn = false;
	public int startFrame;
	public int endFrame;
	
	private Vector3 ppScale;
	private float per;
	private float defaultViewCamerafullSize=1;
	private float t = 0.5f;

	void Start () {
		openAnim = GameObject.Find("OpenAnimIPad").GetComponent<OpenAnimIPad>();
		topLeft = GameObject.Find("TopLeft") as GameObject;
		bottomRight = GameObject.Find("BottomRight") as GameObject;
	}

	void Update () {

	}
	
	public void GotoNextPanel(){
		if(openAnim.isInOperation) return;
		switch(this.gameObject.name){
		case "L1(Clone)":
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");		
			openAnim.jumpToFrame(218);
			openAnim.restart();
			break;
		case "L9(Clone)":
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");	
			openAnim.jumpToFrame(309);
			openAnim.restart();
			break;
		case "L26(Clone)":
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");	
			openAnim.jumpToFrame(386);
			openAnim.restart();
			break;
		case "L42(Clone)":
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");	
			openAnim.effectEnd(null);
			break;
		default:
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			break;
		}
		
		if(nextPanel != null && isZoomIn){
			GameObject nextObj = GameObject.Find(nextPanel.name) as GameObject;
			OpenAnimPanel np = nextObj.transform.GetChild(0).GetComponent<OpenAnimPanel>();
			if(np != null){
				this.isZoomIn = false;
				np.isZoomIn = true;
				StartCoroutine(np.CalculateOffset());
			}
		}
	}
	
	public void BackLastPanel(){
		if(openAnim.isInOperation) return;
		switch(this.gameObject.name){
		case "L8(Clone)":
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");	
			openAnim.jumpToFrame(0);
			openAnim.restart();
			iTween.MoveTo(openAnim.L0.gameObject, new Hashtable(){{"position",new Vector3(0f,0f,-.1f)},{"time",t},{ "easetype","linear"}});
			iTween.ValueTo(gameObject,new Hashtable(){{"from",openAnim.marvel.alpha},{"to",1f},{"time",t},{ "onupdate","SetColorAlpha"},{ "easetype","linear"}});
			Vector3 tl = new Vector3((-.5f)*Utils.getScreenLogicWidth(),(.5f)*Utils.getScreenLogicHeight(),0);
			Vector3 br = new Vector3((.5f)*Utils.getScreenLogicWidth(),(-.5f)*Utils.getScreenLogicHeight(),0);
			CreateTweenEffect(tl,br,Vector3.zero,openAnim.viewCamera.fullSize,defaultViewCamerafullSize);
			break;
		case "L18(Clone)":
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");	
			openAnim.jumpToFrame(99);
			openAnim.restart();
			break;
		case "L30(Clone)":
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");	
			openAnim.jumpToFrame(214);
			openAnim.restart();
			break;
		case "L40(Clone)":
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");	
			openAnim.jumpToFrame(299);
			openAnim.restart();
			break;
		default:
//			MusicManager.playEffectMusic("SFX_UI_button_tap_simple_1b");
			MusicManager.playEffectMusic("SFX_UI_button_tap_2a");
			break;
		}
		
		if(lastPanel != null && isZoomIn){
			GameObject lastObj = GameObject.Find(lastPanel.name) as GameObject;
			OpenAnimPanel lp = lastObj.transform.GetChild(0).GetComponent<OpenAnimPanel>();
			if(lp != null){
				this.isZoomIn = false;
				lp.isZoomIn = true;
				StartCoroutine(lp.CalculateOffset());
			}
		}
	}
	
	public void OnDoubleClick(){
		OpenAnimPanel[]oaps= GameObject.FindObjectsOfType(typeof(OpenAnimPanel)) as OpenAnimPanel[];
		foreach(OpenAnimPanel oap in oaps){
			if(oap != this) oap.isZoomIn = false;
		}
		if(openAnim.isInOperation) return;
		isZoomIn = !isZoomIn; 
		openAnim.isZoomIn = isZoomIn;
		if(isZoomIn){
			for(int n = 0;n < openAnim.PageFrameDef.Count;n++){
				if(openAnim.getCurrentFrame() <= openAnim.PageFrameDef[n].y+1){
					openAnim.curFrame = openAnim.getCurrentFrame();
					openAnim.jumpToFrame((int)openAnim.PageFrameDef[n].x-1);
					openAnim.restart();
					break;
				}
			}
		}else{
			//openAnim.jumpToFrame(openAnim.curFrame-2);
			openAnim.jumpToFrame(endFrame-1);
			openAnim.restart();
		}
		StartCoroutine(CalculateOffset());
	}
	
	public IEnumerator CalculateOffset(){
		yield return new WaitForSeconds(0.01f);
		
		Vector3 tl;
		Vector3 br;
		ppScale = openAnim.transform.localScale;
		if((float)this.transform.localScale.x/(float)this.transform.localScale.y >= (float)Screen.width/(float)Screen.height){
			per = ppScale.x*this.transform.localScale.x/  Utils.getScreenLogicWidth();	
		}else{
			per = ppScale.y*this.transform.localScale.y/   Utils.getScreenLogicHeight();
		}
		if(isZoomIn){
			tl = new Vector3((-.5f)*ppScale.x*this.transform.localScale.x/per,(.5f)*ppScale.y*this.transform.localScale.y/per,this.transform.localScale.z);
			br = new Vector3((.5f)*ppScale.x*this.transform.localScale.x/per,(-.5f)*ppScale.y*this.transform.localScale.y/per,this.transform.localScale.z);	
			CreateTweenEffect(tl,br,this.transform.parent.localPosition,openAnim.viewCamera.fullSize,per);
		}else{
			tl = new Vector3((-.5f)*Utils.getScreenLogicWidth(),(.5f)*Utils.getScreenLogicHeight(),0);
			br = new Vector3((.5f)*Utils.getScreenLogicWidth(),(-.5f)*Utils.getScreenLogicHeight(),0);
			CreateTweenEffect(tl,br,Vector3.zero,per,defaultViewCamerafullSize);
		}	
	}
	
	private void CreateTweenEffect(Vector3 tl,Vector3 br,Vector3 pos,float fromValue,float toValue){
		UIRoot ur = GameObject.FindObjectOfType(typeof(UIRoot)) as UIRoot;
		float rootScaleValue = ur.transform.localScale.x;
		float ppScaleValue = ppScale.x;
		iTween.MoveTo(topLeft.gameObject, new Hashtable(){{"position", tl*rootScaleValue},{"time", t},{ "easetype","linear"}});
		iTween.MoveTo(bottomRight.gameObject,new Hashtable(){{"position", br*rootScaleValue},{"time", t},{ "easetype","linear"}});
		iTween.MoveTo(openAnim.viewCamera.gameObject,new Hashtable(){{"position", pos*rootScaleValue*ppScaleValue},{"time", t},{ "easetype","linear"}});
		iTween.ValueTo(gameObject,new Hashtable(){{"from", fromValue},{"to", toValue},{"time", t},{ "onupdate","SetFullSize"},{"oncomplete","SetComplete"},{ "easetype","linear"}});	
	}
	
	private void SetComplete(){
		openAnim.isInOperation = false;
	}
	
	private void SetFullSize(float f){
		openAnim.isInOperation = true;
		openAnim.viewCamera.fullSize = f;
	}
	
	private void SetColorAlpha(float a){
		openAnim.marvel.alpha = a;
		openAnim.title.alpha = a;
	}
}
