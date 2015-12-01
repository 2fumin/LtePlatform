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
using System.Collections.Generic;
using Castle.Core.Test.Mixins;
using Castle.DynamicProxy;
using NUnit.Framework;

namespace Castle.Core.Test.Main
{
    [TestFixture]
	public class MixinDataTestCase
	{
		private SimpleMixin simpleMixin;
		private OtherMixin otherMixin;
		private ComplexMixin complexMixin;

#if FEATURE_XUNITNET
		public MixinDataTestCase()
#else
		[SetUp]
		public void SetUp()
#endif
		{
			simpleMixin = new SimpleMixin();
			otherMixin = new OtherMixin();
			complexMixin = new ComplexMixin();
		}

		[Test]
		public void Mixins()
		{
			var mixinData = new MixinData(new object[] {simpleMixin});
			var mixins = new List<object>(mixinData.Mixins);
			Assert.AreEqual(1, mixins.Count);
			Assert.AreSame(simpleMixin, mixins[0]);
		}

		[Test]
		public void ContainsMixinWithInterface()
		{
			var mixinData = new MixinData(new object[] { simpleMixin });
			Assert.IsTrue(mixinData.ContainsMixin(typeof(ISimpleMixin)));
			Assert.IsFalse(mixinData.ContainsMixin(typeof(IOtherMixin)));
		}

		[Test]
		public void MixinsNotImplementingInterfacesAreIgnored()
		{
			var mixinData = new MixinData(new[] {new object()});
			var mixins = new List<object>(mixinData.Mixins);
			Assert.AreEqual(0, mixins.Count);
		}

		[Test]
		public void MixinsAreSortedByInterface()
		{
			var mixinData1 = new MixinData(new object[] {simpleMixin, otherMixin});
			var mixins1 = new List<object>(mixinData1.Mixins);
			Assert.AreEqual(2, mixins1.Count);
			Assert.AreSame(otherMixin, mixins1[0]);
			Assert.AreSame(simpleMixin, mixins1[1]);

			var mixinData2 = new MixinData(new object[] {otherMixin, simpleMixin});
			var mixins2 = new List<object>(mixinData2.Mixins);
			Assert.AreEqual(2, mixins2.Count);
			Assert.AreSame(otherMixin, mixins2[0]);
			Assert.AreSame(simpleMixin, mixins2[1]);
		}

		[Test]
		public void MixinInterfaces()
		{
			var mixinData = new MixinData(new object[] { simpleMixin });
			var mixinInterfaces = new List<Type>(mixinData.MixinInterfaces);
			Assert.AreEqual(1, mixinInterfaces.Count);
			Assert.AreSame(mixinInterfaces[0], typeof (ISimpleMixin));
		}

		[Test]
		public void MixinInterfaces_SortedLikeMixins()
		{
			var mixinData1 = new MixinData(new object[] { simpleMixin, otherMixin });
			var mixinInterfaces1 = new List<Type>(mixinData1.MixinInterfaces);
			Assert.AreEqual(2, mixinInterfaces1.Count);
			Assert.AreSame(typeof(IOtherMixin), mixinInterfaces1[0]);
			Assert.AreSame(typeof(ISimpleMixin), mixinInterfaces1[1]);

			var mixinData2 = new MixinData(new object[] { otherMixin, simpleMixin });
			var mixinInterfaces2 = new List<Type>(mixinData2.MixinInterfaces);
			Assert.AreEqual(2, mixinInterfaces2.Count);
			Assert.AreSame(typeof (IOtherMixin), mixinInterfaces2[0]);
			Assert.AreSame(typeof (ISimpleMixin), mixinInterfaces2[1]);
		}

		[Test]
		public void GetMixinPosition()
		{
			var mixinData = new MixinData(new object[] { simpleMixin });
			Assert.AreEqual(0, mixinData.GetMixinPosition(typeof(ISimpleMixin)));
		}

