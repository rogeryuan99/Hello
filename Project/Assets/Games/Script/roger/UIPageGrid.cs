using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Page Grid")]

public class UIPageGrid : MonoBehaviour {
	public enum Arrangement {
		Horizontal,
		Vertical,
	}
	
	public Arrangement arrangement = Arrangement.Horizontal;
	public int row = 2;
	public int col = 2;
	public float cellWidth = 200f;
	public float cellHeight = 200f;
	public float pageMargin = 500f;
	public float offsetX = 0f;
	public float offsetY = 0f;
	
	public bool repositionNow = false;
	public bool sorted = false;
	public bool hideInactive = true;
	
	bool mStarted = false;

	void Start () {
		mStarted = true;
		Reposition();
	}

	void Update () {
		if (repositionNow)
		{
			repositionNow = false;
			Reposition();
		}
	}
	
	static public int SortByName (Transform a, Transform b) { return string.Compare(a.name, b.name); }
	
	public void Reposition () {
		if (!mStarted) {
			repositionNow = true;
			return;
		}

		Transform myTrans = transform;

		int x = 0;
		int y = 0;
		int pageIndex = 0;

		if (sorted) {
			List<Transform> list = new List<Transform>();

			for (int i = 0; i < myTrans.childCount; ++i) {
				Transform t = myTrans.GetChild(i);
				if (t && NGUITools.GetActive(t.gameObject)) list.Add(t);
			}
			list.Sort(SortByName);

			for (int i = 0, imax = list.Count; i < imax; ++i)
			{
				Transform t = list[i];

				if (!NGUITools.GetActive(t.gameObject) && hideInactive) continue;

				float depth = t.localPosition.z;

				t.localPosition = (arrangement == Arrangement.Horizontal) ?
					new Vector3(offsetX + cellWidth * x + pageIndex * pageMargin,offsetY - cellHeight * y,depth) :
					new Vector3(offsetX + cellWidth * x,offsetY - cellHeight * y - pageIndex * pageMargin);
				if(++x >= col && col > 0) {
					x = 0;
					++y;
				}
				if(y >= row && row > 0) {
					y = 0;
					++pageIndex;
				}
			}
		}
		else
		{
			for (int i = 0; i < myTrans.childCount; ++i)
			{
				Transform t = myTrans.GetChild(i);

				if (!NGUITools.GetActive(t.gameObject) && hideInactive) continue;

				float depth = t.localPosition.z;

				t.localPosition = (arrangement == Arrangement.Horizontal) ?
					new Vector3(offsetX + cellWidth * x + pageIndex * pageMargin,offsetY - cellHeight * y,depth) :
					new Vector3(offsetX + cellWidth * x,offsetY - cellHeight * y - pageIndex * pageMargin);
				if(++x >= col && col > 0) {
					x = 0;
					++y;
				}
				if(y >= row && row > 0) {
					y = 0;
					++pageIndex;
				}
			}
		}

		UIDraggablePanel drag = NGUITools.FindInParents<UIDraggablePanel>(gameObject);
		if (drag != null) drag.UpdateScrollbars(true);
	}
}
