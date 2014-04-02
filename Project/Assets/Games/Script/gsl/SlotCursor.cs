using UnityEngine;
using System.Collections;
[RequireComponent(typeof(UISprite))]
public class SlotCursor : MonoBehaviour {
	public static SlotCursor instance;
	public Camera uiCamera;
	public EquipData equipData;
	private UISprite mSprite;
	private bool isDrag;

	void Start () {
		instance = this;
		mSprite = this.GetComponent<UISprite>();
		
		if(uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
		}
	}
	
	void Update () {
		if(this.isDrag){
			if(uiCamera != null)
			{
				Vector3 pos = Input.mousePosition;
				pos.x = Mathf.Clamp01(pos.x / Screen.width);
				pos.y = Mathf.Clamp01(pos.y / Screen.height);
				this.transform.position = uiCamera.ViewportToWorldPoint(pos);
				this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,-48f);
				//this.mSprite.transform.position = new Vector3(this.mSprite.transform.position.x, this.mSprite.transform.position.y, -0.1f);
				if (uiCamera.isOrthoGraphic)
				{
					this.mSprite.transform.localPosition = NGUIMath.ApplyHalfPixelOffset(this.mSprite.transform.localPosition, this.mSprite.transform.localScale);
				}
				
			}
		}
	}
	
	public void SetDrag(bool drag){
		if(this != null) {
			this.isDrag = drag;
			if(uiCamera != null)
			{
				Vector3 pos = Input.mousePosition;
				pos.x = Mathf.Clamp01(pos.x / Screen.width);
				pos.y = Mathf.Clamp01(pos.y / Screen.height);
				this.mSprite.transform.position = uiCamera.ViewportToWorldPoint(pos);
				this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,-48f);
				//this.mSprite.transform.position = new Vector3(this.mSprite.transform.position.x, this.mSprite.transform.position.y, -0.1f);
				if (uiCamera.isOrthoGraphic)
				{
					this.mSprite.transform.localPosition = NGUIMath.ApplyHalfPixelOffset(this.mSprite.transform.localPosition, this.mSprite.transform.localScale);
				}
			}
		}
		if(!this.isDrag) this.mSprite.enabled = false;
		
		if(drag){
//			MusicManager.Instance.playEffectMusicForLoop("SFX_UI_swipe_loop_1a");	
		}else{
//			MusicManager.Instance.stopEffectMusicForLoop("SFX_UI_swipe_loop_1a");		
		}
	}

	public void SetEquipData(EquipData ed,UIAtlas atlas,string spriteName){
		if(this != null){
			this.equipData = ed;	
			if(ed != null){
//				Debug.Log("Equip : " + this.equipData.equipDef.equipName + "  in cursor~");
				this.mSprite.enabled = true;
				this.mSprite.atlas = atlas;
				this.mSprite.spriteName = spriteName;
				this.mSprite.MakePixelPerfect();
				this.mSprite.depth = 10;
				this.mSprite.transform.localScale *= .5f;
			}
		}
	}
	
	public EquipData.Type getEquipType(){
		if(this.equipData != null)
			return this.equipData.equipDef.type;	
		else{
			return 	EquipData.Type.NONE;
		}
	}
	
	public EquipData getEquipData(){
		return this.equipData;	
	}
	
	public bool getEnabledOfSlotCursor(){
		return this.mSprite.enabled;	
	}
}
