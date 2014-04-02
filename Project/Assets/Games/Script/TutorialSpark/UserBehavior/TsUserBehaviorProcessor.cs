using UnityEngine;
using System.Collections;

public class TsUserBehaviorProcessor : Singleton<TsUserBehaviorProcessor> {
	
	public delegate void FinishedCallbackDelegate();
	public event FinishedCallbackDelegate OnFinished;
	
	/// <summary>
	/// Waits to click something.
	/// </summary>
	/// <param name='parms'>
	/// Parms Format: {0}ObjName{1}CameraName
	/// </param>
	public void WaitToClickSomething(string[] parms){
		for (int i=0; i<parms.Length; i+=2){
			GameObject obj = TsObjectFactory.GetGameObject(parms[i]);
			if (null == obj) Debug.LogError(string.Format("Object {0} is not exist.\n-Call in function WaitToClickSomething.", parms[i]));
			
			TsUserClickSomething behavior = obj.AddComponent<TsUserClickSomething>();
			behavior.UsingCamera = TsObjectFactory.GetGameObject(parms[i+1]).GetComponent<Camera>();
			behavior.OnFinished += ()=>{
				if (null != OnFinished)
					OnFinished();
			};
		}
	}
	
	public void WaitToClickDownSomethingAndUpLButton(string[] parms){
		for (int i=0; i<parms.Length; i+=2){
			GameObject obj = TsObjectFactory.GetGameObject(parms[i]);
			if (null == obj) Debug.LogError(string.Format("Object {0} is not exist.\n-Call in function WaitToClickDownSomething.", parms[i]));
			
			TsUserClickSomething behavior = obj.AddComponent<TsUserClickSomething>();
			behavior.UsingCamera = TsObjectFactory.GetGameObject(parms[i+1]).GetComponent<Camera>();
			behavior.OnClickUp += ()=>{
				if (null != OnFinished){
					Destroy(behavior);
					OnFinished();
				}
			};
		}
	}
	
	/// <summary>
	/// Waits to move something to something.
	/// </summary>
	/// <param name='parms'>
	/// Parms Format: {0}DragObj{1}ToObj{2}CameraName
	/// </param>
	public void WaitToMoveSomethingToSomething(string[] parms){
		GameObject obj = TsObjectFactory.GetGameObject(parms[0]);
		if (null == obj) Debug.LogError(string.Format("Object {0} is not exist.\n-Call in function WaitToMoveSomethingToSomething.", parms[0]));
		
		TsUserMoveSomething behavior = obj.AddComponent<TsUserMoveSomething>();
		behavior.UsingCamera = TsObjectFactory.GetGameObject(parms[3]).GetComponent<Camera>();
		behavior.TargetObj = TsObjectFactory.GetGameObject(parms[1]);
		behavior.Radius = float.Parse(parms[2]);
		behavior.OnFinished += ()=>{
			if (null != OnFinished){
				OnFinished();
			}
		};
	}
	
	private bool tmpBoolValue = false;
	public void WaitToDragSomethingOnSomething(string[] parms){
		GameObject obj = TsObjectFactory.GetGameObject(parms[0]);
		if (null == obj) Debug.LogError(string.Format("Object {0} is not exist.\n-Call in function WaitToMoveSomethingToSomething.", parms[0]));
		
		TsUserClickSomething obj1Listener = obj.AddComponent<TsUserClickSomething>();
		obj1Listener.UsingCamera = TsObjectFactory.GetGameObject(parms[1]).GetComponent<Camera>();
		
		obj1Listener.OnClickDown = () => { tmpBoolValue = true; };
		obj1Listener.OnClickOutside   = () => { tmpBoolValue = false; };
		
		// -----------------------
		GameObject obj2 = TsObjectFactory.GetGameObject(parms[2]);
		if (null == obj2) Debug.LogError(string.Format("Object {0} is not exist.\n-Call in function WaitToMoveSomethingToSomething.", parms[2]));
		
		TsUserClickSomething obj2Listener = obj2.AddComponent<TsUserClickSomething>();
		obj2Listener.UsingCamera = TsObjectFactory.GetGameObject(parms[3]).GetComponent<Camera>();
		
		obj2Listener.OnClickDown = () => { tmpBoolValue = false; };
		obj2Listener.OnClickUp   = () => {
			if (false == tmpBoolValue){
				return; 
			}
			if (null != OnFinished){
				OnFinished();
			}
			Destroy(obj1Listener);
			Destroy(obj2Listener);
		};
	}
	
