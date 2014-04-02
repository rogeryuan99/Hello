using UnityEngine;
using System.Collections;

public class CDMaskRect : MonoBehaviour {
private Mesh mesh;
private float originalW;
private float originalH;
	
	public float diameter =126; 

public void Awake (){
//	mesh = this.gameObject.GetComponent<MeshFilter>().mesh;
//	print(this.renderer.bounds.size);
//	buildMesh(82,82);
//	buildMesh(this.renderer.bounds.size.x,this.renderer.bounds.size.y);
}

public void buildMesh ( float w ,   float h  ){
//	originalW = w;
//	originalH = h;
//	
//	Vector3[] vts = new Vector3[4];
//	Vector2[] uvs = new Vector2[4];
//	int[] triangles  = new int[6];
//	
//	vts[0] = new Vector3(-w/2, h/2, 0);
//	vts[1] = new Vector3(w/2,  h/2, 0);
//	vts[2] = new Vector3(w/2, -h/2,0);
//	vts[3] = new Vector3(-w/2,  -h/2,0);
//	
//	uvs[0] = new Vector2(0,1);
//	uvs[1] = new Vector2(1,1);
//	uvs[2] = new Vector2(1,0);
//	uvs[3] = new Vector2(0,0);
//	
//	triangles[0] = 0;
//	triangles[1] = 1;
//	triangles[2] = 2;
//	triangles[3] = 0;
//	triangles[4] = 2;
//	triangles[5] = 3;
//		
//	mesh.Clear();
//	mesh.name= "skcooldown";
//	mesh.vertices = vts;
//	mesh.uv = uvs;
//	mesh.triangles = triangles;
//	mesh.RecalculateNormals();
}

public void updateMesh ( float w ,   float h  ){
	//add by xiaoyong 20130621 for mesh missing
//	if(mesh == null)
//	{
//		mesh = this.gameObject.GetComponent<MeshFilter>().mesh;
//		buildMesh(this.renderer.bounds.size.x,this.renderer.bounds.size.y);
//	}
//	Vector3[] vts = mesh.vertices;
//	Vector2[] uvs = mesh.uv;
//	
//	vts[2] = new Vector3(w/2, originalH/2-h, 0);
//	vts[3] = new Vector3(-w/2,  originalH/2-h, 0);
//	
//	uvs[2] = new Vector2(1,(originalH-h)/originalH);
//	uvs[3] = new Vector2(0,(originalH-h)/originalH);
//	
//	mesh.vertices = vts;
//	mesh.uv = uvs;
		UISprite s = gameObject.GetComponent<UISprite>();
		s.fillAmount = h / diameter;
}

public void Update (){
	
}
}