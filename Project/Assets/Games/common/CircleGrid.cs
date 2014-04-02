using UnityEngine;
using System.Collections;

public class CircleGrid : MonoBehaviour {
	private UIGrid uigrid;
	private DynamicGrid dynamicGrid;
	private UICenterOnChild centerOnChild;
	public int leftRightMin=1;
	
	void Start(){
		uigrid = this.GetComponent<UIGrid>();	
		if(uigrid == null){
			Debug.LogError("UIGrid must in parent");	
		}
		dynamicGrid = this.GetComponent<DynamicGrid>();
		if(uigrid == null){
			Debug.LogError("DynamicGrid must in parent");	
		}
		centerOnChild = this.GetComponent<UICenterOnChild>();	
		if(centerOnChild == null){
			Debug.LogError("UICenterOnChild must in parent");	
		}
		centerOnChild.recalculateCenterCellOnUpdate = true;
	}
	// Update is called once per frame
	void Update () {
		if(dynamicGrid.list.Count <= leftRightMin+leftRightMin+1){
			return;	
		}
		GameObject hotCell = centerOnChild.hotCellGo;
		GameObject leftMostCell = null;
		GameObject rightMostCell = null;
		if(centerOnChild.hotCellGo == null) return;
		foreach(DynamicCell dc in dynamicGrid.list){
//			Debug.Log("centerOnChild.hotCellGo="+centerOnChild.hotCellGo);
//			Debug.Log("dc.gameObject="+dc.gameObject);
			if(leftMostCell == null || leftMostCell.transform.localPosition.x > dc.gameObject.transform.localPosition.x){
				leftMostCell = dc.gameObject;	
			}
			if(rightMostCell == null || rightMostCell.transform.localPosition.x < dc.gameObject.transform.localPosition.x){
				rightMostCell = dc.gameObject;	
			}
		}
//		Debug.Log("hot:"+hotCell.name);
//		Debug.Log(" left:"+leftMostCell.name);
//		Debug.Log(" right:"+rightMostCell.name);
//		Debug.Log("hot:"+hotCell.name+" left:"+leftMostCell.name+" right:"+rightMostCell.name);
		int leftCells = (int) Mathf.Abs(Mathf.Ceil((leftMostCell.transform.localPosition.x - hotCell.transform.localPosition.x)/uigrid.cellWidth));
		int rightCells = (int) Mathf.Abs(Mathf.Ceil((rightMostCell.transform.localPosition.x - hotCell.transform.localPosition.x)/uigrid.cellWidth));
		if(leftCells<leftRightMin){
			rightMostCell.transform.localPosition = leftMostCell.transform.localPosition - new Vector3(uigrid.cellWidth,0,0);	
		}else if(rightCells<leftRightMin){
			leftMostCell.transform.localPosition = rightMostCell.transform.localPosition + new Vector3(uigrid.cellWidth,0,0);	
		}
	}
}
