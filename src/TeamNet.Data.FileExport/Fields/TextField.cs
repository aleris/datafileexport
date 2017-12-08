// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using TeamNet.Data.FileExport.Dbf;

namespace TeamNet.Data.FileExport.Fields
{
    /// <summary>
    /// Represents a character field.
    /// </summary>
    public class TextField : FieldSkeleton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class with data source field name the same as the field name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="totalSize">The field total size.</param>
        public TextField(string name, byte totalSize)
            : base(name, totalSize, 0)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class with data source field name the same as the field name and with the default size of 255.
        /// </summary>
        /// <param name="name">The name.</param>
        public TextField(string name)
            : base(name, MaximumTotalSize, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="sourceName">The data source field name.</param>
        public TextField(string name, byte totalSize, string sourceName) 
            : base(name, totalSize, 0, sourceName)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class with the default size of 255.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sourceName">The data source field name.</param>
        public TextField(string name, string sourceName)
            : base(name, MaximumTotalSize, 0, sourceName)
        { }

        /// <summary>
        /// Gets the maximum total size for character fields.
        /// </summary>
        public const byte MaximumTotalSize = 255;

        /// <summary>
        /// Gets the character writer instance. 
        /// </summary>
        /// <returns>
        /// A new instance of a <see cref="DbfCharacterFieldWriter"/>.
        /// </returns>
        protected override IFieldWriter GetFieldWriterInstance(IDataFileExportFormatFactory exportFormatFactory)
        {
            return exportFormatFactory.CreateTextFieldWriter(this);
        }
    }
}
