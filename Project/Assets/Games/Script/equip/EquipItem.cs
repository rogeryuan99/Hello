using UnityEngine;
using System.Collections;

public class EquipItem : MonoBehaviour {
EquipData equipData;
Vector3 originalVc3;

//static Hashtable equipList = new Hashtable();

void Awake (){
	//equipList[id] = this.gameObject;
}

void Start (){
	
	buildItem(equipData);
}

void buildItem ( EquipData equipD  ){
	//display icon 
}
void Update (){
}
}