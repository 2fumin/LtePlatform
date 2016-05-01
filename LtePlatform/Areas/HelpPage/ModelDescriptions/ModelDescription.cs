using System;
using System.Collections.Generic;

namespace LtePlatform.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Describes a type model.
    /// </summary>
    public abstract class ModelDescription
    {
        public string Documentation { get; set; }

        public string ParameterDocumentation { get; set; }

        public Type ModelType { get; set; }

        public string Name { get; set; }

        public abstract IList<ParameterDescription> GetParameterDescriptions();
    }
}