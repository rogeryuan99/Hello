// Build a folder containing unity3d file and html file
class BuildTest
{
	@MenuItem ("Build/Skill Scene")
	static function BuildSkillScene(){
		BuildPipeline.BuildStreamedSceneAssetBundle( ["Assets/Games/Scenes/skill.unity", "Assets/Games/Scenes/skill_iphone.unity"], 
       												"skillTreePanel.unity3d", BuildTarget.iPhone);
       	Debug.Log("Skill scene");
	}
	
	@MenuItem ("Build/FriendList Scene")
	static function BuildFriendList(){
       	 BuildPipeline.BuildStreamedSceneAssetBundle( ["Assets/Games/Scenes/FriendList.unity"],//, "Assets/Games/Scenes/FriendList_iphone.unity"], 
       												"FriendList.unity3d", BuildTarget.iPhone);
       	Debug.Log("FriendList scene");
	}
	
	@MenuItem ("Build/Armory Scene")
	static function BuildArmory(){
        	BuildPipeline.BuildStreamedSceneAssetBundle( ["Assets/Games/Scenes/iPad/Armory.unity", "Assets/Games/Scenes/iPad/Armory_iphone.unity"], 
       												"Armory.unity3d", BuildTarget.iPhone); 
       		Debug.Log("Armory scene");
	}
	
	@MenuItem ("Build/EditTeam Scene")
	static function BuildEditTeam(){
        	BuildPipeline.BuildStreamedSceneAssetBundle( ["Assets/Games/Scenes/editTeam.unity"],// "Assets/Games/Scenes/editTeam_iphone.unity"], 
       												"editTeam.unity3d", BuildTarget.iPhone); 
       												Debug.Log("editTeam scene");
	}
	@MenuItem ("Build/Merchant Scene")
	static function BuildMerchant(){
        	BuildPipeline.BuildStreamedSceneAssetBundle( ["Assets/Games/Scenes/Merchant_New.unity", "Assets/Games/Scenes/Merchant_New_iphone.unity"], 
       												"Merchant_New.unity3d", BuildTarget.iPhone); 
       												Debug.Log("Merchant_New scene");
	}
	@MenuItem ("Build/yourTeam Scene")
	static function BuildyourTeam(){
        	BuildPipeline.BuildStreamedSceneAssetBundle( ["Assets/Games/Scenes/yourTeam.unity"],//, "Assets/Games/Scenes/yourTeam_iphone.unity"], 
       												"yourTeam.unity3d", BuildTarget.iPhone); 
       												Debug.Log("yourTeam scene");
	}
}
