//#define ENCRYPT
using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Xml.Serialization;

public class SaveUtils
{
	
	
	static byte[] bytes = ASCIIEncoding.ASCII.GetBytes ("ZeroCool");

	public static void testGood ()
	{
		
	}

	public static void test ()
	{
		
	}
	
	private static string saveKey = "1234567890";
	
	public static void EncryptAndSave (string filename, string content)
	{
		string encryptionKey = saveKey;
		var key = new DESCryptoServiceProvider ();
		var e = key.CreateEncryptor (Encoding.ASCII.GetBytes ("64bitPas"), Encoding.ASCII.GetBytes (encryptionKey));
		using (var fs = File.Open(filename, FileMode.Create)) {
#if ENCRYPT			
		using (var cs = new CryptoStream(fs, e, CryptoStreamMode.Write)){
			byte[] data = ASCIIEncoding.ASCII.GetBytes(content);
			cs.Write(data,0,data.Length);				
        	cs.Flush();
		}
#else
			byte[] data = ASCIIEncoding.ASCII.GetBytes(content);
			fs.Write(data,0,data.Length);				
        	fs.Flush();
#endif		
		}
	}

	public static string DecryptAndRead (string filename)
	{
		string encryptionKey = saveKey;
		var key = new DESCryptoServiceProvider ();
		var d = key.CreateDecryptor (Encoding.ASCII.GetBytes ("64bitPas"), Encoding.ASCII.GetBytes (encryptionKey));
		using (var fs = File.Open(filename, FileMode.Open)) {
#if ENCRYPT			
		using (var cs = new CryptoStream(fs, d, CryptoStreamMode.Read)){
			StreamReader reader = new StreamReader(cs);
			string data = reader.ReadToEnd();
			reader.Close();
			return data;
		}
#else
			StreamReader reader = new StreamReader (fs);
			string data = reader.ReadToEnd ();
			reader.Close ();
			return data;

#endif				
		}
	}
	
	public static void EncryptAndSerialize<T> (string filename, T obj)
	{
		string encryptionKey = saveKey;
		var key = new DESCryptoServiceProvider ();
		var e = key.CreateEncryptor (Encoding.ASCII.GetBytes ("64bitPas"), Encoding.ASCII.GetBytes (encryptionKey));
		using (var fs = File.Open(filename, FileMode.Create)) {
#if ENCRYPT			
		using (var cs = new CryptoStream(fs, e, CryptoStreamMode.Write)){
			(new XmlSerializer (typeof(T))).Serialize (cs, obj);
		}
#else
			(new XmlSerializer (typeof(T))).Serialize (fs, obj);
#endif
		}
	}

	public static T DecryptAndDeserialize<T> (string filename)
	{
		string encryptionKey = saveKey;
		var key = new DESCryptoServiceProvider ();
		var d = key.CreateDecryptor (Encoding.ASCII.GetBytes ("64bitPas"), Encoding.ASCII.GetBytes (encryptionKey));
		using (var fs = File.Open(filename, FileMode.Open)) {
#if ENCRYPT			
		using (var cs = new CryptoStream(fs, d, CryptoStreamMode.Read)){
			return (T)(new XmlSerializer (typeof(T))).Deserialize (cs);
			}
#else
			return (T)(new XmlSerializer (typeof(T))).Deserialize (fs);
#endif		
		}
	}
	
	private static string masterKey = "bunny20123456789";
	
	public static byte[] generateKey (string userKey)
	{
		string sk = "";
		for (int i = 0; i < masterKey.Length; i++) {
			sk += System.Convert.ToChar (('A' + 'Z' - System.Char.ToUpper (masterKey [i]))).ToString ();
		}
		
		string message;
		string key;

		key = sk;
		message = userKey;

		System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding ();

		byte[] keyByte = encoding.GetBytes (key);

		HMACSHA1 hmacsha1 = new HMACSHA1 (keyByte);

		byte[] messageBytes = encoding.GetBytes (message);

		byte[] hashmessage = hmacsha1.ComputeHash (messageBytes);
		
		byte[] output = new byte[8];
		System.Array.Copy (hashmessage, 8, output, 0, 8);
        
		return output;
	}
	
	public static string decryptString (string userKey, string cryptedString)
	{
		byte[] saveKey = SaveUtils.generateKey (userKey);
		
		DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider ();
		MemoryStream memoryStream = new MemoryStream (System.Convert.FromBase64String (cryptedString));
		CryptoStream cryptoStream = new CryptoStream (memoryStream, cryptoProvider.CreateDecryptor (saveKey, saveKey), CryptoStreamMode.Read);
		StreamReader reader = new StreamReader (cryptoStream);
		return reader.ReadToEnd ();
	}
	
	public static string encryptString (string userKey, string originalString)
	{
		byte[] saveKey = SaveUtils.generateKey (userKey);
		
		DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider ();
		MemoryStream memoryStream = new MemoryStream ();
		CryptoStream cryptoStream = new CryptoStream (memoryStream, cryptoProvider.CreateEncryptor (saveKey, saveKey), CryptoStreamMode.Write);
		StreamWriter writer = new StreamWriter (cryptoStream);
		writer.Write (originalString);
		writer.Flush ();
		cryptoStream.FlushFinalBlock ();
		writer.Flush ();
		return System.Convert.ToBase64String (memoryStream.GetBuffer (), 0, (int)memoryStream.Length);
	}
	
	public static string encryptStream (string userKey, string originalString)
	{
		byte[] saveKey = SaveUtils.generateKey (userKey);
		
		DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider ();
		MemoryStream memoryStream = new MemoryStream ();
		CryptoStream cryptoStream = new CryptoStream (memoryStream, cryptoProvider.CreateEncryptor (saveKey, saveKey), CryptoStreamMode.Write);
		StreamWriter writer = new StreamWriter (cryptoStream);
		writer.Write (originalString);
		writer.Flush ();
		cryptoStream.FlushFinalBlock ();
		writer.Flush ();
		return System.Convert.ToBase64String (memoryStream.GetBuffer (), 0, (int)memoryStream.Length);
	}
}
