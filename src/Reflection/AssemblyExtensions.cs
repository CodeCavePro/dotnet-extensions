﻿using System.IO;
using System.Linq;

// ReSharper disable once CheckNamespace

namespace System.Reflection
{
    /// <summary>
    /// Extensions for <see cref="Assembly"/> class.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets the resource names.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static string[] GetResourceNames(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return assembly?.GetManifestResourceNames();
        }

        /// <summary>
        /// Gets the resource stream.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="resourceName">Name of the resource to get stream for.</param>
        /// <returns>
        /// Steam representing the given resource.
        /// </returns>
        /// <exception cref="ArgumentException">Ambiguous name, cannot identify resource - resName.</exception>
        public static Stream GetResourceStream(this Assembly assembly, string resourceName)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentException($"'{nameof(resourceName)}' cannot be null or whitespace", nameof(resourceName));
            }

            var resources = assembly.GetResourceNames();
            var possibleCandidates = resources.Where(s => s.Contains(resourceName)).ToArray();
            return possibleCandidates.Length switch
            {
                0 => null,
                1 => assembly.GetManifestResourceStream(possibleCandidates[0]),
                _ => throw new ArgumentException($"Could not identify resource with the following name: '{resourceName}'", nameof(resourceName)),
            };
        }

        /// <summary>
        /// Gets the entry assembly's resources.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <value>
        /// The entry assembly's resources.
        /// </value>
        /// <returns>The assembly resource stream.</returns>
        public static Stream GetEntryAssemblyResource(this string resourceName)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentNullException(nameof(resourceName));
            }

            return Assembly.GetEntryAssembly().GetResourceStream(resourceName);
        }

        /// <summary>
        /// Gets the resource from the assembly of a give type.
        /// </summary>
        /// <param name="typeInfo">The type information.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>The stream of the resource name.</returns>
        /// <exception cref="ArgumentNullException">
        /// typeInfo
        /// or
        /// resourceName.
        /// </exception>
        public static Stream GetTypesAssemblyResource(this TypeInfo typeInfo, string resourceName)
        {
            if (typeInfo == null)
            {
                throw new ArgumentNullException(nameof(typeInfo));
            }

            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentNullException(nameof(resourceName));
            }

            return typeInfo.Assembly.GetResourceStream(resourceName);
        }

#if !NETSTANDARD1_6

        /// <summary>Gets the executing assembly's resources.</summary>
        /// <param name="resourceName">The name of the resource.</param>
        /// <value>The executing assembly's resources.</value>
        public static Stream GetExecutingAssemblyResource(this string resourceName) => Assembly.GetExecutingAssembly().GetResourceStream(resourceName);

        /// <summary>Gets the calling assembly's resources.</summary>
        /// <param name="resourceName">The name of the resource.</param>
        /// <value>The calling assembly's resources.</value>
        public static Stream GetCallingAssemblyResource(this string resourceName) => Assembly.GetCallingAssembly().GetResourceStream(resourceName);
#endif
    }
}
