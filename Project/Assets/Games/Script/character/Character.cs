using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
	public GameObject model;
	
	public string enemyType;
	
	public ArrayList skContainer = new ArrayList();
	
	public HPBar hpBar;
	
	public CharacterData data;
	
	public GameObject slashEft;
	public GameObject harmEftPrb;
	
	public int realHp;
	public int realMaxHp;
	
	public Vector6 realAtk = new Vector6();
	public Vector6 realDef = new Vector6();
	
	public float realMspd;
	public float realAspd;
	
	// delete by why 2014.2.7
//	public float realStk;
//	public float realEvade;
//	public float realCStk;
	
	public Vector3 scaleSize = new Vector3 (0.8f, 0.8f, 1);
	
	public ArrayList atkPosAry;
	
	public Vector3 outOfControlPos = Vector3.zero;
	
	public static Color damageColor = new Color32(255, 128, 128, 255);
	public static Color beHealColor = new Color32(128, 255, 128, 255);
	public static Color selectColor = new Color32(255, 255, 255, 255);
	public static Color normalColor = new Color32(128, 128, 128, 255);
	public static Color freezeColor = new Color32(128, 128, 230, 255);
	public Color currentColor = normalColor;
	
	public List<Vector2> movePath = new List<Vector2>();
	
	public delegate void ParmlessHandlerByParam(Character c);
	
	public bool isAtkSameTag = false;
	public bool isNebula25Buff = false;
	public bool isSkunge30Buff = false;
	public bool isOnceMusic = true;
	
	public string attackAnimaName = "Attack";
	
	public delegate int ConcentrateFire(Character c);
	public ConcentrateFire addConcentrateFireDelegate;
	
	public delegate int CruelCuts(Character c);
	public CruelCuts addCruelCutsDelegate;
	
	public string SkillTargetType{
		get{
			if (skContainer.Count > 0)
				return SkillLib.instance.getSkillDefBySkillID((string)skContainer[0]).target;
			return string.Empty;
		}
	}
	
	public Vector6 displayAtk{
		get{
			Vector6 temp = new Vector6().Add(this.realAtk).Div(0.3f);	
			return temp;
		}
	}
	
	public Vector3 OutOfControlPos
	{
		get 
		{
			if (this.isActionStateActive(ActionStateIndex.OutOfControlMoveState))
			{
				return outOfControlPos;
			}
			return transform.position;
		}
		set 
		{
			outOfControlPos = value;
		}
	}
	
	public static string DefaultActionStates = "11110";
	public static string AllActionNotActiveStates = "00000";
	
	public string currentActionStates = DefaultActionStates;

	
	public enum ABNORMAL_NUM
	{
		NORMAL,		//	1|1|1|1
		TWINE,		//	1|0|0|0
		TWIST,		//	1|0|0|1
		FIRE,		//	0|0|1|0
		FEAR, 		//	0|0|1|1
		LAYDOWN,	//	0|0|0|0
		FREEZE,		//	0|1|0|0
		STUN,		//	0|0|0|0
		CREAZY		//	0|0|1|0
	};
	
//	public List<UInt32> abnormalStates = new List<UInt32>
//	{
//		Convert.ToUInt32("1111", 2),
//		Convert.ToUInt32("1000", 2),
//		Convert.ToUInt32("1001", 2),
//		Convert.ToUInt32("0010", 2),
//		Convert.ToUInt32("0011", 2),
//		0,
//		Convert.ToUInt32("0100", 2),
//		0,
//		Convert.ToUInt32("0010", 2)
//	};
	
	protected Hashtable abnormalStateTable = null;
	
	public bool isDead { get; set; }

	private string _state;

	public string state {
		get{ return _state;}
		set { 
//			Debug.Log(this.gameObject + " setState "+ value);
			
			this._state = value;
		}
	}

//	public ABNORMAL_NUM abnormalState;
	
	protected GameObject cstkAnimPrb;//load from resourceload
	public string id;
	
	public Vector3 dirVc3;
	
	public Vector3 targetPt;
	
	protected GameObject _targetObj = null;
	
	protected Point barrierPos = Point.Invalid;
	
	protected delegate void BarrierMapDelegate();
	
	protected event BarrierMapDelegate OnMovingOnBarrierMap;
	
	public GameObject targetObj
	{
		get
		{
			
			return _targetObj;
		}
		set{
			if (null != _targetObj)
			{
				_targetObj.GetComponent<Character>().OnMovingOnBarrierMap -= UpdateBarrierPathToTarget;
			}
			_targetObj = value;
			if (null != _targetObj)
			{
				_targetObj.GetComponent<Character>().OnMovingOnBarrierMap += UpdateBarrierPathToTarget;
			}
		}
	}
	
	public bool isPlayAtkAnim = false;
	protected int atkAnimKeyFrame;
	protected int atkerAmount = 0;
	
