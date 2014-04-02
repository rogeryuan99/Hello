using UnityEngine;
using System;
using System.Collections;
using System.Text;

public static class ExtensionMethods {
	
	// Vector2
	public static void SetX(this Vector2 v2, float x){
		v2.Set(x, v2.y);
	}
	public static void SetY(this Vector2 v2, float y){
		v2.Set(v2.x, y);
	}
	
	// Vector3
	public static void SetX(this Vector3 v3, float x){
		v3.Set(x, v3.y, v3.z);
	}
	public static void SetY(this Vector3 v3, float y){
		v3.Set(v3.x, y, v3.z);
	}
	public static void SetZ(this Vector3 v3, float z){
		v3.Set(v3.x, v3.y, z);
	}
	
	// ArrayList
	public static string Join(this ArrayList al, string ch){
		System.Text.StringBuilder str = new System.Text.StringBuilder(al.Count > 0 ? (string)al[0] : "");
		for (int i=1; i<al.Count; i++){
			str.Append(ch + al[i]);
		}
		return str.ToString();
	}
}
