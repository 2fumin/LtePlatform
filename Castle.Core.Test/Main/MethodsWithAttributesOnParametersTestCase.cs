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
using Castle.Core.Test.DynamicProxy.Classes;
using Castle.Core.Test.Interceptors;
using NUnit.Framework;

namespace Castle.Core.Test.Main
{
    [TestFixture]
	public class MethodsWithAttributesOnParametersTestCase : BasePEVerifyTestCase
	{
		[Test]
		public void ParametersAreCopiedToProxiedObject()
		{
			var requiredObj = (ClassWithAttributesOnMethodParameters)generator.CreateClassProxy(
				typeof (ClassWithAttributesOnMethodParameters), new RequiredParamInterceptor());

			var ex = Assert.Throws<ArgumentException>(() =>
				requiredObj.MethodOne(-1)
			);
			Assert.AreEqual("No default value for argument", ex.Message);
		}

        [Test]
        public void CanGetParameterAttributeFromProxiedObject()
        {
            var requiredObj = (ClassWithAttributesOnMethodParameters)
                generator.CreateClassProxy(
                    typeof (ClassWithAttributesOnMethodParameters),
                    new RequiredParamInterceptor());

            requiredObj.MethodTwo(null);
        }
	}
}