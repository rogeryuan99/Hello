using UnityEngine;
using System.Collections;

public class TsObjectMover: MonoBehaviour{
	
	/// <summary>
	/// Moves to.
	/// </summary>
	/// <param name='parms'>
	/// Parms Format: {0}ObjName{1}x{2}y{3}z
	/// </param>
	public void MoveTo(string[] parms){
		if (parms.Length < 4) Debug.LogError("TsObjectMover.MoveTo() parms illegal.");
		GameObject obj = TsObjectFactory.GetGameObject(parms[0]);
		
		obj.transform.position = new Vector3 (float.Parse(parms[1]),
									float.Parse(parms[2]),
									float.Parse(parms[3])
								);
	}
	
	public void MoveToInLocal(string[] parms){
		if (parms.Length < 4) Debug.LogError("TsObjectMover.MoveTo() parms illegal.");
		GameObject obj = TsObjectFactory.GetGameObject(parms[0]);
		
		obj.transform.localPosition = new Vector3 (float.Parse(parms[1]),
										float.Parse(parms[2]),
										float.Parse(parms[3])
									);
	}
}
