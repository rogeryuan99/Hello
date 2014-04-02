using UnityEngine;
using System.Collections;

public class BoneCleave : PieceAnimation {
	
	public GameObject dbzg3C;      
	public GameObject dbzg2C;      
	public GameObject dbzg1C;      
	public GameObject sd2C	;      
	public GameObject sd3C	;      
	public GameObject sd4C	;      
	public GameObject gj1C	;      
	public GameObject gj2C	;      

	public override void Awake (){ 		
		base.Awake();
		animaPlayEndScript(destroySelf);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		partList["dbzg3C"] = dbzg3C;
		partList["dbzg2C"] = dbzg2C;
		partList["dbzg1C"] = dbzg1C;
		partList["sd2C"] = sd2C;    
		partList["sd3C"] = sd3C;    
		partList["sd4C"] = sd4C;    
		partList["gj1C"] = gj1C;    
		partList["gj2C"] = gj2C;    

	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
