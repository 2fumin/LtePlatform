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

using System;

namespace Castle.Core.Test.DynamicProxy.Classes
{
#if FEATURE_SERIALIZATION
	[Serializable]
#endif
	public class ClassOverridingEqualsAndGetHashCode
	{
	    public virtual Guid Id { get; set; }

	    public virtual string Name { get; set; }

	    public virtual bool Equals(ClassOverridingEqualsAndGetHashCode other)
	    {
	        // use this pattern to compare value members

	        // use this pattern to compare reference members
			// if (!Object.Equals(Id, other.Id)) return false;
	        return other != null && Id.Equals(other.Id);
	    }

	    public override bool Equals(object obj)
		{
		    if (!(obj is ClassOverridingEqualsAndGetHashCode))
				return false;

			// safe because of the GetType check
			return Equals((ClassOverridingEqualsAndGetHashCode) obj);
		}

		public override int GetHashCode()
		{
			var hash = 7;

			hash = 31*hash + Id.GetHashCode();

			return hash;
		}
	}
}