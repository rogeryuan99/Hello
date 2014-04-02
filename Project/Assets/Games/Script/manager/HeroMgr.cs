using UnityEngine;
using System.Collections;

public class HeroMgr : MonoBehaviour
{
	private static int id = 0;
	public static Hashtable heroHash = new Hashtable ();

//public static Array heroDataAry = [];

	public static string getID ()
	{
		id++;
		return "Player_" + id;
	}

	public static void clear ()
	{
		heroHash.Clear();
	}

	public static Hero getHeroByID (string id)
	{
		return heroHash [id] as Hero;
	}

	public static Hero getHeroByType (string type)
	{
		foreach (DictionaryEntry tempHero in heroHash) {
			Hero hero = tempHero.Value as Hero;
			if (hero.data.type == type) {
				return hero;
			}
		}
		return null;
	}
	
	public static Hero getHeroByIndex (int index)
	{
		int indexTemp = 0;
		foreach (Hero hero in heroHash.Values) 
		{
			if(indexTemp == index)
			{
				return hero;
			}
			indexTemp++;
		}
		return null;
	}

	public static void delHero (string id)
	{
		heroHash.Remove (id);
	}

	public static Hero getRandomHero (bool notUnderAttack=false)
	{
		int length = heroHash.Count;
		int randomID = (int)(Random.value * length);
		foreach (DictionaryEntry tempHero in heroHash) {
			Hero hero = tempHero.Value as Hero;
			if (notUnderAttack && hero.isUnderAttack () || !hero.isSelfCollider())
				continue;
			if (randomID-- == 0) {
				return hero;
			}
		}
		return null;
	}

	public static Hero getDefMaxHero (bool notUnderAttack=false)
	{
		int def = 0;
		Hero selected = null;
		foreach (DictionaryEntry tempHero in heroHash) {
			Hero hero = tempHero.Value as Hero;
			if (notUnderAttack && hero.isUnderAttack () || !hero.isSelfCollider())
			{
				continue;
			}
			if (hero.realDef.total() > def)
			{
				def = (int)(hero.realDef.total());
				selected = hero;
			}			
		}
		return selected;
	}

	public static Hero getDefMinHero (bool notUnderAttack=false)
	{
		int def;
		def = 9999999;
		Hero selected = null;
		foreach (DictionaryEntry tempHero in heroHash)
		{
			Hero hero = tempHero.Value as Hero;
			if (notUnderAttack && hero.isUnderAttack () || !hero.isSelfCollider())
				continue;
			if (hero.realDef.total() < def) {
				def = (int)(hero.realDef.total());
				selected = hero;
			}
		}
		return selected;
	}
	
	public static Hero getLowestHealthHero(bool notUnderAttack=false)
	{
		int currentHP = 99999999;
		Hero selected = null;
		foreach(DictionaryEntry tempHero in heroHash)
		{
			Hero hero = tempHero.Value as Hero;
			if (notUnderAttack && hero.isUnderAttack () || !hero.isSelfCollider())
			{
				continue;
			}
			if(hero.realHp < currentHP)
			{
				currentHP = hero.realHp;
				selected = hero;
			}
		}
		return selected;
	}
}