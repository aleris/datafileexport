// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;
using System.IO;

namespace TeamNet.Data.FileExport.Csv
{
    /// <summary>
    /// Defines methods for creating writers for CSV data file export format.
    /// </summary>
    /// <remarks>
    /// Because CSV files are type unaware, all field writers are of <see cref="CsvGenericFieldWriter"/> type.
    /// </remarks>
    public class CsvDataFileExportFormatFactory : IDataFileExportFormatFactory
    {
		/// <summary>
		/// The default export options for FixedLength files.
		/// </summary>
		public static IDataFileExportOptions DefaultCsvDataFileExportOptions =
			new TextDataFileExportOptions()
			{
				IncludeFileHeader = true,
				Separator = ","
			};

        internal CsvDataFileWriter CsvDataFileWriter { get; private set; }

		/// <summary>
		/// Gets or sets the export options.
		/// </summary>
		public IDataFileExportOptions Options { get; set; }

		/// <summary>
		/// Constructor for the CsvDataFileExportFormatFactory class
		/// </summary>
		public CsvDataFileExportFormatFactory()
		{
			this.Options = DefaultCsvDataFileExportOptions;
		}

        /// <summary>
        /// Creates a CSV data file export writer.
        /// </summary>
        /// <param name="stream">The output stream to which the writer will write to.</param>
        /// <returns>A CSV data file writer.</returns>
        public IDataFileWriter CreateWriter(Stream stream)
        {
            if (null != this.CsvDataFileWriter)
                throw new InvalidOperationException("Data file export format instance factory cannot be reused.");

            return this.CsvDataFileWriter = new CsvDataFileWriter(stream, this.Options);
        }

        /// <summary>
        /// Creates a <see cref="CsvGenericFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="CsvGenericFieldWriter"/> field writer</returns>
        public IFieldWriter CreateTextFieldWriter(IField field)
        {
            return new CsvGenericFieldWriter(this.CsvDataFileWriter, field);
        }

        /// <summary>
		/// Creates a <see cref="CsvNumericFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
		/// <returns>A <see cref="CsvNumericFieldWriter"/> field writer</returns>
        public IFieldWriter CreateNumericFieldWriter(IField field)
        {
            return new CsvNumericFieldWriter(this.CsvDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="CsvGenericFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="CsvGenericFieldWriter"/> field writer</returns>
        public IFieldWriter CreateDateFieldWriter(IField field)
        {
            return new CsvDateFieldWriter(this.CsvDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="CsvGenericFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="CsvGenericFieldWriter"/> field writer</returns>
        public IFieldWriter CreateBooleanFieldWriter(IField field)
        {
            return new CsvGenericFieldWriter(this.CsvDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="CsvGenericFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="CsvGenericFieldWriter"/> field writer</returns>
        public IFieldWriter CreateGenericFieldWriter(IField field)
        {
            return new CsvGenericFieldWriter(this.CsvDataFileWriter, field);
        }
    }
}
