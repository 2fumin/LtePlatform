using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.MySqlFramework.Abstract;

namespace Lte.Evaluations.DataService.Kpi
{
    public class FlowService
    {
        private readonly IFlowHuaweiRepository _huaweiRepository;

        public FlowService(IFlowHuaweiRepository huaweiRepositroy)
        {
            _huaweiRepository = huaweiRepositroy;
        }
    }
}
