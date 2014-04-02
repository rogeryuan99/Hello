<?php
include "mysql.connect.php";
/*$conn = mysql_pconnect("127.0.0.1", "root", "4219");
mysql_select_db("gameTest");
mysql_query("set name utf8;");
*/
//sendInvent
if( isset($_REQUEST['data'])  )
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

"learnSkill":{"MARINE1":-1,"MARINE2":-1 }
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
	timeStamp   int  latin1_swedish_ci		是	NULL		
	
*/
	$data =  $_REQUEST['data'];
	$json = json_decode( urldecode( $data ) );
	$id      = $json->id;
	//$id  = 2;
	//echo 'id'.$id.'  ';
	$timeStamp = time();
	$sql = "SELECT timeStamp FROM userGameData WHERE id=$id";
	$result = $mysqli->query($sql);
	while($row = $result->fetch_object())
	{
		$serverDataStamp = $row->timeStamp;
		if($serverDataStamp != NULL)
		{
			if( $timeStamp - $serverDataStamp < 60 )
			{
				$mysqli->close();
				//echo "server had a new timeStamp.";
				echo "-1";
				return;
			}
		}
	}
	
	$boxKey  = $json->boxKey;
	//echo 'boxKey'.$boxKey.'  ';
	$box     = $json->box;
	//echo 'box'.$box.'  ';
	$fuel    = $json->fuel;
	//echo 'fuel'.$fuel.'  ';
	$potion1c = $json->clearCD;
	//echo 'potion1c'.$potion1c.'  ';
	$potion2r = $json->relive;
	//echo 'potion2r'.$potion2r.'  ';
	$coins   = $json->coins;
	//echo 'coins'.$coins.'  ';
	$credits = $json->credits;
	//echo 'credits'.$credits.'  ';
	$equiplist = $json->equipmentIDList;
	//echo 'equiplist'.$equiplist.'  ';
	$heroes  = urlencode( json_encode($json->heroes) );
	
	
	$map     = urlencode( json_encode($json->map) );
	//echo 'map'.$map.'\n';
	
	if( isset($_REQUEST['version']) )
	{
		$learnSkill = urlencode( json_encode($json->learnSkill) );
	}else{
		$learnSkill = "{}";
	}
	
	$sql = "INSERT INTO userGameData(id, coins, credits, fuel, box, boxKey, potion1cd, potion2live, equipList, heroes, map, learnSkill,timeStamp) 
				VALUES ($id, $coins, $credits, $fuel, $box, $boxKey, $potion1c, $potion2r, '$equiplist', '$heroes', '$map', '$learnSkill', $timeStamp)
				ON DUPLICATE KEY UPDATE coins=$coins, credits=$credits, fuel=$fuel, box=$box, boxKey=$boxKey, potion1cd=$potion1c,
										potion2live=$potion2r, equipList='$equiplist', heroes='$heroes', map='$map', learnSkill='$learnSkill', timeStamp=$timeStamp";
	//echo $sql;
	//echo urldecode($heroes);
	echo $timeStamp;
	$mysqli->query($sql);
	$mysqli->close();
}

?>