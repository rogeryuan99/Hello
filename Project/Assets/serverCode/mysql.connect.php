<?php
$mysqli = new mysqli("127.0.0.1", "root", "4219");
if($mysqli->errno)
{
	echo "$mysqli->errno <br/>";
}else{
	$mysqli->select_db("gameTest");
}
?>