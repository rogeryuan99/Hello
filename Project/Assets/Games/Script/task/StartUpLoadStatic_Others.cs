using UnityEngine;
using System.Collections;
using System.IO;

public class StartUpLoadStatic_Others : Task
{
	public override void run ()
	{
		TsFtueManager.Instance.Init();
		StoreGoodsManager.Instance.parseFromJson((Resources.Load ("configData/nouse/CONF_StoreGoodsDef") as TextAsset).text);
		StaticData.setEftXmlData ((Resources.Load ("configData/SkillCastEffectData") as TextAsset).text); 
		AnimaFileMgr.initEftActData ();
		AnimaFileMgr.initEftBoneData ();

		complete();
	}
}
