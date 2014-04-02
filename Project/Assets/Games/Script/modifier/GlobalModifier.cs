using UnityEngine;
using System.Collections;

public class GlobalModifier {
//	public static int exp;
	public static int gold;
	public static int fuelChance;
	public static int de_def;//enemy reduce defense
	public static int luck;
	
	public static void reset (){
//		exp  = 0;
		gold = 0;
		fuelChance = 0;
		de_def = 0;
		luck = 0;
	}
}