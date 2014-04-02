using UnityEngine;
using System.Collections;

public class TsGrayLockOrGlowTool : MonoBehaviour {

	public Collider collider;
	public GameObject gray;
	public GameObject glow;
	
	public void Normal(){
		if (null != gray)     gray.SetActive(false);
		if (null != collider) collider.enabled = true;
		if (null != glow)     glow.SetActive(false);
	}
	
	public void GrayLock(){
		if (null != gray)     gray.SetActive(true);
		if (null != collider) collider.enabled = false;
		if (null != glow)     glow.SetActive(false);
	}
	
	public void Glow(){
		if (null != gray)     gray.SetActive(false);
		if (null != collider) collider.enabled = true;
		if (null != glow)     glow.SetActive(true);
	}
	
	public void GlowByTutorial(string[] parms){
		if (TsFtueManager.Instance.HasDoneBefore(parms[0])) 
			Normal();
		else
			Glow();
	}
	
	public void FingerTipByTutorial(string[] parms){
		Normal();
		if (!TsFtueManager.Instance.HasDoneBefore(parms[0])) {
			Object prefab = Resources.Load(string.Format("{0}/{1}", parms[1], parms[2]));
			GameObject gameObject = Instantiate(prefab) as GameObject;
			gameObject.transform.parent = GameObject.Find("UIRoot").transform;
			gameObject.transform.localScale = Vector3.one;
			gameObject.name = parms[2];
			TsFingerTip finger = gameObject.GetComponent<TsFingerTip>();
			
			finger.AddMotion(string.Format("MoveToSomething|{0}", parms[3]).Split('|'));
			finger.AddMotion("ClickPrompt".Split('|'));
			finger.AddMotion("Wait|1".Split('|'));
			finger.PlayMotion();
		}
	}
}
