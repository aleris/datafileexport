// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;
using System.IO;

namespace TeamNet.Data.FileExport.FixedLength
{
    /// <summary>
    /// Defines methods for creating writers for FixedLength data file export format.
    /// </summary>
    /// <remarks>
    /// Because FixedLength files are type unaware, all field writers are of <see cref="FixedLengthGenericFieldWriter"/> type.
    /// </remarks>
    public class FixedLengthDataFileExportFormatFactory : IDataFileExportFormatFactory
    {
		/// <summary>
		/// The default export options for FixedLength files.
		/// </summary>
		public static IDataFileExportOptions DefaultFixedLengthDataFileExportOptions =
			new TextDataFileExportOptions()
				{
					IncludeFileHeader = true,
					Separator = string.Empty
				};

        internal FixedLengthDataFileWriter FixedLengthDataFileWriter { get; private set; }

		/// <summary>
		/// Gets or sets the export options.
		/// </summary>
		public IDataFileExportOptions Options { get; set; }

		/// <summary>
		/// Constructor for the FixedLengthDataFileExportFormatFactory class
		/// </summary>
		public FixedLengthDataFileExportFormatFactory()
		{
			this.Options = DefaultFixedLengthDataFileExportOptions;
		}

        /// <summary>
        /// Creates a FixedLength data file export writer.
        /// </summary>
        /// <param name="stream">The output stream to which the writer will write to.</param>
        /// <returns>A FixedLength data file writer.</returns>
        public IDataFileWriter CreateWriter(Stream stream)
        {
            if (null != this.FixedLengthDataFileWriter)
                throw new InvalidOperationException("Data file export format instance factory cannot be reused.");

            return this.FixedLengthDataFileWriter = new FixedLengthDataFileWriter(stream, this.Options);
        }

        /// <summary>
        /// Creates a <see cref="FixedLengthGenericFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="FixedLengthGenericFieldWriter"/> field writer</returns>
        public IFieldWriter CreateTextFieldWriter(IField field)
        {
            return new FixedLengthGenericFieldWriter(this.FixedLengthDataFileWriter, field);
        }

        /// <summary>
		/// Creates a <see cref="FixedLengthNumericFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
		/// <returns>A <see cref="FixedLengthNumericFieldWriter"/> field writer</returns>
        public IFieldWriter CreateNumericFieldWriter(IField field)
        {
            return new FixedLengthNumericFieldWriter(this.FixedLengthDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="FixedLengthGenericFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="FixedLengthGenericFieldWriter"/> field writer</returns>
        public IFieldWriter CreateDateFieldWriter(IField field)
        {
            return new FixedLengthDateFieldWriter(this.FixedLengthDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="FixedLengthGenericFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="FixedLengthGenericFieldWriter"/> field writer</returns>
        public IFieldWriter CreateBooleanFieldWriter(IField field)
        {
            return new FixedLengthGenericFieldWriter(this.FixedLengthDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="FixedLengthGenericFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="FixedLengthGenericFieldWriter"/> field writer</returns>
        public IFieldWriter CreateGenericFieldWriter(IField field)
        {
            return new FixedLengthGenericFieldWriter(this.FixedLengthDataFileWriter, field);
        }
    }
}
