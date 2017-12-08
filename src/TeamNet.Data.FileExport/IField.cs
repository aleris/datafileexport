// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
namespace TeamNet.Data.FileExport
{
    /// <summary>
    /// Defines a DBF field.
    /// </summary>
    public interface IField
    {
        /// <summary>
        /// Gets or sets the DBF field header name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the source field name.
        /// </summary>
        /// <value>The source field name.</value>
        /// <remarks>
        /// The source field name is used to get data from a <see cref="IDataSource"/>.
        /// </remarks>
        string SourceName { get; set; }

        /// <summary>
        /// Gets or sets the total size of the field values. For numeric type this include the . decimal separator.
        /// </summary>
        /// <value>The total size.</value>
        byte TotalSize { get; set; }

        /// <summary>
        /// Gets or sets the decimal places for numeric types. For other types the value is 0.
        /// </summary>
        /// <value>The decimal places.</value>
        byte DecimalPlaces { get; set; }

        /// <summary>
        /// Gets the field writer.
        /// </summary>
        /// <returns>A IFieldWriter.</returns>
        IFieldWriter GetFieldWriter(IDataFileExportFormatFactory exportFormatFactory);
    }
}
