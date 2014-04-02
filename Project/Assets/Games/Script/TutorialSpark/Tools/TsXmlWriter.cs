using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class TsXmlWriter {

	public static void WriteDynamicData(string ids){
		XmlDocument doc = new XmlDocument();
		doc.LoadXml((Resources.Load ("configData/TsDynamicData") as TextAsset).text);
		doc.ChildNodes[1].Attributes["ids"].Value = ids;
		
		doc.Save(string.Format("{0}/Resources/configData/TsDynamicData.xml", Application.dataPath));
	}
}
