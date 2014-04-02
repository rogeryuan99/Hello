using UnityEngine;
using System.Collections;

public class HeroBarCell : DetailCell {
	public UILabel Index;
	public UILabel name;
	private int index;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void OnIn(object data){
		index = this.transform.parent.GetComponent<DynamicCell>().index;
		Index.text = "" + index;
		name.text = "Character" + index; 
	}
	
	public override void OnOut(){
		
	}
}
