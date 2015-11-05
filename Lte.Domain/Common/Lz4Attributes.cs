using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    internal sealed class AssertionConditionAttribute : Attribute
    {
        private readonly AssertionConditionType myConditionType;

        public AssertionConditionAttribute(AssertionConditionType conditionType)
        {
            myConditionType = conditionType;
        }

        public AssertionConditionType ConditionType
        {
            get
            {
                return myConditionType;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    internal sealed class AssertionMethodAttribute : Attribute
    {
    }

    [BaseTypeRequired(typeof(Attribute)), AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    internal sealed class BaseTypeRequiredAttribute : Attribute
    {
        private readonly Type[] myBaseTypes;

        public BaseTypeRequiredAttribute(Type baseType)
        {
            myBaseTypes = new[] { baseType };
        }

        public IEnumerable<Type> BaseTypes
        {
            get
            {
                return myBaseTypes;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Delegate | AttributeTargets.Parameter
        | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method,
        AllowMultiple = false, Inherited = true)]
    public sealed class CanBeNullAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    internal sealed class CannotApplyEqualityOperatorAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Parameter, Inherited = true)]
    internal sealed class InstantHandleAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    internal sealed class InvokerParameterNameAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    internal sealed class LocalizationRequiredAttribute : Attribute
    {
        public LocalizationRequiredAttribute(bool required)
        {
            Required = required;
        }

        public override bool Equals(object obj)
        {
            LocalizationRequiredAttribute attribute = obj as LocalizationRequiredAttribute;
            return ((attribute != null) && (attribute.Required == Required));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Required { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    internal sealed class MeansImplicitUseAttribute : Attribute
    {
        [UsedImplicitly]
        public MeansImplicitUseAttribute()
            : this(ImplicitUseKindFlags.Default)
        {
        }

        [UsedImplicitly]
        public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        [UsedImplicitly]
        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags,
            ImplicitUseTargetFlags targetFlags = ImplicitUseTargetFlags.Default)
        {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }

        [UsedImplicitly]
        public ImplicitUseTargetFlags TargetFlags { get; private set; }

        [UsedImplicitly]
        public ImplicitUseKindFlags UseKindFlags { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Delegate | AttributeTargets.Parameter | AttributeTargets.Field 
        | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class NotNullAttribute : Attribute
    {
    }

    [MeansImplicitUse]
    internal sealed class PublicAPIAttribute : Attribute
    {
        public PublicAPIAttribute()
        {
        }

        public PublicAPIAttribute(string comment)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    internal sealed class PureAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = false, Inherited = true)]
    internal sealed class StringFormatMethodAttribute : Attribute
    {
        private readonly string myFormatParameterName;

        public StringFormatMethodAttribute(string formatParameterName)
        {
            myFormatParameterName = formatParameterName;
        }

        public string FormatParameterName
        {
            get
            {
                return myFormatParameterName;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class TerminatesProgramAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    internal sealed class UsedImplicitlyAttribute : Attribute
    {
        [UsedImplicitly]
        public UsedImplicitlyAttribute()
            : this(ImplicitUseKindFlags.Default)
        {
        }

        [UsedImplicitly]
        public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        [UsedImplicitly]
        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags,
            ImplicitUseTargetFlags targetFlags = ImplicitUseTargetFlags.Default)
        {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }

        [UsedImplicitly]
        public ImplicitUseTargetFlags TargetFlags { get; private set; }

        [UsedImplicitly]
        public ImplicitUseKindFlags UseKindFlags { get; private set; }
    }
}
