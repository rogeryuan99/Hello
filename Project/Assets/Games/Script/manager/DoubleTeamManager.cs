using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if !KKK
public class DoubleTeamManager : MonoBehaviour
{
	static public DoubleTeamManager Instance{get; set;}
	
	private List<DoubleTeam> teams = new List<DoubleTeam>();
	
	
	public GameObject doubleTeamPrefab;
	
	void Awake()
	{
		Instance = this;
	}
	
	public void destroySelf()
	{
		for (int i=teams.Count-1; i>=0; i++)
			teams[i].Dismiss();
		
		teams.Clear();
		Instance = null;
		Destroy(gameObject);
	}
	
	public DoubleTeam createDoubleTeam(Hero h1, Hero h2)
	{
		if (!CheckCanCreate(h1, h2)) return null;
		
		GameObject obj = (GameObject)Instantiate(doubleTeamPrefab);
		DoubleTeam team = obj.GetComponent<DoubleTeam>();
		team.AddHeros(h1, h2);
		teams.Add(team);
		
		return team;
	}
	
	public void DeleteDoubleTeam(Hero hero)
	{
		DoubleTeam team = GetDoubleTeam(hero);
		if (null != team){
			teams.Remove(team);
		}
	}
	
	public DoubleTeam GetDoubleTeam(Hero hero)
	{
		foreach(DoubleTeam team in teams)
		{
			if (team.CheckIsHeroExist(hero))
				return team;
		}
		
		return null;
	}
	
	public void FixedUpdate () 
	{
		for (int i=teams.Count-1; i>=0; i--){
			if (null == teams[i]){
				teams.RemoveAt(i);
			}
		}
	}
	
	// Privates
	
	private bool CheckCanCreate(Hero h1, Hero h2)
	{
		foreach(DoubleTeam team in teams)
		{
			if (team.CheckIsHeroExist(h1) 
				|| team.CheckIsHeroExist(h2))
				return false;
		}
		
		return true;
	}
}

#else
public class DoubleTeamManager : MonoBehaviour
{
	static public DoubleTeamManager instance = null;
	
	ArrayList doubleTeamArray = null;
	
	
	public GameObject doubleTeamPrefab;
	
	void Awake()
	{
		instance = this;
		init ();
	}
	
	public void destroySelf()
	{
		foreach(DoubleTeam doubleTeam in doubleTeamArray)
		{
			doubleTeam.heroArray.Clear();
			
		}
		doubleTeamArray.Clear();
		instance = null;
	}
	
	public void init()
	{
		doubleTeamArray = new ArrayList();
	}
	
	
	public void createDoubleTeam(GameObject doubleTeamGameObject)
	{
		doubleTeamArray.Add(doubleTeamGameObject.GetComponent<DoubleTeam>());
	}
	
	public void createDoubleTeam(Hero h1, Hero h2)
	{
		foreach(DoubleTeam doubleTeam in doubleTeamArray)
		{
			if(doubleTeam.heroArray.Count <=1)
			{
				continue;
			}
			Hero h1Temp = (Hero)doubleTeam.heroArray[0];
			Hero h2Temp = (Hero)doubleTeam.heroArray[1];
			if((h1 == h1Temp && h2 == h2Temp) || ((h1 == h2Temp && h2 == h1Temp))) // or hero name or hero id
			{
				return;
			}
		}
		GameObject doubleTeamGameObjectTemp = (GameObject)Instantiate(doubleTeamPrefab);
		DoubleTeam doubleTeamTemp = doubleTeamGameObjectTemp.GetComponent<DoubleTeam>();
		doubleTeamTemp.addHeros(h1, h2);
		doubleTeamArray.Add(doubleTeamTemp);
	}
	
	public void deleteDoubleTeam(DoubleTeam doubleTeam)
	{
		Debug.Log("void deleteDoubleTeam(DoubleTeam doubleTea");
		doubleTeamArray.Remove(doubleTeam);
	}
	
	public void deleteDoubleTeam(Hero h1, Hero h2)
	{
		foreach(DoubleTeam doubleTeam in doubleTeamArray)
		{
			if(doubleTeam.heroArray.Count <=1)
			{
				continue;
			}
			Hero h1Temp = (Hero)doubleTeam.heroArray[0];
			Hero h2Temp = (Hero)doubleTeam.heroArray[1];
			if(h1 == h1Temp && h2 == h2Temp) // or hero name or hero id
			{
				doubleTeamArray.Remove(doubleTeam);
			}
		}
	}
	
	public DoubleTeam getDoubleTeamByIndex(int index)
	{
		if(index >= doubleTeamArray.Count || index < 0)
		{
			return null;
		}
		return (DoubleTeam)doubleTeamArray[index];
	}
	
	public DoubleTeam getDoubleTeamByHeros(Hero h1, Hero h2)
	{
		foreach(DoubleTeam doubleTeam in doubleTeamArray)
		{
			if(doubleTeam.heroArray.Count <=1)
			{
				continue;
			}
			Hero h1Temp = (Hero)doubleTeam.heroArray[0];
			Hero h2Temp = (Hero)doubleTeam.heroArray[1];
			
			if(h1 == h1Temp && h2 == h2Temp) // or hero name or hero id
			{
				return doubleTeam;
			}
		}
		
		return null;
	}
	
	public void addEnemy(Enemy e)
	{
		foreach(DoubleTeam doubleTeam in doubleTeamArray)
		{
			
			doubleTeam.attack(e);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log("update");
		if(doubleTeamArray == null || doubleTeamArray.Count == 0)
		{
			return;
		}
		DoubleTeam deltedt = null;
		foreach(DoubleTeam doubleTeam in doubleTeamArray)
		{
			if(doubleTeam.heroArray.Count <=1)
			{
				continue;
			}
			// 1.Why we don't transfer thease code to DoubleTeam class? : Ask by Funing
			Hero h1Temp = (Hero)doubleTeam.heroArray[0];
			Hero h2Temp = (Hero)doubleTeam.heroArray[1];
			
			float dis = Vector3.Distance(h1Temp.gameObject.transform.localPosition, h2Temp.gameObject.transform.localPosition);
			
			// 2.Why we couldn't define localPosition of doubleTeam anytime? : Ask by Funing
			if(dis <= 160) 
			{
				float x = (h1Temp.gameObject.transform.localPosition.x + h2Temp.gameObject.transform.localPosition.x) / 2.0f;
				float y = (h1Temp.gameObject.transform.localPosition.y + h2Temp.gameObject.transform.localPosition.y) / 2.0f;
				doubleTeam.gameObject.transform.localPosition = new Vector3(x, h1Temp.gameObject.transform.localPosition.y, 0);
				Debug.Log(doubleTeam.e);
				
			}
			// 1.End
			if(doubleTeam.e != null)
			{
				Debug.Log("Attack !!!!!!!!!!!!!!!!!!!!!!");
				
				deltedt = doubleTeam;
				doubleTeam.e = null;
				break;
			}
		}
		if(deltedt != null)
		{
			deleteDoubleTeam(deltedt);
			Destroy(deltedt.gameObject);
		}
		
	}
}

#endif