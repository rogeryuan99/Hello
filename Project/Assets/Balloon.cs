using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {
	public UILabel label;
	public UISprite balloon;
	public UISprite arrow;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		balloon.transform.localScale = label.mSize*40f;
		Debug.Log(label.mSize);
	}
}
