using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class PieceAnimation : MonoBehaviour
{ 
	public float fat = 1.0f;
	public GameObject head;
	public GameObject body;
	public GameObject legL;
	public GameObject legR;
	public GameObject handL;
	public GameObject handR;
	public Hashtable partHash = new Hashtable ();
	public TextAsset actFile;
	public TextAsset boneFile;
	public ActData[] actDatas;
	public bool  isAutoPlaying;
	public bool  isUseManager;
//	public Material managerMaterial;
	// protected float dur;
	// protected float cumulativeTime = 0;
	//protected int frameCount = 0;
	// protected float speed = 1.0f;
	// protected int totalFrame;
	protected int loopCycles;
	protected int playCount;
	protected Hashtable partList;
	protected Hashtable actList;
//	actlist : hashtabel 
//	key = actname, 
//	value = Hashtable{
//		key = body partname , 
//		value = Hashtable{
//				key = frame
//				value = FrameDef
//			}
//	}
//	[ActData] 
//     name=Move
//     ]Hashtable]data :  Shadow[HashTable]:
//        1:(-5.0, 5.0, 0.0)(0.0, 0.0, 0.0)(0.0, 0.0, 0.0, 1.0)False <FrameDef>
	
	protected bool  isPlaying;
	protected string currentAnima;
	protected Hashtable funcHash;
	
	public delegate void PlayEndFunc (string s);

	public PlayEndFunc playEndFunc;
	protected SpriteManager sptMgr;
	
	//add by xiaoyong for support opening animation at 20131101.
	public static bool isSupportAlpha = true;
	
	public virtual void Awake ()
	{
//		Time.timeScale = 0.2f;
		funcHash = new Hashtable ();
		initPartData ();
		buildBone ();
		buildActList ();
		initDefaultFrame();
//		playAct("attack");
//		print(list2[0].GetElementsByTagName("data").Count);
//		print(xml.FirstChild["part"].Attributes.GetNamedItem("name").Value);
//		list2 = actXML.GetElementsByTagName("data");
//		print(node.Item("name"));
	}
	
	protected virtual void Start ()
	{
		if (isAutoPlaying && currentAnima != null) {
			
			playAct (currentAnima);
		}
	}
	
	protected virtual void initPartData ()
	{
		partList = new Hashtable ();
		partList ["head"] = head;
		partList ["body"] = body;
		partList ["legL"] = legL;
		partList ["legR"] = legR;
		partList ["handL"] = handL;
		partList ["handR"] = handR;
	}
	
	protected virtual void buildBone ()
	{
		if (isUseManager) {
//			GameObject gameObj = new GameObject("SptMgr"+this.GetInstanceID());
			sptMgr = gameObject.GetComponent<SpriteManager> ();
//			sptMgr = gameObject.AddComponent<SpriteManager>();
		}
		XmlDocument xml = new XmlDocument ();
		//Debug.Log (boneFile.text);
		xml.LoadXml (boneFile.text);
		XmlNodeList list = xml.DocumentElement.GetElementsByTagName ("part");
		for (int i=0; i<list.Count; i++) {
			XmlNode node = list [i];
			string partName = node.Attributes.GetNamedItem ("name").Value;
			GameObject container = new GameObject (partName);
			container.transform.parent = this.gameObject.transform;
//			Debug.LogError("partname="+partName);
			GameObject entry = Instantiate (partList [partName] as Object, new Vector3 (0, 0, 0), transform.rotation) as GameObject;
			PackedSprite spt = entry.GetComponent<PackedSprite> ();
			if (sptMgr != null) {
				entry.renderer.enabled = false;
				spt.managed = true;
				spt.manager = sptMgr;
				sptMgr.AddSprite (entry);
//				sptMgr.renderer.material = managerMaterial;
			}
			
//			GameObject entry = partList[partName];
//			entry.transform.rotation = transform.rotation;
//			entry.transform.position = new Vector3(0,0,0);
			entry.transform.parent = container.transform;
			
//			Vector2 pos1 = Vector2.zero;
//			if(node.Attributes.GetNamedItem ("pos") != null){
//				string posStr = node.Attributes.GetNamedItem ("pos").Value;
//			 	pos1 = new Vector2 (float.Parse (posStr.Split ("|" [0]) [0]), float.Parse (posStr.Split ("|" [0]) [1]));
//			}
			Vector2 cPos1 = Vector2.zero;
			if(node.Attributes.GetNamedItem ("epos") != null){
				string cPosStr = node.Attributes.GetNamedItem ("epos").Value;
				cPos1 = new Vector2 (float.Parse (cPosStr.Split ("|" [0]) [0]), float.Parse (cPosStr.Split ("|" [0]) [1]));
			}
				
			entry.transform.localPosition = new Vector3 (cPos1.x, cPos1.y, entry.transform.localPosition.z);
			
			//string rotation = node.Attributes.GetNamedItem ("roration").Value;
			container.transform.localPosition = Vector3.zero;
			container.transform.rotation = Quaternion.Euler (Vector3.zero);
			container.transform.localScale = Vector3.one;
			
//			partHash[partName] = [container, entry];
			ArrayList bodyObjs = new ArrayList ();
			bodyObjs.Add (container);
			bodyObjs.Add (entry);
			partHash [partName] = bodyObjs;
			
		}
	}
	private HashSet<string> allPartNames = new HashSet<string>();
	private FrameDef defaultInvisibleFrame = null;
			
	protected virtual void buildActList ()
	{
		actList = new Hashtable ();
		XmlDocument loadXML = new XmlDocument ();
		loadXML.LoadXml (actFile.text);
		
		XmlNodeList actXMLList = loadXML.DocumentElement.GetElementsByTagName ("act");
		for (int i=0; i<actXMLList.Count; i++) {
			XmlNode node = actXMLList [i];
			string actName = node.Attributes.GetNamedItem ("name").Value;
			switch(actName){
			case "idle":
				node.Attributes.GetNamedItem("name").Value = "Stand";
				break;
			case "run":
				node.Attributes.GetNamedItem("name").Value = "Move";
				break;
			case "injured":
				node.Attributes.GetNamedItem("name").Value = "Damage";
				break;
			case "die":
				node.Attributes.GetNamedItem("name").Value = "Death";
				break;
			case "punch":
				node.Attributes.GetNamedItem("name").Value = "Attack";
				break;
			case "shoot":
				node.Attributes.GetNamedItem("name").Value = "Attack";
				break;
			}
		}		
		
		
		
		
		int length = actXMLList.Count;
		actDatas = new ActData[length];
		for (int i=0; i<actXMLList.Count; i++) {
			XmlNode node = actXMLList [i];
			string actName = node.Attributes.GetNamedItem ("name").Value;
			string total = node.Attributes.GetNamedItem ("totalFrame").Value;
			
			Hashtable allPartFrames = xmllistToHash (node.ChildNodes);
			ActData actD = new ActData (actName, allPartFrames, int.Parse (total));
			actD.loopCycles = -1;
			actD.fps = 24;
			actDatas [i] = actD;
			actList [actName] = actD;
		}
		//Debug.LogError(Utils.dumpHashTable(actList));
		//Debug.Break();
		ActData defaultActD = actDatas [0];
		currentAnima = defaultActD.name;
	}
	protected void initDefaultFrame(){
		//string ss = this.gameObject.name+" :";
		foreach(ActData ad in actList.Values){
			foreach(string k in ad.data.Keys){
				if(allPartNames.Contains(k)) continue;
				allPartNames.Add(k);
		//		ss +=k+",";	
			}
		}
		//Debug.LogError(ss);
		defaultInvisibleFrame = new FrameDef();
		defaultInvisibleFrame.visible = false;
		defaultInvisibleFrame.matrix = new Vector3[2];
		defaultInvisibleFrame.matrix[0] = new Vector3(0.001f,0,0);
		defaultInvisibleFrame.matrix[1] = new Vector3(0,0.001f,0);
		defaultInvisibleFrame.matrix = new Vector3[2];
		defaultInvisibleFrame.pos = Vector3.zero;
		defaultInvisibleFrame.localScale = new Vector3(0.001f,0.001f,1);
		defaultInvisibleFrame.rotation = Quaternion.Euler(Vector3.zero);		
	}
	
	public static Hashtable xmllistToHash (XmlNodeList Xmllist)
	{
		//bodypart
		//---|_frameDef
		Hashtable rootHash = new Hashtable ();
		int length = Xmllist.Count;
		for (int i=0; i<length; i++) {
			XmlNode bodyPardNode = Xmllist [i];
			rootHash [bodyPardNode.Name] = new Hashtable ();
			XmlNodeList dataList = bodyPardNode.ChildNodes;
			int dataLen = dataList.Count;
			for (int j=0; j<dataLen; j++) {
				XmlNode dataNode = dataList [j];
				Hashtable frames = rootHash [bodyPardNode.Name] as Hashtable;
				FrameDef fd = new FrameDef ();
				////
				string posStr = dataNode.Attributes ["pos"].Value;
				string[] posAry = posStr.Split ('|');
				fd.pos = new Vector3 (float.Parse (posAry [0]), float.Parse (posAry [1]), -i);
				/////
				XmlAttribute attr = dataNode.Attributes ["v"];
				fd.visible = (attr == null);
				
				if (isSupportAlpha) {
					if (dataNode.Attributes ["a"] == null) {
						fd.alpha = 1;
					} else {
						string attrAlpha = dataNode.Attributes ["a"].Value;
						fd.alpha = float.Parse (attrAlpha);
					}
				}
				////
				
				///
				///
				int frame = int.Parse (dataNode.Attributes ["frame"].Value);
				//Debug.Log(typeNode.Name + "FRAME "+frame +":"+fd);
				if (dataNode.Attributes ["m"] != null) {
					string ms = dataNode.Attributes ["m"].Value;
					fd.matrix = new Vector3[2];	
					string[] msf = ms.Split ('|');
					fd.matrix [0] = new Vector3 (float.Parse (msf [0]), float.Parse (msf [2]), 0);				
					fd.matrix [1] = new Vector3 (float.Parse (msf [1]), float.Parse (msf [3]), 0);
//					fd.matrix [2] = new Vector3 (float.Parse (msf [4]), float.Parse (msf [5]), 0);	
				} else {
					string scaleStr = dataNode.Attributes ["scale"].Value;
					string[] scaleAry = scaleStr.Split ('|');		
					float rotation = float.Parse (dataNode.Attributes ["rotation"].Value);
					fd.rotation = Quaternion.Euler (new Vector3 (0, 0, rotation));
					if (fd.visible) {
						fd.localScale = new Vector3 (float.Parse (scaleAry [0]), float.Parse (scaleAry [1]), 0);
					} else {
						fd.localScale = new Vector3 (0.001f, 0.001f, 0);
					}
				}
				
				frames [frame] = fd;
			}
		}
		return rootHash;
	}
	
	public Vector3 getHeadPosition ()
	{
		Vector3 vc3 = Vector3.zero;
		ActData actD = actList ["Death"] as ActData;
		Hashtable rootHash = actD.data;
		Hashtable dataHash = rootHash ["Shadow"] as Hashtable;
		if (dataHash != null && dataHash.Count > 0) { 
			FrameDef shadowDataHash = dataHash [actD.totalFrame] as FrameDef;
			vc3 = shadowDataHash.pos;
		}
		return vc3;
	}
	
	private FrameDef getKeyFrames (Hashtable frames, int currentIndex, bool forward)
	{
		FrameDef frameDef = frames [currentIndex] as FrameDef;
		if(forward == false){
			while (frameDef == null && currentIndex>=0) {
				currentIndex -= 1;
				frameDef = frames [currentIndex] as FrameDef;
			}
		}else{
			while (frameDef == null && currentIndex<frames.Count) {
				currentIndex += 1;
				frameDef = frames [currentIndex] as FrameDef;
			}
			while (frameDef == null && currentIndex>=0) {
				currentIndex -= 1;
				frameDef = frames [currentIndex] as FrameDef;
			}
		}
		
		return frameDef;
	}
	
	private void doAct1 ()
	{
		ActData actD = actList [currentAnima] as ActData;
		Hashtable bodyPart_frames = actD.data;
		
//		foreach (string key in bodyPart_frames.Keys) {
		foreach (string key in allPartNames) { //if miss a part if a action in flash, let's take it for invisible
			if (!partHash.Contains (key)) {
				Debug.Log (" Part miss " + key + " in " + currentAnima);
			}
			
			FrameDef frameDef = GetMidFrameOnTime (currentAnima, key, actCumulative);
			if (!string.IsNullOrEmpty (additionalAnima)) {
				FrameDef attachFrameDef = GetMidFrameOnTime (additionalAnima, key, additionalCumulative);
//				Debug.Log(gameObject.name+":"+key+" LerpFrame");
				frameDef = LerpFrame (frameDef, attachFrameDef);
			}
			
			FillContainerTransform (key, frameDef);
//TODO:deleted by roger. dont know for what			 
//	if(container.transform.localScale.x == 0.001f)
//	{
//		PackedSprite spt1 = entry.GetComponent<PackedSprite>();
//		if(spt1.animations.Length == 1)
//		{
//			spt1.PlayAnim(0);
//		}
//	}			
		}
	}
	
	private void FillContainerTransform (string key, FrameDef frameDef)
	{
		ArrayList ary = partHash [key] as ArrayList;
		if(ary == null)
		{
			Debug.LogError(key);
		}
		GameObject container = (GameObject)ary [0];
		
		container.transform.localPosition = frameDef.pos;
		
		GameObject entry = (GameObject)ary [1];
		if (frameDef.matrix != null) 
		{
			PackedSprite spt = entry.GetComponent<PackedSprite> ();
			
			if(!frameDef.visible)
			{
//				if(!spt.IsAnimating())
//				{
					if(!spt.IsHidden())
					{
						spt.StopAnim();
						spt.Hide(true);	
					}
//				}
			}
			else 
			{
				if(spt.IsHidden())
				{
					spt.Hide(false);
					if(spt.animations.Length > 0 && !spt.IsAnimating())
					{
						spt.PlayAnim(0);
					}
					
				}
				
				SpriteMesh_Managed sm_m = (SpriteMesh_Managed)spt.spriteMesh;
				if (sm_m.origialVertices == null) {
					sm_m.myVertices = sm_m.myVertices;
				}
				Vector3[] vertices = sm_m.origialVertices;
				if (vertices == null)
					vertices = sm_m.myVertices;
				for (int n = 0; n<vertices.Length; n++) {
					Vector3 oldv = vertices [n];
					vertices [n] = new Vector3 (
							Vector3.Dot (vertices [n]*fat, frameDef.matrix [0]),
							Vector3.Dot (vertices [n], frameDef.matrix [1]*fat),
							frameDef.pos.z);
				}
					
				sm_m.myVertices = vertices;
						
				if (isUseManager) {
					if (spt.drawLayer != -frameDef.pos.z) {
						spt.SetDrawLayer ((int)(-frameDef.pos.z));
					}
				}			
				if(!spt.IsAnimating())
				{
					container.transform.localScale = Vector3.one; //frameDef.localScale;
					container.transform.rotation = Quaternion.Euler (Vector3.zero);
				}
			}
		}
		else
		{
			PackedSprite spt = entry.GetComponent<PackedSprite> ();
			if(spt != null)
			{
				spt.Hide(false);	
			}
			container.transform.localScale = frameDef.localScale;
			container.transform.rotation = frameDef.rotation;
			if (isSupportAlpha) {
				UISprite uisprite = entry.GetComponent<UISprite> ();
				if (uisprite != null) {
					uisprite.alpha = frameDef.alpha;	
				}
			}
			if (isUseManager) {
				if (spt.drawLayer != -frameDef.pos.z) {
					spt.SetDrawLayer ((int)(-frameDef.pos.z));
				}
			}
		}		
		if (isSupportAlpha) {
			PackedSprite sp = entry.GetComponent<PackedSprite>();
			if (null != sp){
				sp.SetColor(new Color(sp.color.r, sp.color.g, sp.color.b, frameDef.alpha));
			}
		}
	}
	
	private FrameDef GetMidFrameOnTime (string animName, string key, CumulativeDef cumulativeDef)
	{
		ActData actD = actList [animName] as ActData;
		Hashtable bodyPart_frames = actD.data;
		Hashtable frames = bodyPart_frames [key] as Hashtable;
		FrameDef midFrameDef = null;
		if(frames == null){
			midFrameDef = defaultInvisibleFrame;
		}else{
			FrameDef pre_frameDef = getKeyFrames (frames, cumulativeDef.preIndex + 1,false);
			FrameDef post_frameDef = getKeyFrames (frames, cumulativeDef.postIndex + 1,false);
			midFrameDef = TweenFrame (pre_frameDef, post_frameDef, cumulativeDef.t);
			if(post_frameDef != null)AdjustFrameLayer (key, post_frameDef);
		}
		return midFrameDef;
	}
	
	private FrameDef TweenFrame (FrameDef pre_frameDef, FrameDef post_frameDef, float t)
	{
		FrameDef midFrameDef = new FrameDef ();
		if(post_frameDef==null){
			midFrameDef = pre_frameDef;
			return midFrameDef;
		}
		
		midFrameDef.visible = post_frameDef.visible;
		if(post_frameDef.visible == false || pre_frameDef.visible == false){
			midFrameDef = post_frameDef;
		}else{
			midFrameDef.pos = Vector3.Lerp (pre_frameDef.pos, post_frameDef.pos, t);
			if (pre_frameDef.matrix!=null && post_frameDef.matrix!=null) {
				midFrameDef.matrix = new Vector3[2];
				midFrameDef.matrix[0] = Vector3.Lerp (pre_frameDef.matrix[0], post_frameDef.matrix[0], t);
				midFrameDef.matrix[1] = Vector3.Lerp (pre_frameDef.matrix[1], post_frameDef.matrix[1], t);
			} else {
				midFrameDef.localScale = Vector3.Lerp (pre_frameDef.localScale, post_frameDef.localScale, t);
				midFrameDef.rotation = Quaternion.Lerp (pre_frameDef.rotation, post_frameDef.rotation, t);
			}
			if (isSupportAlpha) {
				midFrameDef.alpha = pre_frameDef.alpha;
			}
		}
		if(post_frameDef.visible == false){
			post_frameDef = defaultInvisibleFrame;	
		}
		return midFrameDef;
	}
	
	private void AdjustFrameLayer (string part, FrameDef frameDef)
	{
		ArrayList ary = partHash [part] as ArrayList;//ArrayList of GameObject
		GameObject entry = (GameObject)ary [1];
		
		if (isUseManager) {
			PackedSprite spt = entry.GetComponent<PackedSprite> ();
			if (spt.drawLayer != -frameDef.pos.z) {
				spt.SetDrawLayer ((int)(-frameDef.pos.z));
			}
		}
	}
	
	private FrameDef LerpFrame (FrameDef orignFrame, FrameDef attachFrame)
	{
		return attachFrame;
		// lerp not work properly, leave it here
		if (IsNullFrame (attachFrame) || IsNullFrame (orignFrame))
			return attachFrame;
			
		FrameDef result = new FrameDef ();
		if (attachFrame.visible == false || orignFrame.visible == false) {
			result = attachFrame;
		}else{
			result.visible = attachFrame.visible;
			float Weight = 0.6f;
			result.pos = Vector3.Lerp (orignFrame.pos, attachFrame.pos, Weight);
			if(attachFrame.matrix!=null){
				result.matrix = new Vector3[2];
				if(orignFrame.matrix !=null){
					result.matrix[0] = Vector3.Lerp(orignFrame.matrix[0],attachFrame.matrix[0],Weight);	
					result.matrix[1] = Vector3.Lerp(orignFrame.matrix[1],attachFrame.matrix[1],Weight);	
	//				Debug.Log("orignFrame: v="+ orignFrame.visible + " m = "+orignFrame.matrix);
				}else{
					result.matrix[0] = attachFrame.matrix[0];
					result.matrix[1] = attachFrame.matrix[1];
	//				Debug.Log("attachFrame: v="+ attachFrame.visible + " m = "+attachFrame.matrix);
				}
			}else{
				result.localScale = Vector3.Lerp (orignFrame.localScale, attachFrame.localScale, Weight);
				result.rotation = Quaternion.Lerp (orignFrame.rotation, attachFrame.rotation, Weight);
			}
	//		Debug.Log("result: v="+ result.visible + " m = "+result.matrix);
			if (isSupportAlpha) {
				result.alpha = orignFrame.alpha;
			}
		}
		if(result.visible == false){
			result = defaultInvisibleFrame;	
		}
		
		return result;
	}
	
	private bool IsNullFrame (FrameDef frameDef)
	{
		return (null == frameDef || false == frameDef.visible);
	}
	
//	private function doAct1( int index  )
//	{
//		ActData actD = actList[currentAnima];
//		XmlNodeList actInfo = actD.data;
//		int length = actInfo.Count;
//		for( int j=0; j<length; j++)
//		{
//			XmlNode typeNode = actInfo[j];
//			Array ary = partHash[typeNode.Name];
//			XmlNodeList dataList = typeNode.ChildNodes;//.GetElementsByTagName("data");
//			XmlNode node = dataList[index];
//			string frameLab = (index+1)+"";
//			if(node == null || node.Attributes["frame"].Value != frameLab)
//			{
//				continue;
//			}
//			
//			GameObject container = ary[0];
//			GameObject entry  = ary[1];
//			
//			string posStr = node.Attributes.GetNamedItem("pos").Value;
//			Vector3 pos1 = Vector3( float.Parse( posStr.Split("|"[0])[0] ), float.Parse( posStr.Split("|"[0])[1] ) , -j);
//			if(isUseManager)
//			{
//				PackedSprite spt = entry.GetComponent<PackedSprite>();
//				if(spt.drawLayer != j)
//				{
//					spt.SetDrawLayer(j);
//				}
//			}
//			container.transform.localPosition = pos1;
//			
//			string cPosStr = node.Attributes.GetNamedItem("epos").Value;
//			Vector2 cPos1 = new Vector2( float.Parse( cPosStr.Split("|"[0])[0] ), float.Parse( cPosStr.Split("|"[0])[1] ) );
//			entry.transform.localPosition = cPos1;
//			
//			string scaleStr = node.Attributes.GetNamedItem("scale").Value;
//			Array scaleAry  = scaleStr.Split("|"[0]);
//			
//			XmlAttribute visible = node.Attributes["v"];
//			if(visible != null)
//			{
//				container.transform.localScale = Vector3(0,0,0);
//			}else{
//				string scaleX = scaleAry[0];
//				string scaleY = scaleAry[1];
//				Vector3 scale = Vector3( float.Parse( scaleX ), float.Parse( scaleY ) , 1);
//				container.transform.localScale = scale;
//			}
//			
//			
//			string rotation = node.Attributes["rotation"].Value;
//			container.transform.rotation.eulerAngles = Vector3(0,0,float.Parse(rotation));
//			
//			
//		}
//		
//		
//	}
	

	
	public void doubleSpd ()
	{
		actCumulative.speed = 1.5f;
	}

	public void normalSpd ()
	{
		actCumulative.speed = 1;
	}
	public void halfSpd ()
	{
		actCumulative.speed = 0.5f;
	}

	
	public void pauseAnima ()
	{
		isPlaying = false;
	}
	
	public void restart ()
	{
		isPlaying = true;
	}
	
	public void playAct (string actName)
	{
//		Debug.LogError(" >    "+this.gameObject.name +"-"+ actName+" play");
		pauseAnima ();
		actCumulative.time = 0;
		doAct (actName);
	}
	
	protected List<string> actFrameKeyList = new List<string> ();
	private string additionalAnima = string.Empty;
	private CumulativeDef additionalCumulative = new CumulativeDef ();
	private const float CD_ADDITIONAL_ANIM = .5f;
	private float cdAdditionalAnim = 0f;
	
	public void attachAct (string actName)
	{
//		Debug.Log(" ===== "+this.gameObject.name+" attachAct:"+actName);
		if (!string.IsNullOrEmpty (additionalAnima)
				|| cdAdditionalAnim > 0f
				|| null == actList [actName]) 
			return;
		
		ActData actD = actList [actName] as ActData;

		additionalAnima = actName;
		additionalCumulative.time = 0;
		additionalCumulative.totalFrame = actD.totalFrame;
		isPlaying = true;
	}
	
	CumulativeDef actCumulative = new CumulativeDef ();

	public void doAct (string actName)
	{
		if (actList.ContainsKey (actName)) {
			if (currentAnima != actName) {
				pauseAnima ();
				actCumulative.time = 0;
			}
			
			currentAnima = actName;
			ActData actD = actList [currentAnima] as ActData;
			loopCycles = actD.loopCycles;
			actCumulative.totalFrame = actD.totalFrame;
			isPlaying = true;
			actFrameKeyList.Clear ();
			for (int i = 0; i < actCumulative.totalFrame; ++i) {
				if (funcHash.ContainsKey (actName + i)) {
					actFrameKeyList.Add (actName + i);
				}
			}
		}
	}
	
	public int getAnimaLength (string name)
	{
		ActData actD = actList [name] as ActData;
		return actD.totalFrame;
	}
	
	public void animaPlayEndScript (PlayEndFunc func)
	{
		playEndFunc = func;
//		int length = getAnimaLength(animaName);
//		addFrameScript(animaName,length, func);
	}
	
	public void addFrameScript (string animaName, int frame, PlayEndFunc func)
	{
		int keyFrame = frame - 1;
		funcHash.Add (animaName + keyFrame, new ArrayList (){frame - 1, func});
	}

	public void removeFrameScript (string animaName, int frame)
	{
		int keyFrame = frame - 1;
		funcHash.Remove (animaName + keyFrame);
	}
	
	public void showPiece (string partName, string pieceName)
	{
		ArrayList partAry = (ArrayList)partHash [partName];
		if (partAry == null) {
			print (partName + "is not exist!");
			return;
		}
//		GameObject gameObj = partAry[1];
		PackedSprite spt = (partAry [1] as GameObject).GetComponent<PackedSprite> ();
		spt.PlayAnim (pieceName);
	}
	
	public int getCurrentFrame(){
		 return actCumulative.preIndex;
	}
	
	public void jumpToFrame(int frame){
		actCumulative.jumpToFrame(frame);
	}
	
	public void Update ()
	{
		if (isPlaying) {
			actCumulative.Update ();
			additionalCumulative.Update ();
			
			doAct1 ();
			
			if (additionalCumulative.IsNeedClear) {
				additionalCumulative.Clear ();
				additionalAnima = string.Empty;
				cdAdditionalAnim = CD_ADDITIONAL_ANIM;
			}
			cdAdditionalAnim = (cdAdditionalAnim > 0) ? cdAdditionalAnim - Time.deltaTime * actCumulative.speed : 0;

			for (int i = 0; i <= actCumulative.preIndex; ++i) {
				string key = currentAnima + i;
				if (actFrameKeyList.Contains (key)) {
					actFrameKeyList.Remove (key);
					if (funcHash [key] != null) {
						ArrayList funcAry = funcHash [key] as ArrayList;//[frame, func]
						
						if (actCumulative.preIndex == int.Parse (funcAry [0].ToString ())) {
							PlayEndFunc func = funcAry [1] as PlayEndFunc;
							func("");
						}
					}
				
				}
			}

			//frameCount = (++frameCount + gotoFrame) % totalFrame;
			float allTime = (float)actCumulative.totalFrame * CumulativeDef.dur;
			if (actCumulative.time > allTime) {
				actCumulative.time -= allTime;
				if (playEndFunc != null) {
					playEndFunc (currentAnima);
				}
				if (loopCycles != -1 && playCount == loopCycles) {
					isPlaying = false;
				}
				playCount++;
			}
		}
		
	}
	
	public string GetCurrentAnimaName()
	{
		return this.currentAnima;
	}
	
	public virtual void OnDestroy ()
	{
		if (sptMgr) {
			Destroy (sptMgr.gameObject);
		}
//		print("remove function script");
//		foreach( string key in funcHash.Keys)
//		{
//			funcHash.Remove(key);
//		}
		
	}

}

