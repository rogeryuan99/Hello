using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicGrid : MonoBehaviour {
	public List<GameObject> pool = new List<GameObject>();
	public int debugCount;
	public int minCount=0;
	[HideInInspector] public Camera viewCamera;
	public List<DynamicCell> list = new List<DynamicCell>();
	public GameObject cellPrefab;
	public float renderRange = 1.0f;
	[HideInInspector] public GameObject poolRoot;
	void Start () {
		if(viewCamera == null){
			viewCamera = Camera.mainCamera;
		}
		//if there are special premake cells...
		for (int i = 0; i < this.transform.childCount; ++i)
		{
			Transform t = this.transform.GetChild(i);
			DynamicCell cell = t.GetComponent<DynamicCell>();
			if (null != cell && false == list.Contains(cell))
				list.Add(cell);
		}
		//common cells
		for( int i = 0;i<debugCount;i++){
			GameObject go = new GameObject();
			go.name = "Cell_"+i;	
			DynamicCell dc = go.AddComponent<DynamicCell>();
			list.Add(dc);
			dc.viewCamera = Camera.mainCamera;
			go.transform.parent = this.transform;
			go.transform.localScale = Vector3.one;
		}
		if(this.GetComponent<UIGrid>()!=null){
			this.GetComponent<UIGrid>().repositionNow = true;
		}
		if(this.GetComponent<UIPageGrid>()!=null){
			this.GetComponent<UIPageGrid>().repositionNow = true;
		}
		poolRoot = new GameObject("poolRoot");
		poolRoot.transform.parent = this.transform.parent;
		poolRoot.layer = this.gameObject.layer;
		poolRoot.transform.localPosition = new Vector3(100000,0,0);
	}
	public void setData(IList datas){
		Debug.Log("clean up grid");
		for (int i = 0; i < this.transform.childCount; ++i)
		{
			Transform t = this.transform.GetChild(i);
			Destroy(t.gameObject);//hong 2013/5/23
		}
		this.transform.DetachChildren();
		list.Clear();

		
//		for (int i = 0; i < pool.Count; ++i)
//		{
//			GameObject go = pool[i];
//			Destroy(go);
//		}
//		pool.Clear();

		//clear over
		//create
		for( int i = 0;i<datas.Count || i<minCount;i++){
			GameObject go = new GameObject();
			go.name = "Cell_"+i;	
			go.layer = this.gameObject.layer;
			DynamicCell dc = go.AddComponent<DynamicCell>();
			list.Add(dc);
			dc.viewCamera = Camera.mainCamera;
			go.transform.parent = this.transform;
			go.transform.localPosition = Vector3.zero;
			go.transform.localScale = Vector3.one;
			dc.data = (i<datas.Count)?datas[i]:null;
			dc.index = i;
		}
		if(this.GetComponent<UIGrid>()!=null){
			this.GetComponent<UIGrid>().repositionNow = true;
		}
		if(this.GetComponent<UIPageGrid>()!=null){
			this.GetComponent<UIPageGrid>().repositionNow = true;
		}
	}
	
	public DynamicCell getDynamicCellAt(int index){
		return list[index];	
	}
	public int Count{
		get{
			return list.Count;	
		}
	}
	
	public void GlowOneAndGrayLockOthers(string[] name){
		GlowOneAndGrayLockOthers(name[0]);
	}
	public void GlowOneAndGrayLockOthers(string name){
		for (int i=0; i<list.Count; i++){
			list[i].OnInitedCallBack = (dCell) => {
				dCell.OnInitedCallBack = null;
				ChapterDetailCell cell = dCell.detailCell.GetComponent<ChapterDetailCell>();
				TsGrayLockOrGlowTool tool = cell.GetComponent<TsGrayLockOrGlowTool>();
				
				if (null == tool || null == cell){
					return;
				}
				if(cell.textName.text == name){
					tool.Glow();
				}
				else{
					tool.GrayLock();
				}
			};
		}
	}
}
