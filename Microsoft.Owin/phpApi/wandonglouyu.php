<?php
header('content-type:application/json;charset=utf8');
include_once("zonghexinxiConnection.php");
$sqlwd = "SELECT * FROM wandonglouyu where 是否5米地图区域='是' and 评估结果1='合格'";
$resultwd = mysql_query($sqlwd);
$recordcountwd = mysql_num_rows($resultwd);
$result=array();
while ($rowswd = mysql_fetch_assoc($resultwd))
{
    array_push($result, array ('longtitute'=>$rowswd["经度"],'lattitute'=>$rowswd["纬度"]));
}
echo json_encode($result);
?>