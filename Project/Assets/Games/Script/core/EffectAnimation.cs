using UnityEngine;
using System.Collections;

public class EffectAnimation : PieceAnimation {
	public string type = "";
	
	public override void Awake()
	{
		base.Awake();
	}
	
	protected override void buildActList (){	
		selectActInFileMgr();
		ActData defaultActD = actDatas[0];
		currentAnima = defaultActD.name;
		if(isAutoPlaying)
		{
			playAct(currentAnima);
		}
			
	}
	
	protected virtual void selectActInFileMgr (){
		Hashtable actMgr = AnimaFileMgr.actHash[type] as Hashtable;
		actDatas = actMgr["actDatas"] as ActData[];
		actList  = actMgr["actList"] as Hashtable;
	}
	protected virtual Hashtable selectBoneInFileMgr (){
		return AnimaFileMgr.boneHash[type] as Hashtable; 
	}
	
	protected override void buildBone (){
		if(isUseManager)
		{
//			GameObject gameObj = new GameObject("SptMgr"+this.GetInstanceID());
			sptMgr = gameObject.GetComponent<SpriteManager>();
//			sptMgr = gameObject.AddComponent<SpriteManager>();
		}
		Hashtable boneMgr = selectBoneInFileMgr(); 
		ArrayList partNameArray = boneMgr["partName"] as ArrayList;
		ArrayList pos1Array = boneMgr["pos1"] as ArrayList;
		ArrayList cpos1Array = boneMgr["cpos1"] as ArrayList;
		ArrayList rotationArray = boneMgr["rotation"] as ArrayList;
		for( int i=0; i< partNameArray.Count; i++)
		{
			GameObject container = new GameObject(partNameArray[i].ToString());
			
			container.transform.parent = this.gameObject.transform;
			GameObject entry = Instantiate(partList[partNameArray[i]] as GameObject, new Vector3(0,0,0), transform.rotation) as GameObject;
			PackedSprite spt = entry.GetComponent<PackedSprite>();
			if(sptMgr != null)
			{
				entry.renderer.enabled  = false;
				spt.managed = true;
				spt.manager = sptMgr;
				sptMgr.AddSprite(entry);
//				sptMgr.renderer.material = managerMaterial;
			}
//			GameObject entry = partList[partName];
//			entry.transform.rotation = transform.rotation;
//			entry.transform.position = new Vector3(0,0,0);
			entry.transform.parent = container.transform; 
			 
			Vector2 pos1 =  (Vector2)pos1Array[i];
			container.transform.localPosition = new Vector3(pos1.x, pos1.y, 0)+ new Vector3(0,0,-i);

			Vector2 cpos1 =  (Vector2)cpos1Array[i];
			entry.transform.localPosition = new Vector3(cpos1.x, cpos1.y, entry.transform.localPosition.z);
			
			string rotation =  rotationArray[i].ToString();
			container.transform.rotation = Quaternion.Euler(new Vector3(0,0,float.Parse(rotation)));
//			container.transform.rotation.eulerAngles = Vector3(0,0,float.Parse(rotation));
			container.transform.localScale = new Vector3(0.001f,0.001f,1);
			
			partHash[partNameArray[i]] = new ArrayList(){container, entry};
		}
	}

	
}
