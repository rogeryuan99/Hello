using UnityEngine;
using System.Collections;

public class BoneHealEftSK1 : EffectAnimation {
	public GameObject fw;
	public GameObject fw2;
	public GameObject gg11;
	
	
	public override void Awake (){
base.Awake();
		animaPlayEndScript(destroySelf);
//		playAct("Move");
	}
	
	protected override void initPartData (){
		partList = new Hashtable();
		
		partList["fw11"] = fw;
		partList["fw12"] = fw2;
		partList["fw13"] = fw;
		partList["fw14"] = fw2;
		partList["fw10"] = fw;
		partList["fw9"] =  fw2;
		partList["fw15"] = fw;
		partList["fw16"] = fw2;
		partList["fw17"] = fw;
		partList["fw18"] = fw2;
		partList["fw19"] = fw;
		partList["fw20"] = fw2;
		
		partList["fw21"] = fw;
		partList["fw22"] = fw2;
		partList["fw23"] = fw;
		partList["fw8"] = fw2;
		partList["fw7"] = fw;
		partList["fw6"] = fw2;
		partList["fw5"] = fw;
		partList["fw4"] = fw2;
		partList["fw3"] = fw;
		partList["fw2"] = fw2;
		partList["fw1"] = fw;
		partList["bw1"] = gg11;
	}
	
	protected void destroySelf (string s){
		Destroy(this.gameObject);
	}
}
