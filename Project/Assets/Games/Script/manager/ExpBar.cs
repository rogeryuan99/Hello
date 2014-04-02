using UnityEngine;
using System.Collections;

public class ExpBar : MonoBehaviour
{
	public GameObject[] hpObj;
//	public int lvUpNum = 0;
	[HideInInspector] public int maxExp =0;//readyonly
	[HideInInspector] public int minExp =0;//readyonly
	[HideInInspector] public int level =1;//readyonly
	[HideInInspector] public int exp =0;
//	private int upMaxExp;
//	private int upExp;
//	private bool  isChange = false;
//	private float spd = 0.05f;
//	private Hero hero;
	
// Jugg
	public UILabel expTxt;
	public UILabel lvUpTxt;
	//public static string ADD_EXP_COMPLETE = "ADD_EXP_COMPLETE";

	public void initBar(int exp)
	{
		this.exp = exp;
		minExp = 0;
		maxExp = int.MaxValue;
		level =1;
		for(int n = 0;n<HeroData.expList.Count;n++){
			int l = (int)HeroData.expList[n];
			if(l>exp){
				maxExp = l;
				level = n+1;
				break;
			}else{
				minExp = l;	
			}
		}
		float scaleValue = Mathf.Max(0.01f,exp-minExp) / Mathf.Max(0.01f,maxExp - minExp);
		hpObj [0].transform.localScale = new Vector3 (scaleValue, 1, 1);
		hpObj [1].transform.localScale = new Vector3 (scaleValue, 1, 1);
	}
//
//	public void clearTxt ()
//	{
//		//expTxt.text = "";
//	}
	
//// Jugg
//	public void initBar (int maxExp, int exp, Hero hero)
//	{
//		this.maxExp = maxExp;
//		this.exp = exp;
//		this.hero = hero;
//		minExp = exp;
//		float scaleValue = Mathf.InverseLerp (0, maxExp, exp);
//		Debug.Log ("#### initBar --maxExp: " + maxExp + "----exp: " + exp + " scale=" + scaleValue);
//		hpObj [0].transform.localScale = new Vector3 (scaleValue, 1, 1);
//		hpObj [1].transform.localScale = new Vector3 (scaleValue, 1, 1);
//	}

//	public IEnumerator ChangeExpHandler (int exp, int lvUpNmu, int upMaxExp, float time)
//	{
//		this.lvUpNum = lvUpNmu;
//	
//		if (lvUpNmu != 0) {
//			this.upExp = Mathf.Max(1,exp - maxExp);
//			this.exp = exp;
//			this.upMaxExp = upMaxExp;
//		} else {
//			this.exp = exp;
//		}
//
//		float scaleValue = Mathf.InverseLerp (0, maxExp, this.exp);
//		hpObj [1].transform.localScale = new Vector3 (scaleValue, 1, 1);
//		Debug.Log ("#### ChangeExpHandler" + "  exp: " + this.exp + "  maxExp: " + maxExp + "  scaleValue=" + scaleValue);//	hpObj[1].transform.localScale.x = scaleValue;
//		yield return new WaitForSeconds(0.2f);
//		iTween.ScaleTo (hpObj [0], new Hashtable (){{"x",scaleValue},{"speed",2},{"easetype","linear"},{"oncomplete","progressComplete"},{ "oncompletetarget",gameObject}});
////	clearTxt();
////	isChange  = true;
//	}
//
//	public void progressComplete ()
//	{
//		if (lvUpNum > 0)
//		{
//			lvUpTxt.text = "Level Up";
//			Invoke ("clearTxt", 0.5f);
//			if (lvUpNum == 1) {
//				float scale = upExp / (upMaxExp * 1.0f); 
//				hpObj [0].transform.localScale = new Vector3 (0, 1, 1);
//				hpObj [1].transform.localScale = new Vector3 (scale, 1, 1);
//				iTween.ScaleTo (hpObj [0], new Hashtable (){{"x",scale},{"speed",2},{ "easetype","linear"},{"oncomplete","progressComplete"},{ "oncompletetarget",gameObject}});
//			} else {
//				hpObj [0].transform.localScale = new Vector3 (0, 1, 1);
//				hpObj [1].transform.localScale = new Vector3 (1, 1, 1);
//				iTween.ScaleTo (hpObj [0], new Hashtable (){{"x",1},{"speed",2},{ "easetype","linear"},{"oncomplete","progressComplete"},{ "oncompletetarget",gameObject}});
//			}
//			
//		} 
//		else
//		{
////			MsgCenter.instance.dispatch (new Message (ADD_EXP_COMPLETE, this));
//		}
//		lvUpNum--;
//	}
	
}