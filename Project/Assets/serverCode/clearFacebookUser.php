<?php
include "mysql.connect.php";
/*$conn = mysql_pconnect("127.0.0.1", "root", "4219");
mysql_select_db("gameTest");
mysql_query("set name utf8;");
*/
//sendInvent
if( isset($_REQUEST['fbid'])  )
{

	$fbid =  $_REQUEST['fbid'];
	
	
	$sql = "SELECT * FROM users WHERE facebook_id='$fbid'";
	$result = $mysqli->query($sql);
	$amount = $result->num_rows;
	if($amount >0)
	{
		while($row = $result->fetch_object())
		{
			
			$id = $row->id;
			$name = $row->name;
			echo "id = $id   name = $name <br/>";
		}
	}else{
		
		echo "this user was not exist.";
		return;
	}
	
	$sql = "DELETE FROM users WHERE id = $id";
	$mysqli->query($sql);
	echo 'users info deleted<br/>';
	$sql = "DELETE FROM friendlist WHERE id =$id";
	$mysqli->query($sql);
	echo 'friendlist info deleted<br/>';
	$sql = "DELETE FROM gift WHERE recipient=$id OR sender=$id";
	$mysqli->query($sql);
	echo 'gift info deleted<br/>';
	$sql = "DELETE FROM messageBox WHERE recipient=$id OR sender=$id";
	$mysqli->query($sql);
	echo 'messageBox info deleted<br/>';
	$idstr = $name.'$'.$id.'$'.$fbid;
	$sql = "DELETE FROM socialRaids WHERE sender=$idstr OR recipientA=$idstr OR recipientB=$idstr OR recipientC=$idstr";
	$mysqli->query($sql);
	echo 'socialRaids info deleted<br/>';
	$sql = "DELETE FROM userGameData WHERE id=$id";
	$mysqli->query($sql);
	echo 'userGameData info deleted<br/>';
	$sql = "DELETE FROM tutor WHERE user_id=$id OR tutor_id = $id";
	$mysqli->query($sql);
	echo 'tutor info deleted<br/>';
	$sql = "DELETE FROM facebookinfo WHERE user_id=$id";
	$mysqli->query($sql);
	echo 'facebookinfo info deleted<br/>';
	$mysqli->close();
}

?>
<form action='clearFacebookUser.php' method="post">
    <label>facebook id:</label>
    <input name='fbid' type='text'/>
    <input type='submit'/>
</form>