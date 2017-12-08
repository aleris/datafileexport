// (c) Copyright TeamNet International
// This source is subject to the GPL-2 licence.
// See http://www.gnu.org/licenses/gpl-2.0.html
// All other rights reserved.
using System;
using System.IO;

namespace TeamNet.Data.FileExport.OfficeXml
{
    /// <summary>
    /// Defines methods for creating writers for Office XML data file export format.
    /// </summary>
    public class OfficeXmlDataFileExportFormatFactory : IDataFileExportFormatFactory
    {
        internal OfficeXmlDataFileWriter OfficeXmlDataFileWriter { get; private set; }

        /// <summary>
        /// Creates a Office XML data file export writer.
        /// </summary>
        /// <param name="stream">The output stream to which the writer will write to.</param>
        /// <returns>A Office XML data file writer.</returns>
        public IDataFileWriter CreateWriter(Stream stream)
        {
            if (null != this.OfficeXmlDataFileWriter)
                throw new InvalidOperationException("Data file export format factory instance cannot be reused.");

            return this.OfficeXmlDataFileWriter = new OfficeXmlDataFileWriter(stream);
        }

        /// <summary>
        /// Creates a <see cref="OfficeXmlTextFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="OfficeXmlTextFieldWriter"/> field writer</returns>
        public IFieldWriter CreateTextFieldWriter(IField field)
        {
            return new OfficeXmlTextFieldWriter(this.OfficeXmlDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="OfficeXmlNumberFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="OfficeXmlNumberFieldWriter"/> field writer</returns>
        public IFieldWriter CreateNumericFieldWriter(IField field)
        {
            return new OfficeXmlNumberFieldWriter(this.OfficeXmlDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="OfficeXmlDateTimeFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="OfficeXmlDateTimeFieldWriter"/> field writer</returns>
        public IFieldWriter CreateDateFieldWriter(IField field)
        {
            return new OfficeXmlDateTimeFieldWriter(this.OfficeXmlDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="OfficeXmlGeneralFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="OfficeXmlGeneralFieldWriter"/> field writer</returns>
        public IFieldWriter CreateBooleanFieldWriter(IField field)
        {
            return new OfficeXmlGeneralFieldWriter(this.OfficeXmlDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="OfficeXmlGeneralFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="OfficeXmlGeneralFieldWriter"/> field writer</returns>
        public IFieldWriter CreateGenericFieldWriter(IField field)
        {
            return new OfficeXmlGeneralFieldWriter(this.OfficeXmlDataFileWriter, field);
        }
    }
}
