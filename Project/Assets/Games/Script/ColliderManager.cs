using UnityEngine;
using System.Collections;

public class ColliderManager : Singleton<ColliderManager> {
	
	private float ENLAGE_SCALE_SELECTION = 2.0f;
	
	public void EnlargeHeroColliders (){
		Hashtable heroes = HeroMgr.heroHash.Clone() as Hashtable;
		if(heroes.Count>0){
			foreach(string key in heroes.Keys)
			{
				Character en = heroes[key] as Character;
				BoxCollider collider = en.gameObject.collider as BoxCollider;
				if (en.isEnlarged == false)
				{
					collider.size = new Vector3(collider.size.x * ENLAGE_SCALE_SELECTION, collider.size.y * ENLAGE_SCALE_SELECTION, collider.size.z);
					en.isEnlarged = true;
				}
			}
		}
	}	
	public void EnlargeEnemyColliders (){
		Hashtable enemyHash = EnemyMgr.enemyHash.Clone() as Hashtable;
		if(enemyHash.Count>0){
			foreach(string key in enemyHash.Keys)
			{
				Character en = enemyHash[key] as Character;
				BoxCollider collider = en.gameObject.collider as BoxCollider;
				if (en.isEnlarged == false) {
					collider.size = new Vector3(collider.size.x * ENLAGE_SCALE_SELECTION, collider.size.y * ENLAGE_SCALE_SELECTION, collider.size.z);
					en.isEnlarged = true;
				}
			}
		}
	}
	
	public void ShrinkHeroColliders (){
		Hashtable heroes = HeroMgr.heroHash.Clone() as Hashtable;
		if(heroes.Count>0){
			foreach(string key in heroes.Keys){
				Character en = heroes[key] as Character;
				BoxCollider collider = en.gameObject.collider as BoxCollider;
				if (en.isEnlarged == true) {
					collider.size = new Vector3(collider.size.x / ENLAGE_SCALE_SELECTION, collider.size.y / ENLAGE_SCALE_SELECTION, collider.size.z);
					en.isEnlarged = false;
				}
			}
		}	
	}
	public void ShrinkEnemyColliders (){
		Hashtable enemyHash = EnemyMgr.enemyHash.Clone() as Hashtable;
		if(enemyHash.Count>0){
			foreach(string key in enemyHash.Keys){
				Character en = enemyHash[key] as Character;
				BoxCollider collider = en.gameObject.collider as BoxCollider;
				if (en.isEnlarged == true) {
					collider.size = new Vector3(collider.size.x / ENLAGE_SCALE_SELECTION, collider.size.y / ENLAGE_SCALE_SELECTION, collider.size.z);
					en.isEnlarged = false;
				}
			}
		}
	//	}
	}
}
