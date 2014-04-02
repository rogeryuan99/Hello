using UnityEngine;
using System.Collections;

public class DataViewer : MonoBehaviour
{
	void Awake ()
	{
		DontDestroyOnLoad (this.gameObject);	
	}

	private bool showEnemiesButton = false;
	// Use this for initialization
	void OnGUI ()
	{
		GUILayout.BeginVertical ();
		GUILayout.Space (300);
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("UserInfo.heroDataList")) {
			for (int i=0; i<UserInfo.heroDataList.Count; i++) {
				HeroData heroData = UserInfo.heroDataList [i] as HeroData;
				Debug.Log (Utils.dumpObject (heroData, 0, 5, false));
			}
		}
		if (GUILayout.Button ("SkillLib.instance.lib")) {
			Debug.Log (Utils.dumpHashTable (SkillLib.instance.allHeroSkillHash));
		}
		if (GUILayout.Button ("Hero in Game")) {
			Exxo exxo = GameObject.FindObjectOfType (typeof(Exxo)) as Exxo;
			Debug.Log (Utils.dumpObject (exxo));
		}
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Goto TestGround")) {
			showEnemiesButton = true;
			Application.LoadLevel ("TestGround");
		}
		if (GUILayout.Button ("CreatePlayer")) {
			for (int i=0; i<UserInfo.heroDataList.Count; i++) {
				HeroData heroData = UserInfo.heroDataList [i] as HeroData;
				if (heroData.state != HeroData.State.SELECTED)
					continue;//selected only
			
				GameObject heroObj = null;
				Vector2 pt = new Vector2 (-482 + Random.Range (-300, 300), Random.Range (-250, 150));
				heroObj = Instantiate (CacheMgr.getHeroPrb (heroData.type), new Vector3 (pt.x, pt.y, StaticData.objLayer), new Quaternion (0, 0, 0, 0)) as GameObject;
				Transform a = GameObject.Find("Anchor").transform;
				heroObj.transform.parent = a;
				Hero hero = heroObj.GetComponent<Hero> ();
				hero.initData (heroData);
				Vector3 initPosition = heroObj.transform.position + new Vector3 (500, 0, 0);
				hero.move (initPosition);
			}			
		}
		GUILayout.EndHorizontal ();
		if (showEnemiesButton) {
			GUILayout.BeginHorizontal ();
			int n = 0;
			foreach (string enemyTypeID in EnemyDataLib.instance.Keys) {
				if (++n == 5) {
					n = 0;
					GUILayout.EndHorizontal ();
					GUILayout.BeginHorizontal ();
				}
				if (GUILayout.Button (enemyTypeID)) {
					StartCoroutine (createEnemy (enemyTypeID));
				}
			}
			GUILayout.EndHorizontal ();
		} else {
			if (GUILayout.Button ("Enemies")) {
				showEnemiesButton = true;
			}
		}
		GUILayout.EndVertical ();
		GUILayout.BeginArea (new Rect (Screen.width - 400, 0, 400, 400));
		GUILayout.BeginVertical ();
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Nothing")) {
			letAllEnemyPlay ("xx");
		}
		if (GUILayout.Button ("Stand")) {
			letAllEnemyPlay ("Stand");
		}
		if (GUILayout.Button ("Move")) {
			letAllEnemyPlay ("Move");
		}
		if (GUILayout.Button ("Attack")) {
			letAllEnemyPlay ("Attack");
		}
		if (GUILayout.Button ("Damage")) {
			letAllEnemyPlay ("Damage");
		}
		if (GUILayout.Button ("Death")) {
			letAllEnemyPlay ("Death");
		}
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
		
		GUILayout.EndArea ();
	}
	
	private IEnumerator createEnemy (string enemyTypeID)
	{
		GameObject e = createEnemyByType (enemyTypeID, new Vector3 (600, Random.Range (-250, 150), -200));
		//e.transform.localScale = new Vector3 (.5f, .5f, .5f);
		Enemy enemyDoc = e.GetComponent<Enemy> ();
//		Transform a = GameObject.Find("Anchor").transform;
//		e.transform.parent = a;
		enemyDoc.realHp = 100;
//		enemyDoc.realDef = 100;
//		enemyDoc.realAtk = 100;
		enemyDoc.realMaxHp = 200;
		enemyDoc.hpBar.initBar (enemyDoc.realHp);
		yield return new WaitForSeconds(0.5f);
		enemyDoc.relive ();
		enemyDoc.move (new Vector3 (Random.Range (-300, 300), Random.Range (-250, 150), -200));	
	}

	public static GameObject createEnemyByType (string type, Vector3 pos)
	{
		GameObject pref = CacheMgr.getEnemyPrb (type) as GameObject;//Resources.Load("enemies/enemy"+type);
		GameObject enemyObj = Instantiate (pref, pos, new Quaternion (0, 0, 0, 0)) as GameObject;
		Enemy enemyDoc = enemyObj.GetComponent<Enemy> ();
		CharacterData characterD = new CharacterData (EnemyDataLib.instance [type] as Hashtable);
		enemyDoc.initData (characterD);
		return enemyObj;
	}

	private void letAllEnemyPlay (string action)
	{
		Character[] chars = FindObjectsOfType (typeof(Character)) as Character[];
		foreach (Character c in chars) {
			c.playAnim (action);
		}	
	}
}
