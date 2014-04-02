<?php
include "mysql.connect.php";

//session_start();
//$_SESSION['gameID']=1
//unset($_SESSION['views']);
//session_destroy();

//
if( isset($_GET['id']) && isset($_GET['q']) )
{
	$userID = $_GET['id'];
	
	$query = "SELECT id FROM users WHERE id=$userID";
	$result = $mysqli->query($query);
	if($result->num_rows < 1)
	{
		echo("fail");
	}else{
		$query = "SELECT * FROM quest WHERE id=$userID";
		$result = $mysqli->query($query);
		if($result->num_rows < 1)
		{
			$query = "INSERT INTO quest(id, quest1,date1) VALUES($userID,  0, 'S')";
			$result = $mysqli->query($query);
			echo("success");
		}else{
			$row = $result->fetch_object();
			$preDate = $row->date1;
			$nowDate = date('y-n-j');
			$questIndex = $row->quest1;
			if(strcmp($nowDate,$preDate) != 0)
			{
				echo("success");
			}else{
				echo("fail");
			}
			/*if($questIndex > 0)
			{
				echo("fail");
			}else{
				echo("success");
			}*/
		}	
	}
	$mysqli->close();
}
//complete quest
if( isset($_GET['id']) && isset($_GET['c']) )
{
	$userID = $_GET['id'];
	
	$query = "SELECT id FROM users WHERE id=$userID";
	$result = $mysqli->query($query);
	if($result->num_rows < 1)
	{
		echo("-1");
	}else{
		$query = "SELECT * FROM quest WHERE id=$userID";
		$nowDate = date('y-n-j');
		$result = $mysqli->query($query);
		if($result->num_rows < 1)
		{
			echo("-1");
		}else{
			$row = $result->fetch_object();
			$preDate = $row->date1;
			if( strcmp($nowDate,$preDate) == 0 )
			{
				echo("-1");
			}else{
				$query = "UPDATE quest SET quest1=1, date1='$nowDate' WHERE id=$userID";
				$result = $mysqli->query($query);
				echo("1");
			}
		}	
	}
	$mysqli->close();
}
?>