# LtePlatform
This is a .net solution for LTE wireless network optimization when I work in China Telecom.
该解决方案是一个主要以WEB页面形式为呈现方式的综合网络优化分析呈现平台。

##Lte.Parameters
该工程定义了整个解决方案的基础数据库，这里主要采用Entity Framework code first框架，
###数据库包括但不仅限于：
1. LTE基础数据库：主要定义了eNodeb、小区信息，从工参文件导入；
1. CDMA基础数据库：类似于LTE基础数据库，并作为后者的补充，主要定义了BTS、CDMA小区信息，也是从工参文件导入；
1. 日常2G、3G KPI指标：目前包括掉话率、3G连接成功率、话务量、3G流量等指标，分为忙时指标和全天指标两类。其中忙时指标由专门的支撑平台导出的数据再次导入，更新较为完善。全天指标仅包括掉话率相关的指标，目前已很久没更新；
1. 日常4G精确覆盖率指标：主要包括按照镇区聚合、单小区的每天重叠覆盖率指标统计，后期待考核指标有所改变后将不再存储所有的小区统计指标（仅存储TOP指标）；
1. MRS数据提取结果：统计MR数据的总体统计指标，目前导入机制仍需完善；
1. MRO数据提取结果：统计周期性上报小区测量信息，已转化为小区间干扰关系，目前导入机制仍需完善；
1. 邻区定义信息：由于MR数据仅上报邻区PCI，这里定义了根据中心小区和邻区PCI推断邻小区信息的关系数据；
1. 告警信息：从网管系统导入。

###Entities-定义数据库实体类

###Abstract

###Concrete

##Lte.Evaluations

###DataService

##Lte.Domain

##TraceParser

##LtePlatform
