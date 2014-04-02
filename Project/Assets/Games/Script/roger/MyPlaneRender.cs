using UnityEngine;
using System.Collections;

public class MyPlaneRender : MonoBehaviour
{
	
	public Vector3[] newVertices;
	public Vector2[] newUV;
	public int[] newTriangles;
	public Material mat;
	
	// Use this for initialization
	void Start ()
	{
		complexMesh();
	}
	
	private void simpleMesh ()
	{
		Mesh mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;
		
		float w = 50;//1024
		float h = 80;//768
		
		newVertices = new Vector3[4];
		newUV = new Vector2[4];
		newTriangles = new int[6];

		newVertices [0] = new Vector3 (0, 0, 0);
		newVertices [1] = new Vector3 (w, 0, 0);
		newVertices [2] = new Vector3 (w, h, 0);
		newVertices [3] = new Vector3 (0, h, 0);
		
		newUV [0] = new Vector2 (0, 0);
		newUV [1] = new Vector2 (1, 0);
		newUV [2] = new Vector2 (1, 1);//test
		newUV [3] = new Vector2 (0, 1);
		
		newTriangles [0] = 0;
		newTriangles [1] = 2;
		newTriangles [2] = 1;
		
		newTriangles [3] = 0;
		newTriangles [4] = 3;
		newTriangles [5] = 2;
		
		mesh.Clear ();
		mesh.vertices = newVertices;
		mesh.uv = newUV;
		mesh.triangles = newTriangles;
		
		///*
		Vector3[] normals = new Vector3[newVertices.Length];
		for (int i = 0; i < newVertices.Length; i++) {
			normals [i] = Vector3.forward;
		}
		//*/
		mesh.normals = normals;
		//mesh.RecalculateNormals();
		
		gameObject.renderer.material = mat;
		gameObject.renderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;		
	}

	private void complexMesh ()
	{
		Mesh mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;
		int rects = 8;

		newVertices = new Vector3[4 * rects];
		newUV = new Vector2[4 * rects];
		newTriangles = new int[6 * rects];
		

		for (int n = 0; n<rects; n++) {
			float w = Random.Range(20,40);
			float h = Random.Range(20,40);
			float xx = Random.Range(-50,50);
			float yy = Random.Range(-50,50);
			newVertices [n*4+0] = new Vector3 (xx+0, yy+0, 0);
			newVertices [n*4+1] = new Vector3 (xx+w, yy+0, 0);
			newVertices [n*4+2] = new Vector3 (xx+w, yy+h, 0);
			newVertices [n*4+3] = new Vector3 (xx+0, yy+h, 0);
			
			float u = Random.Range(0,0.9f);
			float v = Random.Range(u,1f);
			newUV [n*4+0] = new Vector2 (0, 0);
			newUV [n*4+1] = new Vector2 (u, 0);
			newUV [n*4+2] = new Vector2 (u, v);//test
			newUV [n*4+3] = new Vector2 (0, v);
			
			newTriangles [n*6+0] = n*4+0;
			newTriangles [n*6+1] = n*4+2;
			newTriangles [n*6+2] = n*4+1;
			
			newTriangles [n*6+3] = n*4+0;
			newTriangles [n*6+4] = n*4+3;
			newTriangles [n*6+5] = n*4+2;			
		}
		
		
		mesh.Clear ();
		mesh.vertices = newVertices;
		mesh.uv = newUV;
		mesh.triangles = newTriangles;
		
		///*
		Vector3[] normals = new Vector3[newVertices.Length];
		for (int i = 0; i < newVertices.Length; i++) {
			normals [i] = Vector3.forward;
		}
		//*/
		mesh.normals = normals;
		//mesh.RecalculateNormals();
		
		gameObject.renderer.material = mat;
		gameObject.renderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;		
	}
	/*
function createWithMesh(vv:Vector3[], uv:Vector2[], color:Color[], tr:int[], mt:Material)
{
//	Debug.Log("---- create mesh object: vv length: " + vv.Length);
	var mesh:Mesh = new Mesh();
	GetComponent(MeshFilter).mesh = mesh;
	
	mesh.Clear();
	mesh.vertices = vv;
	mesh.uv = uv;
	mesh.colors = color;
	mesh.triangles = tr;
	gameObject.renderer.material = mt;
	
//	var normals:Vector3[] = new Vector3[vv.Length];
//	for(var i:int = 0; i < vv.Length; i++)
//	{
//		normals[i] = Vector3.forward;
//	}
//	mesh.RecalculateNormals();
//	var t:float = Time.realtimeSinceStartup;
//	Debug.Log("========vertices befor optimize="+mesh.vertices.Length);
//	mesh.Optimize();
//	Debug.Log("========vertices after optimize="+mesh.vertices.Length+"  cost time:"+(Time.realtimeSinceStartup-t) );


}
*/
}
