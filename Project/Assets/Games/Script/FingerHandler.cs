using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FingerHandler {
	public static bool LockInput = false;
	public static bool LockCombo = false;
	public static bool LockMove  = false;
	public static bool LockAttack = false;
	public static List<GameObject> LockObjects = new List<GameObject>();
	public bool skipUpdateOnce = false;
	
	private static FingerHandler _instance;
	private HealMenu healMenu;
	private RaycastHit outHit;
	
	public static FingerHandler Instance{
		get{
			if (null == _instance){
				_instance = new FingerHandler();
			}
			return _instance;
		}
	}
	public static void Release(){
		if( _instance != null){
			_instance.Dispose();
			_instance = null;
		}
	}
	public Hero SelectedHero{
		get{ return Hero.Selected; }
	}
	// Update is called once per frame
	public void Update () {
		if(StaticData.isBattleEnd){
			tempGroup.Clear();
		}
		else 
		if (!StaticData.paused && !LockInput)
		{
			if(Input.GetMouseButtonDown(0)){
				fingerDownHandler(Input.mousePosition);
			}
			
			if(Input.GetMouseButtonUp(0)){
				if (skipUpdateOnce){
					skipUpdateOnce = false;
				}
				else if (null != DoubleTeam.Selected){					
					fingerUpHandlerForTeam(Input.mousePosition);
				}
				else
					fingerUpHandler(Input.mousePosition);  		
			}
			
			if(Input.GetMouseButton(0) && !skipUpdateOnce){
				if (null != DoubleTeam.Selected){
					fingerMoveHandlerForTeam(Input.mousePosition);
					DoubleTeam.Selected.ResetCountdown();
				}
				else
					fingerMoveHandler(Input.mousePosition);
				
			}
		}
		
		/*if(Input.GetMouseButton(0) == false){
			if(sCircel && sCircel.transform.localScale != new Vector3(0,0,0)){
				line.EndDrawing();
				if(sCircel)
					iTween.ScaleTo(sCircel, new Hashtable(){{"scale", new Vector3(0,0,0)},{ "time",0.5f},{"easetype","linear"}});	
			}
		}*/
	}
	
	public void Init(HealMenu menu){
		healMenu = menu;
	}
	public void Dispose(){
		_instance = null;
	}
	
	public bool IsTargetPlayer(Hero hero){
		if (SkillIconManager.TargetType.PLAYER.ToString() == hero.SkillTargetType)
			return true;
		return false;
	}
	
#if !KKK
	IntentionGroup tempGroup  = IntentionGroupResources.Instance.TempGroup;
	public void fingerDownHandler ( Vector3 pos){
		Vector3 vc3 = new Vector3(pos.x,pos.y,StaticData.lineLayer);
		Vector3 worldVc3 = Camera.mainCamera.ScreenToWorldPoint(vc3);
		Ray ray = Camera.main.ScreenPointToRay(pos);
		
		RaycastHit []hitObjs = Physics.RaycastAll(ray);
		if (LockObjects.Count > 0){
			for (int i=0; i<hitObjs.Length; i++){
				if (LockObjects.Contains(hitObjs[i].transform.gameObject)){
					skipUpdateOnce = true;
					return;
				}
			}
		}
		
		for (int i=0; i<hitObjs.Length; i++){
			if ("Group" == hitObjs[i].transform.tag){
				DoubleTeam.Selected = hitObjs[i].transform.GetComponent<DoubleTeam>();
				ColliderManager.Instance.EnlargeEnemyColliders();
				
				tempGroup.Clear();
				tempGroup.AddTarget(MyCircleRenderer.TYPE.GROUP, DoubleTeam.Selected.gameObject);
				return;
			}
		}
		int layerMask = (1<< LayerMask.NameToLayer("Heroes"));
		bool hasHitSomething = Physics.Raycast(ray, out outHit,Mathf.Infinity, layerMask);
		/*if (hasHitSomething 
				&& outHit.transform.tag == "Player"
				&& null != DoubleTeamManager.Instance.GetDoubleTeam(outHit.transform.GetComponent<Hero>())){
			DoubleTeam team = DoubleTeamManager.Instance.GetDoubleTeam(outHit.transform.GetComponent<Hero>());
			if (null != DoubleTeam.Selected && DoubleTeam.Selected.Equals(team)){
			}
			else{
				ColliderManager.Instance.EnlargeEnemyColliders();
				DoubleTeam.Selected = team;
			}
			tempGroup.Clear();
			tempGroup.AddTarget(MyCircleRenderer.TYPE.GROUP, DoubleTeam.Selected.gameObject);
			Hero.Selected = null;
		}
		else*/
		if(hasHitSomething && outHit.transform.tag == "Player")
		{
			//MusicManager.playEffectMusic("SFX_swipe_move_1b");
			BoxCollider heroCollider = outHit.transform.gameObject.collider as BoxCollider;
			// selectHeroCY =  int.Parse(heroCollider.size.y.ToString());
			// selectHeroCX =  int.Parse(heroCollider.center.y.ToString());
			// selectHeroCY =  (int)heroCollider.size.y;
			// selectHeroCX =  (int)heroCollider.center.y;
	
			DoubleTeam.Selected = null;
			Hero.Selected = outHit.transform.gameObject.GetComponent<Hero>();
			Hero.Selected.showHP();
			Hero.Selected.changeStateColor(Character.selectColor);
			if( (Hero.Selected.data as HeroData).type == HeroData.MANTIS)
			{
//				if(!Hero.Selected.data.isDead)
//				{
//					healMenu.initIcons();
//				}
//				(Hero.Selected as Healer).stopMoving();
//				(Hero.Selected as Healer).isDown = true;
//				(Hero.Selected as Healer).hpBar.hideHpBar();
//				healMenu.transform.position 
//						= 
//							outHit.transform.position
//							+ 
//							new Vector3(0,200,-400);
//				
//				healMenu.fingerDownHandler(worldVc3);
//				
				ColliderManager.Instance.EnlargeHeroColliders();//make heal targets easier to select
			}
			else
			{
				// startTouch = worldVc3;
				// line.StartDrawing(worldVc3);
				
				// iTween.Stop(sCircel);
				// sCircel.transform.position = worldVc3;
				// sCircel.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
				
				ColliderManager.Instance.EnlargeEnemyColliders();//make enemies easier to select
			}
			
			if (IsTargetPlayer(Hero.Selected))
			{
				ColliderManager.Instance.EnlargeHeroColliders();
			}
			//heroCollider.size = new Vector3(heroCollider.size.x, heroCollider.size.y * 0.75f, heroCollider.size.z);
	//		heroCollider.size.y *= 0.75f;
				
			//heroCollider.center = new Vector3(heroCollider.center.x, heroCollider.size.y/6, heroCollider.center.z);
			//heroCollider.center.y -= heroCollider.size.y/6;
			tempGroup.Clear();
			tempGroup.AddTarget(MyCircleRenderer.TYPE.PLAYER, Hero.Selected.gameObject);
		}
		else 
		{
			Hero.Selected = null;
		}
	}
	
	
	private void fingerMoveHandler ( Vector3 pos  )
	{	
		if (null == Hero.Selected) return;
		
		Vector3 worldVc3 = Camera.mainCamera.ScreenToWorldPoint(pos);
		// isDrag = true;
		
		if(Hero.Selected.data.type == HeroData.MANTIS)
		{		
			ColliderManager.Instance.EnlargeHeroColliders();
			ColliderManager.Instance.ShrinkEnemyColliders();
//			healMenu.fingerMoveHandler(worldVc3);
		}
		 
		Ray ray = Camera.main.ScreenPointToRay(pos);
		Character character = (Physics.Raycast(ray,out outHit))? 
								outHit.transform.gameObject.GetComponent<Character>(): null;
		if(null != character 
			&& outHit.transform.tag == Hero.Selected.getTargetTagType()
			&& false == BattleBg.IsOutOfFingerBounce(outHit.transform.position))
		{
			if(Hero.Selected.data.type == HeroData.MANTIS)
			{
				// line.SetWidth(0, 0);
				// sCircel.SetActiveRecursively(false);
//				tempGroup.EndDrawing();
				tempGroup.AddTarget(MyCircleRenderer.TYPE.PLAYER, character.gameObject);		
//				(Hero.Selected as Mantis).startHeal(outHit.transform.gameObject);
			}
			else
			{
				// Wait to Code : Funing
				// line.SetColors(Color.red,Color.red);
				// line.SetWidth(360,360);
				
				// selectObj = outHit.transform.gameObject;
				
				// targetIndtr.show( (Hero.Selected.data as HeroData).type, (character.data).type );
				
				// Wait to Code : Fuing
				// if(sCircel.transform.localScale.x == 0.8f){
				// 	iTween.ScaleTo(sCircel, new Hashtable(){{"scale",new Vector3(1, 1, 1)}, {"time",0.2f},{"easetype","linear"}});
				// }	
//				if(Hero.Selected.data.type == HeroData.HEALER)
//				{
//					healMenu.glowIconByType(character.data.type);
				// }		
				tempGroup.AddTarget(MyCircleRenderer.TYPE.ENEMY, character.gameObject);		
			}
		}
		/*else
		if (null != character 
			&& false == character.Equals((Character)Hero.Selected)
			&& false == BattleBg.IsOutOfFingerBounce(outHit.transform.position)
			&& MyCircleRenderer.TYPE.PLAYER.ToString() == outHit.transform.tag.ToUpper()
			&& false == LockCombo){
			tempGroup.AddTarget(MyCircleRenderer.TYPE.PLAYER, character.gameObject);		
			if (!string.IsNullOrEmpty(Hero.Selected.getSkIdCanCastFromContainer(character.tag.ToUpper()))){
				Hero.Selected.setTarget(character.gameObject);
			}
			else 
			if (IntentionGroup.TYPE.P2P == tempGroup.Type){
				DoubleTeam team = DoubleTeamManager.Instance.createDoubleTeam(Hero.Selected, outHit.transform.gameObject.GetComponent<Hero>());
				if (null != team){
					DoubleTeam.Selected = team;
					DoubleTeam.Selected.OnDismissed += OnGroupDismissed;
					SkillIconManager.Instance.HideSkillIcon();
				}
			}
		}*/
		else if (null != character 
				&& IsTargetPlayer(Hero.Selected)
				&& outHit.transform.tag.ToUpper() == Hero.Selected.SkillTargetType
				&& false == BattleBg.IsOutOfFingerBounce(outHit.transform.position))
		{
			
			tempGroup.AddTarget(MyCircleRenderer.TYPE.PLAYER, character.gameObject);		
		}
		else
		{
			if(healMenu!=null && healMenu.selectIco && Hero.Selected.data.type == HeroData.HEALER)
			{ 
				// line.SetWidth(0, 0);
				// sCircel.SetActiveRecursively(false);
				tempGroup.EndDrawing();
			}
			else
			{
				// line.SetWidth(180,180);
				if (null == character || outHit.transform.tag.ToUpper() != MyCircleRenderer.TYPE.PLAYER.ToString())
				{
					
					tempGroup.AddTarget(MyCircleRenderer.TYPE.FINGER);
					
					// targetIndtr.hide();
				}
			}
			
			// if(selectObj != null)
			// 	selectObj = null;
			// Wait to Code : Funing
			// line.SetColors(Color.green,Color.green);
			// if(sCircel.transform.localScale.x == 1 || sCircel.transform.localScale.x == 0){
			// 	sCircel.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			// }
		}
	}
	private void fingerMoveHandlerForTeam(Vector3 pos)
	{
		Character character = GetRaycastCharacter(pos, "ENEMY");
		if (null != character)
		{
			tempGroup.AddTarget((MyCircleRenderer.TYPE)Enum.Parse(typeof(MyCircleRenderer.TYPE), "ENEMY"), 
									character.gameObject);
		}
		else
		{
			tempGroup.AddTarget(MyCircleRenderer.TYPE.FINGER);
		}
	}
	private Character GetRaycastCharacter(Vector3 pos, string type){
		Ray ray = Camera.main.ScreenPointToRay(pos);
		RaycastHit []hitObjs = Physics.RaycastAll(ray);
		
		foreach (RaycastHit hitObj in hitObjs){
			Character character = hitObj.transform.gameObject.GetComponent<Character>();
			
			if (null != character
				&& hitObj.transform.tag.ToUpper() == type
				&& false == BattleBg.IsOutOfFingerBounce(hitObj.transform.position)){
				
				return character;
			}
		}
		return null;
	}
	private void OnGroupDismissed(){
		DoubleTeam.Selected = null;
		Hero.Selected = null;
		
		tempGroup.Clear();
	}
	
	
	private void fingerUpHandler ( Vector3 pos  ){ 
		Vector3 worldVc3_end= Camera.mainCamera.ScreenToWorldPoint(pos);
		// targetIndtr.hide();
		
		if(Hero.Selected != null)
		{
			BoxCollider heroCollider = Hero.Selected.gameObject.collider as BoxCollider;
			//heroCollider.size = new Vector3(heroCollider.size.x, selectHeroCY, heroCollider.size.z);
	//		heroCollider.size.y = selectHeroCY; 
			//heroCollider.center = new Vector3(heroCollider.center.x, selectHeroCX, heroCollider.center.x);	
	//		heroCollider.center.y = selectHeroCX;
		 
			// Wait to Code : Funing
			// line.EndDrawing();
			// if(sCircel)
			// 	iTween.ScaleTo(sCircel, new Hashtable(){{"scale", new Vector3(0,0,0)},{ "time",0.5f},{"easetype","linear"}});
			tempGroup.EndDrawing();
				
	//		Rect rect_end = get2DRect(Hero.Selected.gameObject);
	//		print(!isDrag);
	//		Character character = selectObj.GetComponent<Character>();
	//		if(character.getIsDead())return;
			Ray rayPos = Camera.main.ScreenPointToRay(pos);
			if( Physics.Raycast(rayPos,out outHit) )
			{  
				/*if(selectObj != null)
				{ 
					Character character = selectObj.GetComponent<Character>();
					selectObj = null;
				}*/
				
				if(outHit.transform.tag == "Player")
				{ 
					// Heal Hero or Skill on Hero
					if(Hero.Selected.gameObject == outHit.transform.gameObject)
					{
						Hero.Selected.selecting();
						BattleObjects.skHero    = Hero.Selected;
						
						SkillIconManager.Instance.ChangeSkillDisplay();
//						ComboController.Instance.ChangeComboDisplay();
	//					HeroData heroD = BattleObjects.skHero.data;
	//					ConsoleObj.instance.showInfo("Lv:"+heroD.lv+" BaseAtk:"+heroD.attack+" BonusAtk:"+
	//												heroD.eAttack+" BaseDef:"+heroD.defense+" BonusDef:"+heroD.eDefense);
	//					 BattleObjects.skHero.showLightCircel();
						
						
					}
					else if (IsTargetPlayer(Hero.Selected) && !string.IsNullOrEmpty(Hero.Selected.getSkIdCanCastFromContainer()))
					{
						
						Hero.Selected.setTarget( outHit.transform.gameObject );
						SkillIconManager.Instance.CastSkill(Hero.Selected);
					}
					else
					{
						if(Hero.Selected.isDead)
						{
							return;
						}
						// if (!string.IsNullOrEmpty(Hero.Selected.getSkIdCanCastFromContainer())){ 
						// 	SkillIconManager.Instance.CastSkill(Hero.Selected);
						// }
						// else 
						if (Hero.Selected is Mantis)
						{
							(Hero.Selected as Mantis).startHeal(outHit.transform.gameObject);
						}
					}
				}
				else if(outHit.transform.tag == "Enemy")
				{ 
					if(Hero.Selected.data.type == HeroData.MANTIS)
					{
						return;
					}
					// Attack
					if(Hero.Selected.getTargetTagType() != "Enemy")
					{
						ColliderManager.Instance.ShrinkHeroColliders();
						ColliderManager.Instance.ShrinkEnemyColliders();
						return;
					}
					GameObject targetObj1 = outHit.transform.gameObject;//Hero.Selected.getTarget();
					Hero.Selected.setTarget( targetObj1 );
					if(Hero.Selected.isDead)
					{
						ColliderManager.Instance.ShrinkHeroColliders();
						ColliderManager.Instance.ShrinkEnemyColliders();
						return;
					}
					
					//add by xiaoyong 20120426  
					//for When you drag a character to a new target or location, that character should automatically be selected.  
					BattleObjects.skHero = Hero.Selected;
					SkillIconManager.Instance.ChangeSkillDisplay();
//					ComboController.Instance.ChangeComboDisplay();
					// BattleObjects.skHero.showLightCircel();
					//////////
					if (!string.IsNullOrEmpty(Hero.Selected.getSkIdCanCastFromContainer()) && !IsTargetPlayer(Hero.Selected))
					{ 
						SkillIconManager.Instance.CastSkill(Hero.Selected);
					}
					else
					{
						if (!LockAttack)
						{
							//MusicManager.playMoveMusic("SFX_character_jog_2a");
							Hero.Selected.moveToTarget(targetObj1);
						}
						else
						{
							Hero.Selected.setTarget(null);
						}
					}
				}
				else if(outHit.transform.tag == "Background")
				{ 
					MoveHeroOnFingerUp(worldVc3_end);
				}
			}
			else
			{
				if (null != Hero.Selected)
				{
					MoveHeroOnFingerUp(BattleBg.CorrectingEndPointToActionBounds(worldVc3_end));
				}
			}
			if (null != Hero.Selected)
			{
				BattleObjects.skHero = Hero.Selected;
				SkillIconManager.Instance.ChangeSkillDisplay();
			}
			
			ColliderManager.Instance.ShrinkHeroColliders();
			ColliderManager.Instance.ShrinkEnemyColliders();   
		}
	}
	private void MoveHeroOnFingerUp(Vector3 pos)
	{
		if(Hero.Selected.isDead)
		{
			Debug.Log("Hero.Selected.getIsDead()");
			ColliderManager.Instance.ShrinkHeroColliders();
			ColliderManager.Instance.ShrinkEnemyColliders(); 
			return;
		}
		if (LockMove) return;
		//MusicManager.Instance.playMoveMusic((Hero.Selected.data as HeroData).type);
		Hero.Selected.setTarget( null );
		Hero.Selected.move(pos);
		//add by xiaoyong 20120426  
		//for When you drag a character to a new target or location, that character should automatically be selected.  
		BattleObjects.skHero = Hero.Selected;
		SkillIconManager.Instance.ChangeSkillDisplay();
//		ComboController.Instance.ChangeComboDisplay();
		// BattleObjects.skHero.showLightCircel();
	}

	private void fingerUpHandlerForTeam(Vector3 pos)
	{
		tempGroup.Clear();
		tempGroup.AddTarget(MyCircleRenderer.TYPE.GROUP, DoubleTeam.Selected.gameObject);
		
		Ray ray = Camera.main.ScreenPointToRay(pos);
		Character character = (Physics.Raycast(ray,out outHit))? 
								outHit.transform.gameObject.GetComponent<Character>(): null;
		
		if (null != character
				&& outHit.transform.tag.ToUpper() == "ENEMY"
				&& !LockAttack){
			DoubleTeam.Selected.Attack(character.gameObject);
		}
		else{
			if (LockMove) return;
			DoubleTeam.Selected.MoveTo(
				BattleBg.CorrectingEndPointToActionBounds(
					Camera.mainCamera.ScreenToWorldPoint(pos)));
		}
	}
}
#endif