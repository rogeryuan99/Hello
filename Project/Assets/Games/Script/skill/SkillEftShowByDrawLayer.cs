using UnityEngine;
using System.Collections;

public class SkillEftShowByDrawLayer : MonoBehaviour 
{
	public PackedSprite flag;
	protected PackedSprite self;
	
	public void Awake()
	{
		self = GetComponent<PackedSprite>();
	}
	
	public void Update()
	{
		if(flag.IsHidden())
		{
			self.Hide(true);
		}
		else
		{
			self.Hide(false);
			if(self.animations.Length > 0 && !self.IsAnimating())
			{
				self.PlayAnim(0);
			}
		}
		
	}
}
