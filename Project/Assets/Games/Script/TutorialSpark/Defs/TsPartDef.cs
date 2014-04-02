using UnityEngine;
using System.Collections;
using System.Xml;

public class TsPartDef :TsDefinition {
	
	private TsChapterDef[] chapters;
	
	public TsChapterDef[] Charpters{
		get{ return chapters; } 
	}
	
	
	public override void FillWithXmlNode (XmlNode node){
		if (null == node){ Debug.LogError("node is null"); return; }
		
		string[] chapterNames = TsParmsTranslator.Translate(node.Attributes["chapters"].Value);
		
		chapters = new TsChapterDef[chapterNames.Length];
		for (int i=0; i<chapterNames.Length; i++){
			chapters[i] = new TsChapterDef();
			chapters[i] = TsXmlReader.ReadChapter(chapterNames[i]);
		}
	}
	
	public override string ToString ()
	{
		string results = "[TsPartDef]\n";
		for (int i=0; i<chapters.Length; i++){
			results += chapters[i].ToString() + "\n =======\n";
		}
		
		return results;
	}
}
