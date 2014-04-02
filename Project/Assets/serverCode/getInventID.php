<?php
include "mysql.connect.php";

$userName = "";
$gameID   = 1000;
if( isset($_GET['username']) )
{
	$userName = $_GET['username'];
	$deviceToken = "";
	if( isset($_GET['deviceTk']) )
	{
		$deviceToken = $_GET['deviceTk'];
	}
	
	$queryUser = "SELECT * FROM users WHERE name='$userName'";
	//echo "$queryUser <br/>";
	$result = $mysqli->query($queryUser);
	$amount = $result->num_rows;
	//echo "$amount <br/>";
	if( $amount> 0)
	{
		while($row = $result->fetch_object())
		{
			$inventID = $row->id;
			$name     = $row->name;
			echo "{\"name\":\"$name\", \"invent_id\":$inventID}";
		}
		if(strlen($deviceToken)>0)
		{
			$update = "UPDATE users SET deviceToken='$deviceToken' WHERE name='$userName'";
			$mysqli->query($update);
		}
		
	}else{
		//$insert = "INSERT INTO users(name,deviceToken) VALUES('$userName','$deviceToken')";
		//$insert = "INSERT INTO users VALUES('ddd',4219)";
		//$mysqli->query($insert);
		echo "{\"name\":\"guaidao\", \"invent_id\":4219}";
	}
	
	
	/*$jsonData = array(
     'name' => $userName,
     'id'   => 4219
     );
     
    $json_string = json_encode($jsonData);*/
	//echo $json_string;
	//echo "userName = $jsondata->name";
}
?>