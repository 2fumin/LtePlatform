using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Loader
{
    internal class DefaultLoader
    {
        private readonly Func<Type, object> _activator;
        private readonly Func<string, IList<string>, Action<IAppBuilder>> _next;
        private readonly IEnumerable<Assembly> _referencedAssemblies;

        public DefaultLoader() : this(null, null, null)
        {
        }

        public DefaultLoader(IEnumerable<Assembly> referencedAssemblies) : this(null, null, referencedAssemblies)
        {
        }

        public DefaultLoader(Func<string, IList<string>, Action<IAppBuilder>> next, Func<Type, object> activator) : this(next, activator, null)
        {
        }

        public DefaultLoader(Func<string, IList<string>, Action<IAppBuilder>> next, Func<Type, object> activator = null, IEnumerable<Assembly> referencedAssemblies = null)
        {
            _next = next ?? NullLoader.Instance;
            _activator = activator ?? Activator.CreateInstance;
            _referencedAssemblies = referencedAssemblies ?? new AssemblyDirScanner();
        }

        private static void CheckForStartupType(string startupName, Assembly assembly, ref Type matchedType, ref bool conflict, IList<string> errors)
        {
            var type = assembly.GetType(startupName, false);
            if (type != null)
            {
                if (matchedType != null)
                {
                    conflict = true;
                    errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.Exception_StartupTypeConflict,
                        matchedType.AssemblyQualifiedName, type.AssemblyQualifiedName));
                }
                else
                {
                    matchedType = type;
                }
            }
        }

        public static IEnumerable<string> DotByDot(string text)
        {
            if (text == null)
            {
                goto Label_00B1;
            }
            text = text.Trim('.');
            var length = text.Length;
            Label_PostSwitchInIterator:
            if (length > 0)
            {
                yield return text.Substring(0, length);
                length = text.LastIndexOf('.', length - 1, length - 1);
                goto Label_PostSwitchInIterator;
            }
            Label_00B1:;
        }

        private Tuple<Type, string> GetDefaultConfiguration(string friendlyName, IList<string> errors)
        {
            friendlyName = friendlyName ?? string.Empty;
            var conflict = false;
            var tuple = SearchForStartupAttribute(friendlyName, errors, ref conflict);
            if (((tuple == null) && !conflict) && string.IsNullOrEmpty(friendlyName))
            {
                tuple = SearchForStartupConvention(errors);
            }
            return tuple;
        }

        private Tuple<Type, string> GetTypeAndMethodNameForConfigurationString(string configuration, IList<string> errors)
        {
            var tuple = HuntForAssembly(configuration, errors);
            if (tuple != null)
            {
                var text = tuple.Item1;
                var assembly = tuple.Item2;
                foreach (var str2 in DotByDot(text).Take(2))
                {
                    var type = assembly.GetType(str2, false);
                    if (type == null)
                    {
                        errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.ClassNotFoundInAssembly, configuration, str2, assembly.FullName));
                    }
                    else
                    {
                        return new Tuple<Type, string>(type, (str2 == text) ? null : text.Substring(str2.Length + 1));
                    }
                }
            }
            return null;
        }

        private Tuple<string, Assembly> HuntForAssembly(string configuration, IList<string> errors)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            var index = configuration.IndexOf(',');
            if (index >= 0)
            {
                var str = DotByDot(configuration.Substring(0, index)).FirstOrDefault();
                var assemblyName = configuration.Substring(index + 1).Trim();
                var assembly = TryAssemblyLoad(assemblyName);
                if (assembly != null) return Tuple.Create(str, assembly);
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.AssemblyNotFound, configuration, assemblyName));
                return null;
            }
            foreach (
                var assembly2 in
                    _referencedAssemblies.Where(
                        assembly2 => DotByDot(configuration).Take(2).Any(str3 => assembly2.GetType(str3, false) != null))
                )
            {
                return Tuple.Create(configuration, assembly2);
            }
            errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.TypeOrMethodNotFound, configuration));
            return null;
        }

        public Action<IAppBuilder> Load(string startupName, IList<string> errorDetails)
        {
            return (LoadImplementation(startupName, errorDetails) ?? _next(startupName, errorDetails));
        }

        private Action<IAppBuilder> LoadImplementation(string startupName, IList<string> errorDetails)
        {
            Tuple<Type, string> defaultConfiguration = null;
            startupName = startupName ?? string.Empty;
            if (!startupName.Contains(','))
            {
                defaultConfiguration = GetDefaultConfiguration(startupName, errorDetails);
            }
            if ((defaultConfiguration == null) && !string.IsNullOrWhiteSpace(startupName))
            {
                defaultConfiguration = GetTypeAndMethodNameForConfigurationString(startupName, errorDetails);
            }
            if (defaultConfiguration == null)
            {
                return null;
            }
            var type = defaultConfiguration.Item1;
            var methodName = !string.IsNullOrWhiteSpace(defaultConfiguration.Item2)
                ? defaultConfiguration.Item2
                : Constants.Configuration;
            var startup = MakeDelegate(type, methodName, errorDetails);
            if (startup == null)
            {
                return null;
            }
            return delegate(IAppBuilder builder)
            {
                object obj2;
                if (builder == null)
                {
                    throw new ArgumentNullException(nameof(builder));
                }
                if (
                    !builder.Properties.TryGetValue(Constants.HostAppName,
                        out obj2) ||
                    string.IsNullOrWhiteSpace(Convert.ToString(obj2,
                        CultureInfo.InvariantCulture)))
                {
                    builder.Properties[Constants.HostAppName] = type.FullName;
                }
                startup(builder);
            };
        }

        private Action<IAppBuilder> MakeDelegate(Type type, string methodName, IList<string> errors)
        {
            MethodInfo info = null;
            var methods = type.GetMethods();
            for (var i = 0; i < methods.Length; i++)
            {
                var methodInfo = methods[i];
                if (methodInfo.Name.Equals(methodName))
                {
                    if (Matches(methodInfo, false, typeof(IAppBuilder)))
                    {
                        var instance = methodInfo.IsStatic ? null : _activator(type);
                        return delegate (IAppBuilder builder) {
                            methodInfo.Invoke(instance, new object[] { builder });
                        };
                    }
                    if (Matches(methodInfo, true, typeof(IDictionary<string, object>)))
                    {
                        var instance = methodInfo.IsStatic ? null : _activator(type);
                        return delegate (IAppBuilder builder) {
                            builder.Use(methodInfo.Invoke(instance, new object[] { builder.Properties }));
                        };
                    }
                    if (Matches(methodInfo, true))
                    {
                        var instance = methodInfo.IsStatic ? null : _activator(type);
                        return delegate (IAppBuilder builder) {
                            builder.Use(methodInfo.Invoke(instance, new object[0]));
                        };
                    }
                    info = info ?? methodInfo;
                }
            }
            errors.Add(info == null
                ? string.Format(CultureInfo.CurrentCulture, Resources.MethodNotFoundInClass, methodName,
                    type.AssemblyQualifiedName)
                : string.Format(CultureInfo.CurrentCulture, Resources.UnexpectedMethodSignature, methodName,
                    type.AssemblyQualifiedName));
            return null;
        }

        private static bool Matches(MethodInfo methodInfo, bool hasReturnValue, params Type[] parameterTypes)
        {
            var flag = methodInfo.ReturnType != typeof(void);
            if (hasReturnValue != flag)
            {
                return false;
            }
            var parameters = methodInfo.GetParameters();
            return parameters.Length == parameterTypes.Length &&
                   parameters.Zip(parameterTypes, (pi, t) => (pi.ParameterType == t)).All(b => b);
        }

        private Tuple<Type, string> SearchForStartupAttribute(string friendlyName, IList<string> errors, ref bool conflict)
        {
            friendlyName = friendlyName ?? string.Empty;
            var flag = false;
            Tuple<Type, string> tuple = null;
            Assembly assembly = null;
            foreach (var assembly2 in _referencedAssemblies)
            {
                object[] customAttributes;
                try
                {
                    customAttributes = assembly2.GetCustomAttributes(false);
                }
                catch (CustomAttributeFormatException)
                {
                    continue;
                }
                foreach (var obj2 in from attribute in customAttributes
                                        where attribute.GetType().Name.Equals(Constants.OwinStartupAttribute, StringComparison.Ordinal)
                                        select attribute)
                {
                    var type = obj2.GetType();
                    flag = true;
                    var property = type.GetProperty(Constants.StartupType, typeof(Type));
                    if (property == null)
                    {
                        errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.StartupTypePropertyMissing,
                            type.AssemblyQualifiedName, assembly2.FullName));
                    }
                    else
                    {
                        var type2 = property.GetValue(obj2, null) as Type;
                        if (type2 == null)
                        {
                            errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.StartupTypePropertyEmpty,
                                assembly2.FullName));
                        }
                        else
                        {
                            var b = string.Empty;
                            var info2 = type.GetProperty(Constants.FriendlyName, typeof (string));
                            if (info2 != null)
                            {
                                b = (info2.GetValue(obj2, null) as string) ?? string.Empty;
                            }
                            if (!string.Equals(friendlyName, b, StringComparison.OrdinalIgnoreCase))
                            {
                                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.FriendlyNameMismatch, b,
                                    friendlyName, assembly2.FullName));
                            }
                            else
                            {
                                var str2 = string.Empty;
                                var info3 = type.GetProperty(Constants.MethodName, typeof (string));
                                if (info3 != null)
                                {
                                    str2 = (info3.GetValue(obj2, null) as string) ?? string.Empty;
                                }
                                if (tuple != null)
                                {
                                    conflict = true;
                                    errors.Add(string.Format(CultureInfo.CurrentCulture,
                                        Resources.Exception_AttributeNameConflict, assembly.GetName().Name, tuple.Item1,
                                        assembly2.GetName().Name, type2, friendlyName));
                                }
                                else
                                {
                                    tuple = new Tuple<Type, string>(type2, str2);
                                    assembly = assembly2;
                                }
                            }
                        }
                    }
                }
            }
            if (!flag)
            {
                errors.Add(Resources.NoOwinStartupAttribute);
            }
            return conflict ? null : tuple;
        }

        private Tuple<Type, string> SearchForStartupConvention(IList<string> errors)
        {
            Type matchedType = null;
            var conflict = false;
            foreach (var assembly in _referencedAssemblies)
            {
                CheckForStartupType(Constants.Startup, assembly, ref matchedType, ref conflict, errors);
                CheckForStartupType(assembly.GetName().Name + ".Startup", assembly, ref matchedType, ref conflict, errors);
            }
            if (matchedType == null)
            {
                errors.Add(Resources.NoAssemblyWithStartupClass);
                return null;
            }
            if (conflict)
            {
                return null;
            }
            if (!matchedType.GetMethods().Any(methodInfo => methodInfo.Name.Equals(Constants.Configuration)))
            {
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.MethodNotFoundInClass,
                    Constants.Configuration, matchedType.AssemblyQualifiedName));
                return null;
            }
            return new Tuple<Type, string>(matchedType, Constants.Configuration);
        }

        private static Assembly TryAssemblyLoad(string assemblyName)
        {
            try
            {
                return Assembly.Load(assemblyName);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (FileLoadException)
            {
                return null;
            }
        }

        private class AssemblyDirScanner : IEnumerable<Assembly>
        {
            public IEnumerator<Assembly> GetEnumerator()
            {
                var setupInformation = AppDomain.CurrentDomain.SetupInformation;
                IEnumerable<string> first = new string[0];
                if ((setupInformation.PrivateBinPathProbe == null) ||
                    string.IsNullOrWhiteSpace(setupInformation.PrivateBinPath))
                {
                    first = first.Concat(new[] {string.Empty});
                }
                if (!string.IsNullOrWhiteSpace(setupInformation.PrivateBinPath))
                {
                    first = first.Concat(setupInformation.PrivateBinPath.Split(';'));
                }
                foreach (var iteratorVariable2 in first)
                {
                    var path = Path.Combine(setupInformation.ApplicationBase, iteratorVariable2);
                    if (Directory.Exists(path))
                    {
                        var iteratorVariable4 =
                            Directory.GetFiles(path, "*.dll").Concat(Directory.GetFiles(path, "*.exe"));
                        foreach (var iteratorVariable5 in iteratorVariable4)
                        {
                            Assembly iteratorVariable6;
                            try
                            {
                                iteratorVariable6 = Assembly.Load(AssemblyName.GetAssemblyName(iteratorVariable5));
                            }
                            catch (BadImageFormatException)
                            {
                                continue;
                            }
                            yield return iteratorVariable6;
                        }
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

    }
}
