using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class TsDefinition {
	
	public TsDefinition(){}
	public virtual void FillWithXmlNode(XmlNode node){}
	
	protected T[] GetListFromXmlNode<T>(XmlNode node) where T:TsDefinition, new(){
		T[] array = null;
		List<T> defs = GetListFromXmlNode_ListFormat<T>(node);
		
		if (null != defs || 0 == defs.Count){
			array = defs.ToArray();
		}
		
		return array;
	}
	
	protected List<T> GetListFromXmlNode_ListFormat<T>(XmlNode node) where T:TsDefinition, new(){
		if (null == node) return null;
		
		XmlNodeList list = node.ChildNodes;
		List<T> defs = new List<T>();
		for (int i=0; i<list.Count; i++){
			if (null != list[i].Attributes){
				T tmp = new T();
				tmp.FillWithXmlNode(list[i]);
				defs.Add(tmp);
			}
		}
		
		return defs;
	}
	
	protected void LogNode(XmlNode node){
		string result = string.Format("<{0} ", node.Name);
		
		for (int i=0; i<node.Attributes.Count; i++){
			result += string.Format("{0}=\"{1}\" ", node.Attributes[i].Name, node.Attributes[i].Value);
		}
		result += "/>";
		Debug.LogError(result);
	}
}
