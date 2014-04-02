using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(DynamicGrid))]
public class SelectableGrid : MonoBehaviour {
	public enum SelectTooMany{
		KICK,
		FORBID
	}
	public enum SelectResult{
		OK,
		FORBID,
		ERROR_ALREADY_SELECTED
	}
	public enum UnselectResult{
		OK,
		ERROR_NOTSELECTED
	}
	public int max = 1;
	public SelectTooMany selectTooMany = SelectTooMany.FORBID;
	public List<int> selectedIndexList = new List<int>();
	private DynamicGrid dynamicGrid;
	void Awake(){
		dynamicGrid = GetComponent<DynamicGrid>();
	}
	
	public SelectResult tryToSelect(int index){
		if(selectedIndexList.Contains(index)){
			return SelectResult.ERROR_ALREADY_SELECTED;	
		}
		if(isSelectionFull()){
			if(selectTooMany == SelectTooMany.FORBID){
				return SelectResult.FORBID;	
			}else if(selectTooMany == SelectTooMany.KICK){
				int kicked = selectedIndexList[0];
				selectedIndexList.RemoveAt(0);
				DynamicCell dc = dynamicGrid.getDynamicCellAt(kicked);
				if(dc.transform.childCount>0){
					Transform child = dc.transform.GetChild(0);
					child.gameObject.SendMessage("OnKicked");
				}
			}
		}
		if(index!=-1){
			selectedIndexList.Add(index);
		}
		
		if(isSelectionFull()) {
			foreach( DynamicCell dc in dynamicGrid.list){
				if(dc.transform.childCount>0){
					Transform child = dc.transform.GetChild(0);
					child.gameObject.SendMessage("OnSelectionFull",true,SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		return SelectResult.OK;
	}
	
	public UnselectResult tryToUnselect(int index){
		if(!isSelected(index)) return UnselectResult.ERROR_NOTSELECTED; 
		for( int n = 0;n<selectedIndexList.Count;n++){
			if(selectedIndexList[n]==index){
				selectedIndexList.RemoveAt(n);	
			}
		}
		if(!isSelectionFull()) {
			foreach( DynamicCell dc in dynamicGrid.list){
				if(dc.transform.childCount>0){
					Transform child = dc.transform.GetChild(0);
					child.gameObject.SendMessage("OnSelectionFull",false,SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		return UnselectResult.OK;
	}
	
	public List<int> getSelectedIndexList(){
		return selectedIndexList;	
	}
	
	public List<object> getSelectedDataList(){
		List<object> list = new List<object>();
		foreach( int i in selectedIndexList){
			DynamicCell dc = dynamicGrid.getDynamicCellAt(i);
			list.Add(dc.data);
		}
		return list;
	}
	public bool isSelected(int index){
		return selectedIndexList.Contains(index);	
	}
	public bool isSelectionFull(){
		return selectedIndexList.Count >= max;
	}
	
	public void changeData(int index){
		//Object data1 = 
	}
	
	public List<DynamicCell> getSelectedCell(){
		List<DynamicCell> list = new List<DynamicCell>();
		foreach( int i in selectedIndexList){
			DynamicCell dc = dynamicGrid.getDynamicCellAt(i);
			list.Add(dc);
		}
		return list;
	}
}
