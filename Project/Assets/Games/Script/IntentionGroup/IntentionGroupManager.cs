using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntentionGroupManager {
	
	private static IntentionGroupManager _instance;
	
	
	public static IntentionGroupManager Instance{
		get{
			if (null == _instance){
				_instance = new IntentionGroupManager();
			}
			return _instance;
		}
	}
}
