using UnityEngine;
using System.Collections;

public class GrayTextureTest : MonoBehaviour {
	
	public UISprite sp;
	public UITexture tx;
	public Shader shader;
	
	public void OnGUI(){
		if (GUI.Button(new Rect(100f,100f,100f,50f), "Enable")){
			Enable();
		}
		if (GUI.Button(new Rect(100f,200f,100f,50f), "Disable")){
			Disable();
		}
	}
	
	public void Enable(){
		tx.gameObject.SetActive(true);
		tx.mainTexture = sp.mainTexture;
		tx.uvRect = sp.innerUV;
		tx.shader = shader;
		tx.transform.localScale = sp.transform.localScale;
		sp.gameObject.SetActive(false);
	}
	
	public void Disable(){
		sp.enabled = true;
		sp.gameObject.SetActive(true);
		tx.gameObject.SetActive(false);
	}
}
