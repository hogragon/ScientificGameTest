<?php
	$link = mysqli_connect("127.0.0.1", "root", "", "test");

	if (!$link) {
		echo "Error: Unable to connect to MySQL." . PHP_EOL;
		echo "Debugging errno: " . mysqli_connect_errno() . PHP_EOL;
		echo "Debugging error: " . mysqli_connect_error() . PHP_EOL;
		exit;
	}

	if(!empty($_POST["playerid"]))
	{
		
		if(!empty($_POST["salt"]))
		{			
			if(password_verify("pwd",$_POST["salt"]))
			{	
				$stmt = $link->query("call update_player(".$_POST["playerid"].",".$_POST["coins_won"].",".$_POST["coins_bet"].")") or die("Query fail: " . mysqli_error());
				$rsarray = array();	 
				while ($row = mysqli_fetch_array($stmt,MYSQLI_ASSOC))
				{
					$rsarray[] = $row;
				}
				
				echo json_encode($rsarray);
				
				
			}
			else
			{
				$err = (object) array('error' => 'invalid salt');
				echo json_encode($err);
			}			
		}
	
	}
	else
	{
		echo "No player id" . PHP_EOL;
	}
	



mysqli_close($link);
?>