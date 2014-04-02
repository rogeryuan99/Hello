using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {
	
	protected const string TITLE = "title";
	protected const string DESCRIPTION = "descrption";
	protected const string HEADICON = "headIcon";
	
	protected Hashtable data = new Hashtable();
	
	public virtual void FillData(string secretParms){
		string[] parms = TsParmsTranslator.Translate(secretParms);
		data.Clear();
		data.Add (TITLE,       parms[0]);
		data.Add (DESCRIPTION, parms[1]);
		data.Add (HEADICON,    parms[2]);
	}
}
