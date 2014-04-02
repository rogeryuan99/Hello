// #define TEST_ASTAR

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;


public class AStar {
	
	private class Node{
		public Node Parent = null;
		public float G;
		public float H;
		public float F;
		public Point pos;
		
		public Node(Point pos_, float G_, Point fromPos, Point toPos){
			pos = pos_;
			G = G_ + pos.Distance(fromPos);
			H = pos.Distance(toPos);
			F = G + H;
		}
		
		public override bool Equals (object obj){
			bool result = false;
			
			if (obj is Node){
				Node node_ = obj as Node;
				result = pos.Equals(node_.pos);
			}
			else if (obj is Point){
				Point pos_ = obj as Point;
				result = pos.Equals(pos_);
			}
			
			return result;
		}
	}
	
#if TEST_ASTAR
	public char[,] Map = {
		{'1','1','1','1','1','1','1','1','1'},
		{'1','0','0','0','0','1','1','1','1'},
		{'1','1','1','1','0','1','1','1','1'},
		{'1','1','1','1','0','1','1','1','1'},
		{'1','1','1','1','0','1','1','1','1'},
		{'1','1','1','1','0','1','1','1','1'},
		{'1','1','1','1','0','1','1','1','1'},
		{'1','1','0','0','0','1','1','1','1'},
		{'1','1','1','1','1','1','1','1','1'}};
	
	
	public Point MAP_SIZE = new Point(9,9);
	public Point S = new Point(4,1);
	public Point E = new Point(4,7);
#else
	public char[,] Map{ get; set; }
	public Point MAP_SIZE{ get; set; }
	public Point S{ get; set; }
	public Point E{ get; set; }
#endif
	
	private const int ERROR_MARK = 2;
	private List<Node> OpenNodes = new List<Node>();
	private List<Node> CloseNodes = new List<Node>();
	
	public List<Point> Begin(Point startPos, Point endPos){
		if (null == Map) return null;
		
		OpenNodes.Clear(); CloseNodes.Clear();
		S = startPos; E = endPos;
		OpenNodes.Add(new Node(S, 0, S, E));
		Node node = null;
		do{
			node = GetMinestNodeFromOpenNodes();
			CloseNodes.Add(node);
			OpenNodes.Remove(node);
			
			ProcessNode(node);
		}while(!node.pos.Equals(E) && OpenNodes.Count > 0);
		
#if TEST_ASTAR
		List<Point> finalPath = GetFinalPath(node);
		PrintFinalPath(finalPath);
		
		return finalPath;
#else
		return GetFinalPath(node);
#endif
	}
	
	public void PrintFinalPath(List<Point> path){
		if (null == path || 0 == path.Count){
			//Debug.LogError(string.Format("Dead End. S={0}, E={1}", S, E));
		}
		else{
			/*char mark = 'a';
			for (int i=0; i<path.Count; i++){
				Map[path[i].x, path[i].y] = mark++;
			}
			PrintMap();*/
		}
	}
	
	private void ProcessNode(Node node){
		List<int> lockedI = new List<int>();
		List<int> lockedJ = new List<int>();
		for (int i=-1; i<=1; i++){
			for (int j=-1; j<=1; j++){
				Node target = new Node(new Point(node.pos.x+i, node.pos.y+j), node.G, node.pos, E);
				if (CloseNodes.Contains(target)
					|| target.pos.x<0 || target.pos.x>MAP_SIZE.x-1
					|| target.pos.y<0 || target.pos.y>MAP_SIZE.y-1
					|| lockedI.Contains(i) || lockedJ.Contains(j)){
					continue;
				}
				if ('0' == Map[target.pos.x, target.pos.y]){
					if (i != j){
						if (0==i) lockedJ.Add(j);
						else
						if (0==j) lockedI.Add(i);
					}
					continue;
				}
				if (!OpenNodes.Contains(target)){
					target.Parent = node;
					OpenNodes.Add(target);
				}
				else{
					target = OpenNodes.Find((n)=>{
						return (n.pos.Equals(target.pos));;
					});
					float finalG = node.G + node.pos.Distance(target.pos);
					if (finalG < target.G){
						target.Parent = node;
						target.G = finalG;
						target.F = target.G + target.H;
					}
				}
			}
		}
	}
	
	private List<Point> GetFinalPath(Node node){
		List<Point> path = new List<Point>();
		
		if (node.pos.Equals(E)){
			Point markPoint = node.pos;
			path.Add(node.pos);
			while(!node.pos.Equals(S)){
				if (IsKeyPoint(markPoint, node.Parent.pos)){
					path.Add(node.pos);
					markPoint = node.Parent.pos;
				}
				Map[node.pos.x, node.pos.y] = '-';
				node = node.Parent;
			}
			
			path.Reverse();
		}
		
		return path;
	}
	
	private bool IsKeyPoint(Point markPoint, Point nextPoint){
		bool result = false;
		Point dValue = nextPoint - markPoint;
		int max = Mathf.Abs((Mathf.Abs(dValue.x) > Mathf.Abs(dValue.y)? 
								dValue.x: dValue.y));
		Vector2 lerp = new Vector2((float)dValue.x/max, (float)dValue.y/max);
		
		for (int i=1; i<=max; i++){
			Point checkPoint = new Point(Mathf.CeilToInt(nextPoint.x - lerp.x*i),
											Mathf.CeilToInt(nextPoint.y - lerp.y*i));
			Point checkPoint2 = new Point((int)(nextPoint.x - lerp.x*i),
											(int)(nextPoint.y - lerp.y*i));
			if ('0' == Map[checkPoint.x, checkPoint.y]
				|| '0' == Map[checkPoint2.x, checkPoint2.y]){
				result = true;
				i = max+1;
			}
		}
		
		return result;
	}
	
	private Node GetMinestNodeFromOpenNodes(){
		Node minFNode = null;
		
		for (int i=0; i<OpenNodes.Count; i++){
			if (null == minFNode || minFNode.F > OpenNodes[i].F){
				minFNode = OpenNodes[i];
			}
		}
		
		return minFNode;
	}
	
	private void PrintMap(){
		StringBuilder paper = new StringBuilder("Result: \n");
		
		for (int i=0; i<MAP_SIZE.x; i++){
			for (int j=0; j<MAP_SIZE.y; j++){
				paper.Append(Map[i,j]);
			}
			paper.Append('\n');
		}
		
		Debug.LogError(paper);
	}
}
