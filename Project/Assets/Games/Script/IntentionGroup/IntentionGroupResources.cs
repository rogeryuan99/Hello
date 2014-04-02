using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Line = MyLineRenderer;
using Circle = MyCircleRenderer;

public class IntentionGroupResources : MonoBehaviour {

	private static IntentionGroupResources _instance;
	public static IntentionGroupResources Instance{
		get{
			return _instance;
		}
	}
	
	
	
	public void Awake(){
		_instance = this;
		TempGroup = new GameObject("IntentionGroup").AddComponent<IntentionGroup>();;
		SkillGroup = new GameObject("IntentionGroup_Skill").AddComponent<IntentionGroup>();;
	}
	public void Update(){
		TempGroup.Update();
		SkillGroup.Update();
	}
	
	public void OnDestroy(){
		_instance = null;
	}
	
	
	public IntentionGroup TempGroup{ get; set; }
	public IntentionGroup SkillGroup{ get; set; }
	
	// For Lines
	public Material []linesMaterial;
	public Material GetMaterialOfLine(Line.TYPE type_){
		int index = (int)type_;
		
		if (linesMaterial.Length-1 < index){
			Debug.LogError("Line's Material is null.");
		}
		
		return linesMaterial[index];
	}
	
	public Line.TYPE GetLineTypeShouldBe(Circle.TYPE type1, Circle.TYPE type2){
		Line.TYPE lineType = Line.TYPE.H2H;
		
		if (Circle.TYPE.NONE == type1 && Circle.TYPE.FINGER == type2){
			// skill: aiming target
		}
		else
		if (Circle.TYPE.NONE == type1 && Circle.TYPE.ENEMY == type2){
			// skill: aim enemy
		}
		else
		if (Circle.TYPE.NONE == type1 && Circle.TYPE.PLAYER == type2){
			// skill: aim hero
		}
		else
		if (Circle.TYPE.PLAYER == type1 && Circle.TYPE.PLAYER == type2){
			// hero: group
		}
		else 
		if (Circle.TYPE.PLAYER == type1 && Circle.TYPE.ENEMY == type2){
			// hero: attack
		}
		else
		if (Circle.TYPE.PLAYER == type1 && Circle.TYPE.FINGER == type2){
			// hero: move
		}
		else 
		if (Circle.TYPE.GROUP == type1 && Circle.TYPE.ENEMY == type2){
			// group: attack
		}
		else 
		if (Circle.TYPE.GROUP == type1 && Circle.TYPE.FINGER == type2){
			// group: move
		}
		
		return lineType;
	}
	
	// For Circles
	public GameObject []circlePrefabs;
	public GameObject GetCircle(Circle.TYPE type_){
		int index = (int)type_;
		
		if (circlePrefabs.Length-1 < index){
			Debug.LogError("Circle Prefabs is null");
		}
		
		return (GameObject)(Instantiate(circlePrefabs[index]));
	}
}
