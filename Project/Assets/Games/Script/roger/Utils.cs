using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
public class Utils : MonoBehaviour
{
	public static string dumpHashTable (Hashtable t)
	{
		return "Hashtable DUMP...\n" + dumpHashTable (t,0);
	}

	private static string dumpHashTable (Hashtable t, int level)
	{
		string space = "";
		for (int n = 0; n<level; n++) {
			space += "  ";	
		}
		string s = "";
		foreach (object k in t.Keys) {
			var v = t [k];
			if (v is Hashtable) {
				s += space + k + "[HashTable]:\n";
				s += dumpHashTable (v as Hashtable, level + 1);	
			} else if (v is ICollection) {
				s += space + k + "[ArrayList]:\n";
				s += dumpList (v as IList, level + 1);
			} else {
				s += space + k + ":" + v + " <" + ((v==null)?null:v.GetType ()) + ">\n";
			}
		}
		return s;
	}

	public static string dumpList (IList list)
	{ 
		return dumpList (list, 0);
	}

	private static string dumpList (IList list, int level)
	{ 
		string space = "";
		for (int n = 0; n<level; n++) {
			space += "   ";	
		}
		string s = "";
		foreach (var v in list) {
			if (v is Hashtable) {
				s += space + "[HashTable]:\n";
				s += dumpHashTable (v as Hashtable, level + 1);	
			} else if (v is ICollection) {
				s += space + "[ArrayList]:\n";
				s += dumpList (v as IList, level + 1);
			} else {
				s += space + v + " <" + (v == null ? "  " : v.GetType ().ToString ()) + ">\n";
			}
		}
		return s;
	}
	
	public static string dumpObject (object o, int level =0, int maxLeve = 4, bool includeStatic = true)
	{
		if (level == maxLeve)
			return "TOO DEEP";
		System.Type type = o.GetType ();
		string s = "\n[" + type.Name + "] " + "\n";
		System.Reflection.PropertyInfo[] pis = type.GetProperties ();
		foreach (System.Reflection.PropertyInfo pi in pis) {
			//Debug.Log("property    "+pi.Name+"="+pi.GetValue(this,null));
			string space = "";
			for (int n = 0; n<level; n++) {
				space += "   ";	
			}			
			var v = pi.GetValue (o, null);
			if (v is Hashtable) {
				foreach (var kk in (v as Hashtable).Keys) {
					s += space + "key:" + kk + "  value:" + dumpObject ((v as Hashtable) [kk], level++);
				}
			} else if (v is IList) {
				foreach (var vv in (v as IList)) {
					s += space + dumpObject (vv, level++) + "\n";
				}
			} else {
				s += space + "  " + pi.Name + "=" + v + "\n";
			}
		}
		System.Reflection.FieldInfo[] fis = type.GetFields ();
		foreach (System.Reflection.FieldInfo fi in fis) {	
			string space = "";
			for (int n = 0; n<level; n++) {
				space += "   ";	
			}
			if (fi.IsStatic && includeStatic == false)
				continue;
			var v = fi.GetValue (o);
			if (v is Hashtable) {
				s += space + "  [Hashtable]" + fi.Name + " :";
				s += space + dumpHashTable (v as Hashtable, 1);
			} else if (v is IList) {
				s += space + "  [IList]" + fi.Name + " :";
				
				s += space + dumpList (v as IList, 1);
			} else if (fi.FieldType == typeof(string[])) {
				//Debug.Log("Field   Array    "+fi.Name);			
				if (fi.GetValue (o) == null) {
					s += space + "  " + fi.Name + "=null";
				} else {
					s += space + "  " + fi.Name + ":[";
					foreach (string ss in (string[])fi.GetValue(o)) {
						//Debug.Log("              element:       "+ss);
						s += space + ss + ",";
					}
					s += space + "]";
				}
			} else {
				//Debug.Log("Field       "+fi.Name+"="+fi.GetValue(this));			
				s += space + "  " + fi.Name + "=" + fi.GetValue (o);
			}
			s += "\n";
		}
		return s;
	}
	
	public static string dumpObjectSimple (object o, bool includeStatic = true)
	{
		System.Type type = o.GetType ();
		string s = "\n[" + type.Name + "] " + "\n";
		System.Reflection.PropertyInfo[] pis = type.GetProperties ();
		foreach (System.Reflection.PropertyInfo pi in pis) {
			var v = pi.GetValue (o, null);
			s += "  " + pi.Name + "=" + v;
		}
		System.Reflection.FieldInfo[] fis = type.GetFields ();
		foreach (System.Reflection.FieldInfo fi in fis) {	
			var v = fi.GetValue (o);
			s += "  " + fi.Name + "=" + fi.GetValue (o);
		}
		return s;
	}

	public static List<int> parseCommaSeperatedInt (string s)
	{
		if (s == null)
			return null;
		string[] a = s.Split ("," [0]);
		List<int> list = new List<int> ();
		foreach (string ss in a) {
			int n = int.Parse (ss);
			list.Add (n);
		}
		return list;
	}

	public static string dumpPrimaryStringArray (string[] a)
	{
		string s = "";
		foreach (string ss in a) {
			s += ",   ";	
		}
		return s;
	}

	public static float myRandom (float near, float far)
	{
		float n = Mathf.Min (near, far);
		float f = Mathf.Max (near, far);
		float r = Random.Range (n, f);
		return (Random.Range (0, 100) > 50) ? r : -r;
	}

	public static float myAngle (Vector3 fromV, Vector3 toV)
	{
		float dis_y = toV.y - fromV.y;
		float dis_x = toV.x - fromV.x;
		float angle = Mathf.Atan2 (dis_y, dis_x);
		float deg = (angle * 360) / (2 * Mathf.PI);
		return deg;
	}

	private static bool isInited = false;
	private static bool _isPad;
	private static float _LogicWidth;
	
	private static void init ()
	{
		isInited = true;
		_LogicWidth = (640f * ((float)Screen.width / (float)Screen.height));
		float wPerH = (float)Screen.width / Screen.height;
		float ipadWPerH = 4.01f / 3.0f;
		_isPad = wPerH <= ipadWPerH;
	}
	
	public static float characterSize
	{
		get
		{
			if (!isInited)
				init ();
			if (_isPad)
			{
				return 0.2f;
			}
			else
			{
				return 0.23f;
			}
		}
	}
	public static float characterScale {
		get {
			if (!isInited)
				init ();
			if (_isPad)
				return 1f;
			else
				return 1.15f;
		}
	}

	public static float getScreenLogicHeight ()
	{
		if (!isInited)
			init ();
		return 640f;
	}

	public static  float getScreenLogicWidth ()
	{
		if (!isInited)
			init ();
		return _LogicWidth;
	}
	
	public static Vector2 toLogicPosition(Vector2 screenPosition){
		Vector2 v = new Vector2();
		v.x = screenPosition.x / Screen.width * getScreenLogicWidth();
		v.y = screenPosition.y / Screen.height * getScreenLogicHeight();
		return v;
	}
	
	public  static bool isPad ()
	{
		if (!isInited)
			init ();
		return _isPad;
	}
	public static System.DateTime FromUnixTime(double unixTime)
	{
	    var epoch = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
	    return epoch.AddSeconds((long)unixTime);
	}
}
