<?php
require_once("../config/db.php");
	header("Content-type: text/html; charset=utf-8");
	// error_reporting(E_ALL & ~E_NOTICE & ～E_WARNING);
	$conn = mysql_pconnect($hostname_DB,$username_DB,$password_DB) 
		or die("could not connect");
	mysql_select_db($database_DB) 
		or die("could not select db");	
	mysql_query("set names utf8;");
?>
