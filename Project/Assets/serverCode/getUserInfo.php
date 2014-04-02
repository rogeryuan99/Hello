<?php
include "mysql.connect.php";
	$query = "SELECT id,name FROM users WHERE deviceToken != ''";
	$result = $mysqli->query($query);
	while($row = $result->fetch_object())
	{
		echo "\"".$row->name."\", ".$row->id."<br/>";
	}
	$mysqli->close();
?>