//	protected HPBar hpBarCom;
	
	public static string MOVE_STATE = "MOVE_STATE";
	public static string MOVE_TARGET_STATE = "MOVE_TARGET_STATE";
	public static string MOVE_TARGET_DIRECTLY_STATE = "MOVE_TARGET_DIRECTLY_STATE";
	public static string STANDBY_STATE = "STANDBY_STATE";
	public static string ATK_STATE = "ATK_STATE";
	public static string CAST_STATE = "CAST_STATE";
	public static string DEAD_STATE = "DEAD_STATE";
	
	//protected string LAY_STATE = "LAY_STATE";
	public Hashtable buffHash = new Hashtable ();
	protected ArrayList expiredBuff = new ArrayList ();
	public bool  isEnlarged = false;
	public PieceAnimation pieceAnima; 
	
	public enum HurtBeforeState
	{
		NONE,
		NOTHURT,
		HURT
	}
	
	public enum LossTargetBeforeState
	{
		NONE,
		LOSSTARGET,
	}
	
	public HurtBeforeState hurtBeforeState = HurtBeforeState.NONE;
	
	public LossTargetBeforeState lossTargetBeforeState = LossTargetBeforeState.NONE;
	
	public CharacterAI characterAI;
	
	public virtual void Awake ()
	{
		atkPosAry = new ArrayList (){"0","0","0","0","0","0","0","0"};
		model.transform.localScale = scaleSize;
		setPieceAnima ();
//		print (abnormalState);
		
//		hpBarCom   = hpBar.GetComponent<HPBar>();
	}
	
	public virtual void Start ()
	{
	}
	
	
	public enum ActionStateIndex
	{
		AttackState = 0,
		CallSkillState,
		MoveState,
		DamageActionState,
		OutOfControlMoveState
	}
	
	public void initAbnormalStateTable()
	{
		abnormalStateTable = new Hashtable()
		{
			{ABNORMAL_NUM.TWINE.ToString(), new AbnormalState("10000", 0, twineWithSeconds, twineToNormal)},
			{ABNORMAL_NUM.TWIST.ToString(), new AbnormalState("10010", 0, twistWithSeconds, twistToNormal)},
			{ABNORMAL_NUM.FIRE.ToString(), new AbnormalState("00101", 0, fireWithSeconds, cancelFire)},		//	0|0|1|0
			{ABNORMAL_NUM.FEAR.ToString(), new AbnormalState("00111", 0, fearWithSeconds, cancelFear)}, 		//	0|0|1|1
			{ABNORMAL_NUM.LAYDOWN.ToString(), new AbnormalState(AllActionNotActiveStates, 0, layDownWithSeconds, cancelLayDown)},	//	0|0|0|0
			{ABNORMAL_NUM.FREEZE.ToString(), new AbnormalState("01000", 0, freezeWithSeconds, freezeToNormal)},		//	0|1|0|0
			{ABNORMAL_NUM.STUN.ToString(), new AbnormalState(AllActionNotActiveStates, 0, stunWithSeconds, stunToNormal)},		//	0|0|0|0
			{ABNORMAL_NUM.CREAZY.ToString(), new AbnormalState(DefaultActionStates, 0, null, null)}		//	0|0|1|0
		};
		
//		abnormalStateTable[ABNORMAL_NUM.TWINE.ToString()] =  new AbnormalState("1000", 0, twineWithSeconds, twineToNormal);

	}
	
	public virtual void getCharacterAI()
	{
		this.characterAI = gameObject.GetComponent<CharacterAI>();
	}
	
	public virtual void setCharacterAI(Type aiType)
	{
		gameObject.AddComponent(aiType);
	}

	//gwp
	public virtual void setPieceAnima ()
	{
		pieceAnima = model.GetComponent<PieceAnimation> ();
	}
	
	public virtual void initData (CharacterData characterD)
	{
		data = characterD;
		realHp = characterD.maxHp;
		realDef = characterD.defense.clone();
		realAtk = characterD.attack.clone();
		realMspd = characterD.moveSpeed;
		realAspd = characterD.attackSpeed;
		
		// delete by why 2014.2.7		
//		realCStk = characterD.criticalStk / 100.0f;
//		realEvade = DataModifier.EVADE_VALUE * characterD.evade;
//		realStk = DataModifier.STK_VALUE * characterD.strike;
		hpBar.initBar (characterD.maxHp);
	}
	
	public virtual void fearWithSeconds ()
	{
		this.OutOfControlWithSeconds();
	}
	
	public virtual void cancelFear ()
	{
		this.ClearOutOfControl();
	}
	
	public virtual void fireWithSeconds ()
	{
		this.OutOfControlWithSeconds();	
	}
	
	public virtual void cancelFire ()
	{
		this.executeParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj);
		this.ClearOutOfControl();
	}
	
	public virtual void twineWithSeconds()
	{
//		this.setAbnormalState (Character.ABNORMAL_NUM.TWINE);
//		Invoke("twineToNormal",time);
	}
	
	public virtual void twineToNormal()
	{
//		this.executeParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj);
//		this.setAbnormalState(Character.ABNORMAL_NUM.NORMAL);
		if (this.isDead)
		{
			return;
		}
		standby();
	}
	
	public virtual void twistWithSeconds()
	{
//		this.setAbnormalState(Character.ABNORMAL_NUM.TWIST);
//		Invoke("twistToNormal",time);
	}
	
	public virtual void twistToNormal ()
	{
//		this.executeParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj);
//		this.setAbnormalState(Character.ABNORMAL_NUM.NORMAL);
		if (this.isDead)
		{
			return;
		}
		standby();
	}
	
	public virtual void stunWithSeconds()
	{
//		this.standby();
//		targetObj = null;
		this.playAnim("Stand");
		pieceAnima.pauseAnima();
	}
	
	public virtual void stunToNormal()
	{
		if (this.isDead)
		{
			return;
		}
		pieceAnima.restart();
		
//		this.executeParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj);
	
		this.playAnim("Move");
		standby();
	}
	
	public virtual void freezeWithSeconds()
	{
//		this.setAbnormalState(Character.ABNORMAL_NUM.FREEZE);
		model.renderer.material.color = freezeColor;
		currentColor = freezeColor;
	}
	
	public virtual void freezeToNormal ()
	{
		currentColor = normalColor;
		model.renderer.material.color = currentColor;
		if (this.isDead)
		{
			return;
		}
//		this.executeParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj);
		standby();
	
	}
	
	public virtual void layDownWithSeconds()
	{
		if(this.isDead)
		{
			return;
		}
		
//		this.setAbnormalState(Character.ABNORMAL_NUM.LAYDOWN);
		
		if (this.state == Character.ATK_STATE)
		{
			this.cancelAtk ();
		}
		this.playAnim("Death");
		
//		Invoke("cancelLayDown", time);
	
	}
	
	public virtual void cancelLayDown ()
	{
		if (getIsDead())
			return; 
		this.pieceAnima.restart();
		
		
//		this.setAbnormalState(Character.ABNORMAL_NUM.NORMAL);
		standby();
	}
		
	public virtual void OutOfControlWithSeconds()
	{		
		if(this.getIsDead() || this.isAbnormalStateActive(ABNORMAL_NUM.LAYDOWN))
		{
			return;
		}
		
		this.OutOfControlPos = transform.position;
		this.targetObj = null;
		
		
		
		if(this.state == Character.ATK_STATE)
		{
			this.cancelAtk();
		}
		this.state = Character.STANDBY_STATE;
		this.doAnim("Stand");
		
		this.doubleSpeed ();
		move (BattleBg.getPointInAround (OutOfControlPos));
		
	}
	
	public virtual void ClearOutOfControl()
	{
		if (this.isDead)
		{
			return;
		}
		this.OutOfControlPos = Vector3.zero;
		this.normalSpeed();
		standby();
	}
	
	protected string checkActionState()
	{
		string tempActionStates = DefaultActionStates;
		
		foreach(AbnormalState abnormalState in this.abnormalStateTable.Values)
		{
			if(abnormalState.states.Count == 0)
			{
				continue;
			}
						
			if(!abnormalState.isActionStateActive(ActionStateIndex.AttackState))
			{			
				tempActionStates = StaticData.replaceStringByIndex(tempActionStates, '0', (int)ActionStateIndex.AttackState);
			}
			if(!abnormalState.isActionStateActive(ActionStateIndex.CallSkillState))
			{
				tempActionStates = StaticData.replaceStringByIndex(tempActionStates, '0', (int)ActionStateIndex.CallSkillState);
			}
			if(!abnormalState.isActionStateActive(ActionStateIndex.MoveState))
			{
				tempActionStates = StaticData.replaceStringByIndex(tempActionStates, '0', (int)ActionStateIndex.MoveState);
			}
			if(!abnormalState.isActionStateActive(ActionStateIndex.DamageActionState))
			{
				tempActionStates = StaticData.replaceStringByIndex(tempActionStates, '0', (int)ActionStateIndex.DamageActionState);
			}
			if(abnormalState.isActionStateActive(ActionStateIndex.OutOfControlMoveState))
			{
				tempActionStates = StaticData.replaceStringByIndex(tempActionStates, '1', (int)ActionStateIndex.OutOfControlMoveState);
			}
			
			if(this.currentActionStates == AllActionNotActiveStates)
			{
				return AllActionNotActiveStates;
			}
		}
		return tempActionStates;
	}

	public bool isActionStateActive(Character.ActionStateIndex actionStateIndex)
	{
		return this.currentActionStates[(int)actionStateIndex] == '1';
	}
	
	public bool isAbnormalStateActive(ABNORMAL_NUM abnormal_num)
	{
		AbnormalState abnormalState = this.abnormalStateTable[abnormal_num.ToString()] as AbnormalState;
		if(abnormalState == null)
		{
			return false;
		}
		return abnormalState.states.Count > 0;
	}
	
