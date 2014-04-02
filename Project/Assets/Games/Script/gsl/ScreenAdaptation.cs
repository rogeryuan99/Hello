using UnityEngine;
using System.Collections;

public class ScreenAdaptation : MonoBehaviour {
	public float offsetX = 0f;
	
	public TYPE type = TYPE.Anchor;
	public enum TYPE{
		Anchor,
		Size
	}
	
	private const float norValue = 4.0f/3.0f;
	//private UIAnchor anchor;
	
	void Start () {
		float curValue = Utils.getScreenLogicWidth()/Utils.getScreenLogicHeight();
		float difValue = curValue - norValue;
		if(type == TYPE.Anchor){
			UIAnchor anchor = this.GetComponent<UIAnchor>();
			if(anchor == null){
				anchor = this.gameObject.AddComponent<UIAnchor>();
				anchor.side = UIAnchor.Side.Center;
			}
			anchor.relativeOffset.x = difValue*offsetX;
		}else if(type == TYPE.Size){
			this.transform.localScale = new Vector3(difValue*offsetX,this.transform.localScale.y,this.transform.localScale.z);
		}
		
	}
	
	void Update () {
	
	}
}
