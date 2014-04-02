using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public PackedSprite sprite;
	
	public GameObject targetObj;
	public Object hitEffectPrefab;
	
	public int attack;
	
	public enum TARGETTYPE
	{
		NONE,
		HERO, 
		ENEMY
	};
	
	public TARGETTYPE targetType;
	
	// Use this for initialization
	void Start()
	{
		BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
		if(boxCollider == null)
		{
			gameObject.AddComponent<BoxCollider>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(targetType == TARGETTYPE.HERO)
		{
			bulletIntersectsCharacter(HeroMgr.heroHash);
		}
		else if(targetType == TARGETTYPE.ENEMY)
		{
			bulletIntersectsCharacter(EnemyMgr.enemyHash);
		}
	}
	
	protected void bulletIntersectsCharacter(Hashtable characterHashTable)
	{
		
		// foreach(Character character in HeroMgr.heroHash.Values)
		foreach(Character character in characterHashTable.Values)
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, character.gameObject.transform.position.z);
	
			if(gameObject.collider.bounds.Intersects(character.collider.bounds))
			{
				iTween.StopByName(gameObject, "Buttlet_ButtletMoveTo");
				this.removeBullet(character);
				break;
			}
		}
	}
	
	
	public void shootBullet (Vector3 creatVc3, Vector3 endVc3)
	{
		float dis_y = endVc3.y - creatVc3.y;
		float dis_x = endVc3.x - creatVc3.x;
		float angle = Mathf.Atan2(dis_y, dis_x);
		
		float deg = (angle*360)/(2*Mathf.PI);

		transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
		
		iTween.MoveTo(gameObject,new Hashtable(){{"name","Buttlet_ButtletMoveTo"}, {"x",endVc3.x},{ "y",endVc3.y},{ "speed",1500},{ "easetype","linear"},{ 
								"oncomplete","removeBullet"},{"oncompletetarget",gameObject}});
	}
	
	protected void removeBullet ()
	{
		Destroy(gameObject);
	}
	
	protected void removeBullet (Character targetCharacter)
	{
		if(targetCharacter != null)
		{
			targetCharacter.realDamage((int)(attack));
			if (null != hitEffectPrefab)
			{
				GameObject eft = Instantiate(hitEffectPrefab) as GameObject;
				eft.transform.position = transform.position + new Vector3((targetCharacter.transform.position.x < transform.position.x? -100f: 100f), 0f, 0f);
				eft.transform.localScale = transform.localScale;
			}
		}
		Destroy(gameObject);
	}
}
