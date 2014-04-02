using UnityEngine;
using System.Collections;
using System.Xml;

public class Vector6
{
	public float PHY = 0;
	public float IMP = 0;
	public float PSY = 0;
	public float EXP = 0;
	public float ENG = 0;
	public float MAG = 0;
	public Vector6(){
	}
	public Vector6(float a,float b,float c,float d,float e,float f){
		this.PHY = a;
		this.IMP = b;
		this.PSY = c;
		this.EXP = d;
		this.ENG = e;
		this.MAG = f;
	}
	public static Vector6 createWithStaticDefXml(XmlNode dataNode, string type)
	{
		Vector6 v6 = new Vector6();
		v6.PHY = float.Parse(dataNode.Attributes [type + "_PHY"].Value);
		v6.IMP = float.Parse(dataNode.Attributes [type + "_IMP"].Value);
		v6.PSY = float.Parse(dataNode.Attributes [type + "_PSY"].Value);
		v6.EXP = float.Parse(dataNode.Attributes [type + "_EXP"].Value);
		v6.ENG = float.Parse(dataNode.Attributes [type + "_ENG"].Value);
		v6.MAG = float.Parse(dataNode.Attributes [type + "_MAG"].Value);
		
		return v6;
	}
	
	public static Vector6 createWithHashtable(Hashtable jsonHash, string type)
	{
		Vector6 v6 = new Vector6();
		if(jsonHash.Contains(type+"_PHY")) v6.PHY = float.Parse(jsonHash[type + "_PHY"].ToString());
		if(jsonHash.Contains(type+"_IMP")) v6.IMP = float.Parse(jsonHash[type + "_IMP"].ToString());
		if(jsonHash.Contains(type+"_PSY")) v6.PSY = float.Parse(jsonHash[type + "_PSY"].ToString());
		if(jsonHash.Contains(type+"_EXP")) v6.EXP = float.Parse(jsonHash[type + "_EXP"].ToString());
		if(jsonHash.Contains(type+"_ENG")) v6.ENG = float.Parse(jsonHash[type + "_ENG"].ToString());
		if(jsonHash.Contains(type+"_MAG")) v6.MAG = float.Parse(jsonHash[type + "_MAG"].ToString());
		
		return v6;
	}
	
	public Vector6 Add(float num)
	{
		
		this.PHY += num;
		this.IMP += num;
		this.PSY += num;
		this.EXP += num;
		this.ENG += num;
		this.MAG += num;
		
		return this;
	}
	
	public Vector6 Sub(float num)
	{
		
		this.PHY -= num;
		this.IMP -= num;
		this.PSY -= num;
		this.EXP -= num;
		this.ENG -= num;
		this.MAG -= num;
		return this;
	}
	
	public Vector6 Div(float num)
	{
		
		this.PHY /= num;
		this.IMP /= num;
		this.PSY /= num;
		this.EXP /= num;
		this.ENG /= num;
		this.MAG /= num;
		return this;
	}
	
	public Vector6 Multip(float num)
	{
		
		this.PHY *= num;
		this.IMP *= num;
		this.PSY *= num;
		this.EXP *= num;
		this.ENG *= num;
		this.MAG *= num;
		return this;
	}
	
	public Vector6 Add(Vector6 v6)
	{
		
		this.PHY += v6.PHY;
		this.IMP += v6.IMP;
		this.PSY += v6.PSY;
		this.EXP += v6.EXP;
		this.ENG += v6.ENG;
		this.MAG += v6.MAG;
		return this;
	}
	
	public Vector6 Sub(Vector6 v6)
	{
		
		this.PHY -= v6.PHY;
		this.IMP -= v6.IMP;
		this.PSY -= v6.PSY;
		this.EXP -= v6.EXP;
		this.ENG -= v6.ENG;
		this.MAG -= v6.MAG;
		return this;
	}
	
	public Vector6 Div(Vector6 v6)
	{
		this.PHY /= v6.PHY;
		this.IMP /= v6.IMP;
		this.PSY /= v6.PSY;
		this.EXP /= v6.EXP;
		this.ENG /= v6.ENG;
		this.MAG /= v6.MAG;
		return this;
	}
	
	public Vector6 Multip(Vector6 v6)
	{
		this.PHY *= v6.PHY;
		this.IMP *= v6.IMP;
		this.PSY *= v6.PSY;
		this.EXP *= v6.EXP;
		this.ENG *= v6.ENG;
		this.MAG *= v6.MAG;
		return this;
	}
	
	public Vector6 Zero()
	{
		this.PHY = 0;
		this.IMP = 0;
		this.PSY = 0;
		this.EXP = 0;
		this.ENG = 0;
		this.MAG = 0;
		return this;
	}
	
	public float total()
	{
		return this.PHY + this.IMP + this.PSY + this.EXP + this.ENG + this.MAG;
	}
	
	public Vector6 clone()
	{
		Vector6 v6 = new Vector6();
		v6.PHY = this.PHY;
		v6.IMP = this.IMP;
		v6.PSY = this.PSY;
		v6.EXP = this.EXP;
		v6.ENG = this.ENG;
		v6.MAG = this.MAG;
		
		return v6;
	}
	public Vector6 toInt(){
		this.PHY = (int)this.PHY;
		this.IMP = (int)this.IMP;
		this.PSY = (int)this.PSY;
		this.EXP = (int)this.EXP;
		this.ENG = (int)this.ENG;
		this.MAG = (int)this.MAG;
		return this;
	}
	public override string ToString ()
	{
		return string.Format("[PHY{0} IMP{1} PSY{2} EXP{3} ENG{4} MAG{5}]",this.PHY,this.IMP,this.PSY,this.EXP,this.ENG,this.MAG);
	}

	public string ToStringShorten ()
	{
		//return string.Format("[PHY{0} IMP{1} PSY{2} EXP{3} ENG{4} MAG{5}]",this.PHY,this.IMP,this.PSY,this.EXP,this.ENG,this.MAG);
		string s = "";
		if(this.PHY>2) s += " PHY"+ this.PHY;
		if(this.IMP>2) s += " IMP"+ this.IMP;
		if(this.PSY>2) s += " PSY"+ this.PSY;
		if(this.EXP>2) s += " EXP"+ this.EXP;
		if(this.ENG>2) s += " ENG"+ this.ENG;
		if(this.MAG>2) s += " MAG"+ this.MAG;
		if( s == ""){
			s = "0"	;
		}
		return s;
	}
}
