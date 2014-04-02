<?php
include "mysql.connect.php";
include "../notificationDefine.php";
include "sendNotification.php";
//session_start();
//$_SESSION['gameID']=1
//unset($_SESSION['views']);
//session_destroy();

//get social Raid list
if(isset($_GET['gameInfo']) && isset($_GET['type']) )
{
	$gameInfo = $_GET['gameInfo'];
	//progress,level,id,pathA,pathB,pathC,pathD,sender,endTime 
	$query = "SELECT * FROM socialRaids WHERE 
		sender='$gameInfo' or recipientA='$gameInfo' or recipientB='$gameInfo' or recipientC='$gameInfo'";
	$result = $mysqli->query($query);
	$resultAry = array();
	while($row = $result->fetch_object())
	{
		
		if($row->pathA != "1")
		{
			$pathInfoAry  = explode("$", $row->pathA);
			if($pathInfoAry[3] != -100)
			{
				$pathA = array("name"=>$pathInfoAry[0], "completeTime"=>time()-$pathInfoAry[3]);
			}else{
				$pathA =  array("name"=>$pathInfoAry[0], "completeTime"=>"progress");
			}
			
		}else{
			$pathA = -1;
		}
		if($row->pathB != "1")
		{
			$pathInfoAry  = explode("$", $row->pathB);
			if($pathInfoAry[3] != -100)
			{
				$pathB = array("name"=>$pathInfoAry[0], "completeTime"=>time()-$pathInfoAry[3]);
			}else{
				$pathB =  array("name"=>$pathInfoAry[0], "completeTime"=>"progress");
			}
			array_push($nameList, $pathInfoAry[0]);
		}else{
			$pathB = -1;
		}
		if($row->pathC != "1")
		{
			$pathInfoAry  = explode("$", $row->pathC);
			if($pathInfoAry[3] != -100)
			{
				$pathC = array("name"=>$pathInfoAry[0], "completeTime"=>time()-$pathInfoAry[3]);
			}else{
				$pathC =  array("name"=>$pathInfoAry[0], "completeTime"=>"progress");
			}
			array_push($nameList, $pathInfoAry[0]);
		}else{
			$pathC = -1;
		}
		if($row->pathD != "1")
		{
			$pathInfoAry  = explode("$", $row->pathD);
			if($pathInfoAry[3] != -100)
			{
				$pathD = array("name"=>$pathInfoAry[0], "completeTime"=>time()-$pathInfoAry[3]);
			}else{
				$pathD =  array("name"=>$pathInfoAry[0], "completeTime"=>"progress");
			}
			array_push($nameList, $pathInfoAry[0]);
		}else{
			$pathD = -1;
		}
		
		
		$endTime = $row->endTime - time();
		
		if($endTime<0 && $row->progress != 4)
		{
			$endTime  = -1;
			
			/*if($row->sender == $gameInfo){
				$columnName = "sender";
			}else if($row->recipientA == $gameInfo){
				$columnName = "recipientA";
			}else if($row->recipientB == $gameInfo){
				$columnName = "recipientB";
			}else if($row->recipientC == $gameInfo){
				$columnName = "recipientC";
			}
			$query = "DELETE FROM socialRaids WHERE $columnName='$gameInfo' ";
			$mysqli->query($query);*/
			
			$social_id = $row->id;
			$updateQuery = "UPDATE raidMsg SET state=4 WHERE raid_id=$social_id";
			$mysqli->query($updateQuery);
			$social_id = -1;
			//continue;
		}else{
			$social_id = $row->id;
		}
		
		array_push($resultAry,array("id"=>$social_id,
									"leftTime"=>$endTime,
									"sender"=>$row->sender,
									"recipientA"=>$row->recipientA,
									"recipientB"=>$row->recipientB,
									"recipientC"=>$row->recipientC,
									"pathA"=>$pathA,
									"pathB"=>$pathB,
									"pathC"=>$pathC,
									"pathD"=>$pathD,
									"level"=>$row->level) 
									);
	}
	$json_string = json_encode($resultAry);
	echo $json_string;
	$mysqli->close();
}

