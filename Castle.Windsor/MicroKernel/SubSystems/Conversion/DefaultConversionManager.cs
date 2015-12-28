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

using System.Linq;

namespace Castle.MicroKernel.SubSystems.Conversion
{
	using System;
	using System.Collections.Generic;
	using System.Threading;

	using Castle.Core;
	using Castle.Core.Configuration;
	using Castle.MicroKernel.Context;

	/// <summary>
	///   Composition of all available conversion managers
	/// </summary>
	[Serializable]
	public class DefaultConversionManager : AbstractSubSystem, IConversionManager, ITypeConverterContext
	{
#if (!SILVERLIGHT)
		private static readonly LocalDataStoreSlot slot = Thread.AllocateDataSlot();
#else
		[ThreadStatic]
		private static Stack<Pair<ComponentModel,CreationContext>> slot;
#endif
		private readonly IList<ITypeConverter> converters = new List<ITypeConverter>();
		private readonly IList<ITypeConverter> standAloneConverters = new List<ITypeConverter>();

		public DefaultConversionManager()
		{
			InitDefaultConverters();
		}

		protected virtual void InitDefaultConverters()
		{
			Add(new PrimitiveConverter());
			Add(new TimeSpanConverter());
			Add(new TypeNameConverter(new TypeNameParser()));
			Add(new EnumConverter());
			Add(new ListConverter());
			Add(new DictionaryConverter());
			Add(new GenericDictionaryConverter());
			Add(new GenericListConverter());
			Add(new ArrayConverter());
			Add(new ComponentConverter());
			Add(new AttributeAwareConverter());
#if (SILVERLIGHT)
			Add(new NullableConverter(this));
#endif
			Add(new ComponentModelConverter());
		}

		public void Add(ITypeConverter converter)
		{
			converter.Context = this;

			converters.Add(converter);

			if (!(converter is IKernelDependentConverter))
			{
				standAloneConverters.Add(converter);
			}
		}

		public ITypeConverterContext Context
		{
			get { return this; }
			set { throw new NotImplementedException(); }
		}

		public bool CanHandleType(Type type)
		{
		    return converters.Any(converter => converter.CanHandleType(type));
		}

	    public bool CanHandleType(Type type, IConfiguration configuration)
		{
		    return converters.Any(converter => converter.CanHandleType(type, configuration));
		}

	    public object PerformConversion(string value, Type targetType)
		{
			foreach (var converter in converters.Where(converter => converter.CanHandleType(targetType)))
			{
			    return converter.PerformConversion(value, targetType);
			}

			var message = $"No converter registered to handle the type {targetType.FullName}";

			throw new ConverterException(message);
		}

		public object PerformConversion(IConfiguration configuration, Type targetType)
		{
			foreach (var converter in converters.Where(converter => converter.CanHandleType(targetType, configuration)))
			{
			    return converter.PerformConversion(configuration, targetType);
			}

			var message = $"No converter registered to handle the type {targetType.FullName}";

			throw new ConverterException(message);
		}

		public TTarget PerformConversion<TTarget>(string value)
		{
			return (TTarget)PerformConversion(value, typeof(TTarget));
		}

		public TTarget PerformConversion<TTarget>(IConfiguration configuration)
		{
			return (TTarget)PerformConversion(configuration, typeof(TTarget));
		}

		IKernelInternal ITypeConverterContext.Kernel => Kernel;

	    public void Push(ComponentModel model, CreationContext context)
		{
			CurrentStack.Push(new Pair<ComponentModel, CreationContext>(model, context));
		}

		public void Pop()
		{
			CurrentStack.Pop();
		}

		public ComponentModel CurrentModel => CurrentStack.Count == 0 ? null : CurrentStack.Peek().First;

	    public CreationContext CurrentCreationContext => CurrentStack.Count == 0 ? null : CurrentStack.Peek().Second;

	    public ITypeConverter Composition => this;

	    private Stack<Pair<ComponentModel, CreationContext>> CurrentStack
		{
			get
			{
#if (SILVERLIGHT)
				if(slot == null)
				{
					slot = new Stack<Pair<ComponentModel,CreationContext>>();
				}

				return slot;
#else
				var stack = (Stack<Pair<ComponentModel, CreationContext>>)Thread.GetData(slot);
			    if (stack != null) return stack;
			    stack = new Stack<Pair<ComponentModel, CreationContext>>();
			    Thread.SetData(slot, stack);

			    return stack;

#endif
			}
		}
	}
}