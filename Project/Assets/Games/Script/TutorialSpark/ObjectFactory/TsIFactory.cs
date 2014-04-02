using UnityEngine;
using System.Collections;


public interface TsIFactory {
	
	GameObject Create(string fullName);
	// void AttachUserBehaviourScript();
}