/*
 desc socialRaids;
+------------+--------------+------+-----+---------+----------------+
| Field      | Type         | Null | Key | Default | Extra          |
+------------+--------------+------+-----+---------+----------------+
| id         | int(11)      | NO   | PRI | NULL    | auto_increment |
| sender     | varchar(111) | NO   |     | NULL    |                |
| recipientA | varchar(111) | YES  |     | NULL    |                |
| recipientB | varchar(111) | YES  |     | NULL    |                |
| recipientC | varchar(111) | YES  |     | NULL    |                |
| pathA      | varchar(120) | YES  |     | NULL    |                |
| pathB      | varchar(120) | YES  |     | NULL    |                |
| pathC      | varchar(120) | YES  |     | NULL    |                |
| pathD      | varchar(120) | YES  |     | NULL    |                |
| startTime  | int(11)      | NO   |     | NULL    |                |
| endTime    | int(11)      | NO   |     | NULL    |                |
+------------+--------------+------+-----+---------+----------------+

raidMsg table;
id	int(11)	否 	 	 
uid	int(11)	否 	 	sender 
state	int(11)	否  1 senderState  	 
raid_id int(11) 否
state = 1  new 
state = 2 complete
state = 3 unfinish
state = 4 fail
state = 5 complete and got bonus

*/
//send social raids
//sender=kid$321$fb_id   frdList="mike$321$fb_id | mike$321$fb_id | mike$212$fb_id"
if(isset($_GET['sender']) && isset($_GET['frdList']) && isset($_GET['pathType']))
{
	$sender  = $_GET['sender'];
	$frdStr = $_GET['frdList'];
	$frdList  = explode("|", $frdStr);
	$timeStamp = time();
	$endStamp  = 0;
	$level = $_GET['pathType'];
	switch( $level )
	{
		case "1":
			$endStamp = 3600*3;
			break;
		case "2":
			$endStamp = 3600*12;
			break;
		case "3":
			$endStamp = 3600*24;
			break;
		default:
			$endStamp = 3600*3;
			break;
		
	}
	$query = "SELECT id FROM socialRaids WHERE sender='$sender'";
	$result = $mysqli->query($query);
	$count = 0;
	while($row = $result->fetch_object())
	{
		$endTime = $row->endTime - time();
		if($endTime<0 && $row->progress != 4)
		{
			//fail raid
		}else{
			$count += 1;
		}
	}
	if($count > 0)
	{
		$mysqli->close();
		echo "-1";
		return;
	}
	
	$insertData = "";
	$queryData  = "";
	/*foreach($frdList as $value)
	{
		$insertData .= "(".$sender.",".$value.",4),";
		$queryData  .= "id=".$value." or ";
	}*/
	$insertData = substr($insertData, 0, strlen($insertData)-1);
	//echo $insertData;
	//$insertQ = "INSERT INTO messageBox(sender, recipient, type) VALUES".$insertData;
	
	$insertQ = "INSERT INTO socialRaids(sender, recipientA, recipientB,recipientC,
										pathA, pathB, pathC, pathD, startTime, endTime,level) 
										VALUES('$sender', '$frdList[0]','$frdList[1]','$frdList[2]',
												'1','1','1','1', $timeStamp, $timeStamp+$endStamp, $level)";
	
	$mysqli->query($insertQ);
	$socialR_id = $mysqli->insert_id;
	
	$senderID = explode("$", $sender);//mike$321$fb_id
	$frd_idA  = explode("$", $frdList[0]);//mike$321$fb_id
	$frd_idB  = explode("$", $frdList[1]);//mike$321$fb_id
	$frd_idC  = explode("$", $frdList[2]);//mike$321$fb_id
	
	$insertQ = "INSERT INTO raidMsg(uid,raid_id) VALUES($senderID[1], $socialR_id),($frd_idA[1], $socialR_id),($frd_idB[1], $socialR_id),($frd_idC[1], $socialR_id)";
	$mysqli->query($insertQ);
	
	$query = "SELECT deviceToken FROM users WHERE id=$frd_idA[1] OR id=$frd_idB[1] OR id=$frd_idC[1]";
	$result = $mysqli->query($query);
	
	while($row = $result->fetch_object())
	{
		$token = $row->deviceToken;
		if( strlen($token)>0 )
		{
			$senderName = explode("$",$sender);
			$ntfMsg = str_replace("@",$senderName[0], RAID_INVITE);
			sendNotification($row->deviceToken, $ntfMsg);
		}
	}
	
	// get id
	/*$query = "SELECT id FROM socialRaids WHERE sender='$sender'";
	$result = $mysqli->query($query);
	while($row = $result->fetch_object())
	{
		echo $row->id;
	}*/
	echo $socialR_id;
	//echo $insertQ;
	$mysqli->close();
	return;
	
	/*$queryData  .= "id=$sender";
	$query = "SELECT * FROM users WHERE ".$queryData;
	$result = $mysqli->query($query);
	$socialRaidsAry = array();
	$senderName = '';
	while($row = $result->fetch_object())
	{
		if($row->id != $sender)
		{
			array_push($socialRaidsAry, array('id'=>$row->id, 'name'=>$row->name, 'token'=>$row->deviceToken));
		}else{
			$senderName = $row->name;
		}
		
	}
	foreach($socialRaidsAry as $value)
	{
		if(strlen($value['token'])>0)
		{
			sendNotification($value['token'], 'Bro! '.$senderName.' need you help!');
		}
	}
	$mysqli->close();*/
}
//start social raids
if( isset($_GET['gameInfo']) && isset($_GET['social_id']) && isset($_GET['path']) && isset($_GET['start']))
{
	$socialID = $_GET['social_id'];
	$path   = "path".$_GET['path'];
	
	$query = "SELECT * FROM socialRaids WHERE id=$socialID AND $path='1'";
	$result = $mysqli->query($query);
	if($result->num_rows < 1)
	{
		$query = "SELECT $path FROM socialRaids WHERE id=$socialID";
		$result = $mysqli->query($query);
		while( $row = $result->fetch_object() )
		{
			echo $row->$path;
		}
		//echo "social raid was exist.";
		return -1;
	}
	
	$pathInfo = ($_GET['gameInfo'])."$"."-100";
	$insert = "UPDATE socialRaids SET $path='$pathInfo' WHERE id=$socialID";
	$mysqli->query($insert);
	$userInfo = $_GET['gameInfo'];
	$userInfoAry = explode("$", $userInfo);
	//mike$321$fb_id
	$query1 = "UPDATE raidMsg SET state=3 WHERE uid=$userInfoAry[1] AND raid_id=$socialID";
	$mysqli->query($query1);
	$mysqli->close();
}
//complete social raids 
if( isset($_GET['gameInfo']) && isset($_GET['social_id']) && isset($_GET['path']) && isset($_GET['success']) )
{
	$gameInfo = $_GET['gameInfo'];
	$socialID = $_GET['social_id'];
	$success     = $_GET['success'];
	$path   = "path".$_GET['path'];
	
	if($success == 0)
	{
		$insert = "UPDATE socialRaids SET $path='1' WHERE id=$socialID";
	}else{
		
		$completeTime = time();
		$pathInfo = ($_GET['gameInfo'])."$".$completeTime;
		$insert = "UPDATE socialRaids SET $path='$pathInfo', progress=progress+1 WHERE id=$socialID";
		
		$query = "SELECT * from socialRaids WHERE id=$socialID";
		$result = $mysqli->query($query);
		$idAry = array();
		while($row = $result->fetch_object())
		{
			
			if($gameInfo != $row->sender)
			{
				$user_id = explode("$",$row->sender);
				//var_dump($user_id);
				array_push($idAry, $user_id[1]);	
			}
			if($gameInfo != $row->recipientA)
			{
				$user_id = explode("$",$row->recipientA);
				array_push($idAry, $user_id[1]);
			}
			if($gameInfo != $row->recipientB)
			{
				$user_id = explode("$",$row->recipientB);
				array_push($idAry, $user_id[1]);
			}
			if($gameInfo != $row->recipientC)
			{
				$user_id = explode("$",$row->recipientC);
				array_push($idAry, $user_id[1]);
			}
		}
		//get deviceToken
		$query = "SELECT name,deviceToken FROM users WHERE id=$idAry[0] OR id=$idAry[1] OR id=$idAry[2]";
		$result = $mysqli->query($query);
		$completerAry = explode("$",$gameInfo);
		while($row = $result->fetch_object())
		{
			if(strlen($row->deviceToken)>0)
			{
				echo $row->deviceToken.$completerAry[0]." has completed his path! \br";
				$ntfMsg = str_replace("@", $completerAry[0], RAID_COMPLETE);
				sendNotification($row->deviceToken, $ntfMsg);
			}
		}
		
		$updateQuery = "UPDATE raidMsg SET state=2 WHERE raid_id=$socialID AND uid=$completerAry[1]";
		$mysqli->query($updateQuery);
		//
	}
	/*$query = "SELECT * FROM socialRaids WHERE sender='$raidCreater'";
	$result = $mysqli->query($query);
	if($_GET['path'] != "A" || $_GET['path'] != "B" || $_GET['path'] != "C" || $_GET['path'] != "D")
	{
		echo "error:  this path was not exist";
		return;
	}*/
	
	$mysqli->query($insert);
	echo $insert;
	$mysqli->close();
}

