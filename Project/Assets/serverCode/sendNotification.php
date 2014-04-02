<?php
//if( isset($_GET['deviceTk']) && isset($_GET['message']) )
//{
	//my iphone4 6f3b994f f0921429 3a412098 356965af 15a2c9ef ef2ccdb6 2b310983 e1426c72
	//ipad2      e35c27a3 801d630d 444c2529 9b927035 0950d708 60a19da4 ed790850 d4c3205e
	function sendNotification($deviceTk, $msgStr)
	{
		$deviceToken = $deviceTk;
		$msgBody     = $msgStr;
		$body = array("aps" => array("alert" => $msgBody, "badge" => 1, "sound" => 'default'));  
		$ctx = stream_context_create();
		stream_context_set_option($ctx, "ssl", "local_cert", "ck.pem");   
		$fp = stream_socket_client("ssl://gateway.sandbox.push.apple.com:2195", $err, $errstr, 60, STREAM_CLIENT_CONNECT, $ctx);  
		if (!$fp) {  
			print "Failed to connect $err $errstrn";  
			return;  
		}  
		//print "Connection OK\n";  
		$payload = json_encode($body);  
		//$msg = chr(0) . pack("n",32) . pack("H*", $deviceToken) . pack("n",strlen($payload)) . $payload; 
		$msg = chr(0) . chr(0) . chr(32) . pack('H*', str_replace(' ', '', $deviceToken)) . chr(0) . chr(strlen($payload)) . $payload;
		//print "sending message :" . $payload . "\n";  
		fwrite($fp, $msg);
		//echo fread($fp, 1);
		fclose($fp);
	}
	
//}

//}
?>