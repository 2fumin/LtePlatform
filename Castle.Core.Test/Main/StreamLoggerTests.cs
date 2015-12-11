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
using System.IO;
using System.Text.RegularExpressions;
using Castle.Core.Logging;
using NUnit.Framework;

namespace Castle.Core.Test.Main
{
    [TestFixture]
	public class StreamLoggerTests
	{
		private const string Name = "Test";

		private StreamLogger logger;
		private MemoryStream stream;

#if FEATURE_XUNITNET
		public StreamLoggerTests()
#else
		[SetUp]
		public void SetUp()
#endif
		{
			stream = new MemoryStream();

		    logger = new StreamLogger(Name, stream) {Level = LoggerLevel.Debug};
		}

		[Test]
		public void Debug()
		{
			var message = "Debug message";
			var level = LoggerLevel.Debug;
			
			logger.Debug(message);

			ValidateCall(level, message, null);
		}

		[Test]
		public void DebugWithException()
		{
			var message = "Debug message 2";
			var level = LoggerLevel.Debug;
			var exception = new Exception();
			
			logger.Debug(message, exception);

			ValidateCall(level, message, exception);
		}

		[Test]
		public void Info()
		{
			const string message = "Info message";
			var level = LoggerLevel.Info;
			
			logger.Info(message);

			ValidateCall(level, message, null);
		}

		[Test]
		public void InfoWithException()
		{
			var message = "Info message 2";
			var level = LoggerLevel.Info;
			var exception = new Exception();
			
			logger.Info(message, exception);

			ValidateCall(level, message, exception);
		}

		[Test]
		public void Warn()
		{
			const string message = "Warn message";
			const LoggerLevel level = LoggerLevel.Warn;

		    logger.Warn(message);

			ValidateCall(level, message, null);
		}

		[Test]
		public void WarnWithException()
		{
			var message = "Warn message 2";
			var level = LoggerLevel.Warn;
			var exception = new Exception();
			
			logger.Warn(message, exception);

			ValidateCall(level, message, exception);
		}

		[Test]
		public void Error()
		{
			var message = "Error message";
			var level = LoggerLevel.Error;
			Exception exception = null;
			
			logger.Error(message);

			ValidateCall(level, message, exception);
		}

		[Test]
		public void ErrorWithException()
		{
			var message = "Error message 2";
			var level = LoggerLevel.Error;
			var exception = new Exception();
			
			logger.Error(message, exception);

			ValidateCall(level, message, exception);
		}

		[Test]
		public void FatalError()
		{
			var message = "FatalError message";
			var level = LoggerLevel.Fatal;
			Exception exception = null;
			
			logger.Fatal(message);

			ValidateCall(level, message, exception);
		}

		[Test]
		public void FatalErrorWithException()
		{
			var message = "FatalError message 2";
			var level = LoggerLevel.Fatal;
			var exception = new Exception();
			
			logger.Fatal(message, exception);

			ValidateCall(level, message, exception);
		}

		private void ValidateCall(LoggerLevel level, string expectedMessage, Exception expectedException)
		{
			stream.Seek(0, SeekOrigin.Begin);
			
			var reader = new StreamReader(stream);
			var line = reader.ReadLine();

			var match = Regex.Match(line, @"^\[(?<level>[^]]+)\] '(?<name>[^']+)' (?<message>.*)$");

			Assert.IsTrue(match.Success, "StreamLogger.Log did not match the format");
			Assert.AreEqual(Name, match.Groups["name"].Value, "StreamLogger.Log did not write the correct Name");
			Assert.AreEqual(level.ToString(), match.Groups["level"].Value, "StreamLogger.Log did not write the correct Level");
			Assert.AreEqual(expectedMessage, match.Groups["message"].Value, "StreamLogger.Log did not write the correct Message");

			line = reader.ReadLine();
			
			if (expectedException == null)
			{
				Assert.IsNull(line);
			}
			else
			{
				match = Regex.Match(line, @"^\[(?<level>[^]]+)\] '(?<name>[^']+)' (?<type>[^:]+): (?<message>.*)$");

				Assert.IsTrue(match.Success, "StreamLogger.Log did not match the format");
				Assert.AreEqual(Name, match.Groups["name"].Value, "StreamLogger.Log did not write the correct Name");
				Assert.AreEqual(level.ToString(), match.Groups["level"].Value, "StreamLogger.Log did not write the correct Level");
				Assert.AreEqual(expectedException.GetType().FullName, match.Groups["type"].Value, "StreamLogger.Log did not write the correct Exception Type");
				// Assert.AreEqual(expectedException.Message, match.Groups["message"].Value, "StreamLogger.Log did not write the correct Exception Message");
			}
		}
	}
}
