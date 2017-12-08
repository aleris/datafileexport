// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using TeamNet.Data.FileExport.Dbf;

namespace TeamNet.Data.FileExport.Fields
{
    /// <summary>
    /// Represents a numeric field.
    /// </summary>
    public class NumericField : FieldSkeleton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumericField"/> class with data source field name the same as the field name.
        /// </summary>
        /// <param name="name">The field name and the data source field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="decimalPlaces">The field decimal places.</param>
        public NumericField(string name, byte totalSize, byte decimalPlaces)
            : base(name, totalSize, decimalPlaces)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericField"/> class with data source field name the same as the field name and with the default total size of 255 and 15 decimal places.
        /// </summary>
        /// <param name="name">The field name and the data source field name.</param>
        public NumericField(string name)
            : base(name, MaximumTotalSize, MaximumDecimalPlaces)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="totalSize">The total size.</param>
        /// <param name="decimalPlaces">The decimal places.</param>
        /// <param name="sourceName">The data source field name.</param>
        public NumericField(string name, byte totalSize, byte decimalPlaces, string sourceName) 
            : base(name, totalSize, decimalPlaces, sourceName)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericField"/> class with the default total size of 255 and 15 decimal places.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sourceName">The data source field name.</param>
        public NumericField(string name, string sourceName)
            : base(name, MaximumTotalSize, MaximumDecimalPlaces, sourceName)
        { }

        /// <summary>
        /// Gets the maximum total size for numeric fields.
        /// </summary>
        public const byte MaximumTotalSize = 255;

        /// <summary>
        /// Gets the maximum decimal places for numeric fields.
        /// </summary>
        public const byte MaximumDecimalPlaces = 15;

        /// <summary>
        /// Gets the field writer instance. 
        /// </summary>
        /// <returns>
        /// A new instance of a <see cref="DbfNumericFieldWriter"/>.
        /// </returns>
        protected override IFieldWriter GetFieldWriterInstance(IDataFileExportFormatFactory exportFormatFactory)
        {
            return exportFormatFactory.CreateNumericFieldWriter(this);
        }
    }
}
