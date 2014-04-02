using UnityEngine;
using System.Collections;

public class TimeUtils : MonoBehaviour {
	public static int UnixTime{
		get{
			System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0,System.DateTimeKind.Utc);
			double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds -100;
			return (int)timestamp;
		}
	}
	
}