//get award
if( isset($_GET['gameInfo']) && isset($_GET['social_id']) &&  isset($_GET['complete']))
{
	$socialID = $_GET['social_id'];
	$gameInfo = $_GET['gameInfo'];
	$query = "SELECT * FROM socialRaids WHERE id=$socialID ";
	$result = $mysqli->query($query);
	while($row = $result->fetch_object())
	{
		if($row->progress < 4)
		{
			return -1;
		}else{
			$gameInfoAry = explode("$", $gameInfo);//userName$userID$fb_id
			$completeStr = "complete$".$gameInfoAry[1]."$".$gameInfoAry[2];
			
			if($gameInfo == $row->sender)
			{
				$update = "UPDATE socialRaids SET sender='$completeStr' WHERE id=$socialID";
				$targetUser = $gameInfoAry[1];
			}else if($gameInfo == $row->recipientA){
				$update = "UPDATE socialRaids SET recipientA='$completeStr' WHERE id=$socialID";
				$targetUser = $gameInfoAry[1];
			}else if($gameInfo == $row->recipientB){
				$update = "UPDATE socialRaids SET recipientB='$completeStr' WHERE id=$socialID";
				$targetUser = $gameInfoAry[1];
			}else if($gameInfo == $row->recipientC){
				$update = "UPDATE socialRaids SET recipientC='$completeStr' WHERE id=$socialID";
				$targetUser = $gameInfoAry[1];
			}else{
				$targetUser = "";
				echo "fail";
				return 0;
			}
			$mysqli->query($update);
		}
	}
	if(strlen($targetUser)>0)
	{
		$updateQuery = "UPDATE raidMsg SET state=5 WHERE raid_id=$socialID AND uid=$gameInfoAry[1]";
		$mysqli->query($updateQuery);
	}
	$mysqli->close();
	echo "success";
	return 1;
}