public class ActData
{
	public string name;//such as Death, Move
	private Hashtable _data;
	//bodypart
	// |__frames
	public Hashtable data {
		set {
			_data = value;
			Debug.LogError (Utils.dumpHashTable (data));
			Debug.LogError (name);
		}
		get {
			return _data;	
		}
	}

	public int totalFrame;
	public int loopCycles;
	public int fps;
	
	public ActData (string name, Hashtable data, int totalFrame)
	{
		this.name = name;
		this._data = data;
		this.totalFrame = totalFrame;
	}

	public override string ToString ()
	{
		return Utils.dumpHashTable (this.data);
	}
	
}

public class FrameDef
{
	public Vector3 pos;
	public Vector3 localScale;
	public Quaternion rotation;
	public bool visible;
	public float alpha = 1.0f;
	public Vector3[] matrix = null;

	public override string ToString ()
	{
		return "F";
		//return "" + pos + localScale + rotation + visible;
	}
}

public class CumulativeDef
{
	public const float dur = 1f/24f;
	public float speed = 1.0f;
	public float time;
	public float t;
	public int preIndex;
	public int postIndex;
	public int totalFrame;
	
	public CumulativeDef ()
	{
		time = 0;
		t = 0f;
		preIndex = 0;
		postIndex = 0;
		totalFrame = 0;
	}
	
	public void Update ()
	{
		time += Time.deltaTime * speed;
		float res = (time / dur);
		preIndex = (int)res;
		postIndex = preIndex + 1;
		
		if (preIndex >= totalFrame) {
			preIndex = totalFrame;
			postIndex = totalFrame;
		}
		
		t = res - preIndex;
	}
	public void jumpToFrame(int frame){
		time =dur * (float) frame;
		preIndex = frame;
		postIndex = frame + 1;
		t = 0;
	}
	public bool IsNeedClear {
		get{ return (preIndex >= totalFrame && totalFrame > 0); }
	}
	
	public void Clear ()
	{
		time = 0;
		t = 0;
		preIndex = 0;
		postIndex = 0;
		totalFrame = 0;
	}
	
}
