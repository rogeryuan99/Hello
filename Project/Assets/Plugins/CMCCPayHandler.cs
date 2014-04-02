using UnityEngine;
using System.Collections;

public class CMCCPayHandler : MonoBehaviour 
{
#if UNITY_ANDROID
	public delegate void GetResultJSONString(string msg);
	public GetResultJSONString getResultJSONString;
	public static CMCCPayHandler payHandler;
	public enum PayHandlerType
    {
        INSTANTIATE,
        PAY
    }
	
	void Awake()
	{
		// Set the GameObject name to the class name for easy access from native code
		gameObject.name = this.GetType().ToString();
		DontDestroyOnLoad( this );
		payHandler = this;
		payHandler.getResultJSONString = null;
		isInitComplete = false;
		
		
		unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
       	currActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		CMCCPayPluginClass = new AndroidJavaClass("com.aspire.lavaspark.IapActivity");
	}
	
	static AndroidJavaClass CMCCPayPluginClass;
    static AndroidJavaClass unityPlayer;
    static AndroidJavaObject currActivity;
	
	public static bool isInitComplete = false;
	
	
	public static void CMCCPayHandlerInit(string APPID, string APPKEY)
	{
		CMCCPayPluginClass.CallStatic(
			"iapActivityHandler", 
			(int)PayHandlerType.INSTANTIATE, 
			currActivity, 
			APPID, 
			APPKEY, 
			"");
	}
	
	public static void CMCCPay(string payCode)
	{
		CMCCPayPluginClass.CallStatic(
			"iapActivityHandler", 
			(int)PayHandlerType.PAY, 
			currActivity, 
			"", 
			"", 
			payCode);
	}
	
	public void GetHandlerResultJSONString(string str)
	{
		Debug.Log("GetHandlerResultJSONString " + str);
		if(payHandler.getResultJSONString != null)
		{
			payHandler.getResultJSONString(str);
		}
	}
#endif	
}
