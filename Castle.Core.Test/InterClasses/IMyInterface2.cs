// Copyright 2004-2010 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#define FEATURE_SERIALIZATION
using System;

namespace Castle.Core.Test.InterClasses
{
    /// <summary>
	/// Summary description for IMyInterface.
	/// </summary>
	public interface IMyInterface2
	{
		string Name { get; set; }

		bool Started { get; set; }

		int Calc(int x, int y);

		int Calc(int x, int y, int z, float k);
	}

	/// <summary>
	/// Summary description for MyInterfaceImpl.
	/// </summary>
#if FEATURE_SERIALIZATION
	[Serializable]
#endif
	[My("MyInterfaceImpl")]
	public class MyInterfaceImpl : IMyInterface2
	{
	    public virtual string Name { get; set; }

	    public virtual bool Started { get; set; }

	    [My("Calc1")]
		public virtual int Calc(int x, int y)
		{
			return x + y;
		}

		[My("Calc2")]
		public virtual int Calc(int x, int y, int z, float k)
		{
			return x + y + z + (int) k;
		}
	}

	public class MyInterfaceImplX : MyInterfaceImpl
	{
	}


	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
	public class MyAttribute : Attribute
	{
	    public MyAttribute(string name)
		{
			this.name = name;
		}

		public string name { get; }
	}
}
