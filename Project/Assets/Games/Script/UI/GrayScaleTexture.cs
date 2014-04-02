using UnityEngine;
using System.Collections;

public class GrayScaleTexture : MonoBehaviour {

	public UISprite sp;
	public UITexture tx;
	public Shader shader;
	
	public void Enable(){
		tx.gameObject.SetActive(true);
		tx.mainTexture = sp.mainTexture;
		tx.uvRect = sp.innerUV;
		tx.shader = shader;
		tx.transform.localScale = sp.transform.localScale;
		tx.transform.localPosition = sp.transform.localPosition;
		sp.gameObject.SetActive(false);
	}
	
	public void Disable(){
		sp.enabled = true;
		sp.gameObject.SetActive(true);
		tx.gameObject.SetActive(false);
	}
}
