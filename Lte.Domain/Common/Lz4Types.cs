using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common
{
    internal enum AssertionConditionType
    {
        IS_TRUE,
        IS_FALSE,
        IS_NULL,
        IS_NOT_NULL
    }

    [Flags]
    internal enum ImplicitUseKindFlags
    {
        Access = 1,
        Assign = 2,
        Default = 7,
        InstantiatedNoFixedConstructorSignature = 8,
        InstantiatedWithFixedConstructorSignature = 4
    }

    [Flags]
    internal enum ImplicitUseTargetFlags
    {
        Default = 1,
        Itself = 1,
        Members = 2,
        WithMembers = 3
    }
}
