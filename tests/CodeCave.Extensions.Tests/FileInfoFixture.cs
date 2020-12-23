using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace CodeCave.Extensions.Tests
{
    /// <summary>
    /// A collection of extensions for <see cref="FileInfo"/> class.
    /// </summary>
    public class FileInfoFixture
    {
        private readonly string encodingFolderPath;

        static FileInfoFixture()
        {
#if !NET45
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
        }

        /// <summary>Initializes a new instance of the <see cref="FileInfoFixture" /> class.</summary>
        public FileInfoFixture()
        {
            encodingFolderPath = Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName, "Resources", "Encoding");
        }

        /// <summary>Gets file name to encoding mapping.</summary>
        /// <returns>The file name to encoding mapping.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1204:Static elements should appear before instance elements", Justification = "<Pending>")]
        public static IEnumerable<object[]> GetFileNameToEncoding() => new List<object[]>
        {
            new object[] { "ANSI Latin1.txt", Encoding.GetEncoding(1252) },
            new object[] { "UTF16-BE.txt", Encoding.GetEncoding(1201) },
            new object[] { "UTF16-LE.txt", Encoding.Unicode },
            new object[] { "UTF32-BE.txt", Encoding.GetEncoding(12001) },
            new object[] { "UTF7.txt", new UTF8Encoding(false) },
            new object[] { "UTF8-Without-BOM.txt", new UTF8Encoding(false) },
            new object[] { "UTF8.txt", new UTF8Encoding(true) },
        };

        /// <summary>Checks if encoding is guessed correctly.</summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="desiredEncoding">The desired encoding.</param>
        /// <exception cref="ArgumentException">File path cannot be null or whitespace - filePath.</exception>
        /// <exception cref="ArgumentNullException">desiredEncoding.</exception>
        [Theory]
        [MemberData(nameof(GetFileNameToEncoding))]
        public void CheckIfEncodingIsGuessedCorrectly(string filePath, Encoding desiredEncoding)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace", nameof(filePath));
            }

            if (desiredEncoding is null)
            {
                throw new ArgumentNullException(nameof(desiredEncoding));
            }

            // Arrange
            var fileInfo = new FileInfo(Path.Combine(encodingFolderPath, filePath));

            // Act
            var result = fileInfo.TryGetEncoding(out var encoding);

            // Assert
            Assert.True(result);
            Assert.Equal(desiredEncoding, encoding);
        }
    }
}