		[Test]
		public void GetMixinPosition_MatchesMixinInstances()
		{
			var mixinData1 = new MixinData(new object[] {simpleMixin, otherMixin});
			Assert.AreEqual(0, mixinData1.GetMixinPosition(typeof(IOtherMixin)));
			Assert.AreEqual(1, mixinData1.GetMixinPosition(typeof(ISimpleMixin)));

			var mixinData2 = new MixinData(new object[] {otherMixin, simpleMixin});
			Assert.AreEqual(0, mixinData2.GetMixinPosition(typeof(IOtherMixin)));
			Assert.AreEqual(1, mixinData2.GetMixinPosition(typeof(ISimpleMixin)));
		}

		[Test]
		public void GetMixinPosition_MatchesMixinInstances_WithMultipleInterfacesPerMixin()
		{
			var mixinData = new MixinData(new object[] { complexMixin, simpleMixin });
			Assert.AreEqual(0, mixinData.GetMixinPosition(typeof(IFirst)));
			Assert.AreEqual(1, mixinData.GetMixinPosition(typeof(ISecond)));
			Assert.AreEqual(2, mixinData.GetMixinPosition(typeof(ISimpleMixin)));
			Assert.AreEqual(3, mixinData.GetMixinPosition(typeof(IThird)));

			var mixins = new List<object>(mixinData.Mixins);
			Assert.AreSame(complexMixin, mixins[0]);
			Assert.AreSame(complexMixin, mixins[1]);
			Assert.AreSame(simpleMixin, mixins[2]);
			Assert.AreSame(complexMixin, mixins[3]);
		}

		[Test]
		public void Equals_True_WithDifferentOrder()
		{
			var mixinData1 = new MixinData(new object[] {simpleMixin, otherMixin});
			var mixinData2 = new MixinData(new object[] {otherMixin, simpleMixin});
			Assert.AreEqual(mixinData1, mixinData2);
		}

		[Test]
		public void Equals_True_WithDifferentInstances()
		{
			var mixinData1 = new MixinData(new object[] {simpleMixin, otherMixin});
			var mixinData2 = new MixinData(new object[] {new SimpleMixin(), new OtherMixin()});
			Assert.AreEqual(mixinData1, mixinData2);
		}

		[Test]
		public void Equals_False_WithDifferentInstances()
		{
			var mixinData1 = new MixinData(new object[] {simpleMixin, otherMixin});
			var mixinData2 = new MixinData(new object[] {simpleMixin, complexMixin});
			Assert.AreNotEqual(mixinData1, mixinData2);
		}

		[Test]
		public void Equals_False_WithInstanceCount()
		{
			var mixinData1 = new MixinData(new object[] {otherMixin});
			var mixinData2 = new MixinData(new object[] {otherMixin, simpleMixin});
			Assert.AreNotEqual(mixinData1, mixinData2);
		}

		[Test]
		public void GetHashCode_Equal_WithDifferentOrder()
		{
			var mixinData1 = new MixinData(new object[] {simpleMixin, otherMixin});
			var mixinData2 = new MixinData(new object[] {otherMixin, simpleMixin});
			Assert.AreEqual(mixinData1.GetHashCode(), mixinData2.GetHashCode());
		}

		[Test]
		public void GetHashCode_Equal_WithDifferentInstances()
		{
			var mixinData1 = new MixinData(new object[] {simpleMixin, otherMixin});
			var mixinData2 = new MixinData(new object[] {new SimpleMixin(), new OtherMixin()});
			Assert.AreEqual(mixinData1.GetHashCode(), mixinData2.GetHashCode());
		}

		[Test]
		public void TwoMixinsWithSameInterfaces()
		{
			var mixin1 = new SimpleMixin();
			var mixin2 = new OtherMixinImplementingISimpleMixin();

			Assert.Throws<ArgumentException>(() =>
				new MixinData(new object[] { mixin1, mixin2 })
			);
		}
	}
}