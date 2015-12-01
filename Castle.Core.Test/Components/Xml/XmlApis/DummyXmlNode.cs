﻿// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
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
using System.Xml;
using Castle.Components.DictionaryAdapter.Xml;

#if !SILVERLIGHT // Until support for other platforms is verified
namespace Castle.Core.Test.Components.Xml.XmlApis
{
    internal class DummyXmlNode : IXmlNode
	{
        public DummyXmlNode()
			: this(typeof(object)) { }

		public DummyXmlNode(Type clrType)
		{
			this.ClrType = clrType;
		}

		public Type ClrType { get; }

        public CompiledXPath Path => null;

        public XmlName Name
		{
			get { throw new NotImplementedException(); }
		}

		public XmlName XsiType
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsNil   { get; set; }
		public string Value { get; set; }

		public bool IsReal => true;

        public bool IsElement
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsAttribute
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsRoot
		{
			get { throw new NotImplementedException(); }
		}

		public string Xml
		{
			get { throw new NotImplementedException(); }
		}

		public string GetAttribute(XmlName name)
		{
			throw new NotImplementedException();
		}

		public string LookupPrefix(string namespaceUri)
		{
			throw new NotImplementedException();
		}

		public string LookupNamespaceUri(string prefix)
		{
			throw new NotImplementedException();
		}

		public void DefineNamespace(string prefix, string namespaceUri, bool root)
		{
			throw new NotImplementedException();
		}

		public bool PositionEquals(IXmlNode node)
		{
			throw new NotImplementedException();
		}

		public IXmlNode Save()
		{
			return this;
		}

		public IXmlCursor SelectSelf(Type clrType)
		{
			throw new NotImplementedException();
		}

		public IXmlCursor SelectChildren(IXmlKnownTypeMap knownTypes, IXmlNamespaceSource namespaces, CursorFlags flags)
		{
			throw new NotImplementedException();
		}

		public IXmlIterator SelectSubtree()
		{
			throw new NotImplementedException();
		}

		public IXmlCursor Select(CompiledXPath path, IXmlIncludedTypeMap includedTypes, IXmlNamespaceSource namespaces, CursorFlags flags)
		{
			throw new NotImplementedException();
		}

		public object Evaluate(CompiledXPath path)
		{
			throw new NotImplementedException();
		}

		public void Realize()
		{
			throw new NotImplementedException();
		}

		public event EventHandler Realized
		{
			add    { }
			remove { }
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public XmlReader ReadSubtree()
		{
			throw new NotImplementedException();
		}

		public XmlWriter WriteAttributes()
		{
			throw new NotImplementedException();
		}

		public XmlWriter WriteChildren()
		{
			throw new NotImplementedException();
		}


		public void SetAttribute(XmlName name, string value)
		{
			throw new NotImplementedException();
		}

		public IRealizable<T> AsRealizable<T>()
		{
			return null; // Not realizable
		}


		public IXmlNode Parent
		{
			get { throw new NotImplementedException(); }
		}

		public IXmlNamespaceSource Namespaces
		{
			get { throw new NotImplementedException(); }
		}

		public object UnderlyingObject
		{
			get { throw new NotImplementedException(); }
		}

		public bool UnderlyingPositionEquals(IXmlNode node)
		{
			throw new NotImplementedException();
		}
	}
}
#endif