if( isset($_GET['uid']) )
{
	$uid = $_GET['uid'];
	$query = "SELECT * FROM raidMsg WHERE uid=$uid";
	$result = $mysqli->query($query);
	$stateAry = array();
	$raid_idAry   = array();
	$newAry = array();
	while($row = $result->fetch_object())
	{
		//array_push($stateAry, $row->state);
		$stateAry["$row->raid_id"] = $row->state;
		if($row->state == 2)
		{
			array_push($raid_idAry,  "raid_id=".$row->raid_id);
		}
	}
	
	if( count($raid_idAry)>0)
	{
		$raid_id_list = implode(" OR ", $raid_idAry);
		$query = "SELECT * FROM raidMsg WHERE ".$raid_id_list;
		$result = $mysqli->query($query);
		$raidList = array();
		while($row = $result->fetch_object())
		{
			if( array_key_exists($row->raid_id, $raidList) )
			{
				array_push($raidList["$row->raid_id"], array("state"=>$row->state) );
			}else{
				$raidList["$row->raid_id"] = array( array("state"=>$row->state) );
			}
		}
		
		foreach($raidList as $key=>$value)
		{
			foreach($value as $key2=>$value2)
			{
				if( $value2["state"] != 2)
				{
					if( $stateAry["$key"] == 2)
					{
						$stateAry["$key"] = 5;
					}
					break;
				}
			}
		}
	}
	if( count($stateAry) == 0 )
	{
		echo "-1";
	}else{
		/*state = 1  new 
		state = 2 complete
		state = 3 unfinish
		state = 4 fail
		state = 5 complete and got bonus*/
		asort($stateAry);
		/*if($stateAry[0] == 4)
		{
			array_push($resultAry,array("id"=>$row->id);
			echo $json_string;
		}else{
			echo $stateAry[0];
		}*/
		foreach($stateAry as $key=>$value)
		{
			$curRaidID = $key;
			$curState  = $value;
			echo $curState;
			break;
		}
		$query = "DELETE FROM raidMsg WHERE state=$curState AND uid=$uid AND raid_id=$curRaidID";
		$mysqli->query($query);
	}
	
	
	
	/*if( count($newAry) > 0)
	{
		$newsList = implode(" OR ",$newAry);
		$updateQuery = "UPDATE raidMsg SET state=3 WHERE ".$newsList." AND uid=$uid";
		$mysqli->query($updateQuery);
	}*/
	$mysqli->close();
}

//social raids   handler
/*if( isset($_GET['msg_id']) && isset($_GET['type']) )
{
	$msgID     = $_GET['msg_id'];
	$query = "DELETE FROM messageBox WHERE id=$msgID";
	$mysqli->query($query);
}*/
//time stamp $time()+3600*10
?>