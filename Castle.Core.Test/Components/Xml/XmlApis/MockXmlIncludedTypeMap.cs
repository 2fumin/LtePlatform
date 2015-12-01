// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.f
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Castle.Components.DictionaryAdapter.Xml;

#if !SILVERLIGHT // Until support for other platforms is verified
namespace Castle.Core.Test.Components.Xml.XmlApis
{
    public class MockXmlIncludedTypeMap : IXmlIncludedTypeMap, IXmlIncludedType
	{
        public MockXmlIncludedTypeMap()
		{
			InnerSet = new XmlIncludedTypeSet();
		}

		public XmlIncludedTypeSet InnerSet { get; }
        public Type DefaultClrType { get; set; }

		public  IXmlIncludedType Default => this;
        XmlName IXmlIncludedType.XsiType => XmlName.Empty;
        Type    IXmlIncludedType.ClrType => DefaultClrType;

        public bool TryGet(XmlName xsiType, out IXmlIncludedType includedType)
		{
			return (xsiType == Default.XsiType)
				? Try.Success(out includedType, Default)
				: InnerSet.TryGet(xsiType, out includedType);
		}

		public bool TryGet(Type clrType, out IXmlIncludedType includedType)
		{
			return (clrType == Default.ClrType)
				? Try.Success(out includedType, Default)
				: InnerSet.TryGet(clrType, out includedType);
		}
	}
}
#endif
