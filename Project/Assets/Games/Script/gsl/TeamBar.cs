using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamBar : MonoBehaviour {
	public SelectableGrid selectableGrid;
	public delegate void TeamDelegate(int index);
	public TeamDelegate OnTeamBarCellClicked;
	
	public void init(TeamDelegate OnTeamBarCellClicked){
		this.OnTeamBarCellClicked = OnTeamBarCellClicked;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void __OnTeamBarCellClicked(int index){
		if(index == -1) return;
		if(OnTeamBarCellClicked != null)
		{
			OnTeamBarCellClicked(index);
		}
	}
	
	public int GetSelectedIndex()
	{
		if(selectableGrid.getSelectedCell() == null || selectableGrid.getSelectedCell().Count==0)
		{
			return -1;	
		}
		return selectableGrid.getSelectedCell()[0].index;
	}
	
	public void SetSelectedIndex(int n)
	{
		selectableGrid.tryToSelect(n);
	}
	
	public void ClearSelection()
	{
		selectableGrid.tryToSelect(-1);	
	}
}
