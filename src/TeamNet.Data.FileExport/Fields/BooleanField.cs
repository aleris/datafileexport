// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using TeamNet.Data.FileExport.Dbf;
using TeamNet.Data.FileExport.Dbf.Structures;

namespace TeamNet.Data.FileExport.Fields
{
    /// <summary>
    /// Represents a logical field.
    /// </summary>
    public class BooleanField : FieldSkeleton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanField"/> class with data source field name the same as the field name.
        /// </summary>
        /// <param name="name">The name.</param>
        public BooleanField(string name)
            : base(name, DbfConstants.LogicalFieldLength, 0)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sourceName">The data source field name.</param>
        public BooleanField(string name, string sourceName)
            : base(name, DbfConstants.LogicalFieldLength, 0, sourceName)
        {}

        /// <summary>
        /// Gets the field writer instance.
        /// </summary>
        /// <returns>
        /// A new instance of a <see cref="DbfLogicalFieldWriter"/>.
        /// </returns>
        protected override IFieldWriter GetFieldWriterInstance(IDataFileExportFormatFactory exportFormatFactory)
        {
            return exportFormatFactory.CreateBooleanFieldWriter(this);
        }
    }
}
