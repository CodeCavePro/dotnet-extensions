using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// A collection of extensions for <see cref="byte" /> arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        private static readonly Regex unicodeLetters = new Regex(@"\p{L}", RegexOptions.Compiled | RegexOptions.Multiline);
        private static readonly Regex ansiLatin1Mangled = new Regex(@"[ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýÿŸ]", RegexOptions.Compiled | RegexOptions.Multiline);

        static ByteArrayExtensions()
        {
#if !NET45
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
        }

        /// <summary>Tries the get encoding.</summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="ArgumentNullException">bytes.</exception>
        public static bool TryGetEncoding(this byte[] bytes, out Encoding encoding)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            switch (bytes)
            {
                case var utf7 when utf7[0] == 0x2B && utf7[1] == 0x2F && utf7[2] == 0x76:
                    encoding = Encoding.UTF7; // UTF-7
                    break;

                case var utf32be when utf32be[0] == 0x00 && utf32be[1] == 0x00 && utf32be[2] == 0xFE && utf32be[3] == 0xFF:
                    encoding = Encoding.GetEncoding(12001); // UTF-32, big endian byte order; available only to managed applications
                    break;

                case var utf32le when utf32le[0] == 0xFF && utf32le[1] == 0xFE && utf32le[2] == 0x00 && utf32le[3] == 0x00:
                    encoding = Encoding.GetEncoding(12000); // UTF-32, little endian byte order; available only to managed applications
                    break;

                case var unicode when unicode[0] == 0xFE && unicode[1] == 0xFF:
                    encoding = Encoding.GetEncoding(1201); // 1201 unicodeFFFE Unicode (UTF-16BE aka Unicode big endian)
                    break;

                case var unicode when unicode[0] == 0xFF && unicode[1] == 0xFE:
                    encoding = Encoding.GetEncoding(1200); // 1200 UTF-16 Unicode (UTF-16LE aka Unicode little endian)
                    break;

                case var _ when bytes.HasBomMarker():
                    encoding = new UTF8Encoding(true); // UTF-8 with BOM
                    break;

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
        /// Determines whether [has UTF-8 BOM  arker] [the specified bytes].
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>
        ///   <c>true</c> if [the specified bytes] [has UTF-8 BOM  marker]; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">bytes.</exception>
        public static bool HasBomMarker(this byte[] bytes)
        {
            if (bytes == null || !bytes.Any())
            {
                throw new ArgumentException($"{nameof(bytes)} must be a non-empty array of bytes!");
            }

            return bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF;
        }

        /// <summary>Determines whether [file is in ANSI latin1] [the specified mangled character threshold].</summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="mangledCharThreshold">The mangled character threshold.</param>
        /// <returns>
        ///   <c>true</c> if [is in ANSI latin1] [the specified mangled character threshold]; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">bytes.</exception>
        public static bool IsInAnsiLatin1(this byte[] bytes, double mangledCharThreshold = 60.0)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var ansiLatin1Encoding = Encoding.GetEncoding(1252);
            var ansiText = ansiLatin1Encoding.GetString(bytes);

            var unicodeLettersFound = unicodeLetters.Matches(ansiText);
            var ansiMangledFound = ansiLatin1Mangled.Matches(ansiText);
            var matchRate = ansiMangledFound.Count * 100 / unicodeLettersFound.Count;
            return matchRate <= mangledCharThreshold;
        }

        /// <summary>
        /// Determines whether [is in UTF-8].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if[the specified file] [is in UTF-8]; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">file.</exception>
        public static bool IsInUtf8(this byte[] bytes)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            try
            {
                using var ms = new MemoryStream(bytes);
                using var streamReader = new StreamReader(ms, new UTF8Encoding(bytes.HasBomMarker(), true), true);
                streamReader.ReadToEnd();
                return true;
            }
            catch (DecoderFallbackException)
            {
                return false;
            }
        }
    }
}
