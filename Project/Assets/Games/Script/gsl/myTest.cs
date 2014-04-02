using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class myTest : MonoBehaviour {
	public UISprite sp;
	public Transform SlotTrans;
	public Transform StorageTrans;
	private bool isDrag;
	private Vector3 lastPos;
	private Vector3 curPos;
	// Use this for initialization
	void Start () {
		lastPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(isDrag){
			curPos = Input.mousePosition;
			curPos.x = Mathf.Clamp01(curPos.x / Screen.width);
			curPos.y = Mathf.Clamp01(curPos.y / Screen.height);
			this.transform.position = UICamera.mainCamera.ViewportToWorldPoint(curPos);
			
			if (UICamera.mainCamera.isOrthoGraphic)
			{
				this.transform.localPosition = NGUIMath.ApplyHalfPixelOffset(this.transform.localPosition, this.transform.localScale);
			}
		}else{
			Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit []hitObjs = Physics.RaycastAll(ray);
			if(hitObjs.Length >= 2){
				for(int n = 0;n < hitObjs.Length;n++){
					if(hitObjs[n].transform.tag == "SlotItem"){
						this.transform.position = 	SlotTrans.position;
						break;
					}
					else if(hitObjs[n].transform.tag == "StorageItem"){
						this.transform.position = 	StorageTrans.position;
						break;
					}
					else if(hitObjs[n].transform.tag != "SlotItem" && hitObjs[n].transform.tag != "StorageItem" && hitObjs[n].transform.tag != "EquipItem"){
						this.transform.position	= lastPos;
						break;
					}
				}
			}else if(hitObjs.Length >= 1 && hitObjs[0].transform.tag == "EquipItem"){
				this.transform.position	= lastPos;
			}else 
				return;
		}
	}
	
	void OnDrag(Vector2 delta){
		
	}
	
	void OnPress (bool isPressed){
		if(isPressed){
			sp.spriteName = "Star";	
			isDrag = true;
			//lastPos = this.transform.position;	
			Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit []hitObjs = Physics.RaycastAll(ray);
			for(int n = 0;n < hitObjs.Length;n++){
				if(hitObjs[n].transform.tag == "SlotItem"){
					lastPos = SlotTrans.position;
				}
				else if(hitObjs[n].transform.tag == "StorageItem"){
					lastPos = StorageTrans.position;
				}
			} 
		}else{
			sp.spriteName = "abandon";	
			isDrag = false;
			Ray ray1 = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit []hitObjs1 = Physics.RaycastAll(ray1);
			for(int n = 0;n < hitObjs1.Length;n++){
				if(hitObjs1[n].transform.tag == "SlotItem"){
					lastPos = SlotTrans.position;
				}
				else if(hitObjs1[n].transform.tag == "StorageItem"){
					lastPos = StorageTrans.position;
				}
			} 
		}
	}
}
