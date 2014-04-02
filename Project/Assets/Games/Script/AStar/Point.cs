using UnityEngine;
using System.Collections;


public class Point{
	public int x = 0;
	public int y = 0;
	
	public bool IsValid{
		get{
			return !Equals(Point.Invalid);
		}
	}
	
	public Point(){}
	public Point(int x_, int y_){
		x = x_; y = y_;
	}
	public float Distance(Point target){
		return Mathf.Sqrt((x-target.x)*(x-target.x) + (y-target.y)*(y-target.y));
	}
	public bool Equals (Point target){
		return (this == target);
	}
	public void Set(int x_, int y_){
		x = x_; y = y_;
	}
	public override string ToString ()
	{
		return string.Format ("[{0},{1}]", x, y);
	}
	
	public static Point operator-(Point obj1, Point obj2){
		return new Point(obj1.x-obj2.x, obj1.y-obj2.y);
	}
	
	public static bool operator==(Point obj1, Point obj2){
		return (obj1.x == obj2.x && obj1.y == obj2.y);
	}
	
	public static bool operator!=(Point obj1, Point obj2){
		return (obj1.x != obj2.x || obj1.y != obj2.y);
	}
	
	public static implicit operator Point(Vector2 pos) {
		return new Point((int)pos.x, (int)pos.y);
   	}
	
	public static implicit operator Point(Vector3 pos) {
		return new Point((int)pos.x, (int)pos.y);
   	}
	
	public static Point Parse(string str){
		string[] strResult = str.Split(',');
		Point result = new Point();
		
		result.x = int.Parse(strResult[0]);
		result.y = int.Parse(strResult[1]);
		
		return result;
	}
	
	public static Point Invalid{
		get{
			return new Point(int.MinValue, int.MinValue);
		}	
	}
}