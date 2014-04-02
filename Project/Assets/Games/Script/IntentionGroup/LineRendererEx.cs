using UnityEngine;
using System.Collections;

public static class LineRendererEx {
	
	/// <summary>
	/// Init the specified line. \ndefault: VertexCount is 2, and with is [180.0f,180.0f](start to end)
	/// </summary>
	/// <param name='material'>
	/// Material.
	/// </param>
	public static LineRenderer Init(this LineRenderer line, Material material){
		return line.Init(material, 2, new Vector2(180,180));
	}
	/// <summary>
	/// Init the specified line. \ndefault: with is [180.0f,180.0f](start to end)
	/// </summary>
	/// <param name='material'>
	/// Material.
	/// </param>
	/// <param name='vertextCount'>
	/// Vertext count.
	/// </param>
	public static LineRenderer Init(this LineRenderer line, Material material, int vertextCount){
		return line.Init(material, vertextCount, new Vector2(180,180));
	}
	/// <summary>
	/// Init the specified line
	/// </summary>
	/// <param name='material'>
	/// Material.
	/// </param>
	/// <param name='vertextCount'>
	/// Vertext count.
	/// </param>
	/// <param name='width'>
	/// Vector2(start, end)
	/// </param>
	public static LineRenderer Init(this LineRenderer line, Material material, int vertextCount, Vector2 width){
		line.SetVertexCount(vertextCount);
		line.SetWidth(width.x, width.y);
		line.material = material;
		return line;
	}
	
	/// <summary>
	/// Starts the drawing. Set the first 2 point to 'startVc3', and enable this line
	/// </summary>
	/// <param name='line'>
	/// Line.
	/// </param>
	/// <param name='startVc3'>
	/// Start vc3.
	/// </param>
	public static void StartDrawing (this LineRenderer line, Vector3 startVc3 ){
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
	public static Vector3 DrawMoving (this LineRenderer line, Vector3 startVc3, Vector3 endVc3){
		line.enabled = true;
		
		line.SetPosition(0, startVc3);
		line.SetPosition(1, BattleBg.CorrectingEndPointToFingerBounds(endVc3));
		
		return endVc3;
	}
	
	/// <summary>
	/// Ends the drawing. Enable line false.
	/// </summary>
	public static void EndDrawing(this LineRenderer line){
		if(line){
			line.enabled = false;
		}
	}
}
