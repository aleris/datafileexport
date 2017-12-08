// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
namespace TeamNet.Data.FileExport
{
    /// <summary>
    /// Defines a writer that writes common values to a stream.
    /// </summary>
    public interface IStreamWriter
    {
        /// <summary>
        /// Writes the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        void Write(byte[] buffer);

        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        void Write(byte value);

        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        void Write(int value);

        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        void Write(short value);

        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        void Write(string value);

        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        void Write(char value);
    }
}