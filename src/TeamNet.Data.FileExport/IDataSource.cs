// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
namespace TeamNet.Data.FileExport
{
    /// <summary>
    /// Defines a data source for the DBF writer.
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// Gets the row count in the data source.
        /// </summary>
        /// <value>The row count.</value>
        int RowCount { get; }

        /// <summary>
        /// Gets the data value for the field name at the specified row index.
        /// </summary>
        /// <param name="rowIndex">The row index.</param>
        /// <param name="name">The field name.</param>
        /// <returns>The value for the field name at the specified row index.</returns>
        object GetData(int rowIndex, string name);
    }
}