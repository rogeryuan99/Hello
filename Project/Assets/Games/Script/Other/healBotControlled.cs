using UnityEngine;
using System.Collections;

public class healBotControlled : MonoBehaviour
{
	void Start ()
	{
		iTween.ScaleTo (gameObject, new Hashtable (){{"scale",new Vector3 (1.25f, 1.25f, 1)},{ "time",1},{ "easetype","easeOutBounce"},{ "oncomplete","destroySelf"}});
	}

	private void destroySelf ()
	{
		Destroy (gameObject);
	}

}