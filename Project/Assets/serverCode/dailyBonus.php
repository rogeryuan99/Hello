<?php
include "mysql.connect.php";

//session_start();
//$_SESSION['gameID']=1
//unset($_SESSION['views']);
//session_destroy();

//get social Raid list
if(isset($_GET['id']))
{
	$userID = $_GET['id'];
	
	$query = "SELECT id FROM users WHERE id=$userID";
	$result = $mysqli->query($query);
	if($result->num_rows < 1)
	{
		echo("-1");
	}else{
		$query = "SELECT * FROM dailyBonus WHERE id=$userID";
		$nowTime = time();
		$result = $mysqli->query($query);
		if($result->num_rows < 1)
		{
			$query = "INSERT INTO dailyBonus(id, timestamp,count) VALUES($userID, $nowTime, 1)";
			$result = $mysqli->query($query);
			echo("1");
		}else{
			$row = $result->fetch_object();
			$preTimestamp = $row->timestamp;
			$count        = $row->count;
			if($nowTime - $preTimestamp > 86400)
			{
				$resultCount = $count + 1;//86400
				$query = "UPDATE dailyBonus SET timestamp=$nowTime, count=$resultCount WHERE id=$userID";
				$result = $mysqli->query($query);
				echo($resultCount);
			}else{
				echo("-1");
			}
		}	
	}
	$mysqli->close();
}
?>