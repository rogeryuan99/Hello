using UnityEngine;
using System;
using System.Collections;
using System.Xml;
// using System.Xml;

public class TsXmlReader {
	
	public static TsPartDef ReadPart(string partName){
		TsPartDef def = new TsPartDef();
		
		XmlDocument doc = new XmlDocument();
		doc.LoadXml((Resources.Load ("configData/TsCatalogue") as TextAsset).text);
		
		def.FillWithXmlNode(doc.GetElementsByTagName("Part_" + partName)[0]);
		
		return def;
	}
	
	public static TsChapterDef ReadChapter(string chapterName){
		TsChapterDef def = new TsChapterDef();
		
		XmlDocument doc = new XmlDocument();
		doc.LoadXml((Resources.Load ("configData/TsChapter_" + chapterName) as TextAsset).text);
		
		def.FillWithXmlNode(doc.ChildNodes[1]);
		
		return def;
	}
	
	public static TsTriggerEventContainer ReadTriggerEvents(){
		TsTriggerEventContainer container = new TsTriggerEventContainer();
		
		XmlDocument doc = new XmlDocument();
		doc.LoadXml((Resources.Load ("configData/TsTriggerEvents") as TextAsset).text);
		
		container.FillWithXmlNode(doc.ChildNodes[1]);
		
		return container;
	}
}
