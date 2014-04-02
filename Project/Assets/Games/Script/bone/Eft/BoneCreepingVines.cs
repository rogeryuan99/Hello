using UnityEngine;
using System.Collections;

public class BoneCreepingVines : PieceAnimation {

	public GameObject   light_1;  
	public GameObject   light_2;     
	public GameObject   tree_0 ;     
	public GameObject   tree_1 ;     
	public GameObject   tree_10;     
	public GameObject   tree_11;     
	public GameObject   tree_2 ;     
	public GameObject   tree_3 ;     
	public GameObject   tree_4 ;     
	public GameObject   tree_5 ;     
	public GameObject   tree_6 ;     
	public GameObject   tree_7 ;     
	public GameObject   tree_8 ;     
	public GameObject   tree_9 ;        


	
	
	public override void Awake (){ 		
		base.Awake();
		animaPlayEndScript(destroySelf);
		this.addFrameScript("Stand", 32, PauseAnima);
		
	}
	
	public void PauseAnima(string s)
	{
		this.pauseAnima();
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["light_2"] = light_2	;      
		partList["tree_0"] = tree_0  ;       
		partList["tree_1"] = tree_1  ;       
		partList["tree_3"] = tree_3  ;       
		partList["tree_2"] = tree_2  ;       
		partList["tree_10"] = tree_10 ;      
		partList["tree_11"] = tree_11 ;      
		partList["tree_8"] = tree_8  ;       
		partList["tree_9"] = tree_9  ;       
		partList["tree_4"] = tree_4  ;       
		partList["tree_5"] = tree_5  ;       
		partList["tree_6"] = tree_6  ;       
		partList["tree_7"] = tree_7  ;       
		partList["light_1"] = light_1 ;      
	}
	
	public void OnDestroy()
	{
		removeFrameScript("Stand", 32);
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
