using UnityEngine;
using System.Collections;

public class drawFrame : MonoBehaviour {


public int width;
public int height;

public GameObject edge_top;
public PackedSprite edge_top_sprite;
public GameObject edge_bottom;
public PackedSprite edge_bottom_sprite;
public GameObject edge_left;
public PackedSprite edge_left_sprite;
public GameObject edge_right;
public PackedSprite edge_right_sprite;

public GameObject corner_LT;
public GameObject corner_RT;
public GameObject corner_LB;
public GameObject corner_RB;

public SimpleSprite middleSprite;

public int CORNERSIZE;

[HideInInspector]
public int edgeWidth;
[HideInInspector]
public int edgeHeight;

public void Start (){//lefttop: (-w, h)
	setSize(width, height);
	//setSize(0,0);
}

public void setBGColor (){
	BGMeshColor mc = middleSprite.gameObject.GetComponent<BGMeshColor>();
	if (mc) {
		mc.setColor();
	}
}

public void resetSize (){
	float targetW= (width < 2*CORNERSIZE)?2*CORNERSIZE:width;
	float targetH= (height < 2*CORNERSIZE)?2*CORNERSIZE:height;
	middleSprite.SetSize(targetW - CORNERSIZE*2 + 4, targetH - CORNERSIZE*2 + 4);
}

public void setSize ( float w ,   float h  ){
	float targetW= (w < 2*CORNERSIZE)?2*CORNERSIZE:w;
	float targetH= (h < 2*CORNERSIZE)?2*CORNERSIZE:h;
	
	float edgeHorizontalPos = (targetW - CORNERSIZE)/2;
	float edgeVerticalPos = (targetH - CORNERSIZE)/2;
	
	ExtensionMethods.SetX(edge_top.transform.localPosition, 0);
	ExtensionMethods.SetY(edge_top.transform.localPosition, edgeVerticalPos);	
//	edge_top.transform.localPosition.x = 0;
//	edge_top.transform.localPosition.y = edgeVerticalPos;
	edge_top_sprite.SetSize(targetW-2*CORNERSIZE, CORNERSIZE);
	
	ExtensionMethods.SetX(edge_bottom.transform.localPosition, 0);
	ExtensionMethods.SetY(edge_bottom.transform.localPosition, -edgeVerticalPos);
//	edge_bottom.transform.localPosition.x = 0;
//	edge_bottom.transform.localPosition.y = -edgeVerticalPos;
	edge_bottom_sprite.SetSize(targetW-2*CORNERSIZE, CORNERSIZE);
		
	ExtensionMethods.SetX(edge_left.transform.localPosition, -edgeHorizontalPos);
	ExtensionMethods.SetY(edge_left.transform.localPosition, 0);
//	edge_left.transform.localPosition.x = -edgeHorizontalPos;
//	edge_left.transform.localPosition.y = 0;
	edge_left_sprite.SetSize(targetH-2*CORNERSIZE, CORNERSIZE);
	
	ExtensionMethods.SetX(edge_right.transform.localPosition, edgeHorizontalPos);
	ExtensionMethods.SetY(edge_right.transform.localPosition, 0);
//	edge_right.transform.localPosition.x = edgeHorizontalPos;
//	edge_right.transform.localPosition.y = 0;
	edge_right_sprite.SetSize(targetH-2*CORNERSIZE, CORNERSIZE);
	
	ExtensionMethods.SetX(corner_LT.transform.localPosition, -edgeHorizontalPos);
	ExtensionMethods.SetY(corner_LT.transform.localPosition, edgeVerticalPos);
//	corner_LT.transform.localPosition.x = -edgeHorizontalPos;
//	corner_LT.transform.localPosition.y = edgeVerticalPos;
	
	ExtensionMethods.SetX(corner_RT.transform.localPosition, edgeHorizontalPos);
	ExtensionMethods.SetY(corner_RT.transform.localPosition, edgeVerticalPos);
//	corner_RT.transform.localPosition.x = edgeHorizontalPos;
//	corner_RT.transform.localPosition.y = edgeVerticalPos;
	
	ExtensionMethods.SetX(corner_LB.transform.localPosition, -edgeHorizontalPos);
	ExtensionMethods.SetY(corner_LB.transform.localPosition, -edgeVerticalPos);
//	corner_LB.transform.localPosition.x = -edgeHorizontalPos;
//	corner_LB.transform.localPosition.y = -edgeVerticalPos;
	
	ExtensionMethods.SetX(corner_RB.transform.localPosition, edgeHorizontalPos);
	ExtensionMethods.SetY(corner_RB.transform.localPosition, -edgeVerticalPos);
//	corner_RB.transform.localPosition.x = edgeHorizontalPos;
//	corner_RB.transform.localPosition.y = -edgeVerticalPos;
	middleSprite.SetSize(targetW - CORNERSIZE*2 + 4, targetH - CORNERSIZE*2 + 4);
	setBGColor();
}

public void Update (){

}
}