using UnityEngine;
using System.Collections;

public class Bone_BetaRayBill30B_Lighting : PieceAnimation {
	
    public GameObject flash_f01  ;   
    public GameObject flash_f02  ;   
    public GameObject flash_f03  ;   
    public GameObject flash_sd   ;    
    public GameObject light_c02  ;   	
	
	
	public override void Awake ()
	{
		animaPlayEndScript((s)=>{ Destroy(gameObject); });
		base.Awake ();
	}
	
	// Update is called once per frame
	protected override void initPartData ()
	{
		partList = new Hashtable ();
		
        partList["flash_f01"  ] = flash_f01  ; 
        partList["flash_f02"  ] = flash_f02  ; 
        partList["flash_f03"  ] = flash_f03  ; 
        partList["flash_sd"   ] = flash_sd   ;
        partList["flash_sd__1"] = flash_sd   ;   
        partList["flash_sd__2"] = flash_sd   ;   
        partList["flash_sd__3"] = flash_sd   ;   
        partList["flash_sd__4"] = flash_sd   ;   
        partList["flash_sd__5"] = flash_sd   ;   
        partList["flash_sd__6"] = flash_sd   ;   
        partList["flash_sd__7"] = flash_sd   ;   
        partList["flash_sd__8"] = flash_sd   ;   
        partList["flash_sd__9"] = flash_sd   ;   
        partList["light_c02"  ] = light_c02  ; 		
	}
}
