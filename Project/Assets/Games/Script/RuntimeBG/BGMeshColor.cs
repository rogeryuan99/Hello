using UnityEngine;
using System.Collections;

public class BGMeshColor : MonoBehaviour {


public Color32 topColor;
public Color32 bottomColor;

public void Start (){
	//setColor();
   
}

public void setColor (){
	Mesh mesh = GetComponent<MeshFilter>().mesh;
    Vector3[] vertices = mesh.vertices;
    Color[] colors = new Color[vertices.Length];
//    Debug.Log("vertice count: " + vertices.Length);
//    Debug.Log("color32 R:" +topColor.r + " G:" +topColor.g + " B:" +topColor.b + "A:" +topColor.a);

	//43,121,202
	//Color32 lightBlue = new Color32(43,121,202,255);

    for (int i = 0; i < vertices.Length;i++) {
    	//float value = 
        colors[i] = Color.Lerp(
				bottomColor, 
				topColor, 
				vertices[i].y * 2);
        //Debug.Log("vertice y: " + vertices[i].y);
    }

    mesh.colors = colors;
}

public void resetColor ( Color32 topColor ,  Color32 bottomColor  ){
	this.topColor = topColor;
	this.bottomColor = bottomColor;
	setColor();
}

public void Update (){

}
}