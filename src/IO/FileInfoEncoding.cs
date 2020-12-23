using System.Text;

// ReSharper disable once CheckNamespace
namespace System.IO
{
    /// <summary>
    /// Extensions for <see cref="FileInfo"/> class.
    /// </summary>
    public static class FileInfoEncoding
    {
        static FileInfoEncoding()
        {
#if !NET45
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
        }

        /// <summary>
        /// Tries the get file encoding.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">file.</exception>
        public static bool TryGetEncoding(this FileInfo file, out Encoding encoding)
        {
            if (file == null || !file.Exists)
            {
                throw new ArgumentException($"{nameof(file)} must be a valid path to a file!");
            }

            var bytes = new byte[10];
            using (var fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
            {
                fs.Read(bytes, 0, 10);
#if !NETSTANDARD1_6
                fs.Close();
#endif
            }

            if (bytes.TryGetEncoding(out encoding))
            {
                return true;
            }

            bytes = File.ReadAllBytes(file.FullName);
            switch (bytes)
            {
                case var _ when bytes.IsInUtf8():
                    encoding = new UTF8Encoding(false); // UTF-8 without BOM
                    break;

                case var _ when bytes.IsInAnsiLatin1():
                    encoding = Encoding.GetEncoding(1252); // ANSI Latin
                    break;

                default:
                    encoding = null;
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="throwIfNotDetected">if set to <c>true</c> [throws an exception if encoding is not detected].</param>
        /// <returns>
        /// Detected file encoding or null detection failed.
        /// </returns>
        /// <exception cref="ArgumentException">File must be a valid path to a file.</exception>
        /// <exception cref="InvalidDataException">Unable to detect encoding automatically of the following file.</exception>
        public static Encoding GetEncoding(this FileInfo file, bool throwIfNotDetected = false)
        {
            if (file == null || !file.Exists)
            {
                throw new ArgumentException($"{nameof(file)} must be a valid path to a file!");
            }

            var encoding = file.TryGetEncoding(out var fileEncoding)
                ? fileEncoding
                : null;

            if (encoding == null && throwIfNotDetected)
            {
                throw new InvalidDataException(
                    $"Unable to detect encoding automatically of the following file: {file.FullName}. " +
                    "Most likely it's a non-Latin ANSI, e.g. ANSI Cyrillic, Hebrew, Arabic, Greek, Turkish, Vietnamese etc");
            }

            return encoding;
        }

        /// <summary>
        /// Determines whether [is in ANSI latin1] [the specified thresh hold].
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="mangledCharThreshold">The threshold of mangled characters.</param>
        /// <returns>
        ///   <c>true</c> if [file is ANSI Latin1-encoded] and [the number of mangled characters is lower than specified threshold]; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">file.</exception>
        public static bool IsInAnsiLatin1(this FileInfo file, double mangledCharThreshold = 60.0)
        {
            if (file == null || !file.Exists)
            {
                throw new ArgumentException($"{nameof(file)} must be a valid path to a file!");
            }

            var textBytes = File.ReadAllBytes(file.FullName);
            return textBytes.IsInAnsiLatin1(mangledCharThreshold);
        }

        /// <summary>Determines whether file [is in UTF-8].</summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if[the specified file] [is in UTF-8]; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentException">file.</exception>
        public static bool IsInUtf8(this FileInfo file)
        {
            if (file == null || !file.Exists)
            {
                throw new ArgumentException($"{nameof(file)} must be a valid path to a file!");
            }

            return File.ReadAllBytes(file.FullName).IsInUtf8();
        }

        /// <summary>
        /// Determines whether [has UTF-8 BOM marker].
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if [the specified file] [has UTF-8 BOM marker]; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">file.</exception>
        public static bool HasBomMarker(this FileInfo file)
        {
            if (file == null || !file.Exists)
            {
                throw new ArgumentException($"{nameof(file)} must be a valid path to a file!");
            }

            var buffer = new byte[10];
            using var fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            fs.Read(buffer, 0, 10);
#if !NETSTANDARD1_6
            fs.Close();
#endif

            return buffer.HasBomMarker();
        }
    }
}
