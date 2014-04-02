using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using CircleType = MyCircleRenderer.TYPE;

public class IntentionGroup: MonoBehaviour{
		
	public enum TYPE{
		NONE=0, P2P, P2F, P2E, P2P2F, P2P2E, G2F, G2E
		// Player, Finger, Enemy, Group
	}
	
	private List<MyCircleRenderer> circles    = new List<MyCircleRenderer>();
	private List<MyLineRenderer> lines = new List<MyLineRenderer>();
	private TYPE type = TYPE.NONE;
	
	public TYPE Type{
		get{
			return type;
		}
	}
	
	// Functions
	public void Update(){
		UpdateCircles();
	 	UpdateLines();
	}
	
	public void StartDrawing(){}
	
	public void AddTarget(MyCircleRenderer.TYPE circleType_){
		if (!CheckIsCanAddTarget(circleType_)) return;
		
		if (CheckIsTargetCanReplaceByFinger(circleType_)){
			circles[circles.Count-1].Type = circleType_;
			circles[circles.Count-1].TrackingTarget = null;
			lines[lines.Count-1].Type = IntentionGroupResources.Instance.GetLineTypeShouldBe(
											circles[circles.Count-2].Type, circleType_);
		}
		else {
			circles.Add(new MyCircleRenderer(circleType_, transform));
			if (circles.Count > 1){
				lines.Add(new MyLineRenderer(
								IntentionGroupResources.Instance.GetMaterialOfLine(
									IntentionGroupResources.Instance.GetLineTypeShouldBe(
										circles[circles.Count-2].Type, circles[circles.Count-1].Type))));
			}
		}
		
		RecheckType();
	}
	
	public void AddTarget(MyCircleRenderer.TYPE circleType_, GameObject trackingTarget_){
		if (!CheckIsCanAddTarget(circleType_, trackingTarget_)) return;
		
		if (circles.Count > 1 
			&& (MyCircleRenderer.TYPE.FINGER == circles[circles.Count-1].Type
				|| MyCircleRenderer.TYPE.PLAYER == circles[circles.Count-1].Type)){
			
			circles[circles.Count-1].TrackingTarget = trackingTarget_;
			circles[circles.Count-1].Type = circleType_;
			lines[lines.Count-1].Type = IntentionGroupResources.Instance.GetLineTypeShouldBe(
											circles[circles.Count-2].Type, circleType_);
		}
		else{
			circles.Add(new MyCircleRenderer(circleType_, trackingTarget_, transform));
			if (circles.Count > 1){
				lines.Add(new MyLineRenderer(
								IntentionGroupResources.Instance.GetMaterialOfLine(
									IntentionGroupResources.Instance.GetLineTypeShouldBe(
										circles[circles.Count-2].Type, circleType_))));
			}
		}
		
		RecheckType();
	}
	
	// Warning : Almost the same with function "Clear()"
	public void EndDrawing(){
		for (int i=circles.Count-1; i>0; i--){
			if(i==circles.Count-1){
				circles[i].FadeOutAndClear();
			}else{
				circles[i].Clear();
			}
			circles.RemoveAt(i);
		}
		for (int i=lines.Count-1; i>=0; i--){
			lines[i].EndDrawing();
		}
		lines.Clear();
		RecheckType();
	}
	
	// Warning : Almost the same with function "EndDrawing()"
	public void Clear(){
		for (int i=circles.Count-1; i>=0; i--){
			if(i==circles.Count-1 && i >0){
				circles[i].FadeOutAndClear();
			}else{
				circles[i].Clear();
			}
		}
		circles.Clear();
		for (int i=lines.Count-1; i>=0; i--){
			lines[i].EndDrawing();
		}
		lines.Clear();
	}
	
	// Privates 
		
	private void UpdateCircles(){
		for (int i=0; i<circles.Count; i++){
			circles[i].Update();
		}
	}
	private void UpdateLines(){
		for (int i=0; i<lines.Count; i++){
			Vector3 pos1 = new Vector3(circles[i].Position.x, circles[i].Position.y, StaticData.lineLayer);
			Vector3 pos2 = new Vector3(circles[i+1].Position.x, circles[i+1].Position.y, StaticData.lineLayer);
			lines[i].DrawMoving(pos1, pos2);
		}
	}
	
	private bool CheckIsCanAddTarget(CircleType circleType_){
		return !(0 == circles.Count 
				|| MyCircleRenderer.TYPE.FINGER != circleType_
				|| MyCircleRenderer.TYPE.FINGER == circles[circles.Count-1].Type);
	}
	private bool CheckIsCanAddTarget(CircleType circleType_, GameObject trackingTarget_){
		return (CheckIsNotExist(trackingTarget_)
				 && !CheckIsEnemyFirst(circleType_)
				 && CheckIsGroupValid(circleType_)
				 && !CheckIsTooManyEnemies(circleType_)
				 && !CheckIsAnyEnemyAlreadyIn());
	}
	private bool CheckIsNotExist(GameObject obj){
		for (int i=0; i<circles.Count; i++){
			if (null != circles[i].TrackingTarget
				&& obj.Equals(circles[i].TrackingTarget))
				return false;
		}
		return true;
	}
	private bool CheckIsEnemyFirst(CircleType circleType_){
		return (0 == circles.Count 
				&& MyCircleRenderer.TYPE.ENEMY == circleType_);
	}
	private bool CheckIsGroupValid(CircleType circleType_){
		return (2 != circles.Count
				|| CircleType.ENEMY == circleType_
				|| 2 == circles.Count
				&& MyCircleRenderer.TYPE.PLAYER == circles[0].Type
				&& circles[0].Type == circleType_);
	}
	private bool CheckIsTooManyEnemies(CircleType circleType_){
		return (circles.Count > 1
				&& MyCircleRenderer.TYPE.ENEMY == circleType_
				&& circleType_ == circles[circles.Count-1].Type);
	}
	private bool CheckIsAnyEnemyAlreadyIn(){
		for (int i=0; i<circles.Count; i++){
			if (null != circles[i].TrackingTarget
				&& CircleType.ENEMY == circles[i].Type)
				return true;
		}
		return false;
	}
	private bool CheckIsTargetCanReplaceByFinger(CircleType circleType_){
		return (MyCircleRenderer.TYPE.ENEMY == circles[circles.Count-1].Type
				|| 2 == circles.Count 
					&& MyCircleRenderer.TYPE.PLAYER == circles[circles.Count-1].Type);
	}
	
	private void RecheckType(){
		if (circles.Count < 2){
			type = TYPE.NONE;
		}
		else 
		if (circles.Count < 3){
			if (CircleType.PLAYER == circles[0].Type){
				if      (CircleType.PLAYER == circles[1].Type) type = TYPE.P2P;
				else if (CircleType.FINGER == circles[1].Type) type = TYPE.P2F;
				else if (CircleType.ENEMY  == circles[1].Type) type = TYPE.P2E;
			}
			else
			if (CircleType.GROUP == circles[0].Type){
				if      (CircleType.FINGER == circles[1].Type) type = TYPE.G2F;
				else if (CircleType.ENEMY  == circles[1].Type) type = TYPE.G2E;
			}
		}
		else 
		if (circles.Count < 4){
			if (TYPE.P2P == Type){
				if      (CircleType.FINGER == circles[2].Type) type = TYPE.P2P2F;
				else if (CircleType.ENEMY  == circles[2].Type) type = TYPE.P2P2E;
			}
		}
	}
}