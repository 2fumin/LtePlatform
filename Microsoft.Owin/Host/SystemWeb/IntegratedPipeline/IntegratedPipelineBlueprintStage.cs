using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Host.SystemWeb.IntegratedPipeline
{
    internal class IntegratedPipelineBlueprintStage
    {
        public Func<IDictionary<string, object>, Task> EntryPoint { get; set; }

        public string Name { get; set; }

        public IntegratedPipelineBlueprintStage NextStage { get; set; }
    }
}
