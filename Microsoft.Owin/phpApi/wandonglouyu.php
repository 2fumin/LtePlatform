<?php
include_once("encode_json.php");
header('content-type:application/json;charset=utf8');
header('Access-Control-Allow-Origin: *');
include_once("zonghexinxiConnection.php");
$sqlwd = "SELECT 经度, 纬度, 楼群名称, 类型, RSRP达标率, 评估结果1, 是否5米地图区域 FROM wandonglouyu";
$resultwd = mysql_query($sqlwd);
$recordcountwd = mysql_num_rows($resultwd);
$result=array();
while ($rowswd = mysql_fetch_assoc($resultwd))
{
    array_push($result, array (
        'longtitute'=>$rowswd["经度"],
        'lattitute'=>$rowswd["纬度"],
        'name'=>$rowswd["楼群名称"],
        'type'=>$rowswd["类型"],
        'rsrpCoverage'=>$rowswd["RSRP达标率"],
        'evaluationResult'=>$rowswd["评估结果1"],
        'isInFiveMeter'=>$rowswd["是否5米地图区域"]
        ));
}
echo encode_json($result);
?>