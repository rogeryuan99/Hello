using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#if !KKK
public class DoubleTeam : MonoBehaviour
{

	public static DoubleTeam Selected {get; set;}
	private List<Hero> heros = new List<Hero>();
	private const float COUNTDOWN_OF_AUTO_DISMISS = 2f;
	private float countdown = 0f;
	
	public BoxCollider boxCollider = null;
	public Vector3 Position{
		get{
			float x = (heros[0].gameObject.transform.position.x + heros[1].gameObject.transform.position.x) / 2.0f;
			float y = (heros[0].gameObject.transform.position.y + heros[1].gameObject.transform.position.y) / 2.0f;
			
			return new Vector3(x, y, 0);
		}
	}
	
	public delegate void ParmlessHandler();
	public event ParmlessHandler OnDismissed;
	
	// Functions
	
	public void Update(){
		transform.position = Position;
	}
	public void FixedUpdate(){
		if (2 == heros.Count){
			countdown += Time.deltaTime;
			if (countdown > COUNTDOWN_OF_AUTO_DISMISS){
				Dismiss();
			}
		}
	}
	
	public void AddHeros(Hero h1, Hero h2)
	{
		h1.IsInGroup = h2.IsInGroup = true;
		heros.Clear();
		heros.Add(h1);
		heros.Add(h2);
		transform.position = Position;
		CloseToEachOther();
//		heros[0].characterAI.OnGroupAttackFinished += Dismiss;
		heros[0].addHandlerToParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnGroupAttackFinished, Dismiss);
		
	}
	public void Dismiss(Character c = null){
//		heros[0].characterAI.OnGroupAttackFinished -= Dismiss;
		heros[0].removeHandlerFromParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnGroupAttackFinished, Dismiss);
		for (int i=0; i<heros.Count; i++){
			heros[i].IsInGroup = false;
		}
		heros.Clear();
		
		if (null != OnDismissed){
			OnDismissed();
			OnDismissed = null;
		}
			
		Destroy (gameObject);
	}
	public void MoveTo(Vector3 pos){
		if(heros.Count ==0) return;
		
		Vector3 center = GetCenterPoint();
		pos = AdjustTeamTargetPosition(pos, center);
		
		for (int i=0; i<heros.Count; i++){
			heros[i].setTarget(null);
			heros[i].move(GetHeroTargetPosition(heros[i], pos, center));
		}
		countdown = 0;
	}
	
	public void Attack(GameObject obj){
		
		GameObject superComboPref = Resources.Load("SuperCombo/SuperCombo") as GameObject;
		GameObject superCombo = Instantiate(superComboPref) as GameObject;
		GameObject uiroot = GameObject.Find("UIRoot");
		superCombo.transform.parent = uiroot.transform;
		superCombo.transform.localScale = new Vector3(0.001f,0.001f,1f);
		superCombo.transform.localPosition = new Vector3(0,206.1f,0);
		SkillManager.Instance.chanageBGTextureColorByCoroutine(0.0f, 0.3f);
		SkillManager.Instance.pauseMotionByCoroutine(0.0f, 0.3f);
		iTween.ScaleTo(superCombo,new Hashtable(){{"x",501f},{"y",186f},{"delay",0.01f},{"time",.2f},{"easetype",iTween.EaseType.easeOutBounce},{"oncomplete","destroySelf"},{"oncompletetarget",superCombo}});//,{
		
		MusicManager.playEffectMusic("SFX_Drax_Basic_1a");
		
		for (int i=0; i<heros.Count; i++){
			heros[i].setTarget(obj);
//			heros[i].startGroupAtk();
		}
		Dismiss();
		countdown = 0;
	}
	public bool CheckIsHeroExist(Hero hero){
		return (hero.Equals(heros[0]) 
				|| hero.Equals(heros[1]));
	}
	public void ResetCountdown(){
		countdown = 0;
	}
	
	// Privates
	
	private void CloseToEachOther(){
		Vector3 center = GetCenterPoint();
		for (int i=0; i<heros.Count; i++){
			heros[i].setTarget(null);
			heros[i].move(GetHeroTargetPosition(heros[i], center, center));
		}
	}
	
	private Vector3 GetCenterPoint(){
		Vector3 center = Vector3.zero;
		
		for (int i=0; i<heros.Count; i++){
			center += heros[i].transform.position;
		}
		center = center/heros.Count;
		
		return center;
	}
	
	private Vector3 AdjustTeamTargetPosition(Vector3 pos, Vector3 center){
		for (int i=0; i<heros.Count; i++){
			int direction = GetDirection(heros[i],center);
			Vector3 targetPos = GetHeroTargetPosition(heros[i], pos, center);
			
			if (BattleBg.IsXValueOutOfActionBounce(targetPos.x)){
				pos -= new Vector3(100*direction, 0, 0);
			}
		}
		
		return pos;
	}
	
	private Vector3 GetHeroTargetPosition(Hero hero, Vector3 pos, Vector3 center){
		int direction = GetDirection(hero,center);
		Vector3 heroTargetPos = pos + new Vector3(50*direction, 0, 0);
		
		return heroTargetPos;
	}
	
	private int GetDirection(Hero hero, Vector3 center){
		return hero.transform.localPosition.x > center.x? 1: -1;
	}
	
}

#else
public class DoubleTeam : MonoBehaviour
{
	public ArrayList heroArray = null;
	
	public Enemy e = null;
	
	public BoxCollider boxCollider = null;
	
	void Awake()
	{
		heroArray = new ArrayList();
	}
	
	protected void init()
	{
		
	}
	
	public void addHeros(Hero h1, Hero h2)
	{
		heroArray.Add(h1);
		heroArray.Add(h2);
	}
	
	public void attack(Enemy e)
	{
		this.e = e;
	}
}
#endif