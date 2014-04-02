<?php
require_once "conn.php";
require_once "../notificationDefine.php";
require_once "sendNotification.php";

if(isset($_REQUEST['action'])){
    $action = $_REQUEST['action'];
    $json = $_REQUEST['data'];
    switch ($action) {
        case 'regfbid':
            //加入信息
            $dj = json_decode(urldecode($json));
            $uid = mysql_real_escape_string($dj->uid);
            $fid = mysql_real_escape_string($dj->fid);

            if(!isUserExist($dj->uid)){
                echo "User doesn't exist.";
                return;
            }
            //add by xiaoyong 20120614
            //start----------------->
            $querySql = "SELECT user_id FROM facebookinfo WHERE user_id=$uid";
            $result = mysql_query($querySql) or die(mysql_error());
            if(mysql_num_rows($result) > 0)
            {
            	echo "This fb account already bind other acount.";
            	return;
            }
            //<-----------------end
            
            $sql = "insert into users (id, facebook_id) values ('".$uid."', '".$fid."') ON DUPLICATE KEY UPDATE facebook_id='".$dj->fid."' ;";
            mysql_query($sql) or die(mysql_error());

            $sql = "select id from users where friend_ids like '%".$fid."%';";
            $result = mysql_query($sql) or die(mysql_error());
            while ($row = mysql_fetch_array($result)) {
                addMeToFriendList($uid, $row['id']);
                addMeToFriendList($row['id'], $uid);
            }
            break;
        //add by xiaoyong 20120614
        //start---------------->
        case 'unhook':
        	$dj = json_decode(urldecode($json));
            $uid = mysql_real_escape_string($dj->uid);
            $querySql = "DELETE FROM facebookinfo WHERE user_id=$uid";
            $result = mysql_query($querySql) or die(mysql_error());
            if($result)
            {
            	echo "success";
            }else{
            	echo "failed";
            }
        	break;
        //<----------------------end
        case 'updatefriend':
            $dj = json_decode(urldecode($json));
            $uid = mysql_real_escape_string($dj->uid);
            $friends = mysql_real_escape_string($dj->friends);

            if(!isUserExist($dj->uid)){
                echo "User doesn't exist.";
                return;
            }

            $sql = "insert into users (id, friend_ids) values ('".$uid."', '".$friends."') ON DUPLICATE KEY UPDATE friend_ids='".$dj->friends."' ;";
            mysql_query($sql) or die(mysql_error());

            $friend_array = explode("|", $friends);
            if($friend_array && count($friend_array)>0){
                foreach ($friend_array as $key => $value) {
                    $friend_array[$key] = "'".$value."'";
                }
                $in_str = implode(",", $friend_array);
            }

            $sql = "select id from users where facebook_id in (".$in_str.")";
            $result = mysql_query($sql) or die(mysql_error());
            while ($row = mysql_fetch_array($result)) {
                addMeToFriendList($uid,$row['id']);
                addMeToFriendList($row['id'],$uid);
            }

            break;

        case 'getfriendlist':
            $dj = json_decode(urldecode($json));
            $uid = mysql_real_escape_string($dj->uid);

            $sql = "select list from friendlist where id=".$uid;
            $result = mysql_query($sql) or die(mysql_error());
            $row = mysql_fetch_array($result);
            if($row){
                $friendArray = explode("|", $row['list']);
                if(strlen($row['list'])>0 && $friendArray && count($friendArray)>0){
                    $in_str = implode(",", $friendArray);
                    /*$sql = "select tutor_id from tutor where user_id=".$uid;
                    $result = mysql_query($sql) or die(mysql_error());
                    $resultArray2 = array();
                    while ($row = mysql_fetch_array($result)) {
                        array_push($resultArray2, $row['tutor_id']);
                    }*/

                    $sql = "select a.id, a.name, a.facebook_id from users a where a.id in (".$in_str.")";
                    $result = mysql_query($sql) or die(mysql_error());
                    $resultArray = array();
                    while ($row = mysql_fetch_array($result)) { 
                        //$isTutor = (in_array($row['id'], $resultArray2))?1:0;
                        $isTutor = 0;
                        array_push($resultArray, array('id'=>$row['id'],'name'=>$row['name'],'facebook_id'=>$row['facebook_id'],"research"=>$isTutor));
                    }
                    echo json_encode($resultArray);
                }else{
                    echo "[]";
                }
            }else{
                echo "[]";
            }
            
            break;

        case 'sendgift':
            $dj = json_decode(urldecode($json));
            $sender = mysql_real_escape_string($dj->sender);
            $recipient = mysql_real_escape_string($dj->recipient);
            $gift = mysql_real_escape_string($dj->gift);
            
            $minSendTime = 24*60*60;
            $currentTime = time();
            $lastSendTime = $currentTime - $minSendTime;
            $sql = "select id from gift where sender=".$sender." and recipient=".$recipient." and timestamp>".$lastSendTime;
            $result = mysql_query($sql) or die(mysql_error());
            if(mysql_num_rows($result)>0){
                $resultArray = array("result"=>"time error");
            }else{
                $sql = "insert into gift (sender, recipient, gift, timestamp, received) values (".$sender.",".$recipient.",".$gift.",".$currentTime.",0)";
                mysql_query($sql) or die(mysql_error());
                $resultArray = array("result"=>"ok");
            }
            echo(json_encode($resultArray));
            //add by xiaoyong  20120702 push notification
			$sql = "SELECT id,name,deviceToken FROM users WHERE id=$sender OR id=$recipient";
			$result = mysql_query($sql) or die(mysql_error());
			while( $row = mysql_fetch_array($result) )
			{
				if($recipient == $row['id'])
				{
					if(strlen($row['deviceToken'])< 1)
					{
						break;
					}
					$recipientTk   = $row['deviceToken'];
					//$recipientName = $row['name'];
				}else{
					$senderName = $row['name'];
				}
			}
			$NtfMsg = str_replace("@",$senderName,GIFT_SEND);
			sendNotification($recipientTk, $NtfMsg);
            break;

        case 'giftlist':
            $dj = json_decode(urldecode($json));
            $uid = mysql_real_escape_string($dj->uid);

            $sql = "select distinct a.sender, b.name, b.facebook_id from gift a, users b  where a.received=0 and a.sender=b.id and a.recipient=".$uid;
            $result = mysql_query($sql) or die(mysql_error());
            $resultArray = array();
            while ($row = mysql_fetch_array($result)) {
                array_push($resultArray, array("id"=>$row['sender'],"name"=>$row['name'],"facebook_id"=>$row['facebook_id']));
            }
            echo(json_encode($resultArray));
            break;

        case 'acceptgift':
            $dj = json_decode(urldecode($json));
            $uid = mysql_real_escape_string($dj->uid);
            
            $sql = "select gift,count(id) as giftnumber from gift where recipient=".$uid." and received=0 group by gift";
            $result = mysql_query($sql) or die(mysql_error());
            $resultArray = array();
            while ($row = mysql_fetch_array($result)){
                array_push($resultArray, array("giftid"=>$row['gift'],"giftnumber"=>$row['giftnumber']));
            }
            echo(json_encode($resultArray));
            $sql = "update gift set received=1 where recipient=".$uid." and received=0"; 
            mysql_query($sql) or die(mysql_error());    
            break;

        case 'requestResearch':
            $dj = json_decode(urldecode($json));
            $uid = mysql_real_escape_string($dj->uid);
            $tid = mysql_real_escape_string($dj->tid);
            $heroType = mysql_real_escape_string($dj->heroType);
            $sql = "SELECT id FROM researcherMsg WHERE sender=$uid AND recipient=$tid";
            $result = mysql_query($sql) or die(mysql_error());
            if(mysql_num_rows($result)>0){
            	$resultArray = array("result"=>"error");
            }else{
            	$sql = "INSERT INTO researcherMsg(sender, recipient, heroType, accept) VALUES($uid, $tid, '$heroType', 2)";
            	mysql_query($sql);
            	$resultArray = array("result"=>"ok");
            }
            echo(json_encode($resultArray));
            break;
		case 'acceptResearch':
			$dj = json_decode(urldecode($json));
            $uid = mysql_real_escape_string($dj->uid);
            $tid = mysql_real_escape_string($dj->tid);
            $accept = mysql_real_escape_string($dj->accept);

            $sql = "select count(tutor_id) as tnum from tutor where user_id=".$uid;

            $result = mysql_query($sql) or die(mysql_error());
            $row = mysql_fetch_array($result);
            $resultArray = array();
            if($row && $row['tnum']>=8){
                $resultArray = array("result"=>"too many tutors");
            }else{
            	 if($accept == 1)
            	{
            		$sql = "UPDATE researcherMsg SET accept=1 WHERE sender=$tid AND recipient=$uid";
            	 	mysql_query($sql) or die(mysql_error());
            		$sql = "insert into tutor (user_id, tutor_id) values (".$tid.",".$uid.") ON DUPLICATE KEY UPDATE tutor_id=".$uid;
                	mysql_query($sql) or die(mysql_error());
            	}else{
            		$sql = "DELETE FROM researcherMsg WHERE sender=$tid AND recipient=$uid";
            		mysql_query($sql) or die(mysql_error());
            	}
               
                $resultArray = array("result"=>"ok");
            }
            echo(json_encode($resultArray));
			break;
        case 'getResearchRequest':
            $dj = json_decode(urldecode($json));
            $uid = mysql_real_escape_string($dj->uid);
			
			$sql = "SELECT researcherMsg.accept, researcherMsg.heroType,researcherMsg.sender, users.name, users.facebook_id 
							FROM researcherMsg, users 
							WHERE researcherMsg.recipient=$uid AND researcherMsg.sender=users.id";
            //$sql = "select a.tutor_id, b.name, b.facebook_id from tutor a , users b where b.id=a.tutor_id and a.user_id=".$uid;
            $result = mysql_query($sql) or die(mysql_error());
            $resultArray = array();
            while ($row = mysql_fetch_array($result)) {
            	if($row['accept'] == 2)
            	array_push($resultArray, array("id"=>$row['sender'],"name"=>$row['name'],"facebook_id"=>$row['facebook_id'], "heroType"=>$row['heroType'], "accept"=>$row['accept']));
                //array_push($resultArray, array("id"=>$row['tutor_id'],"name"=>$row['name'],"facebook_id"=>$row['facebook_id']));
            }
            $sql = "SELECT researcherMsg.accept, researcherMsg.heroType,researcherMsg.sender, users.name, users.facebook_id 
							FROM researcherMsg, users 
							WHERE researcherMsg.sender=$uid AND researcherMsg.recipient=users.id";
			$result = mysql_query($sql) or die(mysql_error());
			while ($row = mysql_fetch_array($result)) {
				if($row['accept'] == 1)
            	array_push($resultArray, array("id"=>$row['sender'],"name"=>$row['name'],"facebook_id"=>$row['facebook_id'], "heroType"=>$row['heroType'], "accept"=>$row['accept']));
                //array_push($resultArray, array("id"=>$row['tutor_id'],"name"=>$row['name'],"facebook_id"=>$row['facebook_id']));
            }
            echo(json_encode($resultArray));
            $sql = "DELETE FROM researcherMsg WHERE sender=$uid AND accept=1";
            mysql_query($sql) or die(mysql_error());
            break;
        case 'checkregister':
            $dj = json_decode(urldecode($json));
            $email = mysql_real_escape_string($dj->email);

            $sql = "select id from users where email='".$email."'";
            $result = mysql_query($sql) or die(mysql_error());
            $resultArray = array();

            if(mysql_num_rows($result)>0){
                $resultArray=array("result"=>"YES");
            }else{
                $resultArray=array("result"=>"NO");
            }
            echo(json_encode($resultArray));
            break;

        case 'checkfacebook':
            $dj = json_decode(urldecode($json));
            $uid = mysql_real_escape_string($dj->id);
            $facebook_id = mysql_real_escape_string($dj->facebook_id);
            
            if($uid == -1)
            {
            	$sql = "select id from users where facebook_id='".$facebook_id."'";
            }else{
            	$sql = "select id from users where facebook_id='$facebook_id' and id=$uid";
            }
            $result = mysql_query($sql) or die(mysql_error());
            $resultArray = array();
			//add by xiaoyong 20120614  
			if($uid == -1)
			{
				if(mysql_num_rows($result)>0){
                	$resultArray=array("result"=>"YES");
            	}else{
                	$resultArray=array("result"=>"NO");
           		}
			}else{
				if(mysql_num_rows($result)>0){
                	$resultArray=array("result"=>"NO");
            	}else{
                	$resultArray=array("result"=>"YES");
           		}
			}
            
            echo(json_encode($resultArray));
            break;

        case 'register':
            $dj = json_decode(urldecode($json));
            $email = mysql_real_escape_string($dj->email);
            $nickname = mysql_real_escape_string($dj->nickname);
            $password = md5(getPassword(8));
            $password = md5("1234");
            $secret = getSecret();

            $sql = "select id from users where email='".$email."' or name='".$nickname."'";

            $result = mysql_query($sql) or die(mysql_error());
            $resultArray = array();

            if(mysql_num_rows($result)>0){
                $resultArray=array("result"=>"name used");
            }else{
                $sql = "insert into users (name, password, email, secretToken) values ('$nickname','$password','$email','$secret')";
                $result = mysql_query($sql) or die(mysql_error());
                $id = mysql_insert_id();
                $resultArray=array("result"=>"OK","token"=>$secret,"id"=>$id);
            }
            echo(json_encode($resultArray));
            break;
		case 'changeName':
			$dj = json_decode(urldecode($json));
			$nickname = mysql_real_escape_string($dj->nickname);
			$uid = mysql_real_escape_string($dj->uid);
			
			$querySql = "SELECT name FROM users WHERE name='$nickname' LIMIT 3";
			$result = mysql_query($querySql) or die(mysql_error());
			if(mysql_num_rows($result) < 1)
			{
				$sql = "UPDATE users SET name='$nickname' WHERE id=$uid";
				$result = mysql_query($sql) or die(mysql_error());
				$resultArray = array("error"=>"0");
				echo(json_encode($resultArray));
			}else{
				$resultArray = array("error"=>"1");
				echo(json_encode($resultArray));
			}
			break;
        case 'registerfacebook':
            $dj = json_decode(urldecode($json));
            $facebook_id = mysql_real_escape_string($dj->facebook_id);
            $nickname = mysql_real_escape_string($dj->nickname);
            $facebook_token = mysql_real_escape_string($dj->facebook_token);
            $deviceTk = mysql_real_escape_string($dj->deviceTk);
            $password = md5(getPassword(8));
            $password = md5("1234");
            $secret = getSecret();

            $sql = "select id from users where facebook_id='$facebook_id'";

            $result = mysql_query($sql) or die(mysql_error());
            $resultArray = array();

            if(mysql_num_rows($result)>0){
                $resultArray=array("result"=>"used");
            }else{
                $sql = "insert into users (name, password, facebook_id, secretToken, facebook_token, deviceToken) values ('$nickname','$password','$facebook_id','$secret','$facebook_token', '$deviceTk')";
                $result = mysql_query($sql) or die(mysql_error());
                $id = mysql_insert_id();
                $resultArray=array("result"=>"OK","token"=>$secret,"id"=>$id);
            }
            echo(json_encode($resultArray));
            break;

        case 'login':
            $dj = json_decode(urldecode($json));
            $email = mysql_real_escape_string($dj->email);
            $passwordmd5 = mysql_real_escape_string($dj->passwordmd5);
            $version = mysql_real_escape_string($dj->version);

            $sql = "select id,secretToken,name from users where email='$email' and password='$passwordmd5'";
            $result = mysql_query($sql) or die(mysql_error());
            $resultArray = array();
            $row = mysql_fetch_array($result);
            if($row){
            	//add by xiaoyong 20120629
            	$user_id = $row['id'];
            	$sql = "SELECT * FROM userGameData WHERE id=$user_id";
				$resultGameData = mysql_query($sql);
				$rowGameData = mysql_fetch_array($resultGameData);
				$gameData = array('id'=>intval($rowGameData['id']),
						  'boxKey'=>intval($rowGameData['boxKey']),
						  'box'=>intval($rowGameData['box']),
						  'clearCD'=>intval($rowGameData['potion1cd']),
						  'relive'=>intval($rowGameData['potion2live']),
						  'coins'=>intval($rowGameData['coins']),
						  'credits'=>intval($rowGameData['credits']),
						  'fuel'=>intval($rowGameData['fuel']),
						  'equipmentIDList'=>$rowGameData['equipList'],
						  'map'=>json_decode(urldecode($rowGameData['map'])),
						  'heroes'=> json_decode(urldecode($rowGameData['heroes'])),
						  'learnSkill'=> json_decode(urldecode($rowGameData['learnSkill']))
						  );
				//-----------------------
                $resultArray = array("token"=>$row['secretToken'],"name"=>$row['name'],"id"=>$row['id'],"gameData"=>$gameData);
            }else{
                $resultArray = array("token"=>"","name"=>"","id"=>"","gameData"=>"");
            }
            echo(json_encode($resultArray));
            break;

        case 'loginfacebook':
            $dj = json_decode(urldecode($json));
            $facebook_id = mysql_real_escape_string($dj->facebook_id);
            $facebook_token = mysql_real_escape_string($dj->facebook_token);
            $version = mysql_real_escape_string($dj->version);
            $deviceTk = mysql_real_escape_string($dj->deviceTk);

            $real_id = checkFacebookToken($facebook_token);
            $resultArray = array("token"=>"","name"=>"");
            if($real_id == $facebook_id || true){
            	//modified by xiaoyong 20120614
                $sql = "select id,secretToken,name,email from users where facebook_id='$facebook_id'";
                //$sql = "select id,secretToken,name from users where facebook_id='$facebook_id'";
                $result = mysql_query($sql) or die(mysql_error());
                $resultArray = array();
                $row = mysql_fetch_array($result);
                if($row){
                	//add by xiaoyong 20120629
					$user_id = $row['id'];
					/*$sql = "SELECT * FROM userGameData WHERE id=$user_id";
					$resultGameData = mysql_query($sql);
					$rowGameData = mysql_fetch_array($resultGameData);
					$gameData = array('id'=>intval($rowGameData['id']),
							  'boxKey'=>intval($rowGameData['boxKey']),
							  'box'=>intval($rowGameData['box']),
							  'clearCD'=>intval($rowGameData['potion1cd']),
							  'relive'=>intval($rowGameData['potion2live']),
							  'coins'=>intval($rowGameData['coins']),
							  'credits'=>intval($rowGameData['credits']),
							  'fuel'=>intval($rowGameData['fuel']),
							  'equipmentIDList'=>$rowGameData['equipList'],
							  'map'=>json_decode(urldecode($rowGameData['map'])),
							  'heroes'=> json_decode(urldecode($rowGameData['heroes'])),
							  'learnSkill'=> json_decode(urldecode($rowGameData['learnSkill']))
							  );*/
					//-----------------------
                    $resultArray = array("token"=>$row['secretToken'],"name"=>$row['name'], "timeStamp"=>intval($rowGameData['timeStamp']),
                    					"id"=>$row['id'], "email"=>$row['email']);//, "gameData"=>$gameData);//add a element 'email'
                    
                    $sql = "UPDATE users set deviceToken='$deviceTk' WHERE facebook_id='$facebook_id'";
                    mysql_query($sql);
                }
            }
            echo(json_encode($resultArray));
            break;

        case 'changeuserinfo':
            $dj = json_decode(urldecode($json));
            $name = mysql_real_escape_string($dj->name);
            $email = mysql_real_escape_string($dj->email);
            $passwordmd5 = mysql_real_escape_string($dj->passwordmd5);
			
			//modified by xiaoyong  20120614 
			//start----------------------->
			if($email == "")
			{
				$sql = "update users set password='$passwordmd5' where name='$name'";
				mysql_query($sql) or die(mysql_error());
			}else{
				$querySql = "SELECT name FROM users WHERE email='$email' AND name != '$name'";
				$result = mysql_query($querySql) or die(mysql_error());
				if(mysql_num_rows($result) < 1)
				{
					if($passwordmd5 == "")
					{
						$sql = "update users set email='$email' where name='$name'";
					}else{
						$sql = "update users set email='$email', password='$passwordmd5' where name='$name'";
					}
					
					mysql_query($sql) or die(mysql_error());
				}else{
					$resultArray = array("error"=>"This email was used.");
					echo(json_encode($resultArray));
				}
			}
            /*if($passwordmd5!=""){
                $sql = "update users set email='$email', password='$passwordmd5' where name='$name'";
                mysql_query($sql) or die(mysql_error());
            }*/
            //<--------------------------end
            break;

        default:
            # code...
            break;
    }

}else{
?>
<form action='dataDev2.php' method="post">
    <label>action:</label>
    <input name='action' type='text'/>
    <label>data:</label>
    <input name='data' type='text'/>
    <input type='submit'/>
</form>
<pre>
地址 http://dev.lavaspark.com/sw2/data.php

关于token的计算方法:
把data里面的所有参数名和参数，按照文档顺序（不包括secret本身）连接在一起，把返回给你的token连接在最后，然后取md5 32位小写，当作参数一起提交给服务器
例如
data={"uid":131,"secret":"999034f4fbdcc6bb537734ee1491cdd3"}
token="61b055f82173d03b069a03b802f8a591"
str = uid13161b055f82173d03b069a03b802f8a591
secret = 999034f4fbdcc6bb537734ee1491cdd3
提交

更改密码和邮箱：
action=changeuserinfo
data={"name":"123","email":"a@b.com","passwordmd5":"a657as657a6s57a6576a57a","secret":"61b055f82173d03b069a03b802f8a591"}
正常 return
邮件已被使用 return {"error":"This email was used."}

facebook登陆：
action=loginfacebook
data={"facebook_id":"123","facebook_token":"a657as657a6s57a6576a57a","version":"1"}
return
{"token":"","name":"" "id":"". "email":""}
token为""表示登陆失败

facebook注册
action=registerfacebook
data={"facebook_id":"assdsds","nickname":"xiaossdsdsdyong","facebook_token":"123123"}
return
{"result":"OK","token":"7142cebab5400e4f3279e7c133b77892"}

检查facebook_id是否注册：
action=checkfacebook
data={"facebook_id":"7a6s7d6s76d7s6d8767a6a76a76"}
return
{"result":"NO"}

登陆：
action=login
data={"email":"a@b.c","passwordmd5":"a657as657a6s57a6576a57a","version":"1"}
return
{"token":"","name":""}
token为""表示登陆失败

检查邮箱是否注册：
action=checkregister
data={"email":"a@b.c"}
return
{"result":"NO"}

注册帐号
action=register
data={"email":"assdsds@b.c","nickname":"xiaossdsdsdyong"}
return
{"result":"OK","token":"7142cebab5400e4f3279e7c133b77892"}

添加Facebook ID信息：
action=regfbid
data={"uid":"100","fid":"10000","secret":"61b055f82173d03b069a03b802f8a591"}

取消Facebook 绑定:
action = unhook
data = {"uid":"1234"}
成功返回 "success" 失败返回 "failed"

更新Facebook 好友信息：
action=updatefacebook
data={"uid":"100","friends":"10001|10002|10003","secret":"61b055f82173d03b069a03b802f8a591"}

获得好友列表
action=getfriendlist
data={"uid":131,"secret":"61b055f82173d03b069a03b802f8a591"}

送礼物
action=sendgift
data={"sender":131,"recipient":100,"gift":1,"secret":"61b055f82173d03b069a03b802f8a591"}

查礼物
action=giftlist
data={"uid":100,"secret":"61b055f82173d03b069a03b802f8a591"}

收礼物
action=acceptgift
data={"uid":100,"secret":"61b055f82173d03b069a03b802f8a591"}

加导师
action=addtutor
data={"uid":100,"tid":131,"secret":"61b055f82173d03b069a03b802f8a591"}
return
{"result":"ok"}

查导师
action=listtutor
data={"uid":100,"tid":131,"secret":"61b055f82173d03b069a03b802f8a591"}
return
[{"id":"131","name":"fkool","facebook_id":"100003751540549"}]
</pre>
<?php
}
function addMeToFriendList($me_id,$friend_id) {
        echo "\nAdd Friend ".$me_id.":".$friend_id;

        $sql = "select id, list, block from friendlist where id='".$friend_id."';";
        echo "\n".$sql;
        $result2 = mysql_query($sql) or die(mysql_error());
        if($row2 = mysql_fetch_array($result2)){
            $block = $row2['block'];
            $list = $row2['list'];
            //如果即不在好友列表也不在屏蔽列表里面，则加入好友
        }else{
            $block = "";
            $list = "";
        }

        if(!strstr($block,$me_id) && !strstr($list,$me_id)){
            if(strlen($list)>0){
                $list .= "|".$me_id;
            }else{
                $list = $me_id;
            }
            $sql = "insert into friendlist (id,list,block) values (".$friend_id.",'".$list."','".$block."') ON DUPLICATE KEY UPDATE list='".$list."', block='".$block."'";
            echo "\n".$sql;
            mysql_query($sql) or die(mysql_error());
        }

}

function isUserExist($user_id) {
    $sql = "select * from users where id='".$user_id."';";
    $result = mysql_query($sql) or die(mysql_error());
    if(mysql_num_rows($result)>0){
        return YES;
    }else{
        return NO;
    }
}

function getPassword($length=8) {
    $str = substr(md5(strval(mt_rand(0,65535)).strval(time())), 0, $length);
    return $str;
}

function getSecret() {
    $str = md5(strval(mt_rand(0,65535)).strval(time()));
    return $str;
}

function checkFacebookToken($facebook_token) {
    return "";
}
?>


