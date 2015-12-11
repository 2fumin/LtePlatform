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

namespace Castle.DynamicProxy
{
	using System;
	using System.Diagnostics;
	using System.Reflection;

	public abstract class AbstractInvocation : IInvocation
	{
		private readonly IInterceptor[] interceptors;
		private readonly object[] arguments;
		private int currentInterceptorIndex = -1;
		private Type[] genericMethodArguments;
	    protected readonly object proxyObject;

		protected AbstractInvocation(
			object proxy,
			IInterceptor[] interceptors,
			MethodInfo proxiedMethod,
			object[] arguments)
		{
			Debug.Assert(proxiedMethod != null);
			proxyObject = proxy;
			this.interceptors = interceptors;
			this.Method = proxiedMethod;
			this.arguments = arguments;
		}

		public void SetGenericMethodArguments(Type[] arguments)
		{
			genericMethodArguments = arguments;
		}

		public abstract object InvocationTarget { get; }

		public abstract Type TargetType { get; }

		public abstract MethodInfo MethodInvocationTarget { get; }

		public Type[] GenericArguments => genericMethodArguments;

	    public object Proxy => proxyObject;

	    public MethodInfo Method { get; }

	    public MethodInfo GetConcreteMethod()
		{
			return EnsureClosedMethod(Method);
		}

		public MethodInfo GetConcreteMethodInvocationTarget()
		{
			// it is ensured by the InvocationHelper that method will be closed
			var method = MethodInvocationTarget;
			Debug.Assert(method == null || method.IsGenericMethodDefinition == false,
			             "method == null || method.IsGenericMethodDefinition == false");
			return method;
		}

		public object ReturnValue { get; set; }

		public object[] Arguments => arguments;

	    public void SetArgumentValue(int index, object value)
		{
			arguments[index] = value;
		}

		public object GetArgumentValue(int index)
		{
			return arguments[index];
		}

		public void Proceed()
		{
			if (interceptors == null)
				// not yet fully initialized? probably, an intercepted method is called while we are being deserialized
			{
				InvokeMethodOnTarget();
				return;
			}

			currentInterceptorIndex++;
			try
			{
				if (currentInterceptorIndex == interceptors.Length)
				{
					InvokeMethodOnTarget();
				}
				else if (currentInterceptorIndex > interceptors.Length)
				{
					string interceptorsCount;
					if (interceptors.Length > 1)
					{
						interceptorsCount = " each one of " + interceptors.Length + " interceptors";
					}
					else
					{
						interceptorsCount = " interceptor";
					}

					var message = "This is a DynamicProxy2 error: invocation.Proceed() has been called more times than expected." +
					              "This usually signifies a bug in the calling code. Make sure that" + interceptorsCount +
					              " selected for the method '" + Method + "'" +
					              "calls invocation.Proceed() at most once.";
					throw new InvalidOperationException(message);
				}
				else
				{
					interceptors[currentInterceptorIndex].Intercept(this);
				}
			}
			finally
			{
				currentInterceptorIndex--;
			}
		}

		protected abstract void InvokeMethodOnTarget();

		protected void ThrowOnNoTarget()
		{
			// let's try to build as friendly message as we can
		    var interceptorsMessage = interceptors.Length == 0
		        ? "There are no interceptors specified"
		        : "The interceptor attempted to 'Proceed'";

			string methodKindIs;
			string methodKindDescription;
			if (Method.DeclaringType.GetTypeInfo().IsClass && Method.IsAbstract)
			{
				methodKindIs = "is abstract";
				methodKindDescription = "an abstract method";
			}
			else
			{
				methodKindIs = "has no target";
				methodKindDescription = "method without target";
			}

			var message = $"This is a DynamicProxy2 error: {interceptorsMessage} for method '{Method}' which {methodKindIs}. " +
			              $"When calling {methodKindDescription} there is no implementation to 'proceed' to and " +
			              "it is the responsibility of the interceptor to mimic the implementation " +
			              "(set return value, out arguments etc)";

			throw new NotImplementedException(message);
		}

		private MethodInfo EnsureClosedMethod(MethodInfo method)
		{
		    if (!method.ContainsGenericParameters) return method;
		    Debug.Assert(genericMethodArguments != null);
		    return method.GetGenericMethodDefinition().MakeGenericMethod(genericMethodArguments);
		}
	}
}