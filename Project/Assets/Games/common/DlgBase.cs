using UnityEngine;
using System.Collections;

public class DlgBase : MonoBehaviour {
	public enum CloseStyle{
		MOVE_TO_RIGHT,
		DESTROY
	}
	public CloseStyle closeStyle = CloseStyle.MOVE_TO_RIGHT;
	
	public enum Direction{
		UP,
		DOWN,
		LEFT,
		RIGHT,
		DEFUALT
	}
	
	public delegate void VoidDelegate ();
	public VoidDelegate onClose;
	

	public void hideByLayer () {
		foreach (Transform trans in this.GetComponentsInChildren<Transform>(true)) {
			trans.gameObject.layer = 10;
		}	
	}
	
	// Update is called once per frame
	public void showByLayer () {
		foreach (Transform trans in this.GetComponentsInChildren<Transform>(true)) {
			trans.gameObject.layer = 0;
		}	
	}
	
	public void hideByActive () {
		this.gameObject.SetActiveRecursively(false);	
	}
	
	// Update is called once per frame
	public void showByActive () {
		this.gameObject.SetActiveRecursively(true);	
	}	
	
	public void showByMoveFrom(Direction direction){
		GameObject go = this.gameObject;
		switch (direction){
		case Direction.UP:
			go.transform.localPosition = new Vector3 (0,2000, 0);
			break;
		case Direction.DOWN:
			go.transform.localPosition = new Vector3 (0, -2000, 0);
			break;
		case Direction.LEFT:
			go.transform.localPosition = new Vector3 (-2000, 0, 0);
			break;
		case Direction.RIGHT:
			go.transform.localPosition = new Vector3 (2000, 0, 0);
			break;
		case Direction.DEFUALT:
			break;
		}
		foreach (UIAnchor anchor in go.GetComponentsInChildren<UIAnchor>(true)) {
			anchor.enabled = false;
		}	
		Hashtable t = new Hashtable ();
		t ["position"] = Vector3.zero;
		t ["time"] = .3;
		t ["islocal"] = true;
		t ["oncomplete"] = "enableAnchors";
		t ["oncompleteparams"] = go;
		iTween.MoveTo (go, t);
	}
	private void enableAnchors (GameObject go)
	{
		foreach (UIAnchor anchor in go.GetComponentsInChildren<UIAnchor>(true)) {
			anchor.enabled = true;
		}	
	}
	public void hideByMoveTo(bool destroy,Direction direction){
		GameObject go = this.gameObject;
		foreach (UIAnchor anchor in go.GetComponentsInChildren<UIAnchor>(true)) {
			anchor.enabled = false;
		}	
		Vector3 pos;
		switch (direction){
		case Direction.UP:
			pos = new Vector3 (0,2000, 0);
			break;
		case Direction.DOWN:
			pos = new Vector3 (0, -2000, 0);
			break;
		default:
		case Direction.DEFUALT:
		case Direction.LEFT:
			pos = new Vector3 (-2000, 0, 0);
			break;
		case Direction.RIGHT:
			pos = new Vector3 (2000, 0, 0);
			break;
		}		
		Hashtable t = new Hashtable ();
		t ["position"] = pos;
		t ["time"] = .3;
		t ["islocal"] = true;
		if(destroy){
			t ["oncomplete"] = "delayedDestroy";
			t ["oncompleteparams"] = go;
		}
		iTween.MoveTo (go, t);
	}
	private void delayedDestroy (GameObject go)
	{
		Destroy(go);
	}
	public virtual void OnBtnBackClicked(){
		if(onClose != null){
			onClose();
			switch(closeStyle){
			case CloseStyle.MOVE_TO_RIGHT:	
				hideByMoveTo(true,DlgBase.Direction.RIGHT);
				break;
			case CloseStyle.DESTROY:
				Destroy(gameObject);
				break;
			}
		}else{
			Debug.LogError("Can't back, no onClose()");	
		}

		Debug.LogWarning(" OnDestroy "+this.gameObject);
		DlgManager.instance.activeHiddenDlgInTheStack(this.gameObject);

	}
	public void OnAndroidHome(){
		if(TsTheater.InTutorial) return;		
		OnBtnBackClicked();
	}	
}
