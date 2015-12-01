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
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Castle.DynamicProxy;

namespace Castle.Core.Test.Interceptors
{
	public class ProceedNTimesInterceptor : IInterceptor
	{
		private readonly int retries;

		public ProceedNTimesInterceptor(int retries)
		{
			this.retries = retries;
		}

		public void Intercept(IInvocation invocation)
		{
			for (var i = 0; i < retries; i++)
			{
				try
				{
					invocation.Proceed();
				}
				catch
				{
					// gulp
				}
			}
		}
	}
}