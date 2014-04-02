using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : Hazard {
	protected PowerUpManagerDef powerupMgrDef;
	
	private bool isGenerate = false;
	private float curTime = 0f;
	private int delay = 0;
	private int repeat = 0;
	
//	public List<PowerUp> powerupList = new List<PowerUp>();
	//public List<PowerUpDef> puDefList = new List<PowerUpDef>();
	private PowerUpDef puDef;

	void Start () {
	
	}
	
	void Update () {
		if(StaticData.isBattleEnd || TsTheater.InTutorial || !isEnabled){
			stopGenerate();
			if(IsInvoking("stopGenerate")){
				CancelInvoke("stopGenerate");
			}
			return;
		}
		
		if(!isGenerate){
			isGenerate = true;
			startGenerate();
			startStopGenerate();
		}
	}
	
	public void startGenerate(){
		if(!IsInvoking("generate")){
			InvokeRepeating("generate", (float)puDef.puDelay, (float)puDef.puRepeat);
		}
	}
	
	public void startStopGenerate(){
		if(!IsInvoking("stopGenerate")){
			Invoke("stopGenerate", (float)puDef.puRepeat-(float)puDef.puDelay);
		}
	}
	
	public void generate(){
		showPowerupHpJar();
	}
	
	public void stopGenerate(){
		isGenerate = false;
		cancelGenerate();
	}
	
	public void cancelGenerate(){
		if (IsInvoking ("generate")){
			CancelInvoke ("generate");
		}
	}
	
	public override HazardDef HazardDef{
		get{
		 	return powerupMgrDef; 	
		}
		set{
			hazardDef = value;
			powerupMgrDef = hazardDef as PowerUpManagerDef;
			this.puDef = powerupMgrDef.powerupDef;
		}
	}
	
	private void showPowerupHpJar(){
		Vector2 v2 = getRandomPos(puDef);
		string s = "gsl_cell/Powerup_"+puDef.puBuffType.ToUpper();
		GameObject powerupPrefab = Resources.Load(s) as GameObject;
		GameObject powerupJar = Instantiate(powerupPrefab) as GameObject;
		powerupJar.transform.position = new Vector3(v2.x*Utils.getScreenLogicWidth()/2,700f,0);
		powerupJar.transform.localScale = new Vector3(0.8f,0.8f,1f);
		iTween.MoveTo(powerupJar, new Hashtable(){
			{"y",v2.y*Utils.getScreenLogicHeight()/2}, 
			{"time",.5f}, 
			{"delay",.3f}, 
			{"easetype","linear"},
			{"onCompleteTarget",gameObject},
			{"onComplete", "showFloatingEft"},
			{"oncompleteparams",powerupJar}
		});
		
		PowerUp powerup = powerupJar.GetComponent<PowerUp>();
		if(powerup != null) powerup.powerupDef = puDef;
	}
	
	private Vector2 getRandomPos(PowerUpDef puDef){
		List<Vector2> posList = puDef.puRangePosList;
		int ran = Random.Range(0,posList.Count);
		return posList[ran];
	}
	
	private void showFloatingEft(GameObject go){
		PowerUp powerup = go.GetComponent<PowerUp>();
		GameObject heart = new GameObject();
		if(powerup != null) {
			StartCoroutine(delayBorn(powerup));
			heart = powerup.heart;
		}
		
		iTween.MoveTo(heart, new Hashtable(){
			{"y",heart.transform.position.y+20}, 
			{"time",1f}, 
			{"delay",.05f}, 
			{"easetype",iTween.EaseType.linear},
			{"looptype","pingPong"}
		});
	}
	
	private IEnumerator delayBorn(PowerUp powerup){
		yield return new WaitForSeconds(0.2f);
		powerup.isBorn = true;
	}
}
