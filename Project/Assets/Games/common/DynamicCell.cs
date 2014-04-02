using UnityEngine;
using System.Collections;

public class DynamicCell : MonoBehaviour
{
	public Camera viewCamera;
	public DynamicGrid dynamicGrid;
	public bool shouldShow = false;
	public GameObject detailCell;
	public object data;
	public int index;
	public SelectableGrid selectableGrid;
	public float renderRange;
	// Update is called once per frame
	public delegate void CallBackDelegate(DynamicCell cell);
	public CallBackDelegate OnInitedCallBack;
	
	void Start ()
	{
		if (viewCamera == null) {
			Debug.LogError ("no viewCamera");	
		}
		dynamicGrid = this.transform.parent.GetComponent<DynamicGrid> ();
		if (dynamicGrid == null) {
			Debug.LogError ("DynamicCell must has parent DynamicGrid");	
		}
		this.renderRange = dynamicGrid.renderRange;
		selectableGrid = transform.parent.GetComponent<SelectableGrid> ();
	}

	void Update ()
	{
		bool inView = true;
		float xDistance = viewCamera.transform.position.x - this.transform.position.x;
		xDistance = Mathf.Abs (xDistance);
		float yDistance = viewCamera.transform.position.y - this.transform.position.y;
		yDistance = Mathf.Abs (yDistance);
		if (xDistance > renderRange || yDistance > renderRange)
			inView = false;
		
		if (shouldShow && !inView) {
			shouldShow = false;
			_OnOut ();
		} else if (!shouldShow && inView) {
			shouldShow = true;
			_OnIn ();			
			if (null != OnInitedCallBack){
				OnInitedCallBack(this);
			}
		}
	}

	private void _OnIn ()
	{
//		Debug.Log("override OnIn");
		if (dynamicGrid.pool.Count > 0) {
			detailCell = dynamicGrid.pool [0];
			dynamicGrid.pool.RemoveAt (0);
		} else {
			detailCell = Instantiate (dynamicGrid.cellPrefab) as GameObject;
			UIDragCamera udc = detailCell.GetComponent<UIDragCamera> ();
			if(udc != null)udc.draggableCamera = this.viewCamera.GetComponent<UIDraggableCamera> ();
		}
		detailCell.transform.parent = this.transform;
		detailCell.transform.localPosition = Vector3.zero;
		detailCell.transform.localScale = Vector3.one;
		
		detailCell.GetComponent<DetailCell> ().OnIn (data);
	}

	private void _OnOut ()
	{
		if (detailCell != null) {
			dynamicGrid.pool.Add (detailCell);	
			detailCell.GetComponent<DetailCell> ().OnOut ();
			detailCell.transform.parent = dynamicGrid.poolRoot.transform;
			detailCell.transform.localPosition = Vector3.zero;
		}
	}
	
	//selection related
	
	public void tryToSelect ()
	{
		if (selectableGrid == null) {
			Debug.LogError ("parent SelectableGrid is needed");	
			return;
		}
		switch (selectableGrid.tryToSelect (index)) {
		case SelectableGrid.SelectResult.ERROR_ALREADY_SELECTED:
			break;
		case SelectableGrid.SelectResult.FORBID:
			detailCell.GetComponent<DetailCell> ().OnSelectForbided ();	
			break;
		case SelectableGrid.SelectResult.OK:
			detailCell.GetComponent<DetailCell> ().OnSelected ();
			break;
		}
		///
	}
	
	public void tryToUnselect(){
		if (selectableGrid == null) {
			Debug.LogError ("parent SelectableGrid is needed");	
			return;
		}
		switch (selectableGrid.tryToUnselect(index)) {
			case SelectableGrid.UnselectResult.OK:
				detailCell.GetComponent<DetailCell> ().OnUnselected ();
			break;
		}
	}
	
	public bool isSelected{
		get{
			if (selectableGrid == null) {
				Debug.LogError ("parent SelectableGrid is needed");	
				return false;
			}
			return selectableGrid.isSelected(index);
		}
	}
	
	public void changeSelectedData(object data){
//		DynamicCell cell1 = selectableGrid.getSelectedDataList[0];
		
	}
		
}
