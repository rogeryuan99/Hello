<?php
include "mysql.connect.php";

//session_start();
//$_SESSION['gameID']=1
//unset($_SESSION['views']);
//session_destroy();

//get beat count
if( isset($_GET['id']) && isset($_GET['q']) )
{
	$userID = $_GET['id'];
	
	$query = "SELECT id FROM users WHERE id=$userID";
	$result = $mysqli->query($query);
	if($result->num_rows < 1)
	{
		echo("-1");
	}else{
		$query = "SELECT * FROM worldBoss WHERE id=$userID";
		$result = $mysqli->query($query);
		if($result->num_rows < 1)
		{
			echo("3");
		}else{
			$row = $result->fetch_object();
			$dailyCount = $row->dailyCount;
			if($dailyCount > 0)
			{
				$nowDate = date('y-n-j');
				$preDate = $row->data;
				if($dailyCount == 0)
				{
					if(strcmp($nowDate,$preDate) != 0 )
					{
						echo("3");
					}else{
						echo("0");
					}
				}else{
					echo($dailyCount);
				}
			}else{
				echo("-1");
			}
		}	
	}
	$mysqli->close();
}

//mission start
if( isset($_GET['id']) && isset($_GET['start']) )
{
	
	$userID = $_GET['id'];
	$mysqli->query("SET NAMES utf8");
	$query = "SELECT * FROM users WHERE id=$userID";
	$result = $mysqli->query($query);
	if($result->num_rows < 1)
	{
		echo("-1");
	}else{
		$row = $result->fetch_object();
		$userName = $row->name;
		$query = "SELECT * FROM worldBoss WHERE id=$userID";
		$result = $mysqli->query($query);
		if($result->num_rows < 1)
		{
			$nowTime = time();
			$nowDate = date('y-n-j');
			$query = "INSERT INTO worldBoss(id, dailyCount,date,amountDamage,startTime,endTime,name) VALUES($userID,  3, '$nowDate',0,$nowTime,-1,'$userName')";
			$result = $mysqli->query($query);
			$query = "SELECT * FROM bossList where id=1";
			$result = $mysqli->query($query);
			$row = $result->fetch_object();
			$hp = $row->hp;
			if($hp<100000)$hp = 100500;
			echo($hp);
		}else{
			$row = $result->fetch_object();
			$dailyCount = $row->dailyCount;
			if($dailyCount > 0)
			{
				$query  = "SELECT * FROM bossList where id=1";
				$result = $mysqli->query($query);
				$row    = $result->fetch_object();
				$hp = $row->hp;
				if($hp<100000)$hp = 100500;
				echo($hp);
			}else{
				echo("-1");
			}
		}	
	}
	$mysqli->close();
}

//complete world boss
if( isset($_GET['id']) && isset($_GET['damage']) )
{
	$userID = $_GET['id'];
	$damage = $_GET['damage'];
	if($damage > 100000)$damage = 100000;
	
	$query = "SELECT id FROM users WHERE id=$userID";
	$result = $mysqli->query($query);
	if($result->num_rows < 1)
	{
		echo("no user -1");
	}else{
		$query = "SELECT * FROM worldBoss WHERE id=$userID";
		$nowDate = date('y-n-j');
		$nowTime = time();
		$result = $mysqli->query($query);
		if($result->num_rows < 1)
		{
			echo("no quest -1");
		}else{
			$row = $result->fetch_object();
			$preDate = $row->date1;
			$preTime = $row->startTime;
			$missTime = $nowTime - $preTime;
			$nowCount = $row->dailyCount-1;
			if($damage  == -1 || $missTime < 90)
			{
				$query = "UPDATE worldBoss SET dailyCount=$nowCount,startTime=$nowTime WHERE id=$userID";
				$result = $mysqli->query($query);
				echo("time <90 -1");
				$mysqli->close();
				return;
			}
			if( strcmp($nowDate,$preDate) == 0 || $row->dailyCount < 1)
			{
				echo("fail in now data -1");
			}else{
				$query = "UPDATE worldBoss SET dailyCount=$nowCount,startTime=$nowTime,amountDamage=amountDamage+$damage WHERE id=$userID";
				$result = $mysqli->query($query);
				$query = "UPDATE bossList SET hp=hp-$damage WHERE id=1";
				$result = $mysqli->query($query);
				echo("in update 1");
			}
		}	
	}
	$mysqli->close();
}

if( isset($_GET['leader']) && isset($_GET['id']) )
{	
	$mysqli->query("SET NAMES utf8");
	$query = "SELECT * FROM worldBoss ORDER BY amountDamage DESC LIMIT 10";
	$result = $mysqli->query($query);
	$resultAry = array();
	while($row = $result->fetch_object())
	{
		//echo $row->name;
		$item = array("name"=>urlencode($row->name), "amountDamage"=>$row->amountDamage);
		array_push($resultAry, $item);
	}
	
	$userID = $_GET['id'];
	$query = "SELECT count(1) FROM worldBoss WHERE amountDamage>=(SELECT amountDamage FROM worldBoss WHERE id=$userID)";
	$result = $mysqli->query($query);
	$row = $result->fetch_assoc();
	$item = array("self"=> $row['count(1)']);
	array_push($resultAry, $item);
	$json_string = urldecode(json_encode($resultAry));
	echo $json_string;
	$mysqli->close();
}

if( isset($_GET['bonus']) && isset($_GET['id']) )
{
	$userID = $_GET['id'];
	$query = "SELECT bonus FROM worldBoss WHERE id=$userID";
	$result = $mysqli->query($query);
	if($result->num_rows < 1)
	{
		$resultAry = array("rank"=> -1, "bonus"=>0);
		$json_string =json_encode($resultAry);
		echo $json_string;
	}else{
		$row = $result->fetch_object();
		$bonus = $row->bonus;
		if($bonus == 1)
		{
			$query = "SELECT count(1) FROM worldBoss WHERE amountDamage>=(SELECT amountDamage FROM worldBoss WHERE id=$userID)";
			$result = $mysqli->query($query);
			$row = $result->fetch_assoc();
			$rank = $row['count(1)'];
			switch($rank)
			{
				case 1:
					$bonusValue = 50;
					break;
				case 2:
					$bonusValue = 45;
					break;
				case 3:
					$bonusValue = 40;
					break;
				case 4:
					$bonusValue = 35;
					break;
				case 5:
					$bonusValue = 30;
					break;
				case 6:
					$bonusValue = 25;
					break;
				case 7:
					$bonusValue = 20;
					break;
				case 8:
					$bonusValue = 15;
					break;
				case 9:
					$bonusValue = 12;
					break;
				case 10:
					$bonusValue = 10;
					break;
				default:
					$bonusValue = 1;
					break;
			}
			$resultAry = array("rank"=> $rank, "bonus"=>$bonusValue);
			$json_string =json_encode($resultAry);
			echo $json_string;
		}else{
			$resultAry = array("rank"=> -1, "bonus"=>0);
			$json_string =json_encode($resultAry);
			echo $json_string;
		}
	}
	$mysqli->close();
}

/*
if( isset($_GET['id']) && isset($_GET['rank']) )
{
	$userID = $_GET['id'];
	$query = "SELECT count(1) FROM worldBoss WHERE amountDamage>=(SELECT amountDamage FROM worldBoss WHERE id=$userID)";
	$result = $mysqli->query($query);
	$row = $result->fetch_assoc();
	echo $row['count(1)'];
	//foreach ($row as $value){  
		//echo($key.':'.$val.'<br/>');  
    	//echo($value);  
	//}  
	//var_dump(get_object_vars($row));
	$mysqli->close();
}
*/

?>