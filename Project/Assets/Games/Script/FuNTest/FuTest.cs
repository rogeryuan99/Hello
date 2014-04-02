using UnityEngine;
using System.Collections;

public class FuTest : MonoBehaviour {

	public void OnGUI(){
		if (GUI.Button(new Rect(100f,100f,100f,50f), "Create")){
			GameObject radiationPrb = Resources.Load("eft/BetaRayBill/SkillEft_BETARAYBILL30B_Lighting") as GameObject;
			GameObject radiation = Instantiate(radiationPrb) as GameObject;
		}
		if (GUI.Button(new Rect(100f,200f,100f,50f), "Delete")){
			GameObject radiation = GameObject.Find("SkillEft_BETARAYBILL30B_Lighting(Clone)");
			Destroy(radiation);
		}
	}
}
