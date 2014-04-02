using UnityEngine;
using System.Collections;

public class TsEnemyFactory : TsIFactory {
	
	Enemy tEnemy = null;
	public GameObject Create(string type){
		GameObject obj = null;
		GameObject pref = CacheMgr.getEnemyPrb (type) as GameObject;
		
		obj = MonoBehaviour.Instantiate (pref) as GameObject;
		obj.transform.localScale = new Vector3(Utils.characterSize,Utils.characterSize,1);
		Enemy enemyDoc = obj.GetComponent<Enemy> ();
		if(enemyDoc.enemyAI == null)
		{
			enemyDoc.setCharacterAI(typeof(EnemyAI));
		}
		CharacterData characterD = new CharacterData (EnemyDataLib.instance [type] as Hashtable);
		enemyDoc.initData (characterD);
		
		enemyDoc.enemyType = type;
		enemyDoc.isDead = false;
		enemyDoc.data.isDead = false;
		enemyDoc.hpBar.initBar (enemyDoc.realHp);
		
		return obj;
	}
	
	// public void test(){
	//	tEnemy.move(tEnemy.transform.position);
	// }
}
