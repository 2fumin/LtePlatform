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

using System.Linq;

namespace Castle.Core.Resource
{
	using System;
	using System.Globalization;
	using System.IO;
	using System.Reflection;

	public class AssemblyResource : AbstractStreamResource
	{
		private string assemblyName;
		private string resourcePath;
		private string basePath;

		public AssemblyResource(CustomUri resource)
		{
			CreateStream = () => CreateResourceFromUri(resource, null);
		}

		public AssemblyResource(CustomUri resource, string basePath)
		{
			CreateStream = () => CreateResourceFromUri(resource, basePath);
		}

		public AssemblyResource(string resource)
		{
			CreateStream = () => CreateResourceFromPath(resource, basePath);
		}

		public override IResource CreateRelative(string relativePath)
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "AssemblyResource: [{0}] [{1}]", assemblyName, resourcePath);
		}

		private Stream CreateResourceFromPath(string resource, string path)
		{
			if (!resource.StartsWith("assembly" + CustomUri.SchemeDelimiter, StringComparison.CurrentCulture))
			{
				resource = "assembly" + CustomUri.SchemeDelimiter + resource;
			}

			return CreateResourceFromUri(new CustomUri(resource), path);
		}

		private Stream CreateResourceFromUri(CustomUri resourcex, string path)
		{
			if (resourcex == null) throw new ArgumentNullException(nameof(resourcex));

			assemblyName = resourcex.Host;
			resourcePath = ConvertToResourceName(assemblyName, resourcex.Path);

			var assembly = ObtainAssembly(assemblyName);

			var names = assembly.GetManifestResourceNames();

			var nameFound = GetNameFound(names);

			if (nameFound == null)
			{
				resourcePath = resourcex.Path.Replace('/', '.').Substring(1);
				nameFound = GetNameFound(names);
			}

			if (nameFound == null)
			{
				var message = string.Format(CultureInfo.InvariantCulture, "The assembly resource {0} could not be located", resourcePath);
				throw new ResourceException(message);
			}

			basePath = ConvertToPath(resourcePath);

			return assembly.GetManifestResourceStream(nameFound);
		}

		private string GetNameFound(string[] names)
		{
		    return names.FirstOrDefault(name => string.Compare(resourcePath, name, StringComparison.OrdinalIgnoreCase) == 0);
		}

	    private string ConvertToResourceName(string assembly, string resource)
		{
			assembly = GetSimpleName(assembly);
			// TODO: use path for relative name construction
			return string.Format(CultureInfo.CurrentCulture, "{0}{1}", assembly, resource.Replace('/', '.'));
		}

		private string GetSimpleName(string assembly)
		{
			var indexOfComma = assembly.IndexOf(',');
			if(indexOfComma<0)
			{
				return assembly;
			}
			return assembly.Substring(0, indexOfComma);
		}

		private string ConvertToPath(string resource)
		{
			var path = resource.Replace('.', '/');
			if (path[0] != '/')
			{
				path = string.Format(CultureInfo.CurrentCulture, "/{0}", path);
			}
			return path;
		}

		private static Assembly ObtainAssembly(string assemblyName)
		{
			try
			{
				return Assembly.Load(assemblyName);
			}
			catch(Exception ex)
			{
				var message = string.Format(CultureInfo.InvariantCulture, "The assembly {0} could not be loaded", assemblyName);
				throw new ResourceException(message, ex);
			}
		}
	}
}
