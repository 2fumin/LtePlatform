<?php
	$conn=mysql_connect("localhost","root","Abcdef9*"); //mysql主机,用户名,密码
	if($conn==null)
	{
		echo "数据库打开失败";
		exit;
	} //如果数据库无法链接则提示
	
	mysql_query("set names 'utf8'", $conn) or die(mysql_errno());
    mysql_select_db("zonghexinxi", $conn) or die(mysql_errno());
	
	 
?>