using UnityEngine;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public class AuthUtils : MonoBehaviour
{

	public static string generateRequestToken (string playerId, string secret)
	{
		Hashtable h = new Hashtable ();
		h.Add ("userId", playerId);
		h.Add("expires",TimeUtils.UnixTime);
		string jsonString = MiniJSON.jsonEncode(h);
		string part1 = HMACSHA256Encode(jsonString,secret);
		string all = part1+"."+jsonString;
		string rt = Base64Encode(all);
		return rt;
	}

	public static string HMACSHA256Encode (string input, string key)
	{
		byte[] k = Encoding.ASCII.GetBytes (key);
		HMACSHA256 myhmacsha256 = new HMACSHA256 (k);
		byte[] byteArray = Encoding.ASCII.GetBytes (input);
		using (MemoryStream stream = new MemoryStream (byteArray)){
			return byteToHex(myhmacsha256.ComputeHash (stream));
		}
		
	}

	public static void tryEncode()
	{
		string key = "thePlayerSecret";
		string input = "{\"userId\":\"15\",\"expires\":\"1385059778\"}";
		string result = HMACSHA256Encode (input, key);
		Debug.Log(result);
		Debug.Log(" correct = "+ result.Equals("4f92d1026742aa26d86fdf910702a83315bb3f6a7508f0761396869230c53926"));
	}
	
	public static string Base64Encode(string plainText) {
	  byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
	  return System.Convert.ToBase64String(plainTextBytes);
	}
	
	public static string Base64Decode(string base64EncodedData) {
	  byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
	  return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
	}	
	
	public static string byteToHex(byte[] byteArray) {
	    StringBuilder result = new StringBuilder();
	    foreach (byte b in byteArray) {
			result.AppendFormat("{0:x2}",b);
	    }
    	return result.ToString();
 	}
}
