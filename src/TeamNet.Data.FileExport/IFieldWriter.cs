// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
namespace TeamNet.Data.FileExport
{
    /// <summary>
    /// Represents a writer that can write field header and values to a <see cref="IStreamWriter"/>.
    /// </summary>
    public interface IFieldWriter
    {
        /// <summary>
        /// Writes the field header.
        /// </summary>
        /// <param name="offsetInRecord">The offset in record.</param>
        void WriteHeader(int offsetInRecord);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        void WriteValue(object value);
    }
}