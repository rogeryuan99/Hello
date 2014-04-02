using UnityEngine;
using System.Collections;

public class JumpToFisrtScene : MonoBehaviour {
	void Awake () {
		if( CacheMgr.getIsLoaded()==false)
			Application.LoadLevel("initGameResource");
	}
	
}
