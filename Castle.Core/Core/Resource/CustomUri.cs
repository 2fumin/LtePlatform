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

namespace Castle.Core.Resource
{
	using System;
	using System.Text;

#if FEATURE_SERIALIZATION
	[Serializable]
#endif
	public sealed class CustomUri
	{
		public static readonly string SchemeDelimiter = "://";
		public static readonly string UriSchemeFile = "file";
		public static readonly string UriSchemeAssembly = "assembly";

	    public CustomUri(string resourceIdentifier)
		{
			if (resourceIdentifier == null)
			{
				throw new ArgumentNullException(nameof(resourceIdentifier));
			}
			if (resourceIdentifier == string.Empty)
			{
				throw new ArgumentException("Empty resource identifier is not allowed", nameof(resourceIdentifier));
			}

			ParseIdentifier(resourceIdentifier);
		}

		public bool IsUnc { get; private set; }

	    public bool IsFile { get; private set; }

	    public bool IsAssembly { get; private set; }

	    public string Scheme { get; private set; }

	    public string Host { get; private set; }

	    public string Path { get; private set; }

	    private void ParseIdentifier(string identifier)
		{
			var comma = identifier.IndexOf(':');

			if (comma == -1 && !(identifier[0] == '\\' && identifier[1] == '\\') && identifier[0] != '/')
			{
				throw new ArgumentException("Invalid Uri: no scheme delimiter found on " + identifier);
			}

			var translateSlashes = true;

			if (identifier[0] == '\\' && identifier[1] == '\\')
			{
				// Unc

				IsUnc = true;
				IsFile = true;
				Scheme = UriSchemeFile;
				translateSlashes = false;
			}
			else if (identifier[comma + 1] == '/' && identifier[comma + 2] == '/')
			{
				// Extract scheme

				Scheme = identifier.Substring(0, comma);

				IsFile = (Scheme == UriSchemeFile);
				IsAssembly = (Scheme == UriSchemeAssembly);

				identifier = identifier.Substring(comma + SchemeDelimiter.Length);
			}
			else
			{
				IsFile = true;
				Scheme = UriSchemeFile;
			}

			var sb = new StringBuilder();
			foreach(var ch in identifier.ToCharArray())
			{
				if (translateSlashes && (ch == '\\' || ch == '/'))
				{
					if (Host == null && !IsFile)
					{
						Host = sb.ToString();
						sb.Length = 0;
					}

					sb.Append('/');
				}
				else
				{
					sb.Append(ch);
				}
			}

#if SILVERLIGHT
			path = sb.ToString();
#else
			Path = Environment.ExpandEnvironmentVariables(sb.ToString());
#endif
		}
	}
}