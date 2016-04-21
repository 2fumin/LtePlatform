using System;
using System.Collections.Generic;

namespace LtePlatform.Areas.HelpPage.ModelDescriptions
{
    public class KeyValuePairModelDescription : ModelDescription
    {
        public ModelDescription KeyModelDescription { get; set; }

        public ModelDescription ValueModelDescription { get; set; }

        public override IList<ParameterDescription> GetParameterDescriptions()
        {
            return null;
        }
    }
}