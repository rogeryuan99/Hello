<?php
include "mysql.connect.php";
include "sendNotification.php";

//define('SEND_TYPE',1);
$sender;
$recipient;

//sendInvent
if( isset($_GET['sender']) && isset($_GET['recipient']) && isset($_GET['type']) )
{
	$sender    = $_GET['sender'];
	$recipientName = $_GET['recipient'];
	/*if( $sender == $recipient)
	{
		return 0;
	}*/
	$queryUser = "SELECT * FROM users WHERE name='$recipientName' or id=$sender";
	
	$result = $mysqli->query($queryUser);
	$amount = $result->num_rows;
	if( $amount> 0)
	{
		$resultAry = array();
		$senderName = "";
		$recipientID ;
		$deviceTk = "";
		while($row = $result->fetch_object())
		{
			if($row->id == $sender)
			{
				$senderName = $row->name;
				if($senderName == $recipientName)
				{
					echo $senderName ."----". $recipientName;
					return 0;
				}
			}else if($row->name == $recipientName){
				$recipientID = $row->id;
				$deviceTk      =$row->deviceToken;
				array_push($resultAry, array('recipient'=>$row->id, 'name'=>$row->name));
				$json_string = json_encode($resultAry);
				
			}			
		}
		echo $json_string;
		$insert = "INSERT INTO messageBox(sender, recipient, type,sender_name,recipient_name)
								   VALUES($sender, $recipientID, 1, '$senderName', '$recipientName')";
		$mysqli->query($insert);
		
		if(strlen($deviceTk)>0)
		{
			sendNotification($deviceTk, $senderName.' say Hi~~');
		}
	}else{
		echo "0";
	}
	$mysqli->close();
}

//getInventList
if( isset($_GET['game_id'])  )
{
	$gameID = $_GET['game_id'];
	//SELECT messageBox.*,name FROM messageBox,users WHERE (messageBox.sender=10 or messageBox.recipient=10)  and users.id=10;
	$query = "SELECT * FROM messageBox WHERE sender=$gameID or recipient=$gameID";
	$result = $mysqli->query($query);
	$resultAry = array();
	//array_push($resultAry, "1");
	while($row = $result->fetch_object())
	{
		
		array_push($resultAry,array("msg_id"=>$row->id,
									"sender"=>$row->sender,
									"recipient"=>$row->recipient,
									"senderName"=>$row->sender_name,
									"recipientName"=>$row->recipient_name,
									"type"=>$row->type) );
	}
	$json_string = json_encode($resultAry);
	echo $json_string;
	$mysqli->close();
}

//处理邀请:  参数:msg_id:int, sender:int, recipient:int, accept= 1 or 0 (1同意，0拒绝)
//inventHandler
if( isset($_GET['msg_id']) && isset($_GET['accept']) && isset($_GET['sender']) && isset($_GET['recipient']) )
{
	$msgID     = $_GET['msg_id'];
	$accept    = $_GET['accept'];
	$sender    = $_GET['sender'];
	$recipient = $_GET['recipient'];
	$query = "DELETE FROM messageBox WHERE id=$msgID";
	$mysqli->query($query);
	if( $accept == 1)
	{
		$getFrdList  = "SELECT * FROM friendlist WHERE id=$recipient or id=$sender LIMIT 2";
		$result = $mysqli->query($getFrdList);
		$resultAry = array();
		$frdListStrAry = array();
		$amount = $result->num_rows;
		if($amount > 0)
		{
			while($row = $result->fetch_object())
			{
				$idStr = $row->list;
				if($row->id == $sender)
				{
					strlen($idStr)>0? $idStr.= "|".$recipient : $idStr=$recipient;
					//$idStr .=  "|".$recipient;
				}else{
					strlen($idStr)>0? $idStr.= "|".$sender : $idStr=$sender;
					//$idStr .=  "|".$sender;
				}
				$frdListStrAry[$row->id] = $idStr;
				//$resultAry[$row->id] = explode("|", $row->list);
				//array_push($resultAry[$row->id], $sender);
				//$frdListStrAry[$row->id] = implode("|", $resultAry[$row->id]);
				//$resultAry[$index] = explode("|", $row->list);
				//array_push($resultAry, $sender);
				
				//break;
			}
			if($amount == 2)
			{
				$addFrdQuery = "UPDATE friendlist SET list= CASE id 
													  WHEN $recipient THEN '$frdListStrAry[$recipient]'
													  WHEN $sender    THEN '$frdListStrAry[$sender]'
													  END
													  WHERE id IN($sender,$recipient)";
				$mysqli->query($addFrdQuery);
			}else{
				if( $frdListStrAry[$recipient] )
				{
					$addFrdQuery = "UPDATE friendlist SET list='$frdListStrAry[$recipient]' WHERE id=$recipient;";
					$addFrdQuery .= "INSERT INTO friendlist(id, list) VALUES($sender,'$recipient')";
				}else{
					$addFrdQuery = "UPDATE friendlist SET list='$frdListStrAry[$sender]' WHERE id=$sender;";
					$addFrdQuery .= "INSERT INTO friendlist(id, list) VALUES($recipient,'$sender')";
				}
				$mysqli->multi_query($addFrdQuery);
			}
			//$frdListStrAry = implode("|", $resultAry);
			//$addFrdQuery = "UPDATE friendlist SET list='$frdListStr' WHERE id=$recipient";
			echo $addFrdQuery."<br/>";
			
		}else{
			$addFrdQuery = "INSERT INTO friendlist(id, list) VALUES($recipient, '$sender'),($sender,'$recipient')";
			$mysqli->query($addFrdQuery);
		}
		echo "$recipient add Friend $sender <br/>";
	}else{
		echo "1";
	}
	$mysqli->close();
}

?>