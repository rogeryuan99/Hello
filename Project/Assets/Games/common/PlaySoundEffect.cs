//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Plays the specified sound.
/// </summary>

//[AddComponentMenu("Script/Common/Sound Effect")]
public class PlaySoundEffect : MonoBehaviour
{
	public enum Trigger
	{
		OnClick,
		OnMouseOver,
		OnMouseOut,
		OnPress,
		OnRelease,
	}

	public string se_id = "Button_Click";
	public Trigger trigger = Trigger.OnClick;
	public float volume = 1f;
	public float pitch = 1f;

	void OnHover (bool isOver)
	{
		if (enabled && ((isOver && trigger == Trigger.OnMouseOver) || (!isOver && trigger == Trigger.OnMouseOut)))
		{
			//NGUITools.PlaySound(audioClip, volume, pitch);
		}
	}

	void OnPress (bool isPressed)
	{
		if (enabled && ((isPressed && trigger == Trigger.OnPress) || (!isPressed && trigger == Trigger.OnRelease)))
		{
			//NGUITools.PlaySound(audioClip, volume, pitch);
		}
	}

	void OnClick ()
	{
		if (enabled && trigger == Trigger.OnClick)
		{
			SoundManager.getInstance().playEffect(se_id);
		}
	}
}