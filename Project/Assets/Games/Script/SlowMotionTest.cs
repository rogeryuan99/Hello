using UnityEngine;
using System.Collections;

public class SlowMotionTest : MonoBehaviour {
	
	protected float countTime = 0;
	protected float frameCount = 0;
	
	
	public void OnGUI(){
		if (GUI.Button(new Rect(0, 100, 100, 50), "SlowSpeed")){
			Time.timeScale = 0.1f;
		}
		if (GUI.Button(new Rect(0, 150, 100, 50), "normalSpeed")){
			Time.timeScale = 1.0f;
		}
		if (GUI.Button(new Rect(0, 200, 100, 50), "normalSpeed")){
			Time.timeScale = 0.1f;
			StartCoroutine(s ());
		}
		if (GUI.Button(new Rect(0, 250, 100, 50), "111111")){
			GameObject h = GameObject.Find("STARLORD(Clone)");
			if(h != null)
			{
				StarLord s = h.GetComponent<StarLord>();
				s.playAnim("Damage");
			}
			
			
			h = GameObject.Find("ROCKET(Clone)");
			if(h != null)
			{
				Rocket s = h.GetComponent<Rocket>();
				s.playAnim("Damage");
			}
		}
	}
	
	public IEnumerator s()
	{
		yield return new WaitForSeconds(1.5f);
		Time.timeScale = 1.0f;
	}
	
	public void Update ()
	{
//		countTime += Time.deltaTime;
//		Debug.Log(countTime);
//		if (countTime / (1.0f / 24.0f) > 1)
//		{	
//			countTime = 0;
//			//print(currentAnima+"->"+frameCount);
//			Debug.Log (frameCount);
//			
//			frameCount = (++frameCount + 0) % 99;
//		}
	}
}
