using UnityEngine;
using System.Collections;

public class Skill_REDKING15A : SkillBase {
	private Object ballPrefab;
	private Object ballLightingPrefab;
	private Object crackPrefab;
	private Object crackLightingPrefab;
	
	private GameObject ball;
	private GameObject ballLighting;
	private ArrayList objs;

	public override IEnumerator Cast (ArrayList objs)
	{
		this.objs = objs;
		GameObject caller = objs[1] as GameObject;
		GameObject target = objs[2] as GameObject;
		Character redking   = caller.GetComponent<Character>();
		redking.castSkill("Skill15A");
		yield return new WaitForSeconds(1.3f);
		StartCoroutine(SkillManager.Instance.slowMotion(0.0f, 0.15f));
		StartCoroutine(SkillManager.Instance.chanageBGTextureColor(0.0f, 0.15f));
		StartCoroutine(SkillManager.Instance.shakeCamera(new Vector3(0,160,0), 0.15f, 0f));
		createBall();
		createBallLighting();
		StartCoroutine(createCarck());
		createCrackLighting();
		addBuff();
	}
	private void createBall(){
		GameObject caller = objs[1] as GameObject;
		if (null == ballPrefab){
			ballPrefab = Resources.Load("eft/RedKing/SkillEft_RedKing15_Ball");
		}
		if(null == ball){
			ball = Instantiate(ballPrefab) as GameObject;
		}
		ball.transform.localScale = new Vector3(.75f,.75f,.75f);
		ball.transform.position = caller.transform.position + new Vector3(0,70f,-2f);
		ball.transform.parent = caller.transform;
	}
	
	private void createBallLighting(){
		GameObject caller = objs[1] as GameObject;
		if (null == ballLightingPrefab){
			ballLightingPrefab = Resources.Load("eft/RedKing/SkillEft_RedKing15_Ball_Lightning");
		}
		if(null == ballLighting){
			ballLighting = Instantiate(ballLightingPrefab) as GameObject;
		}
		ballLighting.transform.localScale = new Vector3(.75f,.75f,.75f);
		ballLighting.transform.position = caller.transform.position + new Vector3(0,70f,-3f);
		ballLighting.transform.parent = caller.transform;
	}
	
	private IEnumerator createCarck(){
		GameObject caller = objs[1] as GameObject;
		Character redking   = caller.GetComponent<Character>();
		if (null == crackPrefab){
			crackPrefab = Resources.Load("eft/RedKing/SkillEft_RedKing15_Crack");
		}
		GameObject crack = Instantiate(crackPrefab) as GameObject;
		bool isLeftSide = redking.model.transform.localScale.x > 0;
		crack.transform.localScale = new Vector3(isLeftSide? 0.75f:-0.75f, 0.75f, 0.75f);
		crack.transform.position = caller.transform.position + new Vector3(isLeftSide? 40f: -40f, -40f, 0f);
		yield return new WaitForSeconds(.15f);
		Destroy(crack);
	}
	
	private void createCrackLighting(){
		GameObject caller = objs[1] as GameObject;
		Character redking   = caller.GetComponent<Character>();
		if (null == crackLightingPrefab){
			crackLightingPrefab = Resources.Load("eft/RedKing/SkillEft_RedKing15_Crack_Lighting");
		}
		GameObject crackLighting = Instantiate(crackLightingPrefab) as GameObject;
		bool isLeftSide = redking.model.transform.localScale.x > 0;
		crackLighting.transform.localScale = new Vector3(isLeftSide? 0.75f:-0.75f, 0.75f, 0.75f);
		crackLighting.transform.position = caller.transform.position + new Vector3(isLeftSide? 43f: -43f, -41f, -1f);
	}
	
	private void addBuff(){
		GameObject caller = objs[1] as GameObject;
		Character redking   = caller.GetComponent<Character>();
		SkillDef def = SkillLib.instance.getSkillDefBySkillID("REDKING15A");
		int time = def.buffDurationTime;
		float v = ((Effect)def.buffEffectTable["def_PHY"]).num * 0.01f * redking.realDef.PHY;
		redking.addBuff("SKILL_REDKING15A", time, v, BuffTypes.DEF_PHY, buffFinish);
	}
	
	public void buffFinish(Character character, Buff self)
	{
		Destroy(ball);
		Destroy(ballLighting);
	}
}
