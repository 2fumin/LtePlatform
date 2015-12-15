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
using Castle.Core.Test.Main;

namespace Castle.Core.Test.DynamicProxy.Explicit
{
#if FEATURE_SERIALIZATION
	[Serializable]
#endif
	public class SimpleInterfaceExplicit : ISimpleInterface
	{
		int ISimpleInterface.Do()
		{
			return 5;
		}

		public int DoVirtual()
		{
			return 7;
		}
	}
}