<?php
include_once("connect.php");
$sqlwd = "SELECT * FROM wandonglouyu where 是否5米地图区域='是' and 评估结果1='合格'";
$resultwd = mysql_query($sqlwd);
$recordcountwd = mysql_num_rows($resultwd);
while ($rowswd = mysql_fetch_assoc($resultwd))
{
    echo $rowswd["经度"];
    echo ",";
}
?>