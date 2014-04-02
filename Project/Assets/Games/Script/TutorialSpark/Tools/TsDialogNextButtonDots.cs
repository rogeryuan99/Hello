using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TsDialogNextButtonDots : MonoBehaviour {
	public float interval = 1f;
	private float time;
	private int currentFrame = 0;
	public List<UISprite> dots;
	
	private int [,] arr = 
	{
		{1,1,1},
		{3,1,1},
		{2,3,1},
		{1,2,3}, 
		{1,1,2}, 
		{1,1,1},
		{1,1,1},
		{1,1,1},
		{1,1,1},
		{1,1,1},
	}; 
	
	void Update () {
		time += Time.deltaTime;
		if(time >= interval){
			time = 0;
			currentFrame = (currentFrame +1) % 10;
			for(int n = 0; n<3; n++){
				dots[n].spriteName= "ftue_dot"+arr[currentFrame,n];
				dots[n].MakePixelPerfect();
			}
		}
	}

	
	
}
