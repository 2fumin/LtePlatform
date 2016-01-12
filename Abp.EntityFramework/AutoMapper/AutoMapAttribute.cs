using System;

namespace Abp.EntityFramework.AutoMapper
{
    public class AutoMapAttribute : Attribute
    {
        public Type[] TargetTypes { get; private set; }

        internal virtual AutoMapDirection Direction => AutoMapDirection.From | AutoMapDirection.To;

        public AutoMapAttribute(params Type[] targetTypes)
        {
            TargetTypes = targetTypes;
        }
    }
}