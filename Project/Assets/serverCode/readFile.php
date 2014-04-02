<?php

$dirAry = Array("gameData/bg","gameData/enemy");
$version = isset($_GET['v']);

foreach($dirAry as $value)
{
	$dir =  $value;
	if( is_dir($dir) )
	{
		if($dh=opendir($dir))
		{
			//$files2 = scandir($dir, 1);
			//print_r($files1);
			//get a array 
			
			while($file = readdir($dh))
			{
				if($file != "." && $file != ".." && $file != ".DS_Store")
				{
					if($value == "gameData/bg")
					{
						$keyName = substr($file, 0, -8);
						$path    = "bg/";
					}else if($value == "gameData/enemy"){
						$keyName = substr($file, 5, -6);
						$path    = "enemy/";
					}
					// "bg_02_01":{"version":1, "url":"bg/bg_02_01.imageSH"},
					echo "\"".$keyName."\":{\"version\":".$version.",\"url\":\"".$path.$file."\"},<BR/>";
				}
				
			}
		}
	}else{
		echo "dir was not existed";
	}
	echo "<BR/>";
}

?>