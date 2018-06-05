#region

using System.Diagnostics;
using System.Text;

#endregion

// ReSharper disable CheckNamespace

namespace System.IO
{
    /// <inheritdoc />
    /// <summary>
    /// Useful for redirecting Console.WriteLine-like operations to Debug.WriteLine
    /// </summary>
    /// <seealso cref="T:System.IO.TextWriter" />
    public class ConsoleToDebug : TextWriter
    {
        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, returns the character encoding in which the output is written.
        /// </summary>
        public override Encoding Encoding => Encoding.UTF8;

        /// <summary>
        /// Writes a character to the text string or stream.
        /// </summary>
        /// <param name="value">The character to write to the text stream.</param>
        public override void Write(char value)
        {
            Debug.Write(value);
        }

        /// <inheritdoc />
        /// <summary>
        /// Writes a string followed by a line terminator to the text string or stream.
        /// </summary>
        /// <param name="value">The string to write. If value is null, only the line terminator is written.</param>
        public override void WriteLine(string value)
        {
            Debug.WriteLine(value);
        }
    }
}
