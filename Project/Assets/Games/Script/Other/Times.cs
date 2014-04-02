using UnityEngine;
using System.Collections;

[AddComponentMenu ("My_Self_Script/UnUsed/Times")]
public class Times : MonoBehaviour {
	
// Jugg
public UILabel timeSpriteText;
public string timeString;

private int minute = 0;
private int secondInt = 0;
public int millisecondInt = 0;

private float second = 0.0f;
private float millisecond = 0.0f;

public float millsecondfl = 0.0f;
public int millsecondin = 0;
public float secondfl = 0.0f;
public int secondNum = 0;

public void Update (){		
		second += 0.02f;
		secondInt = int.Parse(second.ToString ());
		
		
		secondfl += 0.02f;
		secondNum = int.Parse(secondfl.ToString ());
		millsecondfl = secondfl*100;
		millsecondin = int.Parse(millsecondfl.ToString ());
		
		if(second <= 0.04f)
		{
			millisecond = 0;
		}
		else if(second < 1)
		{
			millisecond = second*100;
		}
		else
		{
			millisecond = (second - secondInt)*100;
		}
		
		millisecondInt = int.Parse(millisecond.ToString ());
		
		if(secondInt == 60)
		{
			minute += 1;
			second = 0.0f;
			secondInt = 0;
		}
		
		if(minute == 60)
		{
			minute = 0;
		}
		timeString = System.String.Format("{0:D2}", minute + "\"" + secondInt + "\"" + millisecondInt);
		timeSpriteText.text = timeString;
}
}