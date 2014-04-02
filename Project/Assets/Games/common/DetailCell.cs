using UnityEngine;
using System.Collections;

public class DetailCell : MonoBehaviour {
	public virtual void OnOut(){
		Debug.Log("nonoverrided OnOut");
	}
	public virtual void OnIn(object data){
		Debug.Log("nonoverrided OnIn");
	}
	
	public DynamicCell getDynamicCell(){
		DynamicCell dc = transform.parent.GetComponent<DynamicCell>();
		return dc;		
	}
	public void tryToSelect(){
		getDynamicCell().tryToSelect();
	}
	
	public void tryToUnselect(){
		getDynamicCell().tryToUnselect();	
	}
	
	public virtual void OnSelected(){
		Debug.Log("nonoverrided OnSelected");
	}
	public virtual void OnUnselected(){
		Debug.Log("nonoverrided OnUnselected");
	}
	public virtual void OnSelectForbided(){
		Debug.Log("nonoverrided OnSelectForbided");
	}
	public virtual void OnKicked(){
		Debug.Log("nonoverrided OnKicked");
	}		
	public virtual void OnSelectionFull(bool isFull){
		//Debug.Log("nonoverrided OnSelectionFull :"+isFull);
	}	
	
	protected bool isSelected{
		get{
			return getDynamicCell().isSelected;
		}
	}
	
	protected bool isSelectionFull{
		get{
			if(getDynamicCell().selectableGrid == null){
				Debug.LogError("NO SelectableGrid");	
				return false;
			}
			return getDynamicCell().selectableGrid.isSelectionFull();
		}
	}
}
