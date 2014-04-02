using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class BarrierMapData : Singleton<BarrierMapData> {
	
	public static bool Enable = true;
	
	private char[,] curMap = null;
	private List<Point> availabePoints = new List<Point>();
	private Point mapSize = new Point();
	private Vector2 cellSize = new Vector2(0,0);
	private List<List<Rect>> rects = new List<List<Rect>>();
	private List<GameObject> circle = new List<GameObject>();
	
	public void CalculateGrid(){
		string s = string.Format("DynamicBackground/L{0}_{1}(Clone)/textureBG_down", 
			MapMgr.Instance.currentChapterIndex, MapMgr.Instance.currentLevelIndex);
		Debug.Log(s);
		GameObject bgTexture = GameObject.Find(s);
		Vector2 bgSize = new Vector2(bgTexture.transform.lossyScale.x, bgTexture.transform.lossyScale.y);
		
		cellSize.Set(bgSize.x/(mapSize.y), bgSize.y/(mapSize.x));
		rects.Clear();
		for (int i=0; i<mapSize.x; i++){
			List<Rect> rows = new List<Rect>();
			Vector2 topLeft = new Vector2(-(bgSize.x/2f), bgSize.y/2f-cellSize.y*(i+1)+bgTexture.transform.position.y);
			for (int j=0; j<mapSize.y; j++){
				rows.Add(new Rect(topLeft.x+cellSize.x*j, topLeft.y, cellSize.x, cellSize.y));
			}
			rects.Add(rows);
		}
	}
	
	public Point TranslatePosToIndex(Vector2 pos){
		Point index = Point.Invalid;
		
		float minestDistance = float.MaxValue;
		Point theBestChoice = new Point();
		for (int i=0; i<rects.Count; i++){
			for (int j=0; j<rects[0].Count; j++){
				Rect rect = rects[i][j];
				if (rect.Contains(pos)){
					index = new Point(i,j);
					j = rects[i].Count;
					i = rects.Count;
				}
				else if ('0' != curMap[i,j]){
					float distance = Vector2.Distance(pos, rect.center);
					if (distance < minestDistance){
						theBestChoice.Set(i,j);
						minestDistance = distance;
					}
				}
			}
		}
		if (!index.IsValid){
			index = theBestChoice;
		}
		
		return index;
	}
	
	public Point GetClosestValidPointNearEndPoint(Point startPoint, Point endPoint){
		Point result = endPoint;
		if ('0' == curMap[endPoint.x, endPoint.y]){
			Point dValue = endPoint - startPoint;
			int max = Mathf.Abs((Mathf.Abs(dValue.x) > Mathf.Abs(dValue.y)? 
									dValue.x: dValue.y));
			Vector2 lerp = new Vector2((float)dValue.x/max, (float)dValue.y/max);
			
			for (int i=1; i<=max; i++){
				Point checkPoint = new Point(Mathf.CeilToInt(endPoint.x - lerp.x*i),
												Mathf.CeilToInt(endPoint.y - lerp.y*i));
				Point checkPoint2 = new Point((int)(endPoint.x - lerp.x*i),
												(int)(endPoint.y - lerp.y*i));
				bool reachable  = '0' != curMap[checkPoint.x, checkPoint.y];
				bool reachable2 = '0' != curMap[checkPoint2.x, checkPoint2.y];
				if (reachable || reachable2){
					if (reachable && reachable2){
						result = (checkPoint.Distance(endPoint) < checkPoint2.Distance(endPoint))?
								checkPoint: checkPoint2;
					}
					else{
						result = reachable? checkPoint: checkPoint2;
					}
					i = max + 1;
				}
			}
		}
		
		return result;
	}
	
	public Vector2 GetClosestValidPositionOnTheEdge(Vector2 pos){
		Vector2 difValue = rects[0][0].center - pos;
		Point posIndex = new Point((int)(difValue.y/cellSize.y+.5f), (int)(-difValue.x/cellSize.x+.5f));
		Point validPos = new Point(posIndex.x, posIndex.y);
		Vector2 result = pos;
		
		if (posIndex.x < 0 || posIndex.x > mapSize.x-1
			|| posIndex.y < 0 || posIndex.y > mapSize.y-1
			|| '0' == curMap[posIndex.x,posIndex.y]){
			validPos.y = Mathf.Clamp(posIndex.y, 0, mapSize.y-1);
			for (int i=0; i<=mapSize.x; i++){
				int const_sign = (validPos.x > mapSize.x / 2)? 1: -1;
				int sign = const_sign;
				do {
					validPos.x = Mathf.Clamp(posIndex.x + sign*i, 0, mapSize.x-1);
					if ('0' != curMap[validPos.x, validPos.y]){
						sign = const_sign*-1;
						i = mapSize.x;
						result.Set(rects[validPos.x][validPos.y].center.x, rects[validPos.x][validPos.y].center.y);
						result.SetX(result.x + (0 == validPos.x? -cellSize.x: cellSize.x));
					}
					sign *= -1;
				}while(sign != const_sign);
			}
		}
		return result;
	}
	
	public Vector2 GetPointInScreen(){
		Point point = availabePoints[Random.Range(0, availabePoints.Count)];
		return TranslateIndexToPos(point);
	}
	
	public bool IsThePositionValid(Vector2 pos){
		bool result = false;
		Point target = TranslatePosToIndex(pos);
		if (!(target.x < 0 || target.x >= mapSize.x
			|| target.y < 0 || target.y >= mapSize.y)
			&& '0' != curMap[target.x,target.y]){
			result = true;
		}
		return result;
		// return (availabePoints.Contains(TranslatePosToIndex(pos)));
	}
	
	public void Load(string fileName){
		Debug.Log("fileName "+string.Format("MapBarrierInfo/{0}",fileName));
		TextAsset txtMap = Resources.Load(string.Format("MapBarrierInfo/{0}",fileName)) as TextAsset;
		string[] mapRows = txtMap.text.Split('\n');
		
		mapSize.Set(mapRows.Length, mapRows[0].Length);
		curMap = new char[mapSize.x, mapSize.y];
		availabePoints.Clear();
		for (int i=0; i<mapSize.x; i++){
			for (int j=0; j<mapSize.y; j++){
				curMap[i,j] = mapRows[i][j];
				if ('0' != curMap[i,j]){
					availabePoints.Add(new Point(i,j));
				}
			}
		}
	}
	
	public List<Vector2> GetPath(Vector2 starPos, Vector2 endPos){
		// List<Point> pointPath = GetPath(TranslatePosToIndex(starPos), 
		// 							TranslatePosToIndex(endPos));
		Point []points = {TranslatePosToIndex(starPos), TranslatePosToIndex(endPos)};
		Point closestPoint = GetClosestValidPointNearEndPoint(points[0], points[1]);
		List<Point> pointPath = GetPath(points[0], closestPoint);
		List<Vector2> realPath = new List<Vector2>();
		
		for (int i=0; i<pointPath.Count; i++){
			realPath.Add(TranslateIndexToPos(pointPath[i]));
		}
		if (pointPath.Count > 0){
			realPath[realPath.Count-1] += (endPos - rects[points[1].x][points[1].y].center);
		}
		
		return realPath;
	}
	
	public Vector2 OffsetBtweenCircleCenterAndTargetPos(Vector2 targetPos){
		Point targetPoint = TranslatePosToIndex(targetPos);
		return (targetPos - rects[targetPoint.x][targetPoint.y].center);
	}
	
	public void PrintMap(){
		StringBuilder paper = new StringBuilder();
		
		for (int i=0; i<mapSize.x; i++){
			for (int j=0; j<mapSize.y; j++){
				paper.Append(curMap[i,j]);
			}
			paper.Append('\n');
		}
		
		Debug.LogError(paper);
	}
	
	public void ShowGrid(){
		Object prefab = Resources.Load("BarrierRect");
		ReleaseGrid();
		for (int i=0; i<rects.Count; i++){
			for (int j=0; j<rects[i].Count; j++){
				if ('0' == curMap[i,j]){
					Rect rect = rects[i][j];
					GameObject obj = Instantiate(prefab) as GameObject;
					PackedSprite sp = obj.GetComponent<PackedSprite>();
					obj.transform.localScale = new Vector3(rect.width/sp.width, rect.height/sp.height, 1);
					circle.Add(obj);
					circle[circle.Count-1].transform.position = rect.center;
				}
			}
		}
	}

	public void ReleaseGrid(){
		for (int i=0; i<circle.Count; i++){
			GameObject obj = circle[i];
		    DestroyObject(obj);
		}
		circle.Clear();
	}
	
	
	#region Private Functions
	
	private Vector2 TranslateIndexToPos(Point index){
		if (index.x < 0 || index.x >= rects.Count
			|| index.y < 0 || index.y >= rects[0].Count){
			Debug.LogError (string.Format("TranslateIndexToPos out of range:[{0}]", index));
		}
		return rects[index.x][index.y].center;
	}
	
	private List<Point> GetPath(Point starPos, Point endPos){
		AStar a = new AStar();
		a.Map = curMap;
		a.MAP_SIZE = mapSize;
		
		List<Point> path = a.Begin(starPos, endPos);
		a.PrintFinalPath(path);
		
		return path;
	}
	#endregion
}
