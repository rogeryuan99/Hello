using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class SaveGameManager{
	static private SaveGameManager self;
	static private string saveFilePath;
	private Hashtable appDatahash;
	//private SimpleAES aes;
	private System.Text.ASCIIEncoding encoding;
	private System.Text.UTF8Encoding utfEncoding;
	private byte[] xorkey = new byte[]{12,24,36,63,42,21};
	
	
	public static SaveGameManager instance (){
		if (self==null) {
			self = new SaveGameManager();
			//self.init();
		}
		return self;
	}
	
	public void SetInt ( string key ,   int value  ){
		if (null != appDatahash) {
			appDatahash[key] = value;
			saveToFile();
		}
	}
	
	public void SetLong ( string key ,   long value  ){
		if (null != appDatahash) {
			appDatahash[key] = value;
			saveToFile();
		}
	}
	
	public void SetString ( string key ,   string value  ){
		if (null != appDatahash) {
			appDatahash[key] = value;
			saveToFile();
		}
	}
	public void SetObject ( string key ,   object value  ){
		if (null != appDatahash) {
			appDatahash[key] = value;
			saveToFile();
		}
	}
	
	public void SetStringNoSave ( string key ,   string value  ){
		if (null != appDatahash) {
			appDatahash[key] = value;
//			saveToFile();
		}
	}
	
	public int GetInt ( string key  ){
		return GetInt(key, 0);
	}
	
	public long GetLong ( string key  ){
		return GetLong(key, 0);
	}
	
	public object GetObject ( string key  ){
		if (appDatahash != null) {
			return appDatahash[key];
		}else {
			return null;
		}
	}
	
	public int GetInt ( string key ,   int defaultValue  ){
		if (appDatahash != null) {
			if (appDatahash.ContainsKey(key)){
				return int.Parse(appDatahash[key].ToString());
			}else {
				return defaultValue;
			}
		}else {
			return defaultValue;
		}
	}
	
	public long GetLong ( string key ,   long defaultValue  ){
		if (appDatahash != null) {
			if (appDatahash.ContainsKey(key)){
				return long.Parse(appDatahash[key].ToString());
			}else {
				return defaultValue;
			}
		}else {
			return defaultValue;
		}
	}
	
	public string GetString ( string key  ){
		if (appDatahash != null) {
			if (appDatahash.ContainsKey(key)){
				return appDatahash[key].ToString();
			}else {
				return "";
			}
		}else {
			return "";
		}
	}
	
	public bool HasKey ( string key  )
	{
		return (appDatahash.ContainsKey(key));
	}
	
	public void init (){
		Debug.Log("init !!!!!!!!!!!!");
		saveFilePath = Application.persistentDataPath+"/data"+BuildSetting.LocalSaveVersion+".txt";
		Debug.Log("saveFilePath="+saveFilePath);
		
		appDatahash = new Hashtable();
		encoding = new System.Text.ASCIIEncoding();
		utfEncoding = new System.Text.UTF8Encoding();
		//aes = new SimpleAES();
	}
	
	public void deleteSavedData(){
		if (saveDataExist()) {
			System.IO.File.Delete(saveFilePath);
		}
	}
	public void loadLocalSavedData (){
		if (saveDataExist()) {
			loadFromFile();
		}
	}
	
	private bool saveDataExist (){
		 return (System.IO.File.Exists(saveFilePath));
	}
	
	public byte[] xorBytes ( byte[] bytes  ){
		Debug.Log(bytes.ToString());
		//FIXME_VAR_TYPE originalBytes= System.Text.ASCIIEncoding.GetBytes(saveDataString);
		byte[] xoredBytes = new byte[bytes.Length];
		for (int i = 0; i < bytes.Length; i++) {
			int keyoffset = i%6;
			xoredBytes[i] = (byte)(bytes[i] ^ xorkey[keyoffset]);
		}
		Debug.Log(xoredBytes.ToString());
		return xoredBytes;
	}
	
	public string bytesToString ( byte[] bytes  ){
		return utfEncoding.GetString(bytes);
	}
	
	public void saveToFile (){
		//if (!aes) {aes = new SimpleAES();}
		
		string saveDataString= MiniJSON.jsonEncode(appDatahash);
		byte[] bytesToSave= encoding.GetBytes(saveDataString);//
		try {
			//System.IO.File.WriteAllBytes(saveFilePath, bytesToSave);
			System.IO.FileStream stream = new FileStream(saveFilePath, FileMode.Create); 
//			{
		    stream.Write(bytesToSave, 0, bytesToSave.Length);
		    stream.Close();
//			}
		}catch (Exception e) {
			Debug.LogError("Error writing file to disk: " + e.Message);
//			System.IO.File.Delete(saveFilePath);
		}
		//System.IO.File.WriteAllText(saveFilePath, saveDataString);
		//System.IO.File.Encrypt(saveFilePath);
		//byte[] saveDataCipher = aes.Encrypt("fuckuaesfuckuaes");
		/*
		Debug.Log("to save cipher length: "+saveDataCipher.Length);
		Debug.Log("cipher to save: " + new Array(saveDataCipher).Join("-"));
		Debug.Log("cipher decrypted: " + aes.Decrypt(saveDataCipher));
		*/
		//System.IO.File.WriteAllBytes(saveFilePath, saveDataCipher);
		
	}
	
	private void loadFromFile (){
		//if (!aes) {aes = new SimpleAES();}
		//System.IO.File.Decrypt(saveFilePath);
		//byte[] saveDataCipher = System.IO.File.ReadAllBytes(saveFilePath);
		/*
		Debug.Log("loaded cipher length: "+saveDataCipher.Length);
		Debug.Log("cipher loaded: "+ new Array(saveDataCipher).Join("-"));
		Debug.Log("cipher decrypted: " + aes.Decrypt(saveDataCipher));
		*/
		//string saveDataString = aes.Decrypt(saveDataCipher);
		//Debug.Log("saveDataString:" + saveDataString);
		byte[] xoredBytes= System.IO.File.ReadAllBytes(saveFilePath);
		
		//byte[] originalBytes= xorBytes(xoredBytes);
//		
		string saveDataString = encoding.GetString(xoredBytes);
		
		Hashtable loadedHashtable = (Hashtable)MiniJSON.jsonDecode(saveDataString);
		
		if (null != loadedHashtable) {
			foreach(string key in loadedHashtable.Keys) {
				appDatahash.Add(key, loadedHashtable[key]);
			}
		}
		//appDatahash. = 
	}
	public void initFromServerData(Hashtable h){
		appDatahash = h;
	}
}
