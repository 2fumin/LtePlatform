using System;

namespace Abp.EntityFramework.AutoMapper
{
    public class AutoMapToAttribute : AutoMapAttribute
    {
        internal override AutoMapDirection Direction => AutoMapDirection.To;

        public AutoMapToAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
    }
}