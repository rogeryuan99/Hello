using UnityEngine;
using System.Collections;

public class BoneDeckGun : PieceAnimation {

	public GameObject drop_shadow;
	public GameObject nebula_40  ;
	public GameObject nebula_49  ;
	public GameObject nebula_50  ;
	public GameObject nebula_8   ;
	
	public delegate void Callback();
	public Callback OnMoveEnd;
	public Callback OnAttackEnd;
	
	public override void Awake ()
	{ 		
		base.Awake();
	  	
		// addFrameScript("Move",9, fruitExplode);
		
		animaPlayEndScript(OnPlayEnd);
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
				
        partList["drop_shadow" ] = drop_shadow;
        partList["nebula_40"   ] = nebula_40  ;
        partList["nebula_40__1"] = nebula_40  ;
        partList["nebula_49"   ] = nebula_49  ;
        partList["nebula_50"   ] = nebula_50  ;
        partList["nebula_8"    ] = nebula_8   ;
	}
	
	private void OnPlayEnd(string actName){
		if ("Move" == actName && null != OnMoveEnd){
			OnMoveEnd();
		}
		else if ("Attack" == actName && null != OnAttackEnd){
			OnAttackEnd();
		}
	}
}
