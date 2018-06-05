#region

using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;

#endregion

// ReSharper disable CheckNamespace

namespace System.IO
{
    /// <inheritdoc />
    /// <summary>
    /// Useful for redirecting Console.WriteLine-like operations to log
    /// </summary>
    /// <seealso cref="TextWriter" />
    public class ConsoleToLogWriter : TextWriter
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.IO.ConsoleToLogWriter" /> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        public ConsoleToLogWriter(ILoggerFactory loggerFactory)
        {
            var assembly =
#if NETSTANDARD1_6
                Assembly.GetEntryAssembly();
#else
                Assembly.GetExecutingAssembly();
#endif

            _logger = loggerFactory.CreateLogger(assembly.FullName);
        }

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, returns the character encoding in which the output is written.
        /// </summary>
        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char value)
        {
            WriteLine(value);
        }

        /// <inheritdoc />
        /// <summary>
        /// Writes a string followed by a line terminator to the text string or stream.
        /// </summary>
        /// <param name="value">The string to write. If value is null, only the line terminator is written.</param>
        public override void WriteLine(string value)
        {
            _logger.LogWarning(value);
        }
    }
}
