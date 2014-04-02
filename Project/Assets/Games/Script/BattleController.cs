using UnityEngine;
using System.Collections;

public class BattleController : MonoBehaviour {
	
	private static BattleController _instance;
	
	
	public static BattleController Instance{
		get{
			return _instance;
		}
	}
	
	
	// Functions
	public void Awake(){
		_instance = this;
	}
	
	
/* Never Used
	public void setSkEffect ( string skName  ){
	//	nameTxt.SetActiveRecursively(true);
	//	SpriteText sptTxt = nameTxt.GetComponent<SpriteText>();
	//	sptTxt.Text = skName;
	//	yield return new WaitForSeconds(3);
	//	nameTxt.SetActiveRecursively(false);
	}
*/
	
	public void OnDestroy(){
		_instance = null;
	}
}
