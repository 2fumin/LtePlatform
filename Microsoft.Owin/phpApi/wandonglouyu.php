<?php
header('content-type:application/json;charset=utf8');
include_once("zonghexinxiConnection.php");
$sqlwd = "SELECT * FROM wandonglouyu where 是否5米地图区域='是' and 评估结果1='合格'";
$resultwd = mysql_query($sqlwd);
$recordcountwd = mysql_num_rows($resultwd);
$result=array();
while ($rowswd = mysql_fetch_assoc($resultwd))
{
    array_push($result, array (
        'longtitute'=>$rowswd["经度"],
        'lattitute'=>$rowswd["纬度"],
        'name'=>urlencode($rowswd["楼群名称"]),
        'type'=>$rowswd["类型"],
        'rsrpCoverage'=>$rowswd["RSRP达标率"],
        'evaluationResult'=>urlencode($rowswd["评估结果1"]),
        'testResult'=>$rowswd["测试评估"],
        'alternativeResult'=>$rowswd["评估结果"],
        'coverageRate'=>$rowswd["覆盖率"]
        ));
}
echo urldecode(json_encode($result));
?>