	public void WaitCharacterDie(string[] parms){
		GameObject obj = TsObjectFactory.GetGameObject(parms[0]);
		if (null == obj) Debug.LogError(string.Format("Object {0} is not exist.\n-Call in function WaitCharacterDie.", parms[0]));
		Enemy enemy = obj.GetComponent<Enemy>();
		enemy.OnDie+=()=>{
			if (null != OnFinished)
				OnFinished();
		};
	}
	
	public void WaitHeroAttack(string[] parms){
		GameObject obj = TsObjectFactory.GetGameObject(parms[0]);
		if (null == obj) Debug.LogError(string.Format("Object {0} is not exist.\n-Call in function WaitCharacterDie.", parms[0]));
		Hero hero = obj.GetComponent<Hero>();
		
		hero.CallOnAttackAfterAttackCount = int.Parse(parms[1]);
		hero.OnAttack=()=>{
			hero.OnAttack = null;
			if (null != OnFinished)
				OnFinished();
		};
	}
	
	public void WaitToBattleReady(string[] parms){
		BattleBg.Instance.OnBattleReady = () => {
			BattleBg.Instance.OnBattleReady = null;
			if (null != OnFinished)
				OnFinished();
		};
	}
	
	public void WaitToVictoryDlgOpened(string[] parms){
		LevelMgr.Instance.OnVictoryDialogOpened = () => {
			LevelMgr.Instance.OnVictoryDialogOpened = null;
			if (null != OnFinished)
				OnFinished();
		};
	}
	
	public void WaitToEquipArmor(string[] parms){
		TeamDlg.instance.OnEquipArmor = () => {
			TeamDlg.instance.OnEquipArmor = null;
			if (null != OnFinished)
				OnFinished();
		};
	}
	
	public void WaitToTrainingFinished(string[] parms){
		GameObject obj = TsObjectFactory.GetGameObject(parms[0]);
		SkillTreeCell cell = obj.GetComponent<SkillTreeCell>();
		if (cell.learnedData.IsLearned){
			if (null != OnFinished)
				OnFinished();
		}
		else{
			cell.OnFinished += OnFinishedTraining;
		}
	}
	
	private void OnFinishedTraining(SkillTreeCell cell_){
		if (null != OnFinished)
			OnFinished();
		
		cell_.OnFinished -= OnFinishedTraining;
	}
	
	public void Sleep(string[] parms){
		float f = float.Parse((string) parms[0]);
		StartCoroutine(_sleep(f));
	}
	public void LockInput(string[] parms){
		FingerHandler.LockInput = bool.Parse(parms[0]);
		if (null != OnFinished)
				OnFinished();
	}
	
	public void LockObjects(string[] parms){
		for (int i=0; i<parms.Length; i++){
			GameObject obj = TsObjectFactory.GetGameObject(parms[i]);
			if (null != obj && !FingerHandler.LockObjects.Contains(obj))
				FingerHandler.LockObjects.Add(obj);
		}
		if (null != OnFinished)
				OnFinished();
	}
	public void UnlockObjects(string[] parms){
		for (int i=0; i<parms.Length; i++){
			GameObject obj = TsObjectFactory.GetGameObject(parms[i]);
			if (null != obj)
				FingerHandler.LockObjects.Remove(obj);
		}
		if (null != OnFinished)
				OnFinished();
	}
	
	public void LockCombo(string[] parms){
		FingerHandler.LockCombo = true;
		if (null != OnFinished)
				OnFinished();
	}
	public void UnlockCombo(string[] parms){
		FingerHandler.LockCombo = false;
		if (null != OnFinished)
				OnFinished();
	}
	
	public void LockMove(string[] parms){
		FingerHandler.LockMove = true;
		if (null != OnFinished)
				OnFinished();
	}
	public void UnlockMove(string[] parms){
		FingerHandler.LockMove = false;
		if (null != OnFinished)
				OnFinished();
	}
	
	public void LockAttack(string[] parms){
		FingerHandler.LockAttack = true;
		if (null != OnFinished)
				OnFinished();
	}
	public void UnlockAttack(string[] parms){
		FingerHandler.LockAttack = false;
		if (null != OnFinished)
				OnFinished();
	}
	
	private IEnumerator _sleep(float t){
		yield return new WaitForSeconds(t);	
		if (null != OnFinished){
			OnFinished();
		}
	}

}
