using UnityEngine;
using System.Collections;

public class FxTester : MonoBehaviour
{
	public GameObject target;
	public GameObject fx;
	public Vector3 position;
	public Vector3 rotation;
	public Vector3 scale;
	public bool Add = false;
	public bool Delete = false;
	public GameObject lastFxGo;
	
	void Update ()
	{
		if (Add) {
			Add = false;
			if(target == null || fx == null) return;
			lastFxGo = new GameObject("FxContainer");
			lastFxGo.transform.parent = target.transform;
			lastFxGo.transform.localScale = scale;
			lastFxGo.transform.localPosition = position;
			lastFxGo.transform.localRotation = Quaternion.Euler(rotation);
			GameObject go = Instantiate (fx) as GameObject;
			go.transform.parent = lastFxGo.transform;
			go.transform.localScale = fx.transform.localScale;
			go.transform.localPosition = fx.transform.localPosition;
			go.transform.localRotation = fx.transform.localRotation;

		}
		if (Delete) {
			Delete = false;
			if (lastFxGo != null) {
				Destroy (lastFxGo);
			}
		}
		if (lastFxGo != null) {
			lastFxGo.transform.parent = target.transform;
			lastFxGo.transform.localScale = scale;
			lastFxGo.transform.localPosition = position;
			lastFxGo.transform.localRotation = Quaternion.Euler(rotation);
		}
	}
	
}
