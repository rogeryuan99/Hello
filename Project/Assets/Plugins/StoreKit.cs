using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;



public static class StoreKit {

	static string productIdPrefix_;

	public static void Install(string productIdPrefix, string gon, string cmcb, string cncb) {
		productIdPrefix_ = productIdPrefix;
	}

	public static void Buy(string productName) {
	}
}
