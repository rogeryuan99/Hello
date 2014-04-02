using UnityEngine;
using System.Collections;

public class TargetIndicator : MonoBehaviour {
public PackedSprite atker;
public GameObject defer;
public Hashtable textureHash;
public static string LOAD_TEXTURE_EVENT = "LOAD_TEXTURE_EVENT";

public void Start (){
	MsgCenter.instance.addListener(LOAD_TEXTURE_EVENT, loadTexture);
	textureHash = new Hashtable();
	hide();
	getResolutionPosition();
}

void Update (){
	
}

public void getResolutionPosition (){
#if UNITY_ANDROID || UNITY_EDITOR
	float widthInInches = Screen.width/(Screen.height/320.0f);
	transform.position = new Vector3(widthInInches-340, transform.position.y, transform.position.z);
//	transform.position.x = widthInInches-340;
#endif
}

public void loadTexture ( Message msg  ){	
	Texture2D texture = Resources.Load("enemyIcons/"+msg.data) as Texture2D;
	textureHash[msg.data] = texture;
}

public void hide (){
		gameObject.SetActiveRecursively(false);
}
public void show ( string atkerType ,   string deferType  ){
		return ;// added by roger, for error like ERROR: Animation "ROCKET" not found!
//	//print(deferType+"   show------------------>"+atkerType);
	gameObject.SetActiveRecursively(true);
	atker.DoAnim(atkerType);
//	defer.renderer.material.mainTexture = textureHash[deferType];
	if(deferType ==HeroData.HEALER && atkerType ==HeroData.HEALER){
		gameObject.SetActiveRecursively(false);
	}else{
//		//print(deferType+"   222show------------------>"+atkerType);
		defer.renderer.material.mainTexture = textureHash[deferType] as Texture;	
	}
	
	defer.transform.localScale = new Vector3(1,1,defer.transform.localScale.z);
}

public void OnDestroy (){
	MsgCenter.instance.removeListener(LOAD_TEXTURE_EVENT, loadTexture);
}
}