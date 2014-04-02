using UnityEngine;
using System.Collections;

public class GridExtendCell : MonoBehaviour 
{
	public float extendHeight = 0;
	private float lastExtendHeight;
	
	void Update(){
		if(lastExtendHeight!=extendHeight){
			GridExtend ge = this.transform.parent.GetComponent<GridExtend>();
			if(ge==null){
				Debug.LogError("parent should be GridExtend");	
			}else{
				ge.repositionNow = true;	
			}
			lastExtendHeight = extendHeight;
		}
	}
}
