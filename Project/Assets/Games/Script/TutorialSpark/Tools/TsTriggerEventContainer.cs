using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class TsTriggerEventContainer : TsDefinition{
	
	private List<TsTriggerEventDef> events;
	
	public List<TsTriggerEventDef> Events{
		get{ return events; }
	}
	
	public void FillWithXmlNode(XmlNode node){
		events = GetListFromXmlNode_ListFormat<TsTriggerEventDef>(node);
	}
	
	public List<TsTriggerEventDef> GetEventsByName (string eventName){
		List<TsTriggerEventDef> results = new List<TsTriggerEventDef>();
		
		for (int i=0; i<events.Count; i++){
			if (eventName == events[i].Name){
				results.Add(events[i]);
			}
		}
		
		return results;
	}
	
	public TsTriggerEventDef GetEventById (string id){
		TsTriggerEventDef result = null;
		
		for (int i=0; i<events.Count; i++){
			if (events[i].Id == id){
				result = events[i];
				i = events.Count;
			}
		}
		
		return result;
	}
	
	public override string ToString(){
		string result = string.Format("[TsTriggerEventContainer: {0} Events]\n", events.Count);
		for (int i=0; i<events.Count; i++){
			result += events[i].ToString();
		}
		
		return result;
	}
}