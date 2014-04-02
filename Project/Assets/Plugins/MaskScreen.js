#pragma strict

var vertices:Vector3[];
var uvs:Vector2[];
var triangles:int[];
var mat:Material;
var fillmat:Material;
var normals:Vector3[];

var maskPos:Vector2;
var maskSizeW:float = 50.0f;
var maskSizeH:float = 50.0f;

private var mx:float;
private var my:float;

private var c:Camera;

function Start () {
	// create mesh vertex
	c = Camera.main.camera;
	mx = maskPos.x;
	my = maskPos.y;
	
	createMask();
}

function changeMaskPositon(x:float, y:float) 
{
	maskPos = Vector2(x,y);
}

function adjustAnchor(w:float, h:float)
{
	var centerX:float = maskPos.x;
	var centerY:float = maskPos.y;
	var fz:float = 10.0f;
	
	var center:Vector3 = Vector3(centerX, centerY, 0);
//	center = matrix.MultiplyPoint3x4(center);
//	center.z = fz;
	
	var p:Vector3 = Vector3(Screen.width/2, Screen.height/2, 0);
	p = c.ScreenToWorldPoint(center);

//	Debug.Log(" Center Of Screen: " + p);
//	transform.position = Vector3(p.x, p.y, fz);
}

private var vv:Array;// = new Array();
private var tt:Array;// = new Array();
private var uv:Array;// = new Array();

function initArray()
{
	vv = new Array();
	tt = new Array();
	uv = new Array();

}

function createMask()
{
	var step:float = 11.0f;
	
	var baseY:float = 200.0f;
	
	var gridcount:float = 1.0f;
	
	var w:float = 0;
	var h:float = 0;
	var i:int = 0;
	var fz:float = 10;
	
	initArray();
	
		var x0:float = 0.0f + maskPos.x - maskSizeW / 2;
		var y0:float = 0.0f + maskPos.y - maskSizeH / 2;
		
		var x1:float = x0;
		var y1:float = maskPos.y + maskSizeH / 2;;
		
		var v0:Vector3 = Vector3(x0, y0, fz);
		v0 = c.ScreenToWorldPoint(v0);
		var v1:Vector3 = Vector3(x1, y1, fz);
		v1 = c.ScreenToWorldPoint(v1);
		
		var x2:float = maskSizeW / 2 + maskPos.x;
		var y2:float = y0;
		
		var x3:float = x2;
		var y3:float = y1;
		
		var v2:Vector3 = Vector3(x2, y2, fz);
		var v3:Vector3 = Vector3(x3, y3, fz);
		
		
		
		v2 = c.ScreenToWorldPoint(v2);
		
		v3 = c.ScreenToWorldPoint(v3);
		
//		v0.z = v1.z = v2.z = v3.z = 0;
		w = v2.x - v0.x;
		h = v1.y - v0.y;
		
		var triangleIndex:int = 0;
		
		var t:Array = [
			triangleIndex, triangleIndex + 1, triangleIndex + 3, triangleIndex, triangleIndex + 3, triangleIndex + 2
		];
		
		tt = tt.Concat(t);
		
		vv.Push(v0);
		vv.Push(v1);
		vv.Push(v2);
		vv.Push(v3);
		
		uv.Push(Vector2(0.0f, 0.0f));
		uv.Push(Vector2(0.0f, 1.0f));
		uv.Push(Vector2(1.0f, 0.0f));
		uv.Push(Vector2(1.0f, 1.0f));
		
		var sv0:Vector3 = Vector3(0, 0, 0);
		var sv1:Vector3 = Vector3(0, Screen.height, 0);
		var sv2:Vector3 = Vector3(Screen.width, 0, 0);
		var sv3:Vector3 = Vector3(Screen.width, Screen.height, 0);
		
		// rectangle 0
		var rx0:float = Mathf.Min(sv0.x, x0);
		var rx1:float = x0;
		var ry0:float = y0;
		var ry1:float = Mathf.Max(sv1.y, y1);
		
		triangleIndex = addRect(rx0, rx1, ry0, ry1, fz, triangleIndex + 4);
		
		// rectangle 1
		rx0 = x1;
		rx1 = Mathf.Max(sv2.x, x2);
		ry0 = y1;
		ry1 = Mathf.Max(sv1.y, y1);
		
		triangleIndex = addRect(rx0, rx1, ry0, ry1, fz, triangleIndex + 4);
		
		// rectangle 2
		rx0 = x2;
		rx1 = Mathf.Max(sv2.x, x2);
		ry0 = Mathf.Min(sv2.y, y2);
		ry1 = y3;
		
		triangleIndex = addRect(rx0, rx1, ry0, ry1, fz, triangleIndex + 4);
		
		// rectangle 3
		rx0 = Mathf.Min(sv0.x, x0);
		rx1 = x2;
		ry0 = Mathf.Min(sv0.y, y0);
		ry1 = y0;
		
		triangleIndex = addRect(rx0, rx1, ry0, ry1, fz, triangleIndex + 4);
	
	
	Debug.Log(tt.length);
	
	var mesh:Mesh = new Mesh();
	Debug.Log(mesh);
	GetComponent(MeshFilter).mesh = mesh;
	
	mesh.Clear();
	
	vertices = vv.ToBuiltin(Vector3);
	uvs = uv.ToBuiltin(Vector2);
	triangles = tt.ToBuiltin(int);

	mesh.vertices = vertices;
	mesh.uv = uvs;
	mesh.triangles = triangles;
	gameObject.renderer.material = mat;
	
	normals = new Vector3[vv.length];
	for(i = 0; i < vv.length; i++)
	{
		normals[i] = Vector3.forward;
	}
	mesh.normals = normals;
//	mesh.RecalculateNormals();
	mesh.RecalculateBounds();

	mesh.Optimize();
	
	
	//transform.gameObject.AddComponent(MeshCollider);
	//transform.GetComponent(MeshCollider).sharedMesh = mesh;

	adjustAnchor(w, h);
}

function addRect(rx0:float, rx1:float, ry0:float, ry1:float, fz:float, triangleIndex:int):int
{
	var rv0:Vector3 = c.ScreenToWorldPoint(Vector3(rx0, ry0, fz));
	var rv1:Vector3 = c.ScreenToWorldPoint(Vector3(rx0, ry1, fz));
	var rv2:Vector3 = c.ScreenToWorldPoint(Vector3(rx1, ry0, fz));
	var rv3:Vector3 = c.ScreenToWorldPoint(Vector3(rx1, ry1, fz));
	
	vv.Push(rv0);
	vv.Push(rv1);
	vv.Push(rv2);
	vv.Push(rv3);
	
	uv.Push(Vector2(0.0f, 0.0f));
	uv.Push(Vector2(0.0f, 0.1f));
	uv.Push(Vector2(0.1f, 0.0f));
	uv.Push(Vector2(0.1f, 0.1f));
	
	var t:Array = [triangleIndex, triangleIndex + 1, triangleIndex + 3, triangleIndex, triangleIndex + 3, triangleIndex + 2];
	tt = tt.Concat(t);
	
	return triangleIndex;
}

function updateMask()
{
	if(mx == maskPos.x && my == maskPos.y)
		return;
		
	createMask();
}

function Update () {
	updateMask();
}