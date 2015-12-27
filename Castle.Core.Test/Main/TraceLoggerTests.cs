// Copyright 2004-2009 Castle Project - http://www.castleproject.org/
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
using System.Diagnostics;
using System.Linq;
using System.Text;
using Castle.Core.Logging;
using NUnit.Framework;

#if !SILVERLIGHT
namespace Castle.Core.Test.Main
{
    /// <summary>
	/// Tests the TraceLogger and TraceLoggerFactory classes
	/// </summary>
	[TestFixture]
	public class TraceLoggerTests
#if FEATURE_XUNITNET
		: IDisposable
#endif
	{
#if FEATURE_XUNITNET
		public TraceLoggerTests()
#else
		[SetUp]
		public void Initialize()
#endif
		{
			MyListener.ClearMessages();
		}

#if FEATURE_XUNITNET
		public void Dispose()
#else
		[TearDown]
		public void Cleanup()
#endif
		{
			MyListener.ClearMessages();
		}

        [Test]
        public void Test_Logger_Creater()
        {
            var factory = new TraceLoggerFactory();
            var logger = factory.Create("Castle.Core.Test.Main.TraceLoggerTests", LoggerLevel.Debug);
            Assert.IsNotNull(logger);
        }

        [Test]
        public void Test_New_TraceLogger()
        {
            var logger = new TraceLogger("Castle.Core.Test.Main.TraceLoggerTests", LoggerLevel.Debug);
            Assert.IsNotNull(logger);
            Assert.AreEqual(logger.Name, "Castle.Core.Test.Main.TraceLoggerTests");
            Assert.AreEqual(logger.Level, LoggerLevel.Debug);
        }

        [Test]
        public void Test_TraceSource()
        {
            var source = new TraceSource("Castle.Core.Test.Main.TraceLoggerTests");
            Assert.AreEqual(source.Listeners.Count, 2);
            Assert.AreEqual(source.Listeners[0].Name, "Default");
            Assert.IsTrue(source.Listeners[0] is DefaultTraceListener);
            Assert.AreEqual(source.Listeners[1].Name, "tests");
        }

        [Test]
        public void Test_MyListener()
        {
            var source = new TraceSource("Castle.Core.Test.Main");
            Assert.AreEqual(source.Listeners.Count, 1);
            Assert.AreEqual(source.Listeners[0].Name, "Default");
            Assert.IsTrue(source.Listeners[0] is DefaultTraceListener);
        }

		[Test]
		[Platform(Exclude = "mono", Reason = "Mono has a bug that causes the listeners to not fully work.")]
		public void WritingToLoggerByType()
		{
			var factory = new TraceLoggerFactory();
			var logger = factory.Create(typeof(TraceLoggerTests), LoggerLevel.Debug);
			logger.Debug("this is a tracing message");
            Assert.IsTrue(logger is LevelFilteredLogger);
            Assert.AreEqual((logger as LevelFilteredLogger).Name, "Castle.Core.Test.Main.TraceLoggerTests");

			MyListener.AssertContains("testsrule", "Castle.Core.Test.Main.TraceLoggerTests");
			MyListener.AssertContains("testsrule", "this is a tracing message");
		}

		[Test]
		public void TracingErrorInformation()
		{
			var factory = new TraceLoggerFactory();
			var logger = factory.Create(typeof(TraceLoggerTests), LoggerLevel.Debug);
			try
			{
				try
				{
					var fakearg = "Thisisavalue";
					throw new ArgumentOutOfRangeException("fakearg", fakearg, "Thisisamessage" );
				}
				catch (Exception ex)
				{
					throw new ApplicationException("Inner error is " + ex.Message, ex);
				}
			}
			catch (Exception ex)
			{
				logger.Error("Problem handled", ex);
			}

			MyListener.AssertContains("testsrule", "Castle.Core.Test.Main.TraceLoggerTests");
			MyListener.AssertContains("testsrule", "Problem handled");
			MyListener.AssertContains("testsrule", "ApplicationException");
			MyListener.AssertContains("testsrule", "Inner error is");
			MyListener.AssertContains("testsrule", "ArgumentOutOfRangeException");
			MyListener.AssertContains("testsrule", "fakearg");
			MyListener.AssertContains("testsrule", "Thisisavalue");
			MyListener.AssertContains("testsrule", "Thisisamessage");
		}

		[Test]
		[Platform(Exclude = "mono", Reason = "Mono has a bug that causes the listeners to not fully work.")]
		public void FallUpToShorterSourceName()
		{
			var factory = new TraceLoggerFactory();
			var logger = factory.Create(typeof(Configuration.Xml.XmlConfigurationDeserializer), LoggerLevel.Debug);
			logger.Info("Logging to config namespace");

			MyListener.AssertContains("configrule", "Castle.Core.Configuration.Xml.XmlConfigurationDeserializer");
			MyListener.AssertContains("configrule", "Logging to config namespace");
		}

		[Test]
		[Platform(Exclude = "mono", Reason = "Mono has a bug that causes the listeners to not fully work.")]
		public void FallUpToDefaultSource()
		{
			var factory = new TraceLoggerFactory();
			var logger = factory.Create("System.Xml.XmlDocument", LoggerLevel.Debug);
			logger.Info("Logging to non-configured namespace namespace");
            Assert.IsNotNull(logger);

			MyListener.AssertContains("defaultrule", "System.Xml.XmlDocument");
			MyListener.AssertContains("defaultrule", "Logging to non-configured namespace namespace");
		}
	
        #region in-memory listener class

		/// <summary>
		/// This class captures trace text and records it to StringBuilders in a static dictionary.
		/// Used for the sake of unit testing.
		/// </summary>
		public class MyListener : TraceListener
		{
			public MyListener()
			{
			}

			public MyListener(string initializationData)
			{
				traceName = initializationData;
			}

		    readonly string traceName;

		    public static Dictionary<string, StringBuilder> Traces { get; private set; } = new Dictionary<string, StringBuilder>();

		    StringBuilder GetStringBuilder()
			{
				lock (Traces)
				{
					if (!Traces.ContainsKey(traceName))
						Traces.Add(traceName, new StringBuilder());

					return Traces[traceName];
				}
			}

			public override void Write(string message)
			{
				GetStringBuilder().Append(message);
			}

			public override void WriteLine(string message)
			{
				GetStringBuilder().AppendLine(message);
			}

			public static void AssertContains(string traceName, string expected)
			{
				Assert.IsTrue(Traces.ContainsKey(traceName), "Trace named {0} not found", traceName);
				Assert.IsTrue(Traces[traceName].ToString().Contains(expected), $"Trace text expected to contain '{expected}'");
			}

			public static void ClearMessages()
			{
				Traces = new Dictionary<string, StringBuilder>();
			}
		}
		#endregion
	}
	
}
#endif
