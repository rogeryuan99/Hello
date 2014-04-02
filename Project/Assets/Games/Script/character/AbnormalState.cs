using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AbnormalState
{
	
	public delegate void StateCallback();
	
	public string actionStates = Character.DefaultActionStates;
	
	public List<State> states = new List<State>();
	
	public StateCallback stateStartCallback;
	
	public StateCallback stateEndCallback;
	
	public AbnormalState(string actionStates, int totalDurationTime, StateCallback stateStartCallback, StateCallback stateEndCallback)
	{
		this.actionStates = actionStates;
		this.stateStartCallback = stateStartCallback;
		this.stateEndCallback = stateEndCallback;
	}
	
//	public AbnormalState(string actionStates, int totalDurationTime, State state)
//	{
//		this.actionStates = actionStates;
//		this.totalDurationTime = totalDurationTime;
//		this.states.Add(state);
//	}
	
	public void addState(State state)
	{		
		this.states.Add(state);
	}
	
	public void checkTime(float currentTime, Character character)
	{
		List<State> deleteStates = new List<State>();
		
		foreach(State state in states)
		{
			if(state.endTime <= currentTime)
			{
				state.stateFinish(character);
				deleteStates.Add(state);
			}
		}
		
		foreach(State state in deleteStates)
		{
			this.states.Remove(state);
		}
		deleteStates.Clear();
	}
	
	public void clear(Character character)
	{
		foreach(State state in states)
		{
			state.stateFinish(character);
		}
		states.Clear();
		
		if(this.stateEndCallback != null)
		{
			this.stateEndCallback();
		}
	}
	
	public bool isActionStateActive(Character.ActionStateIndex actionStateIndex)
	{
		return this.actionStates[(int)actionStateIndex] == '1';
	}
	
//	public bool isActionStateActive(Character.ActionStateIndex actionStateIndex)
//	{
//		return ((this.actionStates >> (int)actionStateIndex ) & 1) == 1;
//	}
}

public class State
{
	public delegate void StateFinish(State self, Character character);
	public float endTime = 0;
	
	public StateFinish stateFinishCallback;
	
	public State(int endTime, StateFinish stateFinishCallback = null)
	{
		this.endTime = endTime;
		this.stateFinishCallback = stateFinishCallback;
	}
	
	public void stateFinish(Character character)
	{
		if(this.stateFinishCallback != null)
		{
			this.stateFinishCallback(this, character);
		}
	}
}
