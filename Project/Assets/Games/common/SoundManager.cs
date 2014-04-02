using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;

public class SoundManager :MonoBehaviour
{
	private AudioSource currentEffect;
	private AudioSource curBgMusic;
	private bool isEffectMuted;
	private bool isMusicMuted;
	
	public enum Effects
	{
		Battle_Attack1,
		Battle_Attack2,
		Battle_Attack3,
		Battle_Attack4,
		Battle_Attack5,
		Battle_Attack6,
		Battle_Lose,
		Battle_Skill1,
		Battle_Skill2,
		Battle_Skill3,
		Battle_Win,
		Button_Click,
		Summon_Success
	}
	
	public enum Musics
	{
	    Music_Bg_1
	}
	
	private static SoundManager _instance;
	
	public static SoundManager getInstance()
	{
		if (!_instance)
		{
	        GameObject pre = (GameObject)Resources.Load("SoundManager");
			GameObject o = (GameObject)Instantiate(pre);
	        _instance = o.GetComponent<SoundManager>();
	    }
	    return _instance; 
	}
	
	public void Start ()
	{
	    DontDestroyOnLoad(gameObject);
	}
	
	public void playEffect(string se_id)
	{  
	    GameObject o = GameObject.Find(se_id);
	    currentEffect = o.GetComponent<AudioSource>();
	    currentEffect.mute = isEffectMuted;
	    currentEffect.Play();
	}
	
	public void  playMusic(string se_id)
	{ 
	    if(curBgMusic!=null)
	    { 
	       curBgMusic.Stop();   
	    } 
	    curBgMusic = GameObject.Find(se_id).GetComponent<AudioSource>();
		curBgMusic.mute = isMusicMuted;
	    curBgMusic.Play();
	}
		
	public void stopMusic()
	{
		if(curBgMusic != null)
			curBgMusic.Stop();
	}
	
	public void muteEffect(bool isMuted)
	{
	    isEffectMuted = isMuted;
	    if(currentEffect!=null)
	       currentEffect.mute = isMuted;
	}
	
	public void muteMusic(bool isMuted)
	{
	    isMusicMuted = isMuted;
	    if(curBgMusic!=null)
	        curBgMusic.mute = isMusicMuted;
	}
}
