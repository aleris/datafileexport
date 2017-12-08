// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using TeamNet.Data.FileExport.Dbf;

namespace TeamNet.Data.FileExport.Fields
{
    /// <summary>
    /// Represents a generic field.
    /// </summary>
    public class GenericField : FieldSkeleton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericField"/> class with data source field name the same as the field name.
        /// </summary>
        /// <param name="name">The field name and the data source field name.</param>
        public GenericField(string name)
            : base(name, NumericField.MaximumTotalSize, NumericField.MaximumDecimalPlaces)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sourceName">The data source field name.</param>
        public GenericField(string name, string sourceName)
            : base(name, NumericField.MaximumTotalSize, NumericField.MaximumDecimalPlaces, sourceName)
        { }

        /// <summary>
        /// Gets the field writer instance. 
        /// </summary>
        /// <returns>
        /// A new instance of a <see cref="IFieldWriter"/>.
        /// </returns>
        protected override IFieldWriter GetFieldWriterInstance(IDataFileExportFormatFactory exportFormatFactory)
        {
            return exportFormatFactory.CreateGenericFieldWriter(this);
        }
    }
}
