using UnityEngine;
using System.Collections;

public class CharacterAnima : EffectAnimation {
	protected override void selectActInFileMgr (){
		Hashtable actMgr = AnimaFileMgr.heroesActHash[type] as Hashtable;
		actDatas = actMgr["actDatas"] as ActData[];
		actList  = actMgr["actList"] as Hashtable;
	}
	protected override Hashtable selectBoneInFileMgr (){
		
		Hashtable testK = AnimaFileMgr.heroesBoneHash[type] as Hashtable;
		return AnimaFileMgr.heroesBoneHash[type] as Hashtable; 
	}
}
