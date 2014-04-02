#define LOCAL

using UnityEngine;
using System.Collections;
public class BuildSetting : MonoBehaviour {
#if LOCAL	
	public const bool autoReg_n_login = false;
	public const string LocalSaveVersion = "20";
	public const bool UseServerConfig = false;
	public const bool LOCAL_READ = true;
	public const bool LOCAL_SAVE = true;
	public const string gameServerHost = "";
	//public const string gameServerUrl = "http://192.168.2.129:8080/game/batch/json";
	public const string gameServerUrl = "http://n7vgd1ggaxmapp01.general.disney.private:8080/app/batch/json";
#elif GD
	public const bool autoReg_n_login = true;
	public const string LocalSaveVersion = "18";
	public const bool UseServerConfig = true;
	public const bool LOCAL_READ = false;
	public const bool LOCAL_SAVE = true;
	public const string gameServerHost = "";
	public const string gameServerUrl = "http://n7vgd1ggaxmapp01.general.disney.private:8080/app/batch/json";
	
#elif SteamboatQA
	public const bool autoReg_n_login = false;
	public const string LocalSaveVersion = "18";
	public const bool UseServerConfig = true;
	public const bool LOCAL_READ = true;
	public const bool LOCAL_SAVE = true;
	public const string gameServerHost = "";
	//public const string gameServerUrl = "http://192.168.2.129:8080/game/batch/json";
	public const string gameServerUrl = "http://n7vgq1ggaxmapp01.general.disney.private:8080/app/batch/json";
#endif
}
