//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Sample script showing how easy it is to implement a standard button that swaps sprites.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : MonoBehaviour
{
	public UISprite target;
	public string normalSprite;
	public string hoverSprite;
	public string pressedSprite;
	public string disabledSprite;
	public bool isPressed;
	
	public bool isEnabled
	{
		get
		{
			Collider col = collider;
			return col && col.enabled;
		}
		set
		{
			Collider col = collider;
			if (!col) return;

			if (col.enabled != value)
			{
				col.enabled = value;				
			}
		}
	}

	void OnEnable ()
	{
		UpdateImage();		
	}

	void Start ()
	{
		if (target == null) target = GetComponentInChildren<UISprite>();
	}

	void OnHover (bool isOver)
	{
		if (enabled)
		{
			UpdateImage();
		}
	}

	void OnPress (bool pressed)
	{
		if (enabled)
		{
			isPressed = pressed;
			UpdateImage();
		}
	}
	
	void UpdateImage()
	{
		if (target != null)
		{		
			if (isEnabled && !isPressed)
				target.spriteName = UICamera.IsHighlighted(gameObject) ? hoverSprite : normalSprite;
			else if(isPressed)
				target.spriteName = pressedSprite;
			else
				target.spriteName = disabledSprite;
				
			target.MakePixelPerfect();
		}
	}
}