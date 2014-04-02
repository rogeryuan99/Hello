using UnityEngine; 
using System.Collections;
using System.Collections.Generic;

public class DlgManager : MonoBehaviour
{
	
	private static DlgManager _instance;
	public static DlgManager instance{
		get{
			if(_instance == null){
				GameObject rootGo = GameObject.Find("/UIRoot");
				if(rootGo == null){
					return null;	
				}else{
					_instance = rootGo.AddComponent<DlgManager>();	
				}
			}
			return _instance;
		}
	}
	public List<GameObject> dlgStack = new List<GameObject>();
	
	void Awake ()
	{
		_instance = this;
	}
	
	private int delay;
	public void Update(){
		if(delay>0){ 
			delay -= 1;
			return;
		}
		if(TsTheater.InTutorial) return;
        if (Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Escape) )//|| Input.GetKeyDown(KeyCode.Menu))
        {
			for(int n = dlgStack.Count -1;n>=0;n--){
				GameObject	top = dlgStack[n];
				if(top !=null){
					top.SendMessage("OnAndroidHome",SendMessageOptions.DontRequireReceiver);	
					delay = 20;
					return;
				}
			}
			
			GameObject pauseButton = GameObject.Find("PauseButton");
			if(pauseButton != null){
				LevelMgr.Instance.pauseButtonClick();			
				return;
			}
        }
	}
	
	public void pushStack(GameObject dlg){
		dlgStack.Add(dlg);
	}
	public void clearStack(){
		foreach(GameObject dlg in dlgStack){
			Destroy(dlg);	
		}
		dlgStack.Clear();
	}
	public void activeHiddenDlgInTheStack(GameObject skip){
		Debug.LogWarning(" activeHiddenDlgInTheStack");
		for(int n = dlgStack.Count -1;n>=0;n--){
			GameObject	top = dlgStack[n];
			if(top !=null && top != skip){
				Debug.LogWarning(" setActive: "+top);
				if(top.activeInHierarchy == false)
					top.SetActive(true);
				return;
			}
		}
	}
	
	private T showDlg<T>(string prefabName) where T :DlgBase{
		Debug.LogWarning("ShowDlg "+prefabName);
		foreach(GameObject oldDlgGo in dlgStack){
			if(oldDlgGo == null) continue;
			T oldDlgComponent = oldDlgGo.GetComponent<T>();
			if(oldDlgComponent!=null){
				//we have one in the stack,move to the top of stack
				oldDlgGo.SetActive(true);
				dlgStack.Remove(oldDlgGo);
				pushStack(oldDlgGo);
				return oldDlgComponent;
			}
		}
		
		GameObject prefab = Resources.Load (prefabName) as GameObject;
		GameObject go = Instantiate (prefab) as GameObject;
		go.transform.parent = this.transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localScale = Vector3.one;
		
		Component c = go.GetComponent<T>();
		if(c == null){
			Debug.LogError("Dlg class not exist");	
		}
		T dlg = (T)c;
		pushStack(go);
		return dlg;
	}
	
	public PauseDialog showBattlePauseDlg(){
		PauseDialog dlg = showDlg<PauseDialog>("BattlePauseDlg");
		return dlg;
	}
	public HomePageDlg ShowHomePageDlg ()
	{
		HomePageDlg dlg = showDlg<HomePageDlg>("HomePageDlg");
		return dlg;
	}	
	public DebugDlg ShowDebugDlg ()
	{
		DebugDlg dlg = showDlg<DebugDlg>("DebugDlg");
		return dlg;
	}	
	public LevelSelectDlg ShowLevelSelectDlg ()
	{
		LevelSelectDlg dlg = showDlg<LevelSelectDlg>("LevelSelectDlg");
		return dlg;
	}
	public ChapterListDlg ShowChapterListDlg ()
	{
		ChapterListDlg dlg = showDlg<ChapterListDlg>("ChapterListDlg");
		return dlg;
	}
	public SkillTreeDlg showSkillTreeDlg(CharacterData characterData){
		SkillTreeDlg dlg = showDlg<SkillTreeDlg>("SkillTreeDlg");
		dlg.transform.localPosition = new Vector3(0,0,-100);
		dlg.init(characterData as HeroData);
		return dlg;
	}
	
	public GameStoreGoodsListDlg ShowGameStoreGoodsListDlg (List<StoreGoods> goods)
	{
		GameStoreGoodsListDlg dlg = showDlg<GameStoreGoodsListDlg>("gsl_dlg/GameStoreGoodsListDlg");
		dlg.init(goods);
		return dlg;
	}
	public PurchaseGoodsDlg ShowPurchaseGoodsDlg(StoreGoods goods)
	{
		PurchaseGoodsDlg dlg = showDlg<PurchaseGoodsDlg>("gsl_dlg/PurchaseGoodsDlg");	
		dlg.init(goods);
		return dlg;
	}
	public TeamDlg ShowTeamDlg(){
		TeamDlg dlg = showDlg<TeamDlg>("gsl_dlg/TeamDlg");
		return dlg;
	}
	public PurchaseResultDlg ShowPurchaseResultDlg(StoreGoods goods){
		PurchaseResultDlg dlg = showDlg<PurchaseResultDlg>("gsl_dlg/PurchaseResultDlg");
		dlg.init(goods);
		return dlg;
	}
