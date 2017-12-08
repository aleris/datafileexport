// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using System;
using TeamNet.Data.FileExport.Dbf.Structures;

namespace TeamNet.Data.FileExport.Fields
{
    /// <summary>
    /// Provides a skeleton implementation of IField interface.
    /// </summary>
    public abstract class FieldSkeleton : IField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldSkeleton"/> class with data source field name the same as the field name.
        /// </summary>
        /// <param name="name">The field name and the data source field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="decimalPlaces">The field decimal places for numeric types. For other types it should be 0.</param>
        protected FieldSkeleton(string name, byte totalSize, byte decimalPlaces)
            : this(name, totalSize, decimalPlaces, name)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldSkeleton"/> class.
        /// </summary>
        /// <param name="name">The field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="decimalPlaces">The field decimal places for numeric types. For other types it should be 0.</param>
        /// <param name="sourceName">Name of the source data field name.</param>
        protected FieldSkeleton(string name, byte totalSize, byte decimalPlaces, string sourceName)
        {
            Guard.Against<ArgumentNullException>(name == null, "name");
            Guard.Against<ArgumentNullException>(sourceName == null, "sourceName");
            Guard.Against<ArgumentException>(totalSize <= decimalPlaces, "totalSize shoud be bigger than decimalPlaces");
            Guard.Against<ArgumentException>(decimalPlaces > DbfConstants.FieldMaximumDecimals, "decimalPlaces should be maximum 15");

            this.Name = name;
            this.TotalSize = totalSize;
            this.DecimalPlaces = decimalPlaces;
            this.SourceName = sourceName;
        }

        /// <summary>
        /// Gets or sets the DBF field header name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the source name.
        /// </summary>
        /// <value>The source name.</value>
        public string SourceName { get; set; }

        /// <summary>
        /// Gets or sets the total size of the field values. For numeric type this include the . decimal separator.
        /// </summary>
        /// <value>The total size.</value>
        public byte TotalSize { get; set; }

        /// <summary>
        /// Gets or sets the decimal places for numeric types. For other types the value is 0.
        /// </summary>
        /// <value>The decimal places.</value>
        public byte DecimalPlaces { get; set; }

        /// <summary>
        /// Gets the field writer instance. Should be overriden to return a new instance of the field writer. The instance is cached by the <see cref="FieldSkeleton"/> class.
        /// </summary>
        /// <returns>A new instance of a <see cref="IFieldWriter"/> implementation.</returns>
        protected abstract IFieldWriter GetFieldWriterInstance(IDataFileExportFormatFactory exportFormatFactory);

        /// <summary>
        /// Gets the cached field writer.
        /// </summary>
        /// <returns>A IFieldWriter.</returns>
        public IFieldWriter GetFieldWriter(IDataFileExportFormatFactory exportFormatFactory)
        {
            if (null == _fieldWriter || exportFormatFactory != _cachedFormat)
            {
                _cachedFormat = exportFormatFactory;
                _fieldWriter = GetFieldWriterInstance(exportFormatFactory);
            }
            return _fieldWriter;
        }

        private IFieldWriter _fieldWriter;
        private IDataFileExportFormatFactory _cachedFormat;
    }
}
