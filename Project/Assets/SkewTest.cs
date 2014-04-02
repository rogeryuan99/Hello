using UnityEngine;
using System.Collections;

public class SkewTest : MonoBehaviour {
	public PackedSprite sprite;
	void OnGUI(){
		if(GUILayout.Button("Skew")){
			Vector3[] vertices = sprite.GetVertices();	
			for(int n = 0;n<vertices.Length;n++){
				Debug.Log(vertices[n]);	
				vertices[n] =new Vector3(
					Vector3.Dot(vertices[n],new Vector3(1f,0f,0)),
					Vector3.Dot(vertices[n],new Vector3(0.1f,1f,0)),
					Vector3.Dot(vertices[n],new Vector3(0,0,1f)));
//				vertices[n] += new Vector3(10,0,0);
				Debug.Log("--> "+vertices[n]);	
			}
			SpriteMesh_Managed sm_m = (SpriteMesh_Managed)sprite.spriteMesh;
			sm_m.myVertices = vertices;
		}
		if(GUILayout.Button("+")){
			Vector3[] vertices = sprite.GetVertices();	
			for(int n = 0;n<vertices.Length;n++){
				Debug.Log(vertices[n]);	
				vertices[n] =new Vector3(
					Vector3.Dot(vertices[n],new Vector3(2f,0f,0)),
					Vector3.Dot(vertices[n],new Vector3(0f,2f,0)),
					Vector3.Dot(vertices[n],new Vector3(0,0,1f)));
//				vertices[n] += new Vector3(10,0,0);
				Debug.Log("--> "+vertices[n]);	
			}
			SpriteMesh_Managed sm_m = (SpriteMesh_Managed)sprite.spriteMesh;
			sm_m.myVertices = vertices;
		}
		if(GUILayout.Button("-")){
			Vector3[] vertices = sprite.GetVertices();	
			for(int n = 0;n<vertices.Length;n++){
				Debug.Log(vertices[n]);	
				vertices[n] =new Vector3(
					Vector3.Dot(vertices[n],new Vector3(0.5f,0f,0)),
					Vector3.Dot(vertices[n],new Vector3(0f,0.5f,0)),
					Vector3.Dot(vertices[n],new Vector3(0,0,1f)));
//				vertices[n] += new Vector3(10,0,0);
				Debug.Log("--> "+vertices[n]);	
			}
			SpriteMesh_Managed sm_m = (SpriteMesh_Managed)sprite.spriteMesh;
			sm_m.myVertices = vertices;
		}
		if(GUILayout.Button("-")){
			Vector3[] vertices = sprite.GetVertices();	
			for(int n = 0;n<vertices.Length;n++){
				Debug.Log(vertices[n]);	
				vertices[n] =new Vector3(
					Vector3.Dot(vertices[n],new Vector3(0.5f,0f,0)),
					Vector3.Dot(vertices[n],new Vector3(0f,0.5f,0)),
					Vector3.Dot(vertices[n],new Vector3(0,0,1f)));
//				vertices[n] += new Vector3(10,0,0);
				Debug.Log("--> "+vertices[n]);	
			}
			SpriteMesh_Managed sm_m = (SpriteMesh_Managed)sprite.spriteMesh;
			sm_m.myVertices = vertices;
		}
		if(GUILayout.Button("++")){
			sprite.gameObject.transform.localScale *= 2;
		}
		if(GUILayout.Button("--")){
			sprite.gameObject.transform.localScale *= 0.5f;
		}
		if(GUILayout.Button("(")){
			sprite.gameObject.transform.Rotate( new Vector3(0,0,10));
		}
		if(GUILayout.Button(")")){
			sprite.gameObject.transform.Rotate( new Vector3(0,0,-10));
		}
	}
}