//	public EquipUpgradeDlg ShowEquipUpgradeDlg(EquipData ed){
//		EquipUpgradeDlg dlg = showDlg<EquipUpgradeDlg>("gsl_dlg/EquipUpgradeDlg");
//		dlg.init(ed);
//		return dlg;
//	}
	public EnemyPreviewDlg ShowEnemyPreviewDlg(Hashtable typesHash,int chapterID,int levelID){
		EnemyPreviewDlg dlg = showDlg<EnemyPreviewDlg>("gsl_dlg/EnemyPreviewDlg");
		dlg.init(typesHash,chapterID,levelID);
		return dlg;
	}
	public TeamChangeDlg ShowTeamChangeDlg(){
		TeamChangeDlg dlg = showDlg<TeamChangeDlg>("gsl_dlg/TeamChangeDlg");
		return dlg;
	}
	
	public CommonDlg ShowCommonDlg(string s){
		CommonDlg dlg = showDlg<CommonDlg>("gsl_dlg/CommonDlg");
		dlg.transform.localPosition = new Vector3(0,0,-1000);
		dlg.ShowCommonStr(s);
		//MusicManager.playEffectMusic("SFX_Dialog_Box_2a");
		return dlg;
	}
	public CommonDlg ShowCommonDlgSmall(string s){
		CommonDlg dlg = showDlg<CommonDlg>("gsl_dlg/CommonDlgSmall");
		dlg.transform.localPosition = new Vector3(0,0,-700);
		dlg.ShowCommonStr(s);
		//MusicManager.playEffectMusic("SFX_Dialog_Box_2a");
		return dlg;
	}
//	public AnnouncementDlg ShowAnnouncementDlg ()
//	{
//		AnnouncementDlg dlg = showDlg<AnnouncementDlg>("Dlg_hong/AnnouncementDlg");
//		return dlg;
//	}
	void OnGUI(){
		if(BuildSetting.autoReg_n_login){
			GUILayout.BeginHorizontal();
			GUILayout.Space(300);
			if(GUILayout.Button("save to server")){
				UserInfo.instance.saveAll();
				UserInfo.instance.saveAllToServer();
			}
		}
		
		return;
		GUILayout.BeginHorizontal();
		GUILayout.Space(300);
		if(GUILayout.Button("dump dlg stack")){
			Debug.LogWarning("=======  Dump dlg stack=========");
			foreach(GameObject oldDlgGo in dlgStack){
				Debug.LogWarning("      "+ oldDlgGo);
			}
			Debug.LogWarning("------------------------------------------------");
		}
	}
}
