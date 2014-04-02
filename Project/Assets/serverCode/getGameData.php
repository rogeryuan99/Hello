<?php
include "mysql.connect.php";
/*$conn = mysql_pconnect("127.0.0.1", "root", "4219");
mysql_select_db("gameTest");
mysql_query("set name utf8;");
*/
//sendInvent
if( isset($_REQUEST['id'])  )
{
/*
	{
"boxKey":10,
"box":50,
"id":"test001",

"fuel":1000,
"clearCD":4,
"relive":3
"credits":3860,
"coins":1000,
"equipmentIDList":"9999|3010|3004|1036|9999|1021|3001|2011|1066|9999",

"heroes":{
"TRAINER":{"level":1,"exp":0,"isSelect":false,"skList":["Tiger's Claw","Beastly Aspect"],"equips":"Weapons_1015|Armors_2016","passiveList":[],"isHired":false},
"MARINE":{"level":1,"exp":0,"isSelect":true,"skList":["Space Cadet's March","Shotty"],"equips":"Weapons_1015|Armors_2024","passiveList":[],"isHired":true},
"TANK":{"level":1,"exp":0,"isSelect":false,"skList":["Energized Alloy","Heavy Metal Attraction"],"equips":"Weapons_1092|Armors_2027","passiveList":["Life Support"],"isHired":false},
"PRIEST":{"level":1,"exp":0,"isSelect":false,"skList":["Happy Thoughts","Mind Shield"],"equips":"Weapons_1045|Armors_2025","passiveList":[],"isHired":false},
"COWBOY":{"level":1,"exp":0,"isSelect":false,"skList":["Quickdraw","Frontier Spirit"],"equips":"Weapons_1000|Armors_2008","passiveList":[],"isHired":true},
"WIZARD":{"level":1,"exp":0,"isSelect":false,"skList":["Dirac Sea","Nanite Cannon"],"equips":"Weapons_1060|Armors_2028","passiveList":[],"isHired":true},
"HEALER":{"level":1,"exp":0,"isSelect":true,"skList":["Shock Therapy","Overclock"],"equips":"Weapons_1030|Armors_2026","passiveList":[],"isHired":true},
"DRUID":{"level":1,"exp":0,"isSelect":false,"skList":["Monsters from the Id","Super-Ego Restorative"],"equips":"Weapons_1075|Armors_2000","passiveList":[],"isHired":false},
}

}
userGameData
字段	类型	整理	属性	Null	默认	额外	操作
	id	int(11)			否			 	 	 	 	 	 	
	coins	int(11)			否			 	 	 	 	 	 	
	credits	int(11)			否			 	 	 	 	 	 	
	fuel	int(11)			否			 	 	 	 	 	 	
	box	int(11)			否			 	 	 	 	 	 	
	boxKey	int(11)			否			 	 	 	 	 	 	
	potion1cd	int(11)			否			 	 	 	 	 	 	
	potion2live	int(11)			否			 	 	 	 	 	 	
	equipList	text	latin1_swedish_ci		是	NULL		 	 	 				 
	heroes	text	latin1_swedish_ci		否			 	 	 				 
	map	text	latin1_swedish_ci		是	NULL		 	 	 				 
	learnSkill   text  latin1_swedish_ci		是	NULL		
*/
	$id =  $_REQUEST['id'];
	$isFrd = 0;
	if( isset($_REQUEST['isFrd']) )
	{
		$isFrd = 1;
	}
	
	$sql = "SELECT * FROM userGameData WHERE id=$id";
	$result = $mysqli->query($sql);
	
	while($row = $result->fetch_object())
	{
		if($isFrd == 1)
		{
			$heroes = json_decode(urldecode($row->heroes));
			$gameData = array();
			foreach( $heroes as $key=>$value)
			{
				//var_dump($value);
				$lv = $value->level;
				$isSelect = $value->isSelect;//['isSelect'];
				if($isSelect == true)
				{
					array_push($gameData, array('type'=>$key, 'lv'=>$lv) );
				}
			}
		}else{
			if( isset($_REQUEST['version']) )
			{
				$gameData = array('id'=>intval($row->id),
						  'boxKey'=>intval($row->boxKey),
						  'box'=>intval($row->box),
						  'clearCD'=>intval($row->potion1cd),
						  'relive'=>intval($row->potion2live),
						  'coins'=>intval($row->coins),
						  'credits'=>intval($row->credits),
						  'fuel'=>intval($row->fuel),
						  'timeStamp'=>intval($row->timeStamp),
						  'equipmentIDList'=>$row->equipList,
						  'map'=>json_decode(urldecode($row->map)),
						  'heroes'=> json_decode(urldecode($row->heroes)),
						  'learnSkill'=>json_decode(urldecode($row->learnSkill))
						  );
			}else{
				$gameData = array('id'=>intval($row->id),
						  'boxKey'=>intval($row->boxKey),
						  'box'=>intval($row->box),
						  'clearCD'=>intval($row->potion1cd),
						  'relive'=>intval($row->potion2live),
						  'coins'=>intval($row->coins),
						  'credits'=>intval($row->credits),
						  'fuel'=>intval($row->fuel),
						  'equipmentIDList'=>$row->equipList,
						  'map'=>json_decode(urldecode($row->map)),
						  'heroes'=> json_decode(urldecode($row->heroes))
						  );
			}
			
		}  
						  
		echo json_encode($gameData);
	}
	$mysqli->close();
}

?>