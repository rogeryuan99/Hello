using UnityEngine;
using System.Collections;

public class MyLineRenderer {

	public enum TYPE{
		H2H
	}
	
	private LineRenderer line;
	private TYPE type;
	
	public TYPE Type{
		get{
			return type;
		}
		set{
			if (value == type) return;
			
			type = value;
			line.material = IntentionGroupResources.Instance.GetMaterialOfLine(type);
		}
	}
	
	public MyLineRenderer(Material material):this(material, new Vector2(180,180)){}
	public MyLineRenderer(Material material, Vector2 width){
		if (null == line){
			line = (new GameObject("Line")).AddComponent<LineRenderer>();
		}
		line.SetVertexCount(2);
		line.SetWidth(width.x, width.y);
		line.material = material;
	}
	
	/// <summary>
	/// Starts the drawing. Set the first 2 point to 'startVc3', and enable this line
	/// </summary>
	/// <param name='line'>
	/// 
	/// </param>
	/// <param name='startVc3'>
	/// Start vc3.
	/// </param>
	public void StartDrawing (Vector3 startVc3 ){
		line.SetPosition(0,startVc3);
		line.SetPosition(1,startVc3);
		line.enabled = true;
	}
	
	/// <summary>
	/// Draws the line move.
	/// </summary>
	/// <returns>
	/// Vector3 object "movePosition"
	/// </returns>
	/// <param name='startVc3'>
	/// Start vector3.
	/// </param>
	/// <param name='endVc3'>
	/// End vector3.
	/// </param>
	public Vector3 DrawMoving (Vector3 startVc3, Vector3 endVc3){
		line.enabled = true;
		
		line.SetPosition(0, startVc3);
		if ("TestIntentionGroup" == Application.loadedLevelName)	// "if" only used in TestIntentionGroup Scene
			line.SetPosition(1, endVc3);
		else
			line.SetPosition(1, BattleBg.CorrectingEndPointToFingerBounds(endVc3));
		
		return endVc3;
	}
	
	/// <summary>
	/// Ends the drawing. Destroy line object.
	/// </summary>
	public void EndDrawing(){
		MonoBehaviour.DestroyObject(line.gameObject);
	}
}
