using UnityEngine;
using System.Collections;
using System.IO;

public class StartUpLoadStatic_CacheMgrLoad : Task
{
	public override void run ()
	{
		GameObject cacheObj = Instantiate (InitGameData.instance.cacheMgrPrb) as GameObject;
		CacheMgr cacheMgr = cacheObj.GetComponent<CacheMgr> ();
		cacheMgr.shouldStartDownload = true;
	}
	void Update(){
		if(CacheMgr.getIsLoaded ()){
			complete();
		}
	}
}
