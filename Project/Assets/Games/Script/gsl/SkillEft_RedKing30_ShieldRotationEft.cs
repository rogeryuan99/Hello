using UnityEngine;
using System.Collections;

public class SkillEft_RedKing30_ShieldRotationEft : MonoBehaviour {
	public Autorotation rotateParent;
	public Autorotation rotatePart1;
	public Autorotation rotatePart2;
	public Autorotation rotatePart3;
	public Autorotation rotatePart4;
	
	public void setToggle(bool isPlay){
		if(isPlay){
			rotateParent.enabled = true;
			rotatePart1.enabled = true;
			rotatePart2.enabled = true;
			rotatePart3.enabled = true;
			rotatePart4.enabled = true;
		}
	}
}
