using UnityEngine;
using System.Collections;

public class difficultyManager : MonoBehaviour {


public static int maxDifficulty = 1;

public static void unlockNextDifficulty (){
	if (StaticData.difLevel == maxDifficulty) {
		maxDifficulty++;
		if (maxDifficulty <= 3) {//next level unlocked
			//Alert.show("Congratulations!/nYou've unlocked another difficulty level!/nTry it now - but you can always switch back to lower levels through the button below!");
			StaticData.difLevel = maxDifficulty;
		}else {//difficulty already maxed
			maxDifficulty = 3;
		}
		saveDifficultySettings();
	}
}

public static void saveDifficultySettings (){
	SaveGameManager.instance().SetInt("SFHCurrentDifficulty", StaticData.difLevel);
	SaveGameManager.instance().SetInt("SFHMaxUnlockedDifficulty", maxDifficulty);
}

public static void loadDifficultySettings (){
	maxDifficulty = SaveGameManager.instance().GetInt("SFHMaxUnlockedDifficulty", 1);
	StaticData.difLevel = SaveGameManager.instance().GetInt("SFHCurrentDifficulty", 1);
}

public static void setCurrentDifficulty ( int difficulty  ){
	StaticData.difLevel = difficulty;
	saveDifficultySettings();
}
}