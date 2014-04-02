using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager :Singleton<MusicManager> {
private const string prefabsResourcesPath = "audioPrefabs/";
private const string musicResourcesPath = "audioSources/";
private const string bgPrefabsName = "bg_music";
private const string effectPrefabsName = "effect_music";

private AudioSource bgMusicObj;
public AudioSource[] effectMusicObjs;
private GameObject bgMusic;
private GameObject effectMusic;
public bool  isBgMute = false;
public bool  isEffectMute = false;
public bool  isSingleMusic = false; 
	
private Hashtable moveMusicHash = new Hashtable();

void Awake(){
	AudioListener al = this.GetComponent<AudioListener>();
	if(al == null){
		this.gameObject.AddComponent<AudioListener>();
		DontDestroyOnLoad(this.gameObject);
		initBgObj();	
		initEftObj();
	}
}
private void initBgObj (){
	bgMusic = Instantiate(Resources.Load(prefabsResourcesPath + bgPrefabsName)) as GameObject;
	DontDestroyOnLoad(bgMusic.gameObject);
	bgMusicObj = bgMusic.GetComponent<AudioSource>();
}

private void initEftObj (){
	effectMusic = Instantiate(Resources.Load(prefabsResourcesPath + effectPrefabsName)) as GameObject;
	DontDestroyOnLoad(effectMusic.gameObject);
	Component[] tempList;
	tempList = effectMusic.GetComponents<AudioSource>();
	effectMusicObjs = new AudioSource[tempList.Length];
	for(int i=0; i<tempList.Length; i++){
		effectMusicObjs[i] = tempList[i] as AudioSource;
		if(isEffectMute){
			effectMusicObjs[i].mute = true;
		}
	}
}
	
public void playMoveMusic(string heroType){
	if(MusicManager.Instance.moveMusicHash[heroType] == null){
		GameObject prefab = Resources.Load("audioPrefabs/move_music") as GameObject;
		GameObject moveMusicObj = Instantiate(prefab) as GameObject;			
		moveMusicObj.GetComponent<AudioSource>().Play();
		moveMusicHash[heroType] = 	moveMusicObj;
		moveMusicObj.GetComponent<AudioSource>().mute = isEffectMute? true : false;	
	}
}
	
public void stopMoveMusic(string heroType){
	if(moveMusicHash[heroType] != null){
		Destroy(moveMusicHash[heroType] as GameObject);
		moveMusicHash.Remove(heroType);
	}
}

public AudioClip getAudioClipByName ( string name  ){
	string s = MusicPathManager.instance.Get(name) + name;
	return Resources.Load(musicResourcesPath + s) as AudioClip;
}

//public Stack<string> bgStack = new Stack<string>();
public static void playBgMusic ( string musicName  ){
//	if(instance.bgStack.Count>0){
//		string top = instance.bgStack.Peek();
//		Debug.Log(" current top :"+top);
//		if(top == musicName) return; // same do nothing
//	}
	Instance._playBgMusic(musicName);
}
	private string currentMusic;
	private void _playBgMusic ( string musicName  ){
		if(currentMusic != musicName){
			currentMusic = musicName;
			bgMusicObj.loop = true;
			iTween.AudioTo(bgMusicObj.gameObject,iTween.Hash("volume",0,"pitch",1,"time",0.5,"oncomplete","fadeOutEnd","oncompletetarget",this.gameObject));
//			clearClip(bgMusicObj);
//			bgMusicObj.clip = getAudioClipByName(musicName);
//			bgMusicObj.Play();
		}else{
			if(!bgMusicObj.isPlaying){
				bgMusicObj.Play();
			}
		}
	}
	private void fadeOutEnd(){
		Debug.Log("fadeOutEnd, ready to play "+currentMusic);
		clearClip(bgMusicObj);
		bgMusicObj.clip = getAudioClipByName(currentMusic);
//		bgMusicObj.volume = 1;
		bgMusicObj.Play();
		iTween.AudioTo(bgMusicObj.gameObject,iTween.Hash("volume",1,"pitch",1,"time",0.5));
	}

private void clearClip ( AudioSource audio  ){
	if(audio.clip != null){
		audio.Stop();
		Resources.UnloadAsset(audio.clip);
	}
}
public void playSingleMusic(string musicName){
	playEffectMusic(musicName,0);	
	Instance.isSingleMusic = true;
}
	
public static AudioSource playEffectMusic ( string musicName , float crossfadeTime = 0 ){
	if(Instance.isSingleMusic) return null;
	return Instance._playEffectMusic(musicName,crossfadeTime);
}
private AudioSource _playEffectMusic ( string musicName , float crossfadeTime ){
	int audioIndex = getAudioIndex();
	AudioSource tempAudio = effectMusicObjs[audioIndex]; 
	tempAudio.clip = getAudioClipByName(musicName);
	tempAudio.Play();
	
	if(crossfadeTime >0){	
		if(bgMusicObj!=null && bgMusicObj.isPlaying){
			bgMusicObj.volume = 0.3f;
		}
		this.StopCoroutine("resumeBgMusic");
		this.StartCoroutine("resumeBgMusic",crossfadeTime);
	}
	return tempAudio;
}

private IEnumerator resumeBgMusic(float delay){
	yield return new WaitForSeconds(delay);
		Debug.Log("resumeBgMusic");
	if(bgMusic != null){
		iTween.AudioTo(bgMusic.gameObject,1, 1, 0.2f);
	}
}

public void replayBgMusic(string eftMusicName){
	foreach(AudioSource eftMusic in effectMusicObjs){
		if(eftMusic.isPlaying && eftMusic.clip == getAudioClipByName(eftMusicName)){
			iTween.AudioTo(eftMusic.gameObject,0.3f, 1, 0.2f);
			StartCoroutine(delayReplayBgMusic(eftMusic));
		}
	}
	if(bgMusic != null){
		iTween.AudioTo(bgMusic.gameObject,1, 1, 0.2f);
	}	
}
	
private IEnumerator delayReplayBgMusic(AudioSource eftMusic){
	yield return new WaitForSeconds(0.2f);
	eftMusic.volume = 1f;
//	eftMusic.clip.name = "";
	eftMusic.Stop();		
}

public void playEffectMusicForLoop ( string musicName  ){
	playEffectMusic(musicName);	
	foreach(AudioSource eftMusic in effectMusicObjs){
		if(eftMusic.isPlaying && eftMusic.clip == getAudioClipByName(musicName)){
			eftMusic.loop = true;
		}
	}
}
	
public void stopEffectMusicForLoop(string musicName){
	foreach(AudioSource eftMusic in effectMusicObjs){
		if(eftMusic.isPlaying && eftMusic.clip == getAudioClipByName(musicName)){
			eftMusic.Stop();
			eftMusic.loop = false;
		}
	}	
}

public static void cancleLoop ( int audioIndex  ){
	// no use
}

private int getAudioIndex (){
	for(int i=0 ; i<effectMusicObjs.Length ; i++){
		AudioSource tempAudio = effectMusicObjs[i];
		if(!tempAudio.isPlaying){
			return i;
		}
	}
	return 0;
}

public void pauseBgMusic (){
	if(bgMusic != null ){
		bgMusicObj.Pause();
	}
}

public void muteBgMusic (bool isMute){
	bgMusicObj.mute = isMute;
	isBgMute = isMute;
}

public void muteEffectMusic (bool isMute){
	for(int i=0 ; i<effectMusicObjs.Length ; i++){
		effectMusicObjs[i].mute = isMute;
	}
	isEffectMute = isMute;
}

public void bgVolume ( float vol  ){
	bgMusicObj.volume = vol;
}

public void effectVolume ( float vol  ){
	for(int i=0 ; i<effectMusicObjs.Length ; i++){
		effectMusicObjs[i].volume = vol;
	}
}

}
