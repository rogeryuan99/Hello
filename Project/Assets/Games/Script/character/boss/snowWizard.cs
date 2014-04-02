using UnityEngine;
using System.Collections;

public class snowWizard : EnemyRemote {
	protected override void AnimaPlayEnd ( string animaName  ){
//		Debug.Log(">>>>>>animaName:"+animaName);
		switch(animaName)
		{
			case "Attack":
				isPlayAtkAnim = false;
				playAnim("Move");
				pieceAnima.pauseAnima();
				break;
			case "Damage":
				playAnim("Move");
				pieceAnima.pauseAnima();
				break;
			case "Death":
				pieceAnima.pauseAnima();
				
				Invoke("destroyThis",3);
				
				break;
			default:
				break;
		}
	}
}