//	public bool isActionState(Character.ActionStateIndex actionStateIndex)
//	{
//		return ((this.currentActionStates >> (int)actionStateIndex ) & 1) == 1;
//	}
	
	public void addAbnormalState(int durationTime, State.StateFinish stateFinishCallback, ABNORMAL_NUM abnormal_num)
	{
		this.addAbnormalState(new State(durationTime, stateFinishCallback), abnormal_num);
	}
	
	public void addAbnormalState(State state, ABNORMAL_NUM abnormal_num)
	{
		AbnormalState abnormalState = abnormalStateTable[abnormal_num.ToString()] as AbnormalState;
		state.endTime += Time.time; 
		abnormalState.addState(state);
		
		Debug.LogError("addAbnormalState start : " +  abnormal_num.ToString() + "end time " + state.endTime);
		
		if(abnormalState.states.Count == 1)
		{
			if(abnormalState.stateStartCallback != null)
			{
				abnormalState.stateStartCallback();
			}
			this.currentActionStates = checkActionState();
		}
		
		if (!IsInvoking ("abnormalStateTimer"))
		{
			InvokeRepeating ("abnormalStateTimer", 0, 1);
		}
	}
	
	protected void abnormalStateTimer ()
	{
		foreach (string key in this.abnormalStateTable.Keys)
		{
			AbnormalState abnormalState = abnormalStateTable[key] as AbnormalState;
			
			if(abnormalState.states.Count == 0)
			{
				continue;
			}
			
			abnormalState.checkTime(Time.time, this);
			
			if(abnormalState.states.Count <= 0)
			{
				Debug.LogError("addAbnormalState end : " + key.ToString() + " time: "+ Time.time);
				
				if(abnormalState.stateEndCallback != null)
				{
					abnormalState.stateEndCallback();
				}
				
				this.currentActionStates = checkActionState();
				
				if(this.currentActionStates == DefaultActionStates)
				{
					startCheckOpponent();
				}
			}
		}
		
	}
	
	public void clearAbormalState()
	{
		CancelInvoke ("abnormalStateTimer");
		foreach (AbnormalState abnormalState in this.abnormalStateTable.Values)
		{
//			AbnormalState abnormalState = abnormalStateTable[key] as AbnormalState;
			
			abnormalState.clear(this);

		}
		
		this.currentActionStates = DefaultActionStates;
	}
	
	public virtual void addBuff (string skName, int sec, float values, string buffType, Buff.BuffFinish buffFinish = null)
	{
		Debug.Log ("addBuff  " + skName + " values " + values);
		float tempValue;
		
		if (!buffHash.Contains (skName)) 
		{
			tempValue = values;
			buffHash [skName] = new Buff(Time.time + sec, values, buffType, skName, buffFinish);
		} 
		else
		{
			Buff buff = buffHash [skName] as Buff;
			tempValue = values - buff.val;
			buff.endTime += sec;
		}
		
		switch (buffType) 
		{
		case BuffTypes.ATK_PHY:
			realAtk.PHY += (int)tempValue;
			break;
		case BuffTypes.ATK_IMP:
			realAtk.IMP += (int)tempValue;
			break;
		case BuffTypes.ATK_PSY:
			realAtk.PSY += (int)tempValue;
			break;
		case BuffTypes.ATK_EXP:
			realAtk.EXP += (int)tempValue;
			break;
		case BuffTypes.ATK_ENG:
			realAtk.ENG += (int)tempValue;
			break;
		case BuffTypes.ATK_MAG:
			realAtk.MAG += (int)tempValue;
			break;
		
		case BuffTypes.DE_ATK_PHY:
			realAtk.PHY -= (int)tempValue;
			break;
		case BuffTypes.DE_ATK_IMP:
			realAtk.IMP -= (int)tempValue;
			break;
		case BuffTypes.DE_ATK_PSY:
			realAtk.PSY -= (int)tempValue;
			break;
		case BuffTypes.DE_ATK_EXP:
			realAtk.EXP -= (int)tempValue;
			break;
		case BuffTypes.DE_ATK_ENG:
			realAtk.ENG -= (int)tempValue;
			break;
		case BuffTypes.DE_ATK_MAG:
			realAtk.MAG -= (int)tempValue;
			break;
			
		case BuffTypes.DEF_PHY:
			realDef.PHY += (int)tempValue;
			break;
		case BuffTypes.DEF_IMP:
			realDef.IMP += (int)tempValue;
			break;
		case BuffTypes.DEF_PSY:
			realDef.PSY += (int)tempValue;
			break;
		case BuffTypes.DEF_EXP:
			realDef.EXP += (int)tempValue;
			break;
		case BuffTypes.DEF_ENG:
			realDef.ENG += (int)tempValue;
			break;
		case BuffTypes.DEF_MAG:
			realDef.MAG += (int)tempValue;
			break;
		case BuffTypes.DE_DEF_PHY:
			realDef.PHY -= (int)tempValue;
			break;
		case BuffTypes.DE_DEF_IMP:
			realDef.IMP -= (int)tempValue;
			break;
		case BuffTypes.DE_DEF_PSY:
			realDef.PSY -= (int)tempValue;
			break;
		case BuffTypes.DE_DEF_EXP:
			realDef.EXP -= (int)tempValue;
			break;
		case BuffTypes.DE_DEF_ENG:
			realDef.ENG -= (int)tempValue;
			break;
		case BuffTypes.DE_DEF_MAG:
			realDef.MAG -= (int)tempValue;
			break;
		case BuffTypes.HP:
			addHp((int)tempValue);
			break;
		case BuffTypes.DE_HP:
			realDamage((int)tempValue);
			break;
		case BuffTypes.MSPD:
			if (tempValue >= 0)
			{
				changeMoveSpd (true, tempValue);
			}
			else
			{
				changeMoveSpd (false, -tempValue);
			}
			break;
		case BuffTypes.ASPD:
			if (tempValue >= 0) {
				changeAtkSpd (true, tempValue);
			} else {
				changeAtkSpd (false, -tempValue);
			}
			break;
		}
		
		
		if (!IsInvoking ("buffTimer")) {
			InvokeRepeating ("buffTimer", 0, 1);
		}
	}

	private void buffTimer ()
	{
		foreach (string key in buffHash.Keys)
		{
			Buff buff = buffHash [key] as Buff;
			
			switch (buff.type)
			{
				case BuffTypes.HP:
					addHp((int)buff.val);
					break;
				case BuffTypes.DE_HP:
					realDamage((int)buff.val);
					break;
			}
			
			if (Time.time >= buff.endTime)
			{
				//delete 
				expiredBuff.Add (key);
				
				if(buff.buffFinish != null)
				{
					buff.buffFinish(this, buff);
				}
				switch (buff.type)
				{
				case BuffTypes.ATK_PHY:
					realAtk.PHY -= (int)buff.val;
					break;
				case BuffTypes.ATK_IMP:
					realAtk.IMP -= (int)buff.val;
					break;
				case BuffTypes.ATK_PSY:
					realAtk.PSY -= (int)buff.val;
					break;
				case BuffTypes.ATK_EXP:
					realAtk.EXP -= (int)buff.val;
					break;
				case BuffTypes.ATK_ENG:
					realAtk.ENG -= (int)buff.val;
					break;
				case BuffTypes.ATK_MAG:
					realAtk.MAG -= (int)buff.val;
					break;
				case BuffTypes.DEF_PHY:
					realDef.PHY -= (int)buff.val;
					break;
				case BuffTypes.DEF_IMP:
					realDef.IMP -= (int)buff.val;
					break;
				case BuffTypes.DEF_PSY:
					realDef.PSY -= (int)buff.val;
					break;
				case BuffTypes.DEF_EXP:
					realDef.EXP -= (int)buff.val;
					break;
				case BuffTypes.DEF_ENG:
					realDef.ENG -= (int)buff.val;
					break;
				case BuffTypes.DEF_MAG:
					realDef.MAG -= (int)buff.val;
					break;
				case BuffTypes.DE_DEF_PHY:
					realDef.PHY += (int)buff.val;
					break;
				case BuffTypes.DE_DEF_IMP:
					realDef.IMP += (int)buff.val;
					break;
				case BuffTypes.DE_DEF_PSY:
					realDef.PSY += (int)buff.val;
					break;
				case BuffTypes.DE_DEF_EXP:
					realDef.EXP += (int)buff.val;
					break;
				case BuffTypes.DE_DEF_ENG:
					realDef.ENG += (int)buff.val;
					break;
				case BuffTypes.DE_DEF_MAG:
					realDef.MAG += (int)buff.val;
					break;
				case BuffTypes.MSPD:
					if (buff.val >= 0)
					{
						changeMoveSpd (false, buff.val);
					}
					else
					{
						changeMoveSpd (true, -buff.val);
					}
					break;
				case BuffTypes.ASPD:
					if (buff.val >= 0) 
					{
						changeAtkSpd (false, buff.val);
					} 
					else
					{
						changeAtkSpd (true, -buff.val);
					}
					break;
				} 
//				if (skName == flashEffectName) {
//					unFlash ();
//					flashEffectName = "";
//				}
			}
		}
		
		for (int i=expiredBuff.Count-1; i>=0; i--)
		{
			Debug.Log ("remove buff " + expiredBuff [i]);		
			buffHash.Remove (expiredBuff [i]);
			
			if (buffHash.Count < 1) 
			{
				clearBuff ();
			}
		}
		expiredBuff.Clear();
	}

	public virtual void clearBuff ()
	{
		CancelInvoke ("buffTimer");
		unFlash ();
		
		foreach (Buff buff in buffHash.Values)
		{
			if(buff.buffFinish != null)
			{
				buff.buffFinish(this, buff);
			}
		}
		expiredBuff.Clear ();
		buffHash.Clear ();
	}
	
	public bool isTriggerPassiveSkill(int value){
		int randomIndex = UnityEngine.Random.Range(0,100);
		if(value < randomIndex){
			return true;
		}else{
			return false;
		}
	}
	
	public bool isContainBuff(string buffType){
		foreach(string skName in buffHash.Keys){
			Buff buff = buffHash [skName] as Buff;
			if(buff.type == buffType){
				return true;
			}
		}
		return false;
	}
	
	public virtual void setAbnormalState (ABNORMAL_NUM abnormal)
	{
//		if (isDead)
//			return;
//		
//		this.abnormalState = abnormal;
//		
//		if(this.abnormalState == ABNORMAL_NUM.NORMAL)
//		{
//			this.clearAbormalState();
//		}
//		else if (ABNORMAL_NUM.FEAR == this.abnormalState || ABNORMAL_NUM.FIRE == this.abnormalState)
//		{
//			this.OutOfControlPos = transform.position;
//			
//			standby();
//			
//			move (BattleBg.getPointInAround (OutOfControlPos));
//		}
	}

