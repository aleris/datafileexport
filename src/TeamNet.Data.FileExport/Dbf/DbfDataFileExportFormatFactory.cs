// (c) Copyright Adrian Toșcă
// This source is subject to the MIT licence.
// All other rights reserved.
using System;
using System.IO;

namespace TeamNet.Data.FileExport.Dbf
{
    /// <summary>
    /// Defines methods for creating writers for DBF data file export format.
    /// </summary>
    public class DbfDataFileExportFormatFactory : IDataFileExportFormatFactory
    {
        internal DbfDataFileWriter DbfDataFileWriter { get; private set; }

        /// <summary>
        /// Creates a DBF data file export writer.
        /// </summary>
        /// <param name="stream">The output stream to which the writer will write to.</param>
        /// <returns>A DBF data file writer.</returns>
        public IDataFileWriter CreateWriter(Stream stream)
        {
            if (null != this.DbfDataFileWriter)
                throw new InvalidOperationException("Data file export format factory instance cannot be reused.");
            return this.DbfDataFileWriter = new DbfDataFileWriter(stream);
        }

        /// <summary>
        /// Creates a <see cref="DbfCharacterFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="DbfCharacterFieldWriter"/> field writer</returns>
        public IFieldWriter CreateTextFieldWriter(IField field)
        {
            return new DbfCharacterFieldWriter(this.DbfDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="DbfNumericFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="DbfNumericFieldWriter"/> field writer</returns>
        public IFieldWriter CreateNumericFieldWriter(IField field)
        {
            return new DbfNumericFieldWriter(this.DbfDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="DbfDateFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="DbfDateFieldWriter"/> field writer</returns>
        public IFieldWriter CreateDateFieldWriter(IField field)
        {
            return new DbfDateFieldWriter(this.DbfDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="DbfLogicalFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="DbfLogicalFieldWriter"/> field writer</returns>
        public IFieldWriter CreateBooleanFieldWriter(IField field)
        {
            return new DbfLogicalFieldWriter(this.DbfDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="DbfCharacterFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="DbfCharacterFieldWriter"/> field writer</returns>
        public IFieldWriter CreateGenericFieldWriter(IField field)
        {
            return new DbfCharacterFieldWriter(this.DbfDataFileWriter, field);
        }
    }
}
