using UnityEngine;
using System.Collections;

public class EnemyMgr : MonoBehaviour {
private static int id=0;
public static Hashtable enemyHash = new Hashtable();
public static ArrayList enemies;

public static string getID (){
	id++;
	return "Enemy_"+ id;
}
public static void clear (){
	enemyHash.Clear();
}

public static void delEnemy ( string id  ){
	if(enemyHash.ContainsKey(id))
	{
		enemyHash.Remove(id);
	}
}
public static Enemy getMinHpEnemy (){
	Enemy enemy = null;
	float minHp = 1.0f;
	if(enemyHash.Count>0){
		foreach(string key in enemyHash.Keys){
			Enemy en = enemyHash[key] as Enemy;
			float enHp =en.realHp*1.0f;  
			float enMaxHp = en.realMaxHp*1.0f;
			float eHp = enHp/enMaxHp*1.0f;
//			Debug.Log(minHp+"  eHp------->"+eHp); 
			if(eHp < minHp){
					minHp=eHp;
					enemy=en;
//				Debug.Log(minHp+"  eHp------->"+en.data.type); 
			}
		}
	}
//	print(enemy+":enemy----->");
	return enemy;
}
public static Enemy getRandomEnemy (Enemy exceptOne)
{
	int length = enemyHash.Count;
	int randomID = (int)(Random.value * length);
	foreach (DictionaryEntry tempEnemy in enemyHash) {
		Enemy enemy = tempEnemy.Value as Enemy;
		if (enemy == exceptOne)
			continue;
		if (randomID-- == 0) {
			return enemy;
		}
	}
	return null;
}
	
public static void getEnemyByID ( int id  ){
//	for( int i=0; i<enemies.length; i++)
//	{
//		Enemy tempEnemy = enemies[i];
//		if(tempEnemy.getID() == id)
//		{
//			return tempEnemy;
//		}
//	}
//	return -1;
}
void Update (){
}
}