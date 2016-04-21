using System;
using System.Collections.Generic;

namespace LtePlatform.Areas.HelpPage.ModelDescriptions
{
    public class CollectionModelDescription : ModelDescription
    {
        public ModelDescription ElementDescription { get; set; }

        public override IList<ParameterDescription> GetParameterDescriptions()
        {
            var complexTypeModelDescription = ElementDescription as ComplexTypeModelDescription;
            return complexTypeModelDescription?.Properties;
        }
    }
}