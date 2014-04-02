using UnityEngine;
using System;
using System.Collections;

public class SkillLearnedData {
	
	public enum LearnedState{
		LOCKED, LEARNED, LEARNING, UNLEARNED
	}
	
	public string Id{
		get{ return id; }
	}
	public void updateState(){
		if(State == LearnedState.LEARNING){
			if (string.IsNullOrEmpty(learnedTill)){
				throw new Exception("learnedTill can not be null");
			}
			TimeSpan span = DateTime.Parse(learnedTill).Subtract(DateTime.UtcNow);
			if (span.TotalMilliseconds <= 0){
				learnedTill = string.Empty;
				State = LearnedState.LEARNED;
			}
		}
	}
	public string TimeStringShort{
		get{
			if(string.Empty == learnedTill) return "";
			TimeSpan span = DateTime.Parse(learnedTill).Subtract(DateTime.UtcNow);
			if (span.TotalHours>1){
				return string.Format("{0:F0}h {1}m", span.TotalHours, span.Minutes);		
			}else{
				return string.Format("{0}m {1}s", span.Minutes, span.Seconds);		
			}
		}
	}
	
	public string Time{
		get{
			TimeSpan span = DateTime.Parse(learnedTill).Subtract(DateTime.UtcNow);
			return string.Format("{0:F0}:{1}:{2}", span.TotalHours, span.Minutes, span.Seconds);
		}
	}
	
	public int TotalSeconds{
		get{
			if (string.IsNullOrEmpty(learnedTill)) return 0;
			
			TimeSpan span = DateTime.Parse(learnedTill).Subtract(DateTime.UtcNow);
			return (int)span.TotalSeconds;
		}
	}
	
	public object DynamicData{
		get{
			Hashtable result = new Hashtable();
			result.Add("id", id);
			result.Add("till", learnedTill);
			return result;
		}
	}
	
	public bool IsLearned{
		get{ 
			if (!string.IsNullOrEmpty(learnedTill)){
				TimeSpan span = DateTime.Parse(learnedTill).Subtract(DateTime.UtcNow);
				if (span.TotalMilliseconds <= 0) 
					learnedTill = string.Empty;
			}
			return string.IsNullOrEmpty(learnedTill); 
		}
	}
	
	public LearnedState State{
		get; set;
	}
	
	
	public SkillLearnedData(SkillDef def){
		id = def.id;
		learnedTill = string.Empty;
		State = LearnedState.LOCKED;
	}
	
	public SkillLearnedData(Hashtable table){
		id = table["id"] as string;
		learnedTill = table["till"] as string;
		State = IsLearned? LearnedState.LEARNED: LearnedState.LEARNING;
	}
	
	public void SkipLearningTime(){
		learnedTill = string.Empty;
		State = LearnedState.LEARNED;
	}
	
	public void Learn(double learnTime){
		learnedTill = DateTime.UtcNow.AddSeconds(learnTime).ToString();
		State = LearnedState.LEARNING;
	}
	
	#region Private variables
	private string id;
	private string learnedTill;
	#endregion
}
 