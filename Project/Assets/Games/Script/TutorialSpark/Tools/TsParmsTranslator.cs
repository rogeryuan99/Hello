using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TsParmsTranslator{

	public static string[] Translate(string secret){
		return secret.Split('|');
	}
	public static List<string> TranslateToList(string secret){
		List<string> list = new List<string>();
		
		list.AddRange(Translate(secret));
		list.RemoveAll((str)=>{ return (str == string.Empty); });
		
		return list;
	}
}
