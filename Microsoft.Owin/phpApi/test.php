<?php
function encode_json($str) {
	return urldecode(json_encode(url_encode($str)));	
}

/**
 * 
 */
function url_encode($str) {
	if(is_array($str)) {
		foreach($str as $key=>$value) {
			$str[urlencode($key)] = url_encode($value);
		}
	} else {
		$str = urlencode($str);
	}
	
	return $str;
}

header('content-type:application/json;charset=utf8');
$conn=mysql_connect("localhost","root","Abcdef9*"); //mysql主机,用户名,密码
	if($conn==null)
	{
		echo "数据库打开失败";
		exit;
	} //如果数据库无法链接则提示
    
    mysql_query("set names 'utf8'", $conn) or die(mysql_errno());
    mysql_select_db("zonghexinxi", $conn) or die(mysql_errno());
    $sqlwd = "SELECT 经度, 纬度, 楼群名称, 类型, RSRP达标率, 评估结果1 FROM wandonglouyu where 是否5米地图区域='是' and 评估结果1='合格'";
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
        'evaluationResult'=>$rowswd["评估结果1"]
        ));
}
echo encode_json($result);
?>