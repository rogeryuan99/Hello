using UnityEngine;
using System.Collections;

public class BoneEnemyLocustQueen : PieceAnimation {
	public GameObject armdownL;	
	public GameObject armUpR;	
	public GameObject armdownR;	 
	public GameObject FootAfterL; 
	public GameObject FootAfterR;
	public GameObject FootBeforeL;	
	public GameObject FootBeforeR;	

	public GameObject legAfterR;	
	public GameObject legBeforeR;	
	public GameObject Shadow;		
	public override void Awake (){
base.Awake();
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["armdownL"] = armdownL;
		partList["armUpR"] = armUpR;
		partList["armdownR"] = armdownR; 
		partList["body"] = body;    
		
		partList["HandL"] = handL;  
		partList["HandR"] = handR;   
		
		partList["head"] = head;  
		
		partList["FootAfterL"] = FootAfterL;   
		partList["FootAfterR"] = FootAfterR;
		partList["FootBeforeL"] = FootBeforeL; 
		partList["FootBeforeR"] = FootBeforeR;  
		partList["legAfterR"] = legAfterR; 
		partList["legBeforeR"] = legBeforeR; 
		
		partList["Shadow"] = Shadow;
	}
}