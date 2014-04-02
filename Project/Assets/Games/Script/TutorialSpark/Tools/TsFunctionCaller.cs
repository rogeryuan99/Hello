using UnityEngine;
using System.Collections;

public class TsFunctionCaller : MonoBehaviour {
	
	/// <summary>
	/// Sends the vector3 message.
	/// </summary>
	/// <param name='parms'>
	/// Parms Format: {0}objName{1}functionName{2}x{3}y{4}z
	/// </param>
	public void SendVector3Message(string[] parms){
		GameObject obj = TsObjectFactory.GetGameObject(parms[0]);
		Vector3 vc3 = new Vector3(float.Parse(parms[2]), 
									float.Parse(parms[3]), 
									float.Parse(parms[4]));
		
		obj.SendMessage(parms[1], vc3);
	}
	public void SendMessage(string[] parms){
		GameObject obj = TsObjectFactory.GetGameObject(parms[0]);
		obj.SendMessage(parms[1]);
	}
}
