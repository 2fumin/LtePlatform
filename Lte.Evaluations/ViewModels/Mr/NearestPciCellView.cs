﻿using AutoMapper;
using Lte.Domain.Regular.Attributes;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Neighbor;

namespace Lte.Evaluations.ViewModels.Mr
{
    [TypeDoc("包含PCI的LTE邻区关系视图")]
    public class NearestPciCellView
    {
        [MemberDoc("小区编号（对于LTE来说就是基站编号）")]
        public int CellId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("邻区小区编号")]
        public int NearestCellId { get; set; }

        [MemberDoc("邻区扇区编号")]
        public byte NearestSectorId { get; set; }

        [MemberDoc("PCI，便于查询邻区")]
        public short Pci { get; set; }

        [MemberDoc("切换次数，仅供参考")]
        public int TotalTimes { get; set; }

        [MemberDoc("邻区基站名称")]
        public string NearestENodebName { get; set; }
        
        public static NearestPciCellView ConstructView(NearestPciCell stat, IENodebRepository repository,
            ICellRepository cellRepository, IInfrastructureRepository infrastructureRepository)
        {
            var view = Mapper.Map<NearestPciCell, NearestPciCellView>(stat);
            var eNodeb = repository.GetByENodebId(stat.NearestCellId);
            view.NearestENodebName = eNodeb == null ? "Undefined" : eNodeb.Name;
            return view;
        }
    }
}
