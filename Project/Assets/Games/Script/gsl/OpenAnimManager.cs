using UnityEngine;
using System.Collections;

public class OpenAnimManager : MonoBehaviour {
	public GameObject openAnimIPad;
	public GameObject openAnimIPhone;
//	public UIButtonMessage backTopBtn;
//	public UIButtonMessage backBottomBtn;
//	public UIButtonMessage nextTopBtn;
//	public UIButtonMessage nextBottomBtn;
	public GameObject Button_BackIpad;
	public GameObject Button_BackIphone;
	public GameObject Button_NextIpad;
	public GameObject Button_NextIphone;
	public UIButtonMessage skipBtn;
	public UIButtonMessage yesBtn;
	public UIButtonMessage noBtn;
	
	private GameObject target;
	
	void Start () {
		//MusicManager.playBgMusic("Guardians_Combat_Temp_2a");
		if(Utils.isPad()){
			Button_BackIpad.SetActive(true);
			Button_NextIpad.SetActive(true);
			Button_BackIphone.SetActive(false);
			Button_NextIphone.SetActive(false);
			openAnimIPad.gameObject.SetActive(true);
			openAnimIPhone.gameObject.SetActive(false);	
			target = openAnimIPad;
		}else{
			Button_BackIpad.SetActive(false);
			Button_NextIpad.SetActive(false);
			Button_BackIphone.SetActive(true);
			Button_NextIphone.SetActive(true);
			openAnimIPad.gameObject.SetActive(false);
			openAnimIPhone.gameObject.SetActive(true);
			target = openAnimIPhone;	
		}
		
		
//		switch(iPhone.generation){
//		case iPhoneGeneration.iPad1Gen:
//		case iPhoneGeneration.iPad2Gen:
//		case iPhoneGeneration.iPad3Gen:
//		case iPhoneGeneration.iPad4Gen:
//		case iPhoneGeneration.iPadMini1Gen:
//		case iPhoneGeneration.iPadUnknown:
//			openAnimIPad.gameObject.SetActive(true);
//			openAnimIPhone.gameObject.SetActive(false);	
//			target = openAnimIPad;
//			break;
//		case iPhoneGeneration.iPhone:
//		case iPhoneGeneration.iPhone3G:
//		case iPhoneGeneration.iPhone3GS:
//		case iPhoneGeneration.iPhone4:
//		case iPhoneGeneration.iPhone4S:
//		case iPhoneGeneration.iPhone5:
//			openAnimIPad.gameObject.SetActive(false);
//			openAnimIPhone.gameObject.SetActive(true);
//			target = openAnimIPhone;
//			break;
//		default:
//			openAnimIPad.gameObject.SetActive(true);
//			openAnimIPhone.gameObject.SetActive(false);	
//			target = openAnimIPad;
//			break;
//		}
//		backTopBtn.target = target;
//		backBottomBtn.target = target;
//		nextTopBtn.target = target;
//		nextBottomBtn.target = target;
		skipBtn.target = target;
		yesBtn.target = target;
		noBtn.target = target;
	}
}
