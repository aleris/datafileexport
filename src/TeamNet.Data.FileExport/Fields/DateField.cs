// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using TeamNet.Data.FileExport.Dbf;

namespace TeamNet.Data.FileExport.Fields
{
    /// <summary>
    /// Represents a date field.
    /// </summary>
    public class DateField : FieldSkeleton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateField"/> class with data source field name the same as the field name.
        /// </summary>
        /// <param name="name">The name.</param>
        public DateField(string name)
            : base(name, 8, 0)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="DateField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sourceName">The data source field name.</param>
        public DateField(string name, string sourceName) 
            : base(name, 8, 0, sourceName)
        {}

        /// <summary>
        /// Gets the field writer instance. 
        /// </summary>
        /// <returns>
        /// A new instance of a <see cref="DbfDateFieldWriter"/>.
        /// </returns>
        protected override IFieldWriter GetFieldWriterInstance(IDataFileExportFormatFactory exportFormatFactory)
        {
            return exportFormatFactory.CreateDateFieldWriter(this);
        }
    }
}
