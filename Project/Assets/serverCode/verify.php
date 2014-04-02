<?php
	//change VAILDATING_RECEIPTS_URL in ECPurchase.h to "ourServer/verify.php"
	$receiptData = $_POST;
	//extract data from the post
	extract($_POST);
	
	//set POST variables
	$url = 'https://sandbox.itunes.apple.com/verifyReceipt';

	//open connection
	$ch = curl_init();
	
	//set the url, number of POST vars, POST data
	curl_setopt($ch,CURLOPT_URL,$url);
	curl_setopt($ch,CURLOPT_POST,1);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
	
	$data_string = json_encode($receiptData);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $data_string);
	//Send blindly the json-encoded string.
	//The server, IMO, expects the body of the HTTP request to be in JSON

	//execute post
	$result = curl_exec($ch);
	
	//close connection

	curl_close($ch);
	echo($result);
	
	//log iap info on server
	/*
	{
	    "receipt": {
	        "original_purchase_date_pst": "2012-04-30 08:05:55 America/Los_Angeles",
	        "original_transaction_id": "1000000046178817",
	        "original_purchase_date_ms": "1335798355868",
	        "transaction_id": "1000000046178817",
	        "quantity": "1",
	        "product_id": "br.com.jera.Example",
	        "bvrs": "20120427",
	        "purchase_date_ms": "1335798355868",
	        "purchase_date": "2012-04-30 15:05:55 Etc/GMT",
	        "original_purchase_date": "2012-04-30 15:05:55 Etc/GMT",
	        "purchase_date_pst": "2012-04-30 08:05:55 America/Los_Angeles",
	        "bid": "br.com.jera.Example",
	        "item_id": "521129812"
	    },
	    "status": 0
	}
	*/
	
	$resultHash = json_decode($result);
	if ($resultHash['status'] == 0) {
		$receiptDetail = $resultHash['receipt'];
		$transactionID = $receiptDetail['transaction_id'];
		$productID = $receiptDetail['product_id'];
		$quantity = $receiptDetail['quantity'];
		
		$insertTransaction = "INSERT INTO transactions(transactionID, productID, quantity) VALUES('$transactionID','$productID', $quantity)";
		$mysqli->query($addFrdQuery);
	}
	
	
	
	
?>