//	public ABNORMAL_NUM getAbnormalState ()
//	{
//		return this.abnormalState;
//	}

	public string getState ()
	{
		return state;
	}

	public virtual void highLight ()
	{
//		Material material = pieceAnima.getMaterial();
//		material.SetColor("_Color",Color(1, 1, 1, 1));
	}

	public virtual void cancelHighLight ()
	{
		
	}
	
	public void AlongThePath()
	{
		if (this.state == Character.CAST_STATE)
		{
			return;
		}
		this.targetPt = new Vector3(this.movePath[0].x, this.movePath[0].y, this.targetPt.z);
		this.movePath.RemoveAt(0);
		
		if (this.state == Character.ATK_STATE)
		{
			this.cancelAtk ();
		}
		
		this.state = Character.MOVE_STATE;
		this.playAnim ("Move");
		this.setDirection (this.targetPt);
		this.toward (this.targetPt);
	}
	
	public virtual void UpdateBarrierPathToTarget()
	{
		if (!BarrierMapData.Enable) 
		{
			return; 
		}
		this.movePath = BarrierMapData.Instance.GetPath((Vector2)transform.position, (Vector2)this.targetObj.transform.position);
	}
	
//	public function getAtkPostion2( Character atker  ):Vector3
//	{
//		Vector3 vc3;
//		Array topPosAry = atkPosAry[0];
//		Array bottomPosAry = atkPosAry[1];
//		dropAtkPosition(atker);
//		int minX = BattleBg.enemyBounds.min.x;
//		int maxX = BattleBg.enemyBounds.max.x;
//		int minY = BattleBg.enemyBounds.min.y;
//		int maxY = BattleBg.enemyBounds.max.y;
//		if(targetObj != null && atker.gameObject != targetObj){
//			setBackPoint(atker.getID());
//		}else{
//			setFrontPoint(atker.getID());
//		}
//		
//	}
//	
//	private function setFrontPoint( int atkerID  )
//	{
//		if(this.transform.localScale.x< 0)
//		{
//			switch(atkerAmount)
//			{
//				case 0:
//					vc3 =  transform.position + new Vector2(-atker.data.attackRange,0);
//					break;
//				case 1:
//					vc3 =  transform.position + new Vector2(-atker.data.attackRange,-10);
//					break;
//				case 2:
//					break;
//				case 3:
//					break;
//				default:
//					break;
//			}
//		}else{
//			
//		}	
//	}
//	private function setBackPoint( int atkerID  )
//	{
//		if(this.transform.localScale.x< 0)
//		{
//			
//		}else{
//			
//		}
//	}
	//0 left,1 right
	public virtual int isAtkerAtLeft (Character atker)
	{
		if (this.targetObj != null)
		{ 
			Character target = this.targetObj.GetComponent<Character> (); 
			if (!target.isDead) 
			{
				if (target != atker)
				{
					return this.targetObj.transform.position.x - this.transform.position.x > 0 ? 0 : 1; 
				} 
				else
				{
					return this.targetObj.transform.position.x - this.transform.position.x > 0 ? 1 : 0; 
				}
			}
		} 
	
		
		if (atker.model.transform.localScale.x > 0) 
		{
			if (this.transform.position.x > (BattleBg.actionBounds.min.x + 155))
			{
				return 0;
			}
			else
			{
				return 1;
			}
		}
		else
		{
			if (this.transform.position.x < (BattleBg.actionBounds.max.x - 155))
			{
				return 1;
			} 
			else 
			{
				return 0;
			}
		}
	}
	
	public virtual Vector3 getAtkPosition (Character atker)
	{
		this.dropAtkPosition (atker);
		List<Vector3> predefinedPos = new List<Vector3> ();
		predefinedPos.Add (gameObject.transform.position + new Vector3 (atker.data.attackRange*(-.6f) 	, 40, 0));
		predefinedPos.Add (gameObject.transform.position + new Vector3 (atker.data.attackRange*(-1f)	, 0, 0));
		predefinedPos.Add (gameObject.transform.position + new Vector3 (atker.data.attackRange*(-.8f) 	, -40, 0));

		predefinedPos.Add (gameObject.transform.position + new Vector3 (atker.data.attackRange*(.6f)	, 40, 0));
		predefinedPos.Add (gameObject.transform.position + new Vector3 (atker.data.attackRange			, 0, 0));
		predefinedPos.Add (gameObject.transform.position + new Vector3 (atker.data.attackRange*(.8f)	, -40, 0));

		float nearestDist = float.MaxValue;
		int nearestIndex = -1;
		//round one
		for (int i=0; i<predefinedPos.Count; i++) 
		{
			if (this.atkPosAry [i].ToString () == "0" && 
				!BattleBg.IsOutOfActionBounce (predefinedPos [i]) &&
				(BarrierMapData.Enable && BarrierMapData.Instance.IsThePositionValid(predefinedPos [i]) || 
				!BarrierMapData.Enable)) 
			{
				float dist = Vector3.Distance (predefinedPos [i], atker.transform.position);
				if (dist < nearestDist)
				{
					nearestDist = dist;
					nearestIndex = i;
				}
			}
		}
		//round two
		if (nearestIndex == -1) 
		{
			for (int i=0; i<predefinedPos.Count; i++)
			{
				// if (atkPosAry [i].ToString () == "0" ) {
				if (BarrierMapData.Enable && BarrierMapData.Instance.IsThePositionValid(predefinedPos [i]) || 
					!BarrierMapData.Enable && this.atkPosAry [i].ToString () == "0")
				{
					float dist = Vector3.Distance (predefinedPos [i], atker.transform.position);
					if (dist < nearestDist)
					{
						nearestDist = dist;
						nearestIndex = i;
					}
				}
			}
		}
		
		if(nearestIndex == -1)
		{
			Debug.LogError("not found a good postion to attack,use No.1");	
			nearestIndex = 0;
		}
		this.atkPosAry[nearestIndex] = atker.getID();
		Vector3 v = predefinedPos[nearestIndex];

		int minX = (int)BattleBg.actionBounds.min.x;
		int maxX = (int)BattleBg.actionBounds.max.x;
		int minY = (int)BattleBg.actionBounds.min.y;
		int maxY = (int)BattleBg.actionBounds.max.y;
		
		if (v.x < minX )
			v.x = minX + 10;
		if (v.y < minY )
			v.y = minY + 10;
		if (v.x > maxX)
			v.x = maxX - 10;
		if (v.y > maxY)
			v.y = maxY - 10;
		
		return v;
	}
	
	public bool isUnderAttack()
	{
		foreach(string attacker in atkPosAry)
		{
			if(attacker != "0") return true;	
		}
		return false;
	}
	
	public virtual void dropAtkPosition (Character atker)
	{
		for (int i=0; i<atkPosAry.Count; i++)
		{
			if (atkPosAry [i].ToString () == atker.getID ()) 
			{
				atkPosAry [i] = "0"; 
			}
		}
	}
	
	public GameObject getTarget ()
	{
		return targetObj;
	}
	
	public virtual void setTarget (GameObject gObj)
	{ 
		if (targetObj) {
			Character targetDoc = targetObj.GetComponent<Character> (); 
			targetDoc.dropAtkPosition (this);
		}
		targetObj = gObj;
	}

	public virtual string getTargetTagType ()
	{
		return "";
	}

	public virtual bool getIsDead ()
	{
		return isDead;
	}
	
	public int getDamageValue(Vector6 atk)
	{
		int damage = (int)((float) atk.total() / (float)this.realDef.total() * 40);
		if(damage <= 0)
		{
			damage = 1;
		}
		return damage;
	}
	
	public int getSkillDamageValue(Vector6 atk, float atkPer)
	{
		int damage = (int)(getDamageValue(atk) * (atkPer / 100.0f + 1) );
		
		if(damage <= 0)
		{
			damage = 1;
		}
		return damage;
	}
	
	public virtual int defenseAtk (Vector6 atk, GameObject atkerObj)
	{
		int rewardDamage = 0;
		if(null != addConcentrateFireDelegate){
			rewardDamage = addConcentrateFireDelegate(this);	
		}
		
		int heavyDamage = 0;
		if(null != addCruelCutsDelegate){
			heavyDamage = addCruelCutsDelegate(this);	
		}
		
		int additionalDamage = 0;
		additionalDamage += showNebula20Passive(atkerObj);
		
		if (this.isDead)
			return 0;
		//int dam = Mathf.Max(1, damage - realDef);
		
		int dam = getDamageValue(atk);
		dam += (int)(dam*((float)rewardDamage/100f));
		dam += (int)(dam*((float)heavyDamage/100f));
		dam += (int)(dam*((float)additionalDamage/100f));
		
		canShowNebula25Passive();
		
		if (!this.isHealthLocked)
		{
			if(this.hurtBeforeState == HurtBeforeState.NOTHURT)
			{
				this.characterAI.OnDefenseAtkHurtBeforeByDamageValue(dam);		
				this.showHpBar();
				return 0;
			}
			this.realHp -= dam;
			this.characterAI.OnDefenseAtkHurtAfterByRemainsCurrentHP(this.realHp);
		}
		
		this.showHpBar ();
		
		this.changeStateColor(Character.damageColor);
		
		if (this.realHp <= 0)
		{
			if(this is GRoot && LevelMgr.Instance.isRebirth){
				GRoot groot = this as GRoot;
				float t = 4f;
				State state = new State((int)t, null);
				groot.addAbnormalState(state, Character.ABNORMAL_NUM.LAYDOWN);
				groot.lossTargetBeforeState = LossTargetBeforeState.LOSSTARGET;
				StartCoroutine(delayRebirth(t));

				foreach(Enemy e in EnemyMgr.enemyHash.Values){
					if(e.targetObj != null && e.targetObj == this.gameObject){
						this.dropAtkPosition(e);
						e.targetObj = null;	
						e.checkOpponent();
					}
				}	
				
				return 0;
			}
			iTween.Stop(gameObject);
			dead ();
		} 
		else 
		{
			if(this.state == Character.CAST_STATE || 
				!this.isActionStateActive(ActionStateIndex.DamageActionState))
			{
				return dam;
			}
			
			this.attachAnim("Damage");
		
			if(this.isActionStateActive(ActionStateIndex.MoveState))
			{
				this.playDamageEffect(atkerObj,0);
			}
		}
		return dam;
	}
	
	private int showNebula20Passive(GameObject atkerObj){
		Character atker	= atkerObj.GetComponent<Character>();
		int damage = 0;
		if(atker != null && atker is Nebula && !atker.getIsDead()){
			Nebula nebula = atker as Nebula;
			if(nebula.canShowNebulaPassive("NEBULA20A")){
				damage = nebula.showSkill20APassive();
			}
		}else if(atker != null && atker is Ch2_Nebula && !atker.getIsDead()){
			Ch2_Nebula nebula = atker as Ch2_Nebula;
			damage = nebula.showSkill20APassive();
		}
		return damage;
	}
	
	private void canShowNebula25Passive(){
		if(this is Hero && !this.isNebula25Buff){
			foreach(Enemy enemy in EnemyMgr.enemyHash.Values){
				if(enemy is Ch2_Nebula){
					Ch2_Nebula nebula = enemy as Ch2_Nebula;
					nebula.showSkill25APassive(this);
					this.isNebula25Buff = true;
					break;
				}
			}
		}else if(this is Enemy && !this.isNebula25Buff){
			foreach(Hero hero in HeroMgr.heroHash.Values){
				if(hero is Nebula){
					Nebula nebula = hero as Nebula;
					if(nebula.canShowNebulaPassive("NEBULA25A")){
						nebula.showSkill25APassive(this);
						this.isNebula25Buff = true;
						break;	
					}
				}
			}
		}	
	}
	
	private IEnumerator delayRebirth(float t){
		yield return new WaitForSeconds(1.5f);
		
		GameObject prefab = Resources.Load("gsl_dlg/Groot_Particle2") as GameObject;
		GameObject go = Instantiate(prefab) as GameObject;
		go.transform.parent = this.transform;
		go.transform.localPosition = new Vector3(0,300,-500);
		
		yield return new WaitForSeconds(t-1.5f);
		if(this is GRoot && LevelMgr.Instance.isRebirth){
			GRoot groot = this as GRoot;
			groot.showGroot25Passive();
			LevelMgr.Instance.isRebirth = false;	
			groot.lossTargetBeforeState = Character.LossTargetBeforeState.NONE;
		}
	}
	
	public void playDamageEffect(GameObject atkerObj,float distance)
	{
		float backDistance = (distance == 0)? ((this is Hero)? 8: 13) : distance;
		float moveX = (atkerObj.transform.localPosition.x < this.gameObject.transform.localPosition.x)?   backDistance: -backDistance;
		float x = this.gameObject.transform.localPosition.x + moveX;
		x = Mathf.Clamp(x,BattleBg.actionBounds.min.x,BattleBg.actionBounds.max.x);
		if (!BarrierMapData.Enable || BarrierMapData.Enable && BarrierMapData.Instance.IsThePositionValid(new Vector2(x, transform.localPosition.y)))
		{
			iTween.StopByName(gameObject,"Back");
			iTween.MoveTo(gameObject, new Hashtable(){
				{"name","Back"},
				{"x",x},
				{"time",.2f},
				{"easeType", iTween.EaseType.easeOutQuad},
				{"onComplete", "onBackTweenComplete"}
			});
		}
		iTween.PunchPosition(hpBar.gameObject, new Vector3(10f,30f,0), .2f);
	}
	
	public void shakeCharacter()
	{
		iTween.StopByName(gameObject,"shakeCharacter");
		iTween.PunchPosition(gameObject,iTween.Hash("name","shakeCharacter","amount",new Vector3(0,15,0),"time",0.3f));
	}
	
	private void onBackTweenComplete()
	{
		setDirection(targetPt);
	}
	
	public void changeStateColor(Color color)
	{
		changeStateColor(color, currentColor, .2f, iTween.LoopType.none);
	}
	
	public void changeStateColor(Color color, Color currentColor, float time)
	{
		this.currentColor = currentColor;
		changeStateColor(color, currentColor, time, iTween.LoopType.none);
	}
	
	bool isChangeStateColor = false;
	
	public void changeStateColor(Color fromColor, Color toColor, float time, iTween.LoopType loopType)
	{
		if(isChangeStateColor)
		{
			return;
		}
		isChangeStateColor = true;
		model.renderer.material.color = fromColor;
		
		// StartCoroutine(changeStateColorFinished(toColor, 0.2f));
		StartCoroutine(changeStateColorFinished(toColor, time));
		
	}
	
	public IEnumerator changeStateColorFinished(Color toColor, float time)
	{
		yield return new WaitForSeconds(time);
		model.renderer.material.color = toColor;
		isChangeStateColor = false;
	}
	
	public virtual void flash (float r, float g, float b)
	{
		changeStateColor(new Color(r,g,b), normalColor, 0.2f,iTween.LoopType.pingPong);
	}

	public virtual void unFlash ()
	{
		if (isDead)
			return;
		
		currentColor = normalColor;
		model.renderer.material.color = currentColor;
		
//		iTween.ColorTo (model, new Hashtable (){{"r",0.5f},{"g",0.5f},{"b",0.5f},{"time",0.1f},{"easetype","linear"}});
	}
	
	protected float SpeedFactor = 1.0f;

	public virtual void multiSpeed (float factor)
	{
		SpeedFactor = factor;
		int intSpeed = (int)realMspd;
		realMspd = (float)(intSpeed * factor) + (realMspd - (float)intSpeed); 
		pieceAnima.doubleSpd ();
	}

	public virtual void doubleSpeed ()
	{
		multiSpeed (2f);
	}
	
	public virtual void defSpeed (float s)
	{
		multiSpeed (s);
	}

	public virtual void normalSpeed ()
	{
		int intSpeed = (int)realMspd;
		realMspd = (float)(intSpeed / SpeedFactor) + (realMspd - (float)intSpeed); 
		pieceAnima.normalSpd ();
	}
	
	public bool isHealthLocked = false;
	
	public void LockHealth(string[] parms)
	{
		isHealthLocked = true;
	}
	
	public void UnlockHealth(string[] parms){
		isHealthLocked = false;
	}
	
	public void SetHealthTo(string[] parms)
	{
		// realMaxHp = int.Parse(parms[0]);
		realHp = int.Parse(parms[0]);
	}
	
	public void SetDoubleHealth()
	{
		realMaxHp *= 2;
		realHp = realMaxHp;
	}
	
	//////////////////////////////////////
	////skill script use these function
	/////////////////////////////////////
	
	public virtual void addAtkPHY (int atkNum)
	{
		realAtk.PHY += atkNum;
	}
	
	public virtual void addAtkIMP (int atkNum)
	{
		realAtk.IMP += atkNum;
	}
	public virtual void addAtkPSY (int atkNum)
	{
		realAtk.PSY += atkNum;
	}
	public virtual void addAtkEXP (int atkNum)
	{
		realAtk.EXP += atkNum;
	}
	public virtual void addAtkENG (int atkNum)
	{
		realAtk.ENG += atkNum;
	}
	
	public virtual void addAtkMAG (int atkNum)
	{
		realAtk.MAG += atkNum;
	}
	

	public virtual void resetAtk ()
	{
		realAtk = data.attack.clone();
	}
	
	
	public virtual void addDefPHY (int atkNum)
	{
		realAtk.PHY += atkNum;
	}
	
	public virtual void addDefIMP (int atkNum)
	{
		realAtk.IMP += atkNum;
	}
	public virtual void addDefPSY (int atkNum)
	{
		realAtk.PSY += atkNum;
	}
	public virtual void addDefEXP (int atkNum)
	{
		realAtk.EXP += atkNum;
	}
	public virtual void addDefENG (int atkNum)
	{
		realAtk.ENG += atkNum;
	}
	
	public virtual void addDefMAG (int atkNum)
	{
		realAtk.MAG += atkNum;
	}

	public virtual void resetDef ()
	{
		realDef = data.defense.clone ();
	}
	
	public virtual void changeMoveSpd (bool isAdd, float per)
	{
		realMspd += isAdd ? (data.moveSpeed * per) : (-data.moveSpeed * per); 
	}
	
	public virtual void resetMoveSpd ()
	{
		realMspd = data.moveSpeed;
	}

	public virtual void changeAtkSpd (bool isAdd, float per)
	{
		float addValue = data.attackSpeed / (1.0f + per) - data.attackSpeed; 
		realAspd += isAdd ? addValue : (-addValue);
	}
	
	public virtual void resetAtkSpd ()
	{
		realAspd = data.attackSpeed;
	}
	
	public virtual void addHp (int hpNum)
	{
		if(hpNum > 0){
			float deHpValue = 0;
			foreach(string buffName in buffHash.Keys){
				Buff buff = buffHash [buffName] as Buff;
				if(buff.type == BuffTypes.DE_HP_HEALING){
					deHpValue += buff.val;
				}
			}
			hpNum -= (int)deHpValue;
			hpNum = hpNum > 0 ? hpNum:0;
		}
		
		realHp += hpNum;
		realHp = Mathf.Min (realMaxHp, realHp);
		realHp = Mathf.Max (0, realHp);
		showHpBar();
//		changeStateColor(beHealColor);
	}
	
	public virtual void realDamage (int dam)
	{
		if (this.isDead)
			return;
		
		if(hurtBeforeState == HurtBeforeState.NOTHURT)
		{
			this.characterAI.OnRealDamageHurtBeforeByDamageValue(dam);
		
			return;
		}
		
		this.addHp(-dam);
		
		this.characterAI.OnRealDamageHurtAfterByRemainsCurrentHP(this.realHp);
		
		this.changeStateColor(Character.damageColor);
		
		if (this.realHp <= 0)
		{
			this.dead ();
		} 
		else
		{
			if(this.isActionStateActive(ActionStateIndex.DamageActionState))
			{
				this.attachAnim("Damage");
			}
		}
	}

	public string getID ()
	{
		return id;
	}
	//////////////////////////////////////
	
	public virtual void standby ()
	{
		if(this.isDead)
		{
			return;
		}
		if(this.isAbnormalStateActive(ABNORMAL_NUM.LAYDOWN))
		{
			return;
		}
		if(this.state == Character.ATK_STATE)
		{
			this.cancelAtk();
		}
		this.state = Character.STANDBY_STATE;
		this.doAnim("Stand");
	}
	
	public virtual void selecting ()
	{
	
	}
	
	public virtual void cancelAtk ()
	{
		if (IsInvoking ("attackTargetInvok"))
		{
			CancelInvoke ("attackTargetInvok");
		}
	}
	
	public virtual void startAtk ()
	{ 
		if(!this.isActionStateActive(ActionStateIndex.AttackState))
		{
			return;
		}
		
		this.cancelCheckOpponent();
		
		if(this.isDead)
		{
			return;
		}
		this.state = Character.ATK_STATE;
		if (!IsInvoking ("attackTargetInvok")) 
		{
			attackTargetInvok();
			InvokeRepeating ("attackTargetInvok", this.realAspd, this.realAspd);
		}
	}
	
	public void StandBy(string[] parms)
	{ 
		targetObj = null;
		standby();
	}
	   
	public virtual void showHpBar ()
	{
		if(hpBar.isHidenHpBar())
		{
			hpBar.showHpBar();
		}
//		if (!hpBar.gameObject.activeSelf)
//		{
//			hpBar.gameObject.SetActiveRecursively (true);
//		}
		
		StartCoroutine(hpBar.ChangeHp (realHp));
	}

	public virtual void hideHpBar ()
	{
		hpBar.hideHpBar();
	}
	
	public virtual void toward(Vector3 vc3)
	{
		if (model.transform.position.x > vc3.x) 
		{
			model.transform.localScale = new Vector3 (-scaleSize.x, scaleSize.y, 1);
		}
		else
		{
			model.transform.localScale = new Vector3 (scaleSize.x, scaleSize.y, 1);
		}
	}
	
	public virtual void setDirection (Vector3 vc3)
	{
		float dis_y = vc3.y - transform.position.y;
		float dis_x = vc3.x - transform.position.x;
		float angle = Mathf.Atan2 (dis_y, dis_x);
		dirVc3 = new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0);
	}
	
	public void setPosition(Vector3 v)
	{
		transform.position = new Vector3 (v.x, v.y, v.y / 10 + StaticData.objLayer);
	}
	
	public virtual void setDepth ()
	{
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y / 10 + StaticData.objLayer);
	}
	
	public delegate void noParmsDelegate();
	public delegate bool AnimationFinishedDelegate(string name);
	public AnimationFinishedDelegate AnimationFinishedCallback;
	public virtual void playAnim (string name)
	{
		if (null != AnimationFinishedCallback && AnimationFinishedCallback(name)){
			AnimationFinishedCallback = null;
		}
		pieceAnima.playAct (name);
	}  
	
	public virtual void attachAnim (string name)
	{
		if(state == ATK_STATE || 
			state == CAST_STATE) 
		{
			return;
		}
		
		pieceAnima.attachAct (name);
	}

	public virtual void doAnim (string name)
	{
		pieceAnima.doAct (name);
	}
	
	public virtual void dead (string s=null)
	{
		if (this.isDead)
			return;
		
//		CancelInvoke ("twineToNormal");
//		CancelInvoke ("twistToNormal");
//		CancelInvoke ("cancelLayDown");
//		CancelInvoke ("cancelFire");
//		CancelInvoke ("cancelFear");
//		CancelInvoke ("cancelFreeze");
//		CancelInvoke ("freezeToNormal");
//		CancelInvoke ("stunToNormal");
		
//		this.executeParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnDestroySkillEftObj);
		this.clearBuff ();
		
//		this.setAbnormalState (Character.ABNORMAL_NUM.NORMAL);
		resetMoveSpd();
		
		pieceAnima.normalSpd();
		
		resetAtkSpd();
		
		isAtkSameTag = false;
		
		currentColor = normalColor;
		model.renderer.material.color = currentColor;
		
		if (this.state == Character.ATK_STATE)
		{
			this.cancelAtk ();
		}
		
		this.state = Character.DEAD_STATE; 
		
		//add by xiaoyong for  character stand same position 20120508
		if (this.targetObj != null)
		{ 
			Character targetDoc = this.targetObj.GetComponent<Character> ();
			targetDoc.dropAtkPosition (this);
			this.targetObj = null;
		}
		
		this.isDead = true;
		this.data.isDead = true;	
		
		this.clearAbormalState();
		
		Message msg = new Message (MsgCenter.FALL_DOWN, this);
		msg.data = this;
		MsgCenter.instance.dispatch (msg);
		
		if(s != "NoPlayDeathAnimations")
		{
			this.playAnim ("Death");
		}
		
		iTween.Stop (gameObject);
		gameObject.collider.enabled = false;
		this.hideHpBar ();
	}

	public virtual void destroyThis()
	{
		if (StaticData.isTouch4)
		{
			Destroy (gameObject);
		} 
		else
		{
			this.gameObject.transform.position = new Vector3 (-1000, 0, 0);
		}
	}
	
	public virtual void criticalHandler ()
	{
		
	}
	
	public virtual void attackTargetInvok ()
	{
		
	}
	
	public void UpdateBarrierPosition()
	{
		if (!BarrierMapData.Enable)
			return;
		
		Point curPos = BarrierMapData.Instance.TranslatePosToIndex((Vector2)transform.position);
		if (barrierPos.IsValid
				|| (false == barrierPos.Equals(curPos))){
			barrierPos = curPos;
			if (null != OnMovingOnBarrierMap){
				OnMovingOnBarrierMap();
			}
		}
	}

	public virtual void changeWeapon (string weaponID)
	{

	}
	
	protected virtual void checkAtkerDefense ( GameObject atker  )
	{
	
	}
	
	public virtual void Update ()
	{
		if(this.isAbnormalStateActive(ABNORMAL_NUM.STUN))
		{
			playAnim("Stand");
		}
		
		if(!this.isActionStateActive(ActionStateIndex.MoveState))
		{
			
			return;
		}
				
		if (state == MOVE_STATE)
		{
			moveStateUpdate();
		}
		else if (state == MOVE_TARGET_STATE)
		{
			moveTargetStateUpdate();
		}
		else if (state == MOVE_TARGET_DIRECTLY_STATE)
		{
			moveTargetDirectlyStateUpdate();
		}
		else if (state == ATK_STATE)
		{
			moveTargetInAtkUpdate();
		}
	}
	
	// This function can relaced by IsOutOfBounce function in BattleBg class
	public virtual bool isCollider ()
	{
		/*//BoxCollider bgBoxCollider = BattleBg.bgCollider.GetComponent<BoxCollider>();
		Vector3 minVc3 = BattleBg.bounds.min;
		Vector3 maxVc3 = BattleBg.bounds.max;
		Rect rect = new Rect(minVc3.x, minVc3.y, maxVc3.x-minVc3.x, maxVc3.y-minVc3.y);
		
		Vector2 vc2 = new Vector2(targetObj.transform.position.x,targetObj.transform.position.y);
		
		//return true;
		return rect.Contains(vc2);*/
		return !BattleBg.IsOutOfFingerBounce(targetObj.transform.position);
	}
	
	public virtual bool isSelfCollider()
	{
		return !BattleBg.IsOutOfFingerBounce(transform.position);
	}
	
	public event ParmlessHandlerByParam OnGroupAttackFinished;
	
	public event ParmlessHandlerByParam OnMoveToTargetDirectlyFinished;
	
	public event ParmlessHandlerByParam OnMoveToPositionFinished;
	
	public event ParmlessHandlerByParam OnDestroySkillEftObj;
	
	public enum ParmlessHandlerFunNameEnum
	{
		OnMoveToPositionFinished,
		OnGroupAttackFinished,
		OnMoveToTargetDirectlyFinished,
		OnDestroySkillEftObj
	}
	
	
	public void addHandlerToParmlessHandlerByParam(ParmlessHandlerFunNameEnum parmlessHandlerFunNameEnum, ParmlessHandlerByParam handler)
	{
		if(parmlessHandlerFunNameEnum == ParmlessHandlerFunNameEnum.OnDestroySkillEftObj)
		{
			OnDestroySkillEftObj += handler;
		}
		else if(parmlessHandlerFunNameEnum == ParmlessHandlerFunNameEnum.OnMoveToPositionFinished)
		{
			OnMoveToPositionFinished += handler;
		}
		else if(parmlessHandlerFunNameEnum == ParmlessHandlerFunNameEnum.OnGroupAttackFinished)
		{
			OnGroupAttackFinished += handler;
		}
		else if(parmlessHandlerFunNameEnum == ParmlessHandlerFunNameEnum.OnMoveToTargetDirectlyFinished)
		{
			OnMoveToTargetDirectlyFinished += handler;
		}
	}
	
	public void removeHandlerFromParmlessHandlerByParam(ParmlessHandlerFunNameEnum parmlessHandlerFunNameEnum, ParmlessHandlerByParam handler)
	{
		if(parmlessHandlerFunNameEnum == ParmlessHandlerFunNameEnum.OnDestroySkillEftObj)
		{
			OnDestroySkillEftObj -= handler;
		}
		else if(parmlessHandlerFunNameEnum == ParmlessHandlerFunNameEnum.OnMoveToPositionFinished)
		{
			OnMoveToPositionFinished -= handler;
		}
		else if(parmlessHandlerFunNameEnum == ParmlessHandlerFunNameEnum.OnGroupAttackFinished)
		{
			OnGroupAttackFinished -= handler;
		}
		else if(parmlessHandlerFunNameEnum == ParmlessHandlerFunNameEnum.OnMoveToTargetDirectlyFinished)
		{
			OnMoveToTargetDirectlyFinished -= handler;
		}
	}
	
	public void executeParmlessHandlerByParam(ParmlessHandlerFunNameEnum parmlessHandlerByParamEnum)
	{
		if(parmlessHandlerByParamEnum == ParmlessHandlerFunNameEnum.OnDestroySkillEftObj)
		{
			if(OnDestroySkillEftObj != null)
			{
				OnDestroySkillEftObj(this);
			}
		}
		else if(parmlessHandlerByParamEnum == ParmlessHandlerFunNameEnum.OnMoveToPositionFinished)
		{
			if(OnMoveToPositionFinished != null)
			{
				OnMoveToPositionFinished(this);
			}
		}
		else if(parmlessHandlerByParamEnum == ParmlessHandlerFunNameEnum.OnGroupAttackFinished)
		{
			if(OnGroupAttackFinished != null)
			{
				OnGroupAttackFinished(this);
			}
		}
		else if(parmlessHandlerByParamEnum == ParmlessHandlerFunNameEnum.OnMoveToTargetDirectlyFinished)
		{
			if(OnMoveToTargetDirectlyFinished != null)
			{
				OnMoveToTargetDirectlyFinished(this);
			}
		}
	}
	
	public bool isParmlessHandlerByParamNull(ParmlessHandlerFunNameEnum parmlessHandlerByParamEnum)
	{
		if(parmlessHandlerByParamEnum == ParmlessHandlerFunNameEnum.OnDestroySkillEftObj)
		{
			return OnDestroySkillEftObj == null;
		}
		else if(parmlessHandlerByParamEnum == ParmlessHandlerFunNameEnum.OnMoveToPositionFinished)
		{
			return OnMoveToPositionFinished == null;
		}
		else if(parmlessHandlerByParamEnum == ParmlessHandlerFunNameEnum.OnGroupAttackFinished)
		{
			return OnGroupAttackFinished == null;
		}
		else if(parmlessHandlerByParamEnum == ParmlessHandlerFunNameEnum.OnMoveToTargetDirectlyFinished)
		{
			return OnMoveToTargetDirectlyFinished == null;
		}
		return false;
	}
	
	public virtual void moveWhenTargetDead()
	{
		
	}
	
	public virtual void setClearState()
	{
//		if(this.abnormalState == Character.ABNORMAL_NUM.FEAR)
//		{
//			CancelInvoke ("cancelFear");
//			cancelFear();
//		}
//		else if(this.abnormalState == Character.ABNORMAL_NUM.FIRE)
//		{
//			CancelInvoke ("cancelFire");
//			cancelFire();
//		}
//		else if(this.abnormalState == Character.ABNORMAL_NUM.TWINE)
//		{
//			CancelInvoke ("twineToNormal");
//			twineToNormal();
//		}
//		else if(this.abnormalState == Character.ABNORMAL_NUM.TWIST)
//		{
//			CancelInvoke ("twistToNormal");
//			twistToNormal();
//		}
//		else if(this.abnormalState == Character.ABNORMAL_NUM.FREEZE)
//		{
//			CancelInvoke ("freezeToNormal");
//			freezeToNormal();
//		}
//		else if(this.abnormalState == Character.ABNORMAL_NUM.STUN)
//		{
//			CancelInvoke("stunToNormal");
//			stunToNormal();
//		}
	}
	
	public virtual void move (Vector3 vc3)
	{		
		if(!this.isActionStateActive(ActionStateIndex.MoveState) || (this.state != null && this.state == Character.CAST_STATE))
		{
			return;
		}
		
		if (BarrierMapData.Enable)
		{
			this.movePath = BarrierMapData.Instance.GetPath(transform.position, vc3);
			if (this.movePath.Count > 0)
			{
				this.AlongThePath();
			}	
		}
		else
		{
			if(!this.isActionStateActive(ActionStateIndex.MoveState) || (this.state != null && this.state == Character.CAST_STATE))
			{
				return;
			}
			
			this.targetPt = vc3;
			
			if (this.state == Character.ATK_STATE)
			{
				this.cancelAtk ();
			}
			this.state = Character.MOVE_STATE;
			this.playAnim ("Move");
			//if(this as Hero) MusicManager.Instance.playMoveMusic(this.data.type);
			this.setDirection (vc3);
			this.toward (vc3);	
		}
	}
	
	public virtual void SkillFinish()
	{
		standby();
		if(!TsTheater.InTutorial)
		{
			if(targetObj != null)
			{
				// standby();
				moveToTarget(targetObj);
			}
		}
	}
	
	public virtual void moveToTarget (GameObject obj)
	{
		if(!this.isActionStateActive(ActionStateIndex.MoveState) || (this.state != null && this.state == Character.CAST_STATE))
		{
			return;
		}
		
		if(this.state == Character.ATK_STATE)
		{
			this.cancelAtk();
		}
		
		if(this.isDead)
		{
			return;
		}
		if (this.targetObj != null) 
		{
			Character targetDoc = this.targetObj.GetComponent<Character> ();
			targetDoc.dropAtkPosition (this);
		}
		
		this.targetObj = obj;
		this.playAnim ("Move");
		//if(this as Hero) MusicManager.Instance.playMoveMusic(this.data.type);
		this.state = Character.MOVE_TARGET_STATE;
		
		bool b = this.isCollider();
		
		Character character = this.targetObj.GetComponent<Character>();
		
		if(!b && character.getIsDead())
		{
			this.targetObj = null;
			this.standby();
			return;
		}
	}
	
	protected virtual void moveStateUpdate ()
	{		
		float dis = Vector2.Distance (transform.position, this.targetPt);
		float spd = Time.deltaTime * this.realMspd;
		
		if (Mathf.Abs (dis) > spd) 
		{
			if (this.dirVc3.y != 0) 
			{
				this.setDepth ();
			}
			transform.Translate (this.dirVc3 * spd);
			this.UpdateBarrierPosition();
		} 
		else 
		{
			MusicManager.Instance.stopMoveMusic(this.data.type);
			if (this.isActionStateActive(ActionStateIndex.OutOfControlMoveState))
			{
				this.move(BattleBg.getPointInAround (this.OutOfControlPos));
			} 
			else if (BarrierMapData.Enable && this.movePath.Count > 0)
			{
				this.AlongThePath();
			}
			else
			{
				if(this.isParmlessHandlerByParamNull(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished))
				{
					this.standby ();
					this.selecting ();
				}
				else
				{
					this.executeParmlessHandlerByParam(Character.ParmlessHandlerFunNameEnum.OnMoveToPositionFinished);
				}
			}
		}
	}
	
	public virtual void moveTargetStateUpdate ()
	{
		if(this.targetObj == null || 
			this.targetObj.GetComponent<Character>().getIsDead())
		{
			MusicManager.Instance.stopMoveMusic(this.data.type);
			this.moveWhenTargetDead();
			return;
		}
		
//		if(this.abnormalState == Character.ABNORMAL_NUM.TWIST)
//		{
//			return;
//		}
//		
//		if(this.abnormalState == Character.ABNORMAL_NUM.STUN)
//		{
//			return;	
//		}
		
		this.toward(this.targetObj.transform.position);
		Character targetCharacter = this.targetObj.GetComponent<Character>();
		
		if (0 == this.movePath.Count)
		{
			this.UpdateBarrierPathToTarget();
		}
		Vector2 vc2 = (this.movePath.Count > 1)? this.movePath[0] : (Vector2)targetCharacter.getAtkPosition(this);
		// Vector2 vc2 = targetCharacter.getAtkPosition(this);
		
		float dis2 = Vector2.Distance(transform.position, vc2);
		float spd2 = Time.deltaTime * this.realMspd;
		
		this.characterAI.OnMoveToTargetDistance(Vector3.Distance(transform.position, targetObj.transform.position));
		
		if(dis2 > spd2 * 3)
		{
			this.setDirection(vc2);
			if(this.dirVc3.y != 0)
			{
				this.setDepth();
			}
			transform.Translate(this.dirVc3 * Time.deltaTime * this.realMspd);
			this.UpdateBarrierPosition();
		}
		else if (dis2 <= spd2 * 3)
		{
			MusicManager.Instance.stopMoveMusic(this.data.type);
			this.characterAI.OnReachTarget(vc2);
		}
	}
	
	protected virtual void moveTargetInAtkUpdate ()
	{
		if(this.targetObj == null && this.state != Character.STANDBY_STATE )
		{
			this.moveWhenTargetDead();
			return;
		}
		
		if(!this.isPlayAtkAnim)
		{
			int xDis = (int)Mathf.Abs(this.targetObj.transform.position.x - transform.position.x);
			int yDis = (int)Mathf.Abs(this.targetObj.transform.position.y - transform.position.y);
			float spd2 = Time.deltaTime * this.realMspd * 2f;
			bool isInRange = xDis<=(this.data.attackRange+spd2) && xDis>= (this.data.attackRange-spd2) && yDis<this.data.attackRange*0.3f ;
			if(!isInRange)
			{	
				this.moveToTarget(this.targetObj);
			}
		}
	}
	
	public virtual void moveTargetDirectlyStateUpdate()
	{
	}
	
	public virtual void startCheckOpponent()
	{
		cancelAtk();
		InvokeRepeating("checkOpponent",0.1f,realAspd);
	}
	
	public virtual void cancelCheckOpponent()
	{
		if(IsInvoking("checkOpponent"))
		{
			CancelInvoke("checkOpponent");
		}
	}
	
	public virtual bool checkOpponent()
	{
		return true;
	}
	
	protected virtual void ActivatePassiveEffect(){
		
	}
	
	public virtual Character getOpponent(Character primaryOpponent = null)
	{
		return null;
	}
	
	public void castSkill ( string skName  )
	{
		if(!isDead)
		{
			if(data.type == HeroData.HEALER)
			{
				state = CAST_STATE; 
				standby();
				state = CAST_STATE;
			}
			else
			{
				standby();
				state = CAST_STATE;
			}
//			Debug.Log(">>>>>>skillname:"+skName);
			playAnim(skName);
		}
	}
	
	public virtual SkillIconData pickASkillDataFromContainer(string skId)
	{
//		ArrayList skills = getSkillList();
		SkillIconData data = SkillIconManager.Instance.getSkillIconData(skId);
		
		
		skContainer.Remove(skId);
		
		return data;
	}
	
	public void pushSkill ( string skillName  ){
		skContainer.Add(skillName);
	}
	
	public void ClearSkillContainer()
	{
		if(skContainer != null)
		{
			skContainer.Clear();
		}
	}
	
	public void PushSkillIdToContainer(string skId){
		/*bool isExist = false;
		for (int i=0; i<skContainer.Count; i++){
			if ((string)skContainer[i] == skId){
				isExist = true;
				i = skContainer.Count;
			}
		}
		if (!isExist){
			skContainer.Add(skId);
		}*/
		skContainer.Clear();
		skContainer.Add(skId);
	}
	
	public string getSkIdCanCastFromContainer()
	{
		if(skContainer.Count <= 0)
		{
			return "";
		}
		return (string)skContainer[0];
	}
}

public class Buff
{
	public delegate void BuffFinish(Character character, Buff self);
	
	public Buff (float endTime, float val, string type, string buffName, BuffFinish buffFinish)
	{
		this.endTime = endTime;
		this.val = val;
		this.type = type;
		this.buffName = buffName;
		this.buffFinish = buffFinish;
		
	}

	public float endTime;
	public float val;
	public string type;
	public string buffName;
	public BuffFinish buffFinish;
}
 
