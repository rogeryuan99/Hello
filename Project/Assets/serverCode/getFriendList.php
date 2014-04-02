<?php
include "mysql.connect.php";
include "sendNotification.php";
//session_start();
//$_SESSION['gameID']=1
//unset($_SESSION['views']);
//session_destroy();

//getFriendlist
if(isset($_GET['game_id']) && isset($_GET['type']))
{
	$gameID = $_GET['game_id'];
	$query = "SELECT * FROM friendlist WHERE id=$gameID";
	$result = $mysqli->query($query);
	$amount = $result->num_rows;
	
	if( $amount> 0)
	{
		while($row = $result->fetch_object())
		{
			if(strlen($row->list)<1)
			{
				echo "[]";
				return;
			}
			$resultAry = explode("|", $row->list);
			break;
		}
		$idList="";
		foreach( $resultAry as $value)
		{
			$idList .= "(userGameData.id=".$value." AND users.id=".$value.") or ";
		}
		$idList = substr($idList, 0, strlen($idList)-3);
		$query = "SELECT users.name, users.id, userGameData.heroes FROM users,userGameData WHERE ".$idList;
		echo $query."<br/>";
		//return;
		$result = $mysqli->query($query);
		$resultAry = array();
		while($row = $result->fetch_object())
		{
			array_push($resultAry, array("id"=>$row->id, 
										"name"=>$row->name, 
										"heroes"=>json_decode( urldecode($row->heroes) )
										)
										);
		}
		$json_string = json_encode($resultAry);
		echo $json_string;
		
	}else{
		echo "[]";
	}
	$mysqli->close();
}

//delete friend
if(isset($_GET['game_id']) && isset($_GET['delete_id']))
{
	$gameID = $_GET['game_id'];
	$deleteID = $_GET['delete_id'];
	$query = "SELECT list,block FROM friendlist WHERE id=$gameID";
	$result = $mysqli->query($query);
	$amount = $result->num_rows;
	if( $amount> 0)
	{
		$row = $result->fetch_object();
		$resultAry = explode("|", $row->list);
		$blockStr  = $row->block;
		
		$index = -1;//array_search  array_keys
		for( $i=0; $i<count($resultAry); $i++)
		{
			if($deleteID == $resultAry[$i])
			{
				$index = $i;
			}
		}
		//var_dump($resultAry);
		if($index != -1)
		{
			array_splice($resultAry, $index, 1);
			strlen($blockStr)>0? $blockStr.="|".$deleteID : $blockStr=$deleteID;
			
			$resultStr = implode("|",$resultAry);
			$query = "UPDATE friendlist SET list='$resultStr',block='$blockStr' WHERE id=$gameID";
			/*if( count($resultAry)>0 )
			{
				$resultStr = implode("|",$resultAry);
				$query = "UPDATE friendlist SET list='$resultStr',block='$blockStr' WHERE id=$gameID";
			}else{
				$query = "DELETE FROM friendlist WHERE id=$gameID";
			}*/
			
			$mysqli->query($query);
			echo "success";
		}else{
			echo "-1";
		}
		//var_dump($resultAry);
		
	}else{
		echo "0";
	}
	$mysqli->close();
}

//send social raids
//sender=323   frdList="321|215|313"
if(isset($_GET['sender']) && isset($_GET['frdList']) )
{
	$sender  = $_GET['sender'];
	$frdStr = $_GET['frdList'];
	$frdList  = explode("|", $frdStr);
	
	$insertData = "";
	$queryData  = "";
	foreach($frdList as $value)
	{
		$insertData .= "(".$sender.",".$value.",4),";
		$queryData  .= "id=".$value." or ";
	}
	$insertData = substr($insertData, 0, strlen($insertData)-1);
	//echo $insertData;
	$insertQ = "INSERT INTO messageBox(sender, recipient, type) VALUES".$insertData;
	$mysqli->query($insertQ);
	echo $insertQ;
	
	$queryData  .= "id=$sender";
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
	$mysqli->close();
}

//social raids   handler
if( isset($_GET['msg_id']) && isset($_GET['type']) )
{
	$msgID     = $_GET['msg_id'];
	$query = "DELETE FROM messageBox WHERE id=$msgID";
	$mysqli->query($query);
}
//time stamp $time()+3600*10